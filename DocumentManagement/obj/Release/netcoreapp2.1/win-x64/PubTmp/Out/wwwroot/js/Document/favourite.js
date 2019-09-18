(function () {
    var folderId = "", fileId = 0, id = 0, folderType = 0, url = "", menuPosition;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#divFavouriteList").load('/Document/FavouriteList');
    });

    $("body").click(function (e)
    {
        if (e.target.className !== "box" || e.target.className !== "cmenu") {
            $(".cmenu").hide();
        }
    });
        
    $("#divFavouriteList").on('click', '.box', function () {
        folderType = parseInt($(this).data("type"));
        if (folderType > 0) {
            $("#liForFavourite").removeClass('active');
            folderId = $(this).data("folderid");
            if (folderType === 1) {
                url = '/Document/CabinetTileDetails?q=' + folderId;
                onAjaxLoad("Cabinet Details", url);
            }
            else if (folderType === 2) {
                url = '/Document/F1TileDetails?q=' + folderId;
                onAjaxLoad("F1 Details", url);
            }
            else if (folderType === 3) {
                url = '/Document/F2TileDetails?q=' + folderId;
                onAjaxLoad("F2 Details", url);
            }
            else if (folderType === 4) {
                url = '/Document/F3TileDetails?q=' + folderId;
                onAjaxLoad("F3 Details", url);
            }
        }
    });

    //**************folder menu***********************
    $("#divFavouriteList").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            folderType = parseInt($(this).data("type"));
            id = parseInt($(this).data("id"));
            if (folderType === 0) {
                fileId = $(this).data("fileid");
                $(".cmenu-options").empty();
                $(".cmenu-options").append('<li class="cmenu-option btnFileView" data-href="/Document/FileView?q=' + $(this).data("encid") + '">' +
                    '<button class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title="View"><i class="fa fa-eye"></i></button> <span>View</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnDownload" data-href="/Document/DownloadFile?fileId=' + fileId + '">' +
                    '<button class="btn btn-primary waves-effect btn-sm" data-toggle="tooltip" title="Download"><i class="fa fa-download"></i></button> <span>Download</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnUnFavFile" data-id="' + id + '">' +
                    '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Un-Favourite"><i class="fa fa-thumbs-down"></i></button> <span>Un-Favourite</span>' +
                    '</li>' +
                    '<li class="cmenu-option btnPropertiesFile" data-id="' + fileId + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else
            {
                folderId = $(this).data("folderid");
                $(".cmenu-options").empty();
                if (folderType === 1) {
                    $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-href="/Document/CabinetTileDetails?q=' + folderId + '" data-type="1">' +
                        '<a class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></a> <span>View</span>' +
                        '</li > ' +
                        '<li class="cmenu-option btnUnFavFolder" data-id="' + id + '">' +
                        '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Un-Favourite"><i class="fa fa-thumbs-down"></i></button> <span>Un-Favourite</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnPropertiesFolder" data-id="' + folderId + '" data-type="1">' +
                        '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                        '</li>'
                    );
                }
                else if (folderType === 2) {
                    $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-href="/Document/F1TileDetails?q=' + folderId + '" data-type="2">' +
                        '<a class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></a> <span>View</span>' +
                        '</li > ' +
                        '<li class="cmenu-option btnUnFavFolder" data-id="' + id + '">' +
                        '<button type="button" class="btn btn-danger waves-effect btn-sm" data-toggle="tooltip" title="Un-Favourite"><i class="fa fa-thumbs-down"></i></button> <span>Un-Favourite</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnPropertiesFolder" data-id="' + folderId + '" data-type="2">' +
                        '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                        '</li>'
                    );
                }
                else if (folderType === 3) {
                    $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-href="/Document/F2TileDetails?q=' + folderId + '" data-type="3" >' +
                        '<a class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></a> <span>View</span>' +
                        '</li > ' +
                        '<li class="cmenu-option btnUnFavFolder" data-id="' + id + '">' +
                        '<button type="button" class="btn btn-danger waves-effect btn-sm"  data-toggle="tooltip" title="Un-Favourite"><i class="fa fa-thumbs-down"></i></button> <span>Un-Favourite</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnPropertiesFolder" data-id="' + folderId + '" data-type="3">' +
                        '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                        '</li>'
                    );
                }
                else if (folderType === 4) {
                    $(".cmenu-options").append('<li class="cmenu-option btnViewDetails" data-href="/Document/F3TileDetails?q=' + folderId + '" data-type="4">' + 
                        '<a class="btn btn-info waves-effect btn-sm" data-toggle="tooltip" title = "View details" > <i class="fa fa-eye"></i></a> <span>View</span>' +
                        '</li > ' +
                        '<li class="cmenu-option btnUnFavFolder" data-id="' + id + '">' +
                        '<button type="button" class="btn btn-danger waves-effect btn-sm"  data-toggle="tooltip" title="Un-Favourite"><i class="fa fa-thumbs-down"></i></button> <span>Un-Favourite</span>' +
                        '</li>' +
                        '<li class="cmenu-option btnPropertiesFolder" data-id="' + folderId + '" data-type="4">' +
                        '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                        '</li>'
                    );
                }
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

    //********************************View Details***********************************
    $(".cmenu").on('click', '.btnViewDetails', function (e) {
        $("#liForFavourite").removeClass('active');
        var url = $(this).data("href");
        folderType = parseInt($(this).data("type"));
        if (folderType === 1) {
            onAjaxLoad("Cabinet Details", url);
        }
        else if (folderType === 2) {
            onAjaxLoad("F1 Details", url);
        }
        else if (folderType === 3) {
            onAjaxLoad("F2 Details", url);
        }
        else if (folderType === 4) {
            onAjaxLoad("F3 Details", url);
        }
    });

    //****************************properties******************************
    $(".cmenu").on('click', '.btnPropertiesFolder', function () {
        folderId = $(this).data("id");
        folderType = parseInt($(this).data("type"));
        $("#divFavouriteList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + folderId + '&type=' + folderType);
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //********************************View files***********************************
    $(".cmenu").on('click', '.btnFileView', function (e) {
        var url = $(this).data("href");
        window.open(url);
    });
    //********************************download***********************************
    $(".cmenu").on('click', '.btnDownload', function (e) {
        var url = $(this).data("href");
        window.location.href = url;
    });

    //****************************file properties******************************
    $(".cmenu").on('click', '.btnPropertiesFile', function () {
        fileId = $(this).data("id");
        $("#divFavouriteList").append('<div id="divPropertiesWin"></div>');
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

    //********************************un-favourite folder***********************************
    $(".cmenu").on('click', '.btnUnFavFolder', function (e) {
        id = parseInt($(this).data("id"));
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { favId : id },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "Folder un-favourite was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "Folder has been successfully delete from favourite." }, "success");
                    $("#divFavouriteList").empty();
                    $("#divFavouriteList").append('<div class="loading_partial"></div>');
                    $("#divFavouriteList").load('/Document/FavouriteList');
                    favouriteList(); //load favourite list in header
                }
            },
            error: function (error) {
                notification.show({ message: "Folder un-favourite was unsuccessful." }, "error");
            }
        });
    });


    //********************************un-favourite files***********************************
    $(".cmenu").on('click', '.btnUnFavFile', function (e) {
        id = parseInt($(this).data("id"));
        $.ajax({
            url: '/Document/FavouriteSave',
            type: 'Post',
            data: { favId: id },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "File un-favourite was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "File has been successfully delete from favourite." }, "success");
                    $("#divFavouriteList").empty();
                    $("#divFavouriteList").append('<div class="loading_partial"></div>');
                    $("#divFavouriteList").load('/Document/FavouriteList');
                }
            },
            error: function (error) {
                notification.show({ message: "File un-favourite was unsuccessful." }, "error");
            }
        });
    });

}());