using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_ProductInfo 
    {

        public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

        public string Show_ModifiedTime
        {
            get
            {               
                return TConvertHelper.FormatShortDateToString(this.ModifiedTime);
            }
        }

        /// <summary>
        /// 批次属性
        /// </summary>
        public string Show_TraceProperty
        {
            get
            {
                return StatusHelper.GetConstStatus<TracePropertyStatus>(this.TraceProperty);
            }
            
        }
        /// <summary>
        /// 物料分类
        /// </summary>
        public string Show_MaterialClass
        {
            get
            {
                return StatusHelper.GetConstStatus<MaterialClassStatus>(this.MaterialClass);
            }
        }
        /// <summary>
        /// 仓库名称 
        /// </summary>
        public string Show_StockCode
        {
            get;
            set;
        }
        /// <summary>
        /// 库位名称
        /// </summary>
        public string Show_AlibrayCode
        {
            get;
            set;
        }
        /// <summary>
        /// 物料状态
        /// </summary>
        public string Show_MaterialStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<RecordStatus>(this.MaterialStatus);
            }
        }

	}
}