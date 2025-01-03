USE [Fanatic_Serve]
GO

/****** Object:  Table [dbo].[ID]    Script Date: 2025/01/01 22:09:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[ID];
GO

CREATE TABLE [dbo].[ID](
	[id] [uniqueidentifier] NOT NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_ID] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'ID',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO


