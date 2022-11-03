using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Infrastructure.Migrations
{
    public partial class mg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tinklas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OBT_PAVADINIMAS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OBJ_GV_TIPAS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OBJ_NUMERIS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PPlus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PL_T = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PMinus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelDatas");
        }
    }
}
