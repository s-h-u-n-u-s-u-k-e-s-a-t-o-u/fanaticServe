USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[label];
GO

CREATE TABLE [dbo].[live_event](
	[live_event_id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](256) NOT NULL,
	[place] [nvarchar](256) NULL,
	[perform_at] [datetime] NULL,
	[created_at] [datetime] NOT NULL,
	[modified_at] [datetime] NOT NULL,
 CONSTRAINT [PK_live_event] PRIMARY KEY CLUSTERED 
(
	[live_event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'イベントID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'live_event_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'タイトル' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'title'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会場' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'place'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'開演日時' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'perform_at'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登録日時' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日時' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'live_event', @level2type=N'COLUMN',@level2name=N'modified_at'
GO


