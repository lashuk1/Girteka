using Application.Core.CQRS.Commands;
using Application.Core.CQRS.Queries;
using Application.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.Controllers
{
    public class DataController : ApiControllerBase
    {
        [HttpPost]
        public async Task<bool> Post(IFormFile file) => await
             Mediator.Send(new ExcelPostCommand { File = file });


        [HttpGet]
        public async Task<IEnumerable<ExcelDataDTO>> GetDataFromDataBase() => await
            Mediator.Send(new GetAllExcelQuery());
    }
}
