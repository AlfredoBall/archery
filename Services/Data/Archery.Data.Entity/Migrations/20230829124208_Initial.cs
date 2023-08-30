using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archery.Data.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lane",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lane", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeagueID = table.Column<int>(type: "int", nullable: false),
                    LaneID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tournament_Lane_LaneID",
                        column: x => x.LaneID,
                        principalTable: "Lane",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournament_League_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "League",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Player_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservation_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Reservation_Tournament_TournamentID",
                        column: x => x.TournamentID,
                        principalTable: "Tournament",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    TournamentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Set_Tournament_TournamentID",
                        column: x => x.TournamentID,
                        principalTable: "Tournament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    SetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Score_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_Set_SetID",
                        column: x => x.SetID,
                        principalTable: "Set",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lane_Identifier",
                table: "Lane",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_League_Name",
                table: "League",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_MemberID",
                table: "Player",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamID_MemberID",
                table: "Player",
                columns: new[] { "TeamID", "MemberID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TeamID",
                table: "Reservation",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TournamentID",
                table: "Reservation",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Score_PlayerID",
                table: "Score",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Score_SetID",
                table: "Score",
                column: "SetID");

            migrationBuilder.CreateIndex(
                name: "IX_Set_Ordinal_TournamentID",
                table: "Set",
                columns: new[] { "Ordinal", "TournamentID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Set_TournamentID",
                table: "Set",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Name",
                table: "Team",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_LaneID",
                table: "Tournament",
                column: "LaneID");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_LeagueID",
                table: "Tournament",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_Name",
                table: "Tournament",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "Lane");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
