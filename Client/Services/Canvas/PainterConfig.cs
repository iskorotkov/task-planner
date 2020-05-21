using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Services.Canvas
{
    public class PainterConfig
    {
        public ReferenceType Types { get; set; } = ReferenceType.All;

        public Position TitleOffset { get; set; } = new Position(10, 20);
        public Position DescriptionOffset { get; set; } = new Position(10, 40);
    }
}
