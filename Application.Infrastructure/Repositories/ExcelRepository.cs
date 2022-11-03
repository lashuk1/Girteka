using Application.Core.DTO;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using Application.Infrastructure.Data;
using SharedKernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories
{
    public class ExcelRepository : RepositoryBase<ExcelData>, IExcelRepository
    {
        private readonly AppDbContext _dbContext;

        public ExcelRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}
