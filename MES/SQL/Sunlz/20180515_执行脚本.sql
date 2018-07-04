/*-------1.�޸Ļ������ݱ�---------------------------------*/

--(1)�޸Ĳ˵�ģ�ͱ�
IF EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Sys_ModuleItem') and name='ControlName')
BEGIN
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ControlName
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ActionName
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ParentCode

	ALTER TABLE Mes_Sys_ModuleItem ADD ModuleCode VARCHAR(20)
	ALTER TABLE Mes_Sys_ModuleItem ADD SortNo INT
	ALTER TABLE Mes_Sys_ModuleItem ADD [Level] INT
	ALTER TABLE Mes_Sys_ModuleItem ADD UseType INT
	
	EXECUTE sp_addextendedproperty N'MS_Description', N'�˵�����', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'ModuleCode' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'SortNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'�˵��㼶', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'Level' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'ʹ������', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'UseType' 
END
GO

--(2)д��˵�����

SELECT * FROM Mes_Sys_ModuleItem WHERE UseType = 2
--DELETE FROM Mes_Sys_ModuleItem WHERE UseType = 2

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('01','��������',NULL,0,'admin',GETDATE(),'01',1,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0101','������Ϣ','BaseManagement/ProductInfo',0,'admin',GETDATE(),'0101',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0102','������Ϣ','BaseManagement/Workshop',0,'admin',GETDATE(),'0102',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0103','�ͻ�����','BaseManagement/Customer',0,'admin',GETDATE(),'0103',2,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0104','��Ӧ������','BaseManagement/Supplier',0,'admin',GETDATE(),'0104',2,4,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0108','�ֿ���Ϣ','BaseManagement/Stock',0,'admin',GETDATE(),'0108',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0109','��λ��Ϣ','BaseManagement/Alibrary',0,'admin',GETDATE(),'0109',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0110','�ɼ�������','BaseManagement/DataCollectPoint',0,'admin',GETDATE(),'0110',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('20','ϵͳ����',NULL,0,'admin',GETDATE(),'20',1,20,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2001','�û�����','SysManagement/UserMgt',0,'admin',GETDATE(),'2001',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2002','��ɫ����','SysManagement/RoleMgt',0,'admin',GETDATE(),'2002',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2003','���Ź���','SysManagement/OrganizationMgt',0,'admin',GETDATE(),'2003',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2008','Ա������','PersonnelManagement/EmployeeMgt',0,'admin',GETDATE(),'2008',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('02','���չ���',NULL,0,'admin',GETDATE(),'02',1,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0201','���տ�','ProcessManagement/Process',0,'admin',GETDATE(),'0201',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0202','������ϸ����','ProcessManagement/ProcessBomMgt',0,'admin',GETDATE(),'0202',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0203','��������У������','ProcessManagement/TestDataPosition',0,'admin',GETDATE(),'0203',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('03','�ƻ�����',NULL,0,'admin',GETDATE(),'03',1,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0301','��������','PlanManagement/SaleOrderMgt',0,'admin',GETDATE(),'0301',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0302','�ƻ�����','PlanManagement/PlanMgt',0,'admin',GETDATE(),'0302',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0303','��������','PlanManagement/WorkOrder',0,'admin',GETDATE(),'0303',2,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0304','������������','PlanManagement/WorkOrderMaterial',0,'admin',GETDATE(),'0304',2,4,2)




INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('04','�ֿ����',NULL,0,'admin',GETDATE(),'04',1,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0401','������','StockManagement/InStockMgt',0,'admin',GETDATE(),'0401',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0402','���ϵ�','StockManagement/OutMaterialMgt',0,'admin',GETDATE(),'0402',2,2,2)
--INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
--VALUES('0403','�쳣���ϵ�','StockManagement/YcOutMaterialMgt',0,'admin',GETDATE(),'0403',2,3,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0404','��������¼','StockManagement/TraInStock',0,'admin',GETDATE(),'0404',2,4,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0405','���������¼','StockManagement/TraOutStock',0,'admin',GETDATE(),'0405',2,5,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0406','������ⵥ','StockManagement/ProductInStockMgt',0,'admin',GETDATE(),'0406',2,6,2)

--UPDATE Mes_Sys_ModuleItem SET WebRoute = 'StockManagement/ProductInStockMgt' WHERE WebRoute = 'StockManagement/ProductInStock'

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('05','��װ�о�',NULL,0,'admin',GETDATE(),'05',1,5,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0501','��װ�о�����','DeviceManagement/DeviceType',0,'admin',GETDATE(),'0501',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0502','��װ�о�����','DeviceManagement/DeviceInfo',0,'admin',GETDATE(),'0502',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0503','��װ�о�����','DeviceManagement/DeviceOutRecord',0,'admin',GETDATE(),'0503',2,3,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0111','��������','BaseManagement/FlowConfig',0,'admin',GETDATE(),'0111',2,9,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0112','�������','BaseManagement/BarcodeInfo',0,'admin',GETDATE(),'0111',2,10,2)


--(3)�޸�Ա�����ϱ�
ALTER TABLE Mes_Sys_Employee ADD ProcessNameList NVARCHAR(500)
EXECUTE sp_addextendedproperty N'MS_Description', N'Ա�����', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'EmpNo' 
EXECUTE sp_addextendedproperty N'MS_Description', N'Ա������', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'EmpName' 
EXECUTE sp_addextendedproperty N'MS_Description', N'��λ����', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'PostCode' 
EXECUTE sp_addextendedproperty N'MS_Description', N'����ID', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'OrgID' 
EXECUTE sp_addextendedproperty N'MS_Description', N'�����б�', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'ProcessList' 
EXECUTE sp_addextendedproperty N'MS_Description', N'���������б�', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'ProcessNameList' 

/*-------2.���չ���---------------------------------*/
--(1)�޸�������ϸ��
ALTER TABLE Mes_Tec_ProcessBomChangeLog DROP COLUMN SID
ALTER TABLE Mes_Tec_ProcessBomChangeLog ADD ProcessBomID INT
EXECUTE sp_addextendedproperty N'MS_Description', N'������ϸ������ID', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomChangeLog', N'column', N'ProcessBomID' 

--(2)�޸�������ϸ�ӱ�
ALTER TABLE Mes_Tec_ProcessBomItem ADD SubMaterialCode VARCHAR(50)
EXECUTE sp_addextendedproperty N'MS_Description', N'�����ϼ��', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomItem', N'column', N'SubMaterialCode' 


ALTER TABLE Mes_Tec_ProcessBomItem DROP COLUMN ProcessCode
ALTER TABLE Mes_Tec_ProcessBomItem ADD ProcessID INT
EXECUTE sp_addextendedproperty N'MS_Description', N'����ID', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomItem', N'column', N'ProcessID' 


















