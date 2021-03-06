﻿using System;

namespace TaskPlanner.Shared.Data.Coordinates
{
    public class Dimensions
    {
        public Dimensions()
        {

        }

        public Dimensions(int dimensions)
        {
            Width = dimensions;
            Height = dimensions;
        }

        public Dimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public override bool Equals(object? obj)
        {
            return obj is Dimensions dimensions &&
                   Width == dimensions.Width &&
                   Height == dimensions.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }
    }
}
