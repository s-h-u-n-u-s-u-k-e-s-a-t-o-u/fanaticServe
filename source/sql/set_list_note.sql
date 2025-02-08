USE [Fanatic_Serve]
GO

/****** オブジェクト: Table [dbo].[set_list_note] スクリプト日付: 2025/02/04 19:14:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[set_list_note];
GO
CREATE TABLE [dbo].[set_list_note] (
    [set_list_id] UNIQUEIDENTIFIER NOT NULL,
    [note]        NVARCHAR (MAX)   NULL,
    [created_at]  DATETIME         NOT NULL,
    [modified_at] DATETIME         NOT NULL,

     CONSTRAINT [PK_set_list_note] PRIMARY KEY CLUSTERED 
(
	[set_list_id] ASC
)
);

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'セットリストID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list_note',
 @level2type=N'COLUMN', @level2name=N'set_list_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'ノート' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list_note',
 @level2type=N'COLUMN', @level2name=N'note'
GO
EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list_note',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list_note',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO