(function () {
    var startDate = null, endDate = null;
    $(document).ready(function () {
        $("#liForSettings").addClass('active');
        $("#liForSettings .arrow").addClass('open');
        $("#liForFileHistory").addClass('active');
        $("#liForFileHistory .arrow").addClass('open');
        //*****************************Document grid**************************************
        var grid = $("#divFileHistoryGrid").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/Document/GetFileHistory',
                            dataType: 'json',
                            type: 'POST',
                            data: $.extend(options.data, { startDate: startDate, endDate: endDate }),
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
            //toolbar: kendo.template($("#GridHeadertemplate").html()),
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
                    if (dataItem.status === 'Error') {
                        $(this).find('td:eq(2)').wrapInner('<span class="status-label status-red shadow-style"></span>');
                    }
                    else {
                        $(this).find('td:eq(2)').wrapInner('<span class="status-label status-green shadow-style"></span>');
                    }
                });
            },
            columns: [{
                field: "fileName",
                title: "Name",
                filterable: false,
                width: 150
            },
            {
                field: "operation",
                title: "Operation",
                width: 100,
                filterable: false
            },
            {
                field: "status",
                title: "Status",
                width: 100,
                filterable: false
            },
            {
                field: "message",
                title: "Message",
                width: 300,
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
            }
            ]
        });
        $(".k-grid-content table").addClass('table table-hover table-striped');
        $(".k-grid-content").addClass('k-grid-content-h1');
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
                });
            }
        }
    });
}());