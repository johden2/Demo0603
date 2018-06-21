/*-----1.流程人员配置表-------------------------------------------*/
--(1)流程人员配置表:可用于进货单审批表
IF NOT EXISTS ( SELECT  *
            FROM    dbo.SysObjects
            WHERE   ID = OBJECT_ID(N'[Mes_Sys_FlowConfig]') ) 
    BEGIN

        CREATE TABLE Mes_Sys_FlowConfig
            (
              ID INT IDENTITY(1, 1)
                     PRIMARY KEY ,
              BusinessType VARCHAR(20) ,
              OptUserID INT,
              OptUserName NVARCHAR(20),
              SortNo INT,
			  [Creater] [varchar](20) NOT NULL,
			  [CreatedTime] [datetime] NOT NULL,
            )
	
		EXECUTE sp_addextendedproperty N'MS_Description', N'流程人员配置表', N'user',
			N'dbo', N'table', N'Mes_Sys_FlowConfig', NULL, NULL
		EXECUTE sp_addextendedproperty N'MS_Description', N'主键', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'ID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'业务类型', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'BusinessType' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'操作人员ID', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'OptUserID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'操作人员', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'OptUserName' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'排序', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'SortNo' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'创建人', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'Creater' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'创建时间', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'CreatedTime' 

    END

GO


--(2)进货单审批记录表
--DROP TABLE Mes_Sys_FlowConfig
IF NOT EXISTS ( SELECT  *
            FROM    dbo.SysObjects
            WHERE   ID = OBJECT_ID(N'[Mes_Stock_InStockApproval]') ) 
    BEGIN

        CREATE TABLE Mes_Stock_InStockApproval
            (
              ID INT IDENTITY(1, 1)
                     PRIMARY KEY ,
              BillNo VARCHAR(50) ,
              BillType VARCHAR(20) ,
              ApproverID INT,
              ApproverName NVARCHAR(20),
              Memo NVARCHAR(100),
              ApprovalStatus INT,
			  [Creater] [varchar](20) NOT NULL,
			  [CreatedTime] [datetime] NOT NULL,
            )
	
		EXECUTE sp_addextendedproperty N'MS_Description', N'流程人员配置表', N'user',
			N'dbo', N'table', N'Mes_Stock_InStockApproval', NULL, NULL
		EXECUTE sp_addextendedproperty N'MS_Description', N'主键', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'进货单号', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'BillNo' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'进货单别', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'BillType' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'审核人ID', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApproverID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'审核人', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApproverName' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'审核状态(1:通过 2:不通过)', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApprovalStatus' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'创建人', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'Creater' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'创建时间', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'CreatedTime' 

    END

GO



















