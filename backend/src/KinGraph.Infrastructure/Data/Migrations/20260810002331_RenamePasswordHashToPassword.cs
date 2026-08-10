using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinGraph.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamePasswordHashToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_relationships_persons_person_id",
                table: "person_relationships");

            migrationBuilder.DropIndex(
                name: "ix_person_relationships_person_id",
                table: "person_relationships");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "person_relationships");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "person_id",
                table: "person_relationships",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relationships_person_id",
                table: "person_relationships",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_relationships_persons_person_id",
                table: "person_relationships",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");
        }
    }
}
