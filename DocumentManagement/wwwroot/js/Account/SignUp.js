(function () {
    $(document).ready(function () {

        var input = document.querySelector("#phone");
        /*var telInput = intlTelInput(input, {
            autoPlaceholder: "polite",
            geoIpLookup: function (callback) {
                $.get("https://ipinfo.io", function () { }, "jsonp").always(function (resp) {
                    var countryCode = resp && resp.country? resp.country : "";
                    callback(countryCode);
                });
            },
            initialCountry: "auto",
            placeholderNumberType: "MOBILE",
            utilsScript: "../lib/intl-tel-input-master/build/js/utils.js"
        });*/
        var validation = $(".form").kendoValidator({
            rules: {
                verifyPasswords: function (input) {
                    var ret = true;
                    if (input.is("[name=confirmPassword]")) {
                        ret = input.val() === $("#password").val();
                    }
                    return ret;
                },
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
                },
                phoneNumber: function (input) {
                    if (input.is("[name=phone]") && $.trim($("#phone").val()) === "") {
                        console.log($("#phone").val());
                        return false;
                        //return telInput.isValidNumber();
                    }
                    return true;
                }
            },
            messages:
            {
                IsEmailExist: "This email is already exists.",
                phoneNumber: "Please enter valid phone number.",
                verifyPasswords: "Passwords do not match!"
            }
        }).data("kendoValidator");
        $("#btnSignUp").click(function () {
            if (validation.validate()) {
                $(this).prop('disabled', true);
                $("#divMessage").addClass('loading');
                var sendData = {
                    Name: $.trim($("#name").val()),
                    UserName: $.trim($("#email").val()),
                    MobileNumber: $.trim($("#phone").val()),
                    Password: $.trim($("#password").val()),
                };
                $.ajax({
                    url: '/Account/UserSave',
                    type: "POST",
                    data: sendData,
                    success: function (result) {
                        $("#divMessage").removeClass('loading');
                        if (result === "success") {
                            $("#divMessage").append('<h3 class="message">An email has been sent to the following email address. Please confirm the email link.</h3>');
                        }
                        else {
                            $("#divMessage").append('<h4 class="message-danger">Save Error.</h4>');
                            $("#btnSignUp").prop('disabled', false);
                        }
                    }
                });
            }
        });
    });

    /*$("#btnEmail").click(function () {
        $("#phone").val("");
        $("#phone").hide();
        $('#phone').prop('required', false);
        $("#email").show();
        $('#email').prop('required', true);
        $("#phoneValidationMsg").hide();
    });

    $("#btnPhone").click(function () {
        $("#email").val("");
        $("#email").hide();
        $('#email').prop('required', false);
        $("#emailValidationMsg").hide();
        $("#phone").show();
        $('#phone').prop('required', true);
    });*/

    function handle(e) {
        if (e.keyCode === 13) {
            $("#btnLogin").click();
        }
    }
}());