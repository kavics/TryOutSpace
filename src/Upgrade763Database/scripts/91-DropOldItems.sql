ALTER TABLE [dbo].[ReferenceProperties] DROP COLUMN [RowGuid], COLUMN [Timestamp];
GO

DROP TABLE [dbo].[IndexBackup];
GO
DROP TABLE [dbo].[IndexBackup2];
GO
DROP TABLE [dbo].[TextPropertiesNText];
GO
DROP TABLE [dbo].[TextPropertiesNVarchar];
GO
DROP FUNCTION [dbo].[udfGetAllDerivatedNodeTypesByNodeTypeId];
GO
DROP VIEW [dbo].[PropertyInfoView];
GO
DROP VIEW [dbo].[PropertySetsInfoView];
GO
DROP VIEW [dbo].[SysSearchWithFlatsView];
GO
DROP TABLE [dbo].[FlatProperties];
GO
DROP TABLE [dbo].[SchemaDataTypes];
GO
DROP TABLE [dbo].[SchemaPropertySets];
GO
DROP TABLE [dbo].[SchemaPropertySetsPropertyTypes];
GO
DROP TABLE [dbo].[SchemaPropertySetTypes];
GO
DROP TABLE [dbo].[SchemaPropertyTypes];
GO
DROP VIEW [dbo].[SysSearchView];
GO
DROP PROCEDURE [dbo].[proc_LogAddCategory];
GO
DROP PROCEDURE [dbo].[proc_LogSelect];
GO
DROP PROCEDURE [dbo].[proc_LogWrite];
GO
DROP PROCEDURE [dbo].[proc_Node_GetTreeSize];
GO
DROP PROCEDURE [dbo].[proc_NodeHead_Load_Batch];
GO
