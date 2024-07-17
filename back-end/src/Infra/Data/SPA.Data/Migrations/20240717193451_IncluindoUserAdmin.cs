using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPA.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

            migrationBuilder.Sql(@"DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'unique_username'
    ) THEN
        ALTER TABLE ""AspNetUsers"" ADD CONSTRAINT unique_username UNIQUE (""UserName"");
    END IF;
END $$;");

            migrationBuilder.Sql("INSERT INTO \"AspNetUsers\" (\"Id\", \"Name\", \"UserName\", \"NormalizedUserName\", \"Email\", \"NormalizedEmail\", \"EmailConfirmed\", \"PhoneNumberConfirmed\", \"PasswordHash\", \"TwoFactorEnabled\", \"LockoutEnabled\", \"AccessFailedCount\", \"RegistrationDate\") VALUES (uuid_generate_v4(), 'Admin', 'admin', UPPER('admin'), 'admin@hotmail.com', UPPER('admin@hotmail.com'), FALSE, FALSE, 'AQAAAAIAAYagAAAAEG3bCGFEMhxPDLpN2hqB/8pRBfvd78tVwJFoHC0WDufPbIzYFCOxfYGOUBvnUu/ulQ==', FALSE, TRUE, 0, NOW() AT TIME ZONE 'UTC') ON CONFLICT (\"UserName\") DO NOTHING;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");

            migrationBuilder.Sql(@"DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'unique_username'
    ) THEN
        ALTER TABLE ""AspNetUsers"" DROP CONSTRAINT unique_username;
    END IF;
END $$;");

            migrationBuilder.Sql("DELETE FROM \"AspNetUsers\" WHERE \"UserName\" = 'admin';");
        }
    }
}
