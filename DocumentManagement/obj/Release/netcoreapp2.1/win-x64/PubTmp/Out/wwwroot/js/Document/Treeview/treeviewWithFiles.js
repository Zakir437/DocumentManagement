(function () {
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinet").addClass('active');
        $("#liForCabinet .arrow").addClass('open');
        $("#liForCabinetTreeviewWithFiles").addClass("active");

        //****************Tree view*******************************
        $("#divCabinetTreeView").kendoTreeView({
            dataSource: new kendo.data.HierarchicalDataSource({
                transport: {
                    read: {
                        url: "/Document/CabinetWithFiles"
                    }
                },
                schema: {
                    model: {
                        id: "id",
                        children:"items"
                    }
                }
            }),
            dataTextField: "name"
        });
    });
}());