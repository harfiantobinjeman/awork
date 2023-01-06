using Microsoft.EntityFrameworkCore.Migrations;

namespace AWork.Persistence.Migrations
{
    public partial class AddRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78b22bb4-758c-4971-babf-03551a789ab7", "441bdeb5-0055-4746-88db-aeecaa98e195", "EM", "EMPLOYEE" },
                    { "a582ffff-1e97-4d46-8b54-25ef26cb469d", "15e815c6-a009-4d07-9cb8-f7e8964ad07c", "SP", "SALES PERSON" },
                    { "6f110bde-7565-4f10-b319-cc567f1f7a2c", "34577fc8-df04-4db1-b4ca-2a38d879a899", "IN", "INDIVIDUAL CUSTOMER" },
                    { "fa296ab9-0726-47a1-983d-a39cf75c9cda", "164e4923-57f0-45ed-a05c-a3c0c36eed94", "SC", "STORE CONTACT" },
                    { "0d4d4b19-0bc7-4234-91f6-c65297d5dadd", "ef329846-175f-4bb0-a6b0-34d573e1c80c", "VC", "VENDOR CONTACT" },
                    { "e58e2bc4-0491-442c-b133-6c8b80d62147", "7294f6e1-a1ef-459e-98f9-b56c47d31a96", "GC", "GENERAL CONTACT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d4d4b19-0bc7-4234-91f6-c65297d5dadd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f110bde-7565-4f10-b319-cc567f1f7a2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78b22bb4-758c-4971-babf-03551a789ab7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a582ffff-1e97-4d46-8b54-25ef26cb469d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e58e2bc4-0491-442c-b133-6c8b80d62147");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa296ab9-0726-47a1-983d-a39cf75c9cda");
        }
    }
}
