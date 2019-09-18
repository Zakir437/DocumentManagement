(function () {
    var cabinetId = "", url = "", menuPosition;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinetTree").addClass('active');
        //$("#liForCabinetTree").find('ul.sub-menu li').removeClass("active");
        //$("#liForCabinetTree").find('ul.sub-menu li').removeClass("open");
        //$("#liForCabinetTree").find('ul.sub-menu li .arrow').removeClass("open");
        //$("#liForCabinetTree").find('ul').toggle(false, 'slow');
        $("#liForCabinetTree").find('ul:eq(0)').toggle(true, 'slow');
        $("#btnCabinetTreeMenu.arrow").addClass('open');

        $("#divCabinetFolderList").load('/Document/CabinetList');
    });

    $("#divCabinetFolderList").on('click', '.box', function () {
        cabinetId = $(this).data("id");
        url = '/Document/CabinetTileDetails?q=' + cabinetId;
        onAjaxLoad("Cabinet Details", url);
    });
    $("#divCabinetFolderList").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            cabinetId = $(this).data("id");
            $(".cmenu-options").empty();
            $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-href="/Document/CabinetTileDetails?q=' + cabinetId + '">' +
                '<button class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title="View details"><i class="fa fa-eye"></i></button> <span>View</span>' +
                '</li > ' +
                '<li class="cmenu-option btnAddFolder" data-href="/Document/CabinetCreate?q=' + cabinetId + '">' +
                '<button class="btn btn-primary waves-effect btn-sm" data-toggle="tooltip" title="Add Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                '</li>' +
                '<li class="cmenu-option btnEdit" data-id="' + cabinetId + '">' +
                '<button type="button" class="btn btn-warning waves-effect btn-sm" data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button> <span>Edit</span>' +
                '</li>'+
                '<li class="cmenu-option btnRename" data-id="' + cabinetId + '">' +
                '<button type="button" class="btn btn-success waves-effect btn-sm" data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                '</li>' + 
                '<li class="cmenu-option btnFavouriteFolder" data-id="' + cabinetId + '">' +
                '<button type="button" class="btn bg-teal waves-effect btn-sm"  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                '</li>' +
                '<li class="cmenu-option btnDelete" data-id="' + cabinetId + '">' +
                '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                '</li>' + 
                '<li class="cmenu-option btnProperties" data-id="' + cabinetId + '">' +
                '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                '</li>'
            );
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

    //********************************View Cabinet Details***********************************
    $(".cmenu").on('click', '.btnViewDetails', function (e) {
        url = $(this).data("href");
        onAjaxLoad("Cabinet Details", url);
    });

    $(".cmenu").on('click', '.btnCreateCabinet, .btnAddFolder', function (e) {
        url = $(this).data("href");
        onAjaxLoad("Cabinet Create", url);
    });

    //***********************Edit********************************
    $(".cmenu").on('click', '.btnEdit', function () {
        cabinetId = $(this).data("id");
        $("#divCabinetFolderList").append('<div id="divEditWin"></div>');
        $("#divEditWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "45%",
            height: "50%",
            title: 'Edit',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var cabinetEditWin = $("#divEditWin").data("kendoWindow");
        cabinetEditWin.refresh('/Document/CabinetEdit?id=' + cabinetId);
        cabinetEditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //***********************rename********************************
    $(".cmenu").on('click', '.btnRename', function () {
        cabinetId = $(this).data("id");
        $("#divCabinetFolderList").append('<div id="divRenameWin"></div>');
        $("#divRenameWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "35%",
            height: "30%",
            title: 'Rename',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var cabinetRenameWin = $("#divRenameWin").data("kendoWindow");
        cabinetRenameWin.refresh('/Document/CabinetRename?id=' + cabinetId);
        cabinetRenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //***********************delete********************************
    $(".cmenu").on('click', '.btnDelete', function () {
        cabinetId = $(this).data("id");
        //$("#divDeleteWin").empty();
        //$("#divDeleteWin").kendoWindow({
        //    actions: ["Close"],
        //    draggable: false,
        //    modal: true,
        //    visible: false,
        //    width: "45%",
        //    height: "50%",
        //    title: 'Alert',
        //    close: onWindowClose
        //});
        //var CabinetDeleteWin = $("#divDeleteWin").data("kendoWindow");
        //CabinetDeleteWin.refresh('/Document/CabinetDeleteAlert?id=' + cabinetId);
        //CabinetDeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";

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
        msg = "Are you sure want to delete this cabinet?";
        var template = kendo.template($("#temp_win_Doc_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_doc_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_doc_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/DocumentDelete',
                type: 'Post',
                data: { pId: cabinetId, type: 1, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Cabinet has been successfully deleted." }, "success");
                        $("#divCabinetFolderList").empty();
                        $("#divCabinetFolderList").append('<div class="loading_partial"></div>');
                        $("#divCabinetFolderList").load('/Document/CabinetList');
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    //****************************properties******************************
    $(".cmenu").on('click', '.btnProperties', function () {
        cabinetId = $(this).data("id");
        $("#divCabinetFolderList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + cabinetId + '&type=1');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************folder favourite******************************
    $(".cmenu").on('click', '.btnFavouriteFolder', function () {
        cabinetId = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: cabinetId, folderType: 1 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Cabinet favourite was unsuccessful." }, "error");
                }
                else if (data === "exist") {
                    notification.show({ message: "This cabinet is already added." }, "error");
                }
                else {
                    notification.show({ message: "Cabinet has been successfully add to favourite." }, "success");
                    favouriteList(); //load favourite list
                }
            },
            error: function (error) {
                notification.show({ message: "Folder favourite was unsuccessful." }, "error");
            }
        });
    });

    //*****************menu********************************
    $(".body").mousedown(function (e) {
        if (e.target.className === "min-h-1" && e.which === 3) {
            $(".cmenu-options").empty();
            $(".cmenu-options").append('<li class="cmenu-option btnCreateCabinet" data-href="/Document/CabinetCreate">' +
                '<button class="btn btn-primary waves-effect btn-sm "  data-toggle="tooltip" title="Add Cabinet"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                '</li>' +
                '<li class="cmenu-option btnRefresh">' +
                '<button type="button" class="btn bg-teal waves-effect btn-sm" data-toggle="tooltip" title="Refresh"><i class="fa fa-refresh"></i></button> <span>Refresh</span>' +
                '</li>' 
            );
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

    //****************************refresh******************************
    $(".cmenu").on('click', '.btnRefresh', function () {
        $("#divCabinetFolderList").empty();
        $("#divCabinetFolderList").append('<div class="loading_partial"></div>');
        $("#divCabinetFolderList").load('/Document/CabinetList');
    });
}());