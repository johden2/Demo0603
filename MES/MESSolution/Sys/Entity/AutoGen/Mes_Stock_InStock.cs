﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace Sys.Model
{
    /// <summary>
    /// 实体类Mes_Stock_InStock。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Stock_InStock")]
    [Serializable]
    public partial class Mes_Stock_InStock : Entity
    {
        #region Model
        private int _ID;
        private string _BillType;
        private string _BillNo;
        private DateTime _InStockDate;
        private string _SupplierName;
        private string _SupBillNo;
        private int _AuditStatus;
        private int _TotalInStockNum;
        private int _TotalAcceptNum;
        private string _SourceBillType;
        private string _SourceBillNo;
        private int _CheckStatus;
        private string _Creater;
        private DateTime _CreatedTime;
        private string _Modifier;
        private DateTime? _ModifiedTime;
        private string _Factory;
        private int? _SupplierID;
        private DateTime? _BillDate;

        /// <summary>
        /// 
        /// </summary>
        [Field("ID")]
        public int ID
        {
            get { return _ID; }
            set
            {
                this.OnPropertyValueChange("ID");
                this._ID = value;
            }
        }
        /// <summary>
        /// XK01-进货单;WX01-委外进货单
        /// </summary>
        [Field("BillType")]
        public string BillType
        {
            get { return _BillType; }
            set
            {
                this.OnPropertyValueChange("BillType");
                this._BillType = value;
            }
        }
        /// <summary>
        /// 进货单编号
        /// </summary>
        [Field("BillNo")]
        public string BillNo
        {
            get { return _BillNo; }
            set
            {
                this.OnPropertyValueChange("BillNo");
                this._BillNo = value;
            }
        }
        /// <summary>
        /// 进货日期
        /// </summary>
        [Field("InStockDate")]
        public DateTime InStockDate
        {
            get { return _InStockDate; }
            set
            {
                this.OnPropertyValueChange("InStockDate");
                this._InStockDate = value;
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        [Field("SupplierName")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set
            {
                this.OnPropertyValueChange("SupplierName");
                this._SupplierName = value;
            }
        }
        /// <summary>
        /// 供应商单号
        /// </summary>
        [Field("SupBillNo")]
        public string SupBillNo
        {
            get { return _SupBillNo; }
            set
            {
                this.OnPropertyValueChange("SupBillNo");
                this._SupBillNo = value;
            }
        }
        /// <summary>
        /// _1-未审核（默认值);2-已审核
        /// </summary>
        [Field("AuditStatus")]
        public int AuditStatus
        {
            get { return _AuditStatus; }
            set
            {
                this.OnPropertyValueChange("AuditStatus");
                this._AuditStatus = value;
            }
        }
        /// <summary>
        /// 总进货数量
        /// </summary>
        [Field("TotalInStockNum")]
        public int TotalInStockNum
        {
            get { return _TotalInStockNum; }
            set
            {
                this.OnPropertyValueChange("TotalInStockNum");
                this._TotalInStockNum = value;
            }
        }
        /// <summary>
        /// 总验收数量
        /// </summary>
        [Field("TotalAcceptNum")]
        public int TotalAcceptNum
        {
            get { return _TotalAcceptNum; }
            set
            {
                this.OnPropertyValueChange("TotalAcceptNum");
                this._TotalAcceptNum = value;
            }
        }
        /// <summary>
        /// 来源单别
        /// </summary>
        [Field("SourceBillType")]
        public string SourceBillType
        {
            get { return _SourceBillType; }
            set
            {
                this.OnPropertyValueChange("SourceBillType");
                this._SourceBillType = value;
            }
        }
        /// <summary>
        /// 来源单号
        /// </summary>
        [Field("SourceBillNo")]
        public string SourceBillNo
        {
            get { return _SourceBillNo; }
            set
            {
                this.OnPropertyValueChange("SourceBillNo");
                this._SourceBillNo = value;
            }
        }
        /// <summary>
        /// _1-未检验;2-已检验
        /// </summary>
        [Field("CheckStatus")]
        public int CheckStatus
        {
            get { return _CheckStatus; }
            set
            {
                this.OnPropertyValueChange("CheckStatus");
                this._CheckStatus = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Creater")]
        public string Creater
        {
            get { return _Creater; }
            set
            {
                this.OnPropertyValueChange("Creater");
                this._Creater = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("CreatedTime")]
        public DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set
            {
                this.OnPropertyValueChange("CreatedTime");
                this._CreatedTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Modifier")]
        public string Modifier
        {
            get { return _Modifier; }
            set
            {
                this.OnPropertyValueChange("Modifier");
                this._Modifier = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("ModifiedTime")]
        public DateTime? ModifiedTime
        {
            get { return _ModifiedTime; }
            set
            {
                this.OnPropertyValueChange("ModifiedTime");
                this._ModifiedTime = value;
            }
        }
        /// <summary>
        /// 工厂
        /// </summary>
        [Field("Factory")]
        public string Factory
        {
            get { return _Factory; }
            set
            {
                this.OnPropertyValueChange("Factory");
                this._Factory = value;
            }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        [Field("SupplierID")]
        public int? SupplierID
        {
            get { return _SupplierID; }
            set
            {
                this.OnPropertyValueChange("SupplierID");
                this._SupplierID = value;
            }
        }
        /// <summary>
        /// 单据日期
        /// </summary>
        [Field("BillDate")]
        public DateTime? BillDate
        {
            get { return _BillDate; }
            set
            {
                this.OnPropertyValueChange("BillDate");
                this._BillDate = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.ID,
			};
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.ID;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.ID,
				_.BillType,
				_.BillNo,
				_.InStockDate,
				_.SupplierName,
				_.SupBillNo,
				_.AuditStatus,
				_.TotalInStockNum,
				_.TotalAcceptNum,
				_.SourceBillType,
				_.SourceBillNo,
				_.CheckStatus,
				_.Creater,
				_.CreatedTime,
				_.Modifier,
				_.ModifiedTime,
				_.Factory,
				_.SupplierID,
				_.BillDate,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._BillType,
				this._BillNo,
				this._InStockDate,
				this._SupplierName,
				this._SupBillNo,
				this._AuditStatus,
				this._TotalInStockNum,
				this._TotalAcceptNum,
				this._SourceBillType,
				this._SourceBillNo,
				this._CheckStatus,
				this._Creater,
				this._CreatedTime,
				this._Modifier,
				this._ModifiedTime,
				this._Factory,
				this._SupplierID,
				this._BillDate,
			};
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "Mes_Stock_InStock");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Stock_InStock", "");
            /// <summary>
            /// XK01-进货单;WX01-委外进货单
            /// </summary>
            public readonly static Field BillType = new Field("BillType", "Mes_Stock_InStock", "XK01-进货单;WX01-委外进货单");
            /// <summary>
            /// 进货单编号
            /// </summary>
            public readonly static Field BillNo = new Field("BillNo", "Mes_Stock_InStock", "进货单编号");
            /// <summary>
            /// 进货日期
            /// </summary>
            public readonly static Field InStockDate = new Field("InStockDate", "Mes_Stock_InStock", "进货日期");
            /// <summary>
            /// 供应商
            /// </summary>
            public readonly static Field SupplierName = new Field("SupplierName", "Mes_Stock_InStock", "供应商");
            /// <summary>
            /// 供应商单号
            /// </summary>
            public readonly static Field SupBillNo = new Field("SupBillNo", "Mes_Stock_InStock", "供应商单号");
            /// <summary>
            /// _1-未审核（默认值);2-已审核
            /// </summary>
            public readonly static Field AuditStatus = new Field("AuditStatus", "Mes_Stock_InStock", "_1-未审核（默认值);2-已审核");
            /// <summary>
            /// 总进货数量
            /// </summary>
            public readonly static Field TotalInStockNum = new Field("TotalInStockNum", "Mes_Stock_InStock", "总进货数量");
            /// <summary>
            /// 总验收数量
            /// </summary>
            public readonly static Field TotalAcceptNum = new Field("TotalAcceptNum", "Mes_Stock_InStock", "总验收数量");
            /// <summary>
            /// 来源单别
            /// </summary>
            public readonly static Field SourceBillType = new Field("SourceBillType", "Mes_Stock_InStock", "来源单别");
            /// <summary>
            /// 来源单号
            /// </summary>
            public readonly static Field SourceBillNo = new Field("SourceBillNo", "Mes_Stock_InStock", "来源单号");
            /// <summary>
            /// _1-未检验;2-已检验
            /// </summary>
            public readonly static Field CheckStatus = new Field("CheckStatus", "Mes_Stock_InStock", "_1-未检验;2-已检验");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Stock_InStock", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Stock_InStock", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Stock_InStock", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Stock_InStock", "");
            /// <summary>
            /// 工厂
            /// </summary>
            public readonly static Field Factory = new Field("Factory", "Mes_Stock_InStock", "工厂");
            /// <summary>
            /// 供应商ID
            /// </summary>
            public readonly static Field SupplierID = new Field("SupplierID", "Mes_Stock_InStock", "供应商ID");
            /// <summary>
            /// 单据日期
            /// </summary>
            public readonly static Field BillDate = new Field("BillDate", "Mes_Stock_InStock", "单据日期");
        }
        #endregion
    }
}