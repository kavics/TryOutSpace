IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferenceProperties]') AND type in (N'U'))
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT IF EXISTS [FK_ReferenceProperties_PropertyTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LongTextProperties]') AND type in (N'U'))
ALTER TABLE [dbo].[LongTextProperties] DROP CONSTRAINT IF EXISTS [FK_LongTextProperties_PropertyTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BinaryProperties]') AND type in (N'U'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT IF EXISTS [FK_BinaryProperties_PropertyTypes]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodeTypes]') AND type in (N'U'))
ALTER TABLE [dbo].[NodeTypes] DROP CONSTRAINT IF EXISTS [FK_NodeTypes_NodeTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nodes]') AND type in (N'U'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT IF EXISTS [FK_Nodes_NodeTypes]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyTypes]') AND type in (N'U'))
DROP TABLE [dbo].[PropertyTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodeTypes]') AND type in (N'U'))
DROP TABLE [dbo].[NodeTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentListTypes]') AND type in (N'U'))
DROP TABLE [dbo].[ContentListTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LongTextProperties]') AND type in (N'U'))
DROP TABLE [dbo].[LongTextProperties]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_ms_xx_Versions]') AND type in (N'U'))
DROP TABLE [dbo].[tmp_ms_xx_Versions]
GO


/***********************************************************************************************************************/


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_SchemaPropertySets_ContentListTypeId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_SchemaPropertySets_ContentListTypeId]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes_ContentListId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes_ContentListId]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes_CreatedById]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes_CreatedById]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes_ModifiedById]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes_ModifiedById]
GO

