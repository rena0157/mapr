namespace SampleMapperApp.ConsoleApp.Models
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