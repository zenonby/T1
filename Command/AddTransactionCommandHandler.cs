using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using AutoMapper;
using T1.Abstractions;
using T1.Model;

namespace T1.Command;

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand>
{
    private readonly IDb _db;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AddTransactionCommandHandler(
        IDb db,
        ILogger<AddTransactionCommandHandler> logger,
        IMapper mapper)
    {
        _db = db;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task HandleAsync(AddTransactionCommand addTransactionCommand)
    {
        // ���������
        ValidateCommand(addTransactionCommand);

        // �������� � ���� ������
        var transaction = _mapper.Map<Transaction>(addTransactionCommand);

        try
        {
            await _db.InsertTransactionAsync(transaction);
        }
        catch (Exception ex)
        {
            _logger.LogError($"DB error: {ex.Message}, stack trace: {ex.StackTrace}");
            throw;
        }
    }

    /// <summary>
    /// ��������� ������� �������
    /// </summary>
    /// <param name="addTransactionCommand">������� ��� ���������</param>
    private void ValidateCommand(AddTransactionCommand addTransactionCommand)
    {
        try
        {
            ValidationContext validationCtx = new ValidationContext(addTransactionCommand);
            Validator.ValidateObject(addTransactionCommand, validationCtx, true);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Validation error: {ex.Message}, stack trace: {ex.StackTrace}");
            throw;
        }
    }
}
