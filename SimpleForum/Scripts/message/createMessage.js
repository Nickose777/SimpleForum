function createMessage(sender, event) {
    event.preventDefault();

    var form = $(sender).closest("form");

    $.ajax({
        url: "/Message/Create",
        type: "POST",
        dataType: "json",
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                window.location.reload(true);
            }
            else {
                form.parent().html(response.html);
            }
        }
    });
}