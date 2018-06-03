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
    /// 实体类Mes_Tec_ProcessBomChangeLog。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Tec_ProcessBomChangeLog")]
    [Serializable]
    public partial class Mes_Tec_ProcessBomChangeLog : Entity
    {
        #region Model
        private int _ID;
        private string _MaterialProNo;
        private string _MaterialCode;
        private string _Version;
        private int? _UpType;
        private string _ChangeContent;
        private string _Creater;
        private DateTime _CreatedTime;
        private int? _ProcessBomID;

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
        /// 初始版本V1.0,每次更改加0.1
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
        /// <summary>
        /// _1-普通修改;2-版本升级变更
        /// </summary>
        [Field("UpType")]
        public int? UpType
        {
            get { return _UpType; }
            set
            {
                this.OnPropertyValueChange("UpType");
                this._UpType = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("ChangeContent")]
        public string ChangeContent
        {
            get { return _ChangeContent; }
            set
            {
                this.OnPropertyValueChange("ChangeContent");
                this._ChangeContent = value;
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
        /// 配套明细表主表ID
        /// </summary>
        [Field("ProcessBomID")]
        public int? ProcessBomID
        {
            get { return _ProcessBomID; }
            set
            {
                this.OnPropertyValueChange("ProcessBomID");
                this._ProcessBomID = value;
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
				_.MaterialProNo,
				_.MaterialCode,
				_.Version,
				_.UpType,
				_.ChangeContent,
				_.Creater,
				_.CreatedTime,
				_.ProcessBomID,
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
				this._Version,
				this._UpType,
				this._ChangeContent,
				this._Creater,
				this._CreatedTime,
				this._ProcessBomID,
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
            public readonly static Field All = new Field("*", "Mes_Tec_ProcessBomChangeLog");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialProNo = new Field("MaterialProNo", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MaterialCode = new Field("MaterialCode", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 初始版本V1.0,每次更改加0.1
            /// </summary>
            public readonly static Field Version = new Field("Version", "Mes_Tec_ProcessBomChangeLog", "初始版本V1.0,每次更改加0.1");
            /// <summary>
            /// _1-普通修改;2-版本升级变更
            /// </summary>
            public readonly static Field UpType = new Field("UpType", "Mes_Tec_ProcessBomChangeLog", "_1-普通修改;2-版本升级变更");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ChangeContent = new Field("ChangeContent", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Tec_ProcessBomChangeLog", "");
            /// <summary>
            /// 配套明细表主表ID
            /// </summary>
            public readonly static Field ProcessBomID = new Field("ProcessBomID", "Mes_Tec_ProcessBomChangeLog", "配套明细表主表ID");
        }
        #endregion
    }
}