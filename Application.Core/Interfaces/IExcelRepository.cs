using Application.Core.DTO;
using Application.Core.ProjectAggregate;
using SharedKernels.interfaces.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public  interface IExcelRepository : IRepositoryBase<ExcelData>
    {
       
    }
}
