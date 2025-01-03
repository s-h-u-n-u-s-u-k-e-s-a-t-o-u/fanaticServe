USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[label];
GO


CREATE TABLE [dbo].[label](
	[label_id] [uniqueidentifier] NOT NULL,
    [organization_id] [uniqueidentifier] NOT NULL,
    [name] nvarchar(256) NOT NULL,
	[created_at] [datetime] NOT NULL,
    [modified_at] [datetime] NULL,
 CONSTRAINT [PK_label] PRIMARY KEY CLUSTERED 
(
	[label_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'レーベルID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'label',
 @level2type=N'COLUMN', @level2name=N'label_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'組織ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'label',
 @level2type=N'COLUMN', @level2name=N'organization_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'名前' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'label',
 @level2type=N'COLUMN', @level2name=N'name'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'label',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'label',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO
