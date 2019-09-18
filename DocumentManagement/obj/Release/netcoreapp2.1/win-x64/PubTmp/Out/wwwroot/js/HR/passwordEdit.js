(function () {
    $(document).ready(function () {
        $("#divPassEditForm").load('/HR/_ChangePassword');
    });
    $("#btnPassRefresh").click(function () {
        $("#divPassEditForm").empty();
        $("#divPassEditForm").append('<div class="tiny_loading"></div>');
        $("#divPassEditForm").load('/HR/_ChangePassword');
    });
    $("#btnPassCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });
    var validation = $("#divPassEditForm").kendoValidator({
        rules: {
            verifyPasswords: function (input) {
                var ret = true;
                if (input.is("[name=confirmPassword]")) {
                    ret = input.val() === $("#password").val();
                }
                return ret;
            }
        },
        messages:
        {
            verifyPasswords: "Passwords do not match!"
        }
    }).data("kendoValidator");

    $("#btnPassUpdate").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/HR/UserUpdate',
                type: 'Post',
                data: {
                    EncryptedId: $("#btnPassUpdate").data("id"),
                    Password: $.trim($("#password").val()),
                    isPassUpdate: true
                },
                success: function (data) {
                    $("#btnPassCancel").closest(".k-window-content").data("kendoWindow").close();
                    if (data === "error") {
                        notification.show({ message: "Password update was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Password has been successfully updated." }, "success");
                    }
                },
                error: function (error) {
                    notification.show({ message: "Password update was unsuccessful." }, "error");
                }
            });
        }
    });
}());