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
    /// 实体类Mes_Tec_ProductInfo。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Tec_ProductInfo")]
    [Serializable]
    public partial class Mes_Tec_ProductInfo : Entity
    {
        #region Model
        private int _ID;
        private string _MaterialProNo;
        private string _MaterialCode;
        private string _MaterialSize;
        private int? _TraceProperty;
        private string _MaterialClass;
        private string _StockCode;
        private string _AlibrayCode;
        private int? _SafeNum;
        private string _Unit;
        private string _ReplaceMaterialProNo;
        private int _MaterialStatus;
        private int? _PackNumber;
        private string _Memo;
        private string _Creater;
        private DateTime _CreatedTime;
        private string _Modifier;
        private DateTime? _ModifiedTime;

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
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        [Field("TraceProperty")]
        public int? TraceProperty
        {
            get { return _TraceProperty; }
            set
            {
                this.OnPropertyValueChange("TraceProperty");
                this._TraceProperty = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("MaterialClass")]
        public string MaterialClass
        {
            get { return _MaterialClass; }
            set
            {
                this.OnPropertyValueChange("MaterialClass");
                this._MaterialClass = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("StockCode")]
        public string StockCode
        {
            get { return _StockCode; }
            set
            {
                this.OnPropertyValueChange("StockCode");
                this._StockCode = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("AlibrayCode")]
        public string AlibrayCode
        {
            get { return _AlibrayCode; }
            set
            {
                this.OnPropertyValueChange("AlibrayCode");
                this._AlibrayCode = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("SafeNum")]
        public int? SafeNum
        {
            get { return _SafeNum; }
            set
            {
                this.OnPropertyValueChange("SafeNum");
                this._SafeNum = value;
            }
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [Field("ReplaceMaterialProNo")]
        public string ReplaceMaterialProNo
        {
            get { return _ReplaceMaterialProNo; }
            set
            {
                this.OnPropertyValueChange("ReplaceMaterialProNo");
                this._ReplaceMaterialProNo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("MaterialStatus")]
        public int MaterialStatus
        {
            get { return _MaterialStatus; }
            set
            {
                this.OnPropertyValueChange("MaterialStatus");
                this._MaterialStatus = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("PackNumber")]
        public int? PackNumber
        {
            get { return _PackNumber; }
            set
            {
                this.OnPropertyValueChange("PackNumber");
                this._PackNumber = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Memo")]
        public string Memo
        {
            get { return _Memo; }
            set
            {
                this.OnPropertyValueChange("Memo");
                this._Memo = value;
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
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.ID,
				_.MaterialProNo,
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
				_.MaterialProNo,
				_.MaterialCode,
				_.MaterialSize,
				_.TraceProperty,
				_.MaterialClass,
				_.StockCode,
				_.AlibrayCode,
				_.SafeNum,
				_.Unit,
				_.ReplaceMaterialProNo,
				_.MaterialStatus,
				_.PackNumber,
				_.Memo,
				_.Creater,
				_.CreatedTime,
				_.Modifier,
				_.ModifiedTime,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._MaterialProNo,
				this._MaterialCode,
				this._MaterialSize,
				this._TraceProperty,
				this._MaterialClass,
				this._StockCode,
				this._AlibrayCode,
				this._SafeNum,
				this._Unit,
				this._ReplaceMaterialProNo,
				this._MaterialStatus,
				this._PackNumber,
				this._Memo,
				this._Creater,
				this._CreatedTime,
				this._Modifier,
				this._ModifiedTime,
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
            public readonly static Field All = new Field("*", "Mes_Tec_ProductInfo");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialProNo = new Field("MaterialProNo", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialCode = new Field("MaterialCode", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialSize = new Field("MaterialSize", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field TraceProperty = new Field("TraceProperty", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialClass = new Field("MaterialClass", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field StockCode = new Field("StockCode", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field AlibrayCode = new Field("AlibrayCode", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SafeNum = new Field("SafeNum", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Unit = new Field("Unit", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ReplaceMaterialProNo = new Field("ReplaceMaterialProNo", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialStatus = new Field("MaterialStatus", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field PackNumber = new Field("PackNumber", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Memo = new Field("Memo", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Tec_ProductInfo", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Tec_ProductInfo", "");
        }
        #endregion
    }
}