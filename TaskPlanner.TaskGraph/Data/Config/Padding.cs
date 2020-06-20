namespace TaskPlanner.TaskGraph.Data.Config
{
    public class Padding
    {
        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Padding(int horizontal, int vertical)
        {
            Left = horizontal;
            Right = horizontal;
            
            Top = vertical;
            Bottom = vertical;
        }

        public Padding(int all)
        {
            Left = all;
            Right = all;
            Top = all;
            Bottom = all;
        }

        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
    }
}
