/*
    文件功能：通用的控件处理
    文件历史: Created By Sunlz on 9/8/2015       
*/

/************Begin 下拉框绑定********************************************/
/*
    功能：通用的加载combox数据
    参数: sComboxId - combox控件的ID
          sUrl - ajax请求的地址
          parms - 传递的参数，json数据格式，没有时为null
          async - 是否异步，为true为异步，false 为同步
    历史:     
*/
function common_bindCombox(sComboxId, sUrl, parms) {
    common_bindComboxMain(sComboxId, sUrl, parms, false);
}

function common_bindCommonCombox(sComboxId, parms) {
    var sUrl = "/Common/Status_GetList?rnd=" + Math.random();
    common_bindComboxMain(sComboxId, sUrl, parms, false);
}


function common_bindComboxMain(sComboxId, sUrl, parms, isasync) {
    sUrl = sUrl + "?rnd=" + Math.random();
    $.ajax({
        type: "POST",
        url: sUrl,
        data: parms,
        dataType: "json",
        async: isasync,
        success: function (msg) {
            if (msg.IsSuccess) {
                if (msg.Message != undefined && msg.Message.length > 0) {
                    var jsonData = $.parseJSON(msg.Message);
                    $("#" + sComboxId).combobox("loadData", jsonData);
                    //设置默认值
                    if (parms.defaultValue) {
                        $("#" + sComboxId).combobox('select', parms.defaultValue);
                    } else {
                        $("#" + sComboxId).combobox('select', jsonData[0].Value);
                    }
                }
                else {
                    print_clearCombox(sComboxId);
                }
            } else {
                $.messager.alert('提示', msg.Message, "info");
            }
        },
        error: function () {
            $.messager.alert('错误', '加载数据失败！', "error");
        }
    });
}


function print_bindComboxMainAll(comboxIdList, sUrl, parms, isasync) {
    sUrl = sUrl + "?" + Math.random();

    if (typeof (isasync) == "undefined") {
        isasync = false;
    }

    $.ajax({
        type: "GET",
        url: sUrl,
        data: parms,
        dataType: "json",
        async: isasync,
        success: function (msg) {
            if (!msg.IsSuccess) {
                $.messager.alert('提示', msg.Message, "info");
                return;
            }

            var comboxItem = null;
            if (msg.Message != undefined && msg.Message.length > 0) {
                var jsonData = $.parseJSON(msg.Message);
                for (var i = 0; i < comboxIdList.length; i++) {
                    comboxItem = comboxIdList[i];
                    $("#" + comboxItem.ComboxId).combobox("loadData", jsonData);
                    //设置默认值
                    $("#" + comboxItem.ComboxId).combobox('select', jsonData[0].Value);
                }
            }
            else { //没有数据时，需要先清理下拉框
                for (var i = 0; i < comboxIdList.length; i++) {
                    comboxItem = comboxIdList[i];
                    print_clearCombox(comboxItem.ComboxId);
                }
            }

            comboxItem = null; //清理对象
        },
        error: function () {
            $.messager.alert('错误', '加载数据失败！', "error");
        }
    });
}

//清理指定下拉框控件内容
function print_clearCombox(sComboxId) {
    $("#" + sComboxId).combobox("clear");
    $("#" + sComboxId).combobox("loadData", []);
}
/************End 下拉框绑定********************************************/

//DataGrid通用方法
var DataGridUitl = {
    width: '98%',
    pagination: true,
    pageList: [20, 30],
    rownumbers: true,
    singleSelect: true,
    selectOnCheck: false,
    checkOnSelect: false,
    //获取选中的单行记录
    getSelectedRow: function (gridId) {
        if ($("#" + gridId).length > 0) {
            return $("#" + gridId).datagrid('getSelected');
        }

        return null;
    },
    //获取选中的多行记录
    getSelectedRows: function (gridId) {
        if ($("#" + gridId).length > 0) {
            return $("#" + gridId).datagrid('getSelections');
        }

        return null;
    }
};

