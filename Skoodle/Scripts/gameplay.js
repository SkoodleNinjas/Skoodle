var roomId = parseInt($('#room-id').val())
console.log($('#game-id').val())

if (!$('#game-id').val()) {
    $.ajax({
        type: "POST",
        url: "/Game/CreateGame",
        data: { "roomId": roomId },
        success: function (data) {
            $('#game-id').val(data['gameId']);
            $("#round-num").val(data['roundNum']);
        }
    })
}

/*
 * grayScale -> Function which gray scales the image from canvas
 * context -> the canvas context
 * canvas -> the html canvas in which the image is contained
 */
function grayScale(context, canvas) {
    // getting the image from the canvas
    var imgData = context.getImageData(0, 0, canvas.width, canvas.height);
    // getting the pixels from the image
    var pixels = imgData.data;
    // iterating through the pixels and gray scaling each of them
    for (var i = 0, n = pixels.length; i < n; i += 4) {
        // appling the formula for grayscale the pixel
        var grayscale = pixels[i] * .3 + pixels[i + 1] * .59 + pixels[i + 2] * .11;
        // adding the grascaled value to R, G, B so that it is gray
        pixels[i] = grayscale; // red
        pixels[i + 1] = grayscale; // green
        pixels[i + 2] = grayscale; // blue
    }
    // redrawing the image with the new pixel data
    context.putImageData(imgData, 0, 0);
}


/*
 * blur -> Function which blurs the image from canvas
 * imageObj -> the image from the canvas
 * context -> the canvas context in which you want to draw
 * passes -> the number of times you want the image to be blurred
 */
function blur(imageObj, context, passes) {
    var i, x, y;
    passes = passes || 4;
    // Change the opacity of the image to 1/8
    context.globalAlpha = 0.125;
    // iterate 8 times and add 8 images one over another with upto 2
    // x, y coordinate difference
    for (i = 1; i <= passes; i++) {
        for (y = -1; y < 2; y++) {
            for (x = -1; x < 2; x++) {
                context.drawImage(imageObj, x, y);
            }
        }
    }
    // return the opacity again to 1 so that the image doesn't look transparent.
    context.globalAlpha = 1.0;
}


/*
 * negative -> Function which makes the inside iamge of canvas negative
 * imageObj -> the image from the canvas
 * context  -> the canvas context in which you want to draw
 * canvas   -> the html canvas in which the image is contained
 */
function negative(imageObj, context, canvas) {
    var destX = 0;
    var destY = 0;
    // Draw the image which has been passed by reference
    // context.drawImage(imageObj, destX, destY);

    // get image data from the image
    imageData = context.getImageData(0, 0, canvas.width, canvas.height);
    // get the pixels matrix from the image
    pixels = imageData.data;

    // iterate over the pixels and change them to their negatives
    for (var i = 0; i < pixels.length; i += 4) {
        pixels[i] = 255 - pixels[i]; // red
        pixels[i + 1] = 255 - pixels[i + 1]; // green
        pixels[i + 2] = 255 - pixels[i + 2]; // blue
    }

    // redraw the image
    context.putImageData(imageData, 0, 0);
}


// this method will initilize the filter buttons and assign the events for each
function initFilters() {
    var buttonGrayScale = '<button id="grayScale">Gray scale!</button>'
    var buttonBlur = '<button id="blur">Blur!</button>'
    var buttonNegative = '<button id="negative">Negative!</button>'

    $('#canvas-container').append(buttonGrayScale)
    $('#canvas-container').append(buttonBlur)
    $('#canvas-container').append(buttonNegative)

    // Assign to #grayScale button to call the gray scale function
    $("#grayScale").click(function () {
        // get the canvas
        canvas = $(".wPaint-canvas")[0];
        // get the context from the canvas
        context = canvas.getContext('2d');
        grayScale(context, canvas);
    })


    // Assign to #blur  button to call the blur function
    $("#blur").click(function () {
        // get canvas
        canvas = $(".wPaint-canvas")[0];
        // get the canvas context
        context = canvas.getContext('2d');
        // create and assign the image to the canvas image
        img = new Image();
        img.src = canvas.toDataURL();
        blur(img, context);
    })

    // Assign to #negative button to call the negative function
    $("#negative").click(function () {
        // get canvas
        canvas = $(".wPaint-canvas")[0];
        // get the canvas context
        context = canvas.getContext('2d');
        // create and assign the image to the canvas image
        img = new Image();
        img.src = canvas.toDataURL();

        negative(img, context, canvas);
    })
}

// After the library has initialized we change its default iamges
function setUXIcons() {
    $("div[title='Bucket']").css("background-image", "url(/Content/imgs/bucketIcon.png)");
    $("div[title='Pencil']").css("background-image", "url(/Content/imgs/pencilIcon.png)");
    $("div[title='Eraser']").css("background-image", "url(/Content/imgs/eraserIcon.png)");
    $("div[title='Text']").css("background-image", "url(/Content/imgs/textIcon.png)");
    $("div[title='Line']").css("background-image", "url(/Content/imgs/lineIcon.png)");
    $("div[title='Ellipse']").css("background-image", "url(/Content/imgs/elipseIcon.png)");
    $("div[title='Rectangle']").css("background-image", "url(/Content/imgs/rectangleIcon.png)");
    $("div[title='Clear']").css("background-image", "url(/Content/imgs/XIcon.png)");
    $("div[title='Redo']").css("background-image", "url(/Content/imgs/redoIcon.png)");
    $("div[title='Undo']").css("background-image", "url(/Content/imgs/undoIcon.png)");

}


