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

function displayMessage(id) {
    $.ajax({
        url: "/Message/Get/" + id,
        type: "GET",
        dataType: "json",
        success: function (response) {
            $("#buttonSave").enabled = response.success;
            $(".modal-body").html(response.html);
        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
    });
}

function saveMessage(sender, event) {
    event.preventDefault();

    var form = $(sender).closest("form");

    $.ajax({
        url: "/Message/Edit",
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