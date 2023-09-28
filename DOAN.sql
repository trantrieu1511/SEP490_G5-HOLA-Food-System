Create Database SEP490
Use SEP490
--DROP DATABASE SEP490
CREATE TABLE Role
(
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
)

CREATE TABLE [Users]
(
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthDate DATE,
    RoleID INT NOT null,
    Email NVARCHAR(100) NOT NULL,
	 Avatar NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10),
    [Status] bit ,
	 CONSTRAINT FK_User_Role FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
)
CREATE TABLE [Address]
( 
   [AddressId] INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
	[Address] NVARCHAR(mAX) NOT NULL,
	[Status] bit,
   	 CONSTRAINT FK_User_Role11 FOREIGN KEY (UserID) REFERENCES [Users](UserID)
)
CREATE TABLE Category
(
    CategoryID INT PRIMARY KEY,
    Name NVARCHAR(100),
	[Status] bit ,
);
CREATE TABLE Food
(
    FoodID INT PRIMARY KEY,
    Name NVARCHAR(100),
    Description NVARCHAR(MAX),
    CategoryID INT,
	[Status] bit ,
    CONSTRAINT FK_Food_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
Create TABLE FoodImage (
 FoodID INT,
 [Path] NVARCHAR(MAX),
 CONSTRAINT FK_OrderDetail_Order1 FOREIGN KEY (FoodID) REFERENCES Food(FoodID),
);
CREATE TABLE Voucher
(
    VoucherId INT PRIMARY KEY IDENTITY(1,1),
    VoucherName NVARCHAR(100),
    Status NVARCHAR(50),
    StartDate DATE,
    EndDate DATE,
	CONSTRAINT CK_Order_Dates CHECK (StartDate < EndDate),
);
CREATE TABLE [Order]
(
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATETIME,
    RequiredDate DATETIME,
    ShippedDate DATETIME,
    ShipAddress NVARCHAR(MAX),
    ShipperID INT,
	VoucherId int,
	[Status] bit ,
    CONSTRAINT FK_Order_User1111 FOREIGN KEY (CustomerID) REFERENCES [Users](UserID),
    CONSTRAINT FK_Order_Shipper1 FOREIGN KEY (ShipperID) REFERENCES [Users](UserID),
	CONSTRAINT CK_Voucher_Order1 FOREIGN KEY (VoucherId) REFERENCES [Voucher](VoucherId),
    CONSTRAINT CK_Order_Dates1111 CHECK (OrderDate < RequiredDate AND OrderDate < ShippedDate),
    CONSTRAINT CK_Customer_ShipperID1 CHECK (CustomerID!=ShipperID)
);
CREATE TABLE [OrderProgress]
(
    OrderProgressId INT PRIMARY KEY IDENTITY(1,1),
     Note  NVARCHAR(MAX),
    CreateDate DATETIME,
    [Image] NVARCHAR(MAX),
	OrderID INT,
	[Status] bit,
	UserId INT,
    CONSTRAINT FK_Order_User111 FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    CONSTRAINT FK_Order_Shipper1111 FOREIGN KEY (UserId) REFERENCES [Users](UserID),

);

CREATE TABLE OrderDetail
(
    OrderID INT,
    FoodID INT,
    UnitPrice DECIMAL(10, 2),
    Quantity INT,
	Primary key(OrderID,FoodID),
    CONSTRAINT FK_OrderDetail_Order11 FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    CONSTRAINT FK_OrderDetail_Product111 FOREIGN KEY (FoodID) REFERENCES Food(FoodID),
	[Status] bit ,
);

	CREATE TABLE Feedback
	(
		FeedbackId INT PRIMARY KEY IDENTITY(1,1),
		FoodID INT,
		UserId INT,
		FeedbackText NVARCHAR(MAX),
		CreatedDate DATETIME,
		 UpdateDate DATETIME,
		[Status] bit ,
		CONSTRAINT FK_Feedback_UserId121 FOREIGN KEY (UserId) REFERENCES [Users](UserId),
			CONSTRAINT CK_Order_Dates11 CHECK (CreatedDate <= UpdateDate ),
		CONSTRAINT FK_OrderDetail_Product21 FOREIGN KEY (FoodID) REFERENCES Food(FoodID),
	);

	CREATE TABLE ReplyFeedback(
		FeedbackId INT PRIMARY KEY IDENTITY(1,1),
		UserId INT,
		ReplyText NVARCHAR(MAX),
		CreatedDate DATETIME,
		 UpdateDate DATETIME,
		[Status] bit ,
		CONSTRAINT FK_Feedback_UserId111112 FOREIGN KEY (UserId) REFERENCES [Users](UserId),
		CONSTRAINT FK_OrderDetail_Product1121 FOREIGN KEY (FeedbackId) REFERENCES Feedback(FeedbackId),
	);
	CREATE TABLE VoteFeedback
	(
		FeedbackId INT PRIMARY KEY IDENTITY(1,1),
		UserId INT,
    VoteType bit,
    CreatedDate DATETIME,
    CONSTRAINT FK_Feedback_UserId13211 FOREIGN KEY (UserId) REFERENCES [Users](UserId),
	CONSTRAINT FK_Feedback_UserId1321 FOREIGN KEY (FeedbackId) REFERENCES Feedback(FeedbackId),
);


CREATE TABLE Post
(
    PostId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
	Content NVARCHAR(MAX),
    CreatedDate DATETIME,
     [Status] bit ,
    CONSTRAINT FK_Feedback_UserId11 FOREIGN KEY (UserId) REFERENCES [Users](UserId),

);
Create TABLE PostImage (
 PostId INT,
 [Path] NVARCHAR(MAX),
	 CONSTRAINT FK_OrderDetail_Order2221 FOREIGN KEY (PostId) REFERENCES Post(PostId),
);
CREATE TABLE Report
(
    ReportId INT PRIMARY KEY IDENTITY(1,1),
	PostId Int,
    UserId INT,
	ReportContent NVARCHAR(MAX),
    CreatedDate DATETIME,
     [Status] bit ,
    CONSTRAINT FK_Feedback_UserId321 FOREIGN KEY (UserId) REFERENCES [Users](UserId),
	 CONSTRAINT FK_Report_UserId12 FOREIGN KEY (PostId) REFERENCES Post(PostId),

);
