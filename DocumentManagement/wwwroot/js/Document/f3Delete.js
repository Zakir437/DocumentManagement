(function () {
    var f3Id = "", fileId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        f3Id = $("#F3IdInDelete").val();
        $("#divF3Delete").load('/Document/_F3DeleteAlert?id=' + f3Id + '&isMultiple=' + $("#IsMultiple").val());
    });
    $("#btnF3DeleteCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });

    //Check/uncheck all
    $("#divF3Delete").on('click', '#checkAll', function () {
        if ($(this).is(":checked")) {
            $(".check").each(function () {
                fileId = parseInt($(this).data("id"));
                $("#check_" + fileId).prop('checked', true);
            });
        }
        else {
            $(".check").each(function () {
                fileId = parseInt($(this).data("id"));
                $("#check_" + fileId).prop('checked', false);
            });
        }
    });

    //check/uncheck single
    $("#divF3Delete").on('click', '.check', function () {
        var checkAll = true;
        $('.check').each(function () {
            fileId = parseInt($(this).data("id"));
            if ($("#check_" + fileId).is(":checked") === false) {
                checkAll = false;
            }
        });
        if (checkAll) {
            $("#checkAll").prop('checked', true);
        }
        else {
            $("#checkAll").prop('checked', false);
        }
    });

    $("#divF3Delete").on('click', '.check', function () {
        var checkAll = true;
        $('.check').each(function () {
            fileId = parseInt($(this).data("id"));
            if ($("#check_" + fileId).is(":checked") === false) {
                checkAll = false;
            }
        });
        if (checkAll) {
            $("#checkAll").prop('checked', true);
        }
        else {
            $("#checkAll").prop('checked', false);
        }
    });

    $("#btnF3DeleteOk").click(function () {
        $(this).prop('disabled', true);
        var selectIds = [];
        var unSelectIds = [];
        if ($("#checkAll").length === 1) {
            $('.check').each(function () {
                fileId = parseInt($(this).data("id"));
                if ($("#check_" + fileId).is(":checked") === false) {
                    unSelectIds.push(fileId);
                }
                else {
                    selectIds.push(fileId);
                }
            });
        }
        $.ajax({
            url: '/Document/F3Delete',
            type: 'Post',
            data: { id: f3Id, isMultiple: $("#IsMultiple").val(), encryptId: $("#EncryptId").val(), unSelectIds: unSelectIds.toString(), selectIds: selectIds.toString(), type: $("#Type").val() },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully deleted." }, "success");
                    if (data.type === 1) {
                        $("#divCabinetList").empty();
                        $("#divCabinetList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + data.encryptId);
                    }
                    else if (data.type === 2) {
                        $("#divF1FormList").empty();
                        $("#divF1FormList").append('<div class="tiny_loading"></div>');
                        $("#divF1FormList").load('/Document/_F1Edit?id=' + data.encryptId);
                    }
                    else if (data.type === 3) {
                        $("#divF2EditList").empty();
                        $("#divF2EditList").append('<div class="tiny_loading"></div>');
                        $("#divF2EditList").load('/Document/_F2Edit?id=' + data.encryptId);
                    }
                    else {
                        $("#divF2DocList").empty();
                        $("#divF2DocList").append('<div class="tiny_loading"></div>');
                        $("#divF2DocList").load('/Document/F2DocList?f2Id=' + data.encryptId);
                        $(".btnUnSelect").click();
                    }
                    loadCabinetTree();
                }
                $("#btnF3DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            },
            error: function () {
                notification.show({ message: "Delete was unsuccessful." }, "error");
                $("#btnF3DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            }
        });
    });
}());