--Create Database SEP490_HFS
--USE [master]

USE SEP490_HFS
--DROP DATABASE SEP490_HFS

CREATE TABLE [Role]
(
	roleId INT PRIMARY KEY,
	roleName NVARCHAR(50) NOT NULL
)

CREATE TABLE [User]
(
	userId INT PRIMARY KEY IDENTITY(1,1),
	firstName NVARCHAR(50) NOT NULL,
	lastName NVARCHAR(50) NOT NULL,
	gender NVARCHAR(10),
	birthDate DATE,
	roleId INT NOT NULL,
	email NVARCHAR(100) NOT NULL,
	phoneNumber BIGINT,
	PasswordSalt VARBINARY(MAX) NOT NULL,
	PasswordHash VARBINARY(MAX) NOT NULL,
	avatar NVARCHAR(100),
	shopName NVARCHAR(50),
	shopAddress NVARCHAR(MAX),
	isOnline bit default 0 NOT NULL,
	walletBalance MONEY, 
	manageBy INT, --Truong nay the hien rang shipper se thuoc quyen quan ly cua mot shop (seller) nao do
	CONSTRAINT FK_User_Role FOREIGN KEY (roleId) REFERENCES [Role](roleId),
	CONSTRAINT FK_User_User FOREIGN KEY (manageBy) REFERENCES [User](userId)
)

CREATE TABLE [ShipAddress]
( 
	[addressId] INT PRIMARY KEY IDENTITY(1,1),
	userId INT NOT NULL,
	[addressInfo] NVARCHAR(MAX) NOT NULL,
	[isDefaultAddress] bit,
	CONSTRAINT FK_ShipAddress_User FOREIGN KEY (UserId) REFERENCES [User](userId)
)

CREATE TABLE Category
(
	categoryId INT PRIMARY KEY,
	[name] NVARCHAR(100),
	[status] bit
);

CREATE TABLE Food
(
	foodId INT PRIMARY KEY,
	[name] NVARCHAR(100),
	[description] NVARCHAR(MAX),
	categoryId INT,
	[status] bit,
	CONSTRAINT FK_Food_Category FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);

CREATE TABLE FoodImage (
	foodId INT,
	[path] NVARCHAR(MAX),
	CONSTRAINT FK_OrderDetail_Order1 FOREIGN KEY (foodId) REFERENCES Food(foodId)
);

CREATE TABLE Voucher
(
	voucherId INT PRIMARY KEY IDENTITY(1,1),
	voucherName NVARCHAR(100),
	[status] NVARCHAR(50),
	effectiveDate DATE,
	[expireDate] DATE,
	CONSTRAINT CK_Order_Dates CHECK (effectiveDate < [expireDate])
);

CREATE TABLE [Order]
(
	orderId INT PRIMARY KEY IDENTITY(1,1),
	customerId INT,
	orderDate DATETIME,
	requiredDate DATETIME,
	shippedDate DATETIME,
	shipAddress NVARCHAR(MAX),
	shipperId INT,
	voucherId int,
	[status] bit,
	CONSTRAINT FK_Order_User1111 FOREIGN KEY (customerId) REFERENCES [User](userId),
	CONSTRAINT FK_Order_Shipper1 FOREIGN KEY (shipperId) REFERENCES [User](userId),
	CONSTRAINT CK_Voucher_Order1 FOREIGN KEY (voucherId) REFERENCES [Voucher](voucherId),
	CONSTRAINT CK_Order_Dates1111 CHECK (orderDate < requiredDate AND orderDate < shippedDate),
	CONSTRAINT CK_Customer_ShipperID1 CHECK (customerId != shipperId)
);

CREATE TABLE [OrderProgress]
(
	orderProgressId INT PRIMARY KEY IDENTITY(1,1),
	note NVARCHAR(MAX),
	createDate DATETIME,
	[image] NVARCHAR(MAX),
	orderId INT,
	[status] bit,
	userId INT,
	CONSTRAINT FK_Order_User111 FOREIGN KEY (orderId) REFERENCES [Order](orderId),
	CONSTRAINT FK_Order_Shipper1111 FOREIGN KEY (userId) REFERENCES [User](userId)
);

CREATE TABLE OrderDetail
(
	orderId INT,
	foodId INT,
	unitPrice DECIMAL(10, 2),
	quantity INT,
	Primary key(orderId, foodId),
	CONSTRAINT FK_OrderDetail_Order11 FOREIGN KEY (orderId) REFERENCES [Order](orderId),
	CONSTRAINT FK_OrderDetail_Product111 FOREIGN KEY (foodId) REFERENCES Food(foodId),
	[status] bit
);

CREATE TABLE Feedback
(
	feedbackId INT PRIMARY KEY IDENTITY(1,1),
	foodId INT,
	userId INT,
	feedbackMessage NVARCHAR(MAX),
	createdDate DATETIME,
	updateDate DATETIME,
	[status] bit,
	CONSTRAINT FK_Feedback_UserId121 FOREIGN KEY (userId) REFERENCES [User](userId),
	CONSTRAINT CK_Order_Dates11 CHECK (createdDate <= updateDate),
	CONSTRAINT FK_OrderDetail_Product21 FOREIGN KEY (foodId) REFERENCES Food(foodId)
);

CREATE TABLE FeedbackReply (
	replyId INT PRIMARY KEY IDENTITY(1,1),
	feedbackId INT NOT NULL,
	replyMessage NVARCHAR(MAX),
	createdDate DATETIME,
	updateDate DATETIME,
	[status] bit,
	CONSTRAINT FK_OrderDetail_Product1121 FOREIGN KEY (feedbackId) REFERENCES Feedback(feedbackId)
);

CREATE TABLE FeedbackVote
(
	voteId INT PRIMARY KEY IDENTITY(1,1),
	feedbackId INT NOT NULL,
	isLike bit,
	createdDate DATETIME,
	CONSTRAINT FK_Feedback_UserId1321 FOREIGN KEY (feedbackId) REFERENCES Feedback(feedbackId)
);

CREATE TABLE Post
(
	postId INT PRIMARY KEY IDENTITY(1,1),
	userId INT NOT NULL,
	postContent NVARCHAR(MAX),
	createdDate DATETIME default GETDATE(),
	[status] bit,
	CONSTRAINT FK_Feedback_UserId11 FOREIGN KEY (userId) REFERENCES [User](userId)
);

CREATE TABLE PostImage (
	postId INT,
	[path] NVARCHAR(MAX),
	CONSTRAINT FK_OrderDetail_Order2221 FOREIGN KEY (postId) REFERENCES Post(postId)
);


