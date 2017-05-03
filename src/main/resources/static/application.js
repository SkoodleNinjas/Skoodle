var stompClient = null;

function setConnected(connected) {
    $("#connect").prop("disabled", connected);
    $("#disconnect").prop("disabled", !connected);
    if (connected) {
        $("#conversation").show();
    }
    else {
        $("#conversation").hide();
    }
//    $("#greetings").html("");
}

jQuery.ajax({
            url: "/lobby",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function(resultData) {
                for(i = 0; i < resultData.length; i++) {
                    showRoom(resultData[i])
                }
            },
            error : function(jqXHR, textStatus, errorThrown) {
            },

            timeout: 120000,
        });

window.onload = function connect() {
    var socket = new SockJS('/socket-end');
    stompClient = Stomp.over(socket);
    stompClient.connect({}, function (frame) {
        setConnected(true);
        console.log('Connected: ' + frame);
        stompClient.subscribe('/rest/add-room', function (room) {
            showRoom(JSON.parse(room.body));
        });
    });
}

function sendName() {
    stompClient.send("/app/add-room", {}, JSON.stringify({'maxPlayers': $("#maxPlayers").val()}));
}

function showRoom(room) {
      $("#greetings").append("<tr><td>" + room.id + " " + room.maxPlayers + "</td></tr>");
}


$(function () {
    $("form").on('submit', function (e) {
        e.preventDefault();
    });
    $( "#connect" ).click(function() { connect(); });
    $( "#disconnect" ).click(function() { disconnect(); });
    $( "#send" ).click(function() { sendName(); });
});
