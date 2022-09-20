using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Pallet.Database.Migrations
{
    public partial class initial : Migration
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
                    ALM_NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: true),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PRIO = table.Column<string>(type: "char(1)", nullable: true),
                    STYPE = table.Column<string>(type: "char(1)", nullable: true),
                    ALM_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: true),
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
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: true),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PRIO = table.Column<string>(type: "char(1)", nullable: true),
                    STYPE = table.Column<string>(type: "char(1)", nullable: true),
                    ALM_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: true),
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
                    NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: true),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VAL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NAILER_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAILER_ID = table.Column<int>(type: "int", nullable: false),
                    NAILER_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    DESC1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC2 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC3 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DOCK = table.Column<int>(type: "int", nullable: false),
                    NLENGTH = table.Column<int>(type: "int", nullable: false),
                    NWIDTH = table.Column<int>(type: "int", nullable: false),
                    MSIZE = table.Column<int>(type: "int", nullable: false),
                    MCOLOR = table.Column<long>(type: "bigint", nullable: false),
                    NSIZE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAILER_DEF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROG_USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_NAME = table.Column<string>(type: "varchar(50)", nullable: false),
                    USER_DESC = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    USER_ROLE = table.Column<int>(type: "int", nullable: false),
                    USER_HASH = table.Column<string>(type: "char(64)", nullable: true)
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
                    SIG_NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    SIG_ADDRESS = table.Column<string>(type: "varchar(100)", nullable: true),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: true),
                    DESC1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC2 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC3 = table.Column<string>(type: "nvarchar(100)", nullable: true)
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
                    NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    DEVICE = table.Column<string>(type: "varchar(50)", nullable: true),
                    TEXT1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TEXT3 = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSEVENT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WPROD_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WPROD_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    DESC1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DESC2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DESC3 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    SIZE1_X = table.Column<int>(type: "int", nullable: false),
                    SIZE1_Y = table.Column<int>(type: "int", nullable: false),
                    SIZE1_Z = table.Column<int>(type: "int", nullable: false),
                    SIZE2_X = table.Column<int>(type: "int", nullable: false),
                    SIZE2_Y = table.Column<int>(type: "int", nullable: false),
                    SIZE2_Z = table.Column<int>(type: "int", nullable: false),
                    PRESET = table.Column<bool>(type: "bit", nullable: false),
                    TYPE = table.Column<int>(type: "int", nullable: false),
                    PROD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPROD_DEF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WPROD_ELEMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ELE_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    DESC1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC2 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESC3 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ELE_CNT = table.Column<int>(type: "int", nullable: false),
                    SIZEX = table.Column<int>(type: "int", nullable: false),
                    SIZEY = table.Column<int>(type: "int", nullable: false),
                    SIZEZ = table.Column<int>(type: "int", nullable: false),
                    WOOD_DIR = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPROD_ELEMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WTABLE_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WTABLE_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    B_ENA = table.Column<bool>(type: "bit", nullable: false),
                    A1_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    A1_OFFS_Y = table.Column<int>(type: "int", nullable: false),
                    A2_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    A2_OFFS_Y = table.Column<int>(type: "int", nullable: false),
                    B1_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    B1_OFFS_Y = table.Column<int>(type: "int", nullable: false),
                    B2_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    B2_OFFS_Y = table.Column<int>(type: "int", nullable: false),
                    A_SIZE_X = table.Column<int>(type: "int", nullable: false),
                    A_SIZE_Y = table.Column<int>(type: "int", nullable: false),
                    B_SIZE_X = table.Column<int>(type: "int", nullable: false),
                    B_SIZE_Y = table.Column<int>(type: "int", nullable: false),
                    A_WA_SIZE_X = table.Column<int>(type: "int", nullable: false),
                    A_WA_SIZE_Y = table.Column<int>(type: "int", nullable: false),
                    B_WA_SIZE_X = table.Column<int>(type: "int", nullable: false),
                    B_WA_SIZE_Y = table.Column<int>(type: "int", nullable: false),
                    A_WA_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    A_WA_OFFS_Y = table.Column<int>(type: "int", nullable: false),
                    B_WA_OFFS_X = table.Column<int>(type: "int", nullable: false),
                    B_WA_OFFS_Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WTABLE_DEF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WTABLE_TOOLS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WTABLE_TOOLS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WPROD_NAIL_POS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAIL_ID = table.Column<int>(type: "int", nullable: false),
                    POSX = table.Column<int>(type: "int", nullable: false),
                    POSY = table.Column<int>(type: "int", nullable: false),
                    POSZ = table.Column<int>(type: "int", nullable: false),
                    ANGLE1 = table.Column<int>(type: "int", nullable: false),
                    ANGLE2 = table.Column<int>(type: "int", nullable: false),
                    NAIL_FIX = table.Column<bool>(type: "bit", nullable: false),
                    MOVE_TO_NEXT = table.Column<int>(type: "int", nullable: false),
                    ALT = table.Column<int>(type: "int", nullable: false),
                    MODE = table.Column<int>(type: "int", nullable: false),
                    NAILER_ID = table.Column<int>(type: "int", nullable: true),
                    PROD_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPROD_NAIL_POS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WPROD_NAIL_POS_NAILER_DEF_NAILER_ID",
                        column: x => x.NAILER_ID,
                        principalTable: "NAILER_DEF",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WPROD_NAIL_POS_WPROD_DEF_PROD_ID",
                        column: x => x.PROD_ID,
                        principalTable: "WPROD_DEF",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WPROD_ELE_POS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ELE_ID = table.Column<int>(type: "int", nullable: true),
                    PROD_ID = table.Column<int>(type: "int", nullable: true),
                    POS_ID = table.Column<int>(type: "int", nullable: false),
                    POSX = table.Column<int>(type: "int", nullable: false),
                    POSY = table.Column<int>(type: "int", nullable: false),
                    POSZ = table.Column<int>(type: "int", nullable: false),
                    LAYER = table.Column<short>(type: "smallint", nullable: false),
                    OUTLN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPROD_ELE_POS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WPROD_ELE_POS_WPROD_DEF_PROD_ID",
                        column: x => x.PROD_ID,
                        principalTable: "WPROD_DEF",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WPROD_ELE_POS_WPROD_ELEMENTS_ELE_ID",
                        column: x => x.ELE_ID,
                        principalTable: "WPROD_ELEMENTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PROFILE_DEF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROFILE_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    DESC1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DESC2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DESC3 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DT_CREA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_CHNG = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_OPEN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREA_BY = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    WTABLE_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILE_DEF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROFILE_DEF_WTABLE_DEF_WTABLE_ID",
                        column: x => x.WTABLE_ID,
                        principalTable: "WTABLE_DEF",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProfileProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileProducts", x => new { x.ProductId, x.ProfileId, x.Position });
                    table.ForeignKey(
                        name: "FK_ProfileProducts_PROFILE_DEF_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "PROFILE_DEF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileProducts_WPROD_DEF_ProductId",
                        column: x => x.ProductId,
                        principalTable: "WPROD_DEF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTools",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTools", x => new { x.ToolId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfileTools_PROFILE_DEF_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "PROFILE_DEF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTools_WTABLE_TOOLS_ToolId",
                        column: x => x.ToolId,
                        principalTable: "WTABLE_TOOLS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROFILE_DEF_PROFILE_NAME",
                table: "PROFILE_DEF",
                column: "PROFILE_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROFILE_DEF_WTABLE_ID",
                table: "PROFILE_DEF",
                column: "WTABLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileProducts_ProfileId",
                table: "ProfileProducts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTools_ProfileId",
                table: "ProfileTools",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WPROD_DEF_WPROD_NAME",
                table: "WPROD_DEF",
                column: "WPROD_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WPROD_ELE_POS_ELE_ID",
                table: "WPROD_ELE_POS",
                column: "ELE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WPROD_ELE_POS_PROD_ID",
                table: "WPROD_ELE_POS",
                column: "PROD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WPROD_NAIL_POS_NAILER_ID",
                table: "WPROD_NAIL_POS",
                column: "NAILER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WPROD_NAIL_POS_PROD_ID",
                table: "WPROD_NAIL_POS",
                column: "PROD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WTABLE_DEF_WTABLE_NAME",
                table: "WTABLE_DEF",
                column: "WTABLE_NAME",
                unique: true);
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
                name: "ProfileProducts");

            migrationBuilder.DropTable(
                name: "ProfileTools");

            migrationBuilder.DropTable(
                name: "PROG_USER");

            migrationBuilder.DropTable(
                name: "SIGNALS_DEF");

            migrationBuilder.DropTable(
                name: "SYSEVENT");

            migrationBuilder.DropTable(
                name: "WPROD_ELE_POS");

            migrationBuilder.DropTable(
                name: "WPROD_NAIL_POS");

            migrationBuilder.DropTable(
                name: "PROFILE_DEF");

            migrationBuilder.DropTable(
                name: "WTABLE_TOOLS");

            migrationBuilder.DropTable(
                name: "WPROD_ELEMENTS");

            migrationBuilder.DropTable(
                name: "NAILER_DEF");

            migrationBuilder.DropTable(
                name: "WPROD_DEF");

            migrationBuilder.DropTable(
                name: "WTABLE_DEF");
        }
    }
}