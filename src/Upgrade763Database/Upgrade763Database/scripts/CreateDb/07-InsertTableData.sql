TRUNCATE TABLE [dbo].[SchemaDataTypes]
GO
INSERT INTO [dbo].[SchemaDataTypes]
	([Name],[RowGuid])
	SELECT [Name],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[SchemaDataTypes]
GO

TRUNCATE TABLE [dbo].[SchemaPropertySetTypes]
GO
SET IDENTITY_INSERT [dbo].[SchemaPropertySetTypes] ON 
INSERT INTO [dbo].[SchemaPropertySetTypes]
	([PropertySetTypeId],[Name],[RowGuid])
	SELECT [PropertySetTypeId],[Name],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[SchemaPropertySetTypes]
SET IDENTITY_INSERT [dbo].[SchemaPropertySetTypes] OFF
GO

TRUNCATE TABLE [dbo].[SchemaPropertySets]
GO
SET IDENTITY_INSERT [dbo].[SchemaPropertySets] ON 
INSERT INTO [dbo].[SchemaPropertySets]
	([PropertySetId],[ParentId],[Name],[PropertySetTypeId],[ClassName],[RowGuid])
	SELECT [PropertySetId],[ParentId],[Name],[PropertySetTypeId],[ClassName],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[SchemaPropertySets]
SET IDENTITY_INSERT [dbo].[SchemaPropertySets] OFF
GO

TRUNCATE TABLE [dbo].[SchemaPropertySetsPropertyTypes]
GO
--SET IDENTITY_INSERT [dbo].[SchemaPropertySetsPropertyTypes] ON 
INSERT INTO [dbo].[SchemaPropertySetsPropertyTypes]
	([PropertyTypeId],[PropertySetId],[IsDeclared],[RowGuid])
	SELECT [PropertyTypeId],[PropertySetId],[IsDeclared],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[SchemaPropertySetsPropertyTypes]
--SET IDENTITY_INSERT [dbo].[SchemaPropertySetsPropertyTypes] OFF
GO

TRUNCATE TABLE [dbo].[SchemaPropertyTypes]
GO
SET IDENTITY_INSERT [dbo].[SchemaPropertyTypes] ON 
INSERT INTO [dbo].[SchemaPropertyTypes]
	([PropertyTypeId],[Name],[DataTypeId],[Mapping],[IsContentListProperty],[RowGuid])
	SELECT [PropertyTypeId],[Name],[DataTypeId],[Mapping],[IsContentListProperty],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[SchemaPropertyTypes]
SET IDENTITY_INSERT [dbo].[SchemaPropertyTypes] OFF
GO

TRUNCATE TABLE [dbo].[Nodes]
GO
SET IDENTITY_INSERT [dbo].[Nodes] ON 
INSERT INTO [dbo].[Nodes]
	([NodeId],[NodeTypeId],[ContentListTypeId],[ContentListId],[CreatingInProgress],[IsDeleted],[IsInherited],[ParentNodeId],[Name],[Path],[Index],[Locked],[LockedById],[ETag],[LockType],[LockTimeout],[LockDate],[LockToken],[LastLockUpdate],[LastMinorVersionId],[LastMajorVersionId],[CreationDate],[CreatedById],[ModificationDate],[ModifiedById],[DisplayName],[IsSystem],[OwnerId],[SavingState],[RowGuid]/*,[Timestamp]*/)
	SELECT [NodeId],[NodeTypeId],[ContentListTypeId],[ContentListId],[CreatingInProgress],[IsDeleted],[IsInherited],[ParentNodeId],[Name],[Path],[Index],[Locked],[LockedById],[ETag],[LockType],[LockTimeout],[LockDate],[LockToken],[LastLockUpdate],[LastMinorVersionId],[LastMajorVersionId],[CreationDate],[CreatedById],[ModificationDate],[ModifiedById],[DisplayName],[IsSystem],[OwnerId],[SavingState],[RowGuid]/*,[Timestamp]*/
		FROM [devservice.demo.sensenet.com].[dbo].[Nodes]
SET IDENTITY_INSERT [dbo].[Nodes] OFF
GO

