using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Data.IRepositories;
using StoreManagementSystem.Data.Repositories;
using StoreManagementSystem.Domain.Entities;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.Helpers;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Mappers;
using System.Reflection.Metadata.Ecma335;

namespace StoreManagementSystem.Service.Services;

public class StoreService : IStoreService
{
    private readonly IMapper mapper;
    private readonly IRepository<Store> repository;
    public StoreService()
    {
        this.repository = new Repository<Store>();
        
        this.mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<StoreResultDto>> AddAsync(StoreCreationDto dto)
    {
        var store = await this.repository.SelectAsync(c => 
            c.Name.ToLower().Equals(dto.Name.ToLower()));

        if (store is not null)
            return new Response<StoreResultDto>
            {
                StatusCode = 403,
                Message = "This store already exists",
            };

        var mappedStore = this.mapper.Map<Store>(dto);
        await this.repository.CreateAsync(mappedStore);
        await this.repository.SaveAsync();

        return new Response<StoreResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreResultDto>(mappedStore)
        };
    }

    public async Task<Response<StoreResultDto>> ModifyAsync(StoreUpdateDto dto)
    {
        var store = await this.repository.SelectAsync(c => c.Id.Equals(dto.Id),
            includes: new string[] { "StoreManager" });

        if (store is null)
            return new Response<StoreResultDto>
            {
                StatusCode = 404,
                Message = "This store not found exists",
            };

        if(!dto.Name.Equals(store.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existStoreName = await this.repository.SelectAsync(c => 
                c.Name.ToLower().Equals(dto.Name.ToLower()));

            if (existStoreName is not null)
                return new Response<StoreResultDto>
                {
                    StatusCode = 403,
                    Message = "This store already exists",
                };
        }

        var mappedStore = this.mapper.Map(dto, store);
        this.repository.Update(mappedStore);
        await this.repository.SaveAsync();

        return new Response<StoreResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreResultDto>(mappedStore)
        };
    }

    public async Task<Response<bool>> RemoveAsync(long id)
    {
        var store = await this.repository.SelectAsync(c => c.Id.Equals(id));

        if (store is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "This store not found exists",
                Data = false
            };

        this.repository.Delete(store);
        await this.repository.SaveAsync();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Data = true
        };
    }

    public async Task<Response<StoreResultDto>> RetrieveByIdAsync(long id)
    {
        var store = await this.repository.SelectAsync(c => c.Id.Equals(id),
            includes: new string[] { "StoreManager" });
        
        if (store is null)
            return new Response<StoreResultDto>
            {
                StatusCode = 404,
                Message = "This store not found exists",
            };

        return new Response<StoreResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<StoreResultDto>(store)
        };
    }

    public async Task<Response<IEnumerable<StoreResultDto>>> RetrieveAllAsync()
    {
        var stores = this.repository.SelectAll(includes: new string[] { "StoreManager" });

        return new Response<IEnumerable<StoreResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.mapper.Map<IEnumerable<StoreResultDto>>(stores)
        };
    }
}
