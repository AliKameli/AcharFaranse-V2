using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class initTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PictureFilePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EstimatedWageCost = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: true),
                    ParentJobCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCategories_JobCategories_ParentJobCategoryId",
                        column: x => x.ParentJobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobsRequestedCount = table.Column<int>(type: "int", nullable: false),
                    JobsAcceptedByWorkersCount = table.Column<int>(type: "int", nullable: false),
                    JobsFailedByWorkersCount = table.Column<int>(type: "int", nullable: false),
                    JobsCanceledByCostumerCount = table.Column<int>(type: "int", nullable: false),
                    JobsBeingDoneByWorkersCount = table.Column<int>(type: "int", nullable: false),
                    JobsDoneSuccessfullyByWorkersCount = table.Column<int>(type: "int", nullable: false),
                    TotalWagePaid = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalMaterialCostPaid = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalMoneyPaid = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalCompanyProfitEarnedFromCostumer = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    RatingByWorkers = table.Column<short>(type: "smallint", nullable: false),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalSecurityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserCityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costumers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costumers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Costumers_Cities_UserCityId",
                        column: x => x.UserCityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    JobsProposedCount = table.Column<int>(type: "int", nullable: false),
                    JobsAcceptedCount = table.Column<int>(type: "int", nullable: false),
                    JobsFailedByWorkerCount = table.Column<int>(type: "int", nullable: false),
                    JobsCanceledByCostumersCount = table.Column<int>(type: "int", nullable: false),
                    JobsDoingCount = table.Column<int>(type: "int", nullable: false),
                    JobsDoneSuccessfullyCount = table.Column<int>(type: "int", nullable: false),
                    TotalWageEarned = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalMaterialCostEarned = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalMoneyEarned = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    TotalCompanyProfitEarnedFromWorker = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    MoneyOwedToCompany = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    CompanySharePercentage = table.Column<short>(type: "smallint", nullable: false),
                    RatingByCostumers = table.Column<short>(type: "smallint", nullable: false),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalSecurityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserCityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Cities_UserCityId",
                        column: x => x.UserCityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostumerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    GpsCoordinates = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsReceivingByCostumer = table.Column<bool>(type: "bit", nullable: false),
                    ReceivingPersonFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceivingPersonPhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressCityId = table.Column<int>(type: "int", nullable: false),
                    CostumerId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostumerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostumerAddresses_Cities_AddressCityId",
                        column: x => x.AddressCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostumerAddresses_Costumers_CostumerId",
                        column: x => x.CostumerId,
                        principalTable: "Costumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCategoryWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategoryWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCategoryWorkers_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCategoryWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CostumerEndNote = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    WorkerEndNote = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OnlinePaymentReceiptInfo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsOnlinePayment = table.Column<bool>(type: "bit", nullable: false),
                    JobAcceptedByWorkerDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    JobStartedByWorkerDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    JobClosedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    CostumerRatingForWorker = table.Column<short>(type: "smallint", nullable: true),
                    WorkerRatingForCostumer = table.Column<short>(type: "smallint", nullable: true),
                    CostumerEstimatedFinalCost = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: true),
                    WageCost = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    MaterialCost = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    FinalCost = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    CostumerId = table.Column<int>(type: "int", nullable: false),
                    CostumerAddressId = table.Column<int>(type: "int", nullable: true),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    JobCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_CostumerAddresses_CostumerAddressId",
                        column: x => x.CostumerAddressId,
                        principalTable: "CostumerAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Costumers_CostumerId",
                        column: x => x.CostumerId,
                        principalTable: "Costumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostumerAddresses_AddressCityId",
                table: "CostumerAddresses",
                column: "AddressCityId");

            migrationBuilder.CreateIndex(
                name: "IX_CostumerAddresses_CostumerId",
                table: "CostumerAddresses",
                column: "CostumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Costumers_NationalSecurityId",
                table: "Costumers",
                column: "NationalSecurityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costumers_UserCityId",
                table: "Costumers",
                column: "UserCityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_Name",
                table: "JobCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_ParentJobCategoryId",
                table: "JobCategories",
                column: "ParentJobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategoryWorkers_JobCategoryId",
                table: "JobCategoryWorkers",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategoryWorkers_WorkerId",
                table: "JobCategoryWorkers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CostumerAddressId",
                table: "Jobs",
                column: "CostumerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CostumerId",
                table: "Jobs",
                column: "CostumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_WorkerId",
                table: "Jobs",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_NationalSecurityId",
                table: "Workers",
                column: "NationalSecurityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserCityId",
                table: "Workers",
                column: "UserCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "JobCategoryWorkers");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CostumerAddresses");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Costumers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
