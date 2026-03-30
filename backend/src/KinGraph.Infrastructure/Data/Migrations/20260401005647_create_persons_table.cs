using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KinGraph.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_persons_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    gender = table.Column<string>(type: "text", nullable: true),
                    marital_status = table.Column<string>(type: "text", nullable: true),
                    blood_type = table.Column<int>(type: "integer", nullable: true),
                    nationality = table.Column<string>(type: "text", nullable: true),
                    _languages = table.Column<List<string>>(type: "text[]", nullable: false),
                    date_of_birth_value = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    email_address_value = table.Column<string>(type: "text", nullable: true),
                    height_unit = table.Column<int>(type: "integer", nullable: true),
                    height_value = table.Column<decimal>(type: "numeric", nullable: true),
                    occupation_name = table.Column<string>(type: "text", nullable: true),
                    occupation_salary = table.Column<decimal>(type: "numeric", nullable: true),
                    phone_number_country_code = table.Column<string>(type: "text", nullable: true),
                    phone_number_extension = table.Column<string>(type: "text", nullable: true),
                    phone_number_number = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<int>(type: "integer", nullable: true),
                    postal_code = table.Column<string>(type: "text", nullable: true),
                    street = table.Column<string>(type: "text", nullable: true),
                    place_of_birth_city_name = table.Column<string>(type: "text", nullable: true),
                    place_of_birth_state_name = table.Column<string>(type: "text", nullable: true),
                    weight_unit = table.Column<int>(type: "integer", nullable: true),
                    weight_value = table.Column<decimal>(type: "numeric", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persons", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "person_addresses",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    street = table.Column<string>(type: "text", nullable: false),
                    city_name = table.Column<string>(type: "text", nullable: false),
                    state_name = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_addresses_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_citizenships",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    country = table.Column<int>(type: "integer", nullable: false),
                    acquired_at = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_citizenships", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_citizenships_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_documents",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    type = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    expiration_date = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    issuing_country = table.Column<int>(type: "integer", nullable: true),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_documents_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_extra_fields",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_extra_fields", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_extra_fields_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_hobbies",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_hobbies", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_hobbies_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_relationships",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    related_person_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: true),
                    source_person_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(
                        type: "character varying(21)",
                        maxLength: 21,
                        nullable: false
                    ),
                    generation_offset = table.Column<int>(type: "integer", nullable: true),
                    degree = table.Column<int>(type: "integer", nullable: true),
                    is_by_marriage = table.Column<bool>(type: "boolean", nullable: true),
                    is_half = table.Column<bool>(type: "boolean", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_relationships", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_relationships_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id"
                    );
                    table.ForeignKey(
                        name: "fk_person_relationships_persons_source_person_id",
                        column: x => x.source_person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "person_social_profiles",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    platform = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_social_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_social_profiles_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_addresses_person_id",
                table: "person_addresses",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_citizenships_person_id",
                table: "person_citizenships",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_documents_person_id",
                table: "person_documents",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_extra_fields_person_id",
                table: "person_extra_fields",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_hobbies_person_id",
                table: "person_hobbies",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_relationships_person_id",
                table: "person_relationships",
                column: "person_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_relationships_source_person_id_related_person_id_type",
                table: "person_relationships",
                columns: new[] { "source_person_id", "related_person_id", "type" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_person_social_profiles_person_id",
                table: "person_social_profiles",
                column: "person_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "person_addresses");

            migrationBuilder.DropTable(name: "person_citizenships");

            migrationBuilder.DropTable(name: "person_documents");

            migrationBuilder.DropTable(name: "person_extra_fields");

            migrationBuilder.DropTable(name: "person_hobbies");

            migrationBuilder.DropTable(name: "person_relationships");

            migrationBuilder.DropTable(name: "person_social_profiles");

            migrationBuilder.DropTable(name: "persons");
        }
    }
}
