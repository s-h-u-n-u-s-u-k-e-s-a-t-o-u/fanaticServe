USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[roleOnSong];
GO

CREATE TABLE [dbo].[roleOnSong](
	[id] int identity(1,1) NOT NULL,
	[song_id] [uniqueidentifier] NOT NULL,
    [role_id] [uniqueidentifier] NOT NULL,
    [person_id] [uniqueidentifier] NOT NULL,
	[created_at] [datetime] NOT NULL,
    [modified_at] [datetime] NOT NULL,
 CONSTRAINT [PK_roleOnSong] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'楽曲ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'roleOnSong',
 @level2type=N'COLUMN', @level2name=N'song_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'役割ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'roleOnSong',
 @level2type=N'COLUMN', @level2name=N'role_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'人物ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'roleOnSong',
 @level2type=N'COLUMN', @level2name=N'person_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'roleOnSong',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'roleOnSong',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO
