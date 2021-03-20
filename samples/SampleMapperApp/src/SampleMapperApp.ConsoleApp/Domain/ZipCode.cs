namespace SampleMapperApp.ConsoleApp.Domain
{
    public class ZipCode
    {
        public ZipCode(string value)
        {
            // Validation...
            Value = value;
        }
        
        public string Value { get; }
    }
}