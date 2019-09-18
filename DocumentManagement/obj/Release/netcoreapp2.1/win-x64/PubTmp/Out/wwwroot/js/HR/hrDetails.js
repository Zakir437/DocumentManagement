(function () {
    var id = "";
    $(document).ready(function () {
        id = $("#EncryptedId").val();
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForHR").addClass('active');
        $("#liForHR .arrow").addClass('open');
        $("#liForHrList").addClass("active");
        /**********************User log List****************************/
        var grid = $("#divUserLog").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/HR/GetUserLog',
                            dataType: 'json',
                            type: 'POST',
                            data: $.extend(options.data, { id : id }),
                            success: function (result) {
                                options.success(result);
                            }
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                }
            }),
            pageable: {
                refresh: true,
                pageSizes: [5, 10, 15, 20]
            },
            columns: [{
                field: "tableName",
                title: "Table Name",
                width: 100
            },
            {
                field: "status",
                title: "Status",
                width: 400
            },
            {
                field: "createdDate",
                title: "Created Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
            }
            ]
        });
        $(".k-grid-content table").addClass('table table-hover table-striped');
    });

    /************************Edit name***********************/
    $("#btnEditName").click(function () {
        $("#divEditNameWin").empty();
        $("#divEditNameWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "30%",
            title: 'Edit',
            close: onWindowClose
        });
        var nameEdit = $("#divEditNameWin").data("kendoWindow");
        nameEdit.refresh('/HR/UserEdit?id=' + id);
        nameEdit.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    /************************Edit info***********************/
    $("#btnEditInfo").click(function () {
        $("#divEditInfoWin").empty();
        $("#divEditInfoWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "50%",
            title: 'Edit',
            close: onWindowClose
        });
        var infoEdit = $("#divEditInfoWin").data("kendoWindow");
        infoEdit.refresh('/HR/UserInfoEdit?id=' + id);
        infoEdit.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    /************************Edit password***********************/
    $("#btnChangePassword").click(function () {
        $("#divEditPasswordWin").empty();
        $("#divEditPasswordWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "45%",
            title: 'Edit',
            close: onWindowClose
        });
        var passEdit = $("#divEditPasswordWin").data("kendoWindow");
        passEdit.refresh('/HR/ChangePassword?id=' + id);
        passEdit.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    /*****************************User image change**************************/
    $("#userImage").change(function (e) {
        var files = e.target.files;
        if (files.length > 0) {
            var fname = files.item(0).name;
            var extn = fname.substring(fname.lastIndexOf('.') + 1).toLowerCase();
            if (extn === "gif" || extn === "png" || extn === "jpg" || extn === "jpeg") {
                var formData = new FormData();
                formData.append('file',files[0]);
                formData.append('userId', id);
                $.ajax({
                    url: '/HR/UserImageUpdate',
                    type: 'POST',
                    data: formData,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        if (response === "error") {
                            notification.show({ message: "File upload was unsuccessful." }, "error");
                        }
                        else {
                            notification.show({ message: "File has been successfully uploaded." }, "success");
                        }
                        onAjaxLoad('User Details', '/HR/UserDetails?q=' + id + '&isEdit=true');
                    }
                });
            }
            else {
                alert("Please select an image file.");
            }
        }
    });
}());