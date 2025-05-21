using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediCare.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Genders_GenderId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Genders_GenderId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Patients_GenderId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_GenderId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Patients",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Doctors",
                newName: "Gender");

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kardiologia" },
                    { 2, "Dermatologia" },
                    { 3, "Neurologia" },
                    { 4, "Pediatria" },
                    { 5, "Ortopedia" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "jkowalski@example.com", "hashed", 1 },
                    { 2, "anowak@example.com", "hashed", 1 },
                    { 3, "pzielinski@example.com", "hashed", 1 },
                    { 4, "kwiśniewska@example.com", "hashed", 1 },
                    { 5, "tmazur@example.com", "hashed", 1 },
                    { 6, "ejankowska@example.com", "hashed", 1 },
                    { 7, "alewandowski@example.com", "hashed", 1 },
                    { 8, "mdąbrowska@example.com", "hashed", 1 },
                    { 9, "mkaczmarek@example.com", "hashed", 1 },
                    { 10, "bszymańska@example.com", "hashed", 1 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "DoctorNumber", "FirstName", "Gender", "LastName", "PhoneNumber", "SpecializationId", "UserId" },
                values: new object[,]
                {
                    { 1, "1000001", "Jan", 1, "Kowalski", "123456789", 1, 1 },
                    { 2, "1000002", "Anna", 2, "Nowak", "987654321", 2, 2 },
                    { 3, "1000003", "Piotr", 1, "Zieliński", "111222333", 3, 3 },
                    { 4, "1000004", "Katarzyna", 2, "Wiśniewska", "444555666", 4, 4 },
                    { 5, "1000005", "Tomasz", 1, "Mazur", "777888999", 5, 5 },
                    { 6, "1000006", "Ewa", 2, "Jankowska", "222333444", 1, 6 },
                    { 7, "1000007", "Andrzej", 1, "Lewandowski", "555666777", 2, 7 },
                    { 8, "1000008", "Magdalena", 2, "Dąbrowska", "888999000", 3, 8 },
                    { 9, "1000009", "Marek", 1, "Kaczmarek", "101202303", 4, 9 },
                    { 10, "1000010", "Barbara", 2, "Szymańska", "404505606", 5, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Patients",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Doctors",
                newName: "GenderId");

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GenderId",
                table: "Patients",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_GenderId",
                table: "Doctors",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Genders_GenderId",
                table: "Doctors",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Genders_GenderId",
                table: "Patients",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
