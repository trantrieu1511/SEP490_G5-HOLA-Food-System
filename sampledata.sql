USE [SEP490_HFS_2]
GO
INSERT [dbo].[Seller] ([sellerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [shopName], [shopAddress], [confirmedEmail], [isBanned], [isVerified]) VALUES (N'SE00000001', N'duoc', N'nguyen', N'male', CAST(N'2001-11-01' AS Date), N'duocng@gmail.com', NULL, 0x0704A3841E2F7A11FC71966E1DD207EA18A77615D73787649D926075AAC61A8E36C010B7E7EC1931207B1899859203E68EEBC82343CD2478320906B22302D60D, 0x6209AD510CAB1731FAAD1067F60A1D608BE35ABBCE9C6FD4EA2CBAA7CA801F73,0, NULL, N'Cơm ngon quán', N'tan xa', 0, 0, 0)
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned]) VALUES (N'CU00000001', N'duoc', N'nguyen', N'male', CAST(N'2001-11-01' AS Date), N'duocng.2409@gmail.com', NULL, 0x5E1B0D8712C3764CB7B13EA104114FEF5323230DC885EF0C01D93B2AF99762538CAB8E6E570DCACD6BF2015D881C020C0D1D63F5665C91ED3610AD6B2D2046CB, 0xE57FC7004BB884B6F378FAFA9F6242428205E0A926CAAC30402E6795E73A2E51, 0, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1, N'Cơm', NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [description], [categoryId], [status]) VALUES (1, N'SE00000001', N'Cơm thịt xiên nướng + xúc xích', CAST(35 AS Decimal(18, 0)), N'Cơm bao gồm thịt xiên, xúc xích, rau và 1 món phụ', 1, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [description], [categoryId], [status]) VALUES (2, N'SE00000001', N'Cơm thịt xiên nướng + xúc xích', CAST(35 AS Decimal(18, 0)), N'Cơm bao gồm thịt xiên, xúc xích, rau và 1 món phụ', 1, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [description], [categoryId], [status]) VALUES (3, N'SE00000001', N'Cơm thịt xiên nướng + xúc xích', CAST(35 AS Decimal(18, 0)), N'Cơm bao gồm thịt xiên, xúc xích, rau và 1 món phụ', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 
GO
INSERT [dbo].[Feedback] ([feedbackId], [foodId], [customerId], [feedbackMessage], [star], [createdDate], [updateDate], [status]) VALUES (1, 1, N'CU00000001', N'Cơm ngon lắm!', 5, CAST(N'2023-11-07T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Feedback] ([feedbackId], [foodId], [customerId], [feedbackMessage], [star], [createdDate], [updateDate], [status]) VALUES (2, 1, N'CU00000001', N'abc', 5, CAST(N'2023-11-09T06:14:07.627' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[FeedbackReply] ON 
GO
INSERT [dbo].[FeedbackReply] ([replyId], [customerId], [sellerId], [feedbackId], [replyMessage], [createdDate], [updateDate], [status]) VALUES (1, NULL, N'SE00000001', 1, N'Cảm ơn bạn nhé!', CAST(N'2023-11-07T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[FeedbackReply] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (2, N'CU00000001', N'SE00000001', CAST(N'2023-10-31T00:00:00.000' AS DateTime), CAST(N'2023-11-02T00:00:00.000' AS DateTime), CAST(N'2023-11-02T00:00:00.000' AS DateTime), N'ha noi', NULL, NULL, 6, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (3, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:38:15.503' AS DateTime), NULL, NULL, N'default', NULL, NULL, 6, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (4, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:43:40.460' AS DateTime), NULL, NULL, N'Tan xa', NULL, NULL, 4, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (5, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:46:12.970' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 1, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (6, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:48:14.460' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 3, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (7, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:53:07.200' AS DateTime), NULL, NULL, N'Tan xa', NULL, NULL, 2, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (8, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T16:53:42.857' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 4, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (9, N'CU00000001', N'SE00000001', CAST(N'2023-11-02T18:33:39.913' AS DateTime), NULL, NULL, N'Tan xa', NULL, NULL, 5, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (10, N'CU00000001', N'SE00000001', CAST(N'2023-11-09T12:26:58.300' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 0, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (11, N'CU00000001', N'SE00000001', CAST(N'2023-11-09T12:27:00.540' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 0, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (12, N'CU00000001', N'SE00000001', CAST(N'2023-11-09T12:27:38.467' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 0, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (13, N'CU00000001', N'SE00000001', CAST(N'2023-11-09T12:28:55.890' AS DateTime), NULL, NULL, N'Dai Hoc FPT', NULL, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProgress] ON 
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (1, N'abc', CAST(N'2023-10-31T00:00:00.000' AS DateTime), NULL, 2, NULL, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (2, N'Order success! Wait seller.', CAST(N'2023-11-02T16:38:15.770' AS DateTime), NULL, 3, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (3, N'Order success! Wait seller.', CAST(N'2023-11-02T16:43:40.477' AS DateTime), NULL, 4, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (4, N'Order success! Wait seller.', CAST(N'2023-11-02T16:46:13.103' AS DateTime), NULL, 5, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (5, N'Order success! Wait seller.', CAST(N'2023-11-02T16:48:14.590' AS DateTime), NULL, 6, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (6, N'Order success! Wait seller.', CAST(N'2023-11-02T16:53:07.320' AS DateTime), NULL, 7, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (7, N'Order success! Wait seller.', CAST(N'2023-11-02T16:53:42.873' AS DateTime), NULL, 8, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (8, N'Order success! Wait seller.', CAST(N'2023-11-02T18:33:40.123' AS DateTime), NULL, 9, 0, N'SE00000001', N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (9, N'asda', CAST(N'2023-11-09T05:12:25.070' AS DateTime), NULL, 4, 6, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (10, N'ádasd', CAST(N'2023-11-09T06:34:02.017' AS DateTime), NULL, 4, 6, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (11, N'Order success! Wait seller.', CAST(N'2023-11-09T12:26:58.417' AS DateTime), NULL, 10, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (12, N'Order success! Wait seller.', CAST(N'2023-11-09T12:27:00.553' AS DateTime), NULL, 11, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (13, N'Order success! Wait seller.', CAST(N'2023-11-09T12:27:38.473' AS DateTime), NULL, 12, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (14, N'Order success! Wait seller.', CAST(N'2023-11-09T12:29:07.097' AS DateTime), NULL, 13, 0, NULL, N'CU00000001', NULL)
GO
SET IDENTITY_INSERT [dbo].[OrderProgress] OFF
GO
SET IDENTITY_INSERT [dbo].[FeedbackVote] ON 
GO
INSERT [dbo].[FeedbackVote] ([voteId], [feedbackId], [isLike], [createdDate], [voteBy]) VALUES (1, 1, 1, CAST(N'2023-11-08T01:12:54.097' AS DateTime), N'CU00000001')
GO
SET IDENTITY_INSERT [dbo].[FeedbackVote] OFF
GO
SET IDENTITY_INSERT [dbo].[ShipAddress] ON 
GO
INSERT [dbo].[ShipAddress] ([addressId], [customerId], [addressInfo], [isDefaultAddress]) VALUES (1, N'CU00000001', N'Tan xa', 1)
GO
INSERT [dbo].[ShipAddress] ([addressId], [customerId], [addressInfo], [isDefaultAddress]) VALUES (2, N'CU00000001', N'Dai Hoc FPT', NULL)
GO
SET IDENTITY_INSERT [dbo].[ShipAddress] OFF
GO
INSERT [dbo].[CartItem] ([foodId], [cartId], [amount]) VALUES (1, N'CU00000001', 21)
GO
INSERT [dbo].[CartItem] ([foodId], [cartId], [amount]) VALUES (2, N'CU00000001', 5)
GO
SET IDENTITY_INSERT [dbo].[FoodImage] ON 
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1, 1, N'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2, 2, N'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3, 3, N'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (4, 1, N'https://img.freepik.com/free-photo/big-sandwich-hamburger-with-juicy-beef-burger-cheese-tomato-red-onion-wooden-table_2829-19631.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (5, 1, N'https://img.freepik.com/free-photo/big-sandwich-hamburger-with-juicy-beef-burger-cheese-tomato-red-onion-wooden-table_2829-19631.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (6, 1, N'https://img.freepik.com/free-photo/chicken-wings-barbecue-sweetly-sour-sauce-picnic-summer-menu-tasty-food-top-view-flat-lay_2829-6471.jpg?size=626&ext=jpg&ga=GA1.1.1880011253.1699488000&semt=sph')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (7, 1, N'https://img.freepik.com/free-photo/chicken-wings-barbecue-sweetly-sour-sauce-picnic-summer-menu-tasty-food-top-view-flat-lay_2829-6471.jpg?size=626&ext=jpg&ga=GA1.1.1880011253.1699488000&semt=sph')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (8, 1, N'https://img.freepik.com/free-photo/chicken-wings-barbecue-sweetly-sour-sauce-picnic-summer-menu-tasty-food-top-view-flat-lay_2829-6471.jpg?size=626&ext=jpg&ga=GA1.1.1880011253.1699488000&semt=sph')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (9, 1, N'https://img.freepik.com/free-photo/chicken-wings-barbecue-sweetly-sour-sauce-picnic-summer-menu-tasty-food-top-view-flat-lay_2829-6471.jpg?size=626&ext=jpg&ga=GA1.1.1880011253.1699488000&semt=sph')
GO
SET IDENTITY_INSERT [dbo].[FoodImage] OFF
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (2, 1, CAST(35.00 AS Decimal(10, 2)), 10, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (3, 1, CAST(30.00 AS Decimal(10, 2)), 6, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (4, 1, CAST(30.00 AS Decimal(10, 2)), 9, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (5, 1, CAST(30.00 AS Decimal(10, 2)), 6, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (6, 1, CAST(30.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (7, 1, CAST(30.00 AS Decimal(10, 2)), 2, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (8, 1, CAST(30.00 AS Decimal(10, 2)), 4, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (9, 1, CAST(30.00 AS Decimal(10, 2)), 3, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (10, 2, CAST(35.00 AS Decimal(10, 2)), 5, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (11, 2, CAST(35.00 AS Decimal(10, 2)), 5, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (12, 2, CAST(35.00 AS Decimal(10, 2)), 5, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (13, 2, CAST(35.00 AS Decimal(10, 2)), 5, NULL)
GO
