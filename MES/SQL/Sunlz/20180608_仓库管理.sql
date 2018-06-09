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


