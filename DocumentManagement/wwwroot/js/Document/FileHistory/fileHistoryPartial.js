﻿(function () {
    $(document).ready(function () {
        $("#divFileHistoryList").load('/Document/FileHistoryList?id=' + $("#FileId").val());
    });
    $("#btnFileHistoryCancel").click(function () {
        $("#divHistoryWin").data("kendoWindow").close();
    });
}());