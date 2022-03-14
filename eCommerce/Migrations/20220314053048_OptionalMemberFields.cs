using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    public partial class OptionalMemberFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Members_MemberId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MemberId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId1",
                table: "Members",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Members_MemberId1",
                table: "Members",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "MemberId");
        }
    }
}
