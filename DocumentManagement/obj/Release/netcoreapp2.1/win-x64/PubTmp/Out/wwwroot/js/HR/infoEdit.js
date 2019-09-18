(function () {
    var id = "";
    $(document).ready(function () {
        id = $("#btnInfoUpdate").data("id");
        $("#divInfoEditForm").load('/HR/_UserInfoEdit?id=' + id);
    });
    $("#btnInfoRefresh").click(function () {
        $("#divInfoEditForm").empty();
        $("#divInfoEditForm").append('<div class="tiny_loading"></div>');
        $("#divInfoEditForm").load('/HR/_UserInfoEdit?id=' + id);
    });
    $("#btnInfoCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });
    var validation = $("#divInfoEditForm").kendoValidator({
        rules: {
            datepicker: function (input) {
                if (input.is("[data-role=datepicker]") && input.val() !== "") {
                    return input.data("kendoDatePicker").value();
                } else {
                    return true;
                }
            }
        },
        messages: {
            datepicker: "Please enter valid date!"
        }
    }).data("kendoValidator");

    $("#btnInfoUpdate").click(function () {
        if (validation.validate()) {
            $(this).prop('disabled', true);
            $.ajax({
                url: '/HR/UserUpdate',
                type: 'Post',
                data: {
                    EncryptedId: id,
                    MobileNumber: $.trim($("#MobileNumber").val()),
                    DOB: $("#DOB").val(),
                    RoleId: $("#Role").val(),
                    isInfoUpdate : true
                },
                success: function (data) {
                    $("#btnInfoCancel").closest(".k-window-content").data("kendoWindow").close();
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