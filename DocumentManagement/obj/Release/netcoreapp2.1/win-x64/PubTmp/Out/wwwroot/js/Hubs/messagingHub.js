"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/MessagingHub").build();
var UserInfo = {};
connect(connection);

connection.on("GetNotification", function (c) {
    //console.log(c);
    //console.log(parseInt($(".sb-notification").text()));
    let count = c + (parseInt($(".sb-notification").text()) > 0 ? parseInt($(".sb-notification").text()) : 0);
    $(".sb-notification").text(count);
    pushNotification.show({ message: "System Configuration has been changed." }, "success");
});

/*connection.start().catch(function (err) {
    return console.error(err.toString());
});*/

//Connect SignalR
async function connect(conn) {
    conn.start().catch(e => {
        sleep(5000);
        console.log("Reconnecting Socket...");
        connect(conn);
    });
}

//Reconnect SignalR
connection.onclose(function (e) {
    console.log("Socket Disconnected");
    connect(connection);
});
async function sleep(msec) {
    //console.log("Try Reconnecting...");
    return new Promise(resolve => setTimeout(resolve, msec));
}


//Get current user name, username, id etc.
connection.on("GetCurrentUserInfo", function (user) {
    //console.log(user);
    if (user !== null || use !== undefined) {

        UserInfo.UserId = user.userId;
        UserInfo.UserName = user.userName;
        UserInfo.UserFullName = user.userFullName;
    }
});

setTimeout(function () {
    connection.invoke("SendCurrentUserInfo").catch(function (err) {
    });
}, 5000);


