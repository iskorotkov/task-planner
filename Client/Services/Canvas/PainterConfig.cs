using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Services.Canvas
{
    public class PainterConfig
    {
        public ReferenceType Types { get; set; } = ReferenceType.All;
        public Dimensions IconDimensions { get; set; } = new Dimensions(8, 8);
    }
}
