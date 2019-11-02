USE [SN7_Upgrade];
GO
PRINT N'Dropping [dbo].[JournalItems].[IX_JournalItems]...';
GO
DROP INDEX [IX_JournalItems]
    ON [dbo].[JournalItems];
GO
PRINT N'Dropping [dbo].[DF_Files_CreationDate]...';
GO
ALTER TABLE [dbo].[Files] DROP CONSTRAINT [DF_Files_CreationDate];
GO
PRINT N'Dropping [dbo].[DF_Versions_Status]...';
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [DF_Versions_Status];
GO
PRINT N'Dropping unnamed constraint on [dbo].[Versions]...';
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [DF__Versions__RowGui__5733CBC5];
GO
PRINT N'Dropping unnamed constraint on [dbo].[FlatProperties]...';
GO
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [DF__FlatPrope__RowGu__5086CE36];
GO
PRINT N'Dropping [dbo].[DF_IndexBackup_RowGuid]...';
GO
ALTER TABLE [dbo].[IndexBackup] DROP CONSTRAINT [DF_IndexBackup_RowGuid];
GO
PRINT N'Dropping [dbo].[DF_IndexBackup2_RowGuid]...';
GO
ALTER TABLE [dbo].[IndexBackup2] DROP CONSTRAINT [DF_IndexBackup2_RowGuid];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaDataTypes]...';
GO
ALTER TABLE [dbo].[SchemaDataTypes] DROP CONSTRAINT [DF__SchemaDat__RowGu__39A368DE];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaPropertySets]...';
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [DF__SchemaPro__RowGu__5A103870];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaPropertySetsPropertyTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__53633AE1];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaPropertySetTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertySetTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__36C6FC33];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__IsCon__5CECA51B];
GO
PRINT N'Dropping unnamed constraint on [dbo].[SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__5DE0C954];
GO
PRINT N'Dropping unnamed constraint on [dbo].[TextPropertiesNText]...';
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [DF__TextPrope__RowGu__3F5C4234];
GO
PRINT N'Dropping unnamed constraint on [dbo].[TextPropertiesNVarchar]...';
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [DF__TextPrope__RowGu__4238AEDF];
GO
PRINT N'Dropping unnamed constraint on [dbo].[ReferenceProperties]...';
GO
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [DF__Reference__RowGu__3C7FD589];
GO
PRINT N'Dropping [dbo].[FK_TextPropertiesNText_Versions]...';
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_Versions];
GO
PRINT N'Dropping [dbo].[FK_TextPropertiesNVarchar_Versions]...';
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_Versions];
GO
PRINT N'Dropping [dbo].[FK_BinaryProperties_Versions]...';
GO
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Versions];
GO
PRINT N'Dropping [dbo].[FK_FlatProperties_Versions]...';
GO
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [FK_FlatProperties_Versions];
GO
PRINT N'Dropping [dbo].[FK_Versions_Nodes]...';
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes];
GO
PRINT N'Dropping [dbo].[FK_Versions_Nodes_CreatedBy]...';
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO
PRINT N'Dropping [dbo].[FK_Versions_Nodes_ModifiedBy]...';
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO
PRINT N'Dropping [dbo].[FK_PropertySlots_DataTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [FK_PropertySlots_DataTypes];
GO
PRINT N'Dropping [dbo].[FK_Nodes_SchemaPropertySets]...';
GO
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_SchemaPropertySets];
GO
PRINT N'Dropping [dbo].[FK_PropertyTypes_PropertySets]...';
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySets];
GO
PRINT N'Dropping [dbo].[FK_PropertySets_PropertySets]...';
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySets];
GO
PRINT N'Dropping [dbo].[FK_PropertySets_PropertySetTypes]...';
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySetTypes];
GO
PRINT N'Dropping [dbo].[FK_ReferenceProperties_SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [FK_ReferenceProperties_SchemaPropertyTypes];
GO
PRINT N'Dropping [dbo].[FK_TextPropertiesNText_SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_SchemaPropertyTypes];
GO
PRINT N'Dropping [dbo].[FK_TextPropertiesNVarchar_SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_SchemaPropertyTypes];
GO
PRINT N'Dropping [dbo].[FK_BinaryProperties_SchemaPropertyTypes]...';
GO
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_SchemaPropertyTypes];
GO
PRINT N'Dropping [dbo].[FK_PropertyTypes_PropertySlots]...';
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySlots];
GO
PRINT N'Dropping [dbo].[IndexBackup]...';
GO
DROP TABLE [dbo].[IndexBackup];
GO
PRINT N'Dropping [dbo].[IndexBackup2]...';
GO
DROP TABLE [dbo].[IndexBackup2];
GO
PRINT N'Dropping [dbo].[TextPropertiesNText]...';
GO
DROP TABLE [dbo].[TextPropertiesNText];
GO
PRINT N'Dropping [dbo].[TextPropertiesNVarchar]...';
GO
DROP TABLE [dbo].[TextPropertiesNVarchar];
GO
PRINT N'Dropping [dbo].[udfGetAllDerivatedNodeTypesByNodeTypeId]...';
GO
DROP FUNCTION [dbo].[udfGetAllDerivatedNodeTypesByNodeTypeId];
GO
PRINT N'Dropping [dbo].[PropertyInfoView]...';
GO
DROP VIEW [dbo].[PropertyInfoView];
GO
PRINT N'Dropping [dbo].[PropertySetsInfoView]...';
GO
DROP VIEW [dbo].[PropertySetsInfoView];
GO
PRINT N'Dropping [dbo].[SysSearchWithFlatsView]...';
GO
DROP VIEW [dbo].[SysSearchWithFlatsView];
GO
PRINT N'Dropping [dbo].[FlatProperties]...';
GO
DROP TABLE [dbo].[FlatProperties];
GO
PRINT N'Dropping [dbo].[SchemaDataTypes]...';
GO
DROP TABLE [dbo].[SchemaDataTypes];
GO
PRINT N'Dropping [dbo].[SchemaPropertySets]...';
GO
DROP TABLE [dbo].[SchemaPropertySets];
GO
PRINT N'Dropping [dbo].[SchemaPropertySetsPropertyTypes]...';
GO
DROP TABLE [dbo].[SchemaPropertySetsPropertyTypes];
GO
PRINT N'Dropping [dbo].[SchemaPropertySetTypes]...';
GO
DROP TABLE [dbo].[SchemaPropertySetTypes];
GO
PRINT N'Dropping [dbo].[SchemaPropertyTypes]...';
GO
DROP TABLE [dbo].[SchemaPropertyTypes];
GO
PRINT N'Dropping [dbo].[SysSearchView]...';
GO
DROP VIEW [dbo].[SysSearchView];
GO
PRINT N'Dropping [dbo].[proc_CustomProc1]...';
GO
DROP PROCEDURE [dbo].[proc_CustomProc1];
GO
PRINT N'Dropping [dbo].[proc_LogAddCategory]...';
GO
DROP PROCEDURE [dbo].[proc_LogAddCategory];
GO
PRINT N'Dropping [dbo].[proc_LogSelect]...';
GO
DROP PROCEDURE [dbo].[proc_LogSelect];
GO
PRINT N'Dropping [dbo].[proc_LogWrite]...';
GO
DROP PROCEDURE [dbo].[proc_LogWrite];
GO
PRINT N'Dropping [dbo].[proc_Node_GetTreeSize]...';
GO
DROP PROCEDURE [dbo].[proc_Node_GetTreeSize];
GO
PRINT N'Dropping [dbo].[proc_NodeHead_Load_Batch]...';
GO
DROP PROCEDURE [dbo].[proc_NodeHead_Load_Batch];
GO
PRINT N'Altering [dbo].[AccessTokens]...';
GO
ALTER TABLE [dbo].[AccessTokens] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[AccessTokens] ALTER COLUMN [ExpirationDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[Files]...';
GO
ALTER TABLE [dbo].[Files] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[IndexingActivities]...';
GO
ALTER TABLE [dbo].[IndexingActivities] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[IndexingActivities] ALTER COLUMN [LockTime] DATETIME2 (7) NULL;
GO
PRINT N'Altering [dbo].[JournalItems]...';
GO
ALTER TABLE [dbo].[JournalItems] ALTER COLUMN [When] DATETIME2 (7) NOT NULL;
GO
PRINT N'Creating [dbo].[JournalItems].[IX_JournalItems]...';
GO
CREATE NONCLUSTERED INDEX [IX_JournalItems]
    ON [dbo].[JournalItems]([When] DESC, [Wherewith] ASC);
GO
PRINT N'Altering [dbo].[LogEntries]...';
GO
ALTER TABLE [dbo].[LogEntries] ALTER COLUMN [LogDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[Nodes]...';
GO
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [LastLockUpdate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [LockDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [ModificationDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[Packages]...';
GO
ALTER TABLE [dbo].[Packages] ALTER COLUMN [ExecutionDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Packages] ALTER COLUMN [ReleaseDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[ReferenceProperties]...';
GO
ALTER TABLE [dbo].[ReferenceProperties] DROP COLUMN [RowGuid], COLUMN [Timestamp];
GO
PRINT N'Starting rebuilding table [dbo].[SchemaModification]...';
GO
BEGIN TRANSACTION;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
CREATE TABLE [dbo].[tmp_ms_xx_SchemaModification] (
    [SchemaModificationId] INT           IDENTITY (1, 1) NOT NULL,
    [ModificationDate]     DATETIME2 (7) NOT NULL,
    [LockToken]            VARCHAR (50)  NULL,
    [Timestamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_SchemaModification1] PRIMARY KEY CLUSTERED ([SchemaModificationId] ASC)
);
IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[SchemaModification])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SchemaModification] ON;
        INSERT INTO [dbo].[tmp_ms_xx_SchemaModification] ([SchemaModificationId], [ModificationDate])
        SELECT   [SchemaModificationId],
                 [ModificationDate]
        FROM     [dbo].[SchemaModification]
        ORDER BY [SchemaModificationId] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SchemaModification] OFF;
    END
DROP TABLE [dbo].[SchemaModification];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_SchemaModification]', N'SchemaModification';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_SchemaModification1]', N'PK_SchemaModification', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Altering [dbo].[SharedLocks]...';
GO
ALTER TABLE [dbo].[SharedLocks] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
GO
PRINT N'Altering [dbo].[TreeLocks]...';
GO
ALTER TABLE [dbo].[TreeLocks] ALTER COLUMN [LockedAt] DATETIME2 (7) NOT NULL;
GO
PRINT N'Starting rebuilding table [dbo].[Versions]...';
GO
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
        INSERT INTO [dbo].[tmp_ms_xx_Versions] ([VersionId], [NodeId], [MajorNumber], [MinorNumber], [CreationDate], [CreatedById], [ModificationDate], [ModifiedById], [Status], [IndexDocument], [ChangedData], [RowGuid])
        SELECT   [VersionId],
                 [NodeId],
                 [MajorNumber],
                 [MinorNumber],
                 [CreationDate],
                 [CreatedById],
                 [ModificationDate],
                 [ModifiedById],
                 [Status],
                 [IndexDocument],
                 [ChangedData],
                 [RowGuid]
        FROM     [dbo].[Versions]
        ORDER BY [VersionId] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Versions] OFF;
    END
DROP TABLE [dbo].[Versions];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Versions]', N'Versions';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Versions1]', N'PK_Versions', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Creating [dbo].[Versions].[ix_Versions_NodeId]...';
GO
CREATE NONCLUSTERED INDEX [ix_Versions_NodeId]
    ON [dbo].[Versions]([NodeId] ASC);
GO
PRINT N'Creating [dbo].[Versions].[ix_Versions_NodeId_MinorNumber_MajorNumber_Status]...';
GO
CREATE NONCLUSTERED INDEX [ix_Versions_NodeId_MinorNumber_MajorNumber_Status]
    ON [dbo].[Versions]([NodeId] ASC, [MinorNumber] ASC, [Status] ASC);
GO
PRINT N'Creating [dbo].[ContentListTypes]...';
GO
CREATE TABLE [dbo].[ContentListTypes] (
    [ContentListTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (450)  NOT NULL,
    [Properties]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ContentListTypes] PRIMARY KEY CLUSTERED ([ContentListTypeId] ASC)
);
GO
PRINT N'Creating [dbo].[LongTextProperties]...';
GO
CREATE TABLE [dbo].[LongTextProperties] (
    [LongTextPropertyId] INT            IDENTITY (1, 1) NOT NULL,
    [VersionId]          INT            NOT NULL,
    [PropertyTypeId]     INT            NOT NULL,
    [Length]             INT            NULL,
    [Value]              NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_LongTextProperties] PRIMARY KEY CLUSTERED ([LongTextPropertyId] ASC)
);
GO
PRINT N'Creating [dbo].[LongTextProperties].[ix_version_id]...';
GO
CREATE NONCLUSTERED INDEX [ix_version_id]
    ON [dbo].[LongTextProperties]([VersionId] ASC);
GO
PRINT N'Creating [dbo].[NodeTypes]...';
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
PRINT N'Creating [dbo].[NodeTypes].[ix_parentid]...';
GO
CREATE NONCLUSTERED INDEX [ix_parentid]
    ON [dbo].[NodeTypes]([ParentId] ASC);
GO
PRINT N'Creating [dbo].[NodeTypes].[ix_name]...';
GO
CREATE NONCLUSTERED INDEX [ix_name]
    ON [dbo].[NodeTypes]([Name] ASC)
    INCLUDE([NodeTypeId]) WITH (FILLFACTOR = 50);
GO
PRINT N'Creating [dbo].[PropertyTypes]...';
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
PRINT N'Creating [dbo].[PropertyTypes].[ix_name]...';
GO
CREATE NONCLUSTERED INDEX [ix_name]
    ON [dbo].[PropertyTypes]([Name] ASC)
    INCLUDE([PropertyTypeId]) WITH (FILLFACTOR = 50);
GO
PRINT N'Creating [dbo].[DF_Files_CreationDate]...';
GO
ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate];
GO
PRINT N'Creating unnamed constraint on [dbo].[PropertyTypes]...';
GO
ALTER TABLE [dbo].[PropertyTypes]
    ADD DEFAULT ((0)) FOR [IsContentListProperty];
