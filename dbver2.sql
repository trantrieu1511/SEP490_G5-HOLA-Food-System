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
	--[firstName] [nvarchar](50) NOT NULL,
	--[lastName] [nvarchar](50) NOT NULL,
	--[gender] [nvarchar](10) NULL,
	--[birthDate] [date] NULL,
	shopName NVARCHAR(50),
	shopAddress NVARCHAR(MAX),
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[isOnline] [bit] NOT NULL,
	[walletBalance] [money] NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[Status] [tinyint] not null DEFAULT('0'), -- The hien rang nguoi dung nay (Seller) da duoc admin xac nhan cho phep kinh doanh o HFS chua
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
	[createDate][datetime] null,
	[businessCode]  NVARCHAR(50) NULL,
	[Note]  NVARCHAR(MAX),
	--[lat][float]null,
	--[lng][float]null,
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
	[isOnline] [bit] NOT NULL,
	[walletBalance] [money] NULL,
	[confirmedEmail] [bit] NOT NULL DEFAULT('false'),
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
		[createDate][datetime] null,
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
	[isOnline] [bit] NOT NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
	[banLimit] int default 25, -- Gioi han ban bai viet cua mot post moderator
	[reportApprovalLimit] int default 25, -- Gioi approve/not approve post report cua mot post moderator
	[createDate][datetime] null,
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
	[isOnline] [bit] NOT NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
	[banLimit] int default 25, -- Gioi han ban thuc pham cua mot menu moderator
	[reportApprovalLimit] int default 25, -- Gioi approve/not approve food report cua mot menu moderator
	[createDate][datetime] null,
)

CREATE TABLE [dbo].[Accountant](
	[accountantId] [nvarchar](50) NOT NULL primary key,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[email] [nvarchar](100) UNIQUE NOT NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[isOnline] [bit] NOT NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[isBanned] [bit] not null DEFAULT('false'),
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
	[createDate][datetime] null,
)

