(function () {
    var id = $("#btnRoleUpdate").data("id");
    $(document).ready(function () {
        $("#divRoleEditForm").load('/Configuration/RoleEditPartial?id=' + id);
    });
    $("#btnRoleRefresh").click(function () {
        $("#divRoleEditForm").empty();
        $("#divRoleEditForm").append('<div class="tiny_loading"></div>');
        $("#divRoleEditForm").load('/Configuration/RoleEditPartial?id=' + id);
    });
    $("#btnRoleCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });
    var validation = $("#divRoleEditForm").kendoValidator({
        rules: {
            name: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) === "") {
                    return false;
                }
                return true;
            },
            IsRoleNameExist: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) !== "") {
                    var isInvalid;
                    var data = {};
                    var name = $.trim($("#name").val());
                    data = { name: name, id: id };
                    $.ajax({
                        url: '/RemoteValidation/RoleNameExist',
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
            name: "Please enter role name.",
            IsRoleNameExist: "This role name is already exist."
        }
    }).data("kendoValidator");

    $("#btnRoleUpdate").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/Configuration/RoleSave',
                type: 'Post',
                data: { name: $.trim($("#name").val()), id : id },
                success: function (data) {
                    $("#divRoleEditWin").data("kendoWindow").close();
                    if (data === "error") {
                        notification.show({ message: "Role update was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Role has been successfully updated." }, "success");
                        $("#divRoleGrid").data("kendoGrid").dataSource.read();
                    }
                },
                error: function () {
                    notification.show({ message: "Role update was unsuccessful." }, "error");
                    $("#btnRoleUpdate").prop('disabled', false);
                }
            });
        }
    });

}());