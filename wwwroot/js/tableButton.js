var buttons = document.getElementsByTagName("button");
for (var i = 0; i <= buttons.length; i += 1) {
    buttons[i].onclick = function (e) {
        if (this.value == "DELETE"){
            document.getElementById(this.name).remove();
            doPost("/Ticket/Delete/"+this.id);
        }else {
            alert("id " +
                this.id +
                " row-id " +
                this.name +
                " value " +
                this.value);
        }
    };

}
function doPost(url) {
    alert("Posting " + url );
    $.getJSON(url, function (data) {
        alert(data);
    });
}