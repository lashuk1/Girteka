using SharedKernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProjectAggregate
{
    public  class ExcelData :BaseEntity
    {
        public string Tinklas { get; set; }
        public string OBT_PAVADINIMAS { get; set; }
        public string OBJ_GV_TIPAS { get; set; }
        public string OBJ_NUMERIS { get; set; }
        public string PPlus { get; set; }
        public DateTime PL_T { get; set; }
        public string PMinus { get; set; }
    }
}
