USE [TestHelloWorld]
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSPXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
DROP PROCEDURE [dbo].[SP_TwoGetOneInsertSPXmlTran]
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSPXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
DROP PROCEDURE [dbo].[SP_TwoGetOneInsertSPXmlDecryptTran]
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
DROP PROCEDURE [dbo].[SP_TwoGetOneInsertSpTran]
GO
/****** Object:  StoredProcedure [dbo].[SP_OneInsertWithSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
DROP PROCEDURE [dbo].[SP_OneInsertWithSpTran]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Banks_BankId]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSPXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertSPXmlTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertSPXmlTran]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSPXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertSPXmlDecryptTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertSPXmlDecryptTran]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertSpTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertSpTran]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertDynamicSqlXmlTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertDynamicSqlXmlTran]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertDynamicSqlXmlDecryptTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertDynamicSqlXmlDecryptTran]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TwoGetOneInsertDynamicSqlTran]') AND type in (N'U'))
DROP TABLE [dbo].[TwoGetOneInsertDynamicSqlTran]
GO
/****** Object:  Table [dbo].[SampleBank2Info]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SampleBank2Info]') AND type in (N'U'))
DROP TABLE [dbo].[SampleBank2Info]
GO
/****** Object:  Table [dbo].[SampleBank1Info]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SampleBank1Info]') AND type in (N'U'))
DROP TABLE [dbo].[SampleBank1Info]
GO
/****** Object:  Table [dbo].[OneInsertWithSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OneInsertWithSpTran]') AND type in (N'U'))
DROP TABLE [dbo].[OneInsertWithSpTran]
GO
/****** Object:  Table [dbo].[OneInsertDynamicSqlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OneInsertDynamicSqlTran]') AND type in (N'U'))
DROP TABLE [dbo].[OneInsertDynamicSqlTran]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 11/26/2020 2:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
DROP TABLE [dbo].[Banks]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OneInsertDynamicSqlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OneInsertDynamicSqlTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_OneInsertDynamicSqlTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OneInsertWithSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OneInsertWithSpTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_OneInsertWithSpTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SampleBank1Info]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleBank1Info](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[Balance] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_SampleBank1Info] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SampleBank2Info]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleBank2Info](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[Balance] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_SampleBank2Info] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertDynamicSqlTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertDynamicSqlTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertDynamicSqlXmlDecryptTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertDynamicSqlXmlDecryptTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertDynamicSqlXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertDynamicSqlXmlTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertDynamicSqlXmlTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertSpTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertSpTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSPXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertSPXmlDecryptTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertSPXmlDecryptTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TwoGetOneInsertSPXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TwoGetOneInsertSPXmlTran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[SenderVid] [nvarchar](200) NULL,
	[SenderAccNo] [nvarchar](200) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](200) NULL,
	[ReceiverAccNo] [nvarchar](200) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TwoGetOneInsertSPXmlTran] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VID] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[BankId] [int] NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[Balance] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Banks] ON 

INSERT [dbo].[Banks] ([Id], [Name]) VALUES (1, N'SampleBank1')
INSERT [dbo].[Banks] ([Id], [Name]) VALUES (2, N'SampleBank2')
SET IDENTITY_INSERT [dbo].[Banks] OFF
GO
SET IDENTITY_INSERT [dbo].[SampleBank1Info] ON 

INSERT [dbo].[SampleBank1Info] ([Id], [AccountNo], [Balance]) VALUES (1, N'1111111111', CAST(10000000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[SampleBank1Info] OFF
GO
SET IDENTITY_INSERT [dbo].[SampleBank2Info] ON 

INSERT [dbo].[SampleBank2Info] ([Id], [AccountNo], [Balance]) VALUES (1, N'2222222222', CAST(10000000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[SampleBank2Info] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [VID], [Name], [BankId], [AccountNo], [Balance]) VALUES (1, N'arnab@user.idtp', N'Arnab', 1, N'1111111111', CAST(10000000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [VID], [Name], [BankId], [AccountNo], [Balance]) VALUES (3, N'nesar@user.idtp', N'Nesar', 2, N'2222222222', CAST(10000000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Banks_BankId] FOREIGN KEY([BankId])
REFERENCES [dbo].[Banks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Banks_BankId]
GO
/****** Object:  StoredProcedure [dbo].[SP_OneInsertWithSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- CALLING SP
/*
EXEC SP_OneInsertWithSpTran '123-235-2548','2020-11-25 14:10:20.2356482'
*/
-- =============================================

