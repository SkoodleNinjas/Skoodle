function loadImgBg() {
    // internal function for displaying background images modal
    // where images is an array of images (base64 or url path)
    // NOTE: that if you can't see the bg image changing it's probably
    // becasue the foregroud image is not transparent.
    this._showFileModal('bg', images);
}
function loadImgFg() {
    // internal function for displaying foreground images modal
    // where images is an array of images (base64 or url path)
    this._showFileModal('fg', images);
}

$('#wPaint').wPaint({
    menuOffsetLeft: -35,
    menuOffsetTop: -50,
    loadimgBg: loadImgBg,
    loadImgFg: loadImgFg
})

function grayScale(context, canvas) {
    var imgData = context.getImageData(0, 0, canvas.width, canvas.height);
        var pixels  = imgData.data;
        for (var i = 0, n = pixels.length; i < n; i += 4) {
            var grayscale = pixels[i] * .3 + pixels[i + 1] * .59 + pixels[i + 2] * .11;
            pixels[i] = grayscale; // red
            pixels[i + 1] = grayscale; // green
            pixels[i + 2] = grayscale; // blue
    }
    context.putImageData(imgData, 0, 0);
}


function blur(imageObj, context, passes) {
    var i, x, y;
    passes = passes || 4;
    context.globalAlpha = 0.125;
    for (i = 1; i <= passes; i++) {
        for (y = -1; y < 2; y++) {
            for (x = -1; x < 2; x++) {
                context.drawImage(imageObj, x, y);
            }
        }
    }
    context.globalAlpha = 1.0;
}

function negative(imageObj, context, canvas) {
    var destX = 0;
    var destY = 0;

    context.drawImage(imageObj, destX, destY);

    imageData = context.getImageData(0, 0, canvas.width, canvas.height);
    pixels = imageData.data;
    for (var i = 0; i < pixels.length; i += 4) {
        pixels[i] = 255 - pixels[i]; // red
        pixels[i + 1] = 255 - pixels[i + 1]; // green
        pixels[i + 2] = 255 - pixels[i + 2]; // blue
    }
    context.putImageData(imageData, 0, 0);
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

$("#negative").click(function () {
    canvas = $(".wPaint-canvas")[0];
    context = canvas.getContext('2d');
    img = new Image();
    img.src = canvas.toDataURL();

    negative(img, context, canvas);
})