(function () {

    var selectedId = "", id = "", startDate = null, endDate = null, docType = 0, ftype = 0, fileId = 0, menuPosition, resId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#divRecycleList").load('/Document/RecycleList');

        //*****************************Document grid**************************************
        //var grid = $("#divRecycleGrid").kendoGrid({
        //    dataSource: new kendo.data.DataSource({
        //        pageSize: 20,
        //        serverFiltering: true,
        //        serverPaging: true,
        //        transport: {
        //            read: function (options) {
        //                $.ajax({
        //                    url: '/Document/GetRecycleFiles',
        //                    dataType: 'json',
        //                    type: 'POST',
        //                    data: $.extend(options.data, { selectedId: selectedId.toString(), startDate: startDate, endDate: endDate }),
        //                    success: function (result) {
        //                        options.success(result);
        //                    }
        //                });
        //            }
        //        },
        //        schema: {
        //            data: "data",
        //            total: "total"
        //        }
        //    }),
        //    toolbar: kendo.template($("#GridHeadertemplate").html()),
        //    filterMenuInit: dateRange,
        //    filterable: false,
        //    pageable: {
        //        refresh: true,
        //        pageSizes: [5, 10, 15, 20]
        //    },
        //    columns: [{
        //        field: "originalName",
        //        title: "Name",
        //        filterable: false,
        //        width: 200
        //    },
        //    {
        //        field: "createdBy",
        //        title: "Created By",
        //        width: 175,
        //        filterable: false
        //    },
        //    {
        //        field: "createdDate",
        //        title: "Created Date",
        //        width: 175,
        //        template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
        //    },
        //    {
        //        field: "updatedBy",
        //        title: "Updated By",
        //        width: 175,
        //        filterable: false
        //    },
        //    {
        //        field: "updatedDate",
        //        title: "Updated Date",
        //        width: 175,
        //        template: "#= kendo.toString(kendo.parseDate(updatedDate), 'MMM dd,yy hh:mm:ss tt') #"
        //    },
        //    {
        //        title: "Actions",
        //        width: 300,
        //        template: kendo.template($("#btnTemplate").html())
        //    }
        //    ]
        //});
        //$(".k-grid-content table").addClass('table table-hover table-striped');

        //function dateRange(e) {
        //    if (e.field === "createdDate") {
        //        var filterMenu = e.container;
        //        filterMenu.empty();
        //        filterMenu.html("<div id='divDateFilter'>" +
        //            "<input title = 'start date' placeholder = 'start' name = 'start-date' validationMessage = '*' id = 'start-date' class= 'start-date' />" +
        //            "<span class='k-widget k-invalid-msg' data-for='start-date'></span>" +
        //            "<input title='end date' placeholder='end' validationMessage='*' name='end-date' id='end-date' class='end-date' />" +
        //            "<span class='k-widget k-invalid-msg' data-for='end-date'></span>" +
        //            "<button type='button' id='btnDateFilter' class='k-button k-primary'>Filter</button>" +
        //            "<button type='reset' class='k-button'>Clear</button>" +
        //            "</div>");
        //        var dateFilterValidation = $("#divDateFilter").kendoValidator({
        //            rules: {
        //                datepicker: function (input) {
        //                    if (input.is("[data-role=datepicker]")) {
        //                        return input.data("kendoDatePicker").value();
        //                    } else {
        //                        return true;
        //                    }
        //                }
        //            },
        //            messages: {
        //                datepicker: "Please enter valid date!"
        //            }
        //        }).data("kendoValidator");

        //        $("#btnDateFilter").click(function () {
        //            if (dateFilterValidation.validate()) {
        //                if (selectedId !== "") {
        //                    selectedId = "";
        //                    $('#FileMultiselect').data('kendoMultiSelect').value(null);
        //                }
        //                $(this).submit();
        //            }
        //        });

        //        $(".start-date", filterMenu).kendoDatePicker({
        //            format: 'MMM dd,yyyy',
        //            change: function () {
        //                var endPicker = $("#end-date").data("kendoDatePicker");
        //                startDate = this.value();
        //                if (startDate) {
        //                    startDate = new Date(startDate);
        //                    endPicker.min(startDate);
        //                    startDate = startDate.toISOString();
        //                }
        //            }
        //        });
        //        $(".end-date", filterMenu).kendoDatePicker({
        //            format: 'MMM dd,yyyy',
        //            change: function () {
        //                var startPicker = $("#start-date").data("kendoDatePicker");
        //                endDate = this.value();
        //                if (endDate) {
        //                    endDate = new Date(endDate);
        //                    startPicker.max(endDate);
        //                    endDate = endDate.toISOString();
        //                }
        //            }
        //        });

        //        filterMenu.on("click", "[type='reset']", function () {
        //            startDate = null;
        //            endDate = null;
        //            selectedId = "";
        //            $('#FileMultiselect').data('kendoMultiSelect').value(null);
        //        });
        //    }
        //}
        //$("#FileMultiselect").kendoMultiSelect({
        //    placeholder: "Search by name...",
        //    dataTextField: "text",
        //    dataValueField: "value",
        //    minLength: "2",
        //    dataSource: new kendo.data.DataSource({
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: '/Document/GetRecyleList',
        //                type: 'GET',
        //                data: function (e) {
        //                    var widget = $('#FileMultiselect').data('kendoMultiSelect');
        //                    var text = widget.input.val();
        //                    if (text === "Search by name...") {
        //                        text = "";
        //                    }
        //                    return {
        //                        text: text,
        //                        startDate: startDate,
        //                        endDate: endDate
        //                    };
        //                }
        //            }
        //        }
        //    }),
        //    change: onChangeMulti,
        //    filtering: onFiltering
        //});

        //function onFiltering(e) {
        //    var widget = $('#FileMultiselect').data('kendoMultiSelect');
        //    var text = widget.input.val();
        //    if (text === "Search by name..." || text === null) {
        //        e.preventDefault();
        //    }
        //}

        //function onChangeMulti() {
        //    selectedId = "";
        //    if (this.value() !== "") {
        //        selectedId = this.value();
        //    }
        //    grid.data("kendoGrid").dataSource.query({ skip: 0, take: grid.data("kendoGrid").dataSource.pageSize() });
        //}
    });


    //**************folder menu***********************
    $("#divRecycleList").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            docType = parseInt($(this).data("doctype"));
            $(".cmenu-options").empty();
            if (docType === 1) {
                selectedId = $(this).data("id");
                ftype = parseInt($(this).data("ftype"));
                $(".cmenu-options").append('<li class="cmenu-option btnRestoreFolder" data-resid="' + $(this).data("resid") + '" data-doctype="1">' +
                    '<button class="btn btn-warning waves-effect btn-sm "  data-toggle="tooltip" title = "Restore" > <i class="fa fa-window-restore"></i></button> <span>Restore</span>' +
                    '</li> ' +
                    '<li class="cmenu-option btnDelete" data-resid="' + $(this).data("resid") + '" data-doctype="1">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnPropertiesFolder" data-id="' + selectedId + '" data-ftype="' + ftype +'">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else {
                fileId = parseInt($(this).data("id"));
                $(".cmenu-options").append('<li class="cmenu-option btnRestoreFile" data-resid="' + $(this).data("resid") +'" data-doctype="2">' +
                    '<button class="btn btn-warning waves-effect btn-sm" data-toggle="tooltip" title="Restore"><i class="fa fa-window-restore"></i></button> <span>Restore</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnDelete" data-resid="' + $(this).data("resid") + '" data-doctype="2">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnPropertiesFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }

            if ($('#div_lft_bar').is(':hidden') || $('#div_lft_bar').hasClass("in")) {
                menuPosition = {
                    left: e.clientX + 5,
                    top: e.clientY
                };
            }
            else {
                menuPosition = {
                    left: e.clientX + 5,
                    top: e.clientY - 45
                };
            }
            $(".cmenu").css({ top: menuPosition.top + 'px', left: menuPosition.left + 'px' });
            $(".cmenu").show();
        }
    });

    //****************************folder properties******************************
    $(".cmenu").on('click', '.btnPropertiesFolder', function () {
        selectedId = $(this).data("id");
        ftype = parseInt($(this).data("ftype"));
        $("#divRecycleList").append('<div id="divPropertiesWin"></div>');
        $("#divPropertiesWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "40%",
            title: 'Properties',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var propertiesWin = $("#divPropertiesWin").data("kendoWindow");
        propertiesWin.refresh('/Document/Properties?folderId=' + selectedId + '&type=' + ftype + '&isRecycleBin=true');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************file properties******************************
    $(".cmenu").on('click', '.btnPropertiesFile', function () {
        fileId = parseInt($(this).data("id"));
        $("#divRecycleList").append('<div id="divPropertiesWin"></div>');
        $("#divPropertiesWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "40%",
            title: 'Properties',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var propertiesWin = $("#divPropertiesWin").data("kendoWindow");
        propertiesWin.refresh('/Document/Properties?fileId=' + fileId);
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });



    //****************************folder/files restore******************************
    $(".cmenu").on('click', '.btnRestoreFolder, .btnRestoreFile', function () {
        resId = parseInt($(this).data("resid"));
        docType = parseInt($(this).data("doctype"));
        var kendoWindow = $("<div />").kendoWindow({
            actions: ["Close"],
            title: "Alert",
            resizable: false,
            width: "30%",
            modal: true,
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });

        if (docType === 1) {
            msg = "Are you sure want to restore this folder?";
        }
        else {
            msg = "Are you sure want to restore this file?";
        }
        var template = kendo.template($("#temp_win_Doc_restore").html());
        kendoWindow.data("kendoWindow").content(template).center().open();
        kendoWindow.find("#btn_doc_restore_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_doc_restore_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/DocumentRestore',
                type: 'Post',
                data: { resId: resId },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Restore was unsuccessful." }, "error");
                    }
                    else {
                        if (docType === 1) {
                            notification.show({ message: "Folder has been successfully restored." }, "success");
                        }
                        else {
                            notification.show({ message: "File has been successfully restored." }, "success");
                        }
                        $("#divRecycleList").empty();
                        $("#divRecycleList").append('<div class="loading_partial"></div>');
                        $("#divRecycleList").load('/Document/RecycleList');
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Restore was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    //****************************folder/files delete******************************
    $(".cmenu").on('click', '.btnDelete', function () {
        resId = parseInt($(this).data("resid"));
        docType = parseInt($(this).data("doctype"));
        var kendoWindow = $("<div />").kendoWindow({
            actions: ["Close"],
            title: "Alert",
            resizable: false,
            width: "30%",
            modal: true,
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });

        if (docType === 1) {
            msg = "Are you sure want to delete this folder?";
        }
        else {
            msg = "Are you sure want to delete this file?";
        }
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
                url: '/Document/RecycleDelete',
                type: 'Post',
                data: { resId: resId },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        if (docType === 1) {
                            notification.show({ message: "Folder has been successfully deleted." }, "success");
                        }
                        else {
                            notification.show({ message: "File has been successfully deleted." }, "success");
                        }
                        $("#divRecycleList").empty();
                        $("#divRecycleList").append('<div class="loading_partial"></div>');
                        $("#divRecycleList").load('/Document/RecycleList');
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });
}());