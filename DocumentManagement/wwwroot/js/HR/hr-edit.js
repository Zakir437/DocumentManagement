(function () {
    var id = "";
    $(document).ready(function () {
        id = $("#btnUserUpdate").data("id");
        $("#divUserEditForm").load('/HR/_UserEdit?id=' + id);
    });
    $("#btnUserRefresh").click(function () {
        $("#divUserEditForm").empty();
        $("#divUserEditForm").append('<div class="tiny_loading"></div>');
        $("#divUserEditForm").load('/HR/_UserEdit?id=' + id);
    });
    $("#btnUserCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });
    var validation = $("#divUserEditForm").kendoValidator({
        rules: {
            name: function (input) {
                if (input.is("[name=name]") && $.trim(input.val()) === "") {
                    return false;
                }
                return true;
            }
        },
        messages: {
            name: "Please enter user name."
        }
    }).data("kendoValidator");

    $("#btnUserUpdate").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/HR/UserUpdate',
                type: 'Post',
                data: {
                    EncryptedId: id,
                    Name: $.trim($("#name").val())
                },
                success: function (data) {
                    $("#btnUserCancel").closest(".k-window-content").data("kendoWindow").close();
                    if (data === "error") {
                        notification.show({ message: "User update was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "User has been successfully updated." }, "success");
                        onAjaxLoad("User Details", "/HR/UserDetails?q=" + id + "&isEdit=true");
                    }
                },
                error: function (error) {
                    notification.show({ message: "User update was unsuccessful." }, "error");
                }
            });
        }
    });
}());