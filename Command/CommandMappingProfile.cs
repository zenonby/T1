using AutoMapper;
using T1.Model;

namespace T1.Command;

/// <summary>
/// Профиль AutoMapper
/// </summary>
public class CommandMappingProfile : Profile
{
    public CommandMappingProfile()
    {
        CreateMap<AddTransactionCommand, Transaction>();
    }
}
