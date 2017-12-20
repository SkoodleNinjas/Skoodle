var hub = $.connection.roomHub

var roomName = $("#room-name").text();

function checkAndStartGame() {
    if ($('#connected-users li').length >= 1) {
        startGame();
    }
}

hub.client.addChatMessage = function (user, msg) {
    $('#messages').append("<dt>" + user + "</dt> <dd>" + msg + "</dd>")
}

hub.client.userLeft = function (username) {
    users = $("#connected-users li");
    for (var i = 0; i < users.length; i++) {
        user = $(users[i])
        if (user.text() == username) {
            user.remove()
        }
    }
}

hub.client.addUser = function (user)  {
    $('#connected-users').append("<li>" + user + "</li>")
    checkAndStartGame();
}

$.connection.hub.start(function () {
    hub.server.joinRoom(roomName);
    checkAndStartGame();

    $("#send").click(function () {
        hub.server.sendMessage(roomName, $('#message-inp').val());
        $('#message-inp').val(' ')
    })

    $("#leave-room").click(function () {
        hub.server.leaveRoom(roomName)
    })
})

