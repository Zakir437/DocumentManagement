// Write your JavaScript code.
var notification, pushNotification;
(function () {
    //var encId = "", type = 0, url = "", cabinetCount = 0, f1Count = 0, f2Count = 0, f3Count = 0;
    jQuery(document).ready(function () {
        storageInfo(); //load storage info
        favouriteList();//load favourite list
        Metronic.init(); // init metronic core componets
        Layout.init(); // init layout
        QuickSidebar.init(); // init quick sidebar
        //Demo.init(); // init demo features
        //Index.init();
        //Index.initDashboardDaterange();
        //Index.initJQVMAP(); // init index page's custom scripts
        //Index.initCalendar(); // init index page's custom scripts
        //Index.initCharts(); // init index page's custom scripts
        //Index.initChat();
        //Index.initMiniCharts();
        //Tasks.initDashboardWidget();

        $('#ul_lft_bar').slimScroll({
            height: 'calc(100vh - 50px)',
            color: '#bbbbbb'
        });

        if (document.title === 'Home') {
            if (history.pushState) {
                history.pushState({ "html": $('.portal-render').html(), "pageTitle": document.title }, "");
            }
        }

        //**************Notification*****************************
        notification = $("#notification").kendoNotification({
            width: "24em",
            position: {
                pinned: true,
                top: 60,
                right: 15
            },
            autoHideAfter: 2000,
            templates: [{
                type: "error",
                template: $("#errorTemplate").html()
            }, {
                type: "success",
                template: $("#successTemplate").html()
            }]
        }).data("kendoNotification");

    });

    //**************Push Notification*****************************
    pushNotification = $("#pushNotification").kendoNotification({
        width: "24em",
        position: {
            pinned: true,
            bottom: 60,
            left: 15
        },
        autoHideAfter: 3000,
        animation: {
            open: {
                effects: "slideIn:right"
            },
            close: {
                effects: "slideIn:right",
                reverse: true
            }
        },
        templates: [{
            type: "error",
            template: $("#pushErrorTemplate").html()
        }, {
            type: "success",
            template: $("#pushSuccessTemplate").html()
        }]
    }).data("kendoNotification");

    $("body").click(function (e) {
        if (e.target.className !== "box" || e.target.className !== "nav-cmenu") {
            $(".nav-cmenu").hide();
        }
    }
    );

    $("body").click(function (e) {
        if (e.target.className !== "box" || e.target.className !== "cmenu") {
            $(".cmenu").hide();
        }
    }
    );

    //*****************************start cabinet autocomplete*******************
    $("#cabinetAutoComplete").kendoAutoComplete({
        filter: "contains",
        placeholder: "Search folder, file and docs",
        // JSON property name to use
        dataSource: new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: {
                    url: '/Document/GetSearchResult',
                    serverPaging: true,
                    pageSize: 20,
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    dataType: 'json',
                    data: function (e) {
                        let widget = $('#cabinetAutoComplete').data('kendoAutoComplete');
                        let text = widget.value();
                        if (text === "Search folder, file and docs") {
                            text = "";
                        }
                        return {
                            text: text
                        };
                    }
                }
            }
        }),
        dataValueField: "value",
        dataTextField: "text",
        select: onSelectCabinet,
        change: onChangeCabinet
    });

    function onSelectCabinet(e) {
        $(".portal-search-result").show();
        var text = "";
        text = e.dataItem.value;
        var model = {
            text: text.toString()
        };
        $("#divSearchResult").empty();
        $("#divSearchResult").append('<div class="loading_partial"></div>');
        $("#divSearchResult").load('/Document/SearchResultInfo', model);
    }
    function onChangeCabinet(e) {
        if (this.value() === "") {
            $(".portal-search-result").hide();
            $("#divSearchResult").empty();
        }
    }
    $("#divSearchResult").on('click', '.btnView', function () {
        var type = parseInt($(this).data("type"));
        var encId = $(this).data("id");
        if ($(this).data("isfile") === "True") {
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
        }
        else {
            if (type === 1) {
                onAjaxLoad("Cabinet", "/Document/CabinetTile");
            }
            else if (type === 2) {
                onAjaxLoad("Cabinet Details", "/Document/CabinetTileDetails?q=" + encId);
            }
            else if (type === 3) {
                onAjaxLoad("F1 Details", "/Document/F1TileDetails?q=" + encId);
            }
            else if (type === 4) {
                onAjaxLoad("F2 Details", "/Document/F2TileDetails?q=" + encId);
            }
        }
        $(".portal-search-result").hide();
        $("#cabinetAutoComplete").data("kendoAutoComplete").value('');
    });
    //*****************************end cabinet autocomplete*******************


    $("#ul_lft_bar").on('click', 'a', function (e) {
        e.preventDefault();
        url = $(this).attr("href");
        if (url !== undefined && url !== "javascript:;") {
            //$("ul.page-sidebar-menu li").removeClass('active');
            //$("ul.page-sidebar-menu li").removeClass('open');
            //$("ul.page-sidebar-menu li a .arrow").removeClass('open');
            //$("ul.sub-menu li").removeClass('active');
            //$("ul.sub-menu li").removeClass('open');
            //$("ul.sub-menu li .arrow").removeClass('open');
            //$(this).parent().parent().find('ul').toggle(false, 'slow');
            //if (parseInt($(this).data("type")) === 2) {
            //    $(this).parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //}
            //else if (parseInt($(this).data("type")) === 3) {
            //    $(this).parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //    $(this).parent().parent().parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //}
            //else if (parseInt($(this).data("type")) === 4) {
            //    $(this).parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //    $(this).parent().parent().parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //    $(this).parent().parent().parent().parent().parent().parent().parent().siblings().find('ul').toggle(false, 'slow');
            //}
            var title = $(this).data("title");

            //check search result 
            if ($(".portal-search-result").is(':visible')) {
                $(".portal-search-result").hide();
                $("#cabinetAutoComplete").data("kendoAutoComplete").value('');
            }
            onAjaxLoad(title, url);
        }
    });

    window.onpopstate = function (e) {
        if (e.state) {
            $("ul.page-sidebar-menu li").removeClass('active');
            $('.portal-render').html(e.state.html);
            document.title = e.state.pageTitle;
        }
    };

    //storage details
    $("#storageDetails, #btnViewStorageDetails").click(function () {
        $("#div_lft_bar").append('<div id="divStorageDetails"></div>');
        $("#divStorageDetails").empty().kendoWindow({
            actions: ["Close"],
            draggable: false,
            modal: true,
            visible: false,
            width: "30%",
            height: "50%",
            title: 'Storage',
            close: onWindowClose,
            deactivate: function () {
                this.destroy();
            }
        });
        var storageWin = $("#divStorageDetails").data("kendoWindow");
        storageWin.refresh('/Document/StorageInfo');
        storageWin.center().open();
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no";
    });
}());

