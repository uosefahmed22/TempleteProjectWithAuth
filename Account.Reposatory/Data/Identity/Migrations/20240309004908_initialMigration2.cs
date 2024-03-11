using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Reposatory.Data.Identity.Migrations
{
    public partial class initialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62495195-1925-409f-80ac-ae60139eec99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "836a1c70-59f2-4a1f-bfbf-81a1361d5b00");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a6f8389-c3aa-4fe2-a10a-a2aac827d767", "3", "ServiceProvider", "ServiceProvider" },
                    { "3ca25d8e-b157-4eb3-b0d3-947df98c3ef2", "2", "BussinesOwner", "BussinesOwner" },
                    { "cde862ff-197e-42f1-823d-91108973f069", "1", "User", "User" },
                    { "d81730b6-471c-4cf2-8e8f-91f2352cb840", "4", "Admin", "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a6f8389-c3aa-4fe2-a10a-a2aac827d767");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ca25d8e-b157-4eb3-b0d3-947df98c3ef2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cde862ff-197e-42f1-823d-91108973f069");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d81730b6-471c-4cf2-8e8f-91f2352cb840");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62495195-1925-409f-80ac-ae60139eec99", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "836a1c70-59f2-4a1f-bfbf-81a1361d5b00", "2", "User", "User" });
        }
    }
}
