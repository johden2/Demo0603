/*-----1.������Ա���ñ�-------------------------------------------*/
--(1)������Ա���ñ�:�����ڽ�����������
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
	
		EXECUTE sp_addextendedproperty N'MS_Description', N'������Ա���ñ�', N'user',
			N'dbo', N'table', N'Mes_Sys_FlowConfig', NULL, NULL
		EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'ID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'ҵ������', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'BusinessType' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'������ԱID', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'OptUserID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'������Ա', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'OptUserName' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'SortNo' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'������', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'Creater' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'����ʱ��', N'user', N'dbo',
			N'table', N'Mes_Sys_FlowConfig', N'column', N'CreatedTime' 

    END

GO


--(2)������������¼��
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
	
		EXECUTE sp_addextendedproperty N'MS_Description', N'������Ա���ñ�', N'user',
			N'dbo', N'table', N'Mes_Stock_InStockApproval', NULL, NULL
		EXECUTE sp_addextendedproperty N'MS_Description', N'����', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'BillNo' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'��������', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'BillType' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'�����ID', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApproverID' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'�����', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApproverName' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'���״̬(1:ͨ�� 2:��ͨ��)', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'ApprovalStatus' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'������', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'Creater' 
		EXECUTE sp_addextendedproperty N'MS_Description', N'����ʱ��', N'user', N'dbo',
			N'table', N'Mes_Stock_InStockApproval', N'column', N'CreatedTime' 

    END

GO



















