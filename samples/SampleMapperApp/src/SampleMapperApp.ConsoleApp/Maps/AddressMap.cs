using Mapr;
using SampleMapperApp.ConsoleApp.DataModels;
using SampleMapperApp.ConsoleApp.Domain;

namespace SampleMapperApp.ConsoleApp.Maps;

public class AddressMap : IMap<(Address address, int personId), AddressModel>, IMap<AddressModel, Address>
{
    private readonly IMapper _mapper;

    public AddressMap(IMapper mapper)
    {
        _mapper = mapper;
    }
        
    public Address Map(AddressModel source)
    {
        var zipCode = _mapper.Map<string, ZipCode>(source.ZipCode);
        return new Address(source.StreetNumber, source.StreetName, zipCode, source.Country);
    }

    public AddressModel Map((Address address, int personId) source)
    {
        var (address, personId) = source;
        return new()
        {
            PersonId = personId,
            StreetName = address.StreetName,
            Country = address.Country,
            StreetNumber = address.StreetNumber,
            ZipCode = _mapper.Map<ZipCode, string>(address.ZipCode)
        };
    }
}