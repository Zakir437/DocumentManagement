(function () {
    var f2Id = "", fileId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        f2Id = $("#F2IdInDelete").val();
        $("#divF2Delete").load('/Document/_F2DeleteAlert?id=' + f2Id + '&isMultiple=' + $("#IsMultiple").val());
    });
    $("#btnF2DeleteCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });

    //Check/uncheck all
    $("#divF2Delete").on('click', '#checkAll', function () {
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
    $("#divF2Delete").on('click', '.check', function () {
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

    $("#divF2Delete").on('click', '.check', function () {
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

    $("#btnF2DeleteOk").click(function () {
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
            url: '/Document/F2Delete',
            type: 'Post',
            data: { id: f2Id, isMultiple: $("#IsMultiple").val(), encryptId: $("#EncryptId").val(), unSelectIds: unSelectIds.toString(), selectIds: selectIds.toString(), type: $("#Type").val() },
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
                    else {
                        $("#divF1DocList").empty();
                        $("#divF1DocList").append('<div class="tiny_loading"></div>');
                        $("#divF1DocList").load('/Document/F1DocList?f1Id=' + data.encryptId);
                        $(".btnUnSelect").click();
                    }

                    loadCabinetTree();
                }
                $("#btnF2DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            },
            error: function () {
                notification.show({ message: "Delete was unsuccessful." }, "error");
                $("#btnF2DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            }
        });
    });
}());