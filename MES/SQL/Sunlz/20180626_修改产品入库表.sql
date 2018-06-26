
DROP TABLE Mes_Stock_ProductInStock
DROP TABLE Mes_Stock_ProductInStockItem

CREATE TABLE [dbo].[Mes_Stock_ProductInStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillType] [varchar](20) NOT NULL,
	[BillNo] [varchar](50) NOT NULL,
	[InStockDate] [datetime] NOT NULL,
	[AuditStatus] [int] NOT NULL,
	[TotalNum] [int] NOT NULL,
	[Memo] [varchar](100) NULL,
	[Creater] [varchar](20) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modifier] [varchar](20) NULL,
	[ModifiedTime] [datetime] NULL,
	[Factory] [nvarchar](50) NULL,
	[BillDate] [datetime] NULL,
 CONSTRAINT [PK_Mes_Stock_ProductInStock_1] PRIMARY KEY CLUSTERED 
(
	[BillType] ASC,
	[BillNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CP01-��Ʒ������ⵥ;CP02-���Ʒ��ⵥ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStock', @level2type=N'COLUMN',@level2name=N'BillType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-δ���;2-�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStock', @level2type=N'COLUMN',@level2name=N'AuditStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ǽ��������Ϣ,��Ϊ��������ͷ�͵���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStock'
GO

ALTER TABLE [dbo].[Mes_Stock_ProductInStock] ADD  CONSTRAINT [DF_Mes_Stock_ProductInStock_AuditStatus]  DEFAULT ((1)) FOR [AuditStatus]
GO

ALTER TABLE [dbo].[Mes_Stock_ProductInStock] ADD  CONSTRAINT [DF_Mes_Stock_ProductInStock_TotalNum]  DEFAULT ((0)) FOR [TotalNum]
GO




CREATE TABLE [dbo].[Mes_Stock_ProductInStockItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillType] [varchar](20) NOT NULL,
	[BillNo] [varchar](50) NOT NULL,
	[ResNum] [int] NOT NULL,
	[MaterialProNo] [varchar](50) NOT NULL,
	[MaterialCode] [varchar](50) NOT NULL,
	[MaterialSize] [varchar](50) NULL,
	[Num] [int] NOT NULL,
	[Unit] [varchar](20) NULL,
	[WorkOrderType] [varchar](20) NULL,
	[Memo] [varchar](100) NULL,
	[Creater] [varchar](20) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modifier] [varchar](20) NULL,
	[ModifiedTime] [datetime] NULL,
	[WorkOrderNumber] [varchar](50) NULL,
	[ProductInStockID] [int] NULL,
	[StockID] [int] NULL,
	[AlibraryID] [int] NULL,
	[Version] [varchar](20) NULL,
 CONSTRAINT [PK_Mes_Stock_ProductInStockItem] PRIMARY KEY CLUSTERED 
(
	[BillType] ASC,
	[BillNo] ASC,
	[ResNum] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CP01-��Ʒ������ⵥ;CP02-���Ʒ��ⵥ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStockItem', @level2type=N'COLUMN',@level2name=N'BillType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ⵥ��ϸ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStockItem', @level2type=N'COLUMN',@level2name=N'BillNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStockItem', @level2type=N'COLUMN',@level2name=N'WorkOrderType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStockItem', @level2type=N'COLUMN',@level2name=N'WorkOrderNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ⵥID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mes_Stock_ProductInStockItem', @level2type=N'COLUMN',@level2name=N'ProductInStockID'
GO

ALTER TABLE [dbo].[Mes_Stock_ProductInStockItem] ADD  CONSTRAINT [DF_Mes_Stock_ProductInStockItem_Num]  DEFAULT ((0)) FOR [Num]
GO

