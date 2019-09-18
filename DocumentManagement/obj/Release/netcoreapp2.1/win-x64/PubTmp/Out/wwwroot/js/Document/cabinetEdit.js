(function () {
    var id = "", countId = 0, validation;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = $("#cabinetId").val();
        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + id);
    });
    $("#btnCabinetRefresh").click(function () {
        id = $("#cabinetId").val(); 
        $("#divCabinetList").empty();
        $("#divCabinetList").append('<div class="tiny_loading"></div>');
        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + id);
    });
    $("#btnCabinetCancel").click(function () {
        $("#divEditWin").data("kendoWindow").close();
    });
    $("#divCabinetList").kendoValidator().data("kendoValidator");

    //***************************Master folder name edit***********************
    $("#divCabinetList").on('click', '.btnEditF1', function () {
        $(this).hide();
        countId = parseInt($(this).data("count"));
        $("#f1Name_" + countId).prop('disabled', false);
        $("#f1Name_" + countId).focus();
        $("#btnUpdateF1_" + countId).show();
    });

    $("#divCabinetList").on('click', '.btnUpdateF1', function () {
        countId = parseInt($(this).data("count"));
        id = $(this).data("id");
        validation = $("#divF1_" + countId).kendoValidator().data("kendoValidator");
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F1NameUpdate',
                type: 'Post',
                data: { name: $.trim($("#f1Name_" + countId).val()), id : id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Update was unsuccessful." }, "error");
                        $("#btnUpdateF1_" + countId).prop('disabled', false);
                    }
                    else {
                        notification.show({ message: "Master folder name has been successfully updated." }, "success");
                        $("#f1Name_" + countId).prop('disabled', true);
                        $("#btnUpdateF1_" + countId).hide();
                        $("#btnEditF1_" + countId).show();
                        $("#btnUpdateF1_" + countId).prop('disabled', false);

                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Update was unsuccessful." }, "error");
                    $("#btnUpdateF1_" + countId).prop('disabled', false);
                }
            });
        }
    });

    //***************************folder name edit***********************
    $("#divCabinetList").on('click', '.btnEditF2', function () {
        $(this).hide();
        countId = parseInt($(this).data("count"));
        $("#f2Name_" + countId).prop('disabled', false);
        $("#f2Name_" + countId).focus();
        $("#btnUpdateF2_" + countId).show();
    });

    $("#divCabinetList").on('click', '.btnUpdateF2', function () {
        countId = parseInt($(this).data("count"));
        id = $(this).data("id");
        validation = $("#divF2_" + countId).kendoValidator().data("kendoValidator");
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F2NameUpdate',
                type: 'Post',
                data: { name: $.trim($("#f2Name_" + countId).val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Update was unsuccessful." }, "error");
                        $("#btnUpdateF2_" + countId).prop('disabled', false);
                    }
                    else {
                        notification.show({ message: "Folder name has been successfully updated." }, "success");
                        $("#f2Name_" + countId).prop('disabled', true);
                        $("#btnUpdateF2_" + countId).hide();
                        $("#btnEditF2_" + countId).show();
                        $("#btnUpdateF2_" + countId).prop('disabled', false);

                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Update was unsuccessful." }, "error");
                    $("#btnUpdateF2_" + countId).prop('disabled', false);
                }
            });
        }
    });


    //***************************sub folder name edit***********************
    $("#divCabinetList").on('click', '.btnEditF3', function () {
        $(this).hide();
        countId = parseInt($(this).data("count"));
        $("#f3Name_" + countId).prop('disabled', false);
        $("#f3Name_" + countId).focus();
        $("#btnUpdateF3_" + countId).show();
    });

    $("#divCabinetList").on('click', '.btnUpdateF3', function () {
        countId = parseInt($(this).data("count"));
        id = $(this).data("id");
        validation = $("#divF3_" + countId).kendoValidator().data("kendoValidator");
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F3NameUpdate',
                type: 'Post',
                data: { name: $.trim($("#f3Name_" + countId).val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Update was unsuccessful." }, "error");
                        $("#btnUpdateF3_" + countId).prop('disabled', false);
                    }
                    else {
                        notification.show({ message: "Sub folder name has been successfully updated." }, "success");
                        $("#f3Name_" + countId).prop('disabled', true);
                        $("#btnUpdateF3_" + countId).hide();
                        $("#btnEditF3_" + countId).show();
                        $("#btnUpdateF3_" + countId).prop('disabled', false);

                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Update was unsuccessful." }, "error");
                    $("#btnUpdateF3_" + countId).prop('disabled', false);
                }
            });
        }
    });

    //***************************master folder delete***********************
    $("#divCabinetList").on('click', '.btnDeleteF1', function () {
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
                data: { pId: id, type: 2, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        $("#divCabinetList").empty();
                        $("#divCabinetList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + $("#cabinetId").val());
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
        //var f1DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f1DeleteWin.refresh('/Document/F1DeleteAlert?id=' + id + '&isPartial=true');
        //f1DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //***************************folder delete***********************
    $("#divCabinetList").on('click', '.btnDeleteF2', function () {
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
                data: { pId: id, type: 3, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        $("#divCabinetList").empty();
                        $("#divCabinetList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + $("#cabinetId").val());
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
        //var f2DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f2DeleteWin.refresh('/Document/F2DeleteAlert?id=' + id + '&type=1');
        //f2DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //***************************sub folder delete***********************
    $("#divCabinetList").on('click', '.btnDeleteF3', function () {
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
                        $("#divCabinetList").empty();
                        $("#divCabinetList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + $("#cabinetId").val());
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
        //f3DeleteWin.refresh('/Document/F3DeleteAlert?id=' + id + '&type=1');
        //f3DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

}());