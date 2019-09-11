CREATE TABLE [dbo].[LongTextProperties] (
    [LongTextPropertyId] INT            IDENTITY (1, 1) NOT NULL,
    [VersionId]          INT            NOT NULL,
    [PropertyTypeId]     INT            NOT NULL,
    [Length]             INT            NULL,
    [Value]              NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_LongTextProperties] PRIMARY KEY CLUSTERED ([LongTextPropertyId] ASC)
);
GO
