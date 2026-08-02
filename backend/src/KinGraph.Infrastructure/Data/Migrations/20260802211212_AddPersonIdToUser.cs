using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinGraph.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "person_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_users_person_id",
                table: "users",
                column: "person_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_users_persons_person_id",
                table: "users",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_persons_person_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_person_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "users");
        }
    }
}
