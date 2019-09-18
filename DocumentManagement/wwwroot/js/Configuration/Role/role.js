(function () {
    var id = 0, selectedId = "";
    $(document).ready(function () {
        //**************role grid**************************
        var grid = $("#divRoleGrid").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/Configuration/GetRoleList',
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
            columns: [{
                field: "roleName",
                title: "Name",
                width: 200
            }, {
                field: "createdBy",
                title: "Created By",
                width: 140
            }, {
                field: "createdDate",
                title: "Created Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
            },
            {
                width: 110,
                template: '<button type="button" class="btn btn-primary waves-effect btnEdit" data-id="#= id #" data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button>' +
                    '<button type="button" class= "btn btn-danger waves-effect btnDelete" data-id="#= id #" data-toggle="tooltip" title="Delete" > <i class="fa fa-trash"></i></button>'
            }
            ]
        });
        $(".k-grid-content").addClass('h-1');
        $(".k-grid-content table").addClass('table table-hover table-striped');

        //*******************role Multiselect****************
        $("#roleMultiselect").kendoMultiSelect({
            placeholder: "Search by name...",
            dataTextField: "text",
            dataValueField: "value",
            minLength: "2",
            dataSource: new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Configuration/GetRole',
                        type: 'GET',
                        data: function (e) {
                            var widget = $('#roleMultiselect').data('kendoMultiSelect');
                            var text = widget.input.val();
                            if (text === "Search by name...") {
                                text = "";
                            }
                            return {
                                text: text
                            };
                        }
                    }
                }
            }),
            change: function () {
                selectedId = "";
                selectedId = this.value();
                grid.data("kendoGrid").dataSource.query({ skip: 0, take: grid.data("kendoGrid").dataSource.pageSize() });
            },
            filtering: function (e) {
                var widget = $('#roleMultiselect').data('kendoMultiSelect');
                var text = widget.input.val();
                if (text === "Search by name..." || text === null) {
                    e.preventDefault();
                }
            }
        });
        //*******************role delete************************************
        $("#divRoleGrid").on('click', '.btnDelete', function () {
            id = $(this).data("id");
            var kendoWindow = $("<div />").kendoWindow({
                actions: ["Close"],
                title: "Alert",
                resizable: false,
                width: "30%",
                modal: true
            });
            msg = "Are you sure want to delete this role?";
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
                    url: '/Configuration/RoleStatusChange',
                    type: 'Post',
                    data: { id: id },
                    success: function (data) {
                        if (data === "success") {
                            notification.show({ message: "Role has been successfully deleted." }, "success");
                            grid.data("kendoGrid").dataSource.read();
                        }
                        else {
                            notification.show({ message: "Role delete was unsuccessful." }, "error");
                        }
                    }
                });
            }).end();
            document.documentElement.style.overflow = "hidden";
        });
        //********************************role Edit***********************************
        $("#divRoleGrid").on('click', '.btnEdit', function () {
            id = $(this).data("id");
            $("#divRoleEditWin").empty();
            $("#divRoleEditWin").kendoWindow({
                actions: ["Close"],
                draggable: false,
                modal: true,
                visible: false,
                width: "30%",
                height: "35%",
                title: 'Edit',
                resizable: false,
                close: onWindowClose
            });
            var roleEdit = $("#divRoleEditWin").data("kendoWindow");
            roleEdit.refresh('/Configuration/RoleEdit?id=' + id);
            roleEdit.center().open();
            document.documentElement.style.overflow = 'hidden';  // firefox, chrome
            document.body.scroll = "no";
        });
    });

    $("#divRoleGrid").on('click', '#btnCreateRole', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("Role Create", url);
    });
}());