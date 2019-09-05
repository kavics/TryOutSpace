USE [SN7_Upgrade];
GO

CREATE NONCLUSTERED INDEX [ix_Versions_NodeId]
    ON [dbo].[Versions]([NodeId] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_Versions_NodeId_MinorNumber_MajorNumber_Status]
    ON [dbo].[Versions]([NodeId] ASC, [MinorNumber] ASC, [Status] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_JournalItems]
    ON [dbo].[JournalItems]([When] DESC, [Wherewith] ASC);
GO

ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate];
GO
ALTER TABLE [dbo].[PropertyTypes]
    ADD DEFAULT ((0)) FOR [IsContentListProperty];
GO
ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_BinaryProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_Versions];
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes];
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes_CreatedBy] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_CreatedBy];
GO
ALTER TABLE [dbo].[Versions] WITH NOCHECK
    ADD CONSTRAINT [FK_Versions_Nodes_ModifiedBy] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[Nodes] ([NodeId]);
GO
ALTER TABLE [dbo].[Versions] NOCHECK CONSTRAINT [FK_Versions_Nodes_ModifiedBy];
GO
ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_LongTextProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_PropertyTypes];
GO
ALTER TABLE [dbo].[LongTextProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_LongTextProperties_Versions] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[Versions] ([VersionId]);
GO
ALTER TABLE [dbo].[LongTextProperties] NOCHECK CONSTRAINT [FK_LongTextProperties_Versions];
GO
ALTER TABLE [dbo].[NodeTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_NodeTypes_NodeTypes] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO
ALTER TABLE [dbo].[BinaryProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_BinaryProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[BinaryProperties] NOCHECK CONSTRAINT [FK_BinaryProperties_PropertyTypes];
GO
ALTER TABLE [dbo].[Nodes] WITH NOCHECK
    ADD CONSTRAINT [FK_Nodes_NodeTypes] FOREIGN KEY ([NodeTypeId]) REFERENCES [dbo].[NodeTypes] ([NodeTypeId]);
GO
ALTER TABLE [dbo].[Nodes] NOCHECK CONSTRAINT [FK_Nodes_NodeTypes];
GO
ALTER TABLE [dbo].[ReferenceProperties] WITH NOCHECK
    ADD CONSTRAINT [FK_ReferenceProperties_PropertyTypes] FOREIGN KEY ([PropertyTypeId]) REFERENCES [dbo].[PropertyTypes] ([PropertyTypeId]);
GO
ALTER TABLE [dbo].[ReferenceProperties] NOCHECK CONSTRAINT [FK_ReferenceProperties_PropertyTypes];
GO
