using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourism.Migrations
{
    /// <inheritdoc />
    public partial class initiii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Restaurants_restaurantid",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristRestaurants_Restaurants_restaurantid",
                table: "TouristRestaurants");

            migrationBuilder.DropColumn(
                name: "accepted",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "restaurant_id",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "pic",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "pic",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "restaurantid",
                table: "TouristRestaurants",
                newName: "restaurantId");

            migrationBuilder.RenameColumn(
                name: "restaurant_id",
                table: "TouristRestaurants",
                newName: "tableId");

            migrationBuilder.RenameIndex(
                name: "IX_TouristRestaurants_restaurantid",
                table: "TouristRestaurants",
                newName: "IX_TouristRestaurants_restaurantId");

            migrationBuilder.RenameColumn(
                name: "restaurantid",
                table: "Tables",
                newName: "restaurantId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Tables",
                newName: "booked");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_restaurantid",
                table: "Tables",
                newName: "IX_Tables_restaurantId");

            migrationBuilder.AddColumn<string>(
                name: "CreditCardId",
                table: "TouristRestaurants",
                type: "nvarchar(16)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "bookingPrice",
                table: "TouristRestaurants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "bookingPrice",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<byte[]>(
                name: "verificationDocuments",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "passwordHash",
                table: "Restaurants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Restaurants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Restaurants",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Meals",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Restaurantid",
                table: "InboxMsgs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Restaurantid",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TouristRestaurants_CreditCardId",
                table: "TouristRestaurants",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxMsgs_Restaurantid",
                table: "InboxMsgs",
                column: "Restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Restaurantid",
                table: "Images",
                column: "Restaurantid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Restaurants_Restaurantid",
                table: "Images",
                column: "Restaurantid",
                principalTable: "Restaurants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_InboxMsgs_Restaurants_Restaurantid",
                table: "InboxMsgs",
                column: "Restaurantid",
                principalTable: "Restaurants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Restaurants_restaurantId",
                table: "Tables",
                column: "restaurantId",
                principalTable: "Restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristRestaurants_CreditCards_CreditCardId",
                table: "TouristRestaurants",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CardNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristRestaurants_Restaurants_restaurantId",
                table: "TouristRestaurants",
                column: "restaurantId",
                principalTable: "Restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_Restaurantid",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_InboxMsgs_Restaurants_Restaurantid",
                table: "InboxMsgs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Restaurants_restaurantId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristRestaurants_CreditCards_CreditCardId",
                table: "TouristRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristRestaurants_Restaurants_restaurantId",
                table: "TouristRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_TouristRestaurants_CreditCardId",
                table: "TouristRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_InboxMsgs_Restaurantid",
                table: "InboxMsgs");

            migrationBuilder.DropIndex(
                name: "IX_Images_Restaurantid",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "TouristRestaurants");

            migrationBuilder.DropColumn(
                name: "bookingPrice",
                table: "TouristRestaurants");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Restaurantid",
                table: "InboxMsgs");

            migrationBuilder.DropColumn(
                name: "Restaurantid",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "restaurantId",
                table: "TouristRestaurants",
                newName: "restaurantid");

            migrationBuilder.RenameColumn(
                name: "tableId",
                table: "TouristRestaurants",
                newName: "restaurant_id");

            migrationBuilder.RenameIndex(
                name: "IX_TouristRestaurants_restaurantId",
                table: "TouristRestaurants",
                newName: "IX_TouristRestaurants_restaurantid");

            migrationBuilder.RenameColumn(
                name: "restaurantId",
                table: "Tables",
                newName: "restaurantid");

            migrationBuilder.RenameColumn(
                name: "booked",
                table: "Tables",
                newName: "status");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_restaurantId",
                table: "Tables",
                newName: "IX_Tables_restaurantid");

            migrationBuilder.AlterColumn<double>(
                name: "bookingPrice",
                table: "Tables",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "accepted",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "restaurant_id",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "verificationDocuments",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "passwordHash",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Restaurants",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "pic",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Meals",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<byte[]>(
                name: "pic",
                table: "Meals",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Restaurants_restaurantid",
                table: "Tables",
                column: "restaurantid",
                principalTable: "Restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristRestaurants_Restaurants_restaurantid",
                table: "TouristRestaurants",
                column: "restaurantid",
                principalTable: "Restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
