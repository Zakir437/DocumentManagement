var c_CountId = 0, f1_CountId = 0, f2_CountId = 0, f3_CountId = 0, selectedType = 0, menuPosition;
(function () {
    var encId = "", type = 0, url = "", cabinetCount = 0, f1Count = 0, f2Count = 0, f3Count = 0;
    $(document).ready(function () {
        loadCabinetTree(); //load cabinet tree view in nav menu
        $("body").click(function (e) {
            if (e.target.className !== "box" || e.target.className !== "nav-cmenu") {
                $(".nav-cmenu").hide();
            }
        }
        );
        $("body").click(function (e) {
            if (e.target.className !== "box" || e.target.className !== "cmenu") {
                $(".cmenu").hide();
            }
        }
        );
    });

    //nav menu properties
    $("#cabinetTree").on('mousedown', 'li', function (e) {
        encId = "";
        if (e.which === 3) {
            encId = $(this).data("id");
            type = parseInt($(this).data("type"));
            $(".nav-cmenu-options").empty();
            if (type === 1) {
                cabinetCount = parseInt($(this).data("ccount"));
                $(".nav-cmenu-options").append('<li class="nav-cmenu-option btn-c-ViewDetails" data-count="' + cabinetCount + '">' +
                    '<button class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title="View details"><i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li > ' +
                    '<li class="nav-cmenu-option btnAddCabinet" data-href="/Document/CabinetCreate?q=' + encId + '" data-count="' + cabinetCount + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm" data-toggle="tooltip" title="Add Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnEditCabinet" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm" data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button> <span>Edit</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnRenameCabinet" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm" data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnDeleteCabinet" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnPropertiesCabinet" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else if (type === 2) {
                cabinetCount = parseInt($(this).data("ccount"));
                f1Count = parseInt($(this).data("f1count"));
                $(".nav-cmenu-options").append('<li class="nav-cmenu-option btn-f1-ViewDetails" data-count="' + f1Count + '">' +
                    '<button class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li > ' +
                    '<li class="nav-cmenu-option btnF1Add" data-href="/Document/F1Add?q=' + encId + '" data-count="' + f1Count + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm "  data-toggle="tooltip" title="Add Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnEdit_F1" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm"  data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button> <span>Edit</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnRenameF1"  data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm" data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnCopyF1" data-id="' + encId + '">' +
                    '<button type="button" class="btn waves-effect bg-purple btn-sm"  data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnMoveF1" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm"  data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnFavouriteF1" data-id="' + encId + '">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm"  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnDeleteF1" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnPropertiesF1" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else if (type === 3) {
                cabinetCount = parseInt($(this).data("ccount"));
                f1Count = parseInt($(this).data("f1count"));
                f2Count = parseInt($(this).data("f2count"));
                $(".nav-cmenu-options").append('<li class="nav-cmenu-option btn-f2-ViewDetails" data-count="' + f2Count + '">' +
                    '<button class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li > ' +
                    '<li class="nav-cmenu-option btnF2Add" data-href="/Document/F2Add?q=' + encId + '" data-count="' + f2Count + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm" data-toggle="tooltip" title="Add Sub Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnEdit_F2" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm  "  data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button> <span>Edit</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnRenameF2" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm "  data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnCopyFolderF2" data-id="' + encId + '">' +
                    '<button type="button" class="btn waves-effect bg-purple btn-sm "  data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnMoveFolderF2" data-id="' + encId + '" >' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm " data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnFavouriteFolderF2" data-id="' + encId + '">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm "  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnDeleteFolderF2" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnPropertiesFolderF2" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else if (type === 4) {
                $(".nav-cmenu-options").append('<li class="nav-cmenu-option btn-f3-ViewDetails" data-count="' + parseInt($(this).data("f3count")) + '">' +
                    '<button class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li > ' +
                    '<li class="nav-cmenu-option btnRenameF3" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm "  data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnCopyFolderF3" data-id="' + encId + '">' +
                    '<button type="button" class="btn waves-effect bg-purple btn-sm "  data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnMoveFolderF3" data-id="' + encId + '" >' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm " data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnFavouriteFolderF3" data-id="' + encId + '">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm "  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnDeleteFolderF3" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="nav-cmenu-option btnPropertiesFolderF3" data-id="' + encId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }

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
            $(".nav-cmenu").css({ top: menuPosition.top + 'px', left: menuPosition.left + 'px' });
            $(".nav-cmenu").show();

            return false;
        }
    });

    //********************************View Details***********************************
    $(".nav-cmenu").on('click', '.btn-c-ViewDetails', function () {
        $("#liForCabinet_" + $(this).data("count")).find('a:first').click();
    });

    $(".nav-cmenu").on('click', '.btn-f1-ViewDetails', function (e) {
        $("#liForF1_" + $(this).data("count")).find('a:first').click();
    });

    $(".nav-cmenu").on('click', '.btn-f2-ViewDetails', function (e) {
        $("#liForF2_" + $(this).data("count")).find('a:first').click();
    });

    $(".nav-cmenu").on('click', '.btn-f3-ViewDetails', function (e) {
        $("#liForF3_" + $(this).data("count")).find('a:first').click();
    });

    //********************************Create***********************************
    $(".nav-cmenu").on('click', '.btnAddCabinet', function (e) {
        cabinetCount = parseInt($(this).data("count"));
        url = $(this).data("href");
        onAjaxLoad("Cabinet Create", url);
    });


    //********************************f1 add***********************************
    $(".nav-cmenu").on('click', '.btnF1Add', function (e) {
        f1Count = parseInt($(this).data("count"));
        url = $(this).data("href");
        onAjaxLoad("F1 Add", url);
    });

    //********************************f2 add***********************************
    $(".nav-cmenu").on('click', '.btnF2Add', function (e) {
        f2Count = parseInt($(this).data("count"));
        url = $(this).data("href");
        onAjaxLoad("F2 Add", url);
    });

    //**************************cabinet edit*************************
    $(".nav-cmenu").on('click', '.btnEditCabinet', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divEditWin"></div>');
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
        cabinetEditWin.refresh('/Document/CabinetEdit?id=' + encId);
        cabinetEditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************cabinet rename********************************
    $(".nav-cmenu").on('click', '.btnRenameCabinet', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divRenameWin"></div>');
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
        cabinetRenameWin.refresh('/Document/CabinetRename?id=' + encId);
        cabinetRenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************cabinet delete********************************
    $(".nav-cmenu").on('click', '.btnDeleteCabinet', function () {
        encId = $(this).data("id");
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
                data: { pId: encId, type: 1, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Cabinet has been successfully deleted." }, "success");
                        if ($("#divCabinetFolderList").length === 1) {
                            $("#divCabinetFolderList").empty();
                            $("#divCabinetFolderList").append('<div class="tiny_loading"></div>');
                            $("#divCabinetFolderList").load('/Document/CabinetList');
                        }
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";

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
        //CabinetDeleteWin.refresh('/Document/CabinetDeleteAlert?id=' + encId);
        //CabinetDeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //**************************** cabinet properties******************************
    $(".nav-cmenu").on('click', '.btnPropertiesCabinet', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divPropertiesWin"></div>');
        $("#divPropertiesWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Properties',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var propertiesWin = $("#divPropertiesWin").data("kendoWindow");
        propertiesWin.refresh('/Document/Properties?folderId=' + encId + '&type=1');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });


    //***********************f1 Edit********************************
    $(".nav-cmenu").on('click', '.btnEdit_F1', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divEditWin"></div>');
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
        var f1EditWin = $("#divEditWin").data("kendoWindow");
        f1EditWin.refresh('/Document/F1Edit?id=' + encId);
        f1EditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //*********************** f1 rename********************************
    $(".nav-cmenu").on('click', '.btnRenameF1', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divRenameWin"></div>');
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
        var f1RenameWin = $("#divRenameWin").data("kendoWindow");
        f1RenameWin.refresh('/Document/F1Rename?id=' + encId);
        f1RenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //*********************** f1 delete********************************
    $(".nav-cmenu").on('click', '.btnDeleteF1', function () {
        encId = $(this).data("id");
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
        msg = "Are you sure want to delete this folder?";
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
                data: { pId: encId, type: 2, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        if ($("#divCabinetDocList").length === 1) {
                            $(".btnUnSelect").click();
                            $("#divCabinetDocList").empty();
                            $("#divCabinetDocList").append('<div class="tiny_loading"></div>');
                            $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
                        }
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";

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
        //var f1DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f1DeleteWin.refresh('/Document/F1DeleteAlert?id=' + encId);
        //f1DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //**************************** f1 folder favourite******************************
    $(".nav-cmenu").on('click', '.btnFavouriteF1', function () {
        encId = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: encId, folderType: 1 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Folder favourite was unsuccessful." }, "error");
                }
                else if (data === "exist") {
                    notification.show({ message: "This Folder is already added." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully add to favourite." }, "success");
                }
            },
            error: function (error) {
                notification.show({ message: "Folder favourite was unsuccessful." }, "error");
            }
        });
    });

    //**************************** f1 folder copy******************************
    $(".nav-cmenu").on('click', '.btnCopyF1', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=true' + '&type=1');
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //**************************** f1 folder move******************************
    $(".nav-cmenu").on('click', '.btnMoveF1', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=false' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //**************************** f1 properties******************************
    $(".nav-cmenu").on('click', '.btnPropertiesF1', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + encId + '&type=2');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });


    //***********************f2 Edit********************************
    $(".nav-cmenu").on('click', '.btnEdit_F2', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divEditWin"></div>');
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
        var f2EditWin = $("#divEditWin").data("kendoWindow");
        f2EditWin.refresh('/Document/F2Edit?id=' + encId);
        f2EditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************f2 rename********************************
    $(".nav-cmenu").on('click', '.btnRenameF2', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divRenameWin"></div>');
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
        var f2RenameWin = $("#divRenameWin").data("kendoWindow");
        f2RenameWin.refresh('/Document/F2Rename?id=' + encId);
        f2RenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************f2 folder copy******************************
    $(".nav-cmenu").on('click', '.btnCopyFolderF2', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=true' + '&type=2');
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //****************************f2 folder move******************************
    $(".nav-cmenu").on('click', '.btnMoveFolderF2', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=false' + '&type=2' + '&encryptedId=' + $("#F1Id").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************f2 delete folder********************************
    $(".nav-cmenu").on('click', '.btnDeleteFolderF2', function () {
        encId = $(this).data("id");
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
        msg = "Are you sure want to delete this folder?";
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
                data: { pId: encId, type: 3, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        if ($("#divF1DocList").length === 1) {
                            $(".btnUnSelect").click();
                            $("#divF1DocList").empty();
                            $("#divF1DocList").append('<div class="tiny_loading"></div>');
                            $("#divF1DocList").load('/Document/F1DocList?f1Id=' + $("#F1Id").val());
                        }
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";

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
        //var f2DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f2DeleteWin.refresh('/Document/F2DeleteAlert?id=' + encId);
        //f2DeleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //****************************f2 folder favourite******************************
    $(".nav-cmenu").on('click', '.btnFavouriteFolderF2', function () {
        encId = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: encId, folderType: 2 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Folder favourite was unsuccessful." }, "error");
                }
                else if (data === "exist") {
                    notification.show({ message: "This Folder is already added." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully add to favourite." }, "success");
                }
            },
            error: function (error) {
                notification.show({ message: "Folder favourite was unsuccessful." }, "error");
            }
        });
    });

    //****************************f2 properties******************************
    $(".nav-cmenu").on('click', '.btnPropertiesFolderF2', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divPropertiesWin"></div>');
        $("#divPropertiesWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Properties',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var propertiesWin = $("#divPropertiesWin").data("kendoWindow");
        propertiesWin.refresh('/Document/Properties?folderId=' + encId + '&type=3');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************f3 rename********************************
    $(".nav-cmenu").on('click', '.btnRenameF3', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divRenameWin"></div>');
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
        var f3RenameWin = $("#divRenameWin").data("kendoWindow");
        f3RenameWin.refresh('/Document/F3Rename?id=' + encId);
        f3RenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //*********************** f3 delete********************************
    $(".nav-cmenu").on('click', '.btnDeleteFolderF3', function () {
        encId = $(this).data("id");
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
        //var f3DeleteWin = $("#divDeleteWin").data("kendoWindow");
        //f3DeleteWin.refresh('/Document/F3DeleteAlert?id=' + encId);
        //f3DeleteWin.center().open();
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
        msg = "Are you sure want to delete this folder?";
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
                data: { pId: encId, type: 4, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        if ($("#divF2DocList").length === 1) {
                            $(".btnUnSelect").click();
                            $("#divF2DocList").empty();
                            $("#divF2DocList").append('<div class="tiny_loading"></div>');
                            $("#divF2DocList").load('/Document/F2DocList?f2Id=' + $("#F2Id").val());
                        }
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

    //****************************f3 folder favourite******************************
    $(".nav-cmenu").on('click', '.btnFavouriteFolderF3', function () {
        encId = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: encId, folderType: 3 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Folder favourite was unsuccessful." }, "error");
                }
                else if (data === "exist") {
                    notification.show({ message: "This Folder is already added." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully add to favourite." }, "success");
                }
            },
            error: function (error) {
                notification.show({ message: "Folder favourite was unsuccessful." }, "error");
            }
        });
    });

    //**************************** f3 folder copy******************************
    $(".nav-cmenu").on('click', '.btnCopyFolderF3', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=true' + '&type=3');
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //**************************** f3 folder move******************************
    $(".nav-cmenu").on('click', '.btnMoveFolderF3', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divTreeview"></div>');
        $("#divTreeview").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "40%",
            title: 'Select',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var treeViewWin = $("#divTreeview").data("kendoWindow");
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + encId + '&isCopy=false' + '&type=3' + '&encryptedId=' + $("#F2Id").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //**************************** f3 properties******************************
    $(".nav-cmenu").on('click', '.btnPropertiesFolderF3', function () {
        encId = $(this).data("id");
        $("#div_lft_bar").append('<div id="divPropertiesWin"></div>');
        $("#divPropertiesWin").kendoWindow({
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
        propertiesWin.refresh('/Document/Properties?folderId=' + encId + '&type=4');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
}());

function loadCabinetTree() {
    //**********************cabinet tree menu********************
    $("#cabinetTree").empty();
    $.ajax({
        url: '/Document/Cabinets',
        type: 'Get',
        success: function (data) {
            var cabinetCount = 0, f1Count = 0, f2Count = 0, f3Count = 0;
            $(data).each(function (index, cabinet) {
                cabinetCount = cabinet.id;
                $("#cabinetTree").append(
                    '<li id="liForCabinet_' + cabinetCount + '" data-ccount="' + cabinetCount + '" data-id="' + cabinet.encryptedId + '" data-type="1">' +
                    '<a href="/Document/CabinetTileDetails?q=' + cabinet.encryptedId + '" data-title="Cabinet Details" class="liActionLink">' +
                    '<i class="fa fa-list-ul"></i>' +
                    '<span class= "title"> ' + cabinet.name + '</span>' +
                    '</a>' +
                    '<a href="javascript:;" class="liNavLink">' +
                    '<span class="arrow" id="c_arrow_' + cabinetCount + '"></span>' +
                    '</a>' +
                    '<ul class="sub-menu" id="f1Tree_' + cabinetCount + '">' +
                    '</ul>' +
                    '</li>'
                );
                $(cabinet.items).each(function (index, f1) {
                    f1Count = f1.id;
                    if (!$.trim(f1.items)) {
                        $("#f1Tree_" + cabinetCount).append(
                            '<li class="liForF1" data-id="' + f1.encryptedId + '" data-type="2" data-ccount="' + cabinetCount + '" data-f1count="' + f1Count + '" id="liForF1_' + f1Count + '"><a href="/Document/F1TileDetails?q=' + f1.encryptedId + '" data-title="F1 Details" data-type="2"><i class="k-icon k-i-folder"></i> ' + f1.name + '</a></li>'
                        );
                    }
                    else {
                        $("#f1Tree_" + cabinetCount).append(
                            '<li id="liForF1_' + f1Count + '" data-type="2" data-ccount="' + cabinetCount + '" data-f1count="' + f1Count + '" data-id="' + f1.encryptedId + '">' +
                            '<a href="/Document/F1TileDetails?q=' + f1.encryptedId + '" data-title="F1 Details" data-type="2" class="liActionLink">' +
                            '<i class="k-icon k-i-folder"></i>' +
                            '<span class= "title"> ' + f1.name + '</span > ' +
                            '</a>' +
                            '<a href="javascript:;" class="liNavLink">' +
                            '<span class="arrow" id="f1_arrow_' + f1Count + '"></span>' +
                            '</a>' +
                            '<ul class="sub-menu" id="f2Tree_' + f1Count + '">' +
                            '</ul>' +
                            '</li>'
                        );
                        $(f1.items).each(function (index, f2) {
                            f2Count = f2.id;
                            if (!$.trim(f2.items)) {
                                $("#f2Tree_" + f1Count).append(
                                    '<li class="liForF2" id="liForF2_' + f2Count + '" data-ccount="' + cabinetCount + '" data-f1count="' + f1Count + '" data-f2count="' + f2Count + '" data-type="3" data-id="' + f2.encryptedId + '"><a href="/Document/F2TileDetails?q=' + f2.encryptedId + '"  data-title="F2 Details" data-type="3"><i class="k-icon k-i-folder"></i> ' + f2.name + '</a></li>'
                                );
                            }
                            else {
                                $("#f2Tree_" + f1Count).append(
                                    '<li id="liForF2_' + f2Count + '" data-ccount="' + cabinetCount + '" data-f1count="' + f1Count + '" data-f2count="' + f2Count + '" data-type="3" data-id="' + f2.encryptedId + '">' +
                                    '<a href="/Document/F2TileDetails?q=' + f2.encryptedId + '" data-title="F2 Details" class="liActionLink" data-type="3">' +
                                    '<i class="k-icon k-i-folder"></i>' +
                                    '<span class= "title"> ' + f2.name + '</span > ' +
                                    '</a>' +
                                    '<a href="javascript:;" class="liNavLink">' +
                                    '<span class="arrow" id="f2_arrow_' + f2Count + '"></span>' +
                                    '</a>' +
                                    '<ul class="sub-menu" id="f3Tree_' + f2Count + '">' +
                                    '</ul>' +
                                    '</li>'
                                );
                                $(f2.items).each(function (index, f3) {
                                    f3Count = f3.id;
                                    $("#f3Tree_" + f2Count).append(
                                        '<li id="liForF3_' + f3Count + '" data-ccount="' + cabinetCount + '" data-f1count="' + f1Count + '" data-f2count="' + f2Count + '" data-f3count="' + f3Count + '" data-type="4" data-id="' + f3.encryptedId + '"><a href="/Document/F3TileDetails?q=' + f3.encryptedId + '" data-title="F3 Details" data-type="4"><i class="k-icon k-i-folder"></i> ' + f3.name + '</a></li>'
                                    );
                                });
                            }
                        });
                    }
                });
            });

            if (selectedType > 0) {
                if (selectedType === 1) {
                    $("#liForCabinet_" + c_CountId).addClass('active');
                    $("#liForCabinet_" + c_CountId).find('ul').toggle(false, 'slow');
                }
                else if (selectedType === 2) {
                    $("#liForCabinet_" + c_CountId).addClass('active');
                    $("#c_arrow_" + c_CountId).addClass('open');
                    $("#liForF1_" + f1_CountId).addClass('active');
                    $("#liForF1_" + f1_CountId).find('ul').toggle(false, 'slow');
                }
                else if (selectedType === 3) {
                    $("#liForCabinet_" + c_CountId).addClass('active');
                    $("#c_arrow_" + c_CountId).addClass('open');
                    $("#liForF1_" + f1_CountId).addClass('active');
                    $("#f1_arrow_" + f1_CountId).addClass('open');
                    $("#liForF2_" + f2_CountId).addClass('active');
                    $("#liForF2_" + f2_CountId).find('ul').toggle(false, 'slow');
                }
                else if (selectedType === 4) {
                    $("#liForCabinet_" + c_CountId).addClass('active');
                    $("#c_arrow_" + c_CountId).addClass('open');
                    $("#liForF1_" + f1_CountId).addClass('active');
                    $("#f1_arrow_" + f1_CountId).addClass('open');
                    $("#liForF2_" + f2_CountId).addClass('active');
                    $("#f2_arrow_" + f2_CountId).addClass('open');
                    $("#liForF3_" + f3_CountId).addClass('active');
                    $("#liForF3_" + f3_CountId).find('ul').toggle(false, 'slow');
                }
            }
        }
    });
}
