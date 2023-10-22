using AutoMapper;
using StoreManagementSystem.Data.IRepositories;
using StoreManagementSystem.Data.Repositories;
using StoreManagementSystem.Domain.Entities;
using StoreManagementSystem.Service.DTOs.StoreManagers;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.DTOs.StoryManagers;
using StoreManagementSystem.Service.Helpers;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Mappers;
using System.ComponentModel;

namespace StoreManagementSystem.Service.Services;

public class StoreManagerService : IStoreManagerService
{
    private readonly IMapper mapper;
    private readonly IRepository<StoreManager> storeManagerRepository;
    public StoreManagerService()
    {
        this.storeManagerRepository = new Repository<StoreManager>();

        this.mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<StoreManagerResultDto>> AddAsync(StoreManagerCreationDto dto)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u => 
            u.UserName.ToLower().Equals(dto.Username.ToLower()));

        if (existStoreManager is not null)
                return new Response<StoreManagerResultDto>
                {
                    StatusCode = 403,
                    Message = "This storeManager already exists",
                };

        var mappedStoreManager = this.mapper.Map<StoreManager>(dto);
        var passwordHash = PasswordHash.Hash(mappedStoreManager.Password);
        mappedStoreManager.Password = passwordHash.PasswordHash;
        mappedStoreManager.Salt = passwordHash.Passwordsalt;
        await this.storeManagerRepository.CreateAsync(mappedStoreManager);
        await this.storeManagerRepository.SaveAsync();

        var result = this.mapper.Map<StoreManagerResultDto>(mappedStoreManager);
        return new Response<StoreManagerResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreManagerResultDto>(mappedStoreManager)
        };
    }

    public async Task<Response<StoreManagerResultDto>> ModifyAsync(StoreManagerUpdateDto dto)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u => u.Id.Equals(dto.Id),
            includes: new string[] { "Stores" });
        
        if(existStoreManager is null)
            return new Response<StoreManagerResultDto>
            {
                StatusCode = 404,
                Message = "This storeManager not found exists",
            };

        if(!dto.Username.Equals(existStoreManager.UserName, StringComparison.OrdinalIgnoreCase))
        {
            var existStoreManageUsername = await this.storeManagerRepository.SelectAsync(u => 
                u.UserName.ToLower().Equals(dto.Username.ToLower()));

            if (existStoreManageUsername is not null)
                return new Response<StoreManagerResultDto>
                {
                    StatusCode = 403,
                    Message = "This storeManager already exists",
                };
        }

        this.mapper.Map(dto, existStoreManager);
        this.storeManagerRepository.Update(existStoreManager);
        await this.storeManagerRepository.SaveAsync();

        return new Response<StoreManagerResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreManagerResultDto>(existStoreManager)
        };
    }

    public async Task<Response<bool>> RemoveAsync(long id)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u => u.Id.Equals(id));
            if (existStoreManager is null)
                return new Response<bool>
                {
                    StatusCode = 404,
                    Message = "This storeManager not found exists",
                    Data = false
                };

        this.storeManagerRepository.Delete(existStoreManager);
        await this.storeManagerRepository.SaveAsync();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Data = true
        };
    }

    public async Task<Response<StoreManagerResultDto>> RetrieveByIdAsync(long id)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u => u.Id.Equals(id), 
            includes: new string[] { "Stores" });
        
        if (existStoreManager is null)
            return new Response<StoreManagerResultDto>
            {
                StatusCode = 404,
                Message = "This storeManager not found exists",
            };

        return new Response<StoreManagerResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreManagerResultDto>(existStoreManager)
        };
    }

    public async Task<Response<IEnumerable<StoreManagerResultDto>>> RetrieveAllAsync()
    {
        var storeManagers = this.storeManagerRepository.SelectAll(
            includes: new string[] { "Stores" });

        return new Response<IEnumerable<StoreManagerResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<IEnumerable<StoreManagerResultDto>>(storeManagers)
        };
    }

    public async Task<Response<StoreManagerResultDto>> RetrieveByUsernameAsync(string username)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u => 
            u.UserName.ToLower().Equals(username.ToLower()),
            includes: new string[] { "Stores" });

        if (existStoreManager is null)
            return new Response<StoreManagerResultDto>
            {
                StatusCode = 404,
                Message = "This storeManager not found exists",
            };

        return new Response<StoreManagerResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreManagerResultDto>(existStoreManager)
        };
    }

    public async Task<Response<bool>> IsValidPassword(string username, string password)
    {
        var existStoreManager = await this.storeManagerRepository.SelectAsync(u =>
            u.UserName.ToLower().Equals(username.ToLower()));

        if (existStoreManager is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "This username is not found",
                Data = false
            };

        if(!PasswordHash.Verify(password, existStoreManager.Password, existStoreManager.Salt))
            return new Response<bool>
            {
                StatusCode = 422,
                Message = "This password is not valid",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Data = true
        };
    }

    public async Task<Response<bool>> IsStrongPassword(string password)
    {
        if (password.Length < 8)
            return new Response<bool>
            {
                StatusCode = 422,
                Message = "password must be longer than 8 characters",
                Data = false
            };

        var isThereNumber = false;
        var isThereUpper = false;
        var isThereLower = false;
       
        foreach(var character in password)
        {
            if(character>='0'&&character<='9')
                isThereNumber = true;
            else if(character >= 'A' && character <= 'Z')
                isThereUpper = true;
            else if(character >= 'a' && character <= 'z')
                isThereLower = true;
        }

        if(isThereUpper is false)
            return new Response<bool>
            {
                StatusCode = 422,
                Message = "password must contain an uppercase letter",
                Data = false
            };
        else if(isThereLower is false)
            return new Response<bool>
            {
                StatusCode = 422,
                Message = "password must contain an lowercase letter",
                Data = false
            };
        else if(isThereNumber is false)
            return new Response<bool>
            {
                StatusCode = 422,
                Message = "password must contain an number",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Data = true
        };
    }
}
