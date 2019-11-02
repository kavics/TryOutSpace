-- This deletion need to be executed before 'DF_Versions_Status' manipulation because if this temporary table exists
-- after the last unfinished patch-execution the default constraint exists on this table indtead of on the 'Versions' table.
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_ms_xx_Versions]') AND type in (N'U'))
DROP TABLE [dbo].[tmp_ms_xx_Versions]
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JournalItems]') AND name = N'IX_JournalItems')
DROP INDEX [IX_JournalItems]
    ON [dbo].[JournalItems];
GO

IF EXISTS(SELECT 1 FROM sys.default_constraints WHERE [name] = 'DF_Files_CreationDate')
ALTER TABLE [dbo].[Files] DROP CONSTRAINT [DF_Files_CreationDate];
GO
IF EXISTS(SELECT 1 FROM sys.default_constraints WHERE [name] = 'DF_Versions_Status')
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [DF_Versions_Status];
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_BinaryProperties_SchemaPropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.BinaryProperties'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_SchemaPropertyTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_BinaryProperties_Versions') AND parent_object_id = OBJECT_ID(N'dbo.BinaryProperties'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Versions];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_BinaryProperties_Files') AND parent_object_id = OBJECT_ID(N'dbo.BinaryProperties'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Files];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_FlatProperties_Versions') AND parent_object_id = OBJECT_ID(N'dbo.FlatProperties'))
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [FK_FlatProperties_Versions];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_LockedBy') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_LockedBy];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_Parent') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Parent];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_SchemaPropertySets') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_SchemaPropertySets];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_ReferenceProperties_SchemaPropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.ReferenceProperties'))
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [FK_ReferenceProperties_SchemaPropertyTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PropertySets_PropertySets') AND parent_object_id = OBJECT_ID(N'dbo.SchemaPropertySets'))
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySets];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PropertySets_PropertySetTypes') AND parent_object_id = OBJECT_ID(N'dbo.SchemaPropertySets'))
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySetTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PropertyTypes_PropertySets') AND parent_object_id = OBJECT_ID(N'dbo.SchemaPropertySetsPropertyTypes'))
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySets];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PropertyTypes_PropertySlots') AND parent_object_id = OBJECT_ID(N'dbo.SchemaPropertySetsPropertyTypes'))
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySlots];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PropertySlots_DataTypes') AND parent_object_id = OBJECT_ID(N'dbo.SchemaPropertyTypes'))
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [FK_PropertySlots_DataTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_TextPropertiesNText_SchemaPropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.TextPropertiesNText'))
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_SchemaPropertyTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_TextPropertiesNText_Versions') AND parent_object_id = OBJECT_ID(N'dbo.TextPropertiesNText'))
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_Versions];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_TextPropertiesNVarchar_SchemaPropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.TextPropertiesNVarchar'))
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_SchemaPropertyTypes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_TextPropertiesNVarchar_Versions') AND parent_object_id = OBJECT_ID(N'dbo.TextPropertiesNVarchar'))
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_Versions];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes_CreatedBy') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes_ModifiedBy') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_Nodes_CreatedById') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes_CreatedById];
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_Nodes_ModifiedById') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes_ModifiedById];
GO

DECLARE @T TABLE([Table] NVARCHAR(100) NOT NULL, [Column] NVARCHAR(100) NOT NULL, [Constraint] NVARCHAR(100) NOT NULL)
INSERT INTO @T
	SELECT t.[name] AS [Table], col.[name] AS [Column], con.[name] AS [Constraint]
	FROM sys.default_constraints con
		LEFT OUTER JOIN sys.objects t ON con.parent_object_id = t.object_id
		LEFT OUTER JOIN sys.all_columns col ON con.parent_column_id = col.column_id AND con.parent_object_id = col.object_id
DECLARE @sql nvarchar(1000)

SELECT @sql = N'ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'Versions' AND [Column] = 'RowGuid') + N'];'
EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'FlatProperties' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[IndexBackup] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'IndexBackup' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[IndexBackup2] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'IndexBackup2' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaDataTypes] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaDataTypes' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaPropertySets' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaPropertySetsPropertyTypes' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaPropertySetTypes] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaPropertySetTypes' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaPropertyTypes' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'SchemaPropertyTypes' AND [Column] = 'IsContentListProperty') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'TextPropertiesNText' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

--SELECT @sql = N'ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'TextPropertiesNVarchar' AND [Column] = 'RowGuid') + N'];'
--EXEC dbo.sp_executesql @statement = @sql

SELECT @sql = N'ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [' + (SELECT [Constraint] from @T WHERE [Table] = 'ReferenceProperties' AND [Column] = 'RowGuid') + N'];'
EXEC dbo.sp_executesql @statement = @sql

GO
