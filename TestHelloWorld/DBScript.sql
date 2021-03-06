create database test
Go
USE [TestHelloWorld]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 11/25/2020 1:43:19 PM ******/
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
/****** Object:  Table [dbo].[SampleBank1Info]    Script Date: 11/25/2020 1:43:20 PM ******/
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
/****** Object:  Table [dbo].[SampleBank2Info]    Script Date: 11/25/2020 1:43:20 PM ******/
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
/****** Object:  Table [dbo].[Transactions]    Script Date: 11/25/2020 1:43:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](max) NULL,
	[SenderVid] [nvarchar](max) NULL,
	[SenderAccNo] [nvarchar](max) NULL,
	[SenderBankId] [int] NOT NULL,
	[ReceicerVid] [nvarchar](max) NULL,
	[ReceiverAccNo] [nvarchar](max) NULL,
	[ReceicerBankId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TranDate] [datetime2](7) NOT NULL,
	[ClientRequestTime] [nvarchar](max) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/25/2020 1:43:20 PM ******/
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
SET IDENTITY_INSERT [dbo].[SampleBank1Info] ON 

INSERT [dbo].[SampleBank1Info] ([Id], [AccountNo], [Balance]) VALUES (1, N'12345', CAST(1200.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[SampleBank1Info] OFF
SET IDENTITY_INSERT [dbo].[SampleBank2Info] ON 

INSERT [dbo].[SampleBank2Info] ([Id], [AccountNo], [Balance]) VALUES (1, N'12345', CAST(2800.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[SampleBank2Info] OFF
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Id], [TransactionId], [SenderVid], [SenderAccNo], [SenderBankId], [ReceicerVid], [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime]) VALUES (7, N'123', N'', N'12345', 1, N'', N'12345', 2, CAST(25.00 AS Decimal(18, 2)), CAST(N'2020-11-25T00:40:59.0000000' AS DateTime2), N'123')
INSERT [dbo].[Transactions] ([Id], [TransactionId], [SenderVid], [SenderAccNo], [SenderBankId], [ReceicerVid], [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime]) VALUES (8, N'123', N'', N'12345', 1, N'', N'12345', 2, CAST(25.00 AS Decimal(18, 2)), CAST(N'2020-11-25T00:40:59.0000000' AS DateTime2), N'123')
INSERT [dbo].[Transactions] ([Id], [TransactionId], [SenderVid], [SenderAccNo], [SenderBankId], [ReceicerVid], [ReceiverAccNo], [ReceicerBankId], [Amount], [TranDate], [ClientRequestTime]) VALUES (9, N'222', N'', N'12345', 1, N'', N'12345', 2, CAST(25.00 AS Decimal(18, 2)), CAST(N'2020-11-25T00:43:31.0000000' AS DateTime2), N'222')
SET IDENTITY_INSERT [dbo].[Transactions] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [VID], [Name], [BankId], [AccountNo], [Balance]) VALUES (1, N'arnab@user.idtp', N'Arnab', 1, N'12345', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [VID], [Name], [BankId], [AccountNo], [Balance]) VALUES (2, N'nesar@user.idtp', N'Nesar', 2, N'12345', CAST(2000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Banks_BankId] FOREIGN KEY([BankId])
REFERENCES [dbo].[Banks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Banks_BankId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'USP_DIRECT_PAY') AND type in (N'P', N'PC'))
DROP PROCEDURE USP_DIRECT_PAY
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- CALLING SP
/*
EXEC USP_DIRECT_PAY 'arnab@user.idtp', 'nesar@user.idtp', 1.25,'123-235-2548','2020-11-25 14:10:20.2356482'
*/
-- =============================================

CREATE PROCEDURE USP_DIRECT_PAY 
	@SENDER_VID NVARCHAR(200),
	@RECEIVER_VID NVARCHAR(200),
	@AMOUNT DECIMAL(18,2),
	@TRANSACTION_ID NVARCHAR(200),
	@CLIENT_REQUEST_TIME DATETIME2(7)
AS
BEGIN
	SET NOCOUNT ON;

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
		INSERT INTO [dbo].[Transactions]([TransactionId], [SenderVid],[SenderAccNo], [SenderBankId], [ReceicerVid], 
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
