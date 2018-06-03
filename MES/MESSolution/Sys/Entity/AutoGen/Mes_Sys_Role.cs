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
    /// 实体类Mes_Sys_Role。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Sys_Role")]
    [Serializable]
    public partial class Mes_Sys_Role : Entity
    {
        #region Model
        private int _ID;
        private string _RoleName;
        private string _RoleDescription;
        private int _RecordStatus;
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
        [Field("RoleName")]
        public string RoleName
        {
            get { return _RoleName; }
            set
            {
                this.OnPropertyValueChange("RoleName");
                this._RoleName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("RoleDescription")]
        public string RoleDescription
        {
            get { return _RoleDescription; }
            set
            {
                this.OnPropertyValueChange("RoleDescription");
                this._RoleDescription = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("RecordStatus")]
        public int RecordStatus
        {
            get { return _RecordStatus; }
            set
            {
                this.OnPropertyValueChange("RecordStatus");
                this._RecordStatus = value;
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
				_.RoleName,
				_.RoleDescription,
				_.RecordStatus,
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
				this._RoleName,
				this._RoleDescription,
				this._RecordStatus,
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
            public readonly static Field All = new Field("*", "Mes_Sys_Role");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RoleName = new Field("RoleName", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RoleDescription = new Field("RoleDescription", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RecordStatus = new Field("RecordStatus", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Sys_Role", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Sys_Role", "");
        }
        #endregion
    }
}