CREATE PROCEDURE [dbo].[SP_OneInsertWithSpTran]
	@TRANSACTION_ID NVARCHAR(200),
	@CLIENT_REQUEST_TIME DATETIME2(7)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		-- Save Transaction Data
		INSERT INTO [dbo].OneInsertWithSpTran ([TransactionId], [SenderVid],[SenderAccNo], [SenderBankId], [ReceicerVid], 
           [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime])
		VALUES(@TRANSACTION_ID, 'arnab@user.idtp', '1111111111', 1, 'nesar@user.idtp', 
			'2222222222', 2, 1.25, GETDATE(), @CLIENT_REQUEST_TIME)
		
		--SELECT 1 AS Status, 'Submit Successfully' as Message
	END TRY  
	BEGIN CATCH
		 DECLARE @ErrorMessage  NVARCHAR(4000);
		 SET @ErrorMessage = ERROR_MESSAGE()
		--SELECT 0 AS Status, @ErrorMessage AS Message
		RAISERROR( @ErrorMessage,16,1)
	END CATCH;
 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSpTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- CALLING SP
/*
EXEC SP_TwoGetOneInsertSpTran 'arnab@user.idtp', 'nesar@user.idtp', 1.25,'123-235-2548','2020-11-25 14:10:20.2356482'
*/
-- =============================================

CREATE PROCEDURE [dbo].[SP_TwoGetOneInsertSpTran]
	@SENDER_VID NVARCHAR(200),
	@RECEIVER_VID NVARCHAR(200),
	@AMOUNT DECIMAL(18,2),
	@TRANSACTION_ID NVARCHAR(200),
	@CLIENT_REQUEST_TIME DATETIME2(7)
