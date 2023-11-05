USE [master]
--Drop database SEP490_HFS_2
GO
/****** Object:  Database [SEP490_HFS]    Script Date: 09/10/2023 11:11:40 CH ******/
CREATE DATABASE [SEP490_HFS_2]
GO
USE [SEP490_HFS_2]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 09/10/2023 11:11:40 CH ******/


CREATE TABLE [dbo].[Seller](
	[sellerId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL,
	[walletBalance] [money] NULL,
	shopName NVARCHAR(50),
	shopAddress NVARCHAR(MAX),
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[isVerified] [bit] not null DEFAULT('false'), -- The hien rang nguoi dung nay (Seller) da duoc admin xac nhan cho phep kinh doanh o HFS chua
)

CREATE TABLE [dbo].[Admin](
	[adminId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL,
	[walletBalance] [money] NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
)

CREATE TABLE [dbo].[PostModerator](
	[modId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false')
)

CREATE TABLE [dbo].[MenuModerator](
	[modId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false')
)

CREATE TABLE [dbo].[Customer](
	[customerId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL DEFAULT('false'),
	[walletBalance] [money] NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),		
)

CREATE TABLE [dbo].[Shipper](
	[shipperId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[isOnline] [bit] NOT NULL DEFAULT('false'),
	[manageBy] [nvarchar](50) NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[isVerified] [bit] not null DEFAULT('false'), -- The hien rang nguoi dung nay (Shipper) da duoc admin xac nhan cho phep kinh doanh o HFS chua
	CONSTRAINT FK_Shiper_Seller FOREIGN KEY (manageBy) REFERENCES [Seller]([sellerId]),
)

/****** Object:  Table [dbo].[Category]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Food]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[foodId] [int] IDENTITY(1,1) NOT NULL,
	[sellerId] [nvarchar](50) not null,
	[name] [nvarchar](100) NULL,
	[unitPrice] decimal,
	[description] [nvarchar](max) NULL,
	[categoryId] [int] NULL,
	[status] [tinyint] NULL,
	Foreign Key ([sellerId]) REFERENCES [Seller](sellerId),
	FOREIGN KEY([categoryId]) REFERENCES [dbo].[Category] ([categoryId]),
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



-- cart item
CREATE TABLE [dbo].[CartItem](
	[foodId] [int] NOT NULL,
	[cartId] [nvarchar](50) NOT NULL,
	[amount] [int] NOT NULL,
	Foreign Key ([cartId]) REFERENCES [Customer](customerId),
	Foreign key ([foodId]) REFERENCES [Food](foodId),
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC,
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FoodImage]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodImage](
	[imageId] [int] IDENTITY(1,1) NOT NULL,
	[foodId] [int] NULL,
	[path] [nvarchar](max) NULL,
	FOREIGN KEY([foodId]) REFERENCES [dbo].[Food] ([foodId]),
PRIMARY KEY CLUSTERED 
(
	[imageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuReport]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuReport](
	[foodId] [int] NOT NULL,
	[reportBy] [nvarchar](50) NOT NULL,
	[reportContent] [nvarchar](max) NOT NULL,
	[updateBy] [nvarchar](50) null,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
	[isDone] [bit] NOT NULL,
	Foreign Key ([reportBy]) REFERENCES [Customer](customerId),
	Foreign Key ([updateBy]) REFERENCES [MenuModerator](modId),
	Foreign Key ([foodId]) REFERENCES Food(foodId),
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC,
	[reportBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NotificationType]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sendBy] [nvarchar](50) null,
	[receiver] [nvarchar](50) null,
	[typeId] [int] NOT NULL,
	[title] [nvarchar](50) NULL,
	[content] [nvarchar](500) NULL,
	[createDate] [datetime] NULL,
	[isRead] [bit] NULL,
	primary key([id]),
	Foreign Key ([typeId]) REFERENCES [NotificationType]([id]),
	)
GO

/****** Object:  Table [dbo].[Voucher]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[voucherId] [int] IDENTITY(1,1) NOT NULL,
	[sellerId] [nvarchar](50) null,
	[code] [nvarchar](100) NOT NULL,
	[discount_amount] DECIMAL(10, 2) NOT NULL,
	[minimum_order_value] DECIMAL(10, 2),
	[status] [tinyint] NULL,
	[effectiveDate] [date] NULL,
	[expireDate] [date] NULL,
	FOREIGN KEY([sellerId]) REFERENCES [dbo].[Seller] ([sellerId]),
	CONSTRAINT CK_Order_Dates CHECK (effectiveDate < [expireDate]),
PRIMARY KEY CLUSTERED 
(
	[voucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [nvarchar](50) NULL,
	[sellerId][nvarchar](50),
	[orderDate] [datetime] NULL,
	[requiredDate] [datetime] NULL,
	[shippedDate] [datetime] NULL,
	[shipAddress] [nvarchar](max) NULL,
	[shipperId] [nvarchar](50) NULL,
	[voucherId] [int] NULL,
	[status] [tinyint] NULL,
	[paymentMethod] [tinyint] NULL,
	FOREIGN KEY([sellerId]) REFERENCES [dbo].[Seller] ([sellerId]),
	FOREIGN KEY([customerId]) REFERENCES [dbo].[Customer] ([customerId]),
	FOREIGN KEY([shipperId]) REFERENCES [dbo].[Shipper] ([shipperId]),
	FOREIGN KEY([voucherId]) REFERENCES [dbo].[Voucher] ([voucherId]),
	CONSTRAINT CK_Order_Dates_2710 CHECK (orderDate < requiredDate AND orderDate < shippedDate),
PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[orderId] [int] NOT NULL,
	[foodId] [int] NOT NULL,
	[unitPrice] [decimal](10, 2) NULL,
	[quantity] [int] NULL,
	[status] [tinyint] NULL,
	FOREIGN KEY([orderId]) REFERENCES [dbo].[Order] ([orderId]),
	FOREIGN KEY([foodId]) REFERENCES [dbo].[Food] ([foodId]),
PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[foodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProgress]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProgress](
	[orderProgressId] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NULL,
	[createDate] [datetime] NULL,
	[image] [nvarchar](max) NULL,
	[orderId] [int] NULL,
	[status] [tinyint] NULL,
	[sellerId] [nvarchar](50) NULL,
	[customerId] [nvarchar](50) NULL,
	[shipperId] [nvarchar](50) NULL,
	FOREIGN KEY([sellerId]) REFERENCES [dbo].[Seller] ([sellerId]),
	FOREIGN KEY([customerId]) REFERENCES [dbo].[customer] (customerId),
	FOREIGN KEY([shipperId]) REFERENCES [dbo].[Shipper] ([shipperId]),
	FOREIGN KEY([orderId]) REFERENCES [dbo].[Order] ([orderId]),
PRIMARY KEY CLUSTERED 
(
	[orderProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [int] IDENTITY(1,1) NOT NULL,
	[foodId] [int] NULL,
	[customerId] [nvarchar](50)  NULL,
	[feedbackMessage] [nvarchar](max) NULL,
	[star] [tinyint] NULL,
	[createdDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[status] [tinyint] NULL,
	FOREIGN KEY([customerId]) REFERENCES [dbo].[Customer] ([customerId]),
	FOREIGN KEY([foodId]) REFERENCES [dbo].[Food] ([foodId]),
	CONSTRAINT CK_FeedBack_Dates CHECK (createdDate <= updateDate),
PRIMARY KEY CLUSTERED 
(
	[feedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FeedbackReply]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackReply](
	[replyId] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [nvarchar](50) NULL,
	[sellerId] [nvarchar](50) NULL,
	[feedbackId] [int] not null,
	[replyMessage] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[status] [tinyint] NULL,
	foreign key ([customerId]) references Customer(customerId),
	foreign key ([sellerId]) references Seller(sellerId),
	foreign key (feedbackId) references Feedback(feedbackId),
PRIMARY KEY CLUSTERED 
(
	[replyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackVote]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackVote](
	[voteId] [int] IDENTITY(1,1) NOT NULL,
	[feedbackId] [int] NOT NULL,
	[isLike] [bit] NULL,
	[createdDate] [datetime] NULL,
	[voteBy] [nvarchar](50) Not Null,
	Foreign Key ([voteBy]) REFERENCES [Customer](customerId),
	Foreign Key ([feedbackId]) REFERENCES [Feedback](feedbackId),
PRIMARY KEY CLUSTERED 
(
	[voteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Post]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[postId] [int] IDENTITY(1,1) NOT NULL,
	[sellerId] [nvarchar](50) NOT NULL,
	[postContent] [nvarchar](1500) NULL,
	[createdDate] [datetime] NULL,
	[status] [tinyint] NULL,
	Foreign Key ([sellerId]) REFERENCES [Seller](SellerId),
	primary key([postId]),
	)
GO
/****** Object:  Table [dbo].[PostImage]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostImage](
	[imageId] [int] IDENTITY(1,1) NOT NULL,
	[postId] [int] NULL,
	[path] [nvarchar](max) NULL,
	FOREIGN KEY([postId]) REFERENCES [dbo].[Post] ([postId]),
PRIMARY KEY CLUSTERED 
(
	[imageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostReport]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostReport](
	[postId] [int] NOT NULL,
	[reportBy] [nvarchar](50) NOT NULL,
	[reportContent] [nvarchar](max) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
	[updateBy] [nvarchar](50) null,
	[isDone] [bit] NOT NULL,
	FOREIGN KEY([reportBy]) REFERENCES [dbo].[Customer] ([customerId]),
	FOREIGN KEY([updateBy]) REFERENCES [dbo].[PostModerator] ([modId]),
	FOREIGN KEY([postId]) REFERENCES [dbo].[Post] ([postId]),
PRIMARY KEY CLUSTERED 
(
	[postId] ASC,
	[reportBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipAddress]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipAddress](
	[addressId] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [nvarchar](50) NOT NULL,
	[addressInfo] [nvarchar](max) NOT NULL,
	[isDefaultAddress] [bit] NULL,
	FOREIGN KEY([customerId]) REFERENCES [dbo].[Customer] ([customerId]),
PRIMARY KEY CLUSTERED 
(
	[addressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE SellerBan (
    banSellerId INT PRIMARY KEY IDENTITY(1, 1),
    sellerId NVARCHAR(50) NOT NULL,
    Reason NVARCHAR(255),
    CreateDate DATETIME NOT NULL,
    CONSTRAINT FK_SellerBan_Seller FOREIGN KEY (sellerId) REFERENCES Seller(sellerId)
);

-- Bảng lưu trữ thông tin ban khách hàng
CREATE TABLE CustomerBan (
    banCustomerId INT PRIMARY KEY IDENTITY(1, 1),
    customerId NVARCHAR(50) NOT NULL,
    Reason NVARCHAR(255),
    CreateDate DATETIME NOT NULL,
    CONSTRAINT FK_CustomerBan_Customer FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);
CREATE TABLE Chat (
    ChatId INT PRIMARY KEY IDENTITY(1,1),
    SenderId NVARCHAR(50) NOT NULL,
    ReceiverId NVARCHAR(50) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    SentAt DATETIME NOT NULL,
    CONSTRAINT FK_Chat_Customer_Sender FOREIGN KEY (SenderId) REFERENCES Customer (CustomerId),
    CONSTRAINT FK_Chat_Customer_Receiver FOREIGN KEY (ReceiverId) REFERENCES Customer (CustomerId),
    CONSTRAINT FK_Chat_Seller_Sender FOREIGN KEY (SenderId) REFERENCES Seller (SellerId),
    CONSTRAINT FK_Chat_Seller_Receiver FOREIGN KEY (ReceiverId) REFERENCES Seller (SellerId)
);