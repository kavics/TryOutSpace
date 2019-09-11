﻿ALTER TABLE [dbo].[AccessTokens] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[AccessTokens] ALTER COLUMN [ExpirationDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[Files] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[IndexingActivities] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[IndexingActivities] ALTER COLUMN [LockTime] DATETIME2 (7) NULL;
GO
ALTER TABLE [dbo].[JournalItems] ALTER COLUMN [When] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[LogEntries] ALTER COLUMN [LogDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [LastLockUpdate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [LockDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Nodes] ALTER COLUMN [ModificationDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[Packages] ALTER COLUMN [ExecutionDate] DATETIME2 (7) NOT NULL;
ALTER TABLE [dbo].[Packages] ALTER COLUMN [ReleaseDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[SharedLocks] ALTER COLUMN [CreationDate] DATETIME2 (7) NOT NULL;
GO
ALTER TABLE [dbo].[TreeLocks] ALTER COLUMN [LockedAt] DATETIME2 (7) NOT NULL;
GO
