(function () {
    $(document).ready(function () {
        $("#divStorageDetailsInfo").load('/Document/_StorageInfo');
    });
    $("#btnStorageInfoCancel").click(function () {
        $("#divStorageDetails").data("kendoWindow").close();
    });
}())