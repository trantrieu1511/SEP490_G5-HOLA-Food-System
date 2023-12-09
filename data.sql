USE [SEP490_HFS_2]
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [numberOfViolations], [refreshToken], [refreshTokenExpiryTime], [createDate], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'CU00000001', N'Duoc', N'Nguyen', N'male', CAST(N'2001-09-24' AS Date), N'haivlace@gmail.com', N'0966390661', 0x56D89B00477C6DD47CD34F1D829BAB37470B2A83F5CF185336F5356D450E6B66E19AA002B6B99C491EBF21B96419299EECEBA2CEEAC3BB2B21D6A86C28ECA100, 0xCFDF36C421B32201A223008D0227AD3B61FE75AF0BC4112DEED8F034E39479B6, 0, 825000.0000, 1, 0, N'0VVEUcyR1y2NY+wnr4Fr7q3sK0S+AeE7Y/oY4Tvem96I2+YzFtEyAwMHPsKLZmvYK6fxmRRBB+Uk6mPlG+Lejg==', CAST(N'2023-12-14T22:22:35.607' AS DateTime), CAST(N'2023-12-04T12:00:16.907' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [isVerified], [refreshToken], [refreshTokenExpiryTime], [createDate], [lat], [lng]) VALUES (N'SE00000001', N'Cơm ngon 1', N'Tân xã-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'shop1@gmail.com', N'0966390661', 0xB6179D8E0AD131E49D611801DB8ADDC2E570867B400BFB7C54091EF7264D0FB76C8AB70C91A43AF44F1ADCD6834D9F899C7C0ACCFD7856B7F1E2765765B55B46, 0x37819965DB07F4D0C1750D4D92C722CF5C6ADF9A4E3802747F5965F840C4A372, 0, 975000.0000, 1, 0, 1, N'NLAossalDxBx15WQ2ihXV/HS+gIl3UDOgousNjDq5cwEaCt0KUFp5mCney8o5SIIq0BEbofp66G0VVqu67MDQA==', CAST(N'2023-12-15T12:24:11.390' AS DateTime), CAST(N'2023-12-04T09:07:35.313' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [isVerified], [refreshToken], [refreshTokenExpiryTime], [createDate], [lat], [lng]) VALUES (N'SE00000002', N'Cơm ngon 2', N'Tân xã-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'shop2@gmail.com', N'0966390661', 0xABB908AB1399A5BC2B3304262F6C6E262789FB1411C143E05B7FF67E9D3C11B71CE8F425A1045CC7340F3D79A3119B463A68C2BA572FA1A8C78BEB2A12666D6F, 0x054AFF00276C40987EE342088A3AFA6718F4C77548C7D2374FF63ABA0014757C, 0, 175000.0000, 1, 0, 1, N'SeVXcjCYh76KUC2LLZNdMmCSHI2dZiv29gTz5Ce8BIm/UaqyyJkQe3/PSefdxQaG/1M5dbgVcDAaNOKG7T6+KA==', CAST(N'2023-12-12T14:13:10.223' AS DateTime), CAST(N'2023-12-05T14:12:49.373' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1, N'Food', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (2, N'Drink', 1)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1, N'SE00000001', N'Cơm 1', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-04T12:07:27.730' AS DateTime), N'Cơm ngon', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2, N'SE00000001', N'Cơm 2', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-04T12:07:47.867' AS DateTime), N'Cơm 2', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (3, N'SE00000001', N'Cơm 3', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-04T12:08:09.133' AS DateTime), N'Cơm 3', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (4, N'SE00000002', N'Cơm ngon 1', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-05T14:14:25.457' AS DateTime), N'Cơm ngon lắm', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (5, N'SE00000002', N'Cơm ngon 2', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-05T14:14:49.690' AS DateTime), N'Cơm ngon lắm ', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (6, N'SE00000002', N'Cơm ngon 3', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-05T14:15:05.553' AS DateTime), N'Cơm ngon lắm', 1, 1, 0, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
INSERT [dbo].[Shipper] ([shipperId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [manageBy], [confirmedEmail], [isVerified], [refreshToken], [refreshTokenExpiryTime], [createDate]) VALUES (N'SH00000001', N'Duoc', N'Nguyen', N'male', CAST(N'2001-09-24' AS Date), N'shipper@gmail.com', N'0966390661', 0xE4A259616ADDE7821C94C3C0F66B71B47F4DDDB91C9AF1A21C10A069D870075CDD3BC03B559A31908B1568043F2756C3429154714829315C702DDD78A057240B, 0x1FFB2855115D7DEE0EBC902611316CADA6E13BC67EA818CF5FEE875933593E9E, 0, N'SE00000001', 1, 1, N'LWVO4TubcbahBG+D1j5Lum1va/S/lrpnGetFR10NEHmEbLNJNIB/1WW5oSy8Ez4zojDMttK16qdomezrmQObGQ==', CAST(N'2023-12-12T22:15:01.023' AS DateTime), CAST(N'2023-12-04T12:15:51.727' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [customerPhone], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (1, N'CU00000001', N'SE00000001', N'0966390661', CAST(N'2023-12-04T12:10:24.240' AS DateTime), NULL, NULL, N'DH FPT, Phường Hàng Buồm, Quận Hoàn Kiếm, Thành phố Hà Nội', N'SH00000001', NULL, 0, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [customerPhone], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (2, N'CU00000001', N'SE00000001', N'0966390661', CAST(N'2023-12-05T14:37:07.577' AS DateTime), NULL, NULL, N'DH FPT, Xã Tân Xã, Huyện Thạch Thất, Thành phố Hà Nội', NULL, NULL, 0, 0)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [customerPhone], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (3, N'CU00000001', N'SE00000001', N'0966390661', CAST(N'2023-12-05T14:55:05.750' AS DateTime), NULL, NULL, N'DH FPT, Xã Cam Thượng, Huyện Ba Vì, Thành phố Hà Nội', NULL, NULL, 0, 1)
GO
INSERT [dbo].[Order] ([orderId], [customerId], [sellerId], [customerPhone], [orderDate], [requiredDate], [shippedDate], [shipAddress], [shipperId], [voucherId], [status], [paymentMethod]) VALUES (4, N'CU00000001', N'SE00000002', N'0966390661', CAST(N'2023-12-05T14:55:05.813' AS DateTime), NULL, NULL, N'DH FPT, Xã Cam Thượng, Huyện Ba Vì, Thành phố Hà Nội', NULL, NULL, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProgress] ON 
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (1, N'Order success! Wait seller.', CAST(N'2023-12-04T12:10:24.333' AS DateTime), NULL, 1, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (2, NULL, CAST(N'2023-12-04T12:18:08.277' AS DateTime), NULL, 1, 1, N'SE00000001', NULL, NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (3, NULL, CAST(N'2023-12-04T12:18:16.263' AS DateTime), NULL, 1, 2, N'SE00000001', NULL, NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (4, N'Order success! Wait seller.', CAST(N'2023-12-05T14:37:07.657' AS DateTime), NULL, 2, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (5, N'Order success! Wait seller.', CAST(N'2023-12-05T14:55:05.780' AS DateTime), NULL, 3, 0, NULL, N'CU00000001', NULL)
GO
INSERT [dbo].[OrderProgress] ([orderProgressId], [note], [createDate], [image], [orderId], [status], [sellerId], [customerId], [shipperId]) VALUES (6, N'Order success! Wait seller.', CAST(N'2023-12-05T14:55:05.837' AS DateTime), NULL, 4, 0, NULL, N'CU00000001', NULL)
GO
SET IDENTITY_INSERT [dbo].[OrderProgress] OFF
GO
INSERT [dbo].[Admin] ([adminId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [refreshToken], [refreshTokenExpiryTime], [createDate]) VALUES (N'AD00000001', N'string', N'string', N'string', CAST(N'2001-12-04' AS Date), N'admin@gmail.com', NULL, 0xD41BCBBE4DE17A082B66290D78CA9068668ECB0932B3D26B838493B4EE68FC3C8708865A5B592749A8B798CD7C1E47A2A531D1F5CECDFD3B1FE6CE475414F997, 0xD59278511D58930A194E99BA117D122709D38031F66732FD9BC16E5E04674877, 0, NULL, 1, N'5DtnjactPAsdHjgFNJFDnzxGIXRQJpGHktcZ+Q2Y31K6C4Yjwre0VLiXmAUazZotIFnLd3pWSupSNs5+8gAOwg==', CAST(N'2023-12-11T09:06:01.750' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[SellerLicenseImage] ON 
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (1, N'SE00000001', N'photo-1546069901-ba9599a7e63c_20231204_090735.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2, N'SE00000001', N'360_F_403953193_rj9PZdIJMRSKxZdIhAv6g52FtOZnOHm5_20231204_090735.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3, N'SE00000002', N'photo-1546069901-ba9599a7e63c_20231205_141249.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (4, N'SE00000002', N'360_F_403953193_rj9PZdIJMRSKxZdIhAv6g52FtOZnOHm5_20231205_141249.jpg', 0)
GO
SET IDENTITY_INSERT [dbo].[SellerLicenseImage] OFF
GO
INSERT [dbo].[Accountant] ([accountantId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [confirmedEmail], [isBanned], [refreshToken], [refreshTokenExpiryTime], [createDate]) VALUES (N'AC00000001', N'Duoc', N'Nguyen', N'male', CAST(N'2001-09-24' AS Date), N'duocng.2409@gmail.com', N'0966390661', 0xB458741ACEE0CB74A538FD1179A74E66E45206B14EED39D2B6AEBAD8B2D8D6F95908B2C21B6D489DB9049602FB65CAC1FBD1CA2DCA2152C0725380D008C0B2DF, 0x1D11963DC185AFCE79F2D8E4704CA5787F90BC262A0091EF7E3601B39E06B377, 0, 1, 0, N'j2/2eX5P4KePjz/gFZ8trDZT1qJl/KVRXQte+vlqfRKgoqRIyTrlKaHbab7pxh0hm9XSB8fUN8IPdO8Adbxeug==', CAST(N'2023-12-15T12:18:17.743' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[TransactionHistory] ON 
GO
INSERT [dbo].[TransactionHistory] ([transactionId], [senderId], [recieverId], [TransactionType], [Note], [Value], [CreateDate], [UpdateDate], [ExpiredDate], [status], [AcceptBy]) VALUES (1, N'SE00000001', NULL, N'Withdraw', N'dsafdsfadsf', CAST(100000 AS Decimal(18, 0)), CAST(N'2023-12-04T09:09:12.870' AS DateTime), NULL, CAST(N'2023-12-11T09:09:12.870' AS DateTime), 0, NULL)
GO
INSERT [dbo].[TransactionHistory] ([transactionId], [senderId], [recieverId], [TransactionType], [Note], [Value], [CreateDate], [UpdateDate], [ExpiredDate], [status], [AcceptBy]) VALUES (2, N'SE00000001', NULL, N'Withdraw', N'dsafdsfadsf
- Accept by AC00000001', CAST(100000 AS Decimal(18, 0)), CAST(N'2023-12-04T09:10:32.577' AS DateTime), CAST(N'2023-12-04T09:11:08.997' AS DateTime), CAST(N'2023-12-11T09:10:32.577' AS DateTime), 1, N'AC00000001')
GO
INSERT [dbo].[TransactionHistory] ([transactionId], [senderId], [recieverId], [TransactionType], [Note], [Value], [CreateDate], [UpdateDate], [ExpiredDate], [status], [AcceptBy]) VALUES (3, N'CU00000001', N'SE00000001', N'OrderPaid', N'Order food', CAST(105000 AS Decimal(18, 0)), CAST(N'2023-12-05T14:55:05.653' AS DateTime), NULL, NULL, 1, NULL)
GO
INSERT [dbo].[TransactionHistory] ([transactionId], [senderId], [recieverId], [TransactionType], [Note], [Value], [CreateDate], [UpdateDate], [ExpiredDate], [status], [AcceptBy]) VALUES (4, N'CU00000001', N'SE00000002', N'OrderPaid', N'Order food', CAST(70000 AS Decimal(18, 0)), CAST(N'2023-12-05T14:55:05.737' AS DateTime), NULL, NULL, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[TransactionHistory] OFF
GO
INSERT [dbo].[CartItem] ([foodId], [cartId], [amount]) VALUES (1, N'CU00000001', 2)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (1, 1, CAST(35000.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (1, 2, CAST(35000.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (2, 1, CAST(35000.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (2, 2, CAST(35000.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (3, 2, CAST(35000.00 AS Decimal(10, 2)), 3, NULL)
GO
INSERT [dbo].[OrderDetail] ([orderId], [foodId], [unitPrice], [quantity], [status]) VALUES (4, 6, CAST(35000.00 AS Decimal(10, 2)), 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[FoodImage] ON 
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1, 1, N'photo-1546069901-ba9599a7e63c_20231204_120727.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2, 2, N'photo-1546069901-ba9599a7e63c_20231204_120747.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3, 3, N'photo-1546069901-ba9599a7e63c_20231204_120809.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (4, 4, N'photo-1546069901-ba9599a7e63c_20231205_141425.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (5, 5, N'photo-1546069901-ba9599a7e63c_20231205_141449.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (6, 6, N'photo-1546069901-ba9599a7e63c_20231205_141505.jpg')
GO
SET IDENTITY_INSERT [dbo].[FoodImage] OFF
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (1, 0, N'System', N'SE00000001', 0, N'Đơn hàng mới', N'Đơn hàng ID: 1, có một yêu cầu đặt đơn hàng mới', CAST(N'2023-12-04T12:10:24.363' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (1, 1, N'System', N'SE00000001', 0, N'New Order', N'Order ID: 1, there is a new order request', CAST(N'2023-12-04T12:10:24.363' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (2, 0, N'System', N'CU00000001', 0, N'Cập nhật đơn hàng', N'Đơn hàng ID: 1 đã được chấp nhận', CAST(N'2023-12-04T12:18:08.287' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (2, 1, N'System', N'CU00000001', 0, N'Update order', N'Order ID: 1 has been accepted', CAST(N'2023-12-04T12:18:08.287' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (3, 0, N'System', N'SH00000001', 0, N'Đơn hàng mới', N'Đơn hàng ID: 1 được chuyển từ cửa hàng: Cơm ngon 1 để giao hàng', CAST(N'2023-12-04T12:18:16.273' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (3, 1, N'System', N'SH00000001', 0, N'New Order', N'Order ID: 1 transferred from shop: Cơm ngon 1 for delivery', CAST(N'2023-12-04T12:18:16.273' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (4, 0, N'System', N'SE00000001', 0, N'Đơn hàng mới', N'Đơn hàng ID: 2, có một yêu cầu đặt đơn hàng mới', CAST(N'2023-12-05T14:37:07.697' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (4, 1, N'System', N'SE00000001', 0, N'New Order', N'Order ID: 2, there is a new order request', CAST(N'2023-12-05T14:37:07.697' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (5, 0, N'System', N'SE00000001', 0, N'Đơn hàng mới', N'Đơn hàng ID: 3, có một yêu cầu đặt đơn hàng mới', CAST(N'2023-12-05T14:55:05.790' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (5, 1, N'System', N'SE00000001', 0, N'New Order', N'Order ID: 3, there is a new order request', CAST(N'2023-12-05T14:55:05.790' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (6, 0, N'System', N'SE00000002', 0, N'Đơn hàng mới', N'Đơn hàng ID: 4, có một yêu cầu đặt đơn hàng mới', CAST(N'2023-12-05T14:55:05.843' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (6, 1, N'System', N'SE00000002', 0, N'New Order', N'Order ID: 4, there is a new order request', CAST(N'2023-12-05T14:55:05.843' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[ProfileImage] ON 
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1, N'SE00000001', N'photo-1546069901-ba9599a7e63c_20231205_135928.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (2, N'SE00000002', N'photo-1546069901-ba9599a7e63c_20231205_141324.jpg', 0)
GO
SET IDENTITY_INSERT [dbo].[ProfileImage] OFF
GO
SET IDENTITY_INSERT [dbo].[WalletTransferCode] ON 
GO
INSERT [dbo].[WalletTransferCode] ([codeId], [userId], [code], [isUsed], [expiredDate]) VALUES (1, N'SE00000001', N'7WEOB9', 1, CAST(N'2023-12-04T09:15:21.593' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[WalletTransferCode] OFF
GO
