/*
    文件功能：定义通用处理信息    
*/

/************ 1. Begin 通用判断及转换*****************************/
var CommonUtilObj = {
    IsNullOrEmpty: function (sObj) {
        return sObj == undefined || sObj == null || sObj.length == 0;
    },
    IsEmpty: function (sObj) {
        return sObj.length == 0;
    },
    IsUndefinedOrNull: function (obj) {
        return obj == undefined || obj == null;
    },
    IsExistFunction: function (functionName) {
        if (functionName != null && functionName != undefined) return true;

        return false
    },
    //对象数组转JSON字符串(对象为Key,Value的Json对象)
    toJsonStrByList: function (arrList) {
        return CommonUtilObj.toJsonStrByListMain(arrList, false);
    },
    toJsonStrByListMain: function (arrList, isClearEmpty) { //是否清理空值
        if (!arrList || arrList.length == 0) return "";

        var result = "{";
        var value = "";
        for (var i = 0; i < arrList.length; i++) {
            value = arrList[i].Value;
            if (isClearEmpty && (!value || value.length == 0)) continue;

            result += '"' + arrList[i].Key + '":"' + arrList[i].Value + '",';
        }

        return result.trimEnd() + "}";
    },
    toJsonByList: function (arrList) {
        if (!arrList || arrList.length == 0) {
            return { "paramList": "" };
        }

        var sItemList = JSON.stringify(arrList);
        return { "paramList": sItemList };
    },
    getJsonValue: function (name, jsonObj) { //从Json中返回指定名称的值
        if (!jsonObj || !jsonObj[name]) return "";

        return jsonObj[name];
    },
    doSpecialCharacter: function (strJson) { //处理Json字符串的特殊字符
        if (!strJson || strJson.length == 0) return "";

        return strJson.replace(/\n/g, "\\n").replace(/\r/g, "\\r");

        //return strJson.replace(/>/g, "&gt;")
        //        .replace(/>/g, "&lt;")
        //        .replace(/ /g, "&nbsp;")
        //        .replace(/\"/g, "&quot;")
        //        .replace(/\'/g, "&#39;")
        //        .replace(/\\/g, "\\\\")   //对斜线的转义
        //        .replace(/\n"/g, "\\n")
        //        .replace(/\r"/g, "\\r");
    },
    IsEmptyByInput: function (txtId, msg) {
        var sValue = $("#" + txtId).val().trim();
        if (sValue.length == 0) {
            $.messager.alert('提示', msg);
            return false;
        }

        return true;
    },
    IsGreaterDate: function (date1, date2) { //日期比较，判断前面的日期是否大于后面的日期
        var time1 = new Date(date1.replace("-", "/").replace("-", "/"));
        var time2 = new Date(date2.replace("-", "/").replace("-", "/"));

        if (time1 > time2) return true;

        return false;
    },
    addDate: function (date, days) { //日期增加天数
        if (!days || days.length == 0) {
            days = 1;
        }
        var date = new Date(date);
        date.setDate(date.getDate() + days);
        var month = date.getMonth() + 1;
        var day = date.getDate();
        return date.getFullYear() + '-' + CommonUtilObj.getFormatDate(month) + '-' + CommonUtilObj.getFormatDate(day);
    },
    //日期月份/天的显示，如果是1位数，则在前面加上'0'
    getFormatDate: function (arg) {
        if (!arg || arg.length == 0) {
            return '';
        }
        var re = arg + '';
        if (re.length < 2) {
            re = '0' + re;
        }
        return re;
    }
};

