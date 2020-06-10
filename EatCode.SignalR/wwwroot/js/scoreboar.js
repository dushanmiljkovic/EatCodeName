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
        addItemToTheScore(item.name, item.score, i); 
    }
}
 
var clearScoreboard = function () {
    document.getElementById('tbody').innerHTML = ''
}
 
var addItemToTheScore = function (name,score,no) {
    var tableRef = document.getElementById('scoreboar').getElementsByTagName('tbody')[0];

    // Insert a row in the table at the last row
    var newRow = tableRef.insertRow();
     
    var cell1 = newRow.insertCell(0);
    var cell2 = newRow.insertCell(1);
    var cell3 = newRow.insertCell(2);

    cell1.innerHTML = no+1;
    cell2.innerHTML = name;
    cell3.innerHTML = score; 
}



document.getElementById("refreshButton").addEventListener("click", function (event) {
    connection.invoke("ShowScoreboar").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});