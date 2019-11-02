/***** Upgrade PropertyTypes *****/
SET IDENTITY_INSERT dbo.PropertyTypes ON;
INSERT INTO dbo.PropertyTypes (PropertyTypeId, [Name], DataType, Mapping, IsContentListProperty)
	SELECT S.PropertyTypeId, S.[Name], D.[Name] DataType, Mapping, IsContentListProperty
	FROM SchemaPropertyTypes S JOIN SchemaDataTypes D on S.DataTypeId = D.DataTypeId
SET IDENTITY_INSERT dbo.PropertyTypes OFF;
GO

/***** Upgrade ContentListTypes *****/

SET IDENTITY_INSERT dbo.NodeTypes ON;
INSERT INTO dbo.NodeTypes (NodeTypeId, ParentId, [Name], ClassName, Properties)
	SELECT PropertySetId NodeTypeId, ParentId, [Name], ClassName, COALESCE((SELECT STUFF(
		(SELECT ' ' + Property FROM PropertySetsInfoView WHERE PropertySet = S.[Name] AND IsDeclared = 1 FOR XML PATH('')), 1, 1, '')), '') Properties
	FROM SchemaPropertySets S
	WHERE PropertySetTypeId = 1;
SET IDENTITY_INSERT dbo.NodeTypes OFF;
GO

/***** Upgrade ContentListTypes *****/

SET IDENTITY_INSERT dbo.ContentListTypes ON;
INSERT INTO dbo.ContentListTypes (ContentListTypeId, [Name], Properties)
	SELECT PropertySetId ContentListTypeId, [Name], COALESCE((SELECT STUFF(
		(SELECT ' ' + Property FROM PropertySetsInfoView WHERE PropertySet = S.[Name] AND IsDeclared = 1 FOR XML PATH('')), 1, 1, '')), '') Properties
	FROM SchemaPropertySets S
	WHERE PropertySetTypeId = 2;
SET IDENTITY_INSERT dbo.ContentListTypes OFF;
GO
