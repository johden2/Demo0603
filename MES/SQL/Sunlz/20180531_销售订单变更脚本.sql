/*-----1.�޸Ķ�����-------------------------------------------*/

--�޸ı��� 
EXEC sp_rename '[dbo].[Mes_Plan_SaleOrde]','Mes_Plan_SaleOrder'; 

--ALTER TABLE Mes_Plan_SaleOrder DROP COLUMN Num
ALTER TABLE Mes_Plan_SaleOrder DROP COLUMN CustCode
ALTER TABLE Mes_Plan_SaleOrder ADD CustomerID INT

EXECUTE sp_addextendedproperty N'MS_Description', N'����ʱ��', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'OrderDate' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�������', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'OrderAmount' 
EXECUTE sp_addextendedproperty N'MS_Description', N'������', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'PaymentAmount' 
EXECUTE sp_addextendedproperty N'MS_Description', N'���ʽ', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'PaymentType' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ͻ�ID', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'CustomerID' 
EXECUTE sp_addextendedproperty N'MS_Description', N'����״̬', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'OrderStatus' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ص���', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'Closer' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ص�ʱ��', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrder', N'column', N'CloseTime' 


EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrderItem', N'column', N'ShipDate' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ѷ�����', N'user', N'dbo',
			N'table', N'Mes_Plan_SaleOrderItem', N'column', N'AlNum' 



/*-----2.�ƻ�����-------------------------------------------*/
IF EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Plan_PlanInfor') and name='WorkShopCode')
BEGIN

ALTER TABLE Mes_Plan_PlanInfor  DROP COLUMN SaleNo;
ALTER TABLE Mes_Plan_PlanInfor  DROP COLUMN WorkShopCode;
ALTER TABLE Mes_Plan_PlanInfor  DROP COLUMN PlanType;
ALTER TABLE Mes_Plan_PlanInfor  ADD PlanType TINYINT;
ALTER TABLE Mes_Plan_PlanInfor  ADD OrderNo VARCHAR(50);
ALTER TABLE Mes_Plan_PlanInfor  ADD OrderType VARCHAR(20);
ALTER TABLE Mes_Plan_PlanInfor  ADD WorkShopID INT;

EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'Factory' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ����', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'PlanType' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ����', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'PlanCode' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�������', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'OrderNo' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'OrderType' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ����', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'MaterialProNo' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��Ʒ���', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'MaterialCode' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�汾', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'Version' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ�����', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'PlanNum' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�������', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'CompleteNum' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��λ', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'Unit' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'WorkShopID' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ���ʼʱ��', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'BeginTime' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ�����ʱ��', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'EndTime' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�ƻ�״̬', N'user', N'dbo',
			N'table', N'Mes_Plan_PlanInfor', N'column', N'PlanStatus' 

END
GO


