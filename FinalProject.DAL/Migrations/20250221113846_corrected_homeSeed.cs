using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class corrected_homeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Address", "Email", "IsDeleted", "Logo", "LogoUrl", "Phone1", "Phone2", "PhotoWhy", "PhotoWhyUrl", "UpdatedAt" },
                values: new object[] { 1 , "Nizami küçəsi 203B", "turbobidofficial@gmail.com", false, "TurboBid_Logo.webp", "/Images/Settings/TurboBid_Logo.webp", "+994 (070)332-74-14", "(012)310-01-13", "photo-why-pic.jpg", "/Images/Settings/photo-why-pic.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
