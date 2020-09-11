using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstCodeDb.Migrations
{
    public partial class nameinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxonomy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RecordStatus = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RecordStatus = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    TaxonomyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Master_Taxonomy_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalTable: "Taxonomy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Master",
                columns: new[] { "Id", "CreateDateTime", "Key", "RecordStatus", "TaxonomyId", "UpdateDateTime", "Value" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FPTV", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test 1" });

            migrationBuilder.InsertData(
                table: "Taxonomy",
                columns: new[] { "Id", "CreateDateTime", "Key", "RecordStatus", "UpdateDateTime", "Value" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FPTV", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test 1" });

            migrationBuilder.CreateIndex(
                name: "IX_Master_TaxonomyId",
                table: "Master",
                column: "TaxonomyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "Taxonomy");
        }
    }
}
