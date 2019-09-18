(function () {
    var count = 0, countId = 0, f1Id = "";
    $(document).ready(function () {
        f1Id = $("#F1Id").val();
        $('[data-toggle="tooltip"]').tooltip();

        $("#liForCabinetTree").addClass('active');
        $("#btnCabinetTreeMenu.arrow").addClass('open');
        $("#liForCabinetTree").find('ul:eq(0)').toggle(true, 'slow');

        c_CountId = $("#C_countId").val();
        f1_CountId = $("#F1_CountId").val();
        selectedType = 2;

        $("#liForCabinet_" + c_CountId).addClass('active');
        $("#c_arrow_" + c_CountId).addClass('open');
        $("#liForCabinet_" + c_CountId).find('ul:eq(0)').toggle(true, 'slow');

        $("#liForF1_" + f1_CountId).addClass('active');
    });

    var validation = $("#divForm").kendoValidator({
    }).data("kendoValidator");

    /****************************add folder*****************************/
    $("#btnAdd").click(function () {
        if (validation.validate()) {
            if (count === 0) {
                $("#divFolder").empty();
            }
            count++;
            $("#divFolder").append('<div class="row form-group f2" data-count="' + count + '" id="divF2_' + count + '">' +
                '<div class="col-xs-8">' +
                '<input type="text" pattern="^(?!\\s*$).?[\\w\\W]{1,}" id="f2Name_' + count + '" data-count=' + count + ' name="f2Name_' + count + '" required data-required-msg="*" validationMessage="*" placeholder="Folder Name" class="form-control" />' +
                '<span class="k-widget k-invalid-msg" data-for="f2Name_' + count + '"></span>' +
                '</div>' +
                '<div class="col-xs-4">' +
                (count === 1 ? '' : '<button type="button" class="btn btn-primary waves-effect margin-t-r btnCancelF2" data-count=' + count + ' data-toggle="tooltip" title="Cancel"><i class="fa fa-times"></i></button>') +
                '<button type="button" class="btn btn-primary waves-effect margin-t-r btnAddF2" data-count=' + count + ' data-toggle="tooltip" title="Add folder"><i class="fa fa-plus"></i></button>' +
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
        if (count === 0) {
            notification.show({ message: "Please add folder." }, "error");
            return false;
        }
        if (validation.validate()) {
            $(this).prop('disabled', true);
            var f2Id = 0;
            var f2Folder = [];
            var f3Folder = [];
            var f2Name = "";
            var f3Name = "";
            $(".f2").each(function () {
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
            });
            
            $.ajax({
                url: '/Document/FolderSave',
                type: 'POST',
                data: { F1Id: f1Id, F2Folder: f2Folder  },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Folder has been successfully Created." }, "success");
                        onAjaxLoad('F1 Details', '/Document/F1TileDetails?q=' + f1Id);
                        loadCabinetTree();
                    }
                    else {
                        notification.show({ message: "Folder create was unsuccessful." }, "error");
                        $("#btnSave").prop('disabled', false);
                    }
                },
                error: function () {
                    notification.show({ message: "Folder create was unsuccessful." }, "error");
                    $("#btnSave").prop('disabled', false);
                }
            });
        }
    });

    $("#btnRefresh").click(function () {
        onAjaxLoad('F1 Add', '/Document/F1Add?q=' + f1Id);
    });
    $("#btnCancel").click(function () {
        onAjaxLoad('F1 Details', '/Document/F1TileDetails?q=' + f1Id);
    });
}());