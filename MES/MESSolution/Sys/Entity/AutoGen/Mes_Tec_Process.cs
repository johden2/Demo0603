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
    /// 实体类Mes_Tec_Process。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Tec_Process")]
    [Serializable]
    public partial class Mes_Tec_Process : Entity
    {
        #region Model
        private int _ID;
        private string _ProcessCode;
        private string _Name;
        private string _ParentCode;
        private int? _State;
        private int _IsRepairStatus;
        private int _RecordStatus;
        private string _Creater;
        private DateTime _CreatedTime;
        private string _Modifier;
        private DateTime? _ModifiedTime;

        /// <summary>
        /// ID
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
        /// 工序编码
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
        /// 工序名称
        /// </summary>
        [Field("Name")]
        public string Name
        {
            get { return _Name; }
            set
            {
                this.OnPropertyValueChange("Name");
                this._Name = value;
            }
        }
        /// <summary>
        /// 父级工序
        /// </summary>
        [Field("ParentCode")]
        public string ParentCode
        {
            get { return _ParentCode; }
            set
            {
                this.OnPropertyValueChange("ParentCode");
                this._ParentCode = value;
            }
        }
        /// <summary>
        /// 工序属性
        /// </summary>
        [Field("State")]
        public int? State
        {
            get { return _State; }
            set
            {
                this.OnPropertyValueChange("State");
                this._State = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("IsRepairStatus")]
        public int IsRepairStatus
        {
            get { return _IsRepairStatus; }
            set
            {
                this.OnPropertyValueChange("IsRepairStatus");
                this._IsRepairStatus = value;
            }
        }
        /// <summary>
        /// 记录状态(0-有效;1-无效)
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
        /// 创建人
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
        /// 创建日期
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
        /// 修改人
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
        /// 修改日期
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
				_.ProcessCode,
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
				_.ProcessCode,
				_.Name,
				_.ParentCode,
				_.State,
				_.IsRepairStatus,
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
				this._ProcessCode,
				this._Name,
				this._ParentCode,
				this._State,
				this._IsRepairStatus,
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
            public readonly static Field All = new Field("*", "Mes_Tec_Process");
            /// <summary>
            /// ID
            /// </summary>
            public readonly static Field ID = new Field("ID", "Mes_Tec_Process", "ID");
            /// <summary>
            /// 工序编码
            /// </summary>
            public readonly static Field ProcessCode = new Field("ProcessCode", "Mes_Tec_Process", "工序编码");
            /// <summary>
            /// 工序名称
            /// </summary>
            public readonly static Field Name = new Field("Name", "Mes_Tec_Process", "工序名称");
            /// <summary>
            /// 父级工序
            /// </summary>
            public readonly static Field ParentCode = new Field("ParentCode", "Mes_Tec_Process", "父级工序");
            /// <summary>
            /// 工序属性
            /// </summary>
            public readonly static Field State = new Field("State", "Mes_Tec_Process", "工序属性");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field IsRepairStatus = new Field("IsRepairStatus", "Mes_Tec_Process", "");
            /// <summary>
            /// 记录状态(0-有效;1-无效)
            /// </summary>
            public readonly static Field RecordStatus = new Field("RecordStatus", "Mes_Tec_Process", "记录状态(0-有效;1-无效)");
            /// <summary>
            /// 创建人
            /// </summary>
            public readonly static Field Creater = new Field("Creater", "Mes_Tec_Process", "创建人");
            /// <summary>
            /// 创建日期
            /// </summary>
            public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Tec_Process", "创建日期");
            /// <summary>
            /// 修改人
            /// </summary>
            public readonly static Field Modifier = new Field("Modifier", "Mes_Tec_Process", "修改人");
            /// <summary>
            /// 修改日期
            /// </summary>
            public readonly static Field ModifiedTime = new Field("ModifiedTime", "Mes_Tec_Process", "修改日期");
        }
        #endregion
    }
}