--CREATE TABLE [dbo].[Customer](
--	[customerId] [nvarchar](50) NOT NULL primary key,
--	[firstName] [nvarchar](50) NOT NULL,
--	[lastName] [nvarchar](50) NOT NULL,
--	[gender] [nvarchar](10) NULL,
--	[birthDate] [date] NULL,
--	[email] [nvarchar](100) UNIQUE NOT NULL,
--	[phoneNumber] [nvarchar](11) NULL,
--	[PasswordSalt] [varbinary](max) NOT NULL,
--	[PasswordHash] [varbinary](max) NOT NULL,
--	[isOnline] [bit] NOT NULL DEFAULT('false'),
--	[walletBalance] [money] NULL,
--	[confirmedEmail] [bit] not NULL DEFAULT('false'),
--	[isBanned] [bit] not null DEFAULT('false'),		
--	[refreshToken] [varchar](max),
--	[refreshTokenExpiryTime] [datetime],
--)
CREATE TABLE [dbo].[Customer](
    [customerId] [nvarchar](50) NOT NULL PRIMARY KEY,
    [firstName] [nvarchar](50) NOT NULL,
    [lastName] [nvarchar](50) NOT NULL,
    [gender] [nvarchar](10) NULL,
    [birthDate] [date] NULL,
    [email] [nvarchar](100) UNIQUE NOT NULL,
    [phoneNumber] [nvarchar](11) NULL,
    [PasswordSalt] [varbinary](max) NOT NULL,
    [PasswordHash] [varbinary](max) NOT NULL,
    [isOnline] [bit] NOT NULL DEFAULT('false'),
    [walletBalance] [money] NULL,
    [confirmedEmail] [bit] NOT NULL DEFAULT('false'),
    --[banStartTime] [datetime] NULL,
    --[banEndTime] [datetime] NULL,
    [numberOfViolations] [int] NOT NULL DEFAULT(0),
    [refreshToken] [varchar](max),
    [refreshTokenExpiryTime] [datetime],
	[createDate][datetime] null,
	--CONSTRAINT CK_S_Dates CHECK ([banStartTime] < [banEndTime]),
);

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
	[isOnline] [bit] NOT NULL DEFAULT('false'),
	[manageBy] [nvarchar](50) NULL,
	[confirmedEmail] [bit] not NULL DEFAULT('false'),
	[Status] [tinyint] not null DEFAULT('0'), -- The hien rang nguoi dung nay (Shipper) da duoc admin xac nhan cho phep kinh doanh o HFS chua
	[refreshToken] [varchar](max),
	[refreshTokenExpiryTime] [datetime],
	[createDate][datetime] null,
	[Note]  NVARCHAR(MAX) null,
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
	[createDate] [datetime] null,
	[description] [nvarchar](max) NULL,
	[categoryId] [int] NULL,
	[status] [tinyint] NULL,
	[reportedTimes] [int] default(0) NUll, -- Thể hiện cho số lần mà thực phẩm này nhận phải tố cáo hợp lệ (Đã thông qua kiểm duyệt của menu mod) đến từ khách hàng
	[banBy] [nvarchar](50) NUll,
	[banDate] [datetime] Null,
	[banNote] [nvarchar](MAX) NUll,
	Foreign Key ([banBy]) REFERENCES [MenuModerator](ModId),
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
	[CreateDate][datetime],
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
	[updateDate] [datetime] NULL,
	[status] [tinyint] NOT NULL, -- 0: Pending: KH moi tao report, menu mod chua xu ly report, 1: Approved - Menu mod chap nhan to cao va da xu ly xong thuc pham bi to cao, 2: NotApproved: Menu mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
	[note] [nvarchar](MAX) NULL,
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
/****** Object:  Table [dbo].[Notification]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[id] [int] NOT NULL,
	[lang] [int] not null,
	[sendBy] [nvarchar](50) null,
	[receiver] [nvarchar](50) null,
	[type] [int] NOT NULL,
	[title] [nvarchar](50) NULL,
	[content] [nvarchar](500) NULL,
	[createDate] [datetime] NULL,
	[isRead] [bit] NULL,
	primary key([id], [lang]),
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
	[effectiveDate] [datetime] NULL,
	[expireDate] [datetime] NULL,
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
	[customerPhone][nvarchar](50) NULL,
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
	[orderId] [int] Null,
	[foodId] [int] NULL,
	[customerId] [nvarchar](50)  NULL,
	[feedbackMessage] [nvarchar](max) NULL,
	[star] [tinyint] NULL,
	[createdDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[status] [tinyint] NULL,
	FOREIGN KEY([orderId]) REFERENCES [dbo].[Order] ([OrderId]),
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

/****** Object:  Table [dbo].[PostVote]    Script Date: 09/10/2023 11:11:40 CH ******/
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
	--[reportedTimes] [int] default(0) NUll, -- Thể hiện cho số lần mà bài đăng này nhận phải tố cáo hợp lệ (Đã thông qua kiểm duyệt của post mod) đến từ khách hàng
	[banBy] [nvarchar](50) NUll,
	[banDate] [datetime] Null,
	[banNote] [nvarchar](MAX) Null, -- Lý do khác hoặc ghi chú bổ sung cho việc bị ban vì > 3 tố cáo hợp lệ đến từ khách hàng
	Foreign Key ([banBy]) REFERENCES [PostModerator](ModId),
	Foreign Key ([sellerId]) REFERENCES [Seller](SellerId),
	primary key([postId]),
	)

GO

