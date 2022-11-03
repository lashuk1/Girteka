using Application.Core.DTO;
using Application.Core.Interfaces;
using Application.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services
{
    public class ExcelDataReaderServices : NpoiExcelImporterBase<ExcelDTO>, IExcelDataReader
    {
        private readonly ILogger<ExcelDataReaderServices> _logger;

        public ExcelDataReaderServices(ILogger<ExcelDataReaderServices> logger)
        {
            _logger = logger;
        }
        public ExcelDTO GetDataFromExcel(byte[] fileBytes)
        {
            var entity = new ExcelDTO();
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }
       
        private ExcelDTO ProcessExcelRow(ISheet worksheet, int row, ExcelDTO data)
        {
            var exceptionMessage = new StringBuilder();
            try
            {
                _logger.LogInformation("Parsing excel file");
                var item = new excelItemDTO();
                item.Tinklas = GetRequiredValueFromRowOrNull(worksheet, row, 0, nameof(item.Tinklas), exceptionMessage, CellType.String);
                item.OBT_PAVADINIMAS = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(item.OBT_PAVADINIMAS), exceptionMessage, CellType.String);
                item.OBJ_GV_TIPAS = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(item.OBJ_GV_TIPAS), exceptionMessage, CellType.String);
                item.OBJ_NUMERIS = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(item.OBJ_NUMERIS), exceptionMessage, CellType.String); 
                item.PPlus = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(item.PPlus), exceptionMessage, CellType.String);
                item.PL_T = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(item.PL_T), exceptionMessage, CellType.Unknown);
                item.PMinus = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(item.PMinus), exceptionMessage, CellType.String);
                data.Tinklas.Add(item);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
            return data;
        }
        private string GetRequiredValueFromRowOrNull(
           ISheet worksheet,
           int row,
           int column,
           string columnName,
           StringBuilder exceptionMessage,
           CellType? cellType = null)
        {
            var cell = worksheet.GetRow(row).GetCell(column);
            //DateCellValue
            if (cell == null)
            {
                return "";
            }
            if (cellType == CellType.Unknown)
            {
                return cell.DateCellValue.ToString();
            }

            if (cellType.HasValue)
            {
                cell.SetCellType(cellType.Value);
            }

            var cellValue = cell.StringCellValue;
            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue))
            {
                return cellValue;
            }

            return null;
        }
    }
}
