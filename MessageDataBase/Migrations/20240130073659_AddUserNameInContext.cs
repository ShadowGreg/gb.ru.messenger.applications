using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageDataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameInContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");
        }
    }
}
