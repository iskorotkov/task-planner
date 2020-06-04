using System;

namespace TaskPlanner.Shared.Data.References
{
    [Flags]
    public enum ReferenceType : int
    {
        None = 0,
        Child = 1,
        Parent = 2,
        Dependency = 4,
        Dependant = 8,
        Alternative = 16,
        Similar = 32,
        TestFor = 64,
        TestedBy = 128,
        All = Child | Parent | Dependency | Dependant | Alternative | Similar | TestFor | TestedBy
    }
}
