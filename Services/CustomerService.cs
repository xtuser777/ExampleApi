using AutoMapper;
using ExampleApi.DTOs;
using ExampleApi.Models;
using ExampleApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDTO?> Create(CreateCustomerDTO dto)
    {
        var entity = _mapper.Map<Customer>(dto);

        using var transaction = _unitOfWork.BeginTransaction;
        var address = await _unitOfWork.AddressRepository.Create(entity.Individual.Address);
        if (address is null) throw new Exception("Erro ao inserir o endereço.");
        var contact = await _unitOfWork.ContactRepository.Create(entity.Individual.Contact);
        if (contact is null) throw new Exception("Erro ao inserir o contato.");
        entity.Individual.AddressId = address.Id;
        entity.Individual.ContactId = contact.Id;
        var individual = await _unitOfWork.IndividualRepository.Create(entity.Individual);
        if (individual is null) throw new Exception("Erro ao inserir o individuo.");
        entity.IndividualId = individual.Id;
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        var customer = await _unitOfWork.CustomerRepository.Create(entity);
        await _unitOfWork.Commit(transaction);

        return customer != null ? _mapper.Map<CustomerDTO>(customer) : null;
    }

    public async Task<CustomerDTO?> Update(int id, UpdateCustomerDTO dto)
    {
        var entity = await _unitOfWork.CustomerRepository.FindOne(id);
        if (entity is null) throw new Exception("404 - Not Found");

        using var transaction = _unitOfWork.BeginTransaction;
        
        entity.Individual.Name = dto.Name;
        entity.Individual.Document = dto.Document;
        entity.Individual.Birth = dto.Birth;
        entity.Individual.Address.Street = dto.Street;
        entity.Individual.Address.Number = dto.Number;
        entity.Individual.Address.Neighborhood = dto.Neighborhood;
        entity.Individual.Address.Complement = dto.Complement;
        entity.Individual.Address.Code = dto.Code;
        entity.Individual.Address.City = dto.City;
        entity.Individual.Address.State = dto.State;
        entity.Individual.Contact.Phone = dto.Phone;
        entity.Individual.Contact.Cellphone = dto.Cellphone;
        entity.Individual.Contact.Email = dto.Email;


        var address = _unitOfWork.AddressRepository.Update(entity.Individual.Address);
        if (address is null) throw new Exception("Erro ao atualizar o endereço.");
        var contact = _unitOfWork.ContactRepository.Update(entity.Individual.Contact);
        if (contact is null) throw new Exception("Erro ao atualizar o contato.");
        var individual = _unitOfWork.IndividualRepository.Update(entity.Individual);
        if (individual is null) throw new Exception("Erro ao atualizar o individuo.");
        var customer = _unitOfWork.CustomerRepository.Update(entity);
        await _unitOfWork.Commit(transaction);

        return customer != null ? _mapper.Map<CustomerDTO>(customer) : null;
    }

    public async Task<CustomerDTO?> Delete(int id)
    {
        var entity = await _unitOfWork.CustomerRepository.FindOne(id);
        if (entity is null) throw new Exception("404 - Not Found");

        using var transaction = _unitOfWork.BeginTransaction;
        var customer = _unitOfWork.CustomerRepository.Delete(entity);
        if (customer is null) throw new Exception("Erro ao remover o cliente");
        var individual = _unitOfWork.IndividualRepository.Delete(entity.Individual);
        if (individual is null) throw new Exception("Erro ao remover o indivíduo.");
        var address = _unitOfWork.AddressRepository.Delete(entity.Individual.Address);
        if (address is null) throw new Exception("Erro ao remover o endereço.");
        var contact = _unitOfWork.ContactRepository.Delete(entity.Individual.Contact);
        if (contact is null) throw new Exception("Erro ao remover o contato.");
        await _unitOfWork.Commit(transaction);

        return customer != null ? _mapper.Map<CustomerDTO>(customer) : null;
    }

    public async Task<IEnumerable<CustomerDTO>> Find()
    {
        var customers = await _unitOfWork.CustomerRepository.FindAll();

        var customersDTO = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

        return customersDTO;
    }

    public async Task<CustomerDTO?> FindOne(int id)
    {
        var customer = await _unitOfWork.CustomerRepository.FindOne(id);

        return customer != null ? _mapper.Map<CustomerDTO>(customer) : null;
    }
}