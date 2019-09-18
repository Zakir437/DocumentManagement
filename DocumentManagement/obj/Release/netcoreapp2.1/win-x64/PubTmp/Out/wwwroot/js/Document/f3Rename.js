(function () {
    var id = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = parseInt($("#f3Id").val());
        $("#divF3RenameForm").load('/Document/_F3Rename?id=' + id);
    });
    $("#btnF3RenameRefresh").click(function () {
        $("#divF3RenameForm").empty();
        $("#divF3RenameForm").append('<div class="tiny_loading"></div>');
        $("#divF3RenameForm").load('/Document/_F3Rename?id=' + id);
    });
    $("#btnF3RenameCancel").click(function () {
        $("#divRenameWin").data("kendoWindow").close();
    });
    var validation = $("#divF3RenameForm").kendoValidator({
        rules: {
            name: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) === "") {
                    return false;
                }
                return true;
            },
            IsNameExist: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) !== "") {
                    var isInvalid;
                    var data = {};
                    var name = $.trim($("#name").val());
                    data = { id: id, cId: parseInt($("#Cid").val()), f1Id: parseInt($("#f1Id").val()), f2Id: parseInt($("#f2Id").val()),  name: name };
                    $.ajax({
                        url: '/RemoteValidation/F3NameExist',
                        mode: "abort",
                        port: "validate" + input.attr('name'),
                        dataType: "json",
                        type: input.attr("data-val-remote-type"),
                        data: data,
                        async: false,
                        success: function (response) {
                            isInvalid = response;
                            if (isInvalid === true) {
                                isInvalid = true;
                            }
                            else {
                                isInvalid = false;
                            }
                        }
                    });
                    return isInvalid;
                }
                return true;
            }
        },
        messages: {
            name: "Please enter folder name.",
            IsNameExist: "This folder name is already exist."
        }
    }).data("kendoValidator");

    $("#btnF3RenameOk").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F3RenameSave',
                type: 'POST',
                data: { name: $.trim($("#name").val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Folder rename was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder name has been successfully updated." }, "success");
                        $("#divF2DocList").empty();
                        $("#divF2DocList").append('<div class="tiny_loading"></div>');
                        $("#divF2DocList").load('/Document/F2DocList?f2Id=' + data.f2Id);
                        loadCabinetTree();
                    }
                    $("#divRenameWin").data("kendoWindow").close();
                },
                error: function () {
                    notification.show({ message: "Folder rename was unsuccessful." }, "error");
                    $("#divRenameWin").data("kendoWindow").close();
                }
            });
        }
    });
}())