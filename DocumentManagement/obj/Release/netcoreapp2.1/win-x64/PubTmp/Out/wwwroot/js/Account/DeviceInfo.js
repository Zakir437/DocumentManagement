
//https://api.ipify.org/ //unlimited bandwidth
//https://jsonip.com/
function deviceInfo() {
    let info = {
        Browser: "Unknown",
        DeviceOS: "Unknown"
    };
    let ba = ["Chrome", "Firefox", "Safari", "Opera", "MSIE", "Edge", "Mozilla"];
    let osName = ["Windows", "Mac", "Linux"];
    let kendAgent = Object.keys(kendo.support.browser);//using kendo ui
    let browser = "Unknown", OS = "Unknown";
    for (let b in ba) {
        if (kendAgent.indexOf(ba[b].toLowerCase()) !== -1) {
            browser = ba[b];
            info.Browser = browser;
            break;
        }
    }
    for (let os in osName) {
        if (navigator.userAgent.toLowerCase().indexOf(osName[os].toLowerCase()) !== -1) {
            OS = osName[os] + " Pc";
            info.DeviceOS = OS;
            break;
        }
    }
    return info;
}

function ipInfo() {
    let info = {
        IPAddress: "Unknown",
        City: "Unknown",
        Country: "Unknown"
    };

    var promise = new Promise(function (resolve, reject) {
        $.getJSON('https://ipapi.co/json', function (data) {
            if (data !== null || data !== undefined) {
                info.IPAddress = data.ip;
                info.City = data.city;
                info.Country = data.country_name;
                resolve(info);
            }
            else {
                reject(info);
            }
        });
        //resolve(info);
    });

    promise.then(function (value) {
        var a = deviceInfo();//get device info
        let allInfo = {
            Browser: a.Browser,
            DeviceOS: a.DeviceOS,
            IPAddress: value.IPAddress,
            City: value.City,
            Country: value.Country,
            UniqueId: "UID"
        };
        $.ajax({
            url: '/Account/RegisterLoginDevice',
            type: "POST",
            data: allInfo,
            success: function (result) {
                if (result === "success") {
                    console.log(result);
                }
                else {
                    console.log(result);
                }
            }
        });
    });
}
