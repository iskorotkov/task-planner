using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Layout
{
    public class LayoutBuilder
    {
        public IEnumerable<RenderElement> Build(Position nodePosition, GraphConfig config)
        {
            var padding = new Position(config.ContentPadding.Left, config.ContentPadding.Top);
            var position = nodePosition + padding;
            foreach (var nodeElement in config.ElementsPositions)
            {
                yield return new RenderElement(position + nodeElement.Offset, nodeElement.MaxLetters);
            }
        }
    }
}
