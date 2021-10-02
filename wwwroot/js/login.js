var password = document.getElementById("Password"), username = document.getElementById("Username");

var form = document.getElementById("Form");



form.addEventListener('submit', event => {
    if (username.value.length == 0 || password.value.length == 0) {
        password.setCustomValidity("Fill in Both Fields");
        event.preventDefault();
    } else {
        password.setCustomValidity('');
    }
});
form.addEventListener('change', event => {
    event.preventDefault();
    if (username.value.length == 0 || password.value.length == 0) {
        password.setCustomValidity("Fill in Both Fields");
        event.preventDefault();
    } else {
        password.setCustomValidity('');
    }
});

form.onsubmit=reportValidity();