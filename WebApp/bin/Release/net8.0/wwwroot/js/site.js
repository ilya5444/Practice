// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formatDate(date) {
    return `${("0" + date.getDate()).slice(-2)}.${("0" + (date.getMonth() + 1)).slice(-2)}.${date.getFullYear()}`
}