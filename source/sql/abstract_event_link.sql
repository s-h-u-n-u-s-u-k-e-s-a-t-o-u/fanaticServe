USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[abstract_event];
GO

CREATE TABLE [dbo].[abstract_event_link](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [uniqueidentifier] NOT NULL,
	[abstract_event_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_abstract_event_link] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'abstract_event_link', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'イベントID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'abstract_event_link', @level2type=N'COLUMN',@level2name=N'event_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抽象イベントID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'abstract_event_link', @level2type=N'COLUMN',@level2name=N'abstract_event_id'
GO
