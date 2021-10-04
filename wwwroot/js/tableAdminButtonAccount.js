var buttons = document.getElementsByTagName("button");
for (var i = 0; i <= buttons.length; i += 1) {
    buttons[i].onclick = function (e) {
        if (this.value == "DELETE"){
            document.getElementById(this.name).remove();
            if (this.id != "admin");
            doPost("/Account/Delete/"+this.id);
        }else {
            location.href = "/Account/UpdateAdmin/"+this.id;
        }
    };

}
function doPost(url) {
    $.getJSON(url, function (data) {
    });
}