(function () {
    var id = "", countId = 0, validation;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = $("#F2Id_").val();
        $("#divF2EditList").load('/Document/_F2Edit?id=' + id);
    });
    $("#btnF2Refresh").click(function () {
        id = $("#F2Id_").val();
        $("#divF2EditList").empty();
        $("#divF2EditList").append('<div class="tiny_loading"></div>');
        $("#divF2EditList").load('/Document/_F2Edit?id=' + id);
    });
    $("#btnF2Cancel").click(function () {
        $("#divEditWin").data("kendoWindow").close();
    });
    $("#divF2EditList").kendoValidator().data("kendoValidator");

    //***************************folder name edit***********************
    //$("#divF2EditList").on('click', '.btnEditF2', function () {
    //    $(this).hide();
    //    countId = parseInt($(this).data("count"));
    //    $("#f2Name_" + countId).prop('disabled', false);
    //    $("#f2Name_" + countId).focus();
    //    $("#btnUpdateF2_" + countId).show();
    //});

    //$("#divF2EditList").on('click', '.btnUpdateF2', function () {
    //    countId = parseInt($(this).data("count"));
    //    id = $(this).data("id");
    //    validation = $("#divF2_" + countId).kendoValidator().data("kendoValidator");
    //    if (validation.validate()) {
    //        $(this).prop('disabled', true);
    //        $.ajax({
    //            url: '/Document/F2NameUpdate',
    //            type: 'Post',
    //            data: { name: $.trim($("#f2Name_" + countId).val()), id: id },
    //            success: function (data) {
    //                if (data === "error") {
    //                    notification.show({ message: "Update was unsuccessful." }, "error");
    //                    $("#btnUpdateF2_" + countId).prop('disabled', false);
    //                }
    //                else {
    //                    notification.show({ message: "Folder name has been successfully updated." }, "success");
    //                    $("#f2Name_" + countId).prop('disabled', true);
    //                    $("#btnUpdateF2_" + countId).hide();
    //                    $("#btnEditF2_" + countId).show();
    //                    $("#btnUpdateF2_" + countId).prop('disabled', false);

    //                    loadCabinetTree();

    //                }
    //            },
    //            error: function () {
    //                notification.show({ message: "Update was unsuccessful." }, "error");
    //                $("#btnUpdateF2_" + countId).prop('disabled', false);
    //            }
    //        });
    //    }
    //});

    //***************************sub folder name edit***********************
    $("#divF2EditList").on('click', '.btnEditF3', function () {
        $(this).hide();
        countId = parseInt($(this).data("count"));
        $("#f3Name2_" + countId).prop('disabled', false);
        $("#f3Name2_" + countId).focus();
        $("#btn2UpdateF3_" + countId).show();
    });

    $("#divF2EditList").on('click', '.btnUpdateF3', function () {
        countId = parseInt($(this).data("count"));
        id = $(this).data("id");
        validation = $("#divF3_" + countId).kendoValidator().data("kendoValidator");
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F3NameUpdate',
                type: 'Post',
                data: { name: $.trim($("#f3Name2_" + countId).val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Update was unsuccessful." }, "error");
                        $("#btn2UpdateF3_" + countId).prop('disabled', false);
                    }
                    else {
                        notification.show({ message: "Sub folder name has been successfully updated." }, "success");
                        $("#f3Name2_" + countId).prop('disabled', true);
                        $("#btn2UpdateF3_" + countId).hide();
                        $("#btn2EditF3_" + countId).show();
                        $("#btn2UpdateF3_" + countId).prop('disabled', false);

                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Update was unsuccessful." }, "error");
                    $("#btn2UpdateF3_" + countId).prop('disabled', false);
                }
            });
        }
    });

    //***************************sub folder delete***********************
    $("#divF2EditList").on('click', '.btnDeleteF3', function () {
        id = $(this).data("id");

        var kendoWindow = $("<div />").kendoWindow({
            actions: ["Close"],
            title: "Alert",
            resizable: false,
            width: "30%",
            modal: true,
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        msg = "Are you sure want to delete this folder?";
        var template = kendo.template($("#temp_win_Doc_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_doc_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_doc_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/DocumentDelete',
                type: 'Post',
                data: { pId: id, type: 4, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        $("#divF2EditList").empty();
                        $("#divF2EditList").append('<div class="tiny_loading"></div>');
                        $("#divF2EditList").load('/Document/_F2Edit?id=' + $("#F2Id_").val());
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";

        //$("#divDeleteWin").empty();
        //$("#divDeleteWin").kendoWindow({
        //    actions: ["Close"],
        //    draggable: false,
        //    modal: true,
        //    visible: false,
        //    width: "45%",
        //    height: "50%",
        //    title: 'Alert',
        //    close: onWindowClose
        //});
        //var f3DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f3DeleteWin.refresh('/Document/F3DeleteAlert?id=' + id + '&type=3');
        //f3DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

}());