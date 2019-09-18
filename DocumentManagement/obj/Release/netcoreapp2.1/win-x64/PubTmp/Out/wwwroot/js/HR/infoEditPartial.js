(function () {
    $(document).ready(function () {
        //*******************Order Type Dropdown********************************
        $("#Role").kendoDropDownList({
            filter: "startswith",
            optionLabel: "--Select role--",
            dataTextField: "text",
            dataValueField: "value",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        url: '/HR/GetRole',
                        contentType: 'application/json; charset=utf-8',
                        type: 'GET',
                        dataType: 'json'
                    }
                }
            })
        });

        //*****************Delivery Date picker*********************************
        $("#DOB").kendoDatePicker({
            format: "MMM dd yyyy"
        });
    });
}());