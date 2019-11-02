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

EXECUTE sp_refreshsqlmodule N'[dbo].[PermissionInfoView]';
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
