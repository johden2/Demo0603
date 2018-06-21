using System.Collections.Generic;
using System;
using System.Reflection;

namespace Sys.Model
{
    #region 状态类型参数

    #region 公共字典

    /// <summary>
    /// 是否类型状态
    /// </summary>
    public class YesNoType
    {
        /// <summary>
        /// 是
        /// </summary>
        [CustField("是")]
        public const int Yes = 1;

        /// <summary>
        /// 否
        /// </summary>
        [CustField("否")]
        public const int No = 2;
    }

    public class YesNoEnum
    {
        /// <summary>
        /// 是
        /// </summary>
        [CustField("是")]
        public const string Yes = "Y";

        /// <summary>
        /// 否
        /// </summary>
        [CustField("否")]
        public const string No = "N";
    }

    public class AuditEnum
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [CustField("未审核")]
        public const int No= 1;

        /// <summary>
        /// 已审核
        /// </summary>
        [CustField("已审核")]
        public const int Yes = 2;
    }


    public class HeaderType
    {
        /// <summary>
        /// 请选择
        /// </summary>
        [CustField("请选择")]
        public const int Select = 1;

        /// <summary>
        /// 全部
        /// </summary>
        [CustField("全部")]
        public const int All = 2;

        /// <summary>
        /// 空白
        /// </summary>
        [CustField("空白")]
        public const int Blank = 3;

    }

    #endregion 公共字典

    #region 基础管理

    /// <summary>
    /// 流程配置
    /// </summary>
    public class FlowBusinessType
    {
        /// <summary>
        /// 进货单审批
        /// </summary>
        [CustField("进货单审批")]
        public const string A = "InStockApproval";

    }

    #endregion 基础管理


    #region 订单、计划字典

    /// <summary>
    /// 订单单别
    /// </summary>
    public class OrderType
    {
        /// <summary>
        /// 正常工单
        /// </summary>
        [CustField("正常工单")]
        public const string ZC = "1";

        /// <summary>
        /// 临时工单
        /// </summary>
        [CustField("临时工单")]
        public const string LS = "10";
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// 已下单
        /// </summary>
        [CustField("已下单")]
        public const int YXD = 1;

        /// <summary>
        /// 已关单
        /// </summary>
        [CustField("已关单")]
        public const int YGD = 8;

        /// <summary>
        /// 已作废
        /// </summary>
        [CustField(true,"已作废")]
        public const int ZF = 20;
    }

    /// <summary>
    /// 付款方式
    /// </summary>
    public class PaymentType
    {
        /// <summary>
        /// 在线支付
        /// </summary>
        [CustField("在线支付")]
        public const int ZX = 1;
        /// <summary>
        /// 货到付款
        /// </summary>
        [CustField("货到付款")]
        public const int HDFK = 2;
        /// <summary>
        /// 邮寄
        /// </summary>
        [CustField("邮寄")]
        public const int YJ = 3;
        /// <summary>
        /// 其他
        /// </summary>
        [CustField("其他")]
        public const int QT = 10;
    }

    /// <summary>
    /// 计划状态
    /// </summary>
    public class PlanStatus
    {
        /// <summary>
        /// 已制定
        /// </summary>
        [CustField("已制定")]
        public const int A = 1;

        /// <summary>
        /// 已下达
        /// </summary>
        [CustField("已下达")]
        public const int B = 2;

        /// <summary>
        /// 已完工
        /// </summary>
        [CustField("已完工")]
        public const int E = 5;

        /// <summary>
        /// 已关闭
        /// </summary>
        [CustField(true, "已关闭")]
        public const int F = 6;
    }

    /// <summary>
    /// 计划类型
    /// </summary>
    public class PlanType
    {
        /// <summary>
        /// 正常计划
        /// </summary>
        [CustField("正常计划")]
        public const int A = 1;

        /// <summary>
        /// 客退计划
        /// </summary>
        [CustField("客退计划")]
        public const int B = 2;
    }

    #endregion 订单、计划字典

    #region 生产相关字典

    /// <summary>
    /// 批次属性
    /// </summary>
    public class TracePropertyStatus
    {
        [CustField("单件追溯")]
        public const string SG = "2";

        [CustField("批次追溯")]
        public const string BT = "0";

    }

    /// <summary>
    /// 物料分类属性
    /// </summary>
    public class MaterialClassStatus
    {
        [CustField("物料")]
        public const string WL = "0";

        [CustField("成品")]
        public const string CP = "1";
    }

    public class RecordStatus
    {
        [CustField("启用")]
        public const int Valid = 0;
        [CustField("禁用")]
        public const int NoValid = 1;
    }

    public class TestPropertyStatus
    {
        [CustField("合格数据")]
        public const int Passed = 0;

        [CustField("不合格数据")]
        public const int NoPassed = 1;
    }

    public class ProcessState
    {
        [CustField("装配")]
        public const int Zp = 1;
        [CustField("老化")]
        public const int Lh = 2;
        [CustField("调试")]
        public const int Ts = 3;
        [CustField("总装")]
        public const int Zz = 4;
        [CustField("电检")]
        public const int Dj = 5;
        [CustField("包装")]
        public const int Bz = 6;
    }

    public class RepairStatus
    {
        [CustField("正常")]
        public const int Normal = 1;
        [CustField("返修")]
        public const int Repair = 2;
    }

    #endregion 生产相关字典

    #region 仓库管理相关字典

    /// <summary>
    /// 检验状态
    /// </summary>
    public class CheckStatus
    {
        /// <summary>
        /// 未检验
        /// </summary>
        [CustField("未检验")]
        public const int No = 1;

        /// <summary>
        /// 已检验
        /// </summary>
        [CustField("已检验")]
        public const int Yes = 2;
    }

    /// <summary>
    /// 进货单据类别
    /// </summary>
    public class StockBillType
    {
        /// <summary>
        /// 进货单
        /// </summary>
        [CustField("进货单")]
        public const int A = 1;

        /// <summary>
        /// 委外进货单
        /// </summary>
        [CustField("委外进货单")]
        public const int B = 2;
    }

    /// <summary>
    /// 领料单据类别
    /// </summary>
    public class LLBillType
    {
        /// <summary>
        /// 领料单
        /// </summary>
        [CustField("领料单")]
        public const int A = 1;

        /// <summary>
        /// 异常领料单
        /// </summary>
        [CustField("异常领料单")]
        public const int B = 2;
    }

    #endregion 仓库管理相关字典

    #endregion  状态类型参数

    #region 通用状态解析

    public class StatusHelper
    {
        /// <summary>
        /// 读取状态类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<ItemModel> GetConstStatus<T>() where T : class
        {
            IList<ItemModel> list = new List<ItemModel>();
            ItemModel item = null;
            string description = string.Empty;

            Type type = typeof(T);
            foreach (FieldInfo f in type.GetFields())
            {
                CustFieldAttribute fieldlAtt = Attribute.GetCustomAttribute(f, typeof(CustFieldAttribute)) as CustFieldAttribute;
                if (fieldlAtt != null)
                {
                    description = fieldlAtt.Description;
                }
                item = new ItemModel() { Value = f.GetValue(null).ToString(), Text = description };
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 读取状态类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<ItemModel> GetConstStatus(string statusType)
        {
            IList<ItemModel> list = new List<ItemModel>();
            ItemModel item = null;
            string description = string.Empty;

            string fullName = typeof(StatusHelper).Namespace + "." + statusType;
            Type t = Type.GetType(fullName);
            foreach (FieldInfo f in t.GetFields())
            {
                CustFieldAttribute fieldAtt = Attribute.GetCustomAttribute(f, typeof(CustFieldAttribute)) as CustFieldAttribute;
                if (fieldAtt != null)
                {
                    description = fieldAtt.Description;
                }
                item = new ItemModel() { Value = f.GetValue(null).ToString(), Text = description };
                list.Add(item);
            }

            return list;
        }


        /// <summary>
        /// 获取指定状态的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetConstStatus<T>(string status) where T : class
        {
            string description = string.Empty;
            string value = string.Empty;

            Type t = typeof(T);
            foreach (FieldInfo f in t.GetFields())
            {
                value = f.GetValue(null).ToString();
                if (value != status) continue;

                var filedAtt = Attribute.GetCustomAttribute(f, typeof(CustFieldAttribute)) as CustFieldAttribute;
                if (filedAtt != null)
                {
                    return filedAtt.Description;
                }
            }

            return string.Empty;
        }

        public static string GetConstStatus<T>(object status) where T : class
        {
            if (status == null || status.ToString().Length == 0) return string.Empty;

            return GetConstStatus<T>(status.ToString());
        }

        /// <summary>
        /// 通过描述获取状态值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static string GetStatusByDescription<T>(string description) where T : class
        {
            string value = string.Empty;

            Type t = typeof(T);
            foreach (FieldInfo f in t.GetFields())
            {
                var filedAtt = Attribute.GetCustomAttribute(f, typeof(CustFieldAttribute)) as CustFieldAttribute;
                if (filedAtt != null && filedAtt.Description != description) continue;

                return f.GetValue(null).ToString();
            }

            return string.Empty;
        }


    }

     #endregion  通用标准参数


}




