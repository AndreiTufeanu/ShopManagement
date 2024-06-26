USE [master]
GO
/****** Object:  Database [Shop]    Script Date: 12.05.2024 16:11:31 ******/
CREATE DATABASE [Shop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Shop', FILENAME = N'D:\SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Shop.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Shop_log', FILENAME = N'D:\SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Shop_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Shop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Shop] SET RECOVERY FULL 
GO
ALTER DATABASE [Shop] SET  MULTI_USER 
GO
ALTER DATABASE [Shop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Shop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Shop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Shop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Shop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Shop', N'ON'
GO
ALTER DATABASE [Shop] SET QUERY_STORE = ON
GO
ALTER DATABASE [Shop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Shop]
GO
/****** Object:  Table [dbo].[Barcode]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Barcode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[value] [varchar](50) NOT NULL,
	[producer_id] [int] NOT NULL,
	[product_type_id] [int] NOT NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Barcode_value] UNIQUE NONCLUSTERED 
(
	[value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_CategoryName] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offer]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[reason] [varchar](255) NOT NULL,
	[discount] [int] NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producer]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[country_of_origin] [varchar](255) NOT NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ProducerName] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [bit] NULL,
	[stock_id] [int] NOT NULL,
	[receipt_id] [int] NULL,
	[selling_price] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Stock]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Stock](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [int] NOT NULL,
	[supply_date] [date] NOT NULL,
	[expiration_date] [date] NULL,
	[price_per_unit] [float] NULL,
	[active] [bit] NULL,
	[barcode_id] [int] NOT NULL,
	[offer_id] [int] NULL,
	[selling_price_per_unit] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Type]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[unit] [varchar](20) NOT NULL,
	[active] [bit] NULL,
	[category_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ProductTypeName] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date_of_purchase] [date] NULL,
	[cashier_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[user_type] [nvarchar](20) NULL,
	[is_active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CHK_Username_Unique] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Barcode] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Offer] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Producer] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Product_Stock] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Product_Type] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Receipt] ADD  DEFAULT (getdate()) FOR [date_of_purchase]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ('cashier') FOR [user_type]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Barcode]  WITH CHECK ADD  CONSTRAINT [FK_Producer] FOREIGN KEY([producer_id])
REFERENCES [dbo].[Producer] ([id])
GO
ALTER TABLE [dbo].[Barcode] CHECK CONSTRAINT [FK_Producer]
GO
ALTER TABLE [dbo].[Barcode]  WITH CHECK ADD  CONSTRAINT [FK_ProductType] FOREIGN KEY([product_type_id])
REFERENCES [dbo].[Product_Type] ([id])
GO
ALTER TABLE [dbo].[Barcode] CHECK CONSTRAINT [FK_ProductType]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Receipt] FOREIGN KEY([receipt_id])
REFERENCES [dbo].[Receipt] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Receipt]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Stock] FOREIGN KEY([stock_id])
REFERENCES [dbo].[Product_Stock] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Stock]
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [FK_Barcode] FOREIGN KEY([barcode_id])
REFERENCES [dbo].[Barcode] ([id])
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [FK_Barcode]
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [FK_Offer] FOREIGN KEY([offer_id])
REFERENCES [dbo].[Offer] ([id])
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [FK_Offer]
GO
ALTER TABLE [dbo].[Product_Type]  WITH CHECK ADD  CONSTRAINT [FK_Category] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Product_Type] CHECK CONSTRAINT [FK_Category]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Cashier] FOREIGN KEY([cashier_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Cashier]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [CHK_EndDateAfterStartDate] CHECK  (([end_date]>[start_date]))
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [CHK_EndDateAfterStartDate]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD CHECK  (([discount]>=(0) AND [discount]<=(100)))
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [CHK_amount_positive] CHECK  (([amount]>(0)))
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [CHK_amount_positive]
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [CHK_ExpirationAfterSupply] CHECK  (([expiration_date]>[supply_date]))
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [CHK_ExpirationAfterSupply]
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [CHK_price_per_unit_positive] CHECK  (([price_per_unit]>(0)))
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [CHK_price_per_unit_positive]
GO
ALTER TABLE [dbo].[Product_Stock]  WITH CHECK ADD  CONSTRAINT [CHK_Selling_greater_than_Buying] CHECK  (([selling_price_per_unit]>=[price_per_unit]))
GO
ALTER TABLE [dbo].[Product_Stock] CHECK CONSTRAINT [CHK_Selling_greater_than_Buying]
GO
/****** Object:  StoredProcedure [dbo].[DeactivateBarcode]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateBarcode]
    @barcodeId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Barcode]
    SET active = 0
    WHERE id = @barcodeId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateCategory]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateCategory]
    @categoryId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Category]
    SET active = 0
    WHERE id = @categoryId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateProducer]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateProducer]
    @producerId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Producer]
    SET active = 0
    WHERE id = @producerId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateProduct]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateProduct]
    @productId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Product]
    SET active = 0
    WHERE id = @productId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateProductType]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateProductType]
    @productTypeId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Product_Type]
    SET active = 0
    WHERE id = @productTypeId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateStock]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateStock]
    @stockId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Product_Stock]
    SET active = 0
    WHERE id = @stockId;

	UPDATE [Product]
    SET active = 0
    WHERE stock_id = @stockId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeactivateUser]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateUser]
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [User]
    SET is_active = 0
    WHERE id = @userId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllBarcodesInfo]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBarcodesInfo]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT value, producer_id, product_type_id
    FROM [Barcode];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCategories]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT name
    FROM [Category];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducers]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducers]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT name, country_of_origin
    FROM [Producer];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProductTypes]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProductTypes]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT name, unit, category_id
    FROM [Product_Type];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT id, username, password, user_type, is_active
    FROM [User];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetBarcodesWithNames]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBarcodesWithNames]
AS
BEGIN
    SELECT 
        Barcode.value,
        Producer.name AS producer_name,
        Product_Type.name AS product_type_name
    FROM 
        [Barcode]
    JOIN 
        [Producer] ON Barcode.producer_id = Producer.id
    JOIN 
        [Product_Type] ON Barcode.product_type_id = Product_Type.id;
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertStock]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertStock]
    @amount INT,
    @supplyDate DATE,
    @expirationDate DATE,
    @pricePerUnit FLOAT,
    @barcodeId INT,
    @offerId INT
AS
BEGIN
	DECLARE @newSellingPricePerUnit FLOAT;
	SET @newSellingPricePerUnit = @pricePerUnit * 1.25;

	INSERT INTO dbo.[Product_Stock] (amount, supply_date, expiration_date, price_per_unit, selling_price_per_unit, barcode_id, offer_id)
    VALUES (@amount, @supplyDate, @expirationDate, @pricePerUnit, @newSellingPricePerUnit, @barcodeId, @offerId);
END;
GO
/****** Object:  StoredProcedure [dbo].[ModifyBarcodeData]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyBarcodeData]
    @barcodeID INT,
	@newBarcode NVARCHAR(50),
    @newProducerId INT,
	@newProductTypeId INT
AS
BEGIN
    UPDATE dbo.[Barcode]
    SET 
        value = @newBarcode,
		producer_id = @newProducerId,
		product_type_id = @newProductTypeId
    WHERE
        id = @barcodeID;
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyCategory]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyCategory]
    @categoryID INT,
    @newName NVARCHAR(50)
AS
BEGIN
    UPDATE dbo.[Category]
    SET 
        name = @newName
    WHERE
        id = @categoryID;
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyProducer]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProducer]
    @producerID INT,
    @newName NVARCHAR(50),
	@newCountryOfOrigin NVARCHAR(50)
AS
BEGIN
    UPDATE dbo.[Producer]
    SET 
        name = @newName,
		country_of_origin = @newCountryOfOrigin
    WHERE
        id = @producerID;
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyProduct]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProduct]
    @productID INT,
    @newPrice FLOAT
AS
BEGIN
    UPDATE p
    SET 
        p.selling_price = @newPrice
    FROM dbo.[Product] p
    WHERE
        p.id = @productID
        AND @newPrice > (SELECT s.price_per_unit FROM dbo.[Product_Stock] s WHERE s.id = p.stock_id);
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyProductType]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProductType]
    @productTypeID INT,
    @newName NVARCHAR(50),
	@newUnit NVARCHAR(50),
	@newCategoryId INT
AS
BEGIN
    UPDATE dbo.[Product_Type]
    SET 
        name = @newName,
		unit = @newUnit,
		category_id = @newCategoryId
    WHERE
        id = @productTypeID;
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyStock]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyStock]
    @stockID INT,
    @newAmount INT,
    @newSupplyDate DATETIME,
    @newExpirationDate DATE,
    @newPricePerUnit FLOAT,
    @newSellingPrincePerUnit FLOAT,
    @newBarcodeId INT,
    @newOfferId INT
AS
BEGIN
    DECLARE @TodayDate DATE = CAST(GETDATE() AS DATE); -- Cast to DATE

    UPDATE dbo.[Product_Stock]
    SET 
        amount = @newAmount,
        supply_date = @newSupplyDate,
        expiration_date = @newExpirationDate,
        barcode_id = @newBarcodeId,
        selling_price_per_unit = @newSellingPrincePerUnit,
        offer_id = @newOfferId,
        price_per_unit = CASE WHEN CAST(@newSupplyDate AS DATE) > @TodayDate THEN @newPricePerUnit ELSE price_per_unit END
    WHERE
        id = @stockID;
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyUserCredentials]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyUserCredentials] 
    @userID INT,
    @newUsername NVARCHAR(50),
    @newPassword NVARCHAR(50)
AS
BEGIN
    UPDATE dbo.[User]
    SET 
        username = @newUsername, 
        password = @newPassword
    WHERE
        id = @userID;
END
GO
/****** Object:  StoredProcedure [dbo].[SearchUser]    Script Date: 12.05.2024 16:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserID INT;
    DECLARE @UserType NVARCHAR(50);
    DECLARE @IsActive BIT;
    DECLARE @OutputPassword NVARCHAR(50);

    -- Check if username and password match
    SELECT TOP 1
        @UserID = id,
        @OutputPassword = [password],
        @UserType = user_type,
        @IsActive = is_active
    FROM [dbo].[User]
    WHERE username = @Username AND [password] = @Password;

    -- Return user information directly
    SELECT @UserID AS UserID,
           @OutputPassword AS OutputPassword,
           @UserType AS UserType,
           @IsActive AS IsActive;
END;
GO
USE [master]
GO
ALTER DATABASE [Shop] SET  READ_WRITE 
GO
