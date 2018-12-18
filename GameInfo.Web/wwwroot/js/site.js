// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ChangeContent(elementId, callback) {
    var elements = document.getElementsByClassName('search-result');

    var i;
    for (i = 0; i < elements.length; i++) {
        elements[i].style.display = 'none';
    }

    id = elementId;
    var element = document.getElementById(id);
    if (element.style.display == 'none') {
        element.style.display = 'block';
    }

    callback();
}

function AwaitFunction(elementId) {
    ChangeContent(elementId, function() {
        console.log('function completed');
    });
}