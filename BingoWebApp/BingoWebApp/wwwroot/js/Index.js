/*function toggleMenu() {
    console.log("i am clicked");
    var menu = document.getElementById("myMenu");
    menu.style.display = (menu.style.display === "block") ? "none" : "block";
    // Stop event propagation to prevent it from reaching the window.onclick
    event.stopPropagation();
}

// Close the menu if the user clicks outside of it
window.onclick = function (event) {
    var menu = document.getElementById("myMenu");
    if (event.target !== menu && !menu.contains(event.target)) {
        menu.style.display = "none";
    }
};*/

function toggleSideMenu() {
    const userActions = document.querySelector('.user-actions');
    userActions.classList.toggle('show-menu');
    event.stopPropagation();
};

document.addEventListener('click', function (event) {
    const userActions = document.querySelector('.user-actions');

    if (!event.target.closest('.user-actions')) {
        userActions.classList.remove('show-menu');
    }
});

function submitSearchForm() {
    var searchTerm = $("#searchTerm").val();

    $.ajax({
        type: "POST",
        url: "/product/GetSearchProducts",
        data: { searchTerm: searchTerm },
        success: function (result) {
            // Handle the result as needed
            console.log(result);
        },
        error: function (error) {
            console.error(error);
        }
    });
}
/*
$(document).ready(function () {
    // Attach a click event to the search button
    $(".search-button").click(function () {
        // Get the search term from the input field
        var searchTerm = $(".search-bar-input").val();

        // Make an asynchronous request to GetSearchProducts
        $.ajax({
            type: "GET",
            url: "/Product/GetSearchProducts",
            data: { searchTerm: searchTerm },
            success: function (result) {
                // Update the page with the result (assuming the result is HTML)
                console.log(result);
            },
            error: function (error) {
                console.error(error);
            }
        });
    });
}); */

/*
document.addEventListener('click', function (event) {
    const sideMenu = document.getElementById('sideMenu');
    const userActions = document.querySelector('.user-actions');

    if (!userActions.contains(event.target) && !sideMenu.contains(event.target)) {
        userActions.classList.remove('show-menu');
    }
});

// Prevent the click inside the menu from triggering the global click event
document.getElementById('sideMenu').addEventListener('click', function (event) {
    event.stopPropagation();
})*/