/****** Object:  ForeignKey [FK_BinaryProperties_SchemaPropertyTypes]    Script Date: 10/25/2007 15:49:18 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BinaryProperties_SchemaPropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[BinaryProperties]'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_SchemaPropertyTypes]
GO
/****** Object:  ForeignKey [FK_BinaryProperties_Versions]    Script Date: 10/25/2007 15:49:19 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BinaryProperties_Versions]') AND parent_object_id = OBJECT_ID(N'[dbo].[BinaryProperties]'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Versions]
GO
/****** Object:  ForeignKey [FK_BinaryProperties_Files] ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BinaryProperties_Files]') AND parent_object_id = OBJECT_ID(N'[dbo].[BinaryProperties]'))
ALTER TABLE [dbo].[BinaryProperties] DROP CONSTRAINT [FK_BinaryProperties_Files]
GO

/****** Object:  ForeignKey [FK_FlatProperties_Versions]    Script Date: 10/25/2007 15:50:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FlatProperties_Versions]') AND parent_object_id = OBJECT_ID(N'[dbo].[FlatProperties]'))
ALTER TABLE [dbo].[FlatProperties] DROP CONSTRAINT [FK_FlatProperties_Versions]
GO
/****** Object:  ForeignKey [FK_Nodes_LockedBy]    Script Date: 10/25/2007 15:50:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_LockedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_LockedBy]
GO
/****** Object:  ForeignKey [FK_Nodes_Parent]    Script Date: 10/25/2007 15:50:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Parent]
GO
/****** Object:  ForeignKey [FK_Nodes_SchemaPropertySets]    Script Date: 10/25/2007 15:50:17 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_SchemaPropertySets]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_SchemaPropertySets]
GO
/****** Object:  ForeignKey [FK_ReferenceProperties_SchemaPropertyTypes]    Script Date: 10/25/2007 15:50:19 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ReferenceProperties_SchemaPropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[ReferenceProperties]'))
ALTER TABLE [dbo].[ReferenceProperties] DROP CONSTRAINT [FK_ReferenceProperties_SchemaPropertyTypes]
GO
/****** Object:  ForeignKey [FK_PropertySets_PropertySets]    Script Date: 10/25/2007 15:50:23 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PropertySets_PropertySets]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchemaPropertySets]'))
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySets]
GO
/****** Object:  ForeignKey [FK_PropertySets_PropertySetTypes]    Script Date: 10/25/2007 15:50:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PropertySets_PropertySetTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchemaPropertySets]'))
ALTER TABLE [dbo].[SchemaPropertySets] DROP CONSTRAINT [FK_PropertySets_PropertySetTypes]
GO
/****** Object:  ForeignKey [FK_PropertyTypes_PropertySets]    Script Date: 10/25/2007 15:50:25 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PropertyTypes_PropertySets]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchemaPropertySetsPropertyTypes]'))
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySets]
GO
/****** Object:  ForeignKey [FK_PropertyTypes_PropertySlots]    Script Date: 10/25/2007 15:50:25 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PropertyTypes_PropertySlots]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchemaPropertySetsPropertyTypes]'))
ALTER TABLE [dbo].[SchemaPropertySetsPropertyTypes] DROP CONSTRAINT [FK_PropertyTypes_PropertySlots]
GO
/****** Object:  ForeignKey [FK_PropertySlots_DataTypes]    Script Date: 10/25/2007 15:50:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PropertySlots_DataTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchemaPropertyTypes]'))
ALTER TABLE [dbo].[SchemaPropertyTypes] DROP CONSTRAINT [FK_PropertySlots_DataTypes]
GO
/****** Object:  ForeignKey [FK_TextPropertiesNText_SchemaPropertyTypes]    Script Date: 10/25/2007 15:50:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TextPropertiesNText_SchemaPropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[TextPropertiesNText]'))
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_SchemaPropertyTypes]
GO
/****** Object:  ForeignKey [FK_TextPropertiesNText_Versions]    Script Date: 10/25/2007 15:50:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TextPropertiesNText_Versions]') AND parent_object_id = OBJECT_ID(N'[dbo].[TextPropertiesNText]'))
ALTER TABLE [dbo].[TextPropertiesNText] DROP CONSTRAINT [FK_TextPropertiesNText_Versions]
GO
/****** Object:  ForeignKey [FK_TextPropertiesNVarchar_SchemaPropertyTypes]    Script Date: 10/25/2007 15:50:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TextPropertiesNVarchar_SchemaPropertyTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[TextPropertiesNVarchar]'))
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_SchemaPropertyTypes]
GO
/****** Object:  ForeignKey [FK_TextPropertiesNVarchar_Versions]    Script Date: 10/25/2007 15:50:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TextPropertiesNVarchar_Versions]') AND parent_object_id = OBJECT_ID(N'[dbo].[TextPropertiesNVarchar]'))
ALTER TABLE [dbo].[TextPropertiesNVarchar] DROP CONSTRAINT [FK_TextPropertiesNVarchar_Versions]
GO
/****** Object:  ForeignKey [FK_Versions_Nodes]    Script Date: 10/25/2007 15:50:36 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Versions_Nodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Versions]'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes]
GO
/****** Object:  ForeignKey [FK_Versions_Nodes_CreatedBy]    Script Date: 10/25/2007 15:50:37 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Versions_Nodes_CreatedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[Versions]'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Versions_Nodes_ModifiedBy]    Script Date: 10/25/2007 15:50:37 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Versions_Nodes_ModifiedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[Versions]'))
ALTER TABLE [dbo].[Versions] DROP CONSTRAINT [FK_Versions_Nodes_ModifiedBy]
GO

/****** Object:  View [dbo].[NodeInfoView]  ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[NodeInfoView]'))
DROP VIEW [dbo].[NodeInfoView]
GO
/****** Object:  View [dbo].[PropertyInfoView]  ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PropertyInfoView]'))
DROP VIEW [dbo].[PropertyInfoView]
GO
/****** Object:  View [dbo].[PermissionInfoView]  ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PermissionInfoView]'))
DROP VIEW [dbo].[PermissionInfoView]
GO
/****** Object:  View [dbo].[SysSearchWithFlatsView]    Script Date: 08/07/2007 14:50:18 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SysSearchWithFlatsView]'))
DROP VIEW [dbo].[SysSearchWithFlatsView]
GO
/****** Object:  View [dbo].[SysSearchView]    Script Date: 08/07/2007 14:50:18 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SysSearchView]'))
DROP VIEW [dbo].[SysSearchView]
GO
/****** Object:  View [dbo].[ReferencesInfoView]    Script Date: 08/07/2007 14:50:18 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ReferencesInfoView]'))
DROP VIEW [dbo].[ReferencesInfoView]
GO
/****** Object:  View [dbo].[PropertySetsInfoView]    Script Date: 08/13/2007 13:40:03 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PropertySetsInfoView]'))
DROP VIEW [dbo].[PropertySetsInfoView]
GO
/****** Object:  View [dbo].[MembershipInfoView]    ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[MembershipInfoView]'))
DROP VIEW [dbo].[MembershipInfoView]
GO

/****** Object:  Table [dbo].[FlatProperties]    Script Date: 10/25/2007 15:50:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatProperties]') AND type in (N'U'))
DROP TABLE [dbo].[FlatProperties]
GO
/****** Object:  Table [dbo].[SchemaPropertySetTypes]    Script Date: 10/25/2007 15:50:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaPropertySetTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaPropertySetTypes]
GO
/****** Object:  Table [dbo].[SchemaDataTypes]    Script Date: 10/25/2007 15:50:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaDataTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaDataTypes]
GO
/****** Object:  Table [dbo].[SchemaPropertyTypes]    Script Date: 10/25/2007 15:50:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaPropertyTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaPropertyTypes]
GO
/****** Object:  Table [dbo].[SchemaPropertySets]    Script Date: 10/25/2007 15:50:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaPropertySets]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaPropertySets]
GO
/****** Object:  Table [dbo].[SchemaPropertySetsPropertyTypes]    Script Date: 10/25/2007 15:50:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaPropertySetsPropertyTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaPropertySetsPropertyTypes]
GO
/****** Object:  Table [dbo].[ReferenceProperties]    Script Date: 10/25/2007 15:50:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferenceProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ReferenceProperties]
GO
/****** Object:  Table [dbo].[TextPropertiesNText]    Script Date: 10/25/2007 15:50:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TextPropertiesNText]') AND type in (N'U'))
DROP TABLE [dbo].[TextPropertiesNText]
GO
/****** Object:  Table [dbo].[BinaryProperties]    Script Date: 10/25/2007 15:49:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BinaryProperties]') AND type in (N'U'))
DROP TABLE [dbo].[BinaryProperties]
GO
/****** Object:  Table [dbo].[Files] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Files]') AND type in (N'U'))
DROP TABLE [dbo].[Files]
GO
/****** Object:  Table [dbo].[TextPropertiesNVarchar]    Script Date: 10/25/2007 15:50:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TextPropertiesNVarchar]') AND type in (N'U'))
DROP TABLE [dbo].[TextPropertiesNVarchar]
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 10/25/2007 15:50:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nodes]') AND type in (N'U'))
DROP TABLE [dbo].[Nodes]
GO
/****** Object:  Table [dbo].[Versions]    Script Date: 10/25/2007 15:50:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Versions]') AND type in (N'U'))
DROP TABLE [dbo].[Versions]
GO
/****** Object:  Table [dbo].[JournalItems]    Script Date: 07/08/2009 07:22:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JournalItems]') AND type in (N'U'))
DROP TABLE [dbo].[JournalItems]
GO

/****** Object:  Table [dbo].[LogEntries]    Script Date: 10/09/2009 10:01:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogEntries]') AND type in (N'U'))
DROP TABLE [dbo].[LogEntries]
GO

/****** Object:  Table [dbo].[IndexingActivities]    Script Date: 09/01/2010 05:28:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IndexingActivities]') AND type in (N'U'))
DROP TABLE [dbo].[IndexingActivities]
GO

/****** [DF_IndexBackup_RowGuid] ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IndexBackup_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IndexBackup] DROP CONSTRAINT [DF_IndexBackup_RowGuid]
END
GO
/****** [DF_IndexBackup_RowGuid] ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IndexBackup2_RowGuid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IndexBackup2] DROP CONSTRAINT [DF_IndexBackup2_RowGuid]
END
GO

/****** Object:  Table [dbo].[IndexBackup]    Script Date: 10/27/2010 20:58:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IndexBackup]') AND type in (N'U'))
DROP TABLE [dbo].[IndexBackup]
GO
/****** Object:  Table [dbo].[IndexBackup]    Script Date: 10/27/2010 20:58:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IndexBackup2]') AND type in (N'U'))
DROP TABLE [dbo].[IndexBackup2]
GO

/****** Object:  Table [dbo].[WorkflowNotification]   ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowNotification]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowNotification]
GO

/****** Object:  Table [dbo].SchemaModification]   ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaModification]') AND type in (N'U'))
DROP TABLE [dbo].[SchemaModification]
GO

--/****** Object:  Table [dbo].[Packages]  ******/
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Packages]') AND type in (N'U'))
--DROP TABLE [dbo].[Packages]
--GO

/****** Object:  Table [dbo].[TreeLocks]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TreeLocks]') AND type in (N'U'))
DROP TABLE [dbo].[TreeLocks]
GO

/****** Object:  Table [dbo].[AccessTokens]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessTokens]') AND type in (N'U'))
DROP TABLE [dbo].[AccessTokens]
GO

/****** Object:  Table [dbo].[SharedLocks]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SharedLocks]') AND type in (N'U'))
DROP TABLE [dbo].[SharedLocks]
GO