TRUNCATE TABLE [dbo].[FlatProperties]
GO
SET IDENTITY_INSERT [dbo].[FlatProperties] ON 
INSERT INTO [dbo].[FlatProperties]
	([Id],[VersionId],[Page]
	,[nvarchar_1],[nvarchar_2],[nvarchar_3],[nvarchar_4],[nvarchar_5],[nvarchar_6],[nvarchar_7],[nvarchar_8],[nvarchar_9],[nvarchar_10]
	,[nvarchar_11],[nvarchar_12],[nvarchar_13],[nvarchar_14],[nvarchar_15],[nvarchar_16],[nvarchar_17],[nvarchar_18],[nvarchar_19],[nvarchar_20]
	,[nvarchar_21],[nvarchar_22],[nvarchar_23],[nvarchar_24],[nvarchar_25],[nvarchar_26],[nvarchar_27],[nvarchar_28],[nvarchar_29],[nvarchar_30]
	,[nvarchar_31],[nvarchar_32],[nvarchar_33],[nvarchar_34],[nvarchar_35],[nvarchar_36],[nvarchar_37],[nvarchar_38],[nvarchar_39],[nvarchar_40]
	,[nvarchar_41],[nvarchar_42],[nvarchar_43],[nvarchar_44],[nvarchar_45],[nvarchar_46],[nvarchar_47],[nvarchar_48],[nvarchar_49],[nvarchar_50]
	,[nvarchar_51],[nvarchar_52],[nvarchar_53],[nvarchar_54],[nvarchar_55],[nvarchar_56],[nvarchar_57],[nvarchar_58],[nvarchar_59],[nvarchar_60]
	,[nvarchar_61],[nvarchar_62],[nvarchar_63],[nvarchar_64],[nvarchar_65],[nvarchar_66],[nvarchar_67],[nvarchar_68],[nvarchar_69],[nvarchar_70]
	,[nvarchar_71],[nvarchar_72],[nvarchar_73],[nvarchar_74],[nvarchar_75],[nvarchar_76],[nvarchar_77],[nvarchar_78],[nvarchar_79],[nvarchar_80]
	,[int_1],[int_2],[int_3],[int_4],[int_5],[int_6],[int_7],[int_8],[int_9],[int_10],[int_11],[int_12],[int_13],[int_14],[int_15],[int_16],[int_17],[int_18],[int_19],[int_20]
	,[int_21],[int_22],[int_23],[int_24],[int_25],[int_26],[int_27],[int_28],[int_29],[int_30],[int_31],[int_32],[int_33],[int_34],[int_35],[int_36],[int_37],[int_38],[int_39],[int_40]
	,[datetime_1],[datetime_2],[datetime_3],[datetime_4],[datetime_5],[datetime_6],[datetime_7],[datetime_8],[datetime_9],[datetime_10],[datetime_11],[datetime_12],[datetime_13]
	,[datetime_14],[datetime_15],[datetime_16],[datetime_17],[datetime_18],[datetime_19],[datetime_20],[datetime_21],[datetime_22],[datetime_23],[datetime_24],[datetime_25]
	,[money_1],[money_2],[money_3],[money_4],[money_5],[money_6],[money_7],[money_8],[money_9],[money_10],[money_11],[money_12],[money_13],[money_14],[money_15],[RowGuid])
	SELECT [Id],[VersionId],[Page]
		,[nvarchar_1],[nvarchar_2],[nvarchar_3],[nvarchar_4],[nvarchar_5],[nvarchar_6],[nvarchar_7],[nvarchar_8],[nvarchar_9],[nvarchar_10]
		,[nvarchar_11],[nvarchar_12],[nvarchar_13],[nvarchar_14],[nvarchar_15],[nvarchar_16],[nvarchar_17],[nvarchar_18],[nvarchar_19],[nvarchar_20]
		,[nvarchar_21],[nvarchar_22],[nvarchar_23],[nvarchar_24],[nvarchar_25],[nvarchar_26],[nvarchar_27],[nvarchar_28],[nvarchar_29],[nvarchar_30]
		,[nvarchar_31],[nvarchar_32],[nvarchar_33],[nvarchar_34],[nvarchar_35],[nvarchar_36],[nvarchar_37],[nvarchar_38],[nvarchar_39],[nvarchar_40]
		,[nvarchar_41],[nvarchar_42],[nvarchar_43],[nvarchar_44],[nvarchar_45],[nvarchar_46],[nvarchar_47],[nvarchar_48],[nvarchar_49],[nvarchar_50]
		,[nvarchar_51],[nvarchar_52],[nvarchar_53],[nvarchar_54],[nvarchar_55],[nvarchar_56],[nvarchar_57],[nvarchar_58],[nvarchar_59],[nvarchar_60]
		,[nvarchar_61],[nvarchar_62],[nvarchar_63],[nvarchar_64],[nvarchar_65],[nvarchar_66],[nvarchar_67],[nvarchar_68],[nvarchar_69],[nvarchar_70]
		,[nvarchar_71],[nvarchar_72],[nvarchar_73],[nvarchar_74],[nvarchar_75],[nvarchar_76],[nvarchar_77],[nvarchar_78],[nvarchar_79],[nvarchar_80]
		,[int_1],[int_2],[int_3],[int_4],[int_5],[int_6],[int_7],[int_8],[int_9],[int_10],[int_11],[int_12],[int_13],[int_14],[int_15],[int_16],[int_17],[int_18],[int_19],[int_20]
		,[int_21],[int_22],[int_23],[int_24],[int_25],[int_26],[int_27],[int_28],[int_29],[int_30],[int_31],[int_32],[int_33],[int_34],[int_35],[int_36],[int_37],[int_38],[int_39],[int_40]
		,[datetime_1],[datetime_2],[datetime_3],[datetime_4],[datetime_5],[datetime_6],[datetime_7],[datetime_8],[datetime_9],[datetime_10],[datetime_11],[datetime_12],[datetime_13]
		,[datetime_14],[datetime_15],[datetime_16],[datetime_17],[datetime_18],[datetime_19],[datetime_20],[datetime_21],[datetime_22],[datetime_23],[datetime_24],[datetime_25]
		,[money_1],[money_2],[money_3],[money_4],[money_5],[money_6],[money_7],[money_8],[money_9],[money_10],[money_11],[money_12],[money_13],[money_14],[money_15],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[FlatProperties]
