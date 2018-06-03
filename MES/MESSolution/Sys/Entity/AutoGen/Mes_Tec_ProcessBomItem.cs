﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
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
    /// 实体类Mes_Tec_ProcessBomItem。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Tec_ProcessBomItem")]
    [Serializable]
    public partial class Mes_Tec_ProcessBomItem : Entity
    {
        #region Model
        private int _ID;
        private int _ProcessBomID;
        private string _ProcessCode;
        private string _SubMaterialProNo;
        private int _Num;
        private string _Unit;
        private string _Memo;
        private string _Creater;
        private DateTime _CreatedTime;
        private string _Modifier;
        private DateTime? _ModifiedTime;
        private string _SubMaterialCode;

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
        /// 和配套明细表单头关联
        /// </summary>
        [Field("ProcessBomID")]
        public int ProcessBomID
        {
            get { return _ProcessBomID; }
            set
            {
                this.OnPropertyValueChange("ProcessBomID");
                this._ProcessBomID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("ProcessCode")]
        public string ProcessCode
        {
            get { return _ProcessCode; }
            set
            {
                this.OnPropertyValueChange("ProcessCode");
                this._ProcessCode = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("SubMaterialProNo")]
        public string SubMaterialProNo
        {
            get { return _SubMaterialProNo; }
            set
            {
                this.OnPropertyValueChange("SubMaterialProNo");
                this._SubMaterialProNo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Num")]
        public int Num
        {
            get { return _Num; }
            set
            {
                this.OnPropertyValueChange("Num");
                this._Num = value;
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
        /// <summary>
        /// 子物料简称
        /// </summary>
        [Field("SubMaterialCode")]
        public string SubMaterialCode
        {
            get { return _SubMaterialCode; }
            set
            {
                this.OnPropertyValueChange("SubMaterialCode");
                this._SubMaterialCode = value;
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
				_.ProcessBomID,
				_.ProcessCode,
				_.SubMaterialProNo,
				_.Num,
				_.Unit,
				_.Memo,
				_.Creater,
				_.CreatedTime,
				_.Modifier,
				_.ModifiedTime,
				_.SubMaterialCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._ProcessBomID,
				this._ProcessCode,
				this._SubMaterialProNo,
				this._Num,
				this._Unit,
				this._Memo,
				this._Creater,
				this._CreatedTime,
				this._Modifier,
				this._ModifiedTime,
				this._SubMaterialCode,
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
            public readonly static Field All = new Field("*", "Mes_Tec_ProcessBomItem");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 和配套明细表单头关联
            /// </summary>
            public readonly static Field ProcessBomID = new Field("ProcessBomID", "Mes_Tec_ProcessBomItem", "和配套明细表单头关联");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ProcessCode = new Field("ProcessCode", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SubMaterialProNo = new Field("SubMaterialProNo", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Num = new Field("Num", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Unit = new Field("Unit", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Memo = new Field("Memo", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Tec_ProcessBomItem", "");
            /// <summary>
            /// 子物料简称
            /// </summary>
            public readonly static Field SubMaterialCode = new Field("SubMaterialCode", "Mes_Tec_ProcessBomItem", "子物料简称");
        }
        #endregion
    }
}