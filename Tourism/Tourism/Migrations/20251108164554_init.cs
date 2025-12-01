using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourism.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardNumber);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    serviceId = table.Column<int>(type: "int", nullable: false),
                    serviceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMsgs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    providerId = table.Column<int>(type: "int", nullable: false),
                    providerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMsgs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceId = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tourists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    age = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    provider_Id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hotline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    verificationDocuments = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    creditCardCardNumber = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hotels_CreditCards_creditCardCardNumber",
                        column: x => x.creditCardCardNumber,
                        principalTable: "CreditCards",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    creditCardCardNumber = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verificationDocuments = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    verified = table.Column<bool>(type: "bit", nullable: false),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Merchants_CreditCards_creditCardCardNumber",
                        column: x => x.creditCardCardNumber,
                        principalTable: "CreditCards",
                        principalColumn: "CardNumber");
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hotline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verificationDocuments = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    creditCardCardNumber = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Restaurants_CreditCards_creditCardCardNumber",
                        column: x => x.creditCardCardNumber,
                        principalTable: "CreditCards",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    TourGuideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    experienceYears = table.Column<int>(type: "int", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    verificationDocuments = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    creditCardCardNumber = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuides", x => x.TourGuideId);
                    table.ForeignKey(
                        name: "FK_TourGuides_CreditCards_creditCardCardNumber",
                        column: x => x.creditCardCardNumber,
                        principalTable: "CreditCards",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristFeedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    touristId = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    targetId = table.Column<int>(type: "int", nullable: false),
                    targetType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristFeedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK_TouristFeedbacks_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberOfMembers = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    hotelId = table.Column<int>(type: "int", nullable: false),
                    accepted = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_hotelId",
                        column: x => x.hotelId,
                        principalTable: "Hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantSocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantSocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantSocialMedias_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    count = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    offer = table.Column<double>(type: "float", nullable: false),
                    merchantId = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accepted = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Merchants_merchantId",
                        column: x => x.merchantId,
                        principalTable: "Merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    pic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Meals_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<int>(type: "int", nullable: false),
                    numberOfPersons = table.Column<int>(type: "int", nullable: false),
                    bookingPrice = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    restaurant_id = table.Column<int>(type: "int", nullable: false),
                    restaurantid = table.Column<int>(type: "int", nullable: false),
                    accepted = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristRestaurants",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    touristId = table.Column<int>(type: "int", nullable: false),
                    restaurant_id = table.Column<int>(type: "int", nullable: false),
                    restaurantid = table.Column<int>(type: "int", nullable: false),
                    tableNumber = table.Column<int>(type: "int", nullable: false),
                    numberOfGuests = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristRestaurants", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_TouristRestaurants_Restaurants_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristRestaurants_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tourGuideId = table.Column<int>(type: "int", nullable: false),
                    accepted = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.id);
                    table.ForeignKey(
                        name: "FK_Trips_TourGuides_tourGuideId",
                        column: x => x.tourGuideId,
                        principalTable: "TourGuides",
                        principalColumn: "TourGuideId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristRooms",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    touristId = table.Column<int>(type: "int", nullable: false),
                    roomId = table.Column<int>(type: "int", nullable: false),
                    numberOfGuests = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristRooms", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_TouristRooms_Rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristRooms_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    TouristId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => new { x.TouristId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProducts_Tourists_TouristId",
                        column: x => x.TouristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteProducts",
                columns: table => new
                {
                    touristId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteProducts", x => new { x.touristId, x.productId });
                    table.ForeignKey(
                        name: "FK_FavouriteProducts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteProducts_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristProducts",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    touristId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristProducts", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_TouristProducts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristProducts_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristTrips",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    touristId = table.Column<int>(type: "int", nullable: false),
                    tripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristTrips", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_TouristTrips_Tourists_touristId",
                        column: x => x.touristId,
                        principalTable: "Tourists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristTrips_Trips_tripId",
                        column: x => x.tripId,
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteProducts_productId",
                table: "FavouriteProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_creditCardCardNumber",
                table: "Hotels",
                column: "creditCardCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_email",
                table: "Hotels",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_restaurantId",
                table: "Meals",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_creditCardCardNumber",
                table: "Merchants",
                column: "creditCardCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_email",
                table: "Merchants",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchantSocialMedias_MerchantId",
                table: "MerchantSocialMedias",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_merchantId",
                table: "Products",
                column: "merchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_creditCardCardNumber",
                table: "Restaurants",
                column: "creditCardCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_email",
                table: "Restaurants",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_hotelId",
                table: "Rooms",
                column: "hotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_restaurantid",
                table: "Tables",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_creditCardCardNumber",
                table: "TourGuides",
                column: "creditCardCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_email",
                table: "TourGuides",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TouristFeedbacks_touristId",
                table: "TouristFeedbacks",
                column: "touristId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristProducts_productId",
                table: "TouristProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristProducts_touristId",
                table: "TouristProducts",
                column: "touristId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristRestaurants_restaurantid",
                table: "TouristRestaurants",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_TouristRestaurants_touristId",
                table: "TouristRestaurants",
                column: "touristId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristRooms_roomId",
                table: "TouristRooms",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristRooms_touristId",
                table: "TouristRooms",
                column: "touristId");

            migrationBuilder.CreateIndex(
                name: "IX_Tourists_email",
                table: "Tourists",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TouristTrips_touristId",
                table: "TouristTrips",
                column: "touristId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristTrips_tripId",
                table: "TouristTrips",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_tourGuideId",
                table: "Trips",
                column: "tourGuideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DropTable(
                name: "FavouriteProducts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "InboxMsgs");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MerchantSocialMedias");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "TouristFeedbacks");

            migrationBuilder.DropTable(
                name: "TouristProducts");

            migrationBuilder.DropTable(
                name: "TouristRestaurants");

            migrationBuilder.DropTable(
                name: "TouristRooms");

            migrationBuilder.DropTable(
                name: "TouristTrips");

            migrationBuilder.DropTable(
                name: "VerificationRequests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Tourists");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
