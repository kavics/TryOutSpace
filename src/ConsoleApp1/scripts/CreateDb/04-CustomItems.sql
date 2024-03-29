/* Creates a custom table with data and a custom stored procedure */
/* These elements need to be untouched during the whole upgrade project */
USE [SN7_Upgrade]
GO

/****** Object:  Table [dbo].[CustomTable1] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomTable1]') AND type in (N'U'))
DROP TABLE [dbo].[CustomTable1]
GO

/****** Object:  Table [dbo].[CustomTable1] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomTable1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomTable1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CustomTable1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/*******************************/

INSERT INTO [CustomTable1] ([Name], [Value]) VALUES ('Name1', 'Value1')
INSERT INTO [CustomTable1] ([Name], [Value]) VALUES ('Name2', 'Value2')
INSERT INTO [CustomTable1] ([Name], [Value]) VALUES ('Name3', 'Value3')
GO

/****** Object:  StoredProcedure [dbo].[proc_CustomProc1] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CustomProc1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_CustomProc1]
GO
/****** Object:  StoredProcedure [dbo].[proc_CustomProc1] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CustomProc1]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_CustomProc1]
AS
	SELECT * FROM [CustomTable1]
' 
END
GO