AS
BEGIN
	
	--Variable declarations
	DECLARE @SENDER_BANK_ID INT
	DECLARE @RECEIVER_BANK_ID INT
	DECLARE @SENDER_ACCOUNT_NO NVARCHAR(200)
	DECLARE @RECEIVER_ACCOUNT_NO NVARCHAR(200)
	
	BEGIN TRY
		
		--- GET SENDER INFORMATION
		SELECT @SENDER_BANK_ID = [BankId], @SENDER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @SENDER_VID

		IF ISNULL(@SENDER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Sender',16,1)
		END

		--- GET RECEIVER INFORMATION
		SELECT @RECEIVER_BANK_ID = [BankId], @RECEIVER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @RECEIVER_VID

		IF ISNULL(@RECEIVER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Receiver',16,1)
		END

		-- Save Transaction Data
		INSERT INTO [dbo].[TwoGetOneInsertSpTran]([TransactionId], [SenderVid],[SenderAccNo], [SenderBankId], [ReceicerVid], 
           [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime])
		VALUES(@TRANSACTION_ID, @SENDER_VID, @SENDER_ACCOUNT_NO, @SENDER_BANK_ID, @RECEIVER_VID, 
			@RECEIVER_ACCOUNT_NO, @RECEIVER_BANK_ID, @AMOUNT, GETDATE(), @CLIENT_REQUEST_TIME)
		
		--SELECT 1 AS Status, 'Submit Successfully' as Message
	END TRY  
	BEGIN CATCH
		 DECLARE @ErrorMessage  NVARCHAR(4000);
		 SET @ErrorMessage = ERROR_MESSAGE()
		--SELECT 0 AS Status, @ErrorMessage AS Message
		RAISERROR( @ErrorMessage,16,1)
	END CATCH;
 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSPXmlDecryptTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- CALLING SP
/*
EXEC SP_TwoGetOneInsertSPXmlDecryptTran 'arnab@user.idtp', 'nesar@user.idtp', 1.25,'123-235-2548','2020-11-25 14:10:20.2356482'
*/
-- =============================================

CREATE PROCEDURE [dbo].[SP_TwoGetOneInsertSPXmlDecryptTran]
	@SENDER_VID NVARCHAR(200),
	@RECEIVER_VID NVARCHAR(200),
	@AMOUNT DECIMAL(18,2),
	@TRANSACTION_ID NVARCHAR(200),
	@CLIENT_REQUEST_TIME DATETIME2(7)
AS
BEGIN
	
	--Variable declarations
	DECLARE @SENDER_BANK_ID INT
	DECLARE @RECEIVER_BANK_ID INT
	DECLARE @SENDER_ACCOUNT_NO NVARCHAR(200)
	DECLARE @RECEIVER_ACCOUNT_NO NVARCHAR(200)
	
	BEGIN TRY
		
		--- GET SENDER INFORMATION
		SELECT @SENDER_BANK_ID = [BankId], @SENDER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @SENDER_VID

		IF ISNULL(@SENDER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Sender',16,1)
		END

		--- GET RECEIVER INFORMATION
		SELECT @RECEIVER_BANK_ID = [BankId], @RECEIVER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @RECEIVER_VID

		IF ISNULL(@RECEIVER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Receiver',16,1)
		END

		-- Save Transaction Data
		INSERT INTO [dbo].[TwoGetOneInsertSPXmlDecryptTran]([TransactionId], [SenderVid],[SenderAccNo], [SenderBankId], [ReceicerVid], 
           [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime])
		VALUES(@TRANSACTION_ID, @SENDER_VID, @SENDER_ACCOUNT_NO, @SENDER_BANK_ID, @RECEIVER_VID, 
			@RECEIVER_ACCOUNT_NO, @RECEIVER_BANK_ID, @AMOUNT, GETDATE(), @CLIENT_REQUEST_TIME)
		
		--SELECT 1 AS Status, 'Submit Successfully' as Message
	END TRY  
	BEGIN CATCH
		 DECLARE @ErrorMessage  NVARCHAR(4000);
		 SET @ErrorMessage = ERROR_MESSAGE()
		--SELECT 0 AS Status, @ErrorMessage AS Message
		RAISERROR( @ErrorMessage,16,1)
	END CATCH;
 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TwoGetOneInsertSPXmlTran]    Script Date: 11/26/2020 2:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- CALLING SP
/*
EXEC SP_TwoGetOneInsertSPXmlTran 'arnab@user.idtp', 'nesar@user.idtp', 1.25,'123-235-2548','2020-11-25 14:10:20.2356482'
*/
-- =============================================

CREATE PROCEDURE [dbo].[SP_TwoGetOneInsertSPXmlTran]
	@SENDER_VID NVARCHAR(200),
	@RECEIVER_VID NVARCHAR(200),
	@AMOUNT DECIMAL(18,2),
	@TRANSACTION_ID NVARCHAR(200),
	@CLIENT_REQUEST_TIME DATETIME2(7)
AS
BEGIN
	
	--Variable declarations
	DECLARE @SENDER_BANK_ID INT
	DECLARE @RECEIVER_BANK_ID INT
	DECLARE @SENDER_ACCOUNT_NO NVARCHAR(200)
	DECLARE @RECEIVER_ACCOUNT_NO NVARCHAR(200)
	
	BEGIN TRY
		
		--- GET SENDER INFORMATION
		SELECT @SENDER_BANK_ID = [BankId], @SENDER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @SENDER_VID

		IF ISNULL(@SENDER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Sender',16,1)
		END

		--- GET RECEIVER INFORMATION
		SELECT @RECEIVER_BANK_ID = [BankId], @RECEIVER_ACCOUNT_NO = [AccountNo] FROM [dbo].[Users] WHERE [VID] = @RECEIVER_VID

		IF ISNULL(@RECEIVER_BANK_ID,0) = 0
		BEGIN
			RAISERROR('Invalid Receiver',16,1)
		END

		-- Save Transaction Data
		INSERT INTO [dbo].[TwoGetOneInsertSPXmlTran]([TransactionId], [SenderVid],[SenderAccNo], [SenderBankId], [ReceicerVid], 
           [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime])
		VALUES(@TRANSACTION_ID, @SENDER_VID, @SENDER_ACCOUNT_NO, @SENDER_BANK_ID, @RECEIVER_VID, 
			@RECEIVER_ACCOUNT_NO, @RECEIVER_BANK_ID, @AMOUNT, GETDATE(), @CLIENT_REQUEST_TIME)
		
		--SELECT 1 AS Status, 'Submit Successfully' as Message
	END TRY  
	BEGIN CATCH
		 DECLARE @ErrorMessage  NVARCHAR(4000);
		 SET @ErrorMessage = ERROR_MESSAGE()
		--SELECT 0 AS Status, @ErrorMessage AS Message
		RAISERROR( @ErrorMessage,16,1)
	END CATCH;
 
END
GO
