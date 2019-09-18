(function () {
    var id = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = parseInt($("#f1Id").val());
        $("#divF1RenameForm").load('/Document/_F1Rename?id=' + id);
    });
    $("#btnF1RenameRefresh").click(function () {
        $("#divF1RenameForm").empty();
        $("#divF1RenameForm").append('<div class="tiny_loading"></div>');
        $("#divF1RenameForm").load('/Document/_F1Rename?id=' + id);
    });
    $("#btnF1RenameCancel").click(function () {
        $("#divRenameWin").data("kendoWindow").close();
    });

    var validation = $("#divF1RenameForm").kendoValidator({
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
                    data = { id: id, cId: parseInt($("#Cid").val()), name: name };
                    $.ajax({
                        url: '/RemoteValidation/F1NameExist',
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

    $("#btnF1RenameOk").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/F1RenameSave',
                type: 'POST',
                data: { name: $.trim($("#name").val()), id: id },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Folder rename was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder name has been successfully updated." }, "success");
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + data.cabinetId);
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