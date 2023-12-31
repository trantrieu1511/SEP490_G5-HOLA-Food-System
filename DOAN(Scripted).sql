USE [master]
--Drop database SEP490_HFS
GO
/****** Object:  Database [SEP490_HFS]    Script Date: 09/10/2023 11:11:40 CH ******/
CREATE DATABASE [SEP490_HFS]
GO
USE [SEP490_HFS]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[birthDate] [date] NULL,
	[roleId] [int] NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phoneNumber] [bigint] NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[avatar] [nvarchar](100) NULL,
	[shopName] [nvarchar](50) NULL,
	[shopAddress] [nvarchar](max) NULL,
	[isOnline] [bit] NOT NULL,
	[walletBalance] [money] DEFAULT 0 NOT NULL,
	[manageBy] [int] NULL,
	[confirmEmail] [bit] not NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[CartItem](
	[foodId] [int] NOT NULL,
	[cartId] [int] NOT NULL,
	[amount] [int] NOT NULL,
	Foreign Key ([cartId]) REFERENCES [User](Userid),
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC,
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [int] IDENTITY(1,1) NOT NULL,
	[foodId] [int] NULL,
	[userId] [int] NULL,
	[feedbackMessage] [nvarchar](max) NULL,
	[star] [tinyint] NULL,
	[createdDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[status] [tinyint] NULL,
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
	[feedbackId] [int] NOT NULL,
	[replyMessage] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[status] [tinyint] NULL,
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
	[voteBy] int Not Null,
	Foreign Key ([voteBy]) REFERENCES [User](Userid),
PRIMARY KEY CLUSTERED 
(
	[voteId] ASC
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
	[shopId] int,
	[name] [nvarchar](100) NULL,
	[unitPrice] decimal,
	[description] [nvarchar](max) NULL,
	[categoryId] [int] NULL,
	[status] [tinyint] NULL,
	Foreign Key ([shopId]) REFERENCES [User](Userid),
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
		[publicId] [nvarchar](max) Null,
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
	[userId] [int] NOT NULL,
	[reportContent] [nvarchar](max) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
	[isDone] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[foodId] ASC,
	[userId] ASC
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
	[typeId] [int] NOT NULL,
	[content] [nvarchar](max) NULL,
	[createDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
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
/****** Object:  Table [dbo].[Order]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [int] NULL,
	[shopId][int],
	[orderDate] [datetime] NULL,
	[requiredDate] [datetime] NULL,
	[shippedDate] [datetime] NULL,
	[shipAddress] [nvarchar](max) NULL,
	[shipperId] [int] NULL,
	[voucherId] [int] NULL,
	[paymentMethod] [tinyint] NOT NULL,
	[status] [bit] NOT NULL,
	FOREIGN KEY([shopId]) REFERENCES [dbo].[User] ([userId]),
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
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[orderProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[postId] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NOT NULL,
	[postContent] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[status] [tinyint] NULL,
	Foreign Key ([shopId]) REFERENCES [User](Userid),
PRIMARY KEY CLUSTERED 
(
	[postId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
		[publicId] [nvarchar](max) Null,
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
	[userId] [int] NOT NULL,
	[reportContent] [nvarchar](max) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
	[isDone] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[postId] ASC,
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] NOT NULL,
	[roleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipAddress]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipAddress](
	[addressId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[addressInfo] [nvarchar](max) NOT NULL,
	[isDefaultAddress] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[addressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Voucher]    Script Date: 09/10/2023 11:11:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[voucherId] [int] IDENTITY(1,1) NOT NULL,
	[voucherName] [nvarchar](100) NULL,
	[status] [tinyint] NULL,
	[effectiveDate] [date] NULL,
	[expireDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[voucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MenuReport] ADD  DEFAULT (getdate()) FOR [createDate]
GO
ALTER TABLE [dbo].[MenuReport] ADD  DEFAULT (getdate()) FOR [updateDate]
GO
ALTER TABLE [dbo].[MenuReport] ADD  DEFAULT ((0)) FOR [isDone]
GO
ALTER TABLE [dbo].[Post] ADD  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[PostReport] ADD  DEFAULT (getdate()) FOR [createDate]
GO
ALTER TABLE [dbo].[PostReport] ADD  DEFAULT (getdate()) FOR [updateDate]
GO
ALTER TABLE [dbo].[PostReport] ADD  DEFAULT ((0)) FOR [isDone]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [isOnline]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Food] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([foodId])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Food]
GO



ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_UserId121] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_UserId121]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product21] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([foodId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_OrderDetail_Product21]
GO
ALTER TABLE [dbo].[FeedbackReply]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product1121] FOREIGN KEY([feedbackId])
REFERENCES [dbo].[Feedback] ([feedbackId])
GO
ALTER TABLE [dbo].[FeedbackReply] CHECK CONSTRAINT [FK_OrderDetail_Product1121]
GO
ALTER TABLE [dbo].[FeedbackVote]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_UserId1321] FOREIGN KEY([feedbackId])
REFERENCES [dbo].[Feedback] ([feedbackId])
GO
ALTER TABLE [dbo].[FeedbackVote] CHECK CONSTRAINT [FK_Feedback_UserId1321]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([categoryId])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Category]
GO
ALTER TABLE [dbo].[FoodImage]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order1] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([foodId])
GO
ALTER TABLE [dbo].[FoodImage] CHECK CONSTRAINT [FK_OrderDetail_Order1]
GO
ALTER TABLE [dbo].[MenuReport]  WITH CHECK ADD  CONSTRAINT [FK_MenuReport_Food] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([foodId])
GO
ALTER TABLE [dbo].[MenuReport] CHECK CONSTRAINT [FK_MenuReport_Food]
GO
ALTER TABLE [dbo].[MenuReport]  WITH CHECK ADD  CONSTRAINT [FK_MenuReport_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[MenuReport] CHECK CONSTRAINT [FK_MenuReport_User]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_NotificationType] FOREIGN KEY([typeId])
REFERENCES [dbo].[NotificationType] ([id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_NotificationType]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [CK_Voucher_Order1] FOREIGN KEY([voucherId])
REFERENCES [dbo].[Voucher] ([voucherId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [CK_Voucher_Order1]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Shipper1] FOREIGN KEY([shipperId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Shipper1]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User1111] FOREIGN KEY([customerId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User1111]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order11] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order11]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product111] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([foodId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product111]
GO
ALTER TABLE [dbo].[OrderProgress]  WITH CHECK ADD  CONSTRAINT [FK_Order_Shipper1111] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[OrderProgress] CHECK CONSTRAINT [FK_Order_Shipper1111]
GO
ALTER TABLE [dbo].[OrderProgress]  WITH CHECK ADD  CONSTRAINT [FK_Order_User111] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderProgress] CHECK CONSTRAINT [FK_Order_User111]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_UserId11] FOREIGN KEY([shopId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Feedback_UserId11]
GO
ALTER TABLE [dbo].[PostImage]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order2221] FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([postId])
GO
ALTER TABLE [dbo].[PostImage] CHECK CONSTRAINT [FK_OrderDetail_Order2221]
GO
ALTER TABLE [dbo].[PostReport]  WITH CHECK ADD  CONSTRAINT [FK_PostReport_Post] FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([postId])
GO
ALTER TABLE [dbo].[PostReport] CHECK CONSTRAINT [FK_PostReport_Post]
GO
ALTER TABLE [dbo].[PostReport]  WITH CHECK ADD  CONSTRAINT [FK_PostReport_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[PostReport] CHECK CONSTRAINT [FK_PostReport_User]
GO
ALTER TABLE [dbo].[ShipAddress]  WITH CHECK ADD  CONSTRAINT [FK_ShipAddress_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[ShipAddress] CHECK CONSTRAINT [FK_ShipAddress_User]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([manageBy])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [CK_Order_Dates11] CHECK  (([createdDate]<=[updateDate]))
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [CK_Order_Dates11]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [CK_Customer_ShipperID1] CHECK  (([customerId]<>[shipperId]))
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [CK_Customer_ShipperID1]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [CK_Order_Dates1111] CHECK  (([orderDate]<[requiredDate] AND [orderDate]<[shippedDate]))
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [CK_Order_Dates1111]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [CK_Order_Dates] CHECK  (([effectiveDate]<[expireDate]))
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [CK_Order_Dates]
GO
insert into [Role] values (1, 'Admin')
insert into [Role] values (2, 'Seller')
insert into [Role] values (3, 'Customer')
insert into [Role] values (4, 'Shipper')
insert into [Role] values (5, 'PostModerator')
insert into [Role] values (6, 'MenuModerator')
GO
USE [master]
GO
ALTER DATABASE [SEP490_HFS] SET  READ_WRITE 
GO