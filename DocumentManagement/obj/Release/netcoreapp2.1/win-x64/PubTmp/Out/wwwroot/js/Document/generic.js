(function () {
    var selectedId = "", id = "", startDate = null, endDate = null;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinet").addClass('active');
        $("#liForCabinet .arrow").addClass('open');
        $("#liForGenericFiles").addClass("active");
        //*****************************Document grid**************************************
        var grid = $("#divGenericGrid").kendoGrid({
            dataSource: new kendo.data.DataSource({
                pageSize: 20,
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: '/Document/GetGenericFiles',
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
            filterable: false,
            pageable: {
                refresh: true,
                pageSizes: [5, 10, 15, 20]
            },
            columns: [{
                field: "originalName",
                title: "Name",
                filterable: false,
                width: 200
            },
            {
                field: "createdBy",
                title: "Created By",
                width: 175,
                filterable: false
            },
            {
                field: "createdDate",
                title: "Created Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(createdDate), 'MMM dd,yy hh:mm:ss tt') #"
            },
            {
                field: "updatedBy",
                title: "Updated By",
                width: 175,
                filterable: false
            },
            {
                field: "updatedDate",
                title: "Updated Date",
                width: 175,
                template: "#= kendo.toString(kendo.parseDate(updatedDate), 'MMM dd,yy hh:mm:ss tt') #"
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
                            $('#FileMultiselect').data('kendoMultiSelect').value(null);
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
                    $('#FileMultiselect').data('kendoMultiSelect').value(null);
                });
            }
        }

        $("#FileMultiselect").kendoMultiSelect({
            placeholder: "Search by name...",
            dataTextField: "text",
            dataValueField: "value",
            minLength: "2",
            dataSource: new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Document/GetGenericList',
                        type: 'GET',
                        data: function (e) {
                            var widget = $('#FileMultiselect').data('kendoMultiSelect');
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
            var widget = $('#FileMultiselect').data('kendoMultiSelect');
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
}());