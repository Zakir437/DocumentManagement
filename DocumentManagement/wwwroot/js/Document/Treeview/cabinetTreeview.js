(function () {
    $(document).ready(function () {
        $("#divCabinetTreeView").kendoTreeView({
            loadOnDemand: true,
            dataSource: new kendo.data.HierarchicalDataSource({
                transport: {
                    read: {
                        url: "/Document/Cabinets"
                    }
                },
                schema: {
                    model: {
                        id: "id",
                        children: "items"
                    }
                }
            }),
            dataTextField: "name",
            dataValueField: "id"
        });
    });
    $("#btnTreeRefresh").click(function () {
        $("#divCabinetTreeView").data("kendoTreeView").dataSource.read();
    });
    $("#btnTreeCancel").click(function () {
        $("#divTreeview").data("kendoWindow").close();
    });

    $("#btnTreeOk").click(function () {
        var tv = $("#divCabinetTreeView").data("kendoTreeView");
        var selectedNode = tv.select();
        var item = tv.dataItem(selectedNode);
        if (item) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/FileCopy',
                type: 'Post',
                data: { destFolderId: item.id, fType: item.type, type: $("#Type").val(), encryptedId: $("#EncryptId").val(), isCopy: $("#IsCopy").val(), fileId: $("#FileId").val(), folderId: $("#FolderId").val(), documents: $("#Documents").val() },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "File update was unsuccessful." }, "error");
                    }
                    else if (data === "same")
                    {
                        notification.show({ message: "This file is already exist." }, "error");
                    }
                    else if (data === "NotPossible")
                    {
                        if ($("#IsCopy").val() === "true") {
                            notification.show({ message: "This folder copy to selected folder not possible. Please try another folder." }, "error");
                        }
                        else {
                            notification.show({ message: "This folder move to selected folder not possible. Please try another folder." }, "error");
                        }
                    }
                    else {
                        if ($("#IsCopy").val() === "true") {
                            notification.show({ message: "File has been successfully copied" }, "success");
                        }
                        else {
                            notification.show({ message: "File has been successfully moved" }, "success");
                        }
                        if (data.type === 1) {
                            $("#divCabinetDocList").empty();
                            $("#divCabinetDocList").append('<div class="tiny_loading"></div>');
                            $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + data.encryptedId);
                        }
                        else if (data.type === 2) {
                            $("#divF1DocList").empty();
                            $("#divF1DocList").append('<div class="tiny_loading"></div>');
                            $("#divF1DocList").load('/Document/F1DocList?f1Id=' + data.encryptedId);
                        }
                        else if (data.type === 3) {
                            $("#divF2DocList").empty();
                            $("#divF2DocList").append('<div class="tiny_loading"></div>');
                            $("#divF2DocList").load('/Document/F2DocList?f2Id=' + data.encryptedId);
                        }
                        else if (data.type === 4) {
                            $("#divF3DocList").empty();
                            $("#divF3DocList").append('<div class="tiny_loading"></div>');
                            $("#divF3DocList").load('/Document/F3DocList?f3Id=' + data.encryptedId);
                        }
                        $(".btnUnSelect").click();
                        //load cabinet tree in nav
                        loadCabinetTree();
                        //load storage info
                        storageInfo();
                    }
                    $("#divTreeview").data("kendoWindow").close();
                },
                error: function () {
                    notification.show({ message: "File update was unsuccessful." }, "error");
                    $("#btnTreeOk").prop('disabled', false);
                }
            });
        } else {
            notification.show({ message: "Please select folder." }, "error");
        }
    });

}());