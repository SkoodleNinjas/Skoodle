/*
 * Initialize the wPaint library into the #wPaint div
 */
$('#wPaint').wPaint({
    menuOffsetLeft: -35,
    menuOffsetTop: -50,
    menuOrientation: 'horizontal'
})

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
$(".")