function storageInfo() {
    $("#divStorageInfo").empty();
    $("#usedli").empty();
    $("#allocatedli").empty();
    $("#availableli").empty();
    $("#percentUsedli").empty();
    $("#storageProgressli").empty();
    //storage info in left nav
    $("#allocatedNav").empty();
    $("#usedNav").empty();
    $("#availableNav").empty();
    $("#usedPercentNav").empty();
    $.ajax({
        url: '/Document/GetStorageInfo',
        type: 'GET',
        success: function (data) {
            if (data === "Error") {
                $("#divStorageInfo").append("Error");
            }
            else {
                $("#divStorageInfo").append(data.used + " of " + data.allocated + " used ");
                //storage info in dropdown menu
                //storage info in nav
                $("#usedli, #usedNav").append(data.used);
                $("#allocatedli, #allocatedNav").append(data.allocated);
                $("#availableli, #availableNav").append(data.available);
                $("#percentUsedli, #usedPercentNav").append(data.percentUsed.toFixed(0) + "%");
                $("#storageProgressli, #storageProgressBar").css("width", data.percentUsed + "%");

                if (data.percentUsed > 70) {
                    $("#storageProgressBar, #storageProgressli").removeClass('progress-bar-success');
                    $("#storageProgressBar, #storageProgressli").addClass('progress-bar-danger');
                }
            }
        }
    });
}

//load favourite list in header favourite menu
function favouriteList() {
    $.ajax({
        url: '/Document/GetFavouriteList',
        type: 'GET',
        success: function (data) {
            $("#ulFavouriteList").empty();
            if (data !== null) {
                $(data).each(function (index, value) {
                    $("#ulFavouriteList").append(
                        '<li>'+
                            '<a href="javascript:;">'+
                                '<span class="task">'+ (value.type === 1 ? '<img src="../images/icon/cabinet.png" width="15" height="15" />  ' : '<span class="k-icon k-i-folder color-folder"></span>  ') + value.name +'</span>'+
                            '</a>'+
                        '</li>'
                    );
                });
            }
            else {
                $("#ulFavouriteList").append('<li><a href="javascript:;"><span class="task">Empty</span></a></li>');
            }
        }
    });
}

function onWindowClose() {
    document.documentElement.style.overflow = 'auto';
    document.body.scroll = "yes";
}

function resetNavMenu() {
    $("ul.page-sidebar-menu li").removeClass('active');
    $("ul.page-sidebar-menu li").removeClass('open');
    $("ul.page-sidebar-menu li a .arrow").removeClass('open');
    $("ul.sub-menu li").removeClass('active');
    $("ul.sub-menu li").removeClass('open');
    $("ul.sub-menu li .arrow").removeClass('open');
    $("ul.page-sidebar-menu").find('ul').toggle(false, 'slow');
}

function onAjaxLoad(title, url) {
    resetNavMenu();
    document.title = title;
    $.ajaxSetup({ cache: false });
    $('.portal-render').empty();
    $.ajax({
        url: url,
        success: function (response) {
            $('.portal-render').html(response);
            if (history.pushState) {
                history.pushState({ "html": response, "pageTitle": document.title }, "", url);
            }
        }
    });
}

//properties, history, storagedetails window close click outside of window
$(document).on("click", ".k-overlay", function () {
    if ($("#divPropertiesWin").length > 0) {
        $("#divPropertiesWin").data("kendoWindow").close();
    }

    if ($("#divHistoryWin").length > 0) {
        $("#divHistoryWin").data("kendoWindow").close();
    }

    if ($("#divStorageDetails").length > 0) {
        $("#divStorageDetails").data("kendoWindow").close();
    }
});