$("#content-container").on("click", "a", function (event) {
    loadBody(event);
});

$("#content-container").on("click", "button", function (event) {
    loadBody(event);
});


function loadBody(event) {
    event.stopPropagation();
    event.preventDefault();

    var url = event.target.href;
    $('#content-container').load(url);
}