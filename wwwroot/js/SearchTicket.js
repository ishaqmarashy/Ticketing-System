var SearchTicket = document.getElementById("SearchTicket");

var form = document.getElementById("Form");

form.addEventListener('submit', event => {
    if (SearchTicket.value.length == 0) {
        SearchTicket.setCustomValidity("Fill in all Fields");
        event.preventDefault();
    } else {
        SearchTicket.setCustomValidity('');
    }
});
form.addEventListener('change', event => {
    event.preventDefault();
    if (SearchTicket.value.length == 0 ) {
        SearchTicket.setCustomValidity("Fill in all Fields");
        event.preventDefault();
    } else {
        SearchTicket.setCustomValidity('');
    }
});

