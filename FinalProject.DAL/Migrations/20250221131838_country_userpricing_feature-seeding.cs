using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class country_userpricing_featureseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "FlagImage", "FlagUrl", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[] { 1, "03f58cc2-cb1e-499c-8a19-a4d0aa5bb9dfazerbaijan.png", "/Images/Countries/03f58cc2-cb1e-499c-8a19-a4d0aa5bb9dfazerbaijan.png", false, "Azerbaijan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UserPricings",
                columns: new[] { "Id", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, false, "Standart", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, false, "Premium", 20.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, false, "Super", 50.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Icon", "IsDeleted", "Name", "UpdatedAt", "UserPricingId" },
                values: new object[,]
                {
                    { 1, true, false, "24/7 Online Support", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, true, false, "Selling 1 Car", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsDeleted", "Name", "UpdatedAt", "UserPricingId" },
                values: new object[] { 3, false, "No Daily Info", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Icon", "IsDeleted", "Name", "UpdatedAt", "UserPricingId" },
                values: new object[,]
                {
                    { 4, true, false, "Selling 10 Cars", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, true, false, "Daily Info", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, true, false, "No Limit Selling Car", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, true, false, "Daily Info", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserPricings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPricings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserPricings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Address", "Email", "IsDeleted", "Logo", "LogoUrl", "Phone1", "Phone2", "PhotoWhy", "PhotoWhyUrl", "UpdatedAt" },
                values: new object[] { 1, "Nizami küçəsi 203B, AF Business House, 2-ci mərtəbə, Baku 1000", "turbobidofficial@gmail.com", false, "TurboBid_Logo.webp", "/Images/Settings/TurboBid_Logo.webp", "+994 (070)332-74-14", "(012)310-01-13", "photo-why-pic.jpg", "/Images/Settings/photo-why-pic.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
