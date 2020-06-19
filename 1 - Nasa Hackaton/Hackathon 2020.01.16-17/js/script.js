var link = "https://api.nasa.gov/mars-photos/api/v1/rovers/{-rover-}/photos?sol=1000&api_key=OOt7OF3QRddMBsrXKEqfacujjeCqG8oLmocfOVyV";

// Loading background image
var bg = "<div class='waiting' id='waiting'><img class='enter' src='./img/earth.gif'></div>";

// Card block
var card = "<div class='card' style='width:400px' id='cards'>" +
    "<img class='card-img-top' src='{img}' alt='Card image'>" +
    "<div class='card-body'>" +
    "<h4 class='card-title'>{name}</h4></div></div>";

// Default images width page start
$(document).ready(function() {
    getImages('curiosity');
});

// Function to switch image by key
function getImages(key) {

    // Set loadning background
    $("#row").html(bg);

    // Loadings cards
    for (var i = 0; i < 12; i++) {
        var row = $("#row");
        $(card.replace("{img}", "./img/moon.webp").replace("{name}", "Loading ...")).insertAfter("#waiting");
    }

    // Get json
    fetch(link.replace("{-rover-}", key))
        .then(response => response.json())
        .then(function(data) {
            // Set limit fro array
            var arr = data.photos.slice(0, 12);
            // Clean elemnt html first
            $("#row").html('');

            setTimeout(function() {
                // Loop images array
                for (var i = 0; i < arr.length; i++) {
                    $("#row").append(card.replace("{img}", arr[i].img_src).replace("{name}", arr[i].camera.full_name));
                 }
            }, 100)
        })
}