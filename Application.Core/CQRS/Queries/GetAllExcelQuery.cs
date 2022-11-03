using Application.Core.DTO;
using Application.Core.Extensions;
using Application.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Core.CQRS.Queries
{
    public class GetAllExcelQuery : IRequest<IEnumerable<ExcelDataDTO>>
    {
    }
    public sealed class GetAllClientQueryHandler : IRequestHandler<GetAllExcelQuery, IEnumerable<ExcelDataDTO>>
    {
        private readonly IExcelRepository _excelRepository;
        private readonly ILogger<GetAllClientQueryHandler> _logger;

        public GetAllClientQueryHandler(IExcelRepository excelRepository, ILogger<GetAllClientQueryHandler> logger)
        {
            _excelRepository = excelRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<ExcelDataDTO>> Handle(GetAllExcelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting all data from database");

                var result = await _excelRepository.ListAsync(cancellationToken);

                return result.Map();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eror happened in GetAllClientQueryHandler exStackTrace : {ex.StackTrace}," +
                    $"exMessages: {ex.Message},exInnerException{ex.InnerException}");
                throw;
            }
        }
    }
}
