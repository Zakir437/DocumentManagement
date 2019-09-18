(function () {
    var selectedId = "", id = "";
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();

        //*****************************User grid**************************************
        var grid = $("#hrGrid").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/HR/GetUser',
                            dataType: 'json',
                            type: 'POST',
                            data: $.extend(options.data, { selectedId: selectedId.toString() }),
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
            toolbar: kendo.template($("#GridHeadertemplate").html()),
            pageable: {
                refresh: true,
                pageSizes: [5, 10, 15, 20]
            },
            dataBound: function () {
                var gridData = this;
                this.tbody.find('tr').each(function () {
                    var dataItem = gridData.dataItem(this);
                    if (dataItem.status === 'Deleted') {
                        $(this).addClass('disabled');
                        $(this).find('td:eq(2)').wrapInner('<span class="status-label status-red shadow-style"></span>');
                    }
                    else {
                        $(this).find('td:eq(2)').wrapInner('<span class="status-label status-green shadow-style"></span>');
                    }
                });
            },
            columns: [{
                title: "Name",
                width: 200,
                template: kendo.template($("#imgTemplate").html())
            },
            {
                field: "userName",
                title: "Username/Email",
                width: 140
            },
            {
                field: "status",
                title: "Status",
                width: 180
            },
            {
                field: "createdDate",
                title: "Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
            },
            {
                title: "Action",
                width: 300,
                template: kendo.template($("#btnTemplate").html())
            }
            ]
        });
        $(".k-grid-content").addClass('h-1');
        $(".k-grid-content table").addClass('table table-hover table-striped');

        //****************User multiselect******************************
        $("#UserMultiSelect").kendoMultiSelect({
            placeholder: "Search by username/email...",
            dataTextField: "text",
            dataValueField: "value",
            minLength: "2",
            dataSource: new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/HR/GetUserForMulti',
                        type: 'GET',
                        data: function () {
                            var widget = $('#UserMultiSelect').data('kendoMultiSelect');
                            var text = widget.input.val();
                            if (text === "Search by username/email...") {
                                text = "";
                            }
                            return {
                                text: text
                            };
                        }
                    }
                }
            }),
            change: onChangeOrderMulti,
            filtering: onFiltering
        });

        function onFiltering(e) {
            var widget = $('#UserMultiSelect').data('kendoMultiSelect');
            var text = widget.input.val();
            if (text === "Search by username/email..." || text === null) {
                e.preventDefault();
            }
        }

        function onChangeOrderMulti() {
            selectedId = "";
            if (this.value() !== "") {
                selectedId = this.value();
            }
            grid.data("kendoGrid").dataSource.query({ skip: 0, take: grid.data("kendoGrid").dataSource.pageSize() });
        }
    });  

    //********************************User edit***********************************
    $("#hrGrid").on('click', '.btnEdit', function () {
        id = $(this).data("id");
        $("#divUserEditWin").empty();
        $("#divUserEditWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "25%",
            height: "20%",
            title: 'Edit',
            close: onWindowClose
        });
        var userEdit = $("#divUserEditWin").data("kendoWindow");
        userEdit.refresh('/HR/UserEdit?id=' + id);
        userEdit.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //*******************User delete************************************
    $("#hrGrid").on('click', '.btnDelete', function () {
        id = $(this).data("id");
        var kendoWindow = $("<div />").kendoWindow({
            actions: ["Close"],
            title: "Alert",
            resizable: false,
            width: "30%",
            modal: true
        });
        msg = "Are you sure want to delete this user?";
        var template = kendo.template($("#temp_win_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/HR/UserStatusChange',
                type: 'Post',
                data: { id: id },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "User has been successfully deleted." }, "success");
                        $("#hrGrid").data("kendoGrid").dataSource.read();
                    }
                    else {
                        notification.show({ message: "User delete was unsuccessful." }, "error");
                    }
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    $("#hrGrid").on('click', '.btnViewDetails', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("User Details", url);
    });

    $("#hrGrid").on('click', '#btnCreateUser', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("Create User", url);
    });
    
    $("#hrGrid").on('click', '#btnRoleList', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("Role List", url);
    });
}());