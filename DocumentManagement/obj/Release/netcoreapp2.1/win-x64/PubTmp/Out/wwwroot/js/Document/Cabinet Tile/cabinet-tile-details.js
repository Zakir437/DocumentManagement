﻿(function () {
    var f1Id = "", fileId = 0, docType = 0, documents = [], url = "", menuPosition, count = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();

        $("#liForCabinetTree").addClass('active');
        $("#liForCabinetTree").addClass('active');
        //$("#liForCabinetTree").find('ul.sub-menu li').removeClass("active");
        //$("#liForCabinetTree").find('ul.sub-menu li').removeClass("open");
        //$("#liForCabinetTree").find('ul.sub-menu li .arrow').removeClass("open");
        //$("#liForCabinetTree").find('ul').toggle(false, 'slow');
        $("#liForCabinetTree").find('ul:eq(0)').toggle(true, 'slow');
        $("#btnCabinetTreeMenu.arrow").addClass('open');

        c_CountId = parseInt($("#CountId").val());
        selectedType = 1;

        $("#liForCabinet_" + c_CountId).addClass('active');
        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
    });

    $("#divCabinetDocList").on('click', '.box', function () {
        if ($(".docCheck").length === 0) {
            docType = parseInt($(this).data("type"));
            if (docType === 1) {
                $("#liForF1_" + parseInt($(this).data("count")) + " a:first").click();
            }
        }
    });

    $("#divCabinetDocList").on('dblclick', '.box', function () {
        if ($(".docCheck").length === 0) {
            docType = parseInt($(this).data("type"));
            if (docType === 2) {
                url = "/Document/FileView?q=" + $(this).data("encid");
                window.open(url);
            }
        }
    });

    //**************folder menu***********************
    $("#divCabinetDocList").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            docType = parseInt($(this).data("type"));
            $(".cmenu-options").empty();
            if (docType === 1) {
                count = parseInt($(this).data("count"));
                f1Id = $(this).data("id");
                $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-count="' + count + '">' +
                    '<button class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li > ' +
                    '<li class="cmenu-option btnAddFolder" data-count="' + count + '" data-id="' + f1Id + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm "  data-toggle="tooltip" title="Add Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnEditF1" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm"  data-toggle="tooltip" title="Edit"><i class="fa fa-edit"></i></button> <span>Edit</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnRename"  data-id="' + f1Id + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm" data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnCopyFolder" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn waves-effect bg-purple btn-sm"  data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnMoveFolder" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm"  data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnFavouriteFolder" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm"  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnDeleteFolder" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnPropertiesFolder" data-id="' + f1Id + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else {
                fileId = parseInt($(this).data("id"));
                $(".cmenu-options").append('<li class="cmenu-option btnFileView" data-href="/Document/FileView?q=' + $(this).data("encid") + '">' +
                    '<button class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title="View"><i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnDownload" data-href="/Document/DownloadFile?fileId=' + fileId + '">' +
                    '<button  class="btn btn-primary waves-effect btn-sm" data-toggle="tooltip" title="Download"><i class="fa fa-download"></i></button> <span>Download</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnCopyFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn waves-effect bg-purple btn-sm "  data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnMoveFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm "  data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnRenameFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn btn-success waves-effect btn-sm "  data-toggle="tooltip" title="Rename"><i class="fa fa-edit"></i></button> <span>Rename</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnFavouriteFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm "  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnDeleteFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm "  data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnHistoryFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn bg-orange waves-effect btn-sm"  data-toggle="tooltip" title="History"><i class="fa fa-history"></i></button> <span>History</span>' +
                    '</li>'+
                    '<li class="cmenu-option btnPropertiesFile" data-id="' + fileId + '">' +
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
            $(".cmenu").css({ top: menuPosition.top + 'px', left: menuPosition.left + 'px' });
            $(".cmenu").show();
        }
    });

    //********************************View f1 Details***********************************
    $(".cmenu").on('click', '.btnViewDetails', function (e) {
        $("#f1Tree_" + c_CountId + " > li:eq(" + parseInt($(this).data("count")) + ") a:first").click();
    });

    $(".cmenu").on('click', '.btnAddFolder', function (e) {
        f1Id = $(this).data("id");
        url = "/Document/F1Add?q=" + f1Id;
        onAjaxLoad("F1 Add", url);
    });

    //***********************Edit********************************
    $(".cmenu").on('click', '.btnEditF1', function () {
        f1Id = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divEditWin"></div>');
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
        f1EditWin.refresh('/Document/F1Edit?id=' + f1Id);
        f1EditWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************rename********************************
    $(".cmenu").on('click', '.btnRename', function () {
        f1Id = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divRenameWin"></div>');
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
        f1RenameWin.refresh('/Document/F1Rename?id=' + f1Id);
        f1RenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************delete********************************
    $(".cmenu").on('click', '.btnDeleteFolder', function () {
        f1Id = $(this).data("id");
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
        //f1DeleteWin.refresh('/Document/F1DeleteAlert?id=' + f1Id);
        //f1DeleteWin.center().open();
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
                data: { pId: f1Id, type: 2, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        $(".btnUnSelect").click();
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="loading_partial"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
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

    //****************************folder favourite******************************
    $(".cmenu").on('click', '.btnFavouriteFolder', function () {
        f1Id = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: f1Id, folderType : 2 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Folder favourite was unsuccessful." }, "error");
                }
                else if (data === "exist") {
                    notification.show({ message: "This Folder is already added." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully add to favourite." }, "success");
                    favouriteList(); //load favourite list
                }
            },
            error: function (error) {
                notification.show({ message: "Folder favourite was unsuccessful." }, "error");
            }
        });
    });

    //****************************folder copy******************************
    $(".cmenu").on('click', '.btnCopyFolder', function () {
        f1Id = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + f1Id + '&isCopy=true' + '&type=1');
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //****************************folder move******************************
    $(".cmenu").on('click', '.btnMoveFolder', function () {
        f1Id = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?folderId=' + f1Id + '&isCopy=false' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************properties******************************
    $(".cmenu").on('click', '.btnPropertiesFolder', function () {
        f1Id = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + f1Id + '&type=2');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //******************file view************************
    $(".cmenu").on('click', '.btnFileView', function (e) {
        url = $(this).data("href");
        window.open(url);
    });
    //******************file download************************
    $(".cmenu").on('click', '.btnDownload', function (e) {
        url = $(this).data("href");
        window.location.href = url;
    });


    //****************************files rename************
    $(".cmenu").on('click', '.btnRenameFile', function () {
        fileId = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divRenameWin"></div>');
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
        var fileRenameWin = $("#divRenameWin").data("kendoWindow");
        fileRenameWin.refresh('/Document/FileRename?id=' + fileId);
        fileRenameWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });


    //***********************delete********************************
    $(".cmenu").on('click', '.btnDeleteFile', function () {
        fileId = $(this).data("id");
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
        msg = "Are you sure want to delete this file?";
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
                data: { fileId: fileId, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully deleted." }, "success");
                        $(".btnUnSelect").click();
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="loading_partial"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Delete was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";



        //var kendoWindow = $("<div />").kendoWindow({
        //    actions: ["Close"],
        //    title: "Alert",
        //    resizable: false,
        //    width: "30%",
        //    modal: true
        //});
        //msg = "Are you sure want to delete this file?";
        //var template = kendo.template($("#temp_win_file_delete_entry").html());
        //kendoWindow.data("kendoWindow").content(template).center().open();

        //kendoWindow.find("#btn_file_delete_Entry_noty_cancel").click(function () {
        //    kendoWindow.data("kendoWindow").close();
        //    document.documentElement.style.overflow = "auto";
        //}).end();
        //kendoWindow.find("#btn_file_delete_Entry_noty_ok").click(function () {
        //    kendoWindow.data("kendoWindow").close();
        //    document.documentElement.style.overflow = "auto";
        //    $.ajax({
        //        url: '/Document/FileStatusChange',
        //        type: 'Post',
        //        data: { fileId: fileId, status: 2, isKeepDocument: kendoWindow.find("#keepCheckBox").is(":checked") },
        //        success: function (data) {
        //            if (data === "error") {
        //                notification.show({ message: "File delete was unsuccessful." }, "error");
        //            }
        //            else {
        //                notification.show({ message: "File has been successfully deleted." }, "success");
        //                $("#divCabinetDocList").empty();
        //                $("#divCabinetDocList").append('<div class="loading_partial"></div>');
        //                $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
        //            }
        //        },
        //        error: function () {
        //            notification.show({ message: "File delete was unsuccessful." }, "error");
        //        }
        //    });
        //}).end();
        //document.documentElement.style.overflow = "hidden";
    });

    //****************************file copy******************************
    $(".cmenu").on('click', '.btnCopyFile', function () {
        fileId = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?fileId=' + fileId + '&isCopy=true' + '&type=1' );
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************file move******************************
    $(".cmenu").on('click', '.btnMoveFile', function () {
        fileId = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?fileId=' + fileId + '&isCopy=false' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************file favourite******************************
    $(".cmenu").on('click', '.btnFavouriteFile', function () {
        fileId = $(this).data("id");
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { fileId: fileId },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "File favourite was unsuccessful." }, "error");
                }
                else if (data === "exist")
                {
                    notification.show({ message: "This file is already added." }, "error");
                }
                else {
                    notification.show({ message: "File has been successfully add to favourite." }, "success");
                }
            },
            error: function (error) {
                notification.show({ message: "File favourite was unsuccessful." }, "error");
            }
        });
    });

    //****************************file properties******************************
    $(".cmenu").on('click', '.btnPropertiesFile', function () {
        fileId = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?fileId=' + fileId);
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************file history******************************
    $(".cmenu").on('click', '.btnHistoryFile', function () {
        fileId = $(this).data("id");
        $("#divCabinetDocList").append('<div id="divHistoryWin"></div>');
        $("#divHistoryWin").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "58%",
            height: "50%",
            title: 'History',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var fileHistoryWin = $("#divHistoryWin").data("kendoWindow");
        fileHistoryWin.refresh('/Document/FileHistoryPartial?id=' + fileId);
        fileHistoryWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //*****************menu********************************
    $(".body").mousedown(function (e) {
        if (e.target.className === "min-h-1" && e.which === 3) {
            var isSelect = false;
            $(".cmenu-options").empty();
            if ($(".docCheck").length > 0) {
                $('.docCheck').each(function () {
                    if ($(this).is(":checked") === true) {
                        isSelect = true;
                    }
                });
                if (isSelect === true) {
                    $(".cmenu-options").append('<li class="cmenu-option btnCopy">' +
                        '<button type="button" class="btn btn-primary waves-effect btn-sm " data-toggle="tooltip" title="Copy"><i class="fa fa-copy"></i></button> <span>Copy</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnMove">' +
                        '<button type="button" class="btn btn-warning waves-effect btn-sm " data-toggle="tooltip" title="Move"><i class="fa fa-arrow-right"></i></button> <span>Move</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnDelete">' +
                        '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
                        '</li>'
                    );
                }
            }
            if (isSelect === false)
            {
                $(".cmenu-options").append('<li class="cmenu-option btnAddFolderF1" data-href="/Document/CabinetCreate?q=' + $("#EncryptedId").val() + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm "  data-toggle="tooltip" title="Add Folder"><i class="fa fa-plus"></i></button> <span>Create</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnFileUpload">' +
                    '<button class="btn btn-success waves-effect btn-sm" data-toggle="tooltip" title="Upload"><i class="fa fa-upload"></i></button> <span>Upload</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnMoveAll">' +
                    '<button type="button" class="btn btn-warning waves-effect btn-sm" data-toggle="tooltip" title="Move All"><i class="fa fa-arrow-right"></i></button> <span>Move All</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnRefresh">' +
                    '<button type="button" class="btn bg-teal waves-effect btn-sm" data-toggle="tooltip" title="Refresh"><i class="fa fa-refresh"></i></button> <span>Refresh</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnProperties">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm"  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
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
            $(".cmenu").css({ top: menuPosition.top + 'px', left: menuPosition.left + 'px' });
            $(".cmenu").show();
        }
    });

    //************f1 folder add******************
    $(".cmenu").on('click', '.btnAddFolderF1', function (e) {
        url = $(this).data("href");
        onAjaxLoad("Cabinet Create", url);
    });


    //*********************file upload********************
    $(".cmenu").on('click', '.btnFileUpload', function () {
        $("#files").click();
    });

    //*****************Select******************************
    $(".btnSelect").click(function () {
        if ($(".box .docCheck").length === 0) {
            $(".divActionBtn").each(function () {
                $(this).prepend('<input  type="checkbox" class="docCheck"/>');
            });
        }
        $(this).hide();
        $(".btnUnSelect").show();
    });

    //*****************Un-Select******************************
    $(".btnUnSelect").click(function () {
        if ($(".docCheck").length > 0) {
            $(".docCheck").each(function () {
                $(this).remove();
            });
        }
        $(this).hide();
        $(".btnSelect").show();
    });
    //**********************File upload****************************************
    $("#files").change(function () {
        var files = [], inputFiles;
        inputFiles = document.getElementById('files');
        if (inputFiles.files.length > 0) {
            for (i = 0; i <= inputFiles.files.length - 1; i++) {
                var fname = inputFiles.files.item(i).name;
                var extn = fname.substring(fname.lastIndexOf('.') + 1).toLowerCase();
                if (extn === "pdf" || extn === "doc" || extn === "docx" || extn === "txt" || extn === "gif" || extn === "png" || extn === "jpg" || extn === "jpeg" || extn === "ppt" || extn === "pptm" || extn === "pptx" || extn === "xlsx" || extn === "xlsm" || extn === "xltx" || extn === "xltm") {
                    files.push(inputFiles.files.item(i));
                }
                else {
                    alert("This (" + fname + ") file not supported.");
                }
            }
        }
        else {
            alert('Please select a file.');
            files = [];
        }

        if (files.length > 0) {
            var sendData = new FormData();
            sendData.append("pId", $("#EncryptedId").val());
            sendData.append("type", 1);
            for (var i = 0; i < files.length; i++) {
                sendData.append("files[" + i + "]", files[i]);
            }
            $.ajax({
                url: '/Document/FileSave',
                type: 'Post',
                data: sendData,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "File upload was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "File has been successfully uploaded." }, "success");
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="loading_partial"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());

                        //load storage info
                        storageInfo();
                    }
                },
                error: function (error) {
                    notification.show({ message: "Error occured." }, "error");
                }
            });
        }
    });

    //****************************copy******************************
    $(".cmenu").on('click', '.btnCopy', function () {
        documents = [];
        if ($(".docCheck").length > 0) {
            $(".docCheck").each(function () {
                if ($(this).is(':checked')) {
                    documents.push($(this).parent().parent().data("id"));
                }
            });
        }
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?documents=' + documents.toString() + '&isCopy=true' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //**************************** move******************************
    $(".cmenu").on('click', '.btnMove', function () {
        documents = [];
        if ($(".docCheck").length > 0) {
            $(".docCheck").each(function () {
                if ($(this).is(':checked')) {
                    documents.push($(this).parent().parent().data("id"));
                }
            });
        }
        $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
        treeViewWin.refresh('/Document/CabinetTreeView?documents=' + documents.toString() + '&isCopy=false' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
        treeViewWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //***********************delete********************************
    $(".cmenu").on('click', '.btnDelete', function () {
        documents = [];
        if ($(".docCheck").length > 0) {
            $(".docCheck").each(function () {
                if ($(this).is(':checked')) {
                    documents.push($(this).parent().parent().data("id"));
                }
            });
        }
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
        msg = "Are you sure want to delete this files?";
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
                data: { pIds: documents.toString(), type : 2, isPermanent: kendoWindow.find("#IsPermanent").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Delete was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Files has been successfully deleted." }, "success");
                        $(".btnUnSelect").click();
                        $("#divCabinetDocList").empty();
                        $("#divCabinetDocList").append('<div class="loading_partial"></div>');
                        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
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
        //var deleteWin = $("#divDeleteWin").data("kendoWindow");
        //deleteWin.refresh('/Document/F1DeleteAlert?id=' + documents.toString() + '&isMultiple=true' + '&encryptId=' + $("#EncryptedId").val());
        //deleteWin.center().open();
        //document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        //document.body.scroll = "no";
    });

    //******************move all*****************************
    $(".cmenu").on('click', '.btnMoveAll', function () {
        documents = [];
        $(".box").each(function () {
            documents.push($(this).data("id"));
        });
        if (documents.length === 0) {
            notification.show({ message: "There is no files to move." }, "error");
        }
        else {
            $("#divCabinetDocList").append('<div id="divTreeview"></div>');
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
            treeViewWin.refresh('/Document/CabinetTreeView?documents=' + documents.toString() + '&isCopy=false' + '&type=1' + '&encryptedId=' + $("#EncryptedId").val());
            treeViewWin.center().open();
            document.documentElement.style.overflow = 'hidden';  // firefox, chrome
            document.body.scroll = "no";
        }
    });

    //****************************properties******************************
    $(".cmenu").on('click', '.btnProperties', function () {
        $("#divCabinetDocList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + $("#EncryptedId").val() + '&type=1');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
    //****************************refresh******************************
    $(".cmenu").on('click', '.btnRefresh', function () {
        $(".btnUnSelect").click();
        $("#divCabinetDocList").empty();
        $("#divCabinetDocList").append('<div class="loading_partial"></div>');
        $("#divCabinetDocList").load('/Document/CabinetDocList?cabinetId=' + $("#EncryptedId").val());
    });
}());