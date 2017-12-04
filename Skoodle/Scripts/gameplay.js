$('#wPaint').wPaint({
    menuOffsetLeft: -35,
    menuOffsetTop: -50
})

function grayScale(context, canvas) {
    var imgData = context.getImageData(0, 0, canvas.width, canvas.height);
        var pixels  = imgData.data;
        for (var i = 0, n = pixels.length; i < n; i += 4) {
            var grayscale = pixels[i] * .3 + pixels[i+1] * .59 + pixels[i+2] * .11;
            pixels[i  ] = grayscale;        // red
            pixels[i+1] = grayscale;        // green
            pixels[i+2] = grayscale;        // blue
            //pixels[i+3]              is alpha
    }
    //redraw the image in black & white
    context.putImageData(imgData, 0, 0);
}


function blur(imageObj, context, passes) {
    var i, x, y;
    passes = passes || 4;
    context.globalAlpha = 0.125;
    // Loop for each blur pass.
    for (i = 1; i <= passes; i++) {
        for (y = -1; y < 2; y++) {
            for (x = -1; x < 2; x++) {
                context.drawImage(imageObj, x, y);
            }
        }
    }
    context.globalAlpha = 1.0;
}


$("#grayScale").click(function () {
    canvas = $(".wPaint-canvas")[0];
    context = canvas.getContext('2d');
    grayScale(context, canvas);
})


$("#blur").click(function () {
    canvas = $(".wPaint-canvas")[0];
    context = canvas.getContext('2d');
    img = new Image();
    img.src = canvas.toDataURL();
    blur(img, context);
})