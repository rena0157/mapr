using Mapr;
using SampleMapperApp.ConsoleApp.DataModels;
using SampleMapperApp.ConsoleApp.Domain;

namespace SampleMapperApp.ConsoleApp.Maps
{
    public class PersonMap : IMap<Person, PersonModel>, IMap<PersonModel, Person>
    {
        private readonly IMapper _mapper;

        public PersonMap(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        public PersonModel Map(Person source)
        {
            return new PersonModel
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                
                // Calling the mapper to map the address to the model.
                AddressModel = _mapper.Map<Address, AddressModel>(source.Address)
            };
        }

        /// <inheritdoc />
        public Person Map(PersonModel source)
        {
            // Convert the address model back to an address.
            var address = _mapper.Map<AddressModel, Address>(source.AddressModel);
            
            return new Person(source.Id, source.FirstName, source.LastName, address);
        }
    }
}