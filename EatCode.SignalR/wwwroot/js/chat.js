"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, status) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var sign = status ? "✓" : "✗";
    var encodedMsg = user + " : " + msg + " [" + sign + "]" + " [" + getTimeStamp() + "]";

    var li = document.createElement("li");
    li.classList.add("list-group-item");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveMessageHistory", function (messages) {
    clearChat();
    loadHistory(messages);
});



connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

var getTimeStamp = function () {
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + ' ' + time;
    return dateTime;
}

var loadHistory = function (items) {
    if (items.length > 0) {
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var msg = cleanMessage(item);
            addItemToTheChet(msg);
        }
    }
    else {
        addItemToTheChet("no chat history");
    }
}



var cleanMessage = function (txt) {
    return txt.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

var clearChat = function () {

    var messageList = document.getElementById("messagesList");
    messageList.innerHTML = "";
}

var addItemToTheChet = function (msg) {

    var li = document.createElement("li");
    li.classList.add("list-group-item");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);
}