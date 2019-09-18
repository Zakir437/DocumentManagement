(function () {
    var f2Id = "", files = [], inputFiles, fileId = 0;
    $(document).ready(function () {
        f2Id = $("#F2Id").val();
        $('[data-toggle="tooltip"]').tooltip();
        $("#liForCabinet").addClass('active');
        $("#liForCabinet .arrow").addClass('open');
        $("#liForCabinetList").addClass("active");

        $("#divF3List").load('/Document/F3List?q=' + f2Id);
        $("#divFileList").load('/Document/FileList?f2Id=' + f2Id + '&type=3');
    });

    $("#divF3List").on('click', '.btnViewDetails', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("F3 Details", url);
    });

    $("#btnAddFolder").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        onAjaxLoad("F2 Add", url);
    });


    //**********************File upload****************************************
    $("#files").change(function () {
        files = [];
        inputFiles = document.getElementById('files');
        if (inputFiles.files.length > 0) {
            $("#divFileUpload").show();
            $("#divSelectedFile").empty();
            for (i = 0; i <= inputFiles.files.length - 1; i++) {
                files.push(inputFiles.files.item(i));
                var fname = inputFiles.files.item(i).name;
                var extn = fname.substring(fname.lastIndexOf('.') + 1).toLowerCase();
                if (extn === "pdf") {
                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-file-pdf file-icon-size pdf-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else if (extn === "doc" || extn === "docx") {

                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-file-word file-icon-size word-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else if (extn === "txt") {

                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-file-txt file-icon-size txt-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else if (extn === "gif" || extn === "png" || extn === "jpg" || extn === "jpeg") {

                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-image file-icon-size img-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else if (extn === "ppt" || extn === "pptm" || extn === "pptx") {
                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-file-ppt file-icon-size pp-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else if (extn === "xlsx" || extn === "xlsm" || extn === "xltx" || extn === "xltm") {
                    $("#divSelectedFile").append(
                        '<div class="col-md-3">' +
                        '<div class="file-box">' +
                        '<i class="k-icon k-i-file-excel file-icon-size excel-icon-color"></i> ' + fname +
                        '</div>' +
                        '</div>'
                    );
                }
                else {
                    alert("This (" + fname + ") file not supported.");
                    files.pop(inputFiles.files.item(i));
                    if (files.length === 0) {
                        $("#divFileUpload").hide();
                        $("#divSelectedFile").empty();
                    }
                }
            }
        }
        else {
            alert('Please select a file.');
            $("#divFileUpload").hide();
            $("#divSelectedFile").empty();
            files = [];
        }
    });

    //*************************File save cancel*****************************************
    $("#btnFileUploadCancel").click(function () {
        $("#divFileUpload").hide();
        $("#divSelectedFile").empty();
        files = [];
    });

    //*****************************File Save************************
    $("#btnFileUpload").click(function () {
        if (files.length > 0) {
            $(this).prop('disabled', true);
            var sendData = new FormData();
            sendData.append("f2Id", f2Id);
            sendData.append("type", 3);
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
                    $("#btnFileUpload").prop('disabled', false);
                    $("#btnFileUploadCancel").click();
                    if (data === "error") {
                        notification.show({ message: "File upload was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "File has been successfully upload." }, "success");
                        $("#divFileList").empty();
                        $("#divFileList").append('<div class="tiny_loading"></div>');
                        $("#divFileList").load('/Document/FileList?f2Id=' + f2Id + '&type=3');
                    }
                },
                error: function (error) {
                    $("#btnFileUpload").prop('disabled', false);
                    $("#btnFileUploadCancel").click();
                    notification.show({ message: "File upload was unsuccessful." }, "error");
                }
            });
        }
    });

    //****************************Replace file***********************************
    $("#divFileList").on('change', '.replaceFile', function (e) {
        fileId = parseInt($(this).data("id"));
        files = [];
        if (e.target.files.length > 0) {
            var fname = e.target.files.item(0).name;
            var extn = fname.substring(fname.lastIndexOf('.') + 1).toLowerCase();
            if (extn === "pdf") {
                files.push(e.target.files.item(0));
            }
            else if (extn === "doc" || extn === "docx" || extn === "txt") {
                files.push(e.target.files.item(0));
            }
            else if (extn === "gif" || extn === "png" || extn === "jpg" || extn === "jpeg") {
                files.push(e.target.files.item(0));
            }
            else if (extn === "ppt" || extn === "pptm" || extn === "pptx") {
                files.push(e.target.files.item(0));
            }
            else if (extn === "xlsx" || extn === "xlsm" || extn === "xltx" || extn === "xltm") {
                files.push(e.target.files.item(0));
            }
            else {
                alert("This (" + fname + ") file not supported.");
            }
            if (files.length > 0) {
                var sendData = new FormData();
                sendData.append("fileId", fileId);
                sendData.append("files[0]", files[0]);
                $.ajax({
                    url: '/Document/FileSave',
                    type: 'Post',
                    data: sendData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data === "error") {
                            notification.show({ message: "File replace was unsuccessful." }, "error");
                        }
                        else {
                            notification.show({ message: "File has been successfully replace." }, "success");
                            $("#divFileList").empty();
                            $("#divFileList").append('<div class="tiny_loading"></div>');
                            $("#divFileList").load('/Document/FileList?f2Id=' + f2Id + '&type=3');
                        }
                    },
                    error: function () {
                        notification.show({ message: "File replace was unsuccessful." }, "error");
                    }
                });
            }
        }
    });

    //****************************View file archive list***********************************
    $("#divFileList").on('click', '.btnViewArchive', function (e) {
        fileId = parseInt($(this).data("id"));
        $("#divFileArchiveWin").empty();
        $("#divFileArchiveWin").kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "45%",
            height: "40%",
            title: 'File Archive List',
            close: onWindowClose
        });
        var archiveWin = $("#divFileArchiveWin").data("kendoWindow");
        archiveWin.refresh('/Document/FileArchive?id=' + fileId);
        archiveWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });

    //*******************file delete************************************
    $("#divFileList").on('click', '.btnFileDelete ', function () {
        fileId = $(this).data("id");
        var kendoWindow = $("<div />").kendoWindow({
            actions: ["Close"],
            title: "Alert",
            width: "30%",
            modal: true
        });
        msg = "Are you sure want to delete this file?";
        var template = kendo.template($("#temp_win_file_delete_entry").html());
        kendoWindow.data("kendoWindow").content(template).center().open();

        kendoWindow.find("#btn_file_delete_Entry_noty_cancel").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
        }).end();
        kendoWindow.find("#btn_file_delete_Entry_noty_ok").click(function () {
            kendoWindow.data("kendoWindow").close();
            document.documentElement.style.overflow = "auto";
            $.ajax({
                url: '/Document/FileStatusChange',
                type: 'POST',
                data: { fileId: fileId, status: 2, isKeepDocument: kendoWindow.find("#keepCheckBox").is(":checked") },
                success: function (data) {
                    if (data === "error") {
                        notification.show({ message: "File restore was unsuccessful." }, "error");
                    }
                    else {
                        notification.show({ message: "File has been successfully restored." }, "success");
                        $("#divFileList").empty();
                        $("#divFileList").append('<div class="tiny_loading"></div>');
                        $("#divFileList").load('/Document/FileList?f2Id=' + f2Id + '&type=3');
                    }
                    $("#btnArchiveCancel").closest(".k-window-content").data("kendoWindow").close();
                }
            });
        }).end();
        document.documentElement.style.overflow = "hidden";
    });

}());