INSERT INTO [LongTextProperties]
	SELECT [VersionId], [PropertyTypeId], DATALENGTH([Value]) [Length], [Value] FROM [TextPropertiesNVarchar]
GO
INSERT INTO [LongTextProperties]
	SELECT [VersionId], [PropertyTypeId], DATALENGTH([Value]) [Length], [Value] FROM [TextPropertiesNText]
GO
CREATE NONCLUSTERED INDEX [ix_version_id]
    ON [dbo].[LongTextProperties]([VersionId] ASC);
GO
