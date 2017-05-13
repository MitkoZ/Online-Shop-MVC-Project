USE [OnlineShop]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Laptops')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'PCs')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Smartphones')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (17, 1, N'Asus', N'i7', 12, 800, N'Windows 7', 800.0000, N'636303194081260703_asus.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (18, 2, N'LG', N'i5', 8, 1300, N'Windows 10', 23000.0000, N'636303194764359774_LG.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (19, 3, N'Iphone 7', N'Octa core', 4, 64, N'IOS', 1200.0000, N'636303195293120017_iphone 7.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (20, 1, N'Dell', N'i3', 4, 350, N'Dos', 300.0000, N'636303210402934249_dell.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (21, 2, N'Alienware', N'i5', 4, 500, N'Windows 8.1', 4000.0000, N'636303211680307311_alienware.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (22, 3, N'Samsung Galaxy S8', N'Quadra Core', 4, 256, N'Android', 2500.0000, N'636303212509834757_samsung galaxy s8.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([ID], [Name]) VALUES (2, N'Sofia')
INSERT [dbo].[Cities] ([ID], [Name]) VALUES (1, N'Veliko Tarnovo')
SET IDENTITY_INSERT [dbo].[Cities] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (1, N'admin', N'83798404B35689663F733BB191ACC48265780E13C0192ABDFD7E7575751C4C95', N'856cd764-eed8-4a3a-a8ee-46d375d892fd', N'admin', N'admin@abv.bg', 2, N'admin', N'378282246310005', 1)
INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (2, N'baiivan', N'98555914BE27C2F9759F582A6AAADA4086360580B7CDB0A4431EB6882305AA64', N'85545e1d-027b-4c65-a54d-a804477b0607', N'Ivan', N'baiivan@abv.bg', 2, N'nqkade si', N'38520000023237', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (10, 17, 2, CAST(N'2017-05-14 01:36:04.617' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (11, 21, 2, CAST(N'2017-05-14 01:36:04.650' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (12, 22, 2, CAST(N'2017-05-14 01:36:04.653' AS DateTime))
SET IDENTITY_INSERT [dbo].[Sales] OFF
SET IDENTITY_INSERT [dbo].[Smartphones] ON 

INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (8, 19, N'20MP', N'Nano sim')
INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (9, 22, N'30MP', N'Nano')
SET IDENTITY_INSERT [dbo].[Smartphones] OFF
SET IDENTITY_INSERT [dbo].[PCs] ON 

INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (6, 17, N'5GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (7, 18, N'10GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (8, 20, N'2gb')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (9, 21, N'3gb')
SET IDENTITY_INSERT [dbo].[PCs] OFF
