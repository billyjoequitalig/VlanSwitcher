using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VLAN_Switching.Migrations
{
    /// <inheritdoc />
    public partial class initializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SwitchPorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwitchIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SwitchPorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNumber = table.Column<int>(type: "int", nullable: false),
                    NTlogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwitchPortId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Users_tbl_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "tbl_Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Users_tbl_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "tbl_Roles",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "FK_tbl_Users_tbl_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tbl_Status",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_tbl_Users_tbl_SwitchPorts_SwitchPortId",
                        column: x => x.SwitchPortId,
                        principalTable: "tbl_SwitchPorts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "tbl_Campaigns",
                columns: new[] { "Id", "Name", "VlanId" },
                values: new object[] { 1, "Shopee", 76 });

            migrationBuilder.InsertData(
                table: "tbl_Roles",
                columns: new[] { "RoleID", "Role" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Agent" }
                });

            migrationBuilder.InsertData(
                table: "tbl_Status",
                columns: new[] { "StatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "tbl_SwitchPorts",
                columns: new[] { "Id", "PortName", "SwitchIP" },
                values: new object[] { 1, "Gi1/0/38", "10.5.2.34" });

            migrationBuilder.InsertData(
                table: "tbl_Users",
                columns: new[] { "Id", "CampaignId", "FullName", "IdNumber", "NTlogin", "RoleID", "StatusId", "SwitchPortId" },
                values: new object[] { 1, 1, "Joe Quitalig", 2025005704, "Joe.Quitalig", 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Users_CampaignId",
                table: "tbl_Users",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Users_RoleID",
                table: "tbl_Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Users_StatusId",
                table: "tbl_Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Users_SwitchPortId",
                table: "tbl_Users",
                column: "SwitchPortId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Users");

            migrationBuilder.DropTable(
                name: "tbl_Campaigns");

            migrationBuilder.DropTable(
                name: "tbl_Roles");

            migrationBuilder.DropTable(
                name: "tbl_Status");

            migrationBuilder.DropTable(
                name: "tbl_SwitchPorts");
        }
    }
}
