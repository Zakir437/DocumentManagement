(function () {
    /*$("#divAllSystemConfiguration").on('click', '.btn_test_window', function () {
        id = $(this).data("id");
        $("#test_window").empty().append("<div id='window'><button id='btn_close'>close</button></div>");
        $("#window").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "25%",
            height: "20%",
            title: 'Edit',
            resizable: false,
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var window = $("#window").data("kendoWindow");
        window.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    $(document).on("click", "#btn_close", function () {
    //$("#btn_close").click(function () {
        console.log('close');
        $("#window").data("kendoWindow").close();
    });*/

    //Theme change click
    $("#divChoseTheme").on("click", "label .sb-settingspane-theme", function () {
        //console.log("test");
        $("#divChoseTheme > label > span").removeClass("sb-settingspane-theme-selection");
        $(this).siblings("span.sb-settingspane-theme-container").addClass("sb-settingspane-theme-selection");
        let themeName = $(this).data('name');
        if (themeName === "Light") {
            removeSbTheme();
            $("body").addClass("sb-theme-light");
        }
        else if (themeName === "Blue") {
            removeSbTheme();
            $("body").addClass("sb-theme-blue");
        }
        else if (themeName === "Dark") {
            removeSbTheme();
            $("body").addClass("sb-theme-dark");
        }

    });
    function removeSbTheme() {
        $("body").removeClass("sb-theme-light").removeClass("sb-theme-blue").removeClass("sb-theme-dark");
    }

    //Refresh button action
    $("#btnRefresh").click(function () {
        onAjaxLoad('All System Configuration', '/Configuration/AllSystemConfiguration');
    });

    //Cancel button action
    $("#btnCancel").click(function () {
        window.history.back();
    });

    
})();
$("#divAllSystemConfiguration").on("click", "#btnSave", function () {
    $.ajax({
        url: '/Configuration/SaveSystemConfiguration',
        type: 'Post',
        data: { Id: 1 },
        success: function (data) {
            if (data === false) {
                notification.show({ message: "System Configuration save was unsuccessful." }, "error");
            }
            else {
                notification.show({ message: "System Configuration has been successfully saved." }, "success");
                connection.invoke("SendNotification", 1).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        },
        error: function () {
            notification.show({ message: "Error occured." }, "error");
        }
    });
});