//Form通用方法
var FormUitl = {
    EditPrefix: 'E_',
    KeyName: "E_KeyValue",
    onGetFormDataAfter: null,//获取状态数据后扩展的方法
    setCtrlValue: function (ctrlId, ctrlValue,ctrlType) { //设置控件的值
        if (!ctrlValue) return;

        if (!ctrlType) {
            ctrlType = "text";
        }

        if (ctrlType == "text" || ctrlType == "textbox") {
            $("#" + ctrlId).val(ctrlValue);
            return;
        }
    },
    setValueByKey: function (key, value, formId) { //设置控件的值
        if (!value) return;

        var id = FormUitl.EditPrefix + key;
        var ctrlObj = $("#" + formId).find("input[id='" + id + "']").first();
        if (ctrlObj.length == 0) {
            ctrlObj = $("#" + formId).find("textarea[id='" + id + "']").first();
            if (ctrlObj.length > 0) {
                $(ctrlObj).val(value);
            }
            return;
        }

        var className = $(ctrlObj).attr("class");
        var typeName = $(ctrlObj).attr("type");
        if (!className) className = "";
        if (!typeName) typeName = "";

        if (className.indexOf("easyui-datebox") >= 0) {
            $(ctrlObj).datebox('setValue', value);
        } else if (className.indexOf("easyui-combobox") >= 0) {
            $(ctrlObj).combobox('setValue', value);
        } else if (className.indexOf("easyui-textbox") >= 0 || typeName == "text" || typeName == "hidden") {
            $(ctrlObj).val(value);
        } else if (typeName == "checkbox" && value == "1" ){
            $(ctrlObj).attr("checked", true);
        }else {
            return;
        }
    },
    getFormData: function (formId) {
        var dataList = FormUitl.getFormParms(formId);
        //将数组列表对象转换为Json串
        var sResult = CommonUtilObj.toJsonStrByList(dataList);
        return $.parseJSON(sResult);
    },
    getFormItem: function (formId) {
        var dataList = FormUitl.getFormParms(formId);
        //直接转换为Json参数
        return CommonUtilObj.toJsonByList(dataList);
    },
    getFormParms: function (formId) {
        var dataList = new Array();
        var parmName = "";
        var parmValue = "";

        $("#" + formId).find("input[id^='" + FormUitl.EditPrefix + "']").each(function (i) {
            parmName = $(this)[0].id;
            var className = $(this).attr("class");
            if (!className) className = "";
            var typeName = $(this).attr("type");
            if (!typeName) typeName = "";

            if (className.indexOf("easyui-datebox") >= 0) {
                parmValue = $(this).datebox('getValue');
            } else if (className.indexOf("easyui-combobox") >= 0) {
                parmValue = $(this).combobox('getValue');
            } else if (className.indexOf("easyui-textbox") >= 0 || typeName == "text" || typeName == "hidden") {
                parmValue = $.trim($(this).val());
            }
            else if (typeName == "checkbox" && $(this).attr("checked") == "checked") {
                parmValue = $.trim($(this).val());
            }
            else
            {
                return; //处理下一个控件
            }

            parmName = parmName.replace(FormUitl.EditPrefix, "");
            dataList.push({ Key: parmName, Value: parmValue.trim() });
        });

        $("#" + formId).find("textarea[id^='" + FormUitl.EditPrefix + "']").each(function (i) {
            parmName = $(this)[0].id;
            parmValue = $.trim($(this).val());
            parmName = parmName.replace(FormUitl.EditPrefix, "");
            dataList.push({ Key: parmName, Value: parmValue.trim() });
        });

        if (this.onGetFormDataAfter != null) {
            dataList = this.onGetFormDataAfter(dataList);
        }

        //增加唯一标识项，保存时需要
        var keyCtrlObj = $("#" + FormUitl.KeyName);
        if (keyCtrlObj.length > 0)
        {
            parmName = $(keyCtrlObj).attr("name").replace(FormUitl.EditPrefix, "");
            dataList.push({ Key: parmName, Value: keyCtrlObj.val().trim() });
        }

        return dataList;
    },
    reset: function(formId){
        FormUitl.resetMain(formId, FormUitl.EditPrefix);
    },
    resetMain: function (formId, sPrefix) {
        $("#" + formId).find("input[id^='" + sPrefix + "']").each(function (i) {
            var clearflag = $(this).attr("clearflag"); //表示该字段值是否不清空
            if (clearflag && clearflag == "0") return;

            var className = $(this).attr("class");
            if (!className) className = "";
            var typeName = $(this).attr("type");
            if (!typeName) typeName = "";
            if (className.indexOf("easyui-datebox") >= 0) {
                $(this).datebox('setValue', '');
            } else if (className.indexOf("easyui-combobox") >= 0) {
                $(this).combobox('setValue', '');
            } else if (className.indexOf("easyui-textbox") >= 0 || typeName == "text" || typeName == "hidden") {
                $(this).val("");
            }
            else if (typeName == "checkbox" && $(this).attr("checked") == "checked") {
                $(this).attr("checked", false);
            }
        });

        $("#" + formId).find("textarea[id^='" + sPrefix + "']").each(function (i) {
            $(this).val("");
        });
    },
    open: function (divName) {
        $('#' + divName).window('open');
    },
    close: function (divName) {
        $('#' + divName).window('close');
    }
};


