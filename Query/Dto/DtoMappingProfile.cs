using AutoMapper;
using T1.Model;

namespace T1.Dto;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Transaction, TransactionDto>();
    }
}
