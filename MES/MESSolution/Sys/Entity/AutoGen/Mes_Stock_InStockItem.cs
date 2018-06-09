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
    /// 实体类Mes_Stock_InStockItem。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Stock_InStockItem")]
    [Serializable]
    public partial class Mes_Stock_InStockItem : Entity
    {
        #region Model
        private int _ID;
        private string _BillType;
        private string _BillNo;
        private int _ResNum;
        private string _MaterialProNo;
        private string _MaterialCode;
        private string _MaterialSize;
        private int _InStockNum;
        private string _Unit;
        private string _BatchNo;
        private int _AcceptNum;
        private DateTime? _AcceptDate;
        private int _BackNum;
        private int _AcceptStatus;
        private int? _InStockID;
        private int? _StockID;
        private int? _AlibraryID;
        private int? _CheckItemStatus;
        private string _Version;

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
        /// 单据行号
        /// </summary>
        [Field("ResNum")]
        public int ResNum
        {
            get { return _ResNum; }
            set
            {
                this.OnPropertyValueChange("ResNum");
                this._ResNum = value;
            }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        [Field("MaterialProNo")]
        public string MaterialProNo
        {
            get { return _MaterialProNo; }
            set
            {
                this.OnPropertyValueChange("MaterialProNo");
                this._MaterialProNo = value;
            }
        }
        /// <summary>
        /// 产品简称
        /// </summary>
        [Field("MaterialCode")]
        public string MaterialCode
        {
            get { return _MaterialCode; }
            set
            {
                this.OnPropertyValueChange("MaterialCode");
                this._MaterialCode = value;
            }
        }
        /// <summary>
        /// 产品规格
        /// </summary>
        [Field("MaterialSize")]
        public string MaterialSize
        {
            get { return _MaterialSize; }
            set
            {
                this.OnPropertyValueChange("MaterialSize");
                this._MaterialSize = value;
            }
        }
        /// <summary>
        /// 进货数量
        /// </summary>
        [Field("InStockNum")]
        public int InStockNum
        {
            get { return _InStockNum; }
            set
            {
                this.OnPropertyValueChange("InStockNum");
                this._InStockNum = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        [Field("Unit")]
        public string Unit
        {
            get { return _Unit; }
            set
            {
                this.OnPropertyValueChange("Unit");
                this._Unit = value;
            }
        }
        /// <summary>
        /// 同一批产品一个批号，同一天多个批号,批号原则:INYYYYMMDDHHMMSSS
        /// </summary>
        [Field("BatchNo")]
        public string BatchNo
        {
            get { return _BatchNo; }
            set
            {
                this.OnPropertyValueChange("BatchNo");
                this._BatchNo = value;
            }
        }
        /// <summary>
        /// 验收数量
        /// </summary>
        [Field("AcceptNum")]
        public int AcceptNum
        {
            get { return _AcceptNum; }
            set
            {
                this.OnPropertyValueChange("AcceptNum");
                this._AcceptNum = value;
            }
        }
        /// <summary>
        /// 验收日期
        /// </summary>
        [Field("AcceptDate")]
        public DateTime? AcceptDate
        {
            get { return _AcceptDate; }
            set
            {
                this.OnPropertyValueChange("AcceptDate");
                this._AcceptDate = value;
            }
        }
        /// <summary>
        /// 验退数量
        /// </summary>
        [Field("BackNum")]
        public int BackNum
        {
            get { return _BackNum; }
            set
            {
                this.OnPropertyValueChange("BackNum");
                this._BackNum = value;
            }
        }
        /// <summary>
        /// _1-未验收;2-已验收
        /// </summary>
        [Field("AcceptStatus")]
        public int AcceptStatus
        {
            get { return _AcceptStatus; }
            set
            {
                this.OnPropertyValueChange("AcceptStatus");
                this._AcceptStatus = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("InStockID")]
        public int? InStockID
        {
            get { return _InStockID; }
            set
            {
                this.OnPropertyValueChange("InStockID");
                this._InStockID = value;
            }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        [Field("StockID")]
        public int? StockID
        {
            get { return _StockID; }
            set
            {
                this.OnPropertyValueChange("StockID");
                this._StockID = value;
            }
        }
        /// <summary>
        /// 库位ID
        /// </summary>
        [Field("AlibraryID")]
        public int? AlibraryID
        {
            get { return _AlibraryID; }
            set
            {
                this.OnPropertyValueChange("AlibraryID");
                this._AlibraryID = value;
            }
        }
        /// <summary>
        /// 检验码(1-未检验;2-已检验)
        /// </summary>
        [Field("CheckItemStatus")]
        public int? CheckItemStatus
        {
            get { return _CheckItemStatus; }
            set
            {
                this.OnPropertyValueChange("CheckItemStatus");
                this._CheckItemStatus = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Version")]
        public string Version
        {
            get { return _Version; }
            set
            {
                this.OnPropertyValueChange("Version");
                this._Version = value;
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
				_.ResNum,
				_.MaterialProNo,
				_.MaterialCode,
				_.MaterialSize,
				_.InStockNum,
				_.Unit,
				_.BatchNo,
				_.AcceptNum,
				_.AcceptDate,
				_.BackNum,
				_.AcceptStatus,
				_.InStockID,
				_.StockID,
				_.AlibraryID,
				_.CheckItemStatus,
				_.Version,
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
				this._ResNum,
				this._MaterialProNo,
				this._MaterialCode,
				this._MaterialSize,
				this._InStockNum,
				this._Unit,
				this._BatchNo,
				this._AcceptNum,
				this._AcceptDate,
				this._BackNum,
				this._AcceptStatus,
				this._InStockID,
				this._StockID,
				this._AlibraryID,
				this._CheckItemStatus,
				this._Version,
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
            public readonly static Field All = new Field("*", "Mes_Stock_InStockItem");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Stock_InStockItem", "");
            /// <summary>
            /// XK01-进货单;WX01-委外进货单
            /// </summary>
            public readonly static Field BillType = new Field("BillType", "Mes_Stock_InStockItem", "XK01-进货单;WX01-委外进货单");
            /// <summary>
            /// 进货单编号
            /// </summary>
            public readonly static Field BillNo = new Field("BillNo", "Mes_Stock_InStockItem", "进货单编号");
            /// <summary>
            /// 单据行号
            /// </summary>
            public readonly static Field ResNum = new Field("ResNum", "Mes_Stock_InStockItem", "单据行号");
            /// <summary>
            /// 产品编码
            /// </summary>
            public readonly static Field MaterialProNo = new Field("MaterialProNo", "Mes_Stock_InStockItem", "产品编码");
            /// <summary>
            /// 产品简称
            /// </summary>
            public readonly static Field MaterialCode = new Field("MaterialCode", "Mes_Stock_InStockItem", "产品简称");
            /// <summary>
            /// 产品规格
            /// </summary>
            public readonly static Field MaterialSize = new Field("MaterialSize", "Mes_Stock_InStockItem", "产品规格");
            /// <summary>
            /// 进货数量
            /// </summary>
            public readonly static Field InStockNum = new Field("InStockNum", "Mes_Stock_InStockItem", "进货数量");
            /// <summary>
            /// 单位
            /// </summary>
            public readonly static Field Unit = new Field("Unit", "Mes_Stock_InStockItem", "单位");
            /// <summary>
            /// 同一批产品一个批号，同一天多个批号,批号原则:INYYYYMMDDHHMMSSS
            /// </summary>
            public readonly static Field BatchNo = new Field("BatchNo", "Mes_Stock_InStockItem", "同一批产品一个批号，同一天多个批号,批号原则:INYYYYMMDDHHMMSSS");
            /// <summary>
            /// 验收数量
            /// </summary>
            public readonly static Field AcceptNum = new Field("AcceptNum", "Mes_Stock_InStockItem", "验收数量");
            /// <summary>
            /// 验收日期
            /// </summary>
            public readonly static Field AcceptDate = new Field("AcceptDate", "Mes_Stock_InStockItem", "验收日期");
            /// <summary>
            /// 验退数量
            /// </summary>
            public readonly static Field BackNum = new Field("BackNum", "Mes_Stock_InStockItem", "验退数量");
            /// <summary>
            /// _1-未验收;2-已验收
            /// </summary>
            public readonly static Field AcceptStatus = new Field("AcceptStatus", "Mes_Stock_InStockItem", "_1-未验收;2-已验收");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field InStockID = new Field("InStockID", "Mes_Stock_InStockItem", "");
            /// <summary>
            /// 仓库ID
            /// </summary>
            public readonly static Field StockID = new Field("StockID", "Mes_Stock_InStockItem", "仓库ID");
            /// <summary>
            /// 库位ID
            /// </summary>
            public readonly static Field AlibraryID = new Field("AlibraryID", "Mes_Stock_InStockItem", "库位ID");
            /// <summary>
            /// 检验码(1-未检验;2-已检验)
            /// </summary>
            public readonly static Field CheckItemStatus = new Field("CheckItemStatus", "Mes_Stock_InStockItem", "检验码(1-未检验;2-已检验)");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Version = new Field("Version", "Mes_Stock_InStockItem", "");
        }
        #endregion
    }
}