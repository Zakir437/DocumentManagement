(function () {
    var validation = $(".form").kendoValidator({
        rules: {
            IsEmailExist: function (input) {
                if (input.is("[name=email]") && $.trim(input.val()) !== "") {
                    var isInvalid;
                    var data = {};
                    var email = $.trim($("#email").val());
                    data = { email: email };
                    $.ajax({
                        url: '/RemoteValidation/UserEmailExist',
                        mode: "abort",
                        port: "validate" + input.attr('name'),
                        dataType: "json",
                        type: input.attr("data-val-remote-type"),
                        data: data,
                        async: false,
                        success: function (response) {
                            isInvalid = response;
                            if (isInvalid === true) {
                                isInvalid = false;
                            }
                            else {
                                isInvalid = true;
                            }
                        }
                    });
                    return isInvalid;
                }
                return true;
            }
        },
        messages:
        {
            IsEmailExist: "This email not registered."
        }
    }).data("kendoValidator");

    function handle(e) {
        if (e.keyCode === 13) {
            $("#btnReset").click();
        }
    }

    $("#btnReset").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $("#divMessage").addClass('loading');
            $.ajax({
                url: '/Account/SendResetLink',
                type: "POST",
                data: { email: $.trim($("#email").val()) },
                success: function (result) {
                    $("#divMessage").removeClass('loading');
                    if (result === "success") {
                        $("#divMessage").append('<h3 class="message">An email has been sent to you. Please use link to reset your password.</h3>');
                    }
                    else {
                        $("#divMessage").append('<h4 class="message-danger">Email can not send. Please try again.</h4>');
                        $("#btnReset").prop('disabled', false);
                    }
                }
            });
        }
    });
}());