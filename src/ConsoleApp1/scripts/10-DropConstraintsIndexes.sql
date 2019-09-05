USE [SN7_Upgrade];
GO

DROP INDEX [IX_JournalItems]
    ON [dbo].[JournalItems];
GO

ALTER TABLE [dbo].[Files] DROP CONSTRAINT [DF_Files_CreationDate];
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [DF_Versions_Status];
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [DF__Versions__RowGui__5733CBC5];
GO
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [DF__FlatPrope__RowGu__5086CE36];
GO
ALTER TABLE [dbo].[IndexBackup] DROP CONSTRAINT [DF_IndexBackup_RowGuid];
GO
ALTER TABLE [dbo].[IndexBackup2] DROP CONSTRAINT [DF_IndexBackup2_RowGuid];
GO
ALTER TABLE [dbo].[SchemaDataTypes] DROP CONSTRAINT [DF__SchemaDat__RowGu__39A368DE];
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [DF__SchemaPro__RowGu__5A103870];
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__53633AE1];
GO
ALTER TABLE [dbo].[SchemaPropertySetTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__36C6FC33];
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__IsCon__5CECA51B];
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [DF__SchemaPro__RowGu__5DE0C954];
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [DF__TextPrope__RowGu__3F5C4234];
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [DF__TextPrope__RowGu__4238AEDF];
GO
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [DF__Reference__RowGu__3C7FD589];
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_Versions];
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_Versions];
GO
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Versions];
GO
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [FK_FlatProperties_Versions];
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes];
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [FK_PropertySlots_DataTypes];
GO
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_SchemaPropertySets];
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySets];
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySets];
GO
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySetTypes];
GO
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [FK_ReferenceProperties_SchemaPropertyTypes];
GO
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_SchemaPropertyTypes];
GO
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_SchemaPropertyTypes];
GO
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_SchemaPropertyTypes];
GO
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySlots];
GO
