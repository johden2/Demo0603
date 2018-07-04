/*-------1.修改基础数据表---------------------------------*/

--(1)修改菜单模型表
IF EXISTS(SELECT * FROM syscolumns WHERE id=object_id('Mes_Sys_ModuleItem') and name='ControlName')
BEGIN
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ControlName
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ActionName
	ALTER TABLE Mes_Sys_ModuleItem DROP COLUMN ParentCode

	ALTER TABLE Mes_Sys_ModuleItem ADD ModuleCode VARCHAR(20)
	ALTER TABLE Mes_Sys_ModuleItem ADD SortNo INT
	ALTER TABLE Mes_Sys_ModuleItem ADD [Level] INT
	ALTER TABLE Mes_Sys_ModuleItem ADD UseType INT
	
	EXECUTE sp_addextendedproperty N'MS_Description', N'菜单编码', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'ModuleCode' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'排序', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'SortNo' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'菜单层级', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'Level' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'使用类型', N'user', N'dbo',
			N'table', N'Mes_Sys_ModuleItem', N'column', N'UseType' 
END
GO

--(2)写入菜单数据

SELECT * FROM Mes_Sys_ModuleItem WHERE UseType = 2
--DELETE FROM Mes_Sys_ModuleItem WHERE UseType = 2

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('01','基础资料',NULL,0,'admin',GETDATE(),'01',1,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0101','物料信息','BaseManagement/ProductInfo',0,'admin',GETDATE(),'0101',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0102','车间信息','BaseManagement/Workshop',0,'admin',GETDATE(),'0102',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0103','客户资料','BaseManagement/Customer',0,'admin',GETDATE(),'0103',2,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0104','供应商资料','BaseManagement/Supplier',0,'admin',GETDATE(),'0104',2,4,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0108','仓库信息','BaseManagement/Stock',0,'admin',GETDATE(),'0108',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0109','库位信息','BaseManagement/Alibrary',0,'admin',GETDATE(),'0109',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0110','采集点配置','BaseManagement/DataCollectPoint',0,'admin',GETDATE(),'0110',2,8,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('20','系统管理',NULL,0,'admin',GETDATE(),'20',1,20,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2001','用户管理','SysManagement/UserMgt',0,'admin',GETDATE(),'2001',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2002','角色管理','SysManagement/RoleMgt',0,'admin',GETDATE(),'2002',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2003','部门管理','SysManagement/OrganizationMgt',0,'admin',GETDATE(),'2003',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('2008','员工管理','PersonnelManagement/EmployeeMgt',0,'admin',GETDATE(),'2008',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('02','工艺管理',NULL,0,'admin',GETDATE(),'02',1,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0201','工艺库','ProcessManagement/Process',0,'admin',GETDATE(),'0201',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0202','配套明细管理','ProcessManagement/ProcessBomMgt',0,'admin',GETDATE(),'0202',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0203','测试数据校验配置','ProcessManagement/TestDataPosition',0,'admin',GETDATE(),'0203',2,2,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('03','计划管理',NULL,0,'admin',GETDATE(),'03',1,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0301','订单管理','PlanManagement/SaleOrderMgt',0,'admin',GETDATE(),'0301',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0302','计划管理','PlanManagement/PlanMgt',0,'admin',GETDATE(),'0302',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0303','工单管理','PlanManagement/WorkOrder',0,'admin',GETDATE(),'0303',2,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0304','工单物料消耗','PlanManagement/WorkOrderMaterial',0,'admin',GETDATE(),'0304',2,4,2)




INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('04','仓库管理',NULL,0,'admin',GETDATE(),'04',1,3,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0401','进货单','StockManagement/InStockMgt',0,'admin',GETDATE(),'0401',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0402','领料单','StockManagement/OutMaterialMgt',0,'admin',GETDATE(),'0402',2,2,2)
--INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
--VALUES('0403','异常领料单','StockManagement/YcOutMaterialMgt',0,'admin',GETDATE(),'0403',2,3,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0404','入库条码记录','StockManagement/TraInStock',0,'admin',GETDATE(),'0404',2,4,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0405','出库条码记录','StockManagement/TraOutStock',0,'admin',GETDATE(),'0405',2,5,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0406','生产入库单','StockManagement/ProductInStockMgt',0,'admin',GETDATE(),'0406',2,6,2)

--UPDATE Mes_Sys_ModuleItem SET WebRoute = 'StockManagement/ProductInStockMgt' WHERE WebRoute = 'StockManagement/ProductInStock'

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('05','工装夹具',NULL,0,'admin',GETDATE(),'05',1,5,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0501','工装夹具类型','DeviceManagement/DeviceType',0,'admin',GETDATE(),'0501',2,1,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0502','工装夹具资料','DeviceManagement/DeviceInfo',0,'admin',GETDATE(),'0502',2,2,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0503','工装夹具领用','DeviceManagement/DeviceOutRecord',0,'admin',GETDATE(),'0503',2,3,2)

INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0111','流程配置','BaseManagement/FlowConfig',0,'admin',GETDATE(),'0111',2,9,2)
INSERT INTO Mes_Sys_ModuleItem(Code,MenuName,WebRoute,NotShow,Creater,CreatedTime,ModuleCode,[Level],SortNo,UseType)
VALUES('0112','条码管理','BaseManagement/BarcodeInfo',0,'admin',GETDATE(),'0111',2,10,2)


--(3)修改员工资料表
ALTER TABLE Mes_Sys_Employee ADD ProcessNameList NVARCHAR(500)
EXECUTE sp_addextendedproperty N'MS_Description', N'员工编号', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'EmpNo' 
EXECUTE sp_addextendedproperty N'MS_Description', N'员工姓名', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'EmpName' 
EXECUTE sp_addextendedproperty N'MS_Description', N'岗位编码', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'PostCode' 
EXECUTE sp_addextendedproperty N'MS_Description', N'部门ID', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'OrgID' 
EXECUTE sp_addextendedproperty N'MS_Description', N'工序列表', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'ProcessList' 
EXECUTE sp_addextendedproperty N'MS_Description', N'工序名称列表', N'user', N'dbo',
			N'table', N'Mes_Sys_Employee', N'column', N'ProcessNameList' 

/*-------2.工艺管理---------------------------------*/
--(1)修改配套明细表
ALTER TABLE Mes_Tec_ProcessBomChangeLog DROP COLUMN SID
ALTER TABLE Mes_Tec_ProcessBomChangeLog ADD ProcessBomID INT
EXECUTE sp_addextendedproperty N'MS_Description', N'配套明细表主表ID', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomChangeLog', N'column', N'ProcessBomID' 

--(2)修改配套明细子表
ALTER TABLE Mes_Tec_ProcessBomItem ADD SubMaterialCode VARCHAR(50)
EXECUTE sp_addextendedproperty N'MS_Description', N'子物料简称', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomItem', N'column', N'SubMaterialCode' 


ALTER TABLE Mes_Tec_ProcessBomItem DROP COLUMN ProcessCode
ALTER TABLE Mes_Tec_ProcessBomItem ADD ProcessID INT
EXECUTE sp_addextendedproperty N'MS_Description', N'工艺ID', N'user', N'dbo',
			N'table', N'Mes_Tec_ProcessBomItem', N'column', N'ProcessID' 


















