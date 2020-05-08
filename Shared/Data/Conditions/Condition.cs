namespace TaskPlanner.Shared.Data.Conditions
{
    public class Condition
    {
        public string Attribute { get; }
        public string Operation { get; }
        public object Value { get; }

        public Condition(string attribute, string operation, object value)
        {
            Attribute = attribute;
            Operation = operation;
            Value = value;
        }
    }
}
