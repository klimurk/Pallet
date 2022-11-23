using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pallet.InternalDatabase.Migrations
{
    public partial class v01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALARM_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ALM_NR = table.Column<int>(type: "int", nullable: false),
                    ALM_NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: false),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PRIO = table.Column<string>(type: "char(1)", nullable: false),
                    STYPE = table.Column<string>(type: "char(1)", nullable: false),
                    ALM_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: false),
                    INVERTED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALARM_DEF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ALARM_LOG",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ALM_NR = table.Column<int>(type: "int", nullable: false),
                    ALM_NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: false),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PRIO = table.Column<string>(type: "char(1)", nullable: false),
                    STYPE = table.Column<string>(type: "char(1)", nullable: false),
                    ALM_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: false),
                    INVERTED = table.Column<bool>(type: "bit", nullable: false),
                    TIMESTAMP1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TIMESTAMP2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GONE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALARM_LOG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: false),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADDR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VAL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROG_USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    USER_DESC = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    USER_ROLE = table.Column<int>(type: "int", nullable: false),
                    USER_HASH = table.Column<string>(type: "char(64)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(name: "Registration Date", type: "datetime2", nullable: false),
                    Adminregistered = table.Column<string>(name: "Admin registered", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROG_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SIGNALS_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIG_NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    SIG_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: false),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: false),
                    DESC1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DESC2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DESC3 = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIGNALS_DEF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SYSEVENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: false),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSEVENT", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALARM_DEF");

            migrationBuilder.DropTable(
                name: "ALARM_LOG");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PROG_USER");

            migrationBuilder.DropTable(
                name: "SIGNALS_DEF");

            migrationBuilder.DropTable(
                name: "SYSEVENT");
        }
    }
}