SET IDENTITY_INSERT [dbo].[FlatProperties] OFF
GO

TRUNCATE TABLE [dbo].[TextPropertiesNVarchar]
GO
SET IDENTITY_INSERT [dbo].[TextPropertiesNVarchar] ON 
INSERT INTO [dbo].[TextPropertiesNVarchar]
	([TextPropertyNVarcharId],[VersionId],[PropertyTypeId],[Value],[RowGuid])
	SELECT [TextPropertyNVarcharId],[VersionId],[PropertyTypeId],[Value],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[TextPropertiesNVarchar]
SET IDENTITY_INSERT [dbo].[TextPropertiesNVarchar] OFF
GO

TRUNCATE TABLE [dbo].[TextPropertiesNText]
GO
SET IDENTITY_INSERT [dbo].[TextPropertiesNText] ON 
INSERT INTO [dbo].[TextPropertiesNText]
	([TextPropertyNTextId],[VersionId],[PropertyTypeId],[Value],[RowGuid])
	SELECT [TextPropertyNTextId],[VersionId],[PropertyTypeId],[Value],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[TextPropertiesNText]
SET IDENTITY_INSERT [dbo].[TextPropertiesNText] OFF
GO

DELETE FROM [dbo].[Files]
GO
SET IDENTITY_INSERT [dbo].[Files] ON 
INSERT INTO [dbo].[Files]
	([FileId],[ContentType],[FileNameWithoutExtension],[Extension],[Size],[Checksum],[CreationDate],[RowGuid],[Staging],[StagingVersionId],[StagingPropertyTypeId],[IsDeleted],[BlobProvider],[BlobProviderData])
	SELECT [FileId],[ContentType],[FileNameWithoutExtension],[Extension],[Size],[Checksum],[CreationDate],[RowGuid],[Staging],[StagingVersionId],[StagingPropertyTypeId],[IsDeleted],[BlobProvider],[BlobProviderData]
		FROM [devservice.demo.sensenet.com].[dbo].[Files]
SET IDENTITY_INSERT [dbo].[Files] OFF
GO

TRUNCATE TABLE [dbo].[BinaryProperties]
GO
SET IDENTITY_INSERT [dbo].[BinaryProperties] ON 
INSERT INTO [dbo].[BinaryProperties]
	([BinaryPropertyId], [VersionId],[PropertyTypeId],[FileId])
	SELECT [BinaryPropertyId], [VersionId],[PropertyTypeId],[FileId]
		FROM [devservice.demo.sensenet.com].[dbo].[BinaryProperties]
SET IDENTITY_INSERT [dbo].[BinaryProperties] OFF
GO

TRUNCATE TABLE [dbo].[ReferenceProperties]
GO
SET IDENTITY_INSERT [dbo].[ReferenceProperties] ON 
INSERT INTO [dbo].[ReferenceProperties]
	([ReferencePropertyId],[VersionId],[PropertyTypeId],[ReferredNodeId],[RowGuid])
	SELECT [ReferencePropertyId],[VersionId],[PropertyTypeId],[ReferredNodeId],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[ReferenceProperties]
SET IDENTITY_INSERT [dbo].[ReferenceProperties] OFF
GO

TRUNCATE TABLE [dbo].[Versions]
GO
SET IDENTITY_INSERT [dbo].[Versions] ON 
INSERT INTO [dbo].[Versions]
	([VersionId], [NodeId],[MajorNumber],[MinorNumber],[CreationDate],[CreatedById],[ModificationDate],[ModifiedById],[Status],[IndexDocument],[ChangedData],[RowGuid])
	SELECT [VersionId], [NodeId],[MajorNumber],[MinorNumber],[CreationDate],[CreatedById],[ModificationDate],[ModifiedById],[Status],[IndexDocument],[ChangedData],[RowGuid]
		FROM [devservice.demo.sensenet.com].[dbo].[Versions]
SET IDENTITY_INSERT [dbo].[Versions] OFF
GO
