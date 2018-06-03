/*------1.页面功能方法--------------------------------*/
/*
    功能：加载部门
    param: 参数JSON串，必须包含{TreeId:"treeOrg",URL:"/"}

*/
function loadOrgTree(param) {
    var url = param.url;
    var setting = {
        callback: {
            //onDblClick: zTreeOnDblClick,
            onClick: function (event, treeId, treeNode) {
                if (param.onClick) {
                    param.onClick(event, treeId, treeNode);
                }
            },
            onRightClick: function (event, treeId, treeNode) {
                if (param.onRightClick) {
                    param.onRightClick(event, treeId, treeNode);
                }
            }
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var zNodes = [];
    var url = "/SysManagement/OrganizationMgt_FindTree";
    doPostAjaxRequestBySync(url, param, function (msg) {
        if (!msg.IsSuccess) {
            $.messager.alert('提示', msg.Message, "info");
            return;
        }

        zNodes = $.parseJSON(msg.Message);
    });

    //function zTreeOnDblClick(event, treeId, treeNode) {
    //    if (treeNode.stationtype == "S") return;

    //    var param = {
    //        StationId: treeNode.id,
    //        StationName: treeNode.name,
    //        Longitude: treeNode.longitude,
    //        Latitude: treeNode.latitude
    //    };

    //    if (_businessType == "Home") { //首页才处理双击事件
    //        showStationLocation(param);
    //    }
    //}

    $.fn.zTree.init($("#" + param.TreeId), setting, zNodes);
}



