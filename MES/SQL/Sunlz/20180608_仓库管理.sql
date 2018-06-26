/*-----1.修改进货单表-------------------------------------------*/

IF NOT EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Stock_InStock') and name='SupplierID')
BEGIN
	ALTER TABLE Mes_Stock_InStock DROP COLUMN FactoryCode;
	ALTER TABLE Mes_Stock_InStock  ADD Factory NVARCHAR(50);
	ALTER TABLE Mes_Stock_InStock DROP COLUMN SupplierNo;
	ALTER TABLE Mes_Stock_InStock  ADD SupplierID INT;
	ALTER TABLE Mes_Stock_InStock DROP COLUMN CheckPerson;
	
	EXECUTE sp_addextendedproperty N'MS_Description', N'进货单编号', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'进货单单别(1-进货单;2-委外进货单)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'进货日期', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'InStockDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工厂', N'user', N'dbo',
			N'table', N'Mes_Stock_InStock', N'column', N'Factory' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'供应商ID', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupplierID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'供应商', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupplierName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'供应商单号', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupBillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据日期', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据状态(1-未审核;2-已审核)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'AuditStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'总进货数量', N'user', N'dbo',
			N'table', N'Mes_Stock_InStock', N'column', N'TotalInStockNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'总验收数量', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'TotalAcceptNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'来源单别', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SourceBillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'来源单号', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SourceBillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'检验状态(1-未检验;2-已检验)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'CheckStatus' 
	
END
GO

	ALTER TABLE Mes_Stock_InStock DROP COLUMN BillDate;
	ALTER TABLE Mes_Stock_InStock  ADD BillDate DATETIME;
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据日期', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillDate' ;

IF NOT EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Stock_InStockItem') and name='InStockID')
BEGIN

	ALTER TABLE Mes_Stock_InStockItem  ADD InStockID INT;
	ALTER TABLE Mes_Stock_InStockItem DROP COLUMN StockCode;
	ALTER TABLE Mes_Stock_InStockItem  ADD StockID INT;
	ALTER TABLE Mes_Stock_InStockItem DROP COLUMN AlibraryCode;
	ALTER TABLE Mes_Stock_InStockItem  ADD AlibraryID INT;
	--ALTER TABLE Mes_Stock_InStockItem DROP COLUMN CheckStatus;
	ALTER TABLE Mes_Stock_InStockItem  ADD CheckItemStatus INT;
	ALTER TABLE Mes_Stock_InStockItem  ADD Version VARCHAR(20);

	EXECUTE sp_addextendedproperty N'MS_Description', N'进货单编号', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'进货单单别(1-进货单;2-委外进货单)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据行号', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'ResNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品编码', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialProNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品简称', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialCode'
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品规格', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialSize' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'进货数量', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'InStockNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单位', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'Unit' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'仓库ID', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'StockID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'库位ID', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'AlibraryID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'批次号', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BatchNo' 	
	EXECUTE sp_addextendedproperty N'MS_Description', N'验收数量', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'验收日期', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'验退数量', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BackNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'检验码(1-未检验;2-已检验)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'CheckItemStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'验收码(1-未验收;2-已验收)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptStatus' 
	

	
	
END
GO


/*-----2.领料单表-------------------------------------------*/

--(1)领料主表
	ALTER TABLE Mes_Stock_OutMaterial  ADD OrgName NVARCHAR(50);
	ALTER TABLE Mes_Stock_OutMaterial DROP COLUMN FactoryCode;
	ALTER TABLE Mes_Stock_OutMaterial  ADD Factory NVARCHAR(50);

	EXECUTE sp_addextendedproperty N'MS_Description', N'领料单号', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'领料单别(1-领料单;2-异常领料单)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'领料日期', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OutStockDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工厂', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterial', N'column', N'Factory' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据日期', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'班组ID', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OrgID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'班组', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OrgName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'单据状态(1-未审核;2-已审核)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'AuditStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'领料数量', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterial', N'column', N'TotalNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'备注', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'Memo' 


--(2)领料明细表

	ALTER TABLE Mes_Stock_OutMaterialItem  ADD OutMaterialID INT;
	ALTER TABLE Mes_Stock_OutMaterialItem DROP COLUMN StockCode;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD StockID INT;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD StockName NVARCHAR(50);
	ALTER TABLE Mes_Stock_OutMaterialItem DROP COLUMN AlibraryCode;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD AlibraryID INT;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD AlibraryName NVARCHAR(50);
	ALTER TABLE Mes_Stock_OutMaterialItem DROP COLUMN ProcessCode;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD ProcessID INT;
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD ProcessName NVARCHAR(50);
	ALTER TABLE Mes_Stock_OutMaterialItem  ADD Version VARCHAR(20);

	EXECUTE sp_addextendedproperty N'MS_Description', N'领料单号', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'领料单别(1-领料单;2-异常领料单)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'行号', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ResNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品编码', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialProNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品简称', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialCode'
	EXECUTE sp_addextendedproperty N'MS_Description', N'版本', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Version' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'产品规格', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialSize'
	EXECUTE sp_addextendedproperty N'MS_Description', N'单位', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Unit' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'仓库ID', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'StockID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'仓库', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'StockName'
	EXECUTE sp_addextendedproperty N'MS_Description', N'库位ID', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'AlibraryID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'库位', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'AlibraryName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工序ID', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ProcessID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工序', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ProcessName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'批次号(进货单批次号关联)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BatchNo' 	
	EXECUTE sp_addextendedproperty N'MS_Description', N'领取数量', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Num' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'计划批号', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'PlanNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'备注', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Memo' 

GO


/*-----3.生产入库单-------------------------------------------*/

ALTER TABLE Mes_Stock_ProductInStock DROP COLUMN FactoryCode;
ALTER TABLE Mes_Stock_ProductInStock DROP COLUMN BillDate;
ALTER TABLE Mes_Stock_ProductInStock  ADD Factory NVARCHAR(50);
ALTER TABLE Mes_Stock_ProductInStock  ADD BillDate DATETIME;


ALTER TABLE Mes_Stock_ProductInStockItem DROP COLUMN PlanNo;
ALTER TABLE Mes_Stock_ProductInStockItem DROP COLUMN StockCode;
ALTER TABLE Mes_Stock_ProductInStockItem DROP COLUMN AlibraryCode;
--ALTER TABLE Mes_Stock_ProductInStockItem  ADD WorkOrderType varchar(20);
ALTER TABLE Mes_Stock_ProductInStockItem  ADD WorkOrderNumber varchar(50);
ALTER TABLE Mes_Stock_ProductInStockItem  ADD StockID INT;
ALTER TABLE Mes_Stock_ProductInStockItem  ADD ProductInStockID INT;
ALTER TABLE Mes_Stock_ProductInStockItem  ADD StockID INT;
ALTER TABLE Mes_Stock_ProductInStockItem  ADD AlibraryID INT;
ALTER TABLE Mes_Stock_ProductInStockItem  ADD Version VARCHAR(20);

EXECUTE sp_addextendedproperty N'MS_Description', N'生产入库单明细表', N'user', N'dbo',
				N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'入库单单别(1-成品生产入库单;2-半成品入库单)', N'user', N'dbo',
				N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'入库单单号', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工单单别', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'WorkOrderType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'工单单号', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'WorkOrderNumber'
	EXECUTE sp_addextendedproperty N'MS_Description', N'入库单ID', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'ProductInStockID'


