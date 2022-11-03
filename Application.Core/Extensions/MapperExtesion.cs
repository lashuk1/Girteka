using Application.Core.DTO;
using Application.Core.ProjectAggregate;

namespace Application.Core.Extensions
{
    public static class MapperExtesion
    {
        public static IEnumerable<IGrouping<string, ExcelData>> Map(this List<excelItemDTO> data) =>
           data.Where(x => x.OBT_PAVADINIMAS == "Butas").Select(t => new ExcelData
           {
               OBJ_GV_TIPAS = t.OBJ_GV_TIPAS,
               PPlus = t.PPlus,
               PMinus = t.PMinus,
               OBJ_NUMERIS = t.OBJ_NUMERIS,
               OBT_PAVADINIMAS = t.OBT_PAVADINIMAS,
               PL_T = Convert.ToDateTime(t.PL_T),
               Tinklas = t.Tinklas
           }).GroupBy(x => x.Tinklas);
        public static IEnumerable<ExcelDataDTO> Map(this List<ExcelData> data) =>
           data.Select(t => new ExcelDataDTO
           {
               OBJ_GV_TIPAS = t.OBJ_GV_TIPAS,
               OBJ_NUMERIS = t.OBJ_NUMERIS,
               OBT_PAVADINIMAS = t.OBT_PAVADINIMAS,
               PL_T = t.PL_T.ToString(),
               PMinus = t.PMinus,
               PPlus = t.PPlus,
               Tinklas = t.Tinklas,
           });
    }
}
