USE [SN7_Upgrade];
GO

/***** INDEXES *****/

CREATE NONCLUSTERED INDEX [ix_Versions_NodeId]
    ON [dbo].[Versions]([NodeId] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_Versions_NodeId_MinorNumber_MajorNumber_Status]
    ON [dbo].[Versions]([NodeId] ASC, [MinorNumber] ASC, [Status] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_JournalItems]
    ON [dbo].[JournalItems]([When] DESC, [Wherewith] ASC);
GO

/***** DEFAULT CONSTRAINTS *****/

ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate];
GO
ALTER TABLE [dbo].[PropertyTypes]
    ADD DEFAULT ((0)) FOR [IsContentListProperty];
GO

/***** FOREIGN KEY CONSTRAINTS *****/

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_BinaryProperties_Versions') AND parent_object_id = OBJECT_ID(N'dbo.BinaryProperties'))
	ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
		ADD CONSTRAINT [FK_BinaryProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_Versions];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
	ALTER TABLE [dbo].[Versions] WITH NOCHECK
		ADD CONSTRAINT [FK_Versions_Nodes] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes];

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes_CreatedBy') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
	ALTER TABLE [dbo].[Versions] WITH NOCHECK
		ADD CONSTRAINT [FK_Versions_Nodes_CreatedBy] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Versions_Nodes_ModifiedBy') AND parent_object_id = OBJECT_ID(N'dbo.Versions'))
	ALTER TABLE [dbo].[Versions] WITH NOCHECK
		ADD CONSTRAINT [FK_Versions_Nodes_ModifiedBy] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_LongTextProperties_PropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.LongTextProperties'))
	ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
	    ADD CONSTRAINT [FK_LongTextProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_PropertyTypes];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_LongTextProperties_Versions') AND parent_object_id = OBJECT_ID(N'dbo.LongTextProperties'))
	ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
		ADD CONSTRAINT [FK_LongTextProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_Versions];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_NodeTypes_NodeTypes') AND parent_object_id = OBJECT_ID(N'dbo.NodeTypes'))
	ALTER TABLE [dbo].[NodeTypes] WITH NOCHECK
		ADD CONSTRAINT [FK_NodeTypes_NodeTypes] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_BinaryProperties_PropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.BinaryProperties'))
	ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
		ADD CONSTRAINT [FK_BinaryProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_PropertyTypes];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_Nodes_NodeTypes') AND parent_object_id = OBJECT_ID(N'dbo.Nodes'))
	ALTER TABLE [dbo].[Nodes] WITH NOCHECK
		ADD CONSTRAINT [FK_Nodes_NodeTypes] FOREIGN KEY ([NodeTypeId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO
ALTER TABLE [dbo].[Nodes] NOCHECK CONSTRAINT [FK_Nodes_NodeTypes];
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_ReferenceProperties_PropertyTypes') AND parent_object_id = OBJECT_ID(N'dbo.ReferenceProperties'))
	ALTER TABLE [dbo].[ReferenceProperties] WITH NOCHECK
		ADD CONSTRAINT [FK_ReferenceProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[ReferenceProperties] NOCHECK CONSTRAINT [FK_ReferenceProperties_PropertyTypes];
GO
