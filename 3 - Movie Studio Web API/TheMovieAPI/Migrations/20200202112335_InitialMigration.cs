using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMovieAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    RentedOut = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Trivia = table.Column<string>(nullable: true),
                    RatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MoviesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studio_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(nullable: false),
                    StudioId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Studio_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Available", "Count", "Description", "Name", "RatedBy", "RentedOut", "Trivia" },
                values: new object[,]
                {
                    { 1, true, 3, "The Survivalist Hugh Glass is hunting the man who left him for dead, after a bear attack.", "The Revenant", "", 0, "" },
                    { 2, true, 3, "A young man, called Amsterdam is looking for revange after his father was murded in a gang war by Bill The Butcher", "Gangs Of New York", "", 0, "" },
                    { 3, true, 3, "A love story in ancient time. Achilles, Hector, Paris.", "Troy", "", 0, "" },
                    { 4, true, 3, "The outlaw Peter Quill is the savior of the galaxy, along side the other guardians.", "Guardians Of The Galaxy", "", 0, "" },
                    { 5, true, 3, "The next installment of the guardians saga. This time, Peter Quill learns more about his dangerous roots.", "Guardians Of The Galaxy Vol 2", "", 0, "" }
                });

            migrationBuilder.InsertData(
                table: "Studio",
                columns: new[] { "Id", "Location", "MoviesId", "Name" },
                values: new object[,]
                {
                    { 1, "Jönköping", null, "Filmstaden" },
                    { 2, "Stockholm", null, "Stockholms Skärgård filmstudio" },
                    { 3, "Lund", null, "Skåne studio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_MovieId",
                table: "Rating",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_StudioId",
                table: "Rating",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Studio_MoviesId",
                table: "Studio",
                column: "MoviesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
