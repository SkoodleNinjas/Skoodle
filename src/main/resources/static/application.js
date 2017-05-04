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
}

jQuery.ajax({
            url: "/rest/rooms",
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
    stompClient.send("/app/add-room", {}, JSON.stringify({'maxPlayers': $("#max-players").val()}));
}

function createBadge(players, maxPlayers) {
	if (players == null) {
	    players = 0;
    }

	var result = players + " of " + maxPlayers + " players";
	return result;
}

function getColor(players, maxPlayers) {
    var colors = ["cyan", "orange", "red"];
    var color = colors[0];
    var step = maxPlayers / 3;
    if (players != 0) {
        color = colors[Math.round(players / step) - 1];
    }
    return color
}


function createTableRow(room) {
    var result = "";

    result += "<li class=\"collection-item avatar\">"
    result += "<i class=\"material-icons circle ";
    result += getColor(room.players, room.maxPlayers);
    result += " \">lock_open</i>";
    result += "<span class=\"title\">";
    result += room.name;
    result += "</span>";
    result += "<p>";
    result += result += createBadge(room.players, room.maxPlayers);
    result += "<br>";
    result += room.numberOfRounds;
    result += " Rounds";
    result += "</p>";
    result += "<a href=\"#!\" class=\"secondary-content\"><i class=\"material-icons\">send</i></a>";
    result += "</li>";

	// var result = "<tr>";
    //
	// result += "<td>";
	// result += room.id;
	// result += "</td>";
    //
	// result += "<td>";
	// result += createBadge(room.players, room.maxPlayers);
	// result += "</td>";
    //
	// result += "<td>";
	// result += room.maxPlayers;
	// result += "</td>";
    //
	// result += "</tr>";
	return result;
}

function showRoom(room) {
      $("#greetings").append(createTableRow(room));
}


$(function () {
    $("form").on('submit', function (e) {
        e.preventDefault();
    });
    $( "#create-new-room" ).click(function() { sendName(); });
});
