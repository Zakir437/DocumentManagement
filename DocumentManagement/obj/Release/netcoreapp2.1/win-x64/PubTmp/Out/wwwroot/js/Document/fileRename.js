(function () {
    var id = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = parseInt($("#fId").val());
        $("#divFileRenameForm").load('/Document/_FileRename?id=' + id);
    });
    $("#btnFileRenameRefresh").click(function () {
        $("#divFileRenameForm").empty();
        $("#divFileRenameForm").append('<div class="tiny_loading"></div>');
        $("#divFileRenameForm").load('/Document/_FileRename?id=' + id);
    });
    $("#btnFileRenameCancel").click(function () {
        //$(this).closest(".k-window-content").data("kendoWindow").close();
        $("#divRenameWin").data("kendoWindow").close();
    });

    var validation = $("#divFileRenameForm").kendoValidator({
        rules: {
            name: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) === "") {
                    return false;
                }
                return true;
            }
        },
        messages: {
            name: "Please enter file name."
        }
    }).data("kendoValidator");

    $("#btnFileRenameOk").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/FileRenameSave',
                type: 'POST',
                data: { name: $.trim($("#name").val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "File rename was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "File name has been successfully updated." }, "success");
                        $("#divFileList").empty();
                        if (data.type === 1) {
                            $("#divCabinetDocList").empty();
                            $("#divCabinetDocList").append('<div class="tiny_loading"></div>');
                            $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + data.cabinetId);
                        }
                        else if (data.type === 2) {
                            $("#divF1DocList").empty();
                            $("#divF1DocList").append('<div class="tiny_loading"></div>');
                            $("#divF1DocList").load('/Document/F1DocList?f1Id=' + data.f1Id);
                        }
                        else if (data.type === 3) {
                            $("#divF2DocList").empty();
                            $("#divF2DocList").append('<div class="tiny_loading"></div>');
                            $("#divF2DocList").load('/Document/F2DocList?f2Id=' + data.f2Id);
                        }
                        else if (data.type === 4) {
                            $("#divF3DocList").empty();
                            $("#divF3DocList").append('<div class="tiny_loading"></div>');
                            $("#divF3DocList").load('/Document/F3DocList?f3Id=' + data.f3Id);
                        }
                    }
                    $("#divRenameWin").data("kendoWindow").close();
                },
                error: function () {
                    notification.show({ message: "File rename was unsuccessful." }, "error");
                    $("#divRenameWin").data("kendoWindow").close();
                }
            });
        }
    });
}())