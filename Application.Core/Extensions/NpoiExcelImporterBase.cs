using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Application.Infrastructure.Extensions
{
    public abstract class NpoiExcelImporterBase<TEntity> where TEntity : class, new()
    {
        protected TEntity ProcessExcelFile(byte[] fileBytes, Func<ISheet, int, TEntity, TEntity> processExcelRow, bool useOldExcelFormat = false)
        {
            var entities = new TEntity();
            // var entities = new List<TEntity>();

            IWorkbook workbook;

            using (var stream = new MemoryStream(fileBytes))
            {
                workbook = useOldExcelFormat ? new HSSFWorkbook(stream) : new XSSFWorkbook(stream);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    entities = ProcessWorksheet(entities, workbook.GetSheetAt(i), processExcelRow);
                    //entities.AddRange(entitiesInWorksheet);
                }
            }
            return entities;
        }
        private TEntity ProcessWorksheet(TEntity entities, ISheet worksheet, Func<ISheet, int, TEntity, TEntity> processExcelRow)
        {
            var rowEnumerator = worksheet.GetRowEnumerator();
            rowEnumerator.Reset();
            var i = 0;
            while (rowEnumerator.MoveNext())
            {
                if (i == 0)
                {
                    //Skip header
                    i++;
                    continue;
                }
                try
                {
                    entities = processExcelRow(worksheet, i++, entities);

                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }
    }
}
