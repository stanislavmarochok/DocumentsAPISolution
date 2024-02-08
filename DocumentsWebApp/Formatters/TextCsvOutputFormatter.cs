using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace DocumentsAPI.Formatters
{
    public class TextCsvOutputFormatter : TextOutputFormatter
    {
        public TextCsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable<object>).IsAssignableFrom(type);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    var vals = item.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(item, null) switch
                            {
                                List<string> list => $"[{string.Join(", ", list.Select(x => $"\"{x}\""))}]",
                                Dictionary<string, string> dictionary => $"[{string.Join(", ", dictionary.Select(pair => $"\"{pair.Key}\":\"{pair.Value}\""))}]",
                                _ => pi.GetValue(item, null)?.ToString()
                            }
                        }
                    );

                    string valueLine = string.Join(",", vals.Select(v => $"\"{v.Value?.ToString()}\""));
                    buffer.AppendLine(valueLine);
                }
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
    }
}
