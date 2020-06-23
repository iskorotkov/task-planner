using System;

namespace TaskPlanner.Shared.Data.Sections
{
    public class UnsupportedSectionException : ApplicationException
    {
        public UnsupportedSectionException(Section section)
            : base($"Section '{section.GetType()}' is not supported.")
        {
        }
    }
}