/************Begin Ajax事件整理********************************************/

//异步请求
function doPostAjaxRequest(sUrl, data, onSuccess, onError) {
    doSyncAjaxRequest(true, sUrl, data, "POST", onSuccess, onError);
}

//同步请求
function doPostAjaxRequestBySync(sUrl, data, onSuccess, onError) {
    doSyncAjaxRequest(false, sUrl, data, "POST", onSuccess, onError);
}

function doAjaxRequest(sUrl, data, type, onSuccess, onError) {
    doSyncAjaxRequest(true, sUrl, data, type, onSuccess, onError);
}

/*
    功能：ajax请求方法
    参数: isasync - 是否异步，为true为异步，false 为同步，默认为true 
    sUrl - ajax请求的地址
    data - 传递的参数，json数据格式，没有时为null
    type - 请求方式:默认为POST
    onSuccess --请求成功后需要执行的方法
    onError --请求失败后需要执行的方法     
*/
function doSyncAjaxRequest(isasync, sUrl, data, type, onSuccess, onError) {
    if (CommonUtilObj.IsUndefinedOrNull(isasync)) {
        isasync = true;
    }
    if (CommonUtilObj.IsUndefinedOrNull(type)) {
        type = "POST";
    }

    //增加随机参数，避免IE缓存问题
    if (sUrl.indexOf("?") > -1) {
        sUrl = sUrl + "&rnd=" + Math.random();
    } else {
        sUrl = sUrl + "?rnd=" + Math.random();
    }

    $.ajax({
        type: type,
        url: sUrl,
        data: data,
        async: isasync,
        dataType: "json",
        success: function (msg) {
            if (!CommonUtilObj.IsUndefinedOrNull(onSuccess)) {
                onSuccess(msg);
            }
        },
        error: function () {
            if (!CommonUtilObj.IsUndefinedOrNull(onError)) {
                onError();
            } else {
                $.messager.alert('错误', '操作失败！', "error");
            }
        }
    });
}

function showAjaxLoading() {
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
    $("<div class=\"datagrid-mask-msg\"></div>").html("数据加载中，请稍后……").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
}
function hideAjaxLoading() {
    $(".datagrid-mask").remove();
    $(".datagrid-mask-msg").remove();
}

//显示消息后渐进消失
function common_fadeOutMessage(divId, message,second) {
    if ($("#" + divId).length == 0) return;

    if (!second) {
        second = 4000; //默认消息显示3秒
    }

    $("#" + divId).html(message);
    $("#" + divId).fadeOut(second, function () { common_fadeOutClear(divId); });
}

function common_fadeOutClear(divId) {
    $("#" + divId).html("");
    $("#" + divId).show();
}



/************End Ajax事件整理********************************************/

