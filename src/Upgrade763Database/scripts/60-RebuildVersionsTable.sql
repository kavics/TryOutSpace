BEGIN TRANSACTION;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Versions] (
    [VersionId]             INT              IDENTITY (1, 1) NOT NULL,
    [NodeId]                INT              NOT NULL,
    [MajorNumber]           SMALLINT         NOT NULL,
    [MinorNumber]           SMALLINT         NOT NULL,
    [CreationDate]          DATETIME2 (7)    NOT NULL,
    [CreatedById]           INT              NOT NULL,
    [ModificationDate]      DATETIME2 (7)    NOT NULL,
    [ModifiedById]          INT              NOT NULL,
    [Status]                SMALLINT         CONSTRAINT [DF_Versions_Status] DEFAULT ((1)) NOT NULL,
    [IndexDocument]         NVARCHAR (MAX)   NULL,
    [ChangedData]           NVARCHAR (MAX)   NULL,
    [DynamicProperties]     NVARCHAR (MAX)   NULL,
    [ContentListProperties] NVARCHAR (MAX)   NULL,
    [RowGuid]               UNIQUEIDENTIFIER DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Timestamp]             ROWVERSION       NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Versions1] PRIMARY KEY CLUSTERED ([VersionId] ASC)
);
IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Versions])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Versions] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Versions] ([VersionId], [NodeId], [MajorNumber], [MinorNumber], [CreationDate], [CreatedById], [ModificationDate], [ModifiedById], [Status]/*, [IndexDocument]*/, [ChangedData], [RowGuid])
        SELECT   [VersionId],
                 [NodeId],
                 [MajorNumber],
                 [MinorNumber],
                 [CreationDate],
                 [CreatedById],
                 [ModificationDate],
                 [ModifiedById],
                 [Status],
                 /*[IndexDocument],*/
                 [ChangedData],
                 [RowGuid]
        FROM     [dbo].[Versions]
        ORDER BY [VersionId] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Versions] OFF;
    END

COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
