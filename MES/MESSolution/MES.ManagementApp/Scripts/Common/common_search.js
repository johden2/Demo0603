/*
    文件功能：通用的查询列表、刷新功能
    文件历史:        
*/


//刷新生成条码Grid
function reloadGenGrid(queryParams, gridName) {
    $("#" + gridName).datagrid('options').queryParams = queryParams;
    $("#" + gridName).datagrid('reload');
}

//清空Grid数据
function clearGenGrid(gridName) {
    $("#" + gridName).datagrid('loadData', { total: 0, rows: [] });
}

//打开或关闭弹出层
function _doProgress(isClose, divId) {
    if (isClose) {
        $('#' + divId).dialog('close');
    } else {
        $('#' + divId).dialog('open');
    }
}

function showMessage(divId,message ) {
    $("#" + divId).html(message);
}


//列表信息对象
var PageListObj = {
    GridName: "dgList",
    QueryDiv: "divQuery",
    QueryPrex: "Q_",
    GridProperties: null,
    onLoad: null,
    onLastLoad: null,
    onDblClickRow: null,
    onLastGetQueryParms: null,
    onReInitGrid: null, //重新初始化Grid
    init: function () {
        //设置查询条件部分功能
        if ($('#btnQuery').length > 0) {
            $('#btnQuery').bind("click", function () {PageListObj.queryList(); });
            $('#tbQuery').on("keyup", function (event) {
                if (event.keyCode == '13' && this.value != '') {
                    PageListObj.queryList();
                }
            });
        }
        //清理
        if ($('#btnClearQuery').length > 0) {
            $('#btnClearQuery').bind("click", function () { PageListObj.queryList(true); });
        }

        //加载指定事件
        if (this.onLoad != null) {
            this.onLoad();
        }

        //初始化Grid数据
        this.initGrid(this.getQueryParms());

        //加载指定事件
        if (this.onLastLoad != null) {
            this.onLastLoad();
        }

    },
    //初始化Grid数据
    initGrid: function (parms) {
        this.ShowGrid(this.GridName, this.GridProperties, parms);
        //bindMainGrid(this.GridName, this.GridProperties, parms, { onDblClickRow: this.onDblClickRow }, "加载列表数据出错");
    },
    loadGrid: function(){ //直接加载Grid
        this.initGrid(this.getQueryParms());
    },
    getQueryParms: function () {
        var arrList = new Array();
        var parmName = "";
        var parmValue = "";

        if (PageListObj.getQueryParms != null) {
            arrList = PageListObj.getQueryParms(arrList);
        } else {
            $("input[id^='" + this.QueryPrex + "']").each(function (i) {
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

                parmName = parmName.replace(PageListObj.QueryPrex, "");
                arrList.push({ Key: parmName, Value: parmValue.trim() });
            });
        }

        //自定义的参数
        if (PageListObj.onLastGetQueryParms != null) {
            arrList = PageListObj.onLastGetQueryParms(arrList);
        }

        //将数组列表对象转换为Json串
        var sResult = CommonUtilObj.toJsonStrByList(arrList);
        return $.parseJSON(sResult);
    },
    //查询记录记录
    queryList: function (isClear) {
        if (isClear) {
            $("input[id^='" + this.QueryPrex + "']").each(function (i) {
                var className = $(this).attr("class");
                if (className && className.indexOf("easyui-datebox") >= 0) {
                    $(this).datebox('setValue', '');
                } else if (className && className.indexOf("easyui-combobox") >= 0) {
                    $(this).combobox('setValue', '');
                } else {
                    $(this).val("");
                }
            });
        }

        var parms = PageListObj.getQueryParms();
        //如果要重新初始化Grid的列
        var isLoad = false;
        if (PageListObj.onReInitGrid != null) {
            isLoad = PageListObj.onReInitGrid();
        }
        if (isLoad) {
            PageListObj.ShowGrid(PageListObj.GridName, PageListObj.GridProperties, parms);
        }
        else {
            $('#' + this.GridName).datagrid('load', parms);
        }
    },
    ShowGrid: function (gridName, properties, qParms) {
        var cols = properties.columns;
        var fcols = properties.fcolumns;
        if (!fcols)
        {
            fcols = [[]];
        }
        var singleSelect = false;
        if (properties.singleSelect && properties.singleSelect == true) {
            singleSelect = true;
        }
        var pageList = [10, 20, 30, 40, 50];
        if (properties.pageList) {
            pageList = properties.pageList;
        }

        $('#' + gridName).datagrid({
            singleSelect: singleSelect,
            fit: true,
            //fitColumns: true,
            cache: false,
            height: $(window).height() - 10,
            striped: true,
            loadMsg: '数据加载中，请稍后……',
            collapsible: true,
            remoteSort: false,
            showFooter: true,
            pagination: true,
            pageList: pageList,
            rownumbers: true,
            frozenColumns: fcols,
            columns: cols,
            queryParams: qParms, //搜索条件的json参数
            url: properties.url,
            toolbar: '#' + PageListObj.QueryDiv,
            onClickRow: function (rowindex, rowData) {
                if (properties.onClickRow) {
                    properties.onClickRow(rowindex, rowData);
                }
            },
            onDblClickRow: function (rowindex, rowData) {
                if (properties.onDblClickRow) {
                    properties.onDblClickRow(rowindex, rowData);
                }
            },
            onBeforeLoad: function (param) {
                //如果返回 false 加载动作就会取消
                if (param.IsLoad != undefined && !param.IsLoad) {
                    return false;
                }
            },
            onLoadSuccess: function (data) {
                if (data.Message) {
                    $.messager.alert('提示', data.Message, "info");
                    return;
                }
                if (properties.onLoadSuccess) {
                    properties.onLoadSuccess(data);
                }
            },
            onLoadError: function (e) {
                //$.messager.alert('错误', e.responseText, "error");
                if (properties.onLoadError) {
                    properties.onLoadError(data);
                }
            }
        });
    }
}

