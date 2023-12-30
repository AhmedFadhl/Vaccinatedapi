using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccinatedapi.Migrations
{
    /// <inheritdoc />
    public partial class _1st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    decs = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "doses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "hospital_Types",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospital_Types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "parents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_card_number = table.Column<int>(type: "int", nullable: false),
                    pirth_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    marital_status = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pirth_place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile_number = table.Column<int>(type: "int", nullable: true),
                    phone_number = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "parents_Kids",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parents_id = table.Column<int>(type: "int", nullable: false),
                    kids_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parents_Kids", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "vaccine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    days_to_take = table.Column<int>(type: "int", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dose_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_vaccine_doses_dose_id",
                        column: x => x.dose_id,
                        principalTable: "doses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hospitals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    city_id = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_hospitals_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hospitals_hospital_Types_type_id",
                        column: x => x.type_id,
                        principalTable: "hospital_Types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kids",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pirth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pirth_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    blood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    host_id = table.Column<int>(type: "int", nullable: false),
                    father_id = table.Column<int>(type: "int", nullable: true),
                    mother_id = table.Column<int>(type: "int", nullable: true),
                    motherID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kids", x => x.ID);
                    table.ForeignKey(
                        name: "FK_kids_hospitals_host_id",
                        column: x => x.host_id,
                        principalTable: "hospitals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kids_parents_father_id",
                        column: x => x.father_id,
                        principalTable: "parents",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_kids_parents_motherID",
                        column: x => x.motherID,
                        principalTable: "parents",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "kid_Vaccines",
                columns: table => new
                {
                    kids_Id = table.Column<int>(type: "int", nullable: false),
                    vaccines_Id = table.Column<int>(type: "int", nullable: false),
                    taken = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kid_Vaccines", x => new { x.kids_Id, x.vaccines_Id });
                    table.ForeignKey(
                        name: "FK_kid_Vaccines_kids_kids_Id",
                        column: x => x.kids_Id,
                        principalTable: "kids",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kid_Vaccines_vaccine_vaccines_Id",
                        column: x => x.vaccines_Id,
                        principalTable: "vaccine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hospitals_city_id",
                table: "hospitals",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_hospitals_type_id",
                table: "hospitals",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_kid_Vaccines_vaccines_Id",
                table: "kid_Vaccines",
                column: "vaccines_Id");

            migrationBuilder.CreateIndex(
                name: "IX_kids_father_id",
                table: "kids",
                column: "father_id");

            migrationBuilder.CreateIndex(
                name: "IX_kids_host_id",
                table: "kids",
                column: "host_id");

            migrationBuilder.CreateIndex(
                name: "IX_kids_motherID",
                table: "kids",
                column: "motherID");

            migrationBuilder.CreateIndex(
                name: "IX_vaccine_dose_id",
                table: "vaccine",
                column: "dose_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advices");

            migrationBuilder.DropTable(
                name: "kid_Vaccines");

            migrationBuilder.DropTable(
                name: "parents_Kids");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "kids");

            migrationBuilder.DropTable(
                name: "vaccine");

            migrationBuilder.DropTable(
                name: "hospitals");

            migrationBuilder.DropTable(
                name: "parents");

            migrationBuilder.DropTable(
                name: "doses");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "hospital_Types");
        }
    }
}