/************Begin 项目自定义方法**************/
var CtrlCommonUtil = {
    BindYearList: function (ctrlId) { //绑定年份列表框
        CtrlCommonUtil.BindYearListAll(ctrlId, { headerType: 0, defaultValue: new Date().getFullYear() });
    },
    BindYearListAll: function (ctrlId, param) {
        common_bindCombox(ctrlId, "/Common/BindYearList", param);
    },
    BindRegionList: function (ctrlId) { //绑定地区列表框
        CtrlCommonUtil.BindRegionListAll(ctrlId, { headerType: 0 });
    },
    BindRegionListAll: function (ctrlId, param) {
        common_bindCombox(ctrlId, "/Common/BindRegionList", param);
    },
    BindRoleListLike: function (ctrlId, properties, onClickRow) { //绑定项目列表
        properties.url = '/SysManagement/RoleMgt_Select';
        properties.columns = [[
            //请注释不必要的列,并调整宽度
            { field: 'RoleName', title: '角色名称', width: 150 },
            { field: 'ID', title: '角色ID', width: 100,hidden:true }
            //{ field: 'CreateBy', title: '创建人', width: 150 },
            //{ field: 'Description', title: '描述', width: 300, }
        ]];
        properties.PanelWidth = 300;
        properties.ID = "ID";
        properties.Name = "RoleName";

        bindCombogrid(ctrlId, properties, onClickRow, null);
    },
    BindMaterialLike: function (ctrlId, properties, onClickRow) {
        properties.url = '/BaseManagement/ProductInfo_Select';
        properties.columns = [[
            { field: 'MaterialProNo', title: '产品编码', width: 130, align: 'center' },
            { field: 'MaterialCode', title: '产品简称', width: 130, align: 'center' },
            { field: 'TraceProperty', title: '批次属性', width: 90, align: 'center' },
            { field: 'ID', title: 'ID', width: 1, hidden: true }
        ]];
        properties.PanelWidth = 400;
        properties.ID = "ID";
        properties.Name = "MaterialProNo";

        bindCombogrid(ctrlId, properties, onClickRow, null);
    },
    BindCustomerLike: function (ctrlId, properties, onClickRow) {
        properties.url = '/BaseManagement/Customer_Select';
        properties.columns = [[
            { field: 'CustomerName', title: '客户名称', width: 150, align: 'center' },
            { field: 'CustomerCode', title: '客户编码', width: 130, align: 'center' },
            { field: 'ID', title: 'ID', width: 1, hidden: true }
        ]];
        properties.PanelWidth = 300;
        properties.ID = "ID";
        properties.Name = "CustomerName";

        bindCombogrid(ctrlId, properties, onClickRow, null);
    },
    BindWorkShopLike: function (ctrlId, properties, onClickRow) {
        properties.url = '/BaseManagement/WorkShop_Select';
        properties.columns = [[
              { field: 'ID', title: 'ID', width: 100, align: 'center', hidden: true },
               { field: 'WorkShopCode', title: '车间编码', width: 100, align: 'center' },
               { field: 'WorkShopName', title: '车间名称', width: 160, align: 'center' },
        ]];
        properties.PanelWidth = 600;
        properties.ID = "ID";
        properties.Name = "WorkShopName";

        bindCombogrid(ctrlId, properties, onClickRow, null);
    },
    BindSaleOrderLike: function (ctrlId, properties, onClickRow) {
        properties.url = '/PlanManagement/SaleOrderItemMgt_Select';
        properties.columns = [[
              { field: 'ID', title: 'ID', width: 1, align: 'center', hidden: true },
               { field: 'Show_OrderStatus', title: '订单状态', width: 80, align: 'center' },
               { field: 'Show_OrderType', title: '订单单别', width: 80, align: 'center' },
               { field: 'OrderNo', title: '订单单号', width: 120, align: 'center' },
               { field: 'ResNum', title: '订单行号', width: 70, align: 'center' },
               { field: 'MaterialProNo', title: '产品编码', width: 120, align: 'center' },
               { field: 'MaterialCode', title: '产品简称', width: 120, align: 'center' },
               { field: 'Version', title: '版本', width: 80, align: 'center' },
               { field: 'Num', title: '订单数量', width: 80, align: 'center' },
               { field: 'Show_ShipDate', title: '发货日期', width: 120, align: 'center' }
        ]];
        properties.PanelWidth = 600;
        properties.ID = "ID";
        properties.Name = "MaterialProNo";

        bindCombogrid(ctrlId, properties, onClickRow, null);
    }
}

//绑定下来Grid控件
function bindCombogrid(sComboxId, properties, onClickRow, onLoadSuccess) {
    var width = 150;
    var panelWidth = 350;
    var panelHeight = 320;
    var sID = "ID";
    var sName = "Name";
    var pageList = [10, 20, 30];

    if (properties.Width != undefined) {
        width = properties.Width;
    }
    if (properties.PanelWidth != undefined) {
        panelWidth = properties.PanelWidth;
    }
    if (properties.PanelHeight != undefined) {
        panelHeight = properties.PanelHeight;
    }
    if (properties.ID != undefined) {
        sID = properties.ID;
    }
    if (properties.Name != undefined) {
        sName = properties.Name;
    }
    if (properties.PageList != undefined) {
        pageList = properties.PageList;
    }

    $('#' + sComboxId).combogrid({
        width: width,
        height: 24,
        panelWidth: panelWidth,
        panelHeight: panelHeight,
        idField: sID,
        textField: sName,
        delay: 500,
        mode: 'remote',
        pagination: true, //是否分页  
        rownumbers: true, //序号 
        pageList: pageList,
        columns: properties.columns,
        queryParams: properties.params, //搜索条件的json参数
        url: properties.url,
        onClickRow: function (rowIndex, rowData) {
            if (!CommonUtilObj.IsUndefinedOrNull(onClickRow)) {
                onClickRow(rowIndex, rowData);
            }
        },
        onLoadSuccess: function (data) {
            $.messager.progress('close');
            if (!CommonUtilObj.IsUndefinedOrNull(onLoadSuccess)) {
                onLoadSuccess(data);
            }
        },
        onShowPanel: function () {
            $.messager.progress({ title: '获取数据中...', msg: '请稍候...' });
            setTimeout(function () { $('#' + sComboxId).combogrid({ url: properties.url }); }, 300);
        },
        onLoadError: function (e) {
            $.messager.progress('close');
            $.messager.alert('错误', "加载数据失败", "error");
        }
    });

}

