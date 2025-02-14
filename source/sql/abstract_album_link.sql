USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[abstract_album_link];
GO

CREATE TABLE [dbo].[abstract_album_link](
    [id]                INT              IDENTITY (1, 1) NOT NULL,
    [album_id]          UNIQUEIDENTIFIER NOT NULL,
    [abstract_album_id] UNIQUEIDENTIFIER NOT NULL,
    [created_at]        DATETIME         NOT NULL,
    [modified_at]       DATETIME         NOT NULL,
 CONSTRAINT [PK_abstract_album_link] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'abstract_album_link',
 @level2type=N'COLUMN', @level2name=N'id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'アルバムID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'abstract_album_link',
 @level2type=N'COLUMN', @level2name=N'album_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'抽象アルバムID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'abstract_album_link',
 @level2type=N'COLUMN', @level2name=N'abstract_album_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'作成日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'abstract_album_link',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'abstract_album_link',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO

