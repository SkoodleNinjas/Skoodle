var gameHub = $.connection.gameHub
var hub = $.connection.roomHub

gameHub.client.roundStart = function () {
    console.log('round start')
    startGame();
}

gameHub.client.roundEnd = function () {
    console.log('round end')
    var round = parseInt($('#round-num').val())
    endRound(round)
}

gameHub.client.votingStart = function () {
    console.log('vote start')
    var roundNum = parseInt($('#round-num').val());
    var gameId = parseInt($('#game-id').val());
    startVotes(gameId, roundNum);
}

gameHub.client.votingEnd = function () {
    console.log('voting end')
    endVotes()
}

gameHub.client.updateStatuses = function () {
    console.log('update statuses')
    updateScores()
}

gameHub.client.gameFinished = function () {
    console.log('gameFinished');
    gameFinished();
}

/*
   fillMsgTemplate -> function which returns the message DOM object
   It uses the username and its First letter for UserImage for the moment
 */
function fillMsgTemplate(user, msg, time) {
    element = '<div class="chat-container">' +
        '<h1>' + user[0] + '</h1>' +
        '<p class="username">' + user + '</p>' +
        '<p>' + msg + '</p>' +
        '<span class="time-right">' + time + '</span>' +
        '</div >';
    return element
}


/*
fillUserTemplate -> takes username and fills it in the template for the users lists
*/
function fillUserTemplate(username) {
    return '<li><span class="glyphicon glyphicon-user"></span>' +
        username +
        '<span class="badge">0</span>' +
        '</li>';
}


/*
assigns to the the message sending socket so that the messages are recieved
*/
hub.client.addChatMessage = function (user, msg, time) {
    $('#messages').append(fillMsgTemplate(user, msg, time));
}

/*
assgins to the event for user leaving the room and if he leaves
his username is removed from the list of users
*/
hub.client.userLeft = function (username) {
    users = $("#connected-users li");
    for (var i = 0; i < users.length; i++) {
        user = $(users[i])
        if (user.text() == username) {
            user.remove()
        }
    }
}

/*
Assings to the event for adding users and if there is new user joined he
is displayed to the user
*/
hub.client.addUser = function (user) {
    $('#connected-users').append(fillUserTemplate(user))
}

/*
on user websocket create the user is joined to his room events
the chat input events are assigned for writting
*/
$.connection.hub.start(function () {
    var roomName = $("#room-name").text();
    var roomId = parseInt($('#room-id').val());
    var gameId = parseInt($('#game-id').val());

    hub.server.joinRoom(roomName);
    console.log('I am here?')

    gameHub.server.joinGame(roomId, gameId);

    $("#message-inp").keyup(function (event) {
        if (event.keyCode === 13) {
            hub.server.sendMessage(roomName, $('#message-inp').val());
            $('#message-inp').val(' ')
        }
    });

    $("#send").click(function () {
        hub.server.sendMessage(roomName, $('#message-inp').val());
        $('#message-inp').val(' ')
    })

    $("#leave-room").click(function () {
        hub.server.leaveRoom(roomName)
        gameHub.server.leaveGame(roomId)
    })
})
