// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.User-button').on('mouseover', function () {
    $('.User-button-dropdown', this).show();
}).on('mouseout', function (e) {
    if (!$(e.target).is('input')) {
        $('.User-button-dropdown', this).hide();
    }
});
