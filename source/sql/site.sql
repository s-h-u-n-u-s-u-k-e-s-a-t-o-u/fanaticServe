USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[site];
GO

CREATE TABLE [dbo].[site](
    [id] int identity(1,1) NOT NULL,
	[site_id] [uniqueidentifier] NOT NULL,
    [sequence] [int] NOT NULL,
    [display_name] nvarchar(256) NOT NULL,
    [url] nvarchar(256) NOT NULL,
	[created_at] [datetime] NOT NULL,
    [modified_at] [datetime] NULL,
 CONSTRAINT [PK_site] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
constraint [UQ_site_site_id] unique ([site_id],[sequence])
) ON [PRIMARY]
GO


EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'レーベルID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'site_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'表示順' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'sequence'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'表示名前' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'display_name'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'url' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'url'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'site',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO
