function logoff(sender, event) {
    event.preventDefault();

    $(sender).closest('form').submit();
}