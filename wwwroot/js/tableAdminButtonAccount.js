var buttons = document.getElementsByTagName("button");
for (var i = 0; i <= buttons.length; i += 1) {
    buttons[i].onclick = function (e) {
        if (this.value == "DELETE"){
            if (this.id == "admin")
                alert("Cannot Delete Admins");
            else {
                document.getElementById(this.name).remove();
                doPost("/Account/Delete/" + this.id);
                }
        }
        /*else if (this.value == "UPDATE") {
            location.href = "/Account/UpdateAdmin/"+this.id;
        }*/
    };

}
function doPost(url) {
    $.getJSON(url, function (data) {
    });
}