using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Services.Canvas
{
    public class PainterConfig
    {
        public ReferenceType Types { get; set; } = ReferenceType.All;
        
        public FontInfo TitleFont { get; set; } = new FontInfo(16);
        public FontInfo DescriptionFont { get; set; } = new FontInfo(12);
        public FontInfo EdgeFont { get; set; } = new FontInfo();
        public FontInfo ComponentFont { get; set; } = new FontInfo();
    }
}
