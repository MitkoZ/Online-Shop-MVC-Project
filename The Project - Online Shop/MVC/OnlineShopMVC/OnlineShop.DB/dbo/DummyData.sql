/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
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
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (23, 1, N'HP', N'intel i5', 10, 1000, N'Windows 7', 400.0000, N'636309978715991988_714MNzSRCGL._SL1500_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (24, 1, N'Acer', N'Celeron', 4, 700, N'Windows XP', 250.0000, N'636309979516751988_31i27XUCoXL.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (25, 1, N'Dell', N'i5', 8, 1000, N'Windows 7', 800.0000, N'636309980264401988_510Tjy95U4L.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (26, 2, N'Lenovo', N'Intel i7', 8, 1200, N'Windows 10', 1200.0000, N'636309981170221988_81nfm7ua6gL._SL1500_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (27, 2, N'Asus', N'i7', 16, 1000, N'Windows 7', 1500.0000, N'636309981933241988_71wrqvp23oL._SL1200_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (28, 2, N'Lenovo', N'Celeron Dual-Core', 4, 1000, N'Windows 7', 470.0000, N'636309982788191988_61b24JoXBjL._SL1000_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (29, 3, N'Samsung Galaxy Note 5', N'Quad Core', 4, 32, N'Android', 800.0000, N'636309983796861988_81eGyuGI7zL._SL1500_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (30, 3, N'ZTE', N'Dual Core', 2, 64, N'Android', 760.0000, N'636309984471041988_91DBPouuFkL._SL1500_.jpg')
INSERT [dbo].[Products] ([ID], [CategoryID], [Name], [Processor], [RAM], [Storage], [OS], [Price], [ImageName]) VALUES (31, 3, N'Apple iPhone 6', N'Quad Core', 4, 64, N'IOS 8', 2000.0000, N'636309985706261988_81N9jWwWzCL._SL1500_.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([ID], [Name]) VALUES (2, N'Sofia')
INSERT [dbo].[Cities] ([ID], [Name]) VALUES (1, N'Veliko Tarnovo')
SET IDENTITY_INSERT [dbo].[Cities] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (1, N'admin', N'83798404B35689663F733BB191ACC48265780E13C0192ABDFD7E7575751C4C95', N'856cd764-eed8-4a3a-a8ee-46d375d892fd', N'admin', N'admin@abv.bg', 2, N'admin', N'378282246310005', 1)
INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (2, N'baiivan', N'98555914BE27C2F9759F582A6AAADA4086360580B7CDB0A4431EB6882305AA64', N'85545e1d-027b-4c65-a54d-a804477b0607', N'Ivan', N'baiivan@abv.bg', 2, N'nqkade si', N'38520000023237', 0)
INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (3, N'gosho', N'930B748387B3FB7AD65DC0DD02DE34820F82E901E0E0FFE2D42E4A2D25449004', N'17784433-def1-4cf5-938d-386f9f52b355', N'Gosho', N'gosho@abv.bg', 1, N'some address', N'5402825491566348', 0)
INSERT [dbo].[Users] ([ID], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CityID], [Address], [CardNumber], [IsAdmin]) VALUES (4, N'Asen1', N'7B59491F5B1AF3BE5C75A5C705C919DF2B90C75E77117E796DD252F54D2ADA88', N'a606a713-e0aa-4b29-8de1-c19cb83a8bf7', N'asen', N'asen@gmail.com', 2, N'nqkav adres', N'5274586091070538', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (10, 17, 2, CAST(N'2017-05-14 01:36:04.617' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (11, 21, 2, CAST(N'2017-05-14 01:36:04.650' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (12, 22, 2, CAST(N'2017-05-14 01:36:04.653' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (1012, 19, 3, CAST(N'2017-05-14 23:40:42.783' AS DateTime))
INSERT [dbo].[Sales] ([ID], [ProductID], [UserID], [DateBought]) VALUES (1013, 18, 3, CAST(N'2017-05-14 23:40:42.840' AS DateTime))
SET IDENTITY_INSERT [dbo].[Sales] OFF
SET IDENTITY_INSERT [dbo].[Smartphones] ON 

INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (8, 19, N'20MP', N'Nano sim')
INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (9, 22, N'30MP', N'Nano')
INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (10, 29, N'20MP', N'Nano-sim')
INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (11, 30, N'12MP', N'Nano sim')
INSERT [dbo].[Smartphones] ([ID], [ProductID], [Camera], [SIMCardType]) VALUES (12, 31, N'30MP', N'Nano sim')
SET IDENTITY_INSERT [dbo].[Smartphones] OFF
SET IDENTITY_INSERT [dbo].[PCs] ON 

INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (6, 17, N'5GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (7, 18, N'10GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (8, 20, N'2gb')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (9, 21, N'3gb')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (10, 23, N'2GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (11, 24, N'1GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (12, 25, N'4GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (13, 26, N'6GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (14, 27, N'GTX 8GB')
INSERT [dbo].[PCs] ([ID], [ProductID], [VideoCard]) VALUES (15, 28, N'2GB')
SET IDENTITY_INSERT [dbo].[PCs] OFF
