var buttons = document.getElementsByTagName("button");
for (var i = 0; i <= buttons.length; i += 1) {
    buttons[i].onclick = function (e) {
        if (this.value == "DELETE"){
            if (this.id == "admin")
                alert("Cannot Delete Admins");
            else {
                doPost("/Account/Delete/" + this.id);
                document.getElementById(this.name).remove();}
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