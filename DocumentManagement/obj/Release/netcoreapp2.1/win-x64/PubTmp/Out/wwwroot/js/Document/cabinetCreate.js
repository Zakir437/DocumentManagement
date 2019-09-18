(function () {
    var count = 0, countId = 0, cabinetId = "";
    $(document).ready(function () {
        cabinetId = $("#EncryptedId").val();
        $('[data-toggle="tooltip"]').tooltip();

        $("#liForCabinetTree").addClass('active');
        $("#btnCabinetTreeMenu.arrow").addClass('open');
        $("#liForCabinetTree").find('ul:eq(0)').toggle(true, 'slow');

        c_CountId = $("#CountId").val();
        selectedType = 1;

        $("#liForCabinet_" + c_CountId).addClass('active');
    });

    var validation = $("#divForm").kendoValidator({
    }).data("kendoValidator");

    /****************************add folder*****************************/
    $("#btnCreateF1").click(function () {
        if (validation.validate()) {
            if (count === 0) {
                $("#divFolder").empty();
            }
            count++;
            $("#divFolder").append('<div class="row form-group f1" data-count="'+ count +'" id="divF1_' + count + '">' +
                '<div class="col-xs-8">' +
                '<input type="text" pattern="^(?!\\s*$).?[\\w\\W]{1,}" id="f1Name_' + count + '" data-count=' + count + ' name="f1Name_' + count + '" required data-required-msg="*" validationMessage="*" placeholder="Master Folder Name" class="form-control" />' +
                '<span class="k-widget k-invalid-msg" data-for="f1Name_' + count + '"></span>' +
                '</div>' +
                '<div class="col-xs-4">' +
                (count === 1 ? '' : '<button type="button" class="btn btn-primary waves-effect margin-t-r btnCancelF1" data-count=' + count + ' data-toggle="tooltip" title="Cancel"><i class="fa fa-times"></i></button>') +
                '<button type="button" class="btn btn-primary waves-effect margin-t-r btnAddF1" data-count=' + count + ' data-toggle="tooltip" title="Add folder"><i class="fa fa-plus"></i></button>' +
                '</div>' +
                '</div>');
        }
    });
    $("#divFolder").on('click', '.btnAddF1', function () {
        if (validation.validate()) {
            count++;
            countId = parseInt($(this).data("count"));
            $("#divF1_" + countId).append('<div class="col-xs-12 col-xs-offset-1 m-t-10 f2" data-id="' + countId + '" data-count="' + count + '" id="divF2_' + count + '">' +
                '<div class="row">'+
                '<div class="col-xs-8">' +
                        '<input type="text" pattern="^(?!\\s*$).?[\\w\\W]{1,}" id="f2Name_' + count + '" data-count=' + count + ' name="f2Name_' + count + '" required data-required-msg="*" validationMessage="*" placeholder="Folder name" class="form-control" />' +
                        '<span class="k-widget k-invalid-msg" data-for="f2Name_' + count + '"></span>'+
                    '</div>'+
                    '<div class="col-xs-4">' +
                        '<button type="button" class="btn btn-primary waves-effect margin-t-r btnCancelF2" data-count="' + count + '" data-toggle="tooltip" title="Cancel"><i class="fa fa-times"></i></button>' +
                        '<button class="btn btn-primary waves-effect margin-t-r btnAddF2" data-count="' + count + '" data-toggle="tooltip" title="Add sub folder"><i class="fa fa-plus"></i></button>'+
                    '</div>'+
                '</div>' +
            '</div>');
        }
    });
    $("#divFolder").on('click', '.btnAddF2', function () {
        if (validation.validate()) {
            count++;
            countId = parseInt($(this).data("count"));
            $("#divF2_" + countId).append('<div class="col-xs-12 col-xs-offset-1 m-t-10 f3" data-id="' + countId + '" data-count="' + count + '" id="divF3_' + count + '">' +
                '<div class="row">' +
                '<div class="col-xs-8">' +
                '<input type="text" pattern="^(?!\\s*$).?[\\w\\W]{1,}" id="f3Name_' + count + '" data-count=' + count + ' name="f3Name_' + count + '" required data-required-msg="*" validationMessage="*" placeholder="Sub Folder name" class="form-control" />' +
                '<span class="k-widget k-invalid-msg" data-for="f3Name_' + count + '"></span>' +
                '</div>' +
                '<div class="col-xs-4">' +
                '<button type="button" class="btn btn-primary waves-effect margin-t-r btnCancelF3" data-count="' + count + '" data-toggle="tooltip" title="Cancel"><i class="fa fa-times"></i></button>' +
                '</div>' +
                '</div>' +
                '</div>');
        }
    });
    /****************************Cancel folder*****************************/
    $("#divFolder").on('click', '.btnCancelF1', function () {
        countId = parseInt($(this).data("count"));
        $("#divF1_" + countId).remove();
    });
    $("#divFolder").on('click', '.btnCancelF2', function () {
        countId = parseInt($(this).data("count"));
        $("#divF2_" + countId).remove();
    });
    $("#divFolder").on('click', '.btnCancelF3', function () {
        countId = parseInt($(this).data("count"));
        $("#divF3_" + countId).remove();
    });
    /****************************save cabinet*****************************/
    $("#btnSave").click(function () {
        if (count === 0 && cabinetId !== "") {
            notification.show({ message: "Please add folder." }, "error");
            return false;
        }
        if (validation.validate()) {
            $(this).prop('disabled', true);

            var f1Id = 0;
            var f2Id = 0;

            var f1Folder = [];
            var f2Folder = [];
            var f3Folder = [];
            var f1Name = "";
            var f2Name = "";
            var f3Name = "";
            $(".f1").each(function () {
                f2Folder = [];
                f1Id = parseInt($(this).data("count"));
                f1Name = $.trim($("#f1Name_" + f1Id).val());
                $(".f2").each(function () {
                    if (parseInt($(this).data("id")) === f1Id) {
                        f3Folder = [];
                        f2Id = parseInt($(this).data("count"));
                        f2Name = $.trim($("#f2Name_" + f2Id).val());
                        $(".f3").each(function () {
                            if (parseInt($(this).data("id")) === f2Id) {
                                countId = parseInt($(this).data("count"));
                                f3Name = $.trim($("#f3Name_" + countId).val());
                                f3Folder.push({
                                    Name: f3Name
                                });
                            }
                        });
                        f2Folder.push({
                            Name: f2Name,
                            F3Folder: f3Folder
                        });
                    }
                });
                f1Folder.push({
                    Name: f1Name,
                    F2Folder: f2Folder
                });
            });
            $.ajax({
                url: '/Document/CabinetSave',
                type: 'POST',
                data: { F1Folder: f1Folder, EncryptedId: cabinetId, CabinetName: $.trim($("#cabinetName").val()) },
                success: function (data) {
                    if (data === "success") {
                        if (cabinetId === "") {
                            notification.show({ message: "Cabinet has been successfully Created." }, "success");
                            onAjaxLoad('Cabinet', '/Document/CabinetTile');
                        }
                        else {
                            notification.show({ message: "Cabinet has been successfully updated." }, "success");
                            onAjaxLoad('Cabinet Details', '/Document/CabinetTileDetails?q=' + cabinetId);
                        }
                        loadCabinetTree();
                    }
                    else {
                        if (cabinetId === "") {
                            notification.show({ message: "Cabinet create was unsuccessful." }, "error");
                        }
                        else {
                            notification.show({ message: "Cabinet update was unsuccessful." }, "error");
                        }
                        $("#btnSave").prop('disabled', false);
                    }
                },
                error: function () {
                    if (cabinetId === "") {
                        notification.show({ message: "Cabinet create was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Cabinet update was unsuccessful." }, "error");
                    }
                    $("#btnSave").prop('disabled', false);
                }
            });
        }
    });

    $("#btnRefresh").click(function () {
        if (cabinetId === "") {
            onAjaxLoad('Cabinet Create', '/Document/CabinetCreate');
        }
        else {
            onAjaxLoad('Cabinet Create', '/Document/CabinetCreate?q=' + cabinetId);
        }
    });
    $("#btnCancel").click(function () {
        if (cabinetId === "") {
            onAjaxLoad('Cabinet', '/Document/CabinetTile');
        }
        else {
            onAjaxLoad('Cabinet Details', '/Document/CabinetTileDetails?q=' + cabinetId);
        }
    });
}());