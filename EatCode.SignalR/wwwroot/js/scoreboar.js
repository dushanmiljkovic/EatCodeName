"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/scoreboarHub").build();
var connectionChatHub = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveScoreboar", function (scoreboarItems) {
    clearScoreboard();
    createScoreboard(scoreboarItems);
});

connectionChatHub.on("ReceiveScoreboar", function (scoreboarItems) {
    clearScoreboard();
    createScoreboard(scoreboarItems);
});

connection.start().then(function () {
    document.getElementById("refreshButton").disabled = false;
    clearScoreboard();
}).catch(function (err) {
    return console.error(err.toString());
});

connectionChatHub.start();

var createScoreboard = function (items) {
    for (var i = 0; i < items.length; i++) {
        var item = items[i];

        var li = document.createElement("li");
        li.textContent = item.name + " - " + item.score;
        document.getElementById("scoreboar").appendChild(li);
    }
}

var clearScoreboard = function () {
    document.getElementById("scoreboar").innerHTML = "";
}

document.getElementById("refreshButton").addEventListener("click", function (event) {
    connection.invoke("ShowScoreboar").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});