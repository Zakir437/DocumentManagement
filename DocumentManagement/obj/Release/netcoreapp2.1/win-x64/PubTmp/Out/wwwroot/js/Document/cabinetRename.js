(function () {
    var id = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        id = parseInt($("#cId").val());
        $("#divCabinetForm").load('/Document/_CabinetRename?id=' + id);
    });
    $("#btnCabinetRenameRefresh").click(function () {
        $("#divCabinetForm").empty();
        $("#divCabinetForm").append('<div class="tiny_loading"></div>');
        $("#divCabinetForm").load('/Document/_CabinetRename?id=' + id);
    });
    $("#btnCabinetRenameCancel").click(function () {
        $("#divRenameWin").data("kendoWindow").close();
    });

    var validation = $("#divCabinetForm").kendoValidator({
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
                    data = { id: id, name: name };
                    $.ajax({
                        url: '/RemoteValidation/CabinetNameExist',
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
            name: "Please enter cabinet name.",
            IsNameExist: "This cabinet name is already exist."
        }
    }).data("kendoValidator");

    $("#btnCabinetRenameOk").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Document/CabinetNameUpdate',
                type: 'POST',
                data: { name: $.trim($("#name").val()), id: id },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Cabinet name has been successfully updated." }, "success");
                        $("#divCabinetFolderList").empty();
                        $("#divCabinetFolderList").append('<div class="tiny_loading"></div>');
                        $("#divCabinetFolderList").load('/Document/CabinetList');

                        loadCabinetTree();
                    }
                    else {
                        notification.show({ message: "Cabinet rename was unsuccessful." }, "error");
                    }
                    $("#btnCabinetRenameCancel").closest(".k-window-content").data("kendoWindow").close();
                },
                error: function () {
                    notification.show({ message: "Cabinet rename was unsuccessful." }, "error");
                    $("#btnCabinetRenameCancel").closest(".k-window-content").data("kendoWindow").close();
                }
            });
        }
    });


}())