GO
PRINT N'Creating [dbo].[FK_BinaryProperties_Versions]...';
GO
ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_BinaryProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_Versions];
GO
PRINT N'Creating [dbo].[FK_Versions_Nodes]...';
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes];
GO
PRINT N'Creating [dbo].[FK_Versions_Nodes_CreatedBy]...';
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes_CreatedBy] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO
PRINT N'Creating [dbo].[FK_Versions_Nodes_ModifiedBy]...';
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes_ModifiedBy] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO
PRINT N'Creating [dbo].[FK_LongTextProperties_PropertyTypes]...';
GO
ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_LongTextProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_PropertyTypes];
GO
PRINT N'Creating [dbo].[FK_LongTextProperties_Versions]...';
GO
ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_LongTextProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_Versions];
GO
PRINT N'Creating [dbo].[FK_NodeTypes_NodeTypes]...';
GO
ALTER TABLE [dbo].[NodeTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_NodeTypes_NodeTypes] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO
PRINT N'Creating [dbo].[FK_BinaryProperties_PropertyTypes]...';
GO
ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_BinaryProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_PropertyTypes];
GO
PRINT N'Creating [dbo].[FK_Nodes_NodeTypes]...';
GO
ALTER TABLE [dbo].[Nodes] WITH NOCHECK
    ADD CONSTRAINT [FK_Nodes_NodeTypes] FOREIGN KEY ([NodeTypeId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO
ALTER TABLE [dbo].[Nodes] NOCHECK CONSTRAINT [FK_Nodes_NodeTypes];
GO
PRINT N'Creating [dbo].[FK_ReferenceProperties_PropertyTypes]...';
GO
ALTER TABLE [dbo].[ReferenceProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_ReferenceProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[ReferenceProperties] NOCHECK CONSTRAINT [FK_ReferenceProperties_PropertyTypes];
GO
PRINT N'Altering [dbo].[NodeInfoView]...';
GO
ALTER VIEW [dbo].[NodeInfoView]
AS
SELECT     N.NodeId, T.Name AS Type, N.Name, N.Path, N.LockedById, V.VersionId, CONVERT(Varchar, V.MajorNumber) + '.' + CONVERT(Varchar, V.MinorNumber) 
                      + '.' + CASE [Status] WHEN 1 THEN 'A' WHEN 2 THEN 'L' WHEN 4 THEN 'D' WHEN 8 THEN 'R' WHEN 16 THEN 'P' ELSE '' END AS Version, 
                      CASE V.VersionId WHEN N .LastMajorVersionId THEN 'TRUE' ELSE 'false' END AS LastPub, 
                      CASE V.VersionId WHEN N .LastMinorVersionId THEN 'TRUE' ELSE 'false' END AS LastWork
FROM         dbo.Versions AS V INNER JOIN
                      dbo.Nodes AS N ON V.NodeId = N.NodeId INNER JOIN
                      dbo.NodeTypes AS T ON N.NodeTypeId = T.NodeTypeId
GO
PRINT N'Refreshing [dbo].[PermissionInfoView]...';
GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PermissionInfoView]';
GO
PRINT N'Altering [dbo].[ReferencesInfoView]...';
GO
ALTER VIEW [dbo].[ReferencesInfoView]
AS
--SELECT     Nodes.Name AS SrcName, 'V' + CAST(Versions.MajorNumber AS nvarchar(20)) + '.' + CAST(Versions.MinorNumber AS nvarchar(20)) AS SrcVer, 
--                      Slots.Name AS RelType, RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, 
--                      RefNodes.Path AS TargetPath
--FROM         dbo.ReferenceProperties AS Refs INNER JOIN
--                      dbo.Versions AS Versions ON Refs.VersionId = Versions.VersionId INNER JOIN
--                      dbo.Nodes AS Nodes ON Versions.NodeId = Nodes.NodeId INNER JOIN
--                      dbo.Nodes AS RefNodes ON Refs.ReferredNodeId = RefNodes.NodeId INNER JOIN
--                      dbo.PropertyTypes AS Slots ON Refs.PropertyTypeId = Slots.PropertyTypeId
-- ReferenceProperties
	SELECT     Nodes.Name AS SrcName, 'V' + CAST(Versions.MajorNumber AS nvarchar(20)) + '.' + CAST(Versions.MinorNumber AS nvarchar(20)) AS SrcVer, 
						  Slots.Name AS RelType, RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, 
						  RefNodes.Path AS TargetPath
	FROM         dbo.ReferenceProperties AS Refs INNER JOIN
						  dbo.Versions AS Versions ON Refs.VersionId = Versions.VersionId INNER JOIN
						  dbo.Nodes AS Nodes ON Versions.NodeId = Nodes.NodeId INNER JOIN
						  dbo.Nodes AS RefNodes ON Refs.ReferredNodeId = RefNodes.NodeId INNER JOIN
						  dbo.PropertyTypes AS Slots ON Refs.PropertyTypeId = Slots.PropertyTypeId
UNION ALL
-- Parent
	SELECT     Nodes.Name AS SrcName, 'V*.*' AS SrcVer, 'Parent' AS RelType, RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, 
						  RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, RefNodes.Path AS TargetPath
	FROM         dbo.Nodes AS Nodes INNER JOIN
						  dbo.Nodes AS RefNodes ON Nodes.ParentNodeId = RefNodes.NodeId
UNION ALL
-- LockedById
	SELECT     Nodes.Name AS SrcName, 'V*.*' AS SrcVer, 'LockedById' AS RelType, RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, 
						  RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, RefNodes.Path AS TargetPath
	FROM         dbo.Nodes AS Nodes INNER JOIN
						  dbo.Nodes AS RefNodes ON Nodes.LockedById = RefNodes.NodeId
UNION ALL
-- CreatedById
	SELECT     Nodes.Name AS SrcName, 'V' + CAST(Versions.MajorNumber AS nvarchar(20)) + '.' + CAST(Versions.MinorNumber AS nvarchar(20)) AS SrcVer, 
						  'CreatedById', RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, 
						  RefNodes.Path AS TargetPath
	FROM         dbo.Nodes AS Nodes INNER JOIN
		                  dbo.Versions AS Versions ON Nodes.NodeId = Versions.NodeId INNER JOIN
			              dbo.Nodes AS RefNodes ON Versions.CreatedById = RefNodes.NodeId
UNION ALL
-- ModifiedById
	SELECT     Nodes.Name AS SrcName, 'V' + CAST(Versions.MajorNumber AS nvarchar(20)) + '.' + CAST(Versions.MinorNumber AS nvarchar(20)) AS SrcVer, 
						  'ModifiedById', RefNodes.Name AS TargetName, Nodes.NodeId AS SrcId, RefNodes.NodeId AS TargetId, Nodes.Path AS SrcPath, 
						  RefNodes.Path AS TargetPath
	FROM         dbo.Nodes AS Nodes INNER JOIN
		                  dbo.Versions AS Versions ON Nodes.NodeId = Versions.NodeId INNER JOIN
			              dbo.Nodes AS RefNodes ON Versions.ModifiedById = RefNodes.NodeId
GO
PRINT N'Checking existing data against newly created constraints';
GO
USE [$(DatabaseName)];
GO
ALTER TABLE [dbo].[NodeTypes] WITH CHECK CHECK CONSTRAINT [FK_NodeTypes_NodeTypes];
GO
PRINT N'Update complete.';
GO
