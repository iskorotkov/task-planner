namespace TaskPlanner.Shared.Services.Formatters
{
    public class NodeTextFormatter
    {
        public string ClampText(string text, int maxLength)
        {
            if (text.Length <= maxLength)
            {
                return text;
            }
            text = text.Substring(0, maxLength - 3).TrimEnd();
            return text + "...";
        }
    }
}
