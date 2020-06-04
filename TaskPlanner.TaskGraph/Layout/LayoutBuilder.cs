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
            var positionInLine = position;
            foreach (var nodeElement in config.Elements)
            {
                if (nodeElement.NextLine)
                {
                    position += nodeElement.Offset;
                    positionInLine = position;
                }
                else
                {
                    positionInLine += nodeElement.Offset;
                }

                var dimensions = nodeElement.Dimensions;
                if (nodeElement.StretchHorizontal)
                {
                    var offsetFromLeft = nodePosition.X - positionInLine.X;
                    var availableWidth = config.Dimensions.Width - config.ContentPadding.Right;
                    dimensions.Width = availableWidth + offsetFromLeft;
                }

                yield return new RenderElement(positionInLine, dimensions);
            }
        }
    }
}
