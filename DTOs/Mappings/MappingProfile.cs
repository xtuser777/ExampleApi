using AutoMapper;
using ExampleApi.Models;

namespace ExampleApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, CustomerDTO>();
        CreateMap<Address, CustomerDTO>();
        CreateMap<Contact, CustomerDTO>();

        CreateMap<Customer, CreateCustomerDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, CreateCustomerDTO>().ReverseMap();
        CreateMap<Address, CreateCustomerDTO>().ReverseMap();
        CreateMap<Contact, CreateCustomerDTO>().ReverseMap();

        CreateMap<Customer, UpdateCustomerDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, UpdateCustomerDTO>().ReverseMap();
        CreateMap<Address, UpdateCustomerDTO>().ReverseMap();
        CreateMap<Contact, UpdateCustomerDTO>().ReverseMap();

        CreateMap<User, UserDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, UserDTO>();
        CreateMap<Address, UserDTO>();
        CreateMap<Contact, UserDTO>();

        CreateMap<User, CreateUserDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, CreateUserDTO>().ReverseMap();
        CreateMap<Address, CreateUserDTO>().ReverseMap();
        CreateMap<Contact, CreateUserDTO>().ReverseMap();

        CreateMap<User, UpdateUserDTO>()
            .IncludeMembers(d => d.Individual, d => d.Individual.Address, d => d.Individual.Contact)
            .ReverseMap();

        CreateMap<Individual, UpdateUserDTO>().ReverseMap();
        CreateMap<Address, UpdateUserDTO>().ReverseMap();
        CreateMap<Contact, UpdateUserDTO>().ReverseMap();
    }
}