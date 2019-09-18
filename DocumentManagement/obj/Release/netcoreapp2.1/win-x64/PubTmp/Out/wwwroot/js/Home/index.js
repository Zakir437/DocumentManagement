$(function () {
    var encId = "", id = 0, type = 0, menuPosition, isPined = "";
    $(document).ready(function () {
        $("#liForDashboard").addClass('active');
        $("#divFrequentFolder").load('/Document/FrequentFolderList');
        $("#divRecentFiles").load('/Document/RecentFileList');

    });

    //*****************View file destination*****************
    $("#divRecentFiles").on('click', '.btnView', function () {
        encId = $(this).data("id");
        type = parseInt($(this).data("type"));
        if (type === 1) {
            onAjaxLoad("Cabinet Details", "/Document/CabinetTileDetails?q=" + encId);
        }
        else if (type === 2) {
            onAjaxLoad("F1 Details", "/Document/F1TileDetails?q=" + encId);
        }
        else if (type === 3) {
            onAjaxLoad("F2 Details", "/Document/F2TileDetails?q=" + encId);
        }
        else if (type === 4) {
            onAjaxLoad("F3 Details", "/Document/F3TileDetails?q=" + encId);
        }
    });

    //*****************recent file delete*****************
    $("#divRecentFiles").on('click', '.btnDelete', function () {
        id = parseInt($(this).data("id"));
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
        msg = "Are you sure want to delete this recent file?";
        var template = kendo.template($("#temp_win_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/RecentFileDelete',
                type: 'Post',
                data: { id: id },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Recent file has been successfully deleted." }, "success");
                        $("#divRecentFiles").empty();
                        $("#divRecentFiles").append('<div class="loading_partial"></div>');
                        $("#divRecentFiles").load('/Document/RecentFileList');
                    }
                    else {
                        notification.show({ message: "Recent file delete was unsuccessful." }, "error");
                    }
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    $("#divFrequentFolder").on('click', '.divIcon', function () {
        type = parseInt($(this).data("type"));
        encId = $(this).data("folderid");
        if (type === 1) {
            url = '/Document/CabinetTileDetails?q=' + encId;
            onAjaxLoad("Cabinet Details", url);
        }
        else if (type === 2) {
            url = '/Document/F1TileDetails?q=' + encId;
            onAjaxLoad("F1 Details", url);
        }
        else if (type === 3) {
            url = '/Document/F2TileDetails?q=' + encId;
            onAjaxLoad("F2 Details", url);
        }
        else if (type === 4) {
            url = '/Document/F3TileDetails?q=' + encId;
            onAjaxLoad("F3 Details", url);
        }
    });

    $("#divFrequentFolder").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            id = parseInt($(this).data("id"));
            type = parseInt($(this).data("type"));
            encId = $(this).data("folderid");
            isPined = $(this).data("ispined");
            $(".cmenu-options").empty();
            $(".cmenu-options").append('<li class="cmenu-option btnPinFolder" data-id="' + id + '" data-ispined="' + isPined + '">' +
                '<button type="button" class="btn btn-primary waves-effect btn-sm"  data-toggle="tooltip" title="Pin">' + (isPined === "True" ? '<i class="k-icon k-i-unpin"></i>' : '<i class="k-icon k-i-pin"></i>') + '</button> <span>' + (isPined === "True" ? 'Unpin' : 'Pin to frequent') + '</span>' +
                '</li>' +
                '<li class="cmenu-option btnFavouriteFolder" data-id="' + encId + '" data-type="' + type +'">' +
                '<button type="button" class="btn bg-teal waves-effect btn-sm"  data-toggle="tooltip" title="Favourite"><i class="fa fa-heart"></i></button> <span>Favourite</span>' +
                '</li>' +
                '<li class="cmenu-option btnDelete" data-id="' + id + '" > ' +
                '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Delete"><i class="fa fa-trash"></i></button> <span>Delete</span>' +
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

    //****************************folder favourite******************************
    $(".cmenu").on('click', '.btnFavouriteFolder', function () {
        encId = $(this).data("id");
        type = parseInt($(this).data("type"));
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { folderId: encId, folderType: type },
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

    //****************************folder pin/unpin******************************
    $(".cmenu").on('click', '.btnPinFolder', function () {
        id = parseInt($(this).data("id"));
        isPined = $(this).data("ispined");
        $.ajax({
            url: '/Document/PinFolderSave',
            type: 'Post',
            data: { freqFolderId: id },
            success: function (data) {
                if (data === "error") {
                    if (isPined === "True") {
                        notification.show({ message: "Folder unpin was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "Folder pin was unsuccessful." }, "error");
                    }
                }
                else {
                    if (isPined === "True") {
                        notification.show({ message: "Folder has been successfully unpined from frequent." }, "success");
                    }
                    else {
                        notification.show({ message: "Folder has been successfully pined to frequent." }, "success");
                    }
                    $("#divFrequentFolder").empty();
                    $("#divFrequentFolder").append('<div class="loading_partial"></div>');
                    $("#divFrequentFolder").load('/Document/FrequentFolderList');
                }
            },
            error: function (error) {
                if (isPined === "True") {
                    notification.show({ message: "Folder unpin was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "Folder pin was unsuccessful." }, "error");
                }
            }
        });
    });

    //****************************folder delete from frequent list******************************
    $(".cmenu").on('click', '.btnDelete', function (e) {
        id = parseInt($(this).data("id"));
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
        msg = "Are you sure want to delete this frequent folder?";
        var template = kendo.template($("#temp_win_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/FrequentFolderDelete',
                type: 'Post',
                data: { id: id },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Frequent Folder has been successfully deleted." }, "success");
                        $("#divFrequentFolder").empty();
                        $("#divFrequentFolder").append('<div class="loading_partial"></div>');
                        $("#divFrequentFolder").load('/Document/FrequentFolderList');
                    }
                    else {
                        notification.show({ message: "Frequent Folder delete was unsuccessful." }, "error");
                    }
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    $("#divFrequentFolder").on('click', '.deleteIcon', function () {
        id = parseInt($(this).data("id"));
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
        msg = "Are you sure want to delete this frequent folder?";
        var template = kendo.template($("#temp_win_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/FrequentFolderDelete',
                type: 'Post',
                data: { id: id },
                success: function (data) {
                    if (data === "success") {
                        notification.show({ message: "Frequent Folder has been successfully deleted." }, "success");
                        $("#divFrequentFolder").empty();
                        $("#divFrequentFolder").append('<div class="loading_partial"></div>');
                        $("#divFrequentFolder").load('/Document/FrequentFolderList');
                    }
                    else {
                        notification.show({ message: "Frequent Folder delete was unsuccessful." }, "error");
                    }
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

    $("#divFrequentFolder").on('mouseenter', '.box', function () {
        $(this).find('.deleteIcon').css('display', 'flex');
    });
    $("#divFrequentFolder").on('mouseleave', '.box', function () {
        $(this).find('.deleteIcon').hide();
    });
});