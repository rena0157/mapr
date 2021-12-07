namespace SampleMapperApp.ConsoleApp.Domain;

public class Person
{
    public Person(int id, string firstName, string lastName, Address address)
    {
        // Validation...
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }
        
    public int Id { get; }
        
    public string FirstName { get; }
        
    public string LastName { get; }

    public Address Address { get; }
        
    public string FullName => $"{FirstName} {LastName}";
}