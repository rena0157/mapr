namespace SampleMapperApp.ConsoleApp.DtoModels
{
    public class PersonModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public AddressModel AddressModel { get; set; }
    }
}