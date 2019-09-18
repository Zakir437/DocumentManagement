(function () {
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinet").addClass('active');
        $("#liForCabinet .arrow").addClass('open');
        $("#liForCabinetTreeview").addClass("active");

        //****************Tree view*******************************
        var F1Folder = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (options) {
                    $.ajax({
                        url: "/Document/F1Folders",
                        data: { cabinetId: options.data.id },
                        type: 'GET',
                        success: function (data) {
                            options.success(data);
                        }
                    });
                }
            },
            schema: {
                model: {
                    id: "f1Id",
                    hasChildren: false,
                    //children: mFolder,
                    name: "f1Name"
                }
            }
        });

        var cabinet = new kendo.data.HierarchicalDataSource({
            transport: {
                read: {
                    url: "/Document/Cabinets"
                }
            },
            schema: {
                model: {
                    id: "id",
                    //hasChildren: "hasF1",
                    children: "items"
                }
            }
        });

        $("#divCabinetTreeView").kendoTreeView({
            loadOnDemand: true,
            dataSource: cabinet,
            dataTextField: ["name", "name"],
            expand: function (e) {
                
            }
        });
    });
}());