//扩展easyui-tree的功能
//获取节点的层级
$.extend($.fn.tree.methods, {
    getLevel: function (jq, target) {
        var l = $(target).parentsUntil("ul.tree", "ul");
        return l.length + 1;
    }
});

/************End 项目自定义方法****************/

//显示窗口
function showTabIframe(title, url) {
    if (!window.parent || !window.parent.addTabDifferent) {
        $.messager.alert('提示', '页面已超时，请在浏览器中刷新界面或重新登录！', "info");
        return false;
    }
    window.parent.addTabDifferent(title, url);
    return true;
}

//显示产品
function showWinProduct(param) {
    var width = 700;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectProduct";
    var content = '<iframe id="ifmProduct" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmProductDiv" title="产品(物料)选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmProductDiv').show();
    var win = $('#ifmProductDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "产品(物料)选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择客户
function showWinCustomer(param) {
    var width = 650;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectCustomer";
    var content = '<iframe id="ifmCustomer" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmCustomerDiv" title="客户选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmCustomerDiv').show();
    var win = $('#ifmCustomerDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "客户选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择供应商
function showWinSupplier(param) {
    var width = 650;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectSupplier";
    var content = '<iframe id="ifmSupplier" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmSupplierDiv" title="供应商选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmSupplierDiv').show();
    var win = $('#ifmSupplierDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "供应商选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择工作车间
function showWinWorkShop(param) {
    var width = 650;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectWorkShop";
    var content = '<iframe id="ifmWorkShop" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmWorkShopDiv" title="车间选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmWorkShopDiv').show();
    var win = $('#ifmWorkShopDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "车间选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择仓库
function showWinStock(param) {
    var width = 650;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectStock";
    var content = '<iframe id="ifmStock" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmStockDiv" title="仓库选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmStockDiv').show();
    var win = $('#ifmStockDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "仓库选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择销售订单
function showWinSaleOrder(param) {
    var width = 700;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectSaleOrder";
    var content = '<iframe id="ifmSaleOrder" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmSaleOrderDiv" title="订单选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmSaleOrderDiv').show();
    var win = $('#ifmSaleOrderDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "订单选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择销售订单
function showWinSaleOrderItem(param) {
    var width = 700;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectSaleOrderItem";
    var content = '<iframe id="ifmSaleOrderItem" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmSaleOrderItemDiv" title="订单明细选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmSaleOrderItemDiv').show();
    var win = $('#ifmSaleOrderItemDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "订单明细选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}

//选择生成计划
function showWinPlan(param) {
    var width = 700;
    var height = 370;
    if (param && param.Width) {
        width = param.Width;
    }
    if (param && param.Height) {
        height = param.Height;
    }
    var url = "/Common/SelectPlan";
    var content = '<iframe id="ifmPlan" src="' + url + '"width=100%" height="100%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="ifmPlanDiv" title="生产计划选择"></div>'//style="overflow:hidden;"可以去掉滚动条  
    $(document.body).append(boarddiv);
    $('#ifmPlanDiv').show();
    var win = $('#ifmPlanDiv').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        title: "生产计划选择",
        onClose: function () {
            $(this).dialog('destroy');
            $(this).remove();
        }
    });
}



/*
    功能：通用的弹出模式对话框处理
    参数: url - 地址
        vArguments - 传递的参数
        sFeatures - 窗口特性
        width - 弹出的窗口宽度
        height - 弹出的窗口高度
    历史: Created By Sunlz on 3/28/2017     
*/
function showModalURL(url, width, height, vArguments, sFeatures) {
    var iWidth = 750;
    var iHeight = 450;

    if (width != undefined) {
        iWidth = width;
    }
    if (height != undefined) {
        iHeight = height;
    }

    var sUrl = "";
    if (url.indexOf("?") > -1) {
        sUrl = url + "&rnd=" + Math.random();
    } else {
        sUrl = url + "?rnd=" + Math.random();
    }

    if (sFeatures == undefined) {
        sFeatures = "dialogWidth=" + iWidth + "px;dialogHeight=" + iHeight + "px;";
    } else {
        sFeatures = sFeatures + ";dialogWidth=" + iWidth + "px;dialogHeight=" + iHeight + "px;";
    }
    sFeatures = sFeatures + "location=no;";

    vArguments = (vArguments) ? vArguments : null;

    return window.showModalDialog(sUrl, vArguments, sFeatures);
}

