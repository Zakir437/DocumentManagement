(function () {
    var cabinetId = "", fileId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        cabinetId = $("#CabinetId").val();
        $("#divCabinetDelete").load('/Document/_CabinetDeleteAlert?id=' + cabinetId);
    });
    $("#btnCabinetDeleteCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });

    //Check/uncheck all
    $("#divCabinetDelete").on('click', '#checkAll', function () {
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
    $("#divCabinetDelete").on('click', '.check', function () {
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

    $("#divCabinetDelete").on('click', '.check', function () {
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

    $("#btnCabinetDeleteOk").click(function () {
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
            url: '/Document/CabinetDelete',
            type: 'Post',
            data: { id: cabinetId, unSelectIds: unSelectIds.toString(), selectIds: selectIds.toString() },
            success: function (data) {
                if (data === "success") {
                    notification.show({ message: "Cabinet has been successfully deleted." }, "success");
                    $("#divCabinetFolderList").empty();
                    $("#divCabinetFolderList").append('<div class="tiny_loading"></div>');
                    $("#divCabinetFolderList").load('/Document/CabinetList');
                }
                else {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
                $("#btnCabinetDeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            },
            error: function () {
                notification.show({ message: "Delete was unsuccessful." }, "error");
                $("#btnCabinetDeleteCancel").closest(".k-window-content").data("kendoWindow").close();
            }
        });
    });
}());