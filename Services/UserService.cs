using System.Security.Cryptography;
using AutoMapper;
using ExampleApi.DTOs;
using ExampleApi.Models;
using ExampleApi.Repositories;
using ExampleApi.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDTO?> Create(CreateUserDTO dto)
    {
        var entity = _mapper.Map<User>(dto);

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
        var User = await _unitOfWork.UserRepository.Create(entity);
        await _unitOfWork.Commit(transaction);

        return User != null ? _mapper.Map<UserDTO>(User) : null;
    }

    public async Task<UserDTO?> Update(int id, UpdateUserDTO dto)
    {
        var entity = await _unitOfWork.UserRepository.FindOne(u => u.Id == id);
        if (entity is null) throw new Exception("404 - Not Found");

        using var transaction = _unitOfWork.BeginTransaction;
        entity.UserName = dto.UserName;
        entity.Password = Crypt.HashPassword(dto.Password);
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
        var User = _unitOfWork.UserRepository.Update(entity);
        await _unitOfWork.Commit(transaction);

        

        return User != null ? _mapper.Map<UserDTO>(User) : null;
    }

    public async Task<UserDTO?> Delete(int id)
    {
        var entity = await _unitOfWork.UserRepository.FindOne(u => u.Id == id);
        if (entity is null) throw new Exception("404 - Not Found");

        using var transaction = _unitOfWork.BeginTransaction;
        var User = _unitOfWork.UserRepository.Delete(entity);
        if (User is null) throw new Exception("Erro ao remover o cliente");
        var individual = _unitOfWork.IndividualRepository.Delete(entity.Individual);
        if (individual is null) throw new Exception("Erro ao remover o indivíduo.");
        var address = _unitOfWork.AddressRepository.Delete(entity.Individual.Address);
        if (address is null) throw new Exception("Erro ao remover o endereço.");
        var contact = _unitOfWork.ContactRepository.Delete(entity.Individual.Contact);
        if (contact is null) throw new Exception("Erro ao remover o contato.");
        await _unitOfWork.Commit(transaction);

        

        return User != null ? _mapper.Map<UserDTO>(User) : null;
    }

    public async Task<IEnumerable<UserDTO>> Find()
    {
        var users = await _unitOfWork.UserRepository.FindAll();

        var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);

        return usersDTO;
    }

    public async Task<UserDTO?> FindOne(int id)
    {
        var user = await _unitOfWork.UserRepository.FindOne(u => u.Id == id);

        return user != null ? _mapper.Map<UserDTO>(user) : null;
    }
}