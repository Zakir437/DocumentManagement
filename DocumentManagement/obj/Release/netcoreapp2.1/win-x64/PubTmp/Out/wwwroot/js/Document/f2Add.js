(function () {
    var count = 0, countId = 0, f2Id = "";
    $(document).ready(function () {
        f2Id = $("#F2Id").val();
        $('[data-toggle="tooltip"]').tooltip();

        $("#liForCabinetTree").addClass('active');
        $("#btnCabinetTreeMenu.arrow").addClass('open');
        $("#liForCabinetTree").find('ul:eq(0)').toggle(true, 'slow');

        c_CountId = $("#C_countId").val();
        f1_CountId = $("#F1_CountId").val();
        f2_CountId = $("#F2_CountId").val();
        selectedType = 3;

        $("#liForCabinet_" + c_CountId).addClass('active');
        $("#c_arrow_" + c_CountId).addClass('open');
        $("#liForCabinet_" + c_CountId).find('ul:eq(0)').toggle(true, 'slow');

        $("#liForF1_" + f1_CountId).addClass('active');
        $("#f1_arrow_" + f1_CountId).addClass('open');
        $("#liForF1_" + f1_CountId).find('ul:eq(0)').toggle(true, 'slow');

        $("#liForF2_" + f2_CountId).addClass('active');
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
            $("#divFolder").append('<div class="row form-group f3" data-count="' + count + '" id="divF3_' + count + '">' +
                '<div class="col-xs-8">' +
                '<input type="text" pattern="^(?!\\s*$).?[\\w\\W]{1,}" id="f3Name_' + count + '" data-count=' + count + ' name="f3Name_' + count + '" required data-required-msg="*" validationMessage="*" placeholder="Sub Folder Name" class="form-control" />' +
                '<span class="k-widget k-invalid-msg" data-for="f3Name_' + count + '"></span>' +
                '</div>' +
                '<div class="col-xs-4">' +
                (count === 1 ? '' : '<button type="button" class="btn btn-primary waves-effect margin-t-r btnCancelF3" data-count="' + count + '" data-toggle="tooltip" title="Cancel"><i class="fa fa-times"></i></button>') +
                '</div>' +
                '</div>');
        }
    });
    /****************************Cancel folder*****************************/
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
            var f3Folder = [];
            var f3Name = "";
            $(".f3").each(function () {
                countId = parseInt($(this).data("count"));
                f3Name = $.trim($("#f3Name_" + countId).val());
                f3Folder.push({
                    Name: f3Name
                });
            });

            $.ajax({
                url: '/Document/SubFolderSave',
                type: 'POST',
                data: { F2Id: f2Id, F3Folder: f3Folder },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Sub Folder has been successfully Created." }, "success");
                        onAjaxLoad('F2 Details', '/Document/F2TileDetails?q=' + f2Id);
                        loadCabinetTree();
                    }
                    else {
                        notification.show({ message: "Sub Folder create was unsuccessful." }, "error");
                        $("#btnSave").prop('disabled', false);
                    }
                },
                error: function () {
                    notification.show({ message: "Sub Folder create was unsuccessful." }, "error");
                    $("#btnSave").prop('disabled', false);
                }
            });
        }
    });

    $("#btnRefresh").click(function () {
        onAjaxLoad('F2 Add', '/Document/F2Add?q=' + f2Id );
    });
    $("#btnCancel").click(function () {
        onAjaxLoad('F2 Details', '/Document/F2TileDetails?q=' + f2Id);
    });
}());