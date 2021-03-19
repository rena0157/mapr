namespace SampleMapperApp.ConsoleApp.Models
{
    public class Address
    {
        public Address(string streetNumber, string streetName, ZipCode zipCode, string country)
        {
            // Validation ...
            StreetNumber = streetNumber;
            StreetName = streetName;
            ZipCode = zipCode;
            Country = country;
        }
        
        public string StreetNumber { get; }
        
        public string StreetName { get; }
        
        public ZipCode ZipCode { get; }
        
        public string Country { get; }
    }
}