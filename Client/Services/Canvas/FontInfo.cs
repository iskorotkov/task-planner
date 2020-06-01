using System;

namespace TaskPlanner.Client.Services.Canvas
{
    public class FontInfo
    {
        public FontInfo(int size = 10, string style = "sans-serif")
        {
            Size = size;
            Style = style ?? throw new ArgumentNullException(nameof(style));
        }

        public int Size { get; }
        public string Style { get; }
        public string FontString => $"{Size}px {Style}";
    }
}
