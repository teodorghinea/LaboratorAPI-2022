using Microsoft.EntityFrameworkCore.Migrations;

namespace LaboratorAPI.Migrations
{
    public partial class NotificationPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Notifications");
        }
    }
}
