(function () {
    var id = 0;
    $(document).ready(function () {
        id = parseInt($("#fileId").val());
        $("#divArchiveList").load('/Document/_FileArchive?id=' + id);
    });
    $("#btnArchiveCancel").click(function () {
        $(this).closest(".k-window-content").data("kendoWindow").close();
    });
    $("#divArchiveList").on('click', '.btnFileRestore', function () {
       var restoreId = parseInt($(this).data("id"));
        $.ajax({
            url: '/Document/FileStatusChange',
            type: 'POST',
            data: { fileId: restoreId, activeId: id, status: 3 },
            success: function (data) {
                if (data === "error") {
                    notification.show({ message: "File restore was unsuccessful." }, "error");
                }
                else {
                    notification.show({ message: "File has been successfully restored." }, "success");
                    $("#divFileList").empty();
                    $("#divFileList").append('<div class="tiny_loading"></div>');
                    if (data.type === 1) {
                        $("#divFileList").load('/Document/FileList?cabinetId=' + data.cabinetId + '&type=1');
                    }
                    else if (data.type === 2) {
                        $("#divFileList").load('/Document/FileList?f1Id=' + data.f1Id + '&type=2');
                    }
                    else if (data.type === 3) {
                        $("#divFileList").load('/Document/FileList?f2Id=' + data.f2Id + '&type=3');
                    }
                    else if (data.type === 4) {
                        $("#divFileList").load('/Document/FileList?f3Id=' + data.f3Id + '&type=4');
                    }
                }
                $("#btnArchiveCancel").closest(".k-window-content").data("kendoWindow").close();
            }
        });
    });
}());