using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CâblageCeff.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Panels",
                columns: table => new
                {
                    PanelId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Batiment = table.Column<string>(type: "TEXT", nullable: true),
                    Emplacement = table.Column<string>(type: "TEXT", nullable: true),
                    NbrPort = table.Column<int>(type: "INTEGER", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panels", x => x.PanelId);
                });

            migrationBuilder.CreateTable(
                name: "Patchs",
                columns: table => new
                {
                    PatchId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Emplacement = table.Column<string>(type: "TEXT", nullable: true),
                    Destination = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PanelId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patchs", x => x.PatchId);
                    table.ForeignKey(
                        name: "FK_Patchs_Panels_PanelId",
                        column: x => x.PanelId,
                        principalTable: "Panels",
                        principalColumn: "PanelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patchs_PanelId",
                table: "Patchs",
                column: "PanelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patchs");

            migrationBuilder.DropTable(
                name: "Panels");
        }
    }
}
