﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
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
    /// 实体类Mes_Plan_TaskPatch。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mes_Plan_TaskPatch")]
    [Serializable]
    public partial class Mes_Plan_TaskPatch : Entity
    {
        #region Model
		private int _ID;
		private string _Date;
		private int _OrgID;
		private string _ProcessCode;
		private string _EmpNo;
		private string _EmpName;
		private string _Creater;
		private DateTime? _CreatedTime;

		/// <summary>
		/// 
		/// </summary>
		[Field("ID")]
		public int ID
		{
			get{ return _ID; }
			set
			{
				this.OnPropertyValueChange("ID");
				this._ID = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Date")]
		public string Date
		{
			get{ return _Date; }
			set
			{
				this.OnPropertyValueChange("Date");
				this._Date = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("OrgID")]
		public int OrgID
		{
			get{ return _OrgID; }
			set
			{
				this.OnPropertyValueChange("OrgID");
				this._OrgID = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("ProcessCode")]
		public string ProcessCode
		{
			get{ return _ProcessCode; }
			set
			{
				this.OnPropertyValueChange("ProcessCode");
				this._ProcessCode = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("EmpNo")]
		public string EmpNo
		{
			get{ return _EmpNo; }
			set
			{
				this.OnPropertyValueChange("EmpNo");
				this._EmpNo = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("EmpName")]
		public string EmpName
		{
			get{ return _EmpName; }
			set
			{
				this.OnPropertyValueChange("EmpName");
				this._EmpName = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Creater")]
		public string Creater
		{
			get{ return _Creater; }
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
		public DateTime? CreatedTime
		{
			get{ return _CreatedTime; }
			set
			{
				this.OnPropertyValueChange("CreatedTime");
				this._CreatedTime = value;
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
				_.Date,
				_.OrgID,
				_.ProcessCode,
				_.EmpNo,
				_.EmpName,
				_.Creater,
				_.CreatedTime,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._Date,
				this._OrgID,
				this._ProcessCode,
				this._EmpNo,
				this._EmpName,
				this._Creater,
				this._CreatedTime,
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
			public readonly static Field All = new Field("*", "Mes_Plan_TaskPatch");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Date = new Field("Date", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field OrgID = new Field("OrgID", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ProcessCode = new Field("ProcessCode", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field EmpNo = new Field("EmpNo", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field EmpName = new Field("EmpName", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Creater = new Field("Creater", "Mes_Plan_TaskPatch", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CreatedTime = new Field("CreatedTime", "Mes_Plan_TaskPatch", "");
        }
        #endregion
	}
}