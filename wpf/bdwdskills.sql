USE [master]
GO
/****** Object:  Database [Trade]    Script Date: 11.09.2022 21:00:23 ******/
CREATE DATABASE [Trade]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Trade', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.FERBRAY\MSSQL\DATA\Trade.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Trade_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.FERBRAY\MSSQL\DATA\Trade_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Trade] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Trade].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Trade] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Trade] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Trade] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Trade] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Trade] SET ARITHABORT OFF 
GO
ALTER DATABASE [Trade] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Trade] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Trade] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Trade] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Trade] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Trade] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Trade] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Trade] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Trade] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Trade] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Trade] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Trade] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Trade] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Trade] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Trade] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Trade] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Trade] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Trade] SET RECOVERY FULL 
GO
ALTER DATABASE [Trade] SET  MULTI_USER 
GO
ALTER DATABASE [Trade] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Trade] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Trade] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Trade] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Trade] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Trade] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Trade', N'ON'
GO
ALTER DATABASE [Trade] SET QUERY_STORE = OFF
GO
USE [Trade]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderStatus] [nvarchar](max) NOT NULL,
	[OrderDeliveryDate] [datetime] NOT NULL,
	[PointId] [int] NOT NULL,
	[OrderCodeGet] [int] NOT NULL,
	[OrderSurnameClient] [nvarchar](max) NULL,
	[OrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Order__C3905BAF133FB807] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProduct]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProduct](
	[OrderProductId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductArticleNumber] [nvarchar](256) NOT NULL,
	[ProductCount] [int] NOT NULL,
 CONSTRAINT [PK_OrderProduct_1] PRIMARY KEY CLUSTERED 
(
	[OrderProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Point]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[PointId] [int] IDENTITY(1,1) NOT NULL,
	[PointName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductArticleNumber] [nvarchar](256) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductUnits] [nvarchar](max) NOT NULL,
	[ProductCost] [decimal](18, 4) NOT NULL,
	[ProductMaxDiscount] [int] NOT NULL,
	[ProductManufacture] [nvarchar](max) NOT NULL,
	[ProductProvider] [nvarchar](max) NOT NULL,
	[ProductCategory] [nvarchar](max) NOT NULL,
	[ProductDiscount] [int] NOT NULL,
	[ProductStockRoom] [int] NOT NULL,
	[ProductDescription] [nvarchar](max) NOT NULL,
	[ProductImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductArticleNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Role__8AFACE3A8BEF3DC7] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11.09.2022 21:00:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserSurname] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[UserPatronymic] [nvarchar](max) NOT NULL,
	[UserLogin] [nvarchar](max) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK__User__1788CCACC05416BF] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (2, N'Завершен
', CAST(N'2022-05-22T00:00:00.000' AS DateTime), 1, 801, N'Ситникова
', CAST(N'2022-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (3, N'Новый 
', CAST(N'2022-05-22T00:00:00.000' AS DateTime), 14, 802, NULL, CAST(N'2022-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (4, N'Новый 
', CAST(N'2022-05-23T00:00:00.000' AS DateTime), 2, 803, N'Воронцова', CAST(N'2022-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (5, N'Новый 
', CAST(N'2022-05-23T00:00:00.000' AS DateTime), 22, 804, NULL, CAST(N'2022-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (6, N'Новый 
', CAST(N'2022-05-25T00:00:00.000' AS DateTime), 2, 805, N'Егоров', CAST(N'2022-05-19T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (7, N'Новый 
', CAST(N'2022-05-26T00:00:00.000' AS DateTime), 28, 806, NULL, CAST(N'2022-05-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (9, N'Новый 
', CAST(N'2022-05-28T00:00:00.000' AS DateTime), 3, 807, NULL, CAST(N'2022-05-22T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (11, N'Новый 
', CAST(N'2022-05-28T00:00:00.000' AS DateTime), 32, 808, NULL, CAST(N'2022-05-22T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (12, N'Новый 
', CAST(N'2022-05-30T00:00:00.000' AS DateTime), 5, 809, NULL, CAST(N'2022-05-24T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [OrderStatus], [OrderDeliveryDate], [PointId], [OrderCodeGet], [OrderSurnameClient], [OrderDate]) VALUES (13, N'Новый 
', CAST(N'2022-05-30T00:00:00.000' AS DateTime), 36, 810, N'Софронов', CAST(N'2022-05-24T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProduct] ON 

INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (1, 2, N'А112Т4', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (2, 2, N'T793T4', 3)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (3, 3, N'G387Y6', 16)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (4, 3, N'F573T5', 10)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (5, 4, N'D735T5', 20)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (6, 4, N'B736H6', 20)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (7, 5, N'H384H3', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (8, 5, N'K437E6', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (9, 6, N'E732R7', 4)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (10, 6, N'R836H6', 3)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (11, 7, N'F839R6', 4)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (12, 7, N'G304H6', 4)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (13, 9, N'C430T4', 10)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (14, 9, N'C946Y6', 3)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (15, 11, N'V403G6', 5)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (16, 11, N'B963H5', 5)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (17, 12, N'V026J4', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (18, 12, N'V727Y6', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (19, 13, N'C635Y6', 2)
INSERT [dbo].[OrderProduct] ([OrderProductId], [OrderId], [ProductArticleNumber], [ProductCount]) VALUES (20, 13, N'W405G6', 2)
SET IDENTITY_INSERT [dbo].[OrderProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[Point] ON 

INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (1, N'344288, г. Талнах, ул. Чехова, 1
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (2, N'614164, г.Талнах,  ул. Степная, 30
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (3, N'394242, г. Талнах, ул. Коммунистическая, 43
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (4, N'660540, г. Талнах, ул. Солнечная, 25
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (5, N'125837, г. Талнах, ул. Шоссейная, 40
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (6, N'125703, г. Талнах, ул. Партизанская, 49
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (7, N'625283, г. Талнах, ул. Победы, 46
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (8, N'614611, г. Талнах, ул. Молодежная, 50
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (9, N'454311, г.Талнах, ул. Новая, 19
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (10, N'660007, г.Талнах, ул. Октябрьская, 19
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (11, N'603036, г. Талнах, ул. Садовая, 4
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (12, N'450983, г.Талнах, ул. Комсомольская, 26
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (13, N'394782, г. Талнах, ул. Чехова, 3
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (14, N'603002, г. Талнах, ул. Дзержинского, 28
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (15, N'450558, г. Талнах, ул. Набережная, 30
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (16, N'394060, г.Талнах, ул. Фрунзе, 43
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (17, N'410661, г. Талнах, ул. Школьная, 50
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (18, N'625590, г. Талнах, ул. Коммунистическая, 20
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (19, N'625683, г. Талнах, ул. 8 Марта
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (20, N'400562, г. Талнах, ул. Зеленая, 32
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (21, N'614510, г. Талнах, ул. Маяковского, 47
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (22, N'410542, г. Талнах, ул. Светлая, 46
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (23, N'620839, г. Талнах, ул. Цветочная, 8
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (24, N'443890, г. Талнах, ул. Коммунистическая, 1
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (25, N'603379, г. Талнах, ул. Спортивная, 46
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (26, N'603721, г. Талнах, ул. Гоголя, 41
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (27, N'410172, г. Талнах, ул. Северная, 13
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (28, N'420151, г. Талнах, ул. Вишневая, 32
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (29, N'125061, г. Талнах, ул. Подгорная, 8
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (30, N'630370, г. Талнах, ул. Шоссейная, 24
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (31, N'614753, г. Талнах, ул. Полевая, 35
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (32, N'426030, г. Талнах, ул. Маяковского, 44
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (33, N'450375, г. Талнах ул. Клубная, 44
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (34, N'625560, г. Талнах, ул. Некрасова, 12
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (35, N'630201, г. Талнах, ул. Комсомольская, 17
')
INSERT [dbo].[Point] ([PointId], [PointName]) VALUES (36, N'190949, г. Талнах, ул. Мичурина, 26
')
SET IDENTITY_INSERT [dbo].[Point] OFF
GO
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'B736H6', N'Набор вилок', N'шт.', CAST(1600.0000 AS Decimal(18, 4)), 30, N'Davinci', N'Максидом', N'Вилки', 2, 6, N'Набор столовых вилок Davinci, 20 см 6 шт.', N'A112T4.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'B963H5', N'Набор ложек', N'шт.', CAST(250.0000 AS Decimal(18, 4)), 10, N'Attribute', N'LeroiMerlin', N'Ложки', 3, 16, N'Набор столовых ложек Baguette 3 предмета серебристый', N'T793T4.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'C430T4', N'Ложка столовая', N'шт.', CAST(441.0000 AS Decimal(18, 4)), 5, N'Doria', N'Максидом', N'Ложки', 4, 23, N'Ложка столовая DORIA L=195/60 мм Eternum', N'G387Y6.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'C635Y6', N'Вилки столовые', N'шт.', CAST(650.0000 AS Decimal(18, 4)), 15, N'Davinci', N'Максидом', N'Вилки', 3, 4, N'Вилки столовые на блистере / 6 шт.', N'F573T5.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'C730R7', N'Ложка чайная', N'шт.', CAST(220.0000 AS Decimal(18, 4)), 5, N'Alaska', N'LeroiMerlin', N'Ложки', 2, 13, N'Ложка чайная ALASKA Eternum', N'D735T5.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'C943G5', N'Вилка столовая', N'шт.', CAST(220.0000 AS Decimal(18, 4)), 5, N'Alaska', N'LeroiMerlin', N'Вилки', 3, 4, N'Вилка столовая 21,2 см «Аляска бэйсик» сталь KunstWerk', N'B736H6.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'C946Y6', N'Набор столовых приборов', N'шт.', CAST(600.0000 AS Decimal(18, 4)), 15, N'Apollo', N'Максидом', N'Наборы', 2, 9, N'Набор столовых приборов для торта Palette 7 предметов серебристый', N'H384H3.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'D735T5', N'Набор вилок', N'шт.', CAST(530.0000 AS Decimal(18, 4)), 5, N'Apollo', N'Максидом', N'Наборы', 3, 16, N'Набор вилок столовых APOLLO "Aurora" 3шт.', N'K437E6.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'E732R7', N'Набор столовых приборов', N'шт.', CAST(990.0000 AS Decimal(18, 4)), 15, N'Smart Home', N'Максидом', N'Наборы', 5, 6, N'Набор столовых приборов Smart Home20 Black в подарочной коробке, 4 шт', N'E732R7.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'F392G6', N'Набор  столовых ножей', N'шт.', CAST(250.0000 AS Decimal(18, 4)), 5, N'Attribute', N'LeroiMerlin', N'Ножи', 3, 16, N'Attribute Набор столовых ножей Baguette 2 предмета серебристый', N'R836H6.jpg')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'F573T5', N'Набор столовых приборов', N'шт.', CAST(490.0000 AS Decimal(18, 4)), 10, N'Apollo', N'LeroiMerlin', N'Наборы', 4, 9, N'Apollo Набор столовых приборов Chicago 4 предмета серебристый', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'F745K4', N'Ложка детская', N'шт.', CAST(300.0000 AS Decimal(18, 4)), 5, N'Smart Home', N'LeroiMerlin', N'Ложки', 3, 17, N'Ложка детская столовая', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'F839R6', N'Ложка чайная', N'шт.', CAST(400.0000 AS Decimal(18, 4)), 15, N'Doria', N'Максидом', N'Ложки', 2, 6, N'Ложка чайная DORIA Eternum', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'G304H6', N'Набор ложек', N'шт.', CAST(500.0000 AS Decimal(18, 4)), 5, N'Apollo', N'Максидом', N'Ложки', 4, 12, N'Набор ложек столовых APOLLO "Bohemica" 3 пр.', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'G387Y6', N'Набор на одну персону', N'шт.', CAST(1600.0000 AS Decimal(18, 4)), 30, N'Attribute', N'LeroiMerlin', N'Наборы', 3, 6, N'Набор на одну персону (4 предмета) серия "Bistro", нерж. сталь, Was, Германия.', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'H384H3', N'Вилка столовая', N'шт.', CAST(300.0000 AS Decimal(18, 4)), 15, N'Apollo', N'LeroiMerlin', N'Вилки', 2, 16, N'Вилка детская столовая', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'H495H6', N'Набор чайных ложек', N'шт.', CAST(200.0000 AS Decimal(18, 4)), 5, N'Attribute', N'Максидом', N'Наборы', 4, 12, N'Attribute Набор чайных ложек Baguette 3 предмета серебристый', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'K437E6', N'Набор чайных ложек', N'шт.', CAST(170.0000 AS Decimal(18, 4)), 5, N'Attribute', N'LeroiMerlin', N'Наборы', 3, 4, N'Attribute Набор чайных ложек Chaplet 3 предмета серебристый', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'L593H5', N'Нож для стейка', N'шт.', CAST(600.0000 AS Decimal(18, 4)), 10, N'Apollo', N'LeroiMerlin', N'Ножи', 4, 15, N'Нож для стейка 11,5 см серебристый/черный', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'N493G6', N'Ложка чайная', N'шт.', CAST(600.0000 AS Decimal(18, 4)), 15, N'Doria', N'Максидом', N'Ложки', 5, 24, N'Ложка чайная DORIA Eternum', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'R836H6', N'Ложка', N'шт.', CAST(800.0000 AS Decimal(18, 4)), 5, N'Smart Home', N'LeroiMerlin', N'Ложки', 3, 8, N'Ложка 21 мм металлическая (медная) (Упаковка 10 шт)', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'S394J5', N'Набор для серверовки', N'шт.', CAST(2550.0000 AS Decimal(18, 4)), 15, N'Smart Home', N'LeroiMerlin', N'Наборы', 4, 6, N'Набор для сервировки сыра Select', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'S395B5', N'набор ножей', N'шт.', CAST(1300.0000 AS Decimal(18, 4)), 25, N'Mayer & Boch', N'Максидом', N'Наборы', 5, 14, N'Набор ножей Mayer & Boch, 4 шт', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'T793T4', N'набор ножей', N'шт.', CAST(700.0000 AS Decimal(18, 4)), 15, N'Apollo', N'Максидом', N'Наборы', 3, 9, N'абор ножей для стейка и пиццы Swiss classic 2 шт. желтый', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'V026J4', N'Набор десертных ложек', N'шт.', CAST(3000.0000 AS Decimal(18, 4)), 10, N'Mayer & Boch', N'LeroiMerlin', N'Ложки', 4, 8, N'Набор десертных ложек на подставке Размер: 7*7*15 см', N'picture.png')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'V403G6', N'Набор стейковых ножей', N'шт.', CAST(7000.0000 AS Decimal(18, 4)), 15, N'Mayer & Boch', N'LeroiMerlin', N'Ножи', 2, 15, N'Набор стейковых ножей 4 пр. в деревянной коробке', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'V727Y6', N'Столовые приборы для салата', N'шт.', CAST(2000.0000 AS Decimal(18, 4)), 10, N'Mayer & Boch', N'LeroiMerlin', N'Наборы', 3, 2, N'Столовые приборы для салата Orskov Lava, 2шт
', N'picture.png')
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'W295Y5', N'Детский столовый набор', N'шт.', CAST(1000.0000 AS Decimal(18, 4)), 15, N'Apollo', N'Максидом', N'Наборы', 4, 25, N'Детский столовый набор Fissman «Зебра»
', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'W405G6', N'Набор столовых приборов', N'шт.', CAST(1300.0000 AS Decimal(18, 4)), 25, N'Attribute', N'LeroiMerlin', N'Наборы', 3, 4, N'Набор сервировочных столовых вилок Цветы', NULL)
INSERT [dbo].[Product] ([ProductArticleNumber], [ProductName], [ProductUnits], [ProductCost], [ProductMaxDiscount], [ProductManufacture], [ProductProvider], [ProductCategory], [ProductDiscount], [ProductStockRoom], [ProductDescription], [ProductImage]) VALUES (N'А112Т4', N'Сервировочный набор для торта', N'шт.', CAST(1100.0000 AS Decimal(18, 4)), 15, N'Attribute', N'Максидом', N'Наборы', 2, 16, N'Набор сервировочный для торта "Розанна"', NULL)
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Клиент')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Менеджер')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'Администратор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (5, N'Савицкая', N'Арина', N'Саввична', N'1y3lj8w0viop@outlook.com', N'2L6KZG', 3)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (6, N'Мартынов', N'Максим', N'Михайлович', N'3o698uk5c0rw@tutanota.com', N'uzWC67', 3)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (7, N'Зубов', N'Александр', N'Дамирович', N'lx24fvrt1aip@yahoo.com', N'8ntwUp', 3)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (8, N'Попов', N'Даниил', N'Олегович', N's8z076vku95j@gmail.com', N'YOyhfR', 2)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (9, N'Семенов', N'Михаил', N'Никитич', N'n3bxm7f2q5i4@outlook.com', N'RSbvHv', 2)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (10, N'Дмитриева', N'Надежда', N'Вячеславовна', N'r7dztn8a5ihq@mail.com', N'rwVDh9', 2)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (12, N'Воронцова', N'Виктория', N'Саввична', N'vofgck2m39hq@gmail.com', N'LdNyos', 1)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (22, N'Софронов', N'Ярослав', N'Игоревич', N's6q9tevybado@yahoo.com', N'gynQMT', 1)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (24, N'Егоров', N'Артём', N'Евгеньевич', N'p7wuls3dtq9v@yahoo.com', N'AtnDjr', 1)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (25, N'Ситникова', N'Эмилия', N'Степановна', N'rdn04s1u2ckb@mail.com', N'JlFRCZ', 1)
INSERT [dbo].[User] ([UserId], [UserSurname], [UserName], [UserPatronymic], [UserLogin], [UserPassword], [RoleId]) VALUES (30, N'123123', N'123123', N'123123', N'123123', N'123123', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Point] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Point] ([PointId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Point]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Order]
GO
ALTER TABLE [dbo].[Point]  WITH CHECK ADD  CONSTRAINT [FK_Point_Point] FOREIGN KEY([PointId])
REFERENCES [dbo].[Point] ([PointId])
GO
ALTER TABLE [dbo].[Point] CHECK CONSTRAINT [FK_Point_Point]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__UserRole__267ABA7A] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__UserRole__267ABA7A]
GO
USE [master]
GO
ALTER DATABASE [Trade] SET  READ_WRITE 
GO
