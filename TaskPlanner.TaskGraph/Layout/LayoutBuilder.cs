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
            var nextLinePosition = nodePosition + padding;
            var currentLinePosition = nextLinePosition;
            foreach (var nodeElement in config.Elements)
            {
                if (nodeElement.NextLine)
                {
                    nextLinePosition += nodeElement.Offset;
                    currentLinePosition = nextLinePosition;
                    nextLinePosition = new Position(nextLinePosition.X,
                        nextLinePosition.Y + nodeElement.Dimensions.Height);
                }
                else
                {
                    currentLinePosition += nodeElement.Offset;
                }

                var dimensions = nodeElement.Dimensions;
                if (nodeElement.StretchHorizontal)
                {
                    var offsetFromLeft = currentLinePosition.X - nodePosition.X;
                    var availableWidth = config.Dimensions.Width - config.ContentPadding.Right;
                    dimensions = new Dimensions(availableWidth - offsetFromLeft, dimensions.Height);
                }

                yield return new RenderElement(currentLinePosition, dimensions);
                currentLinePosition = new Position(currentLinePosition.X + dimensions.Width, currentLinePosition.Y);
            }
        }
    }
}
