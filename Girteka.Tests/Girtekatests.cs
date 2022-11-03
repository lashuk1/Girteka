using Application.Core.ProjectAggregate;
using Application.Infrastructure.Data;
using Application.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Girteka.Tests
{
    public class Girtekatests
    {

        private readonly AppDbContext context;
        public Girtekatests()
        {
            var dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("Girteka"
                );
             context = new AppDbContext(dbOptions.Options);

        }
        [Fact]
        public async Task Test_Excel_Data_Flow()
        {
            //arrange
            var Data = new List<ExcelData>()
            {

                new(){ Id = 1, Tinklas="Vilniaus regiono tinklas", OBT_PAVADINIMAS="Butas",OBJ_GV_TIPAS="N",
                    OBJ_NUMERIS="567918",PPlus="0.34",PL_T= new DateTime(2015, 12, 31),PMinus="0" },

                new(){ Id = 2, Tinklas="Vilniaus regiono tinklas", OBT_PAVADINIMAS="Butas",OBJ_GV_TIPAS="N",
                    OBJ_NUMERIS="2222",PPlus="0.64",PL_T= new DateTime(2001, 12, 31),PMinus="3" },

                new(){ Id = 3, Tinklas="Vilniaus regiono tinklas", OBT_PAVADINIMAS="Butas",OBJ_GV_TIPAS="N",
                    OBJ_NUMERIS="3333",PPlus="0.54",PL_T= new DateTime(2002, 12, 31),PMinus="2" },

                new(){ Id = 4, Tinklas="Vilniaus regiono tinklas", OBT_PAVADINIMAS="Butas",OBJ_GV_TIPAS="N",
                    OBJ_NUMERIS="444",PPlus="0.74",PL_T= new DateTime(2000, 12, 31),PMinus="1" },
            };
            context.AddRange(Data);
            await context.SaveChangesAsync();
            var sut = new ExcelRepository(context);
            //act
            var result = await sut.ListAsync();
            //assert
            Assert.Equal(Data.Count(),result.Count());
        }

    }
}