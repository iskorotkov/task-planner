using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace TaskPlanner.Client.Services.Canvas
{
    public class NodeTextFormatter
    {
        public async Task<string> ClampText(Canvas2DContext context, string text, int maxWidth)
        {
            var metrics = await context.MeasureTextAsync(text);
            if (metrics.Width <= maxWidth)
            {
                return text;
            }

            const string suffix = "...";
            var suffixMetrics = await context.MeasureTextAsync(suffix);
            maxWidth = (int) Math.Floor(maxWidth - suffixMetrics.Width);
            
            var str = string.Empty;
            var previousStr = string.Empty;
            foreach (var letter in text)
            {
                str += letter;
                var width = await context.MeasureTextAsync(str);
                if (width.Width > maxWidth)
                {
                    return previousStr;
                }

                previousStr = str;
            }

            throw new FormatterException(text);
        }
    }

    public class FormatterException : ApplicationException
    {
        public FormatterException(string input)
            : base($"Error occurred during text formatting. The text was '{input}'.")
        {
        }
    }
}
