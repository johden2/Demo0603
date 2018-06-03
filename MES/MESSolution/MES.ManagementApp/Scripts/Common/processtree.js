/*------1.页面功能方法--------------------------------*/
/*
    功能：加载工艺树
    param: 参数JSON串，必须包含{TreeId:"treeOrg",URL:"/"}

*/
function loadProcessTree(param) {
    var url = param.url;
    var setting = {
        callback: {
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
        },
        check: {
            enable: true,
            nocheckInherit: false
        }
    };

    var zNodes = [];
    var url = "/ProcessManagement/ProcessMgt_FindTree";
    doPostAjaxRequestBySync(url, param, function (msg) {
        if (!msg.IsSuccess) {
            $.messager.alert('提示', msg.Message, "info");
            return;
        }

        zNodes = $.parseJSON(msg.Message);
    });

    $.fn.zTree.init($("#" + param.TreeId), setting, zNodes);
}



