/* Re-create [SchemaModification] table */

DROP TABLE [dbo].[SchemaModification];
GO
CREATE TABLE [dbo].[SchemaModification] (
    [SchemaModificationId] INT           IDENTITY (1, 1) NOT NULL,
    [ModificationDate]     DATETIME2 (7) NOT NULL,
    [LockToken]            VARCHAR (50)  NULL,
    [Timestamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SchemaModification] PRIMARY KEY CLUSTERED ([SchemaModificationId] ASC)
);
GO

/* Create new SnSchema tables*/

CREATE TABLE [dbo].[ContentListTypes] (
    [ContentListTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (450)  NOT NULL,
    [Properties]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ContentListTypes] PRIMARY KEY CLUSTERED ([ContentListTypeId] ASC)
);
GO
CREATE TABLE [dbo].[NodeTypes] (
    [NodeTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [ParentId]   INT            NULL,
    [Name]       VARCHAR (450)  NOT NULL,
    [ClassName]  VARCHAR (450)  NULL,
    [Properties] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_NodeTypes] PRIMARY KEY CLUSTERED ([NodeTypeId] ASC)
);
GO
CREATE NONCLUSTERED INDEX [ix_parentid]
    ON [dbo].[NodeTypes]([ParentId] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_name]
    ON [dbo].[NodeTypes]([Name] ASC)
    INCLUDE([NodeTypeId]) WITH (FILLFACTOR = 50);
GO
CREATE TABLE [dbo].[PropertyTypes] (
    [PropertyTypeId]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]                  VARCHAR (450) NOT NULL,
    [DataType]              VARCHAR (10)  NOT NULL,
    [Mapping]               INT           NOT NULL,
    [IsContentListProperty] TINYINT       NOT NULL,
    CONSTRAINT [PK_PropertyTypes] PRIMARY KEY CLUSTERED ([PropertyTypeId] ASC)
);
GO
CREATE NONCLUSTERED INDEX [ix_name]
    ON [dbo].[PropertyTypes]([Name] ASC)
    INCLUDE([PropertyTypeId]) WITH (FILLFACTOR = 50);
GO
