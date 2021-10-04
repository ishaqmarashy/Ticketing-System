var buttons = document.getElementsByTagName("button");
for (var i = 0; i <= buttons.length; i += 1) {
    buttons[i].onclick = function (e) {
        if (this.value == "DELETE"){
            document.getElementById(this.name).remove();
            doPost("/Ticket/Delete/"+this.id);
        } else if (this.value == "UPDATE"){
            location.href = "/Ticket/UpdateAdmin/"+this.id;
        }
    };

}
function doPost(url) {
    $.getJSON(url, function (data) {});
}