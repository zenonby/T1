using AutoMapper;
using Microsoft.Extensions.Logging;
using T1.Abstractions;
using T1.Dto;
using T1.Model;

namespace T1.Query;

public class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly IDb _db;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public GetTransactionByIdQueryHandler(
        IDb db,
        ILogger<GetTransactionByIdQueryHandler> logger,
        IMapper mapper)
    {
        _db = db;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TransactionDto> HandleAsync(GetTransactionByIdQuery query)
    {
        Transaction transaction = await _db.GetTransactionAsync(query.Id);
        var dto = _mapper.Map<TransactionDto>(transaction);
        return dto;
    }
}
