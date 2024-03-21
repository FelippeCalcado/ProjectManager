// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function changeColorByID(id) {


    for (var i = 0; i < 4; i++) {
        if (document.getElementById(`job-id${i}`)[i].textContent == id) {
            document.getElementById(`job-id${i}`)[i].style.backgroundColor = "red";
            document.getElementById(`job-id${i}`)[i].classList.add('redBG');
        }
        console.log(document.getElementById(`job-id${i}`)[i].textContent)
        console.log(id)
    }

}

