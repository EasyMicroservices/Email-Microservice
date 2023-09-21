using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.EmailsMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailServers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    IsSSL = table.Column<bool>(type: "bit", nullable: false),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueueEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<long>(type: "bigint", nullable: false),
                    FromEmailId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueEmails_EmailServers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "EmailServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueEmails_Emails_FromEmailId",
                        column: x => x.FromEmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueueId = table.Column<long>(type: "bigint", nullable: false),
                    EmailEntityId = table.Column<long>(type: "bigint", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendEmails_Emails_EmailEntityId",
                        column: x => x.EmailEntityId,
                        principalTable: "Emails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SendEmails_QueueEmails_QueueId",
                        column: x => x.QueueId,
                        principalTable: "QueueEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_CreationDateTime",
                table: "Emails",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_DeletedDateTime",
                table: "Emails",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_IsDeleted",
                table: "Emails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ModificationDateTime",
                table: "Emails",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UniqueIdentity",
                table: "Emails",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_EmailServers_CreationDateTime",
                table: "EmailServers",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_EmailServers_DeletedDateTime",
                table: "EmailServers",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_EmailServers_IsDeleted",
                table: "EmailServers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmailServers_ModificationDateTime",
                table: "EmailServers",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_EmailServers_UniqueIdentity",
                table: "EmailServers",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_CreationDateTime",
                table: "QueueEmails",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_DeletedDateTime",
                table: "QueueEmails",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_FromEmailId",
                table: "QueueEmails",
                column: "FromEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_IsDeleted",
                table: "QueueEmails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_ModificationDateTime",
                table: "QueueEmails",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_ServerId",
                table: "QueueEmails",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueEmails_UniqueIdentity",
                table: "QueueEmails",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_CreationDateTime",
                table: "SendEmails",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_DeletedDateTime",
                table: "SendEmails",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_EmailEntityId",
                table: "SendEmails",
                column: "EmailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_IsDeleted",
                table: "SendEmails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_ModificationDateTime",
                table: "SendEmails",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_QueueId",
                table: "SendEmails",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmails_UniqueIdentity",
                table: "SendEmails",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendEmails");

            migrationBuilder.DropTable(
                name: "QueueEmails");

            migrationBuilder.DropTable(
                name: "EmailServers");

            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}