function imageVotingTemplate(imageSrc, userId) {
   return "<label>" + '<input type="radio" name="fb" value="' + userId + '" />'+
        '<img src="data:image/png;base64,'+ imageSrc + '">' + "</label>"
}

var timerIds = new Array();

function resetBoard() {
    $('#canvas-container').empty();
    var drawingHtml = '<div>' +
        '<h1 id="timer"></h1>' +
        '<div class="progress" id="progress-bar" style="display:none">' +
        '<div id="timer-progress" class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 0%">' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div id="wPaint" style="position:relative; width:100%; height:300px; background-color:#ffffff;">' +
        '</div>';
    $('#canvas-container').html(drawingHtml);
}

function updateUserScores(data) {
    users = $('#connected-users > li > .badge');
    for (i = 0; i < users.length; i++) {
        jqUser = $(users[i]);
        if (jqUser.parent().html().indexOf(data[i]['Item2']) !== -1) {
            score = parseInt(jqUser.html());
            jqUser.html(score + 1);
        }
    }
}

function putImages(data) {
    $('#canvas-container').empty();
    console.log(data);
    console.log(data.length);
    for (i = 0; i < data.length; i++) {
        var imageSrc = data[i]['Item1'];
        var userId = data[i]['Item2'];
        templ = imageVotingTemplate(imageSrc, userId);
        $('#canvas-container').append(templ)
    }

    timerId = setTimeout(function () {
        inputs = $('#canvas-container input[type="radio"]')
        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                $.ajax({
                    type: "POST",
                    url: "/Game/VoteForImage",
                    data: { "drawingId": inputs[i].value },
                    async: false
                });
                break;
            }
        }
    }, 30 * 1000); // aka as 0.5 min

    timerIds.push(timerId);
    var gameId = parseInt($('#game-id').val());
    var roundNum = parseInt($('#round-num').val());
    timerId = setTimeout(function () {
        inputs = $('#canvas-container input[type="radio"]')
        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                $.ajax({
                    type: "GET",
                    url: "/Game/GetUserScores/?gameId=" + gameId + "&roundNum=" + roundNum,
                    success: function (data) {
                        updateUserScores(data);
                        $('#round-num').val(roundNum + 1);
                        resetBoard();
                        startRound(roundNum + 1);
                    }
                });
                break;
            }
        }
    }, 30.5 * 1000); // aka as 1/2 second after the voting
    timerIds.push(timerId);

}

function cleanTimeoutsForGame() {
    for (var i = 0; i < timerIds.length; i++) {
        clearTimeout(timerIds[i])
    }
}

$('#leave-room').click(function () {
    cleanTimeoutsForGame();
})

$(window).bind('statechange', function () {
    cleanTimeoutsForGame();
    $('#leave-room').trigger("click");
});


function endRound(roundNum) {
    var gameId = parseInt($('#game-id').val())
    var image = $('.wPaint-canvas')[0].toDataURL("image/png");
    image = image.replace('data:image/png;base64,', '');
    $.ajax({
        type: "POST",
        url: '/Game/FinishRound',
        data: { 'gameId': gameId, 'roundNum': roundNum, 'image': image }
    });
    setTimeout(function () {
        $.ajax({
            type: "POST",
            url: '/Game/EndRoundScreen',
            data: { 'gameId': gameId, 'roundNum': roundNum, 'image': image },
            success: function (data) {
                putImages(data)
            }
        });
    }, 500);
}


function startGame() {
    var round = parseInt($('#round-num').val())
    startRound(round);
}


function startRound(roundNum) {
    var secondsBeforeStart = 5; // Time is in seconds
    var timeForGame = 60; // Time is in seconds

    var secondsTillGame = secondsBeforeStart;

    for (var i = 1; i <= secondsBeforeStart; i++) {
        timerIds.push(setTimeout(function () {
            $('#timer').text(secondsTillGame + 's');
            $('#timer').fadeIn();
            $('#timer').fadeOut();
            secondsTillGame--;
        }, 1000 * i));
    }

    var playTimer = setTimeout(function () {
        /*
         * Initialize the wPaint library into the #wPaint div
         */
        $('#wPaint').wPaint({
            menuOffsetLeft: -35,
            menuOffsetTop: -25,
            menuOrientation: 'horizontal'
        })

        initFilters()
        setUXIcons()
        var leftGameTime = timeForGame;
        $('#progress-bar').show();

        // 25 times the time so that we can have 25fps for smooth animation
        for (var i = 1; i <= timeForGame * 25; i++) {
            timerIds.push(setTimeout(function () {
                leftGameTime--;
                $('#timer-progress').width(((timeForGame - leftGameTime) / timeForGame) * 4 + '%')
            }, 40 * i));
        }

        timerIds.push(setTimeout(function () {
            endRound(roundNum);
        }, (timeForGame + 1) * 1000));

    }, (secondsBeforeStart + 1) * 1000);

    timerIds.push(playTimer);
}