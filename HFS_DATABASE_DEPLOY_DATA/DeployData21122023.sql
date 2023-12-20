USE [SEP490_HFS_2]
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000001', N'MeoMeo Food', N'Ngõ 265 Nguyễn Văn Cừ, Đồng Hới, Quảng Bình-Phường Đức Ninh Đông - Thành Phố Đồng Hới - Tỉnh Quảng Bình', N'phamanhqb73@gmail.com', N'0396596633', 0x39C3FF7BCDCCCBE7D3E210A9511E08D998EB8BF3F0C7A0F17098FEB8AF048C5792BA31FEE875E17B669057F2B4A1C07ABBD19C68D53582CB8CDE0BE881FE3D73, 0x8F6A130477A2C780D0D83ABFA712CC381C8D8070DCA2A225A74E43D03341434B, 0, NULL, 1, 0, 1, N'm2FoEXkatphJq1au95PmraWB36+dMbRIzcrkItCEf/7+7U7Q6Kdxnw0ALjttxUf5t8jopJ+3kulkv+WiBBvj4w==', CAST(N'2023-12-27T22:26:23.240' AS DateTime), CAST(N'2023-12-16T04:48:57.003' AS DateTime), N'3232342342432', N'ok nha', 1, NULL, NULL)
GO
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000003', N'Ăn ngon mỗi ngày', N'Ngã 4 thôn 2, Xã Tân Xã, huyện Thạch Thất, Hà Nội-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'project.teamfpt@gmail.com', N'0398768822', 0x76A2C712B4DE362196C8D1C6FDE289D8C67C350DAC76636F972443CFABF50239DD3F493F7D0BA9E54435BBB8F4ABFD87CA76ABA0804E27F5FF66E55256C721DB, 0xCF022C7C1EBC31DF19F0D23F1AF6048025B8767D566E2B5536EE31E8FB86E0DB, 0, NULL, 1, 0, 1, NULL, NULL, CAST(N'2023-12-19T21:59:50.513' AS DateTime), N'087523424882432', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000004', N'Bún Đậu 1966', N'16 Tân Xã-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'phucnvhe@gmail.com', N'0382119255', 0x21C3EAFA1D7ECC1BCEF3BC086E9FE59AEED68B1C8F979400F0C6120F32864C271C4EE789F83850AE97D62A2DCFEE19ED68AD04FE4EEF2B5B078F4FBA84B890D9, 0x2E05A7A07448991F4DF56BB9F63DB41338C7DA796A99881095976CD6A6D066E6, 0, NULL, 1, 0, 1, N'Ip89ae7oc3OTmC+5rFvkopXrEGGJzCJPLvdBA6bNW61LXkAaKR5uWdQIe0gtJ9HE6KdM5vH9O9Q6dnoxhqnX3w==', CAST(N'2023-12-27T21:01:54.030' AS DateTime), CAST(N'2023-12-19T22:54:44.637' AS DateTime), N'0807656', N'Đăng ký thành công', 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJPVFAiOiIyMjYwIiwiZXhwIjoxNzAzMDAxNTE1LCJpc3MiOiJOZ3V5ZW5Ucm9uZ0x1MSIsImF1ZCI6Ik5ndXllblRyb25nTHUifQ.8SWNkvTP0YLTZ_cXe306TsjSgdZeqYiHjO1TFozpHO8', 5)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000005', N'Cơm Tún', N'Tân Xã-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'haivlace@gmail.com', N'0966390661', 0x7738B45A1C630D94E0779BFE27FF80E09E6BE17C3912BFD41D2DE8ABEAE31A7BEFE0369CC8D0A30349DD882EA1D815F85808B910B2A2017753E0DCAF5761B848, 0xE880BF4C9D0F39AE16D96ADC19E5ACC84DCC4875A101FD85A93417308329E835, 0, NULL, 1, 0, 1, N'07ETUMFSB9Z5sGlMeFwK15CpV4RP7vMpb/NUjPJT3JUAg+/RimLR3IX4FSVt96vN/FG9NaJNmIrObcywh+WhUg==', CAST(N'2023-12-27T21:36:09.297' AS DateTime), CAST(N'2023-12-19T23:10:04.740' AS DateTime), N'123456789', N'Đăng ký thành công', 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000006', N'Mộc Hola quán', N'Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'fukyounyancat@gmail.com', N'0363290499', 0xDFE7867489186A78E58F1A2EF3036F572C7FDC2407D9A065F511218ECDC60E46C909203C3A31057E5421865BDCFA890EEFA0F67AED6CA2F28F4784C23E8F293D, 0x04342C72F2DA8B004EF53BB8770EF7A9C8C0C96F09660FE93E6C6A2B1E30B8B9, 0, NULL, 1, 0, 1, N'z8OrfjAjPR91SM7PA5DRQeWd6JGteCIKTYanQJ7nXEkfK/BwBVHOVd4nDTU4A9WhAqw41vs1lZMnPcfj3hkopg==', CAST(N'2023-12-28T00:51:10.680' AS DateTime), CAST(N'2023-12-20T17:51:54.273' AS DateTime), N'41G8041815', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000007', N'Nguyễn Huyền Drink', N'Thôn 3-Xã Thạch Hòa- Huyện Thạch Thất - Thành phố Hà Nội', N'trieutdhe163203@fpt.edu.vn', N'0338393656', 0x2F2B9C010BC7F9F7FF0406E36F7647BE5C0D3CFDCB5FB165900CC8855A618F90371E92DFC638B1895B50164A82A57D3D7CB3AD928BBB750CF5AD7B8772E3B937, 0x1C58CECB459BC34332A64EDE3821C41B2A030CC1754D22B6FA9B8E42F82B8D44, 0, NULL, 1, 0, 1, N'64bXkRGVYnlCsM8UZfz+meEA0pppx8mqkz0UmpYrqZrChxR7HMIhArHNKQInlpfUA5SQt+owYAjdElNfAU5ojA==', CAST(N'2023-12-28T00:52:29.193' AS DateTime), CAST(N'2023-12-20T20:35:06.967' AS DateTime), N'41M8041297', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000008', N'Cơm Đặng Nụ', N'Xã Thạch Hoà - Huyện Thạch Thất - Thành phố Hà Nội', N'swatclgt001@gmail.com', N'0343885666', 0x9A1E3E36AA37085D7F3CB3F5E187574B1691D3632853D9AAC427DB6E468DDDC6A279B776B4FD7E7DDE0CB0DEED06F91C3956EF1D3835CCF16F43D3137DCD26F1, 0x3C89DE45A70A733BB78DF24C12385485CE91DF8BC0A9674AE38DB227328FBBD3, 0, NULL, 1, 0, 1, N'jQQBnGU3GqiSu0opVvTjO5Op/02wDmqGg2bXS6qw9FQSQQ7jIAIzoI36xLrAI/sD9rODYWB30VOtOE09nwBquQ==', CAST(N'2023-12-28T00:50:29.870' AS DateTime), CAST(N'2023-12-20T20:39:04.977' AS DateTime), N'01P8001241', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Seller] ([sellerId], [shopName], [shopAddress], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [isBanned], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [businessCode], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'SE00000009', N'MIXUE TÂN XÃ', N'Đầu, ĐT419, Mục Quyên-Xã Tân Xã - Huyện Thạch Thất - Thành phố Hà Nội', N'duocnqhe150632@fpt.edu.vn', N'0966390662', 0x049C429DF23B162263DC4EA3E9B232B88519DA7BEE17929B9B6219E796EEFFFC56ACF7A5DE8241D5C8A3BAB624AE732B7BCDB1DB983910B5D645BC82310FAC51, 0xD8F7A30EC5AD7ECF5414EC24209A19B539EB9740699E87BA1DF540F340B4DC6A, 0, NULL, 1, 0, 1, N'MvSS5Ea9djXQ5rWWvn4dGO59Q5LXUM18icAMoxVNzcZ+ZboqoG7Sqa23c2OLZmwi7kPEaFSEAzxJfJaoKxKVLQ==', CAST(N'2023-12-27T22:08:37.870' AS DateTime), CAST(N'2023-12-20T20:59:35.583' AS DateTime), N'12312312321', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[PostModerator] ([modId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [confirmedEmail], [isBanned], [refreshToken], [refreshTokenExpiryTime], [banLimit], [reportApprovalLimit], [createDate]) VALUES (N'PM00000001', N'Minh', N'Trần', N'male', CAST(N'2000-02-20' AS Date), N'postmod1@gmail.com', N'0987654321', 0xB3E147AB335B426AE2505B8D939E902FAE49FDD11228B5B7051267C387D86471C7C13CDEBC72E59EDA317206F2A8333E0A85BCBCEE1C689A8A9C36DB870CF7E5, 0x46F068B064F349FFAC0D353A778B5375A8AE80EBCE5287E77ED5CD5999C0A7E5, 0, 1, 0, N'fvKLXaipL+PKxZOk9zhqY5Jgjd1Fej9HwJ7fBX2DifqX1FXkxfS4qXqfYrxP+R1iqnt7M9DK0Pj6zYhunEyv1w==', CAST(N'2023-12-27T23:56:37.767' AS DateTime), 24, 25, NULL)
GO
SET IDENTITY_INSERT [dbo].[Post] ON 
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (1, N'SE00000001', N'Dự báo thời tiết hôm nay 10.12 cụ thể cho các vùng:

Hà Nội

Nhiệt độ thấp nhất: 15 - 17 độ C.

Nhiệt độ cao nhất: 20 - 23 độ C.

Nhiều mây, có mưa vài nơi, riêng gần sáng và sáng mai có mưa, mưa rào rải rác. Gió chuyển hướng đông bắc mạnh dần lên cấp 3. Từ ngày mai trời chuyển rét.

Phía Tây Bắc Bộ

Nhiệt độ thấp nhất: 13-16 độ C.

Nhiệt độ cao nhất: 22-25 độ C, có nơi trên 25 độ C.

Nhiều mây, đêm có mưa vài nơi, ngày có mưa, mưa rào rải rác và có nơi có dông. Gió chuyển hướng đông bắc mạnh dần lên cấp 3. Ngày mai trời chuyển rét.

Hola food đang ship
🧋🧋🧋 Trà sữa nướng - trà hoa quả full menu - Cơm - Bún trộn - Mỳ cay - Nem nướng 🛵
🔥 Bún trộn nem lụi #35k
🔥 Bún trộn nem nướng #35k
🔥 Cơm chả lá lốt #35k
🔥 Cơm gà viên chiên  #35k
Gà viên chiên  #30k/suất
Đùi gà chiên giòn #28k
Mỳ cay bò #40k 
Mỳ cay hải sản #50k
Mỳ cay Thập cẩm #50k
Nem nướng nha trang #35k
Chân gà sốt thái #50k/đĩa
Bánh gà #8k
Kim bắp chiên #20k
Xúc xích chiên #10k
Cá viên #5k
Nem chua rán #50k/đĩa
Khoai lắc phô mai #25k/đĩa
Phô mai que #10k', CAST(N'2023-12-16T05:16:55.240' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (2, N'SE00000001', N'HoLa Food đang ship
🧋🧋🧋 Trà sữa nướng - trà hoa quả full menu - Cơm - Bún trộn - Mỳ cay - Nem nướng 🛵
🔥 Bún trộn nem lụi #35k
🔥 Bún trộn nem nướng #35k
🔥 Cơm chả lá lốt #35k
🔥 Cơm gà viên chiên  #35k
Gà viên chiên  #30k/suất
Đùi gà chiên giòn #28k
Mỳ cay bò #40k 
Mỳ cay hải sản #50k
Mỳ cay Thập cẩm #50k
Nem nướng nha trang #35k
Chân gà sốt thái #50k/đĩa
Bánh gà #8k
Kim bắp chiên #20k
Xúc xích chiên #10k
Cá viên #5k
Nem chua rán #50k/đĩa
Khoai lắc phô mai #25k/đĩa
Phô mai que #10k
🌼🌼🌼Đồ uống ngon👇👇👇
✅ Trà sữa nướng 
✅ Ca cao nóng
✅ Trà mãng cầu
✅ Trà ô long nhài
✅ Trà đào cam sả
✅ Trà nho việt quất
✅ Trà bưởi hồng dâu tây
✅ Trà dưa lưới nhiệt đới
✅ Trà dứa hồng hạc
✅ Trà kiwi bách hương
✅ Sữa tươi tc đường đen
✅ Sữa chua lắc dâu tây/xoài/việt quất
Freeship <2km - Cổng trường - Dom
Phụ phí 5k >2km', CAST(N'2023-12-19T21:21:19.900' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (3, N'SE00000001', N'Sườn xào mãi cũng chán, đem hấp với loại củ hay để ăn lẩu này được món "thôi miên" vị giác
300g sườn ngon, 100g khoai môn, 3 tép tỏi, 1 quả ớt, hành lá vừa đủ, 1 mẩu gừng, 1 thìa tương đậu bản (mua online - không có bỏ qua), 1 thìa rượu nấu ăn, 1 thìa đường trắng, muối vừa ăn, 2 thìa tinh bột ngô, 1 thìa dầu ăn.
Khoai môn gọt vỏ sau đó cắt khoai môn thành từng miếng cỡ ngón tay. Gừng băm nhỏ, tỏi bóc vỏ băm nhỏ, ớt cắt khoanh. 
Sườn heo rửa sạch, cho ra bát. Thêm gừng băm nhuyễn, 1 thìa rượu nấu ăn, 1 thìa đường, lượng muối thích hợp, 2 thìa tinh bột ngô vào trộn đều rồi cho 1 thìa dầu ăn. Thêm tỏi băm, gừng băm và tương đậu bản, ướp trong 20 phút.', CAST(N'2023-12-19T22:04:31.053' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (4, N'SE00000009', N'Update thêm 2 siêu phẩm cực kỳ bánh cuốn ạ : 
🍓Sữa thạch dâu tây 
🥝Sữa thạch Kiwi kiwi 
Thanh thanh, đậm vị sữa mà lại còn có thạch pudding thơm ngon cực các ông ạ 🥰
MIXUE Thạch Hoà với 2 cơ sở : 
👉 Cơ sở 1 : 097 4657376
MIXUE HOLA- đối diện cây xăng 39- Thôn 3- Thạch Hoà- Thạch Thất - HN 
Link maps:      Cách ĐH FPT 1 Km
https://maps.app.goo.gl/MiipijfVH9t3wu6w9?g_st=ic
👉Cơ sở 2: 033 4209879 
Đối diện cơm Thu Thuỷ- cách cổng phụ ĐH Quốc Gia 200m- thôn 3- Thạch Hoà- Thạch Thất- HN 
Link maps:  
https://maps.app.goo.gl/Jj4EkhCJgYQJnt1e9?g_st=ic
Link page cơ sở 2:  Mixue ĐH Quốc Gia- Hoà Lạc
https://www.facebook.com/profile.php?id=100091773753385...
❌ Lưu ý: 
- Các cậu ưu tiên order cửa hàng gần nhất để bên mình ship nhanh và đảm bảo chất lượng của sản phẩm nha 
- Chỉ đặt đơn ở 1 cơ sở để tránh bị trùng đơn ạ 
- Freeship bán kính 2km, nhận đơn từ 2 cốc ạ', CAST(N'2023-12-20T21:11:59.803' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (5, N'SE00000009', N'Update thêm 2 siêu phẩm cực kỳ bánh cuốn ạ : 
🍓Sữa thạch dâu tây 
🥝Sữa thạch Kiwi kiwi 
Thanh thanh, đậm vị sữa mà lại còn có thạch pudding thơm ngon cực các ông ạ 🥰
MIXUE Thạch Hoà với 2 cơ sở : 
👉 Cơ sở 1 : 097 4657376
MIXUE HOLA- đối diện cây xăng 39- Thôn 3- Thạch Hoà- Thạch Thất - HN 
Link maps:      Cách ĐH FPT 1 Km
https://maps.app.goo.gl/MiipijfVH9t3wu6w9?g_st=ic
👉Cơ sở 2: 033 4209879 
Đối diện cơm Thu Thuỷ- cách cổng phụ ĐH Quốc Gia 200m- thôn 3- Thạch Hoà- Thạch Thất- HN 
Link maps:  
https://maps.app.goo.gl/Jj4EkhCJgYQJnt1e9?g_st=ic
Link page cơ sở 2:  Mixue ĐH Quốc Gia- Hoà Lạc
https://www.facebook.com/profile.php?id=100091773753385...
❌ Lưu ý: 
- Các cậu ưu tiên order cửa hàng gần nhất để bên mình ship nhanh và đảm bảo chất lượng của sản phẩm nha 
- Chỉ đặt đơn ở 1 cơ sở để tránh bị trùng đơn ạ 
- Freeship bán kính 2km, nhận đơn từ 2 cốc ạ', CAST(N'2023-12-20T21:14:11.223' AS DateTime), 2, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (6, N'SE00000009', N'Cần bán bộ máy tính này giá 5m
liên hệ 0966390661', CAST(N'2023-12-20T21:29:38.950' AS DateTime), 3, N'PM00000001', CAST(N'2023-12-21T00:04:47.187' AS DateTime), N'Bài đăng không liên quan đến thực phẩm')
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (7, N'SE00000006', N'GIỜ ĂN TỐI ĐÊM ĐẾN RỒI OD THÔI CÁC E ƠI.  #Ship_xuyên_màn_đêm_nha😍
ALO 0327146083 CHỊ SHIP ĐỒ ĂN NÓNG HỔI. XÔI NGON, MỲ, COMBO ĂN VẶT, CHÈ NƯỚC  THEO MENU BÊN DƯỚI NHA CÁC E. Ship all [#DOM_TRỌ_phenika_Tân_xã_ĐH_VIỆT_NHẬT] ship đến 5h sáng.
XÔI - MỲ - BÁNH MỲ
- XÔI NGON 35k ( lạp sườn, trứng thịt kho tàu đặc biệt) 
- XÔI NGON 35k ( xx, lạp sườn, ruốc, trứng ngải)
- XÔI KHÚC NẾP NƯƠNG ĐẶC BIỆT 35k -40k
- MỲ TRỘN TƯƠNG ĐEN ĐẶC BIỆT 35k-40k-50k( cá viên, xx, trứng ốp) or ( kim chi, xx, trứng ốp)
- MỲ INDOMI FULL topping 35k THÊM TRỨNG ỐP 5k
- MỲ INDOMI SUẤT ĐẶC BIỆT 50K
- MỲ TRỘN KIM CHI, TRỨNG ỐP, XX, CÁ VIÊN  35k
📌ĐỒ ĂN VẶT NGON MIỆNG👌
Combo viên chiên 45k
Combo viên chiên 55k
Combo viên chiên 65k
Combo viên chiên 75k
Combo 100 hoặc hơn cũng bán khách ăncombo viên chiên từ 40 -100k ăn bao nhiêu cũng bán😂😂😂  combo như hình ảnh.
👉 Combo bánh mỳ nướng bơ mật ong 30k ( xx, nem chua, viên rau củ, cá viên, khoai tây) khách đặt chỉ cần ghi ( cb bánh mỳ hoặc bánh bao 30k)
👉 Combo bánh bao, xx, nem chua, viên rau củ, cá viên, khoai tây 30k ( khách k thích ăn khoai tây thì báo để chị thay bằng mực xoắn, con tôm nhé)
👉 Đùi gà chiên cay  35k (new)
Gà cuộn rong biển hãng CP 6k/ cái. Combo 35k 6 cái (k bán loại rẻ)', CAST(N'2023-12-20T21:33:47.320' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (1004, N'SE00000008', N'✈ ✈ . Bếp Anh Béo cách trường FPT chỉ 1.5KM.. 🏍 
✈ ship dom .ship sân tập, ship bãi tập xe, ĐHQG. Tân xã, Phenika… ship muôn nơi 😜 😜 
Nhà mình có nhận đặt cơm theo giờ để các bạn chủ động giờ giấc ăn uống đảm bảo SK nha.. :3 :3 
Ship nhà mình đang chạy rồi nha các bạn ơiiiiii
 khách chỉ việc nhấc máy alo còn lại cứ để e lo ship tận cửa nhoaaaaa  
Hôm nay nhà mình có: 
👉  Xôi Sườn Cayyy chỉ 35k ;g ;g
🍚 🍚   Cơm trắng đồng giá 35k
🐤 🐤  Gà các món : Đùi gà: KFC, chiên xù, Mật ong, chua ngọt, rang muối, chiêm mắm, sốt cay
🐤 🐤  Ức gà: KFC, chiên xù, Mật ong, chua ngọt, rang muối, chiêm mắm, sốt cay
👉  Sườn: Chua ngọt, chiên mắm
👉  Thịt nạc vai chiên lá móc mật
👉  Ba chỉ rang cháy cạnh- Heo chao riềng
👉  Heo cháy tỏi
👉  Cơm rang thập cẩm 35k
👉  Mỳ xào Bò, Mỳ xào thập cẩm: 30K
👉  Mỳ trộn Hàn quốc, mỳ trộn Idome thập cẩm: 35k
/-li  Đặc Biệt:  Cơm trộn Hàn Quốc chỉ 40k
✌  Combo 01 cánh + khoai tây chiên kèm tương ớt chỉ 49k
/-v combo 01 cánh+01 đùi+khoai tây kèm tương ớt chỉ 73k 
/-v com bo 2 cánh + khoai tây kèm tương ớt 69k
✌  Com bo 02 cánh + 02 đùi + khoai tây chiên kèm tương ớt chỉ 117k nhoa
Rất nhiều loại nước uống thơm ngon nha mn. Nước ép dưa hấu, nước chanh tươi, trà tắc, nước cam tươi 🍎 🍋 🍇 🍉 
Note: tất cả các đơn đều phụ phí ship giúp mình tuỳ khu vực xa, gần.
   ☎ ☎ 0343.885.666 – 0984.648.24', CAST(N'2023-12-20T22:07:42.483' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (1005, N'SE00000007', N'☎️ Hotline/ Zalo: 0338393656
STK: VPBANK 0338393656
‼️GỌI LÀ SHIP 24/24‼️ LƯU SĐT QUÁN NHÉ ‼️
KÍNH MỜI QUÝ KHÁCH
💯
Mỳ cay xúc xích - bò xx - hải sản - thập cẩm
💯 🧋🧋 Trà sữa Trân Châu
✅ trà sữa truyền thống
🥤trà sữa vị
✅ Dâu
✅ sầu riêng
✅việt quất
✅Khoai môn
✅ Hoa anh đào
✅  Hoa đậu biếc
✅  Bạc Hà
✅  Socola
✅  Matcha
💯
Cafe đen/ nâu/ bạc xỉu/ muối
Trà dâu
Trà Việt Quất
Trà Kiwi
Trà vải nha đam
Trà Chanh nha đam
⛔️
Bò húc: 15k
Coca: 10k
Sting vàng/đỏ: 12k
Ship 5k - 10k - 15k tùy xa, gần
-------------------------------
Thôn 3 - Thạch Hoà - Thạch Thất - Hà Nội
STK: VPBANK 0338393656
☎️ Hotline: 0338393656', CAST(N'2023-12-20T22:37:44.440' AS DateTime), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Post] ([postId], [sellerId], [postContent], [createdDate], [status], [banBy], [banDate], [banNote]) VALUES (1006, N'SE00000006', N'TUYỂN SHIPPER!!!
Lương: 25k/giờ hoặc 3.5k/đơn
Mô tả: phụ các việc đơn giản và chủ yếu là đi ship quanh khu vực Thạch Hoà 
Ưu tiên: thuộc đường các trọ, đã có kinh nghiệm
Thời gian : 15h - 19h chiều và 19h - 23h 
Liên hệ trực tiếp: 0342173747 - Kim Anh.', CAST(N'2023-12-20T23:44:26.423' AS DateTime), 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [numberOfViolations], [refreshToken], [refreshTokenExpiryTime], [createDate], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'CU00000002', N'lu', N'nguyen', N'Null', NULL, N'lunguyen2k22@gail.com', NULL, 0x2456D2FC868E485FD2DFC2286D697DAE60D57C2A038F46F16A149B9C41A203908EFAF9DAFD1A3AEBFE534FE2776E12EA793A3E18B69892D54F0C90C7D6AD2056, 0xAEAA68FC1CBEAD61E0E25BDAF020744F81F2CE065AC0255A411CB5A43B3C7A5A, 0, NULL, 1, 0, N'zmI7z4yN8xQIe8NB4KIP+UfryqGSy3MbjSejWIEWg3NrA381c5KNjddXYHHoMDs0qUZ+lOnkjkPzeyyTgvPMSw==', CAST(N'2023-12-27T15:49:26.373' AS DateTime), CAST(N'2023-12-18T13:36:33.147' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [numberOfViolations], [refreshToken], [refreshTokenExpiryTime], [createDate], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'CU00000003', N'Pham', N'Ngoc', N'Null', NULL, N'anhpnhe153727@fpt.edu.vn', N'0396543212', 0x77C9A9C27885769084FE5CC1CA9971F7071E5ADAE4407EE0B626FC1F3AE310A041E9D5B7BA8199BA63F373EDDE373C840B3A3C9B823932417001E1111BB20E0A, 0x68E7F037CC356B66702183E92A1AB07FE1143CC0EBFE43E591CC6BA690F57AF8, 0, NULL, 1, 0, N'bJBACPUkcyyLkCKnK/aTNxpZQgU9+ZnaJqBjOgZQN8iM7JFtAhnjytyIRzvGqW7k9ivJ9XhsAskdV+dgcCA9Gw==', CAST(N'2023-12-26T04:50:28.237' AS DateTime), CAST(N'2023-12-18T14:40:52.807' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [numberOfViolations], [refreshToken], [refreshTokenExpiryTime], [createDate], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'CU00000004', N'Triệu', N'Trần', N'male', CAST(N'2002-11-15' AS Date), N'trantrieu15112k2@gmail.com', N'0868342491', 0x467DDB9C7737E00D37482B0365133093E9F69E2CDB0FE4347FEC7594373ABE026ECFFC22EF77D694CC5B3300CF19820CF8447ABA99D72A6F57ED2CE1EF33E14F, 0xA7AB9F8EB6E2B8158752BFA29E1C0D986B12E13B62BF0BE89A3BEB5925E8479C, 0, NULL, 1, 0, N'LO9qJkA5G6pcsr8O39uVB2OcgjQnqzyfVPzPe5BsN0dz1WnCh/OY2BGHan8yQ+4nM2nNV0ZcFhZ9grdEzjst9w==', CAST(N'2023-12-27T23:45:33.230' AS DateTime), CAST(N'2023-12-19T02:59:43.967' AS DateTime), 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJPVFAiOiIxNDY5IiwiZXhwIjoxNzAzMDg1NjMxLCJpc3MiOiJOZ3V5ZW5Ucm9uZ0x1MSIsImF1ZCI6Ik5ndXllblRyb25nTHUifQ.OKyV0yadFcZcomv8E1NxUCpJIUCRFailAfWARL9YiTw', 5)
GO
INSERT [dbo].[Customer] ([customerId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [numberOfViolations], [refreshToken], [refreshTokenExpiryTime], [createDate], [isPhoneVerified], [otpToken], [otpTokenExpiryTime]) VALUES (N'CU00000005', N'lu', N'nguyen', N'Null', NULL, N'lunguyen2k18@gmail.com', NULL, 0x6EA37C944ADC961D84118238D7EBAB3BF24AB9D58481F6A2E271CEAAEAB4A451E54B2F71259A9F1E7BEAF48C610D9A681A632E5B39C3199295AD3B605ACB487F, 0xF12E4BB03757CB94EE08BAD29427C932DAAE422BA023A3B7874E24786E08A1A0, 0, NULL, 1, 0, N'4uXyfKNqzZbahpb48FHgnrp0ppayNFA4b2TouzaqEmw6cx7ZuACc61ti66fBABoo280kldHWAnU6AHEDbE3AWw==', CAST(N'2023-12-27T23:45:31.750' AS DateTime), CAST(N'2023-12-20T16:11:34.660' AS DateTime), 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PostVote] ON 
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (1, 2, 1, CAST(N'2023-12-20T15:50:32.047' AS DateTime), N'CU00000002')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (2, 1004, 1, CAST(N'2023-12-20T22:23:23.517' AS DateTime), N'CU00000004')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (3, 1006, 1, CAST(N'2023-12-20T23:58:36.380' AS DateTime), N'CU00000004')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (4, 1006, 1, CAST(N'2023-12-20T23:58:59.590' AS DateTime), N'CU00000005')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (5, 1005, 1, CAST(N'2023-12-20T23:59:02.000' AS DateTime), N'CU00000005')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (6, 1004, 1, CAST(N'2023-12-20T23:59:06.550' AS DateTime), N'CU00000005')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (7, 7, 1, CAST(N'2023-12-20T23:59:08.090' AS DateTime), N'CU00000005')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (8, 4, 1, CAST(N'2023-12-20T23:59:09.953' AS DateTime), N'CU00000005')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (9, 6, 1, CAST(N'2023-12-20T23:59:13.080' AS DateTime), N'CU00000004')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (10, 4, 1, CAST(N'2023-12-20T23:59:32.377' AS DateTime), N'CU00000004')
GO
INSERT [dbo].[PostVote] ([voteId], [PostId], [isLike], [createdDate], [voteBy]) VALUES (11, 7, 1, CAST(N'2023-12-20T23:59:54.720' AS DateTime), N'CU00000004')
GO
SET IDENTITY_INSERT [dbo].[PostVote] OFF
GO
SET IDENTITY_INSERT [dbo].[PostImage] ON 
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1, 1, N'AFP_9A69C6_1621255205791_1621312561575_20231216_051655.webp')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (2, 1, N'dubaothoitiet_20231216_051655.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (3, 2, N'post1_20231219_212119.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (4, 2, N'post1-1_20231219_212119.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (5, 2, N'post1-2_20231219_212119.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (6, 2, N'post1-3_20231219_212119.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (7, 3, N'com_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (8, 3, N'com-rang-thap-cam-5-600x339_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (9, 3, N'image10-1607667365-797-width640height450_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (10, 3, N'bun-cha-2_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (11, 3, N'bun-cha-3_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (12, 3, N'bun-dau-1_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (13, 3, N'bun-dau-2_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (14, 3, N'bun-dau-3_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (15, 3, N'banh-mi1_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (16, 3, N'banh-mi2_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (17, 3, N'banh-mi3_20231219_220431.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (18, 4, N'410552153_328069633329960_6705600994476230299_n_20231220_211159.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (19, 4, N'411404285_328069583329965_5563911415305360455_n_20231220_211159.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (20, 4, N'411406001_328069556663301_2807522322192771919_n_20231220_211159.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (21, 4, N'410502417_328069559996634_3056301884193381309_n_20231220_211159.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (22, 6, N'pcgaming_20231220_212938.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (23, 6, N'1106_goc-pc-gaming-choi-game_20231220_212938.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (24, 7, N'411228042_373868468363679_452088188237676005_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (25, 7, N'411228106_373868431697016_2747942970577164448_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (26, 7, N'411431050_373868198363706_3149914828337885783_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (27, 7, N'411585233_373868521697007_7916288933121805112_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (28, 7, N'411662281_373868495030343_8827178421856938172_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (29, 7, N'411666187_373868515030341_2396966821224295510_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (30, 7, N'411667107_373868531697006_5076491463461333295_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (31, 7, N'411670808_373868558363670_9156424040675662875_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (32, 7, N'411741214_373868415030351_5549913449744768924_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (33, 7, N'412427697_373868581697001_6360858334125477776_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (34, 7, N'412660845_373868595030333_1472412169544209845_n_20231220_213347.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1018, 1004, N'410635566_1531723694282564_8597467158913128299_n_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1019, 1004, N'410894015_1531723630949237_2065036397900951938_n_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1020, 1004, N'410987818_1531723617615905_7313803860875201825_n_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1021, 1004, N'Cơmrangdưabò_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1022, 1004, N'Gàchiênhoàngbào2023-12-20205119_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1023, 1004, N'HeoChaoRieng_20231220_220742.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1024, 1005, N'410247635_1761323111049326_3934209773277302992_n_20231220_223744.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1025, 1005, N'411852685_1761323081049329_3089018823261583425_n_20231220_223744.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1026, 1005, N'412031689_1761323141049323_6744911823591019808_n_20231220_223744.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1027, 1005, N'412266361_1761323177715986_3986053308216091416_n_20231220_223744.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1028, 1005, N'412138316_1761323204382650_8848862056329775293_n_20231220_223744.jpg')
GO
INSERT [dbo].[PostImage] ([imageId], [postId], [path]) VALUES (1029, 1006, N'383952659_1746924935747120_333902451927013233_n_20231220_234426.jpg')
GO
SET IDENTITY_INSERT [dbo].[PostImage] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 
GO
INSERT [dbo].[Comment] ([commentId], [postId], [customerId], [commentContent], [createdDate]) VALUES (1, 6, N'CU00000004', N'wow', CAST(N'2023-12-20T23:59:20.720' AS DateTime))
GO
INSERT [dbo].[Comment] ([commentId], [postId], [customerId], [commentContent], [createdDate]) VALUES (2, 4, N'CU00000004', N'Yummy', CAST(N'2023-12-20T23:59:42.080' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerBan] ON 
GO
INSERT [dbo].[SellerBan] ([banSellerId], [sellerId], [Reason], [CreateDate]) VALUES (1, N'SE00000001', N'Vì giấy phép kinh doanh sai', CAST(N'2023-12-18T07:47:38.120' AS DateTime))
GO
INSERT [dbo].[SellerBan] ([banSellerId], [sellerId], [Reason], [CreateDate]) VALUES (2, N'SE00000001', N'Mở ban', CAST(N'2023-12-18T13:59:21.473' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SellerBan] OFF
GO
INSERT [dbo].[Shipper] ([shipperId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [manageBy], [confirmedEmail], [Status], [refreshToken], [refreshTokenExpiryTime], [createDate], [Note], [isPhoneVerified], [otpToken], [otpTokenExpiryTime], [IDcardNumber], [IDcardFrontImage], [IDcardBackImage]) VALUES (N'SH00000001', N'Lư', N'Nguyễn', N'male', CAST(N'2003-12-28' AS Date), N'lunguyen2@gmail.com', N'0974280518', 0xD9A4867EF864AC913874892887EA8FE1C486DB9C88FFDBBF18C5A681194284918EBBE4E6E27E3DBA691E726602E254633A486FEADFD575BA4890DBCBF6D94603, 0xF27F16671699676929745DD5C2A7D7D26A602593DF0DF42B2794BD45207F122D, 0, N'SE00000006', 1, 1, N'PxtTIF666+xkpM+FB++WiyALBCl25p9C62fYE7oK1mBylpH0dX3h3ZyukKmzbc2Dnp87m/3TnoqZjQYJ7CztAg==', CAST(N'2023-12-27T23:26:27.120' AS DateTime), CAST(N'2023-12-20T21:55:08.477' AS DateTime), N'Dang ky duoc chap nhan', 1, NULL, NULL, N'059493929543', N'cancuoc_20231220_215508.jpg', N'8bf058fe053a152f0854badcc8bce242(1)_20231220_215508.jpg')
GO
SET IDENTITY_INSERT [dbo].[Invitation] ON 
GO
INSERT [dbo].[Invitation] ([InvitationId], [SellerID], [ShipperID], [Accepted]) VALUES (1, N'SE00000006', N'SH00000001', 2)
GO
INSERT [dbo].[Invitation] ([InvitationId], [SellerID], [ShipperID], [Accepted]) VALUES (2, N'SE00000006', N'SH00000001', 2)
GO
INSERT [dbo].[Invitation] ([InvitationId], [SellerID], [ShipperID], [Accepted]) VALUES (3, N'SE00000006', N'SH00000001', 1)
GO
SET IDENTITY_INSERT [dbo].[Invitation] OFF
GO
SET IDENTITY_INSERT [dbo].[ChatMessage] ON 
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (1, N'CU00000005', N'SE00000009', 0, N'hello', CAST(N'2023-12-20T21:14:54.797' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (2, N'CU00000005', N'SE00000006', 0, N'hello', CAST(N'2023-12-20T21:15:08.503' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (3, N'CU00000005', N'SE00000009', 1, N'alo', CAST(N'2023-12-20T21:17:18.630' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (4, N'CU00000005', N'SE00000009', 1, N'co do k', CAST(N'2023-12-20T21:17:45.767' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (5, N'CU00000005', N'SE00000009', 0, N'điêu', CAST(N'2023-12-20T21:17:50.330' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (6, N'CU00000005', N'SE00000006', 0, N'ee', CAST(N'2023-12-20T21:18:03.970' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (7, N'CU00000005', N'SE00000006', 1, N'Hello', CAST(N'2023-12-20T21:19:01.463' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (8, N'CU00000005', N'SE00000006', 0, N'xin rcm chè nào ổn nào', CAST(N'2023-12-20T21:19:26.800' AS DateTime), 1)
GO
INSERT [dbo].[ChatMessage] ([MessageId], [CustomerId], [SellerId], [SenderType], [Message], [SentAt], [IsRead]) VALUES (9, N'CU00000005', N'SE00000006', 1, N'Quán mình có cơm sườn bò, bánh bao nướng, mì indo full toping đồng giá 35k', CAST(N'2023-12-20T21:20:26.677' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[ChatMessage] OFF
GO
INSERT [dbo].[Admin] ([adminId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [walletBalance], [confirmedEmail], [refreshToken], [refreshTokenExpiryTime], [createDate]) VALUES (N'AD00000001', N'string', N'string', N'string', CAST(N'2003-12-03' AS Date), N'admin@gmail.com', NULL, 0x243476CD2C95DC8AEEC82F8E3C9B734FD565AA7C69104A29D61C34D5762B5F837CE9F9CA4EC2DC835CCB990C161389FBBBD4C858C7BCA0EECA3448EB3F656391, 0xCB36CD2343CFD625702F66B6821FD9D9718945C2D9F0C98E0CBB48135EAC0FD0, 0, NULL, 1, N'3lMT5GnTJ5TEarpiscA7S1jGvhWY52jEQuA5HgExRFMXOLvTM07+eCthlnGCX1PCNq0YXxzKkQPmhuFCHI4usw==', CAST(N'2023-12-28T00:09:45.483' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[SellerLicenseImage] ON 
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (1, N'SE00000001', N'download(3)_20231216_044856.jfif', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2, N'SE00000001', N'Screenshot-2023-02-21-102427_20231216_044856.png', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (1002, N'SE00000002', N'1_20231218_184512.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (1003, N'SE00000002', N'2_20231218_184512.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2002, N'SE00000003', N'3_20231219_215950.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2003, N'SE00000003', N'4_20231219_215950.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2004, N'SE00000004', N'mau-giay-phep-dang-ky-kinh-doanh-ho-gia-dinh-744x1030_20231219_225444.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2005, N'SE00000004', N'Loai_2_SX_Kinh_Doanh_20231219_225444.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2006, N'SE00000005', N'giayphep1_20231219_231003.png', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (2007, N'SE00000005', N'giayphep2_20231219_231003.png', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3004, N'SE00000006', N'dang-ky-giay-phep-kinh-doanh-ho-ca-the_20231220_175153.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3005, N'SE00000006', N'gpkd-mat-sau_20231220_175153.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3006, N'SE00000007', N'gpkd-mat-sau_20231220_203506.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3007, N'SE00000007', N'Giay-phep-kinh-doanh-2_20231220_203506.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3008, N'SE00000008', N'gpkd-mat-sau_20231220_203904.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3009, N'SE00000008', N'Giay-phep-kinh-doanh-3_20231220_203904.jpg', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3010, N'SE00000009', N'giayphep1_20231220_205935.png', 0)
GO
INSERT [dbo].[SellerLicenseImage] ([imageLicenseId], [sellerId], [path], [isReplaced]) VALUES (3011, N'SE00000009', N'giayphep2_20231220_205935.png', 0)
GO
SET IDENTITY_INSERT [dbo].[SellerLicenseImage] OFF
GO
INSERT [dbo].[MenuModerator] ([modId], [firstName], [lastName], [gender], [birthDate], [email], [phoneNumber], [PasswordSalt], [PasswordHash], [isOnline], [confirmedEmail], [isBanned], [refreshToken], [refreshTokenExpiryTime], [banLimit], [reportApprovalLimit], [createDate]) VALUES (N'MM00000001', N'Linh', N'Nguyễn', N'female', CAST(N'2002-12-25' AS Date), N'menumod1@gmail.com', N'0368974251', 0x2FA39F6332AD73E474E351EF46D4E8922ACD85E7424372B4FAAC156B51A3BF60E40A4BCC9D28EA05F8FABA73C010B78E36AE8E45ABABBA932B2DC822E3597684, 0x8D3F8B469F4EAAB1A3E5727982862E92EF5D4A5F5C4F1B39FF0177A4D69F423D, 0, 1, 0, N'TDA79E/5kLELhRp8Ka3fKQVAOCTLq+V0o7BfyIKx5T8EE2U83UCIRZKY7LX/bx9VldHU1KhHmcbnTOMxc4MRBA==', CAST(N'2023-12-27T23:45:55.233' AS DateTime), 24, 25, NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1, N'Rice', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (2, N'Drink', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1002, N'Noodles', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1003, N'Bread', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1004, N'Energy Drink', 0)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (1005, N'Fruit', 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [status]) VALUES (2002, N'Junk Food', 1)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1, N'SE00000001', N'Cơm rang', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-16T04:57:38.503' AS DateTime), N'Cơm trộn Hàn Quốc (Bibimbap2) là món ăn phổ biến của người Hàn Quốc nhưng khá hợp với khẩu vị của người Việt. Đây là một món ăn đầy màu sắc và hương vị độc đáo, là sự pha trộn của nhiều loại rau củ, thịt và trứng trên nền gạo trắng. 

Thành phẩm có sự kết hợp giữa cơm trắng thơm ngon, mềm dẻo, trứng lòng đào béo ngậy, phần rau củ tươi ngon, vị thịt bò tươi mát cùng với phần nước sốt thần thánh, tất cả hòa trộn với nhau để rồi tạo nên một hương vị hoàn hảo và hấp dẫn.

', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2, N'SE00000001', N'Bún bò', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:38:23.983' AS DateTime), N'Bún bò Huế là một viên ngọc tiềm ẩn của Việt Nam vẫn chưa “làm nên tên tuổi” trong nền ẩm thực chính thống của Mỹ. Đó là một món súp đậm đà và cay với nhiều lớp hương vị đậm đà. Món súp miền Trung này được kết hợp với những lát thịt bò và thịt lợn mềm, sau đó phủ rất nhiều loại thảo mộc tươi.

Tôi đã hỏi ý kiến người đầu bếp người Việt yêu thích của tôi – Mẹ – về cách làm bún bò Huế. Và để tìm ra những sản phẩm phụ làm nên tính xác thực của BBH. Tôi hứa bạn sẽ thích phiên bản này!', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (3, N'SE00000001', N'Bún đậu mắm tôm', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:40:35.770' AS DateTime), N'Nguyên liệu:
1,5kg bún lá
6-7 bìa đậu phụ
500g thịt chân giò
250g chả cốm
Mắm tôm
Đường
Bột ngọt
Rượu trắng
Ớt tươi
Quất hoặc chanh
Rau thơm ăn kèm: kinh giới, tía tô, húng...
Hãy chọn cho mình một nguyên liệu trên, tùy vào sở thích mà bạn có thể chế biến thêm ít lòng lợn như dồi luộc, dồi sụn nướng, lòng non luộc hay cuống họng luộc... Tuy nhiên, có thể thấy rằng, dù được biến tấu phù hợp với văn hóa vùng miền hay khẩu vị thì cốt lõi của món bún đậu vẫn là bún và đậu phụ rán, chấm cùng với mắm tôm. Và đây vẫn là món ăn mang đậm hương vị người Việt, của người Hà Nội.', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (4, N'SE00000001', N'Bún chả', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:42:13.603' AS DateTime), N'Bún chả đặc biệt dân dã và bình dị ngay từ khâu chế biến tới cách thưởng thức. Việc ngồi trên những bộ bàn ghế nhựa ngoài vỉa hè, xì xụp đĩa bún trắng tinh, mềm mịn bên tô mắm nóng ấm đỏ vàng dường như đã trở nên quá ư thường nhật với người Việt.
Bún chả bao gồm 3 phần chính là nước chấm, chả nướng và tất nhiên, bún. Một suất bún chả có ngon hay không được quyết định phần lớn bởi nước chấm. Nước chấm bún chả được pha đầy đủ chua, cay, mặn, ngọt với mắm, giấm, đường, tỏi, ớt cùng lượng phù hợp tùy vào người pha chế, trong bát nước chấm luôn có thêm nộm gồm đu đủ xanh, cà rốt hay nhiều nơi có cả giá đỗ. Chả nướng có 2 loại là chả miếng và chả viên, thường thì chả miếng sẽ được làm từ thịt ba chỉ để thịt có độ mềm và ngọt nhất định, chả viên được nặn thành khối tròn bằng khoảng ¼ lòng bàn tay, tẩm ướp và nướng dưới bếp than củi đỏ hồng.', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (5, N'SE00000002', N'Trà sữa', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:52:32.617' AS DateTime), N'Trà sữa từ lâu đã trở thành thức uống Quốc dân của giới trẻ Việt. Hiện nay trên thị trường có rất nhiều thương hiệu trà sữa nổi tiếng, được đông đảo khách hàng ưa chuộng và ủng hộ.
Nước pha trà là tinh túy hương vị nguyên bản nhất, nên sử dụng nước nóng từ 80 đến 90 độ C.
Hồng trà (trà đen) kết hợp với các nguyên liệu pha chế trà sữa, thức uống có màu sắc đẹp mắt, vị trà đậm dễ gây nghiện.
Bột sữa Kievit Milk Cap là bột sữa có hương sữa thơm, vị ngọt mặn đã được cân bằng, tạo ra lớp Milk Foam đứng Foam trên bề mặt ly nước mà không bị tuột.', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (6, N'SE00000002', N'Cafe đen', CAST(20000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:55:21.850' AS DateTime), N'Cà phê đen là loại thức uống nguyên chất được pha từ hạt cà phê đã được rang, xay theo tiêu chuẩn. Cà phê đen không giống các loại cà phê khác, nó rất đơn giản từ cách pha đến nguyên liệu chuẩn bị.
Nguyên liệu chính là bột cà phê. Bột được lựa chọn phải hoàn toàn nguyên chất, không pha trộn với bất kì loại bột nào, đảm bảo độ mịn của bột. Mua bột cà phê từ nơi uy tín, có nguồn gốc rõ ràng. 
Lợi ích: 
Làm sạch dạ dày: Ít ai biết đến cà phê đen còn giúp chúng ta lợi tiểu, bằng cách uống cà phê đen không đường thì các độc tố và vi khuẩn có thể dễ dàng được đào thải vì thế dạ dày của chúng ta đã được làm sạch.
Hỗ trợ giảm cân: Cà phê đen thúc đẩy quá trình trao đổi chất, làm cho chúng ta tiêu hóa nhanh hơn, giảm thèm ăn, và giúp cho quá trình tập luyện trở nên năng xuất hơn.', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (7, N'SE00000002', N'Cafe sóc', CAST(15000 AS Decimal(18, 0)), CAST(N'2023-12-18T18:57:26.713' AS DateTime), N'Nguyên liệu: 
Sản phẩm của cà phê Con Sóc được chế biến từ những hạt cà phê tuyển lựa từ cao nguyên Đà Lạt, Lâm Đồng trồng ở độ cao 1500m so với mực nước biển, trong điều kiện khí hậu lý tưởng, ôn hòa, đất đỏ bazan phù hợp cho việc canh tác trồng cà phê.', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1002, N'SE00000001', N'Cơm sườn gà', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-19T21:10:22.360' AS DateTime), N'XÔI SƯỜN & GÀ HABI STREETS CUISINE HOÀ LẠC XIN CHÀO 👋
----------
🥰 Với công thức nước sốt độc quyền cùng nguyên liệu tươi 100%, xôi sườn & gà cay Habi chắc chắn sẽ không làm bạn thất vọng
- Xôi Sườn Cay Habi: 35k
- Xôi Gà Cay Habi : 35k
- Combo Xôi Sườn Và Trà Chanh : 45k
- ComBo Xôi Gà Cay Trà Chanh : 45K
🍗 Đồ gọi thêm : 
- Thêm xôi : 5k
- Thêm sườn : 23k
- Thêm gà : 25k
- Thêm trứng ốp : 6k
- Trà chanh nha đam : 15k
Được làm từ sườn non và đùi gà tươi ngon 100% - gạo nếp hảo hạng - cùng nước sốt cay độc quyền', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1003, N'SE00000001', N'Bún ngon', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-19T21:12:34.123' AS DateTime), N'BÁN TẠI QUÁN  VÀ SHIP TỪ 6H SÁNG ĐẾN 23h
           RIÊU CUA BÒ 30-35K  
           RIÊU - GIÒ TAI- ĐẬU 30K
           RIÊU - BÒ- GIÒ - ĐẬU 35K 
           BÚN GÀ TA MỌC 35K 
           BÚN ỐC 30K 
           BÚN CHẢ CHẤM 
           BÚN CÁ CAY ( KHÔNG CAY ) 30K 
           BÚN CHẢ CANH 30k
           BÚN MỌC 30K 
           BÚN THẬP CẨM 35K 
           BÚN RIÊU CUA CHẢ NƯỚNG 35K
Thêm bún 5k, thêm chả 10k ak
     Tri ân khách hàng FREE SHIP ', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1004, N'SE00000002', N'Trà sữa ô long', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-19T21:14:50.663' AS DateTime), N'Trà sữa ô long chân trâu ô long nhài - Đậm trà - ít ngọt - Chân trâu siêu ngon ạ❤️❤️❤️❤️
Ô long sữa 35k
Ô long nhài sữa 35k
Ô long sữa kem trứng 39k
Ô long sữa kem cheese 39k
Ô long sen vàng 35k
Trà sen lá nếp 35k
👉👉Ship quanh Hola 
👉👉Trà ô long bên em trà ủ lạnh - Sục nóng ko sẵn số lượng nhiều đâu ạ. Khách dùng số lượng nhiều order trước giúp em. Em cảm ơn!!', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1005, N'SE00000005', N'Mì Indomie', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-20T20:50:48.740' AS DateTime), N'Sợi mì vàng dai ngon thấm đều từng sợi trong nước sốt mì trộn Indomie Mi Goreng cay nồng, màu sắc đẹp vô cùng hấp dẫn cùng hương thơm lừng quyến rũ. 40 gói mì xào khô Indomie Mi Goreng Pedas vị cay nồng 79g, lựa chọn hoàn hảo cho bữa ăn nhanh gọn, đơn giản và dinh dưỡng.', 1002, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1006, N'SE00000005', N'Cháo ếch', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-20T20:51:39.140' AS DateTime), N'Cháo ếch là món ăn nổi tiếng của Singapore được du nhập vào Việt Nam trong thời gian gần đây. Món ăn không chỉ thơm ngon mà còn rất có lợi cho sức khỏe. Trong thịt ếch chứa rất ít cholesterol, giàu protein và kali, hỗ trợ giảm huyết áp cao, đột quỵ, suy nhược thần kinh, thể chất, bồi bổ sức khỏe,...', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1007, N'SE00000005', N'Cơm đùi gà', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T20:53:07.143' AS DateTime), N'Cơm rang, hay còn gọi là cơm chiên, kết hợp cùng với đùi gà rán và các nguyên liệu khác đi kèm, đem đến cho bạn một đĩa cơm đầy chất lượng, giàu dinh dưỡng. Thế nhưng, làm thế nào để chế biến món này ngon nhất thì chắc chắn không phải ai cũng biết đâu nhé.', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1008, N'SE00000005', N'Nước ép dưa hấu', CAST(20000 AS Decimal(18, 0)), CAST(N'2023-12-20T20:55:40.470' AS DateTime), N'Nước ép dưa hấu có tác dụng gì cho sức khỏe?
Cung cấp nước cho cơ thể Trong dưa hấu có đến 95% là nước. ...
Chống oxy hóa và bảo vệ cơ thể ...
Phòng ngừa ung thư ...
Phòng ngừa bệnh lý tim mạch. ...
Ngừa bệnh thoái hóa điểm vàng. ...
Giảm đau nhức cơ bắp. ...
Hỗ trợ tiêu hóa. ...
Tốt cho da và tóc.', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1009, N'SE00000009', N'Super Sundae Socola', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:01:09.103' AS DateTime), N'Super Sundae Socola
', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1010, N'SE00000009', N'Trà Đào Bốn Mùa', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:01:55.303' AS DateTime), N'Trà Đào Bốn Mùa
ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223
', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1011, N'SE00000009', N'Nước Chanh Tươi Lạnh', CAST(15000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:02:33.697' AS DateTime), N'Nước Chanh Tươi Lạnh
ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1012, N'SE00000009', N'Super Sundae Mâm Xôi', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:03:18.467' AS DateTime), N'Super Sundae Mâm Xôi
ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1013, N'SE00000009', N'Super Sundae Trân Châu Đường Đen', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:03:53.477' AS DateTime), N'Super Sundae Trân Châu Đường Đen

ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1014, N'SE00000009', N'Trà Sữa Trân Châu Đường Đen', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:04:42.707' AS DateTime), N'Trà Sữa Trân Châu Đường Đen', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1015, N'SE00000009', N'Trà Sữa Bá Vương', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:05:20.600' AS DateTime), N'Trà Sữa Bá Vương

ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1016, N'SE00000006', N'Mì Indo Full Toping', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:06:47.897' AS DateTime), N'Mì indo dai dòn sần sật', 1002, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1017, N'SE00000009', N'Trà Kem Mâm Xôi', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:08:13.060' AS DateTime), N'Trà Kem Mâm Xôi

ĐẶT HÀNG NHANH LIÊN HỆ 0989.764.223', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1018, N'SE00000006', N'Combo bánh mì', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:08:39.520' AS DateTime), N'Combo bánh mì cổ điển 30k, nhanh tay nhanh tay. Gồm xúc xích, bánh chả, khoai tây chiên, viên chiên.', 1003, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1019, N'SE00000009', N'Dương Chi Cam Lộ', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:09:17.140' AS DateTime), N'Dương Chi Cam Lộ', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1020, N'SE00000006', N'Combo bánh bao', CAST(30000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:10:27.330' AS DateTime), N'Combo bánh bao thơm ngon nức mũi. Gồm nem chua rán, xúc xích, viên chiên, bánh bao, khoai lang chiên', 1003, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1021, N'SE00000006', N'Combo 75k', CAST(75000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:15:52.553' AS DateTime), N'Combo gồm tôm, lạp sườn, viên chiên, xúc xích.', 2002, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (1022, N'SE00000009', N'Đào 45kg', CAST(2400000 AS Decimal(18, 0)), CAST(N'2023-12-20T21:35:20.407' AS DateTime), N'Đào tươi', 2002, 3, 0, N'MM00000001', CAST(N'2023-12-20T23:47:06.080' AS DateTime), N'Thực phẩm không phù hợp')
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2005, N'SE00000008', N'Gà chiên hoàng bào', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:02:21.510' AS DateTime), N'Gà chiên hoàng bào ngon ngon', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2006, N'SE00000008', N'Cơm rang', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:03:54.357' AS DateTime), N'Cơm rang dưa bò cổ điển thơm ngon', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2007, N'SE00000008', N'Heo chao riềng', CAST(35000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:06:06.453' AS DateTime), N'Heo chao riềng ngon ngọt', 1, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2008, N'SE00000007', N'Trà sữa socola', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:34:02.887' AS DateTime), N'Trà sữa socola ngon ngon', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2009, N'SE00000007', N'Trà sữa dâu tây', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:34:52.560' AS DateTime), N'Trà sũa dâu tây ngọt lịm', 2, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Food] ([foodId], [sellerId], [name], [unitPrice], [createDate], [description], [categoryId], [status], [reportedTimes], [banBy], [banDate], [banNote]) VALUES (2010, N'SE00000007', N'Sữa chua thanh long', CAST(25000 AS Decimal(18, 0)), CAST(N'2023-12-20T22:37:01.233' AS DateTime), N'Sữa chua hoa quả dầm phiên bản thanh long', 2, 1, 0, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[Voucher] ON 
GO
INSERT [dbo].[Voucher] ([voucherId], [sellerId], [code], [discount_amount], [minimum_order_value], [status], [effectiveDate], [expireDate]) VALUES (1, N'SE00000001', N'7WEEUXXCDU', CAST(10000.00 AS Decimal(10, 2)), CAST(5.00 AS Decimal(10, 2)), 1, CAST(N'2023-12-08T00:00:00.000' AS DateTime), CAST(N'2023-12-14T12:00:10.000' AS DateTime))
GO
INSERT [dbo].[Voucher] ([voucherId], [sellerId], [code], [discount_amount], [minimum_order_value], [status], [effectiveDate], [expireDate]) VALUES (2, N'SE00000009', N'LZ1MB4EODT', CAST(5000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)), 1, CAST(N'2023-12-20T21:30:09.000' AS DateTime), CAST(N'2024-01-28T21:30:14.000' AS DateTime))
GO
INSERT [dbo].[Voucher] ([voucherId], [sellerId], [code], [discount_amount], [minimum_order_value], [status], [effectiveDate], [expireDate]) VALUES (3, N'SE00000009', N'ZZJWHCP3VQ', CAST(10000.00 AS Decimal(10, 2)), CAST(110000.00 AS Decimal(10, 2)), 1, CAST(N'2023-12-20T21:31:29.000' AS DateTime), CAST(N'2024-01-28T21:31:34.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Voucher] OFF
GO
INSERT [dbo].[Groups] ([Name]) VALUES (N'anhpnqb@gmail.com-lunguyen2k18@gmail.com')
GO
INSERT [dbo].[Groups] ([Name]) VALUES (N'duocnqhe150632@fpt.edu.vn-lunguyen2k18@gmail.com')
GO
INSERT [dbo].[Groups] ([Name]) VALUES (N'fukyounyancat@gmail.com-lunguyen2k18@gmail.com')
GO
INSERT [dbo].[Groups] ([Name]) VALUES (N'lunguyen2k18@gmail.com-phamanhqb73@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[ShipAddress] ON 
GO
INSERT [dbo].[ShipAddress] ([addressId], [customerId], [addressInfo], [isDefaultAddress]) VALUES (1, N'CU00000004', N'Nhà trọ Minh Thúy 99, ngõ bể bơi. Xã Thạch Hoà, Huyện Thạch Thất, Thành phố Hà Nội', 0)
GO
INSERT [dbo].[ShipAddress] ([addressId], [customerId], [addressInfo], [isDefaultAddress]) VALUES (2, N'CU00000004', N'Số nhà 16, tổ 12. Phường Tân Thịnh, Thành phố Hòa Bình, Tỉnh Hoà Bình', 1)
GO
SET IDENTITY_INSERT [dbo].[ShipAddress] OFF
GO
INSERT [dbo].[CartItem] ([foodId], [CreateDate], [cartId], [amount]) VALUES (1, CAST(N'2023-12-19T05:42:51.423' AS DateTime), N'CU00000002', 1)
GO
INSERT [dbo].[CartItem] ([foodId], [CreateDate], [cartId], [amount]) VALUES (6, CAST(N'2023-12-20T15:49:45.563' AS DateTime), N'CU00000002', 1)
GO
INSERT [dbo].[CartItem] ([foodId], [CreateDate], [cartId], [amount]) VALUES (7, CAST(N'2023-12-20T15:49:47.633' AS DateTime), N'CU00000002', 1)
GO
SET IDENTITY_INSERT [dbo].[FoodImage] ON 
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1, 1, N'com_20231216_045738.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2, 1, N'com-rang-thap-cam-5-600x339_20231216_045738.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3, 1, N'image10-1607667365-797-width640height450_20231216_045738.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1002, 2, N'bun-bo-1_20231218_183823.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1003, 2, N'bun-bo-2_20231218_183823.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1004, 2, N'bun-bo-3_20231218_183823.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1005, 3, N'bun-dau-1_20231218_184035.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1006, 3, N'bun-dau-2_20231218_184035.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1007, 3, N'bun-dau-3_20231218_184035.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1008, 3, N'bun-dau-4_20231218_184035.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1009, 4, N'bun-cha-1_20231218_184213.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1010, 4, N'bun-cha-2_20231218_184213.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1011, 4, N'bun-cha-3_20231218_184213.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1012, 5, N'tra-sua-1_20231218_185232.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1013, 5, N'tra-sua-2_20231218_185232.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1014, 5, N'tra-sua-3_20231218_185232.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1015, 5, N'tra-sua-4_20231218_185232.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1016, 6, N'cafe-den-1_20231218_185521.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1017, 6, N'cafe-den-2_20231218_185521.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1018, 6, N'cafe-den-3_20231218_185521.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1019, 7, N'cafe-soc-1_20231218_185726.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (1020, 7, N'cafe-soc-2_20231218_185726.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2002, 1002, N'com-suon3_20231219_211022.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2003, 1003, N'bun-ngon1_20231219_211234.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2004, 1003, N'bun-ngon2_20231219_211234.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2005, 1004, N'tra-sua-olong_20231219_211450.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2006, 1005, N'411735361_691573759620822_5247062006994230403_n_20231220_205048.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2007, 1006, N'410242843_691573802954151_6861293890529148828_n_20231220_205139.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2008, 1007, N'411153696_691573782954153_1491825160204149284_n_20231220_205307.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2009, 1008, N'411950238_691573872954144_2708657189164614496_n_20231220_205540.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2010, 1009, N'Super-Sundae-Socola-768x768_20231220_210109.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2011, 1010, N'Tra-Dao-Bon-Mua-768x768_20231220_210155.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2012, 1011, N'Nuoc-Chanh-Tuoi-Lanh-768x768_20231220_210233.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2013, 1012, N'Super-Sundae-mam-xoi-768x960_20231220_210318.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2014, 1013, N'Super-Sundae-Tran-Chau-Duong-Den-768x768_20231220_210353.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2015, 1014, N'Tra-Sua-Tran-Chau-Duong-Den-768x768_20231220_210442.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2016, 1015, N'Tra-Sua-Ba-Vuong-768x768_20231220_210520.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2017, 1016, N'411662281_373868495030343_8827178421856938172_n_20231220_210647.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2018, 1017, N'Tra-Kem-Mam-Xoi-768x960_20231220_210813.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2019, 1018, N'411670808_373868558363670_9156424040675662875_n_20231220_210839.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2020, 1019, N'Duong-Chi-Cam-Lo-768x768_20231220_210917.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2021, 1020, N'412660845_373868595030333_1472412169544209845_n_20231220_211027.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2022, 1021, N'411741214_373868415030351_5549913449744768924_n_20231220_211552.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (2023, 1022, N'382988259_2637969596354621_8612568966631557278_n_20231220_213520.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3006, 2005, N'Gàchiênhoàngbào2023-12-20205119_20231220_220221.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3007, 2006, N'Cơmrangdưabò_20231220_220354.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3008, 2007, N'HeoChaoRieng_20231220_220606.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3009, 2008, N'411852685_1761323081049329_3089018823261583425_n_20231220_223402.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3010, 2009, N'410247635_1761323111049326_3934209773277302992_n_20231220_223452.jpg')
GO
INSERT [dbo].[FoodImage] ([imageId], [foodId], [path]) VALUES (3011, 2010, N'412031689_1761323141049323_6744911823591019808_n_20231220_223701.jpg')
GO
SET IDENTITY_INSERT [dbo].[FoodImage] OFF
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (10, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1002 đang chờ để được chấp nhận', CAST(N'2023-12-19T21:10:22.737' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (10, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1002 is waiting for approval', CAST(N'2023-12-19T21:10:22.737' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (11, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1003 đang chờ để được chấp nhận', CAST(N'2023-12-19T21:12:34.160' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (11, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1003 is waiting for approval', CAST(N'2023-12-19T21:12:34.160' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (12, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1004 đang chờ để được chấp nhận', CAST(N'2023-12-19T21:14:50.690' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (12, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1004 is waiting for approval', CAST(N'2023-12-19T21:14:50.690' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (13, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 2 được tạo mới', CAST(N'2023-12-19T21:21:20.043' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (13, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 2 has been created', CAST(N'2023-12-19T21:21:20.043' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (21, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 3 được tạo mới', CAST(N'2023-12-19T22:04:31.300' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (21, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 3 has been created', CAST(N'2023-12-19T22:04:31.300' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (22, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1005 đang chờ để được chấp nhận', CAST(N'2023-12-20T20:50:48.880' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (22, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1005 is waiting for approval', CAST(N'2023-12-20T20:50:48.880' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (23, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1006 đang chờ để được chấp nhận', CAST(N'2023-12-20T20:51:39.183' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (23, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1006 is waiting for approval', CAST(N'2023-12-20T20:51:39.183' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (24, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1007 đang chờ để được chấp nhận', CAST(N'2023-12-20T20:53:07.173' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (24, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1007 is waiting for approval', CAST(N'2023-12-20T20:53:07.173' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (25, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1008 đang chờ để được chấp nhận', CAST(N'2023-12-20T20:55:40.517' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (25, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1008 is waiting for approval', CAST(N'2023-12-20T20:55:40.517' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (26, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1009 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:01:09.160' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (26, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1009 is waiting for approval', CAST(N'2023-12-20T21:01:09.160' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (27, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1010 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:01:55.337' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (27, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1010 is waiting for approval', CAST(N'2023-12-20T21:01:55.337' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (28, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1011 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:02:33.743' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (28, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1011 is waiting for approval', CAST(N'2023-12-20T21:02:33.743' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (29, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1012 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:03:18.510' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (29, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1012 is waiting for approval', CAST(N'2023-12-20T21:03:18.510' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (30, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1013 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:03:53.550' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (30, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1013 is waiting for approval', CAST(N'2023-12-20T21:03:53.550' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (31, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1014 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:04:42.800' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (31, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1014 is waiting for approval', CAST(N'2023-12-20T21:04:42.800' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (32, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1015 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:05:20.680' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (32, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1015 is waiting for approval', CAST(N'2023-12-20T21:05:20.680' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (33, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1016 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:06:47.943' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (33, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1016 is waiting for approval', CAST(N'2023-12-20T21:06:47.943' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (34, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1017 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:08:13.190' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (34, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1017 is waiting for approval', CAST(N'2023-12-20T21:08:13.190' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (35, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1018 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:08:39.593' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (35, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1018 is waiting for approval', CAST(N'2023-12-20T21:08:39.593' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (36, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1019 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:09:17.213' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (36, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1019 is waiting for approval', CAST(N'2023-12-20T21:09:17.213' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (37, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1020 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:10:27.417' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (37, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1020 is waiting for approval', CAST(N'2023-12-20T21:10:27.417' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (38, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 4 được tạo mới', CAST(N'2023-12-20T21:12:00.043' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (38, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 4 has been created', CAST(N'2023-12-20T21:12:00.043' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (39, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 5 được tạo mới', CAST(N'2023-12-20T21:14:11.273' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (39, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 5 has been created', CAST(N'2023-12-20T21:14:11.273' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (40, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1021 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:15:52.640' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (40, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1021 is waiting for approval', CAST(N'2023-12-20T21:15:52.640' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (41, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 6 được tạo mới', CAST(N'2023-12-20T21:29:39.040' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (41, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 6 has been created', CAST(N'2023-12-20T21:29:39.040' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (42, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 7 được tạo mới', CAST(N'2023-12-20T21:33:47.477' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (42, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 7 has been created', CAST(N'2023-12-20T21:33:47.477' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (43, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 1022 đang chờ để được chấp nhận', CAST(N'2023-12-20T21:35:20.463' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (43, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 1022 is waiting for approval', CAST(N'2023-12-20T21:35:20.463' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (44, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2005 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:02:21.763' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (44, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2005 is waiting for approval', CAST(N'2023-12-20T22:02:21.763' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (45, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2006 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:03:54.410' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (45, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2006 is waiting for approval', CAST(N'2023-12-20T22:03:54.410' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (46, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2007 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:06:06.590' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (46, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2007 is waiting for approval', CAST(N'2023-12-20T22:06:06.590' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (47, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 1004 được tạo mới', CAST(N'2023-12-20T22:07:43.067' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (47, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 1004 has been created', CAST(N'2023-12-20T22:07:43.067' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (48, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2008 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:34:03.030' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (48, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2008 is waiting for approval', CAST(N'2023-12-20T22:34:03.030' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (49, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2009 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:34:52.630' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (49, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2009 is waiting for approval', CAST(N'2023-12-20T22:34:52.630' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (50, 0, N'System', N'MM00000001', 0, N'Menu mới', N'Menu mới ID: 2010 đang chờ để được chấp nhận', CAST(N'2023-12-20T22:37:01.337' AS DateTime), 1)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (50, 1, N'System', N'MM00000001', 0, N'New Menu', N'Menu ID: 2010 is waiting for approval', CAST(N'2023-12-20T22:37:01.337' AS DateTime), 1)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (51, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 1005 được tạo mới', CAST(N'2023-12-20T22:37:44.587' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (51, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 1005 has been created', CAST(N'2023-12-20T22:37:44.587' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (52, 0, N'System', N'PM00000001', 0, N'Bài post mới', N'Bài post ID: 1006 được tạo mới', CAST(N'2023-12-20T23:44:26.483' AS DateTime), 0)
GO
INSERT [dbo].[Notification] ([id], [lang], [sendBy], [receiver], [type], [title], [content], [createDate], [isRead]) VALUES (52, 1, N'System', N'PM00000001', 0, N'New Post', N'Post ID: 1006 has been created', CAST(N'2023-12-20T23:44:26.483' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[ProfileImage] ON 
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1, N'SE00000002', N'OIP_20231219_212449.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (2, N'SE00000001', N'logo_20231219_212938.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (3, N'CU00000004', N'TrieuProfileImage_20231220_131737.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (4, N'SE00000005', N'photo-1546069901-ba9599a7e63c_20231220_204126.jpg', 1)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (5, N'SE00000009', N'download_20231220_210606.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (6, N'SE00000006', N'MocHolaQuan_20231220_212711.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (7, N'SE00000005', N'411153696_691573782954153_1491825160204149284_n_20231220_213656.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1004, N'SE00000008', N'DangNu_20231220_220022.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1005, N'SE00000007', N'NguyenHuyenDrink_20231220_222947.jpg', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1006, N'AD00000001', N'admin_20231220_232100.png', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1007, N'SH00000001', N'shipper1_20231220_232924.png', 1)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1008, N'SH00000001', N'shipper2_20231220_232949.png', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1009, N'MM00000001', N'menu-moderator_20231220_235619.png', 0)
GO
INSERT [dbo].[ProfileImage] ([imageId], [userId], [path], [isReplaced]) VALUES (1010, N'PM00000001', N'post-moderator_20231221_000527.png', 0)
GO
SET IDENTITY_INSERT [dbo].[ProfileImage] OFF
GO
