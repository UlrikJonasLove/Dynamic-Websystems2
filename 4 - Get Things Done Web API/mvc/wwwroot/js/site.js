// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Post 
function sendData(ev, url) {
    var data = $(ev).serialize();
    $.ajax({
        type: "Post",
        url: '/Todos/' + url,
        data: data,
        success: function (res) {
            if (res) {
                history.back();
            }
        }
    });
    return false;
}

// Edit
function editData(ev, url) {
    var data = $(ev).serialize();
    $.ajax({
        type: "Put",
        url: '/Todos/' + url,
        data: data,
        success: function (res) {
            if (res) {
                history.back();
            }
        }
    });
    return false;
}

// Put
function putData(url) {
    $.ajax({
        type: "Put",
        url: '/Todos/' + url,
        success: function (res) {
            if (res) {
                location.reload();
            }
        }
    });
    return false;
}

// Delete
function deleteData(url) {
    $.ajax({
        type: "Delete",
        url: '/Todos/' + url,
        success: function (res) {
            if (res) {
                location.reload();
            }
        }
    });
    return false;
}

// Get search
function getSearch(ev, url) {
    var key = $("#search").val();
    location.href = "/SearchResult?key=" + key;
}