/************ 2. Begin 字符串、日期、数字及其他处理处理****************/
 //将字符串日期转化为日期格式: 字符串格式为: 20150801 转化为 2015-08-01
 String.prototype.formatDate = function () {
     return this.substr(0, 4) + "-" + this.substr(4, 2) + "-" + this.substr(6, 2);
 }

 //字符串位数不足补0的方法
 String.prototype.padLeft = function (n) {
     var str = this;
     var len = this.length;
     while (len < n) {
         str = "0" + str;
         len++;
     }
     return str;
 }

 //去掉最后一位字符
 String.prototype.trimEnd = function () {
     var str = this;
     return str.substr(0, str.length - 1);
 }

 //去掉前后的空白符
 String.prototype.trim = function () {
     return this.replace(/(^\s*)|(\s*$)/g, "");
 }
 String.prototype.lTrim = function () {
     return this.replace(/(^\s*)/g, "");
 }
 String.prototype.rTrim = function () {
     return this.replace(/(\s*$)/g, "");
 }

 String.prototype.replaceAll = function (reg,replacement) {
     return this.replace(reg, replacement);
     //return this.split(s1).join(s2);
 }

 //计算字符串的实际长度(包含中文)
 String.prototype.factLength = function () {
     var MatchArray = this.match(/[^\x00-\xff]/ig);
     return this.length + (MatchArray == null ? 0 : MatchArray.length);
 };

 //去掉字符串中包含的特殊字符
 String.prototype.clearSpecialStr = function(srcStr) {
    return this.replace(/[^a-zA-Z0-9\u4E00-\u9FA5 \-\_，：']/g, "");
 }



 // 对Date的扩展，将 Date 转化为指定格式的String   
 // 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
 // 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
 // 例子：   
 // (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
 // (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18
 Date.prototype.formatStr = function (fmt) { //author: meizz   
     var o = {
         "M+": this.getMonth() + 1,                 //月份   
         "d+": this.getDate(),                    //日   
         "h+": this.getHours(),                   //小时   
         "m+": this.getMinutes(),                 //分   
         "s+": this.getSeconds(),                 //秒   
         "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
         "S": this.getMilliseconds()             //毫秒   
     };
     if (/(y+)/.test(fmt))
         fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
     for (var k in o)
         if (new RegExp("(" + k + ")").test(fmt))
             fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
     return fmt;
 }

 /************ 3. Begin 数据类型校验****************/
 var RegObj = {
     //是否为名称格式：支持数字、字母、下划线
     //data格式为： {Name:"用户名",Value: "123SA",Require: true, MaxLen: 100, MinLen: 10 }
     isEngName: function (data) {
         if (!data.Value || data.Value.length == 0) {
             if (!data.Require || data.Require == false) return "";
             
             return "【" + data.Name + "】不允许为空！";
         }

         var len = data.MaxLen;
         if (!len || len == 0) { //默认长度不超过60个字符
             len = 60;
         }
         if (data.Value.length > len) {
             return ("【[0]】长度不能超过[1]个字符！").replace("[0]", data.Name).replace("[1]", len);
         }
            
         if (!/^[A-Za-z0-9\_]+$/.test(data.Value)) {
             return "【" + data.Name + "】只能包含数字、字母或下划线等！"
         }

         return "";
     },
     isCNName: function (data) {
         if (!data.Value || data.Value.length == 0) {
             if (!data.Require || data.Require == false) return "";

             return "【" + data.Name + "】不允许为空！";
         }

         var len = data.MaxLen;
         if (!len || len == 0) { //默认长度不超过60个字符
             len = 60;
         }
         if (data.Value.length > len) {
             return ("【[0]】长度不能超过[1]个字符！").replace("[0]", data.Name).replace("[1]", len);
         }

         if (!/^[A-Za-z0-9\_\u4E00-\u9FA5]+$/.test(data.Value)) {
             return "【" + data.Name + "】只能包含中文、数字、字母或下划线等！"
         }

         return "";
     },
     isSpecialChar: function (sData) { //判断是否包含特殊字符
         if (!sData || sData.length == 0) return false;

         var len = sData.length;
         //剔除非正常字符(数字、字母，下划线、中文、-、空白符、:、逗号,单引号)
         sData = sData.replace(/[^a-zA-Z0-9\u4E00-\u9FA5 \-\_，：'.。]/g, "");
         return sData.length < len;
     },
     isSpecialCharNew: function (sData) { //判断是否包含特殊字符
         if (!sData || sData.length == 0) return false;

         var len = sData.length;
         //剔除非正常字符(数字、字母，下划线、中文、-、空白符、:、逗号,单引号)
         sData = sData.replace(/>/g, "")
                .replace(/</g, "")
                .replace(/\~/g, "");
                
         return sData.length < len;
     },
    //密码格式校验
    isPassword : function(sData){
         return /^[A-Za-z0-9]{6,20}$/.test(sData);
    },
    //是否为整数(大于0的数)
    isInt : function(sData){
        return /^[1-9]+\d*$/.test(sData);
    },
    //手机号校验
    isMobile : function(sData){
        return /^1[3|4|5|8][0-9]\d{8}$/.test(sData);
    },
     //日期类型校验
    isDate : function(sData){
        return /^([1|2]\d{3})-((0\d)|(1[0-2]))-(([0-2]\d)|(3[0|1]))$/.test(sData);
    }

 }


 //全局的ajax访问，处理ajax清求时sesion超时
 $.ajaxSetup({
     contentType: "application/x-www-form-urlencoded;charset=utf-8",
     complete: function (XMLHttpRequest, textStatus) {
         //通过XMLHttpRequest取得响应头，sessionstatus，
         if (!XMLHttpRequest.getResponseHeader) return;

         var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
         if (sessionstatus == "timeout") {
             //如果超时就处理 ，指定要跳转的页面
             window.location = "/Login/Index";
         }
     }
 });