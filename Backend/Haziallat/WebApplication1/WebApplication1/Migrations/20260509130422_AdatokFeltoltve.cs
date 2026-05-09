using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class AdatokFeltoltve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Species = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Breed = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    BirthYear = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "annakovacs@gmail.com", "Kovacs", "Anna", "+36 20 123 4567" },
                    { 2, null, "Nagy", "Béla", "+36 30 987 6543" },
                    { 3, "csilla.h@freemail.hu", "Horváth", "Csilla", null }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "BirthYear", "Breed", "Name", "OwnerId", "Species" },
                values: new object[,]
                {
                    { 1, 2019, "Labrador", "Bodri", 1, "Kutya" },
                    { 2, 2021, "Európai rövidszőrű", "Cirmos", 1, "Macska" },
                    { 3, 2020, "Németjuhász", "Rex", 2, "Kutya" },
                    { 4, 2022, null, "Hópihe", 3, "Nyúl" },
                    { 5, 2018, "Perzsa", "Mici", 3, "Macska" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