/****** Object:  Table [dbo].[Post]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].PostVote(
	[voteId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[isLike] [bit] NULL,
	[createdDate] [datetime] NULL,
	[voteBy] [nvarchar](50) Not Null,
	Foreign Key ([voteBy]) REFERENCES [Customer](customerId),
	Foreign Key ([PostId]) REFERENCES Post(PostId),
PRIMARY KEY CLUSTERED 
(
	[voteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PostVote]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].PostVote(
	[voteId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[isLike] [bit] NULL,
	[createdDate] [datetime] NULL,
	[voteBy] [nvarchar](50) Not Null,
	Foreign Key ([voteBy]) REFERENCES [Customer](customerId),
	Foreign Key ([PostId]) REFERENCES Post(PostId),
PRIMARY KEY CLUSTERED 
(
	[voteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
	[updateDate] [datetime] NULL,
	[updateBy] [nvarchar](50) NULL,
	[status] [tinyint] NOT NULL, -- 0: Pending: KH moi tao report, post mod chua xu ly report, 1: Approved - Post mod chap nhan to cao va da xu ly xong bai viet bi to cao, 2: NotApproved: Post mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
	[note] [nvarchar](MAX) NULL,
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



-- Tạo bảng Mời làm Shipper (Invitation)
CREATE TABLE Invitation (
    InvitationId INT PRIMARY KEY IDENTITY(1, 1),
    SellerID NVARCHAR(50) NOT NULL,
    ShipperID NVARCHAR(50) NOT NULL,
     Accepted TINYINT NOT NULL DEFAULT 0,
    --CONSTRAINT PK_Invitation PRIMARY KEY (SellerID, ShipperID),
    CONSTRAINT FK_Invitation_Seller FOREIGN KEY (SellerID) REFERENCES Seller(sellerId),
    CONSTRAINT FK_Invitation_Shipper FOREIGN KEY (ShipperID) REFERENCES Shipper(shipperId)
);

CREATE TABLE [dbo].[TransactionHistory](
	[transactionId] [int] IDENTITY(1,1) NOT NULL,
	[senderId] [nvarchar](50) NOT NULL,
	[recieverId] [nvarchar](50) NULL,
	[TransactionType] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](200) NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[CreateDate] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[status] [tinyint] NULL,
	[AcceptBy] [nvarchar](50) NULL,
	FOREIGN KEY([AcceptBy]) REFERENCES [dbo].[Accountant] ([accountantId]),
PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE ChatMessage (
    MessageId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId NVARCHAR(50) NOT NULL,
    SellerId NVARCHAR(50) NOT NULL,
    SenderType BIT NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    SentAt DATETIME NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0, -- Thêm cột để đánh dấu tin nhắn đã đọc hay chưa
    CONSTRAINT FK_ChatMessage_Customer_Sender FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId),
    CONSTRAINT FK_ChatMessage_Seller_Receiver FOREIGN KEY (SellerId) REFERENCES Seller (SellerId)
);
CREATE TABLE Groups (
    Name NVARCHAR(150) PRIMARY KEY 

);
CREATE TABLE Connections (
    [ConnectionId] NVARCHAR(50) PRIMARY KEY, 
	[email] NVARCHAR(100) not null,
	[GroupName] NVARCHAR(150) not null,
	CONSTRAINT FK_ChatMessage_Groups FOREIGN KEY ([GroupName]) REFERENCES Groups (Name)
);
CREATE TABLE [ProfileImage] (
	[imageId] [int] PRIMARY KEY IDENTITY(1, 1),
	[userId] NVARCHAR(50) NOT NULL,
	[path] [nvarchar](max) NOT NULL,
	[isReplaced] [bit] NOT NULL, -- 0: Tức là hình ảnh vẫn còn đang được sử dụng và chưa bị thay thế. 1: Hình ảnh đã bị thay thế bởi người dùng.
)

CREATE TABLE [dbo].[Comment](
    [commentId] [int] IDENTITY(1,1) NOT NULL,
    [postId] [int] NOT NULL,
    [customerId] [nvarchar](50) NOT NULL,
    [commentContent] [nvarchar](1500) NULL,
    [createdDate] [datetime] NULL,
    FOREIGN KEY ([postId]) REFERENCES [Post]([postId]),
    FOREIGN KEY ([customerId]) REFERENCES [Customer]([customerId]),
    PRIMARY KEY ([commentId])
)
CREATE TABLE [dbo].[FeedBackImage](
	[imagefeedbackId] [int] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[feedbackId] [int] NULL,
	[path] [nvarchar](max) NULL,
	FOREIGN KEY([feedbackId]) REFERENCES [dbo].[FeedBack] ([feedbackId]),
)

CREATE TABLE [dbo].[WalletTransferCode](
	[codeId] [int] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[userId][nvarchar](50) NULL,
	[code] [nvarchar](50) NULL,
	[isUsed] bit Null,
	[expiredDate] datetime NULL,
)


--CREATE TABLE [dbo].[ShipperReport](
--	[shipperId] [nvarchar](50) NOT NULL,
--	[reportBy] [nvarchar](50) NOT NULL,
--	[reportContent] [nvarchar](max) NOT NULL,
--	[createDate] [datetime] NOT NULL,
--	[updateDate] [datetime] NULL,
--	[updateBy] [nvarchar](50) NULL,
--	[status] [tinyint] NOT NULL, -- 0: Pending: KH moi tao report, post mod chua xu ly report, 1: Approved - Post mod chap nhan to cao va da xu ly xong bai viet bi to cao, 2: NotApproved: Post mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
--	[note] [nvarchar](MAX) NULL,
--	FOREIGN KEY([reportBy]) REFERENCES [dbo].[Seller] ([sellerId]),
--	FOREIGN KEY([updateBy]) REFERENCES [dbo].[Admin] ([adminId]),
--	FOREIGN KEY([shipperId]) REFERENCES [dbo].[Shipper] ([shipperId]),
--	);

	--CREATE TABLE [dbo].[ShipperReport](
	--[shipperId] [nvarchar](50) NOT NULL,
	--[reportBy] [nvarchar](50) NOT NULL,
	--[reportContent] [nvarchar](max) NOT NULL,
	--[createDate] [datetime] NOT NULL,
	--[updateDate] [datetime] NULL,
	--[updateBy] [nvarchar](50) NULL,
	--[status] [tinyint] NOT NULL, -- 0: Pending: KH moi tao report, post mod chua xu ly report, 1: Approved - Post mod chap nhan to cao va da xu ly xong bai viet bi to cao, 2: NotApproved: Post mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
	--[note] [nvarchar](MAX) NULL,
	--FOREIGN KEY([reportBy]) REFERENCES [dbo].[Seller] ([sellerId]),
	--FOREIGN KEY([updateBy]) REFERENCES [dbo].[Admin] ([adminId]),
	--FOREIGN KEY([shipperId]) REFERENCES [dbo].[Shipper] ([shipperId]),
	--);

	CREATE TABLE [dbo].[SellerReport](
	[sellerReportId] [int] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[sellerId] [nvarchar](50) NOT NULL,
	[reportBy] [nvarchar](50) NOT NULL,
	[reportContent] [nvarchar](max) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NULL,
	[updateBy] [nvarchar](50) NULL,
	[status] [tinyint] NOT NULL, -- 0: Pending: KH moi tao report, post mod chua xu ly report, 1: Approved - Post mod chap nhan to cao va da xu ly xong bai viet bi to cao, 2: NotApproved: Post mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
	[note] [nvarchar](MAX) NULL,
	FOREIGN KEY([reportBy]) REFERENCES [dbo].[Customer] ([customerId]),
	FOREIGN KEY([updateBy]) REFERENCES [dbo].[Admin] ([adminId]),
	FOREIGN KEY([sellerId]) REFERENCES [dbo].[Seller] ([sellerId]),
	);

	CREATE TABLE [dbo].[SellerReportImage](
	[imageSellerReportId] [int] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[sellerReportId] [int] NULL,
	[path] [nvarchar](max) NULL,
	FOREIGN KEY([sellerReportId]) REFERENCES [dbo].[SellerReport] ([sellerReportId]),
)
	CREATE TABLE [dbo].[SellerLicenseImage](
	[imageLicenseId] [int] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[sellerId] [nvarchar](50) NOT NULL,
	[path] [nvarchar](max) NULL,
	[isReplaced] [bit] NOT NULL, -- 0: Tức là hình ảnh vẫn còn đang được sử dụng và chưa bị thay thế. 1: Hình ảnh đã bị thay thế bởi người dùng.
	FOREIGN KEY([sellerId]) REFERENCES [dbo].[Seller] ([sellerId]),
)

ALTER TABLE [dbo].[Customer]
ADD [isPhoneVerified] [bit] NOT NULL DEFAULT('false'),
    [otpToken] [nvarchar](max) NULL,
    [otpTokenExpiryTime] [int] NULL;


ALTER TABLE [dbo].[Seller]
ADD [isPhoneVerified] [bit] NOT NULL DEFAULT('false'),
    [otpToken] [nvarchar](max) NULL,
    [otpTokenExpiryTime] [int] NULL;
	
ALTER TABLE [dbo].[Shipper]
ADD [isPhoneVerified] [bit] NOT NULL DEFAULT('false'),
    [otpToken] [nvarchar](max) NULL,
    [otpTokenExpiryTime] [int] NULL;
