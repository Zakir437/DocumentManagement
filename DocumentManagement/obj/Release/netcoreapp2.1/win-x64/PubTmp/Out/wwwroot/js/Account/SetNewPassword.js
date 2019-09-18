(function () {
    var validation = $(".form").kendoValidator({
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
    $("#btnReset").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            var id = $(this).data("id");
            $("#divMessage").addClass('loading');
            $.ajax({
                url: '/Account/SetPassword',
                type: "POST",
                data: { id: id, password: $.trim($("#password").val()) },
                success: function (result) {
                    $("#divMessage").removeClass('loading');
                    if (result === "success") {
                        $("#divMessage").append('<h3 class="message">New password has been successfully reset.</h3><p class="message">Back to <a href="/Account/Login">Log In</a></p>');
                    }
                    else {
                        $("#divMessage").append('<h3 class="message-danger">New password set error.</h3>');
                        $("#btnReset").prop('disabled', false);
                    }
                }
            });
        }
    });
}());