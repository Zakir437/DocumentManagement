(function () {
    var selectedId = "", id = "", startDate = null, endDate = null;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinet").addClass('active');
        $("#liForCabinet .arrow").addClass('open');
        $("#liForCabinetList").addClass("active");
        //*****************************Document grid**************************************
        var grid = $("#divCabinetGrid").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/Document/GetCabinetList',
                            dataType: 'json',
                            type: 'POST',
                            data: $.extend(options.data, { selectedId: selectedId.toString(), startDate: startDate, endDate: endDate }),
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
            filterMenuInit: dateRange,
            filterable: true,
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
                        $(this).find('td:eq(1)').wrapInner('<span class="status-label status-red shadow-style"></span>');
                    }
                    else {
                        $(this).find('td:eq(1)').wrapInner('<span class="status-label status-green shadow-style"></span>');
                    }
                });
            },
            columns: [{
                field: "name",
                title: "Name",
                filterable: false,
                width: 200
            },
            {
                field: "status",
                title: "Status",
                width: 175,
                filterable: false
            },
            {
                field: "createdBy",
                title: "Created By",
                width: 175,
                filterable: false
            },
            {
                field: "createdDate",
                title: "Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
            },
            {
                title: "Actions",
                width: 300,
                template: kendo.template($("#btnTemplate").html())
            }
            ]
        });
        $(".k-grid-content table").addClass('table table-hover table-striped');

        function dateRange(e) {
            if (e.field === "createdDate") {
                var filterMenu = e.container;
                filterMenu.empty();
                filterMenu.html("<div id='divDateFilter'>" +
                    "<input title = 'start date' placeholder = 'start' name = 'start-date' validationMessage = '*' id = 'start-date' class= 'start-date' />" +
                    "<span class='k-widget k-invalid-msg' data-for='start-date'></span>" +
                    "<input title='end date' placeholder='end' validationMessage='*' name='end-date' id='end-date' class='end-date' />" +
                    "<span class='k-widget k-invalid-msg' data-for='end-date'></span>" +
                    "<button type='button' id='btnDateFilter' class='k-button k-primary'>Filter</button>" +
                    "<button type='reset' class='k-button'>Clear</button>" +
                    "</div>");
                var dateFilterValidation = $("#divDateFilter").kendoValidator({
                    rules: {
                        datepicker: function (input) {
                            if (input.is("[data-role=datepicker]")) {
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

                $("#btnDateFilter").click(function () {
                    if (dateFilterValidation.validate()) {
                        if (selectedId !== "") {
                            selectedId = "";
                            $('#CabinetMultiselect').data('kendoMultiSelect').value(null);
                        }
                        $(this).submit();
                    }
                });

                $(".start-date", filterMenu).kendoDatePicker({
                    format: 'MMM dd,yyyy',
                    change: function () {
                        var endPicker = $("#end-date").data("kendoDatePicker");
                        startDate = this.value();
                        if (startDate) {
                            startDate = new Date(startDate);
                            endPicker.min(startDate);
                            startDate = startDate.toISOString();
                        }
                    }
                });
                $(".end-date", filterMenu).kendoDatePicker({
                    format: 'MMM dd,yyyy',
                    change: function () {
                        var startPicker = $("#start-date").data("kendoDatePicker");
                        endDate = this.value();
                        if (endDate) {
                            endDate = new Date(endDate);
                            startPicker.max(endDate);
                            endDate = endDate.toISOString();
                        }
                    }
                });

                filterMenu.on("click", "[type='reset']", function () {
                    startDate = null;
                    endDate = null;
                    selectedId = "";
                    $('#CabinetMultiselect').data('kendoMultiSelect').value(null);
                });
            }
        }

        //****************Cabinet multiselect******************************
        $("#CabinetMultiselect").kendoMultiSelect({
            placeholder: "Search by name...",
            dataTextField: "text",
            dataValueField: "value",
            minLength: "2",
            dataSource: new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Document/GetCabinet',
                        type: 'GET',
                        data: function (e) {
                            var widget = $('#CabinetMultiselect').data('kendoMultiSelect');
                            var text = widget.input.val();
                            if (text === "Search by name...") {
                                text = "";
                            }
                            return {
                                text: text,
                                startDate: startDate,
                                endDate: endDate
                            };
                        }
                    }
                }
            }),
            change: onChangeMulti,
            filtering: onFiltering
        });

        function onFiltering(e) {
            var widget = $('#CabinetMultiselect').data('kendoMultiSelect');
            var text = widget.input.val();
            if (text === "Search by name..." || text === null) {
                e.preventDefault();
            }
        }

        function onChangeMulti() {
            selectedId = "";
            if (this.value() !== "") {
                selectedId = this.value();
            }
            grid.data("kendoGrid").dataSource.query({ skip: 0, take: grid.data("kendoGrid").dataSource.pageSize() });
        }
    });

    //********************************View Cabinet Details***********************************
    $("#divCabinetGrid").on('click', '.btnViewDetails', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("Document Details", url);
    });

    $("#divCabinetGrid").on('click', '#btnCreateCabinet, .btnAddFolder', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("Cabinet Create", url);
    });


    //***********************Edit********************************
    $("#divCabinetGrid").on('click', '.btnEdit', function () {
        id = $(this).data("id");
        $("#divEditWin").empty();
        $("#divEditWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "45%",
            height: "50%",
            title: 'Edit',
            close: onWindowClose
        });
        var cabinetEditWin = $("#divEditWin").data("kendoWindow");
        cabinetEditWin.refresh('/Document/CabinetEdit?id=' + id);
        cabinetEditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
}());