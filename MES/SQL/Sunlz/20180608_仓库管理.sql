/*-----1.�޸Ľ�������-------------------------------------------*/

IF NOT EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Stock_InStock') and name='SupplierID')
BEGIN
	ALTER TABLE Mes_Stock_InStock DROP COLUMN FactoryCode;
	ALTER TABLE Mes_Stock_InStock  ADD Factory NVARCHAR(50);
	ALTER TABLE Mes_Stock_InStock DROP COLUMN SupplierNo;
	ALTER TABLE Mes_Stock_InStock  ADD SupplierID INT;
	ALTER TABLE Mes_Stock_InStock DROP COLUMN CheckPerson;
	
	EXECUTE sp_addextendedproperty N'MS_Description', N'���������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����������(1-������;2-ί�������)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'InStockDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Stock_InStock', N'column', N'Factory' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ӧ��ID', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupplierID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ӧ��', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupplierName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ӧ�̵���', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SupBillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'BillDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����״̬(1-δ���;2-�����)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'AuditStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�ܽ�������', N'user', N'dbo',
			N'table', N'Mes_Stock_InStock', N'column', N'TotalInStockNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'TotalAcceptNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Դ����', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SourceBillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Դ����', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'SourceBillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����״̬(1-δ����;2-�Ѽ���)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStock', N'column', N'CheckStatus' 
	
END
GO

	ALTER TABLE Mes_Stock_InStock DROP COLUMN BillDate;
	ALTER TABLE Mes_Stock_InStock  ADD BillDate DATETIME;
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
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

	EXECUTE sp_addextendedproperty N'MS_Description', N'���������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����������(1-������;2-ί�������)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�����к�', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'ResNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ����', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialProNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ���', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialCode'
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ���', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'MaterialSize' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'InStockNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��λ', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'Unit' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�ֿ�ID', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'StockID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'��λID', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockItem', N'column', N'AlibraryID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'���κ�', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BatchNo' 	
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'BackNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'������(1-δ����;2-�Ѽ���)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'CheckItemStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'������(1-δ����;2-������)', N'user', N'dbo',
				N'table', N'Mes_Stock_InStockItem', N'column', N'AcceptStatus' 
	

	
	
END
GO


/*-----2.���ϵ���-------------------------------------------*/

--(1)��������
	ALTER TABLE Mes_Stock_OutMaterial  ADD OrgName NVARCHAR(50);
	ALTER TABLE Mes_Stock_OutMaterial DROP COLUMN FactoryCode;
	ALTER TABLE Mes_Stock_OutMaterial  ADD Factory NVARCHAR(50);

	EXECUTE sp_addextendedproperty N'MS_Description', N'���ϵ���', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'���ϵ���(1-���ϵ�;2-�쳣���ϵ�)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OutStockDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterial', N'column', N'Factory' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'BillDate' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����ID', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OrgID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'OrgName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����״̬(1-δ���;2-�����)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'AuditStatus' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterial', N'column', N'TotalNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ע', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterial', N'column', N'Memo' 


--(2)������ϸ��

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

	EXECUTE sp_addextendedproperty N'MS_Description', N'���ϵ���', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'���ϵ���(1-���ϵ�;2-�쳣���ϵ�)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�к�', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ResNum' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ����', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialProNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ���', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialCode'
	EXECUTE sp_addextendedproperty N'MS_Description', N'�汾', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Version' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ���', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'MaterialSize'
	EXECUTE sp_addextendedproperty N'MS_Description', N'��λ', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Unit' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�ֿ�ID', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'StockID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'�ֿ�', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'StockName'
	EXECUTE sp_addextendedproperty N'MS_Description', N'��λID', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'AlibraryID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��λ', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'AlibraryName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����ID', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ProcessID' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Stock_OutMaterialItem', N'column', N'ProcessName' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'���κ�(���������κŹ���)', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'BatchNo' 	
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ȡ����', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Num' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ�����', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'PlanNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ע', N'user', N'dbo',
				N'table', N'Mes_Stock_OutMaterialItem', N'column', N'Memo' 

GO


/*-----3.������ⵥ-------------------------------------------*/

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

EXECUTE sp_addextendedproperty N'MS_Description', N'������ⵥ��ϸ��', N'user', N'dbo',
				N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ⵥ����(1-��Ʒ������ⵥ;2-���Ʒ��ⵥ)', N'user', N'dbo',
				N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ⵥ����', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'BillNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'WorkOrderType' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'WorkOrderNumber'
	EXECUTE sp_addextendedproperty N'MS_Description', N'��ⵥID', N'user', N'dbo',
			N'table', N'Mes_Stock_ProductInStockItem', N'column', N'ProductInStockID'


