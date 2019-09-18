(function () {
    var f1Id = "", fileId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        f1Id = $("#F1IdInDel").val();
        $("#divF1Delete").load('/Document/_F1DeleteAlert?id=' + f1Id + '&isMultiple=' + $("#IsMultiple").val());
    });
    $("#btnF1DeleteCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });

    //Check/uncheck all
    $("#divF1Delete").on('click', '#checkAll', function () {
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
    $("#divF1Delete").on('click', '.check', function () {
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

    $("#divF1Delete").on('click', '.check', function () {
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

    $("#btnF1DeleteOk").click(function () {
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
            url: '/Document/F1Delete',
            type: 'Post',
            data: { id: f1Id, isMultiple: $("#IsMultiple").val(), encryptId: $("#EncryptId").val(), unSelectIds: unSelectIds.toString(), selectIds: selectIds.toString() },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully deleted." }, "success");
                    if ($("#IsPartial").val() === true) {
                        $("#divCabinetList").empty();
                        $("#divCabinetList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetList").load('/Document/_CabinetEdit?id=' + data.cabinetId);
                    }
                    else {
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + data.cabinetId);
                        $(".btnUnSelect").click();
                    }

                    loadCabinetTree();
                }
                $("#btnF1DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            },
            error: function () {
                notification.show({ message: "Delete was unsuccessful." }, "error");
                $("#btnF1DeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            }
        });
    });
}());