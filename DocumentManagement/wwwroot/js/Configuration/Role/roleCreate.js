(function () {
    $(document).ready(function () {
        $("#name").focus();
    });
    var validation = $("form").kendoValidator({
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
                    data = { name: name };
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

    $("form.roleCreateForm").submit(function (e) {
        e.preventDefault();
        $("#btnSubmit").prop('disabled', true);
        if (validation.validate()) {
            $.ajax({
                url: '/Configuration/RoleSave',
                type: 'POST',
                data: { name: $.trim($("#name").val()) },
                success: function (data) {
                    $("#btnSubmit").prop('disabled', false);
                    if (data === "error") {
                        notification.show({ message: "New role create was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "New role has been successfully created." }, "success");
                        $("form").trigger('reset');
                    }
                },
                error: function () {
                    $("form").trigger('reset');
                    $("#btnSubmit").prop('disabled', false);
                    notification.show({ message: "New role create was unsuccessful." }, "error");
                }
            });
        }
    });
}());