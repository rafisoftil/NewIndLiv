// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ShowToast(status, message) {
    const toast = document.getElementById('toast');
    toast.classList.remove('d-none');
    if (status == 'success') {
        status = 'toast_success';
    }
    else {
        status = 'toast_failed'
    }
    toast.classList.add(status);
    toast.textContent = message;
    setTimeout(() => {
        toast.classList.add('d-none');
        toast.classList.remove('toast_failed');
    }, 3000);
}

function ShowSpinner() {
    $('#spinner').show();
}

function HideSpinner() {
    $('#spinner').hide();
}