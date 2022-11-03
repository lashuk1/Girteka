using Application.Core.DTO;
using Application.Core.Extensions;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Core.CQRS.Commands
{
    public class ExcelPostCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
    }
    public sealed class ExcelPostCommandHandler : IRequestHandler<ExcelPostCommand,bool>
    {
        private readonly IExcelRepository _excelRepository;
        private readonly IExcelDataReader _excelDataReader;
        private readonly ILogger<ExcelPostCommandHandler> _logger;

        public ExcelPostCommandHandler(IExcelRepository excelRepository, IExcelDataReader excelDataReader, ILogger<ExcelPostCommandHandler> logger)
        {
            _excelRepository = excelRepository;
            _excelDataReader = excelDataReader;
            _logger = logger;
        }
        public async Task<bool> Handle(ExcelPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting all data from excel");
                var createdExcel = new List<List<ExcelData>>();
                var result = new ExcelDTO();
                using (var ms = new MemoryStream())
                {
                    request.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    result = _excelDataReader.GetDataFromExcel(fileBytes);
                }
                var excelDatas = result.Tinklas.Map();

                foreach (var exceldata in excelDatas)
                {
                  var storedExceldata=  await _excelRepository.AddRangeAsync(exceldata.Select(x => x).ToList());
                    createdExcel.Add(storedExceldata);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error happened in ExcelPostCommandHandler exStackTrace : {ex.StackTrace}," +
                    $"exMessages: {ex.Message},exInnerException{ex.InnerException}");
                return false;
            }
        }
    }
}
