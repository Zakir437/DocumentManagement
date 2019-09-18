$(function () {
    $(document).ready(function () {
        $("#spreadsheet").kendoSpreadsheet();
        var spreadsheet = $("#spreadsheet").data("kendoSpreadsheet");

        $("#editor").kendoEditor({
            resizable: {
                content: true,
                toolbar: true
            }
        });

        //$("#files").on("change", function () {
        //    alert(JSON.stringify(this.files[0]));
        //    spreadsheet.fromFile(this.files[0]);
        //});

        //$.ajax({
        //    url: '/Document/GetSpreadsheetFile?fileId=240',
        //    type: 'GET',
        //    success: function (data) {
        //        var bytes = new Uint8Array(data.FileContents);
        //        var blob = new Blob([bytes], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        //        var file = new File([blob], "asdf", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", lastModified: Date.now() });
        //        spreadsheet.fromFile(file);


        //    }
        //});
    });
});