﻿(function () {
    $(document).ready(function () {
        $("#divProperties").load('/Document/_Properties?fileId=' + $("#PFileId").val() + '&folderId=' + $("#PFolderId").val() + '&type=' + $("#PType").val() + '&isRecycleBin=' + $("#IsRrecycleBin").val());
    });
    $("#btnProCancel").click(function () {
        $("#divPropertiesWin").data("kendoWindow").close();
    });
}());