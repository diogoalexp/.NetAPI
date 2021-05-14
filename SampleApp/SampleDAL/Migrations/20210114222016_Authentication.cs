using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleDAL.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Person_Personid",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddesses_Person_Personid",
                table: "EmailAddesses");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Person",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Personid",
                table: "EmailAddesses",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EmailAddesses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddesses_Personid",
                table: "EmailAddesses",
                newName: "IX_EmailAddesses_PersonId");

            migrationBuilder.RenameColumn(
                name: "Personid",
                table: "Addresses",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_Personid",
                table: "Addresses",
                newName: "IX_Addresses_PersonId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Person_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddesses_Person_PersonId",
                table: "EmailAddesses",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Person_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddesses_Person_PersonId",
                table: "EmailAddesses");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Person",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "EmailAddesses",
                newName: "Personid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmailAddesses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddesses_PersonId",
                table: "EmailAddesses",
                newName: "IX_EmailAddesses_Personid");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Addresses",
                newName: "Personid");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                newName: "IX_Addresses_Personid");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Person_Personid",
                table: "Addresses",
                column: "Personid",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddesses_Person_Personid",
                table: "EmailAddesses",
                column: "Personid",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
