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
    /// 实体类Mes_Sys_ModuleItem。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Sys_ModuleItem")]
    [Serializable]
    public partial class Mes_Sys_ModuleItem : Entity
    {
        #region Model
        private int _ID;
        private string _Code;
        private string _MenuName;
        private string _WebRoute;
        private int? _NotShow;
        private int? _NewWindow;
        private string _Creater;
        private DateTime _CreatedTime;
        private string _Modifier;
        private DateTime? _ModifiedTime;
        private int? _Level;
        private string _ModuleCode;
        private int? _SortNo;
        private int? _UseType;

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
        [Field("Code")]
        public string Code
        {
            get { return _Code; }
            set
            {
                this.OnPropertyValueChange("Code");
                this._Code = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("MenuName")]
        public string MenuName
        {
            get { return _MenuName; }
            set
            {
                this.OnPropertyValueChange("MenuName");
                this._MenuName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("WebRoute")]
        public string WebRoute
        {
            get { return _WebRoute; }
            set
            {
                this.OnPropertyValueChange("WebRoute");
                this._WebRoute = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("NotShow")]
        public int? NotShow
        {
            get { return _NotShow; }
            set
            {
                this.OnPropertyValueChange("NotShow");
                this._NotShow = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("NewWindow")]
        public int? NewWindow
        {
            get { return _NewWindow; }
            set
            {
                this.OnPropertyValueChange("NewWindow");
                this._NewWindow = value;
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
        /// 菜单层级
        /// </summary>
        [Field("Level")]
        public int? Level
        {
            get { return _Level; }
            set
            {
                this.OnPropertyValueChange("Level");
                this._Level = value;
            }
        }
        /// <summary>
        /// 菜单编码
        /// </summary>
        [Field("ModuleCode")]
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set
            {
                this.OnPropertyValueChange("ModuleCode");
                this._ModuleCode = value;
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        [Field("SortNo")]
        public int? SortNo
        {
            get { return _SortNo; }
            set
            {
                this.OnPropertyValueChange("SortNo");
                this._SortNo = value;
            }
        }
        /// <summary>
        /// 使用类型
        /// </summary>
        [Field("UseType")]
        public int? UseType
        {
            get { return _UseType; }
            set
            {
                this.OnPropertyValueChange("UseType");
                this._UseType = value;
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
				_.Code,
				_.MenuName,
				_.WebRoute,
				_.NotShow,
				_.NewWindow,
				_.Creater,
				_.CreatedTime,
				_.Modifier,
				_.ModifiedTime,
				_.Level,
				_.ModuleCode,
				_.SortNo,
				_.UseType,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._Code,
				this._MenuName,
				this._WebRoute,
				this._NotShow,
				this._NewWindow,
				this._Creater,
				this._CreatedTime,
				this._Modifier,
				this._ModifiedTime,
				this._Level,
				this._ModuleCode,
				this._SortNo,
				this._UseType,
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
            public readonly static Field All = new Field("*", "Mes_Sys_ModuleItem");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Code = new Field("Code", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MenuName = new Field("MenuName", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field WebRoute = new Field("WebRoute", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field NotShow = new Field("NotShow", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field NewWindow = new Field("NewWindow", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Sys_ModuleItem", "");
            /// <summary>
            /// 菜单层级
            /// </summary>
            public readonly static Field Level = new Field("Level", "Mes_Sys_ModuleItem", "菜单层级");
            /// <summary>
            /// 菜单编码
            /// </summary>
            public readonly static Field ModuleCode = new Field("ModuleCode", "Mes_Sys_ModuleItem", "菜单编码");
            /// <summary>
            /// 排序
            /// </summary>
            public readonly static Field SortNo = new Field("SortNo", "Mes_Sys_ModuleItem", "排序");
            /// <summary>
            /// 使用类型
            /// </summary>
            public readonly static Field UseType = new Field("UseType", "Mes_Sys_ModuleItem", "使用类型");
        }
        #endregion
    }
}