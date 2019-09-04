USE [SN7_Upgrade]
GO
------------------------------------------------                    --------------------------------------------------------------
------------------------------------------------ DROP CONSTRAINTS   --------------------------------------------------------------
------------------------------------------------                    --------------------------------------------------------------

/****** Object:  ForeignKey [FK_BinaryProperties_PropertyTypes]    Script Date: 10/25/2007 15:49:18 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BinaryProperties_PropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[BinaryProperties]'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_PropertyTypes]
GO
/****** Object:  ForeignKey [FK_Nodes_NodeTypes]    Script Date: 10/25/2007 15:50:17 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_NodeTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_NodeTypes]
GO
/****** Object:  ForeignKey [FK_ReferenceProperties_PropertyTypes]    Script Date: 10/25/2007 15:50:19 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ReferenceProperties_PropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[ReferenceProperties]'))
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [FK_ReferenceProperties_PropertyTypes]
GO
/****** Object:  ForeignKey [FK_NodeTypes_NodeTypes]    Script Date: 10/25/2007 15:50:23 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodeTypes_NodeTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodeTypes]'))
ALTER TABLE [dbo].[NodeTypes] DROP CONSTRAINT [FK_NodeTypes_NodeTypes]
GO
/****** Object:  ForeignKey [FK_LongTextProperties_PropertyTypes]    Script Date: 10/25/2007 15:50:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LongTextProperties_PropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[LongTextProperties]'))
ALTER TABLE [dbo].[LongTextProperties] DROP CONSTRAINT [FK_LongTextProperties_PropertyTypes]
GO

------------------------------------------------                    --------------------------------------------------------------
------------------------------------------------ DROP Tables, Views --------------------------------------------------------------
------------------------------------------------                    --------------------------------------------------------------

/****** Object:  Table [dbo].[PropertyTypes]    Script Date: 10/25/2007 15:50:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyTypes]') AND type in (N'U'))
DROP TABLE [dbo].[PropertyTypes]
GO
/****** Object:  Table [dbo].[NodeTypes]    Script Date: 10/25/2007 15:50:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodeTypes]') AND type in (N'U'))
DROP TABLE [dbo].[NodeTypes]
GO
/****** Object:  Table [dbo].[ContentListTypes]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentListTypes]') AND type in (N'U'))
DROP TABLE [dbo].[ContentListTypes]
GO

------------------------------------------------                      --------------------------------------------------------------
------------------------------------------------ CREATE Tables, Views --------------------------------------------------------------
------------------------------------------------                      --------------------------------------------------------------

/****** Object:  Table [dbo].[PropertyTypes]    Script Date: 10/25/2007 15:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PropertyTypes](
	[PropertyTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](450) NOT NULL,
	[DataType] [varchar](10) NOT NULL,
	[Mapping] [int] NOT NULL,
	[IsContentListProperty] [tinyint] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_PropertyTypes] PRIMARY KEY CLUSTERED 
(
	[PropertyTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[NodeTypes]    Script Date: 10/25/2007 15:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodeTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NodeTypes](
	[NodeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Name] [varchar](450) NOT NULL,
	[ClassName] [varchar](450) NULL,
	[Properties] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_NodeTypes] PRIMARY KEY CLUSTERED 
(
	[NodeTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NodeTypes]') AND name = N'ix_parentid')
CREATE NONCLUSTERED INDEX [ix_parentid] ON [dbo].[NodeTypes]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ContentListTypes]    Script Date: 10/25/2007 15:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentListTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentListTypes](
	[ContentListTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](450) NOT NULL,
	[Properties] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ContentListTypes] PRIMARY KEY CLUSTERED 
(
	[ContentListTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO
