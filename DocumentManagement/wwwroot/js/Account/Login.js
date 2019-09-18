(function () {
    $("form").kendoValidator({
    }).data("kendoValidator");
    /*$("#btnLogin").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            var sendData = {
                username: $.trim($("#username").val()),
                password: $.trim($("#password").val())
            };
            $.ajax({
                url: '/Account/Login',
                type: "POST",
                data: sendData,
                success: function (result) {
                    if (result === "success") {
                        window.location.href = '/Home/Index';
                    }
                    else if (result === "notConfirmed") {
                        alert("Please confirm your email.");
                        window.location.reload();
                    }
                    else {
                        alert("Username or password incorrect");
                        window.location.reload();
                    }
                }
            });
        }
    });*/

    /*function handle(e) {
        if (e.keyCode === 13) {
            $("#btnLogin").click();
        }
    }*/
}());