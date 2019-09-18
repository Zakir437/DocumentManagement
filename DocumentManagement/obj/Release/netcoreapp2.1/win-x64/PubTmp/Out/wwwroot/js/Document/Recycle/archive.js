(function () {
    var selectedId = "", docType = 0, ftype = 0, fileId = 0, menuPosition, resId = 0;
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $("#divArchiveList").load('/Document/RecycleList?isArchive=true');
    });

    //**************folder menu***********************
    $("#divArchiveList").on('mousedown', '.box', function (e) {
        if (e.which === 3) {
            docType = parseInt($(this).data("doctype"));
            $(".cmenu-options").empty();
            if (docType === 1) {
                selectedId = $(this).data("id");
                ftype = parseInt($(this).data("ftype"));
                $(".cmenu-options").append('<li class="cmenu-option btnRestoreFolder" data-resid="' + $(this).data("resid") + '" data-doctype="1">' +
                    '<button class="btn btn-warning waves-effect btn-sm "  data-toggle="tooltip" title = "Restore" > <i class="fa fa-window-restore"></i></button> <span>Restore</span>' +
                    '</li> ' +
                    '<li class="cmenu-option btnPropertiesFolder" data-id="' + selectedId + '" data-ftype="' + ftype + '">' +
                    '<button type="button" class="btn btn-info waves-effect btn-sm "  data-toggle="tooltip" title="Info">&nbsp;<i class="fa fa-info"></i>&nbsp;</button> <span>Properties</span>' +
                    '</li>'
                );
            }
            else {
                fileId = parseInt($(this).data("id"));
                $(".cmenu-options").append('<li class="cmenu-option btnRestoreFile" data-resid="' + $(this).data("resid") + '" data-doctype="2">' +
                    '<button class="btn btn-warning waves-effect btn-sm" data-toggle="tooltip" title="Restore"><i class="fa fa-window-restore"></i></button> <span>Restore</span>' +
                    '</li>' +
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

    //****************************folder properties******************************
    $(".cmenu").on('click', '.btnPropertiesFolder', function () {
        selectedId = $(this).data("id");
        ftype = parseInt($(this).data("ftype"));
        $("#divArchiveList").append('<div id="divPropertiesWin"></div>');
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
        propertiesWin.refresh('/Document/Properties?folderId=' + selectedId + '&type=' + ftype + '&isRecycleBin=true');
        propertiesWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //****************************file properties******************************
    $(".cmenu").on('click', '.btnPropertiesFile', function () {
        fileId = parseInt($(this).data("id"));
        $("#divArchiveList").append('<div id="divPropertiesWin"></div>');
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

    //****************************folder restore******************************
    $(".cmenu").on('click', '.btnRestoreFolder, .btnRestoreFile', function () {
        resId = parseInt($(this).data("resid"));
        docType = parseInt($(this).data("doctype"));
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

        if (docType === 1) {
            msg = "Are you sure want to restore this folder?";
        }
        else {
            msg = "Are you sure want to restore this file?";
        }
        var template = kendo.template($("#temp_win_Doc_restore").html());
        kendoWindow.data("kendoWindow").content(template).center().open();
        kendoWindow.find("#btn_doc_restore_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_doc_restore_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/DocumentRestore',
                type: 'Post',
                data: { resId: resId },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "Restore was unsuccessful." }, "error");
                    }
                    else {
                        if (docType === 1) {
                            notification.show({ message: "Folder has been successfully restored." }, "success");
                        }
                        else {
                            notification.show({ message: "File has been successfully restored." }, "success");
                        }
                        $("#divArchiveList").empty();
                        $("#divArchiveList").append('<div class="loading_partial"></div>');
                        $("#divArchiveList").load('/Document/RecycleList?isArchive=true');
                        loadCabinetTree();
                    }
                },
                error: function () {
                    notification.show({ message: "Restore was unsuccessful." }, "error");
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

}());