(function () {
    (function (e) {
        //Send Last 4 Digit of mobile number
        $("#btn4DigitSend").click(function () {
            var validation4digit = $(".form .OTP_Number_Check").kendoValidator({
                rules: {
                    last4digit: function (input) {
                        if (input.is("[name=last4digit]") && $.trim($("#last4digit").val()) === "") {
                            return false;
                        }
                        return true;
                    }
                },
                messages:
                {
                    last4digit: "Last 4 digit of your given mobile number"
                }
            }).data("kendoValidator");
            if (validation4digit.validate()) {
                $(this).prop('disabled', true);
                $("#divMessage").empty().addClass('loading');
                var sendData = {
                    UserName: $.trim($("#UserName").val()),
                    Password: $.trim($("#Password").val()),
                    IsLoginBefore: $.trim($("#IsLoginBefore").val()),
                    RememberMe: $.trim($("#RememberMe").val()),
                    ReturnUrl: $.trim($("#ReturnUrl").val()),
                    Last4Digit: $.trim($("#last4digit").val())
                };
                $.ajax({
                    url: '/Account/ConfirmOTP4Digit',
                    type: "POST",
                    data: sendData,
                    success: function (result) {
                        $("#divMessage").removeClass('loading');
                        if (result === "success") {
                            $("#divMessage").empty().append('<h3 class="message">A verification code has been send to your mobile number. Please submit the code...</h3>');
                            $(".OTP_Number_Check").hide();
                            $(".OTP_Code_Check").show();
                        }
                        else {
                            $("#divMessage").empty().append("<h4 class='message-danger'>That doesn't match the mobile number associated with your account. Please type the correct one.</h4>");
                            $("#btn4DigitSend").prop('disabled', false);
                        }
                    }
                });
            }
        });

        //Send 6 digit varification code
        $("#btnVerifyCodeSend").click(function () {
            var validationcode = $(".form .OTP_Code_Check").kendoValidator({
                rules: {
                    verificationCode: function (input) {
                        if (input.is("[name=verificationCode]") && $.trim($("#verificationCode").val()) === "") {
                            return false;
                        }
                        return true;
                    }
                },
                messages:
                {
                    verificationCode: "Please enter the verification code"
                }
            }).data("kendoValidator");
            if (validationcode.validate()) {
                $(this).prop('disabled', true);
                $("#divMessage").empty().addClass('loading');
                var sendData = {
                    UserName: $.trim($("#UserName").val()),
                    Password: $.trim($("#Password").val()),
                    IsLoginBefore: $.trim($("#IsLoginBefore").val()),
                    RememberMe: $.trim($("#RememberMe").val()),
                    ReturnUrl: $.trim($("#ReturnUrl").val()),
                    VarificationCode: $.trim($("#verificationCode").val()),
                    SaveBrowser: $("#SaveBrowser").is(":checked")
                };
                console.log(sendData);
                $.ajax({
                    url: '/Account/OTPVerificationCodeCheck',
                    type: "POST",
                    data: sendData,
                    success: function (result) {
                        $("#divMessage").removeClass('loading');
                        if (result === "success") {
                            location.href = "/Account/OAuth";
                        }
                        else {
                            $("#divMessage").empty().append("<h4 class='message-danger'>That code didn't work.Check the code and try again.</h4>");
                            $("#btnVerifyCodeSend").prop('disabled', false);
                        }
                    }
                });
            }
        });
    }());

    //I have a code click
    $(".OTP_Number_Check").on("click", ".iHaveACode", function () {
        $(".k-invalid-msg").hide();
        $("#divMessage").empty();
        $(".OTP_Number_Check").hide();
        $(".OTP_Code_Check").show();
    });

    //Back click
    $(".OTP_Code_Check").on("click", ".codeSendBack", function () {
        $(".k-invalid-msg").hide();
        $("#divMessage").empty();
        $(".OTP_Number_Check").show();
        $(".OTP_Code_Check").hide();
    });
}());