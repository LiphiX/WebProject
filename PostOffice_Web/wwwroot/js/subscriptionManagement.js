window.addEventListener("DOMContentLoaded", () => {

    console.log("test")

    $(".approve").click((event) => {
        console.log("Event");

        var recordId = $(event.target).parent().parent().attr('data-id');

        fetch(`/SubscriptionsManagement/Approve/${recordId}`, {
            method: 'GET',
            credentials: 'include',
            //data: numberOfDownloads,

        })
            .then(respone => respone.text())
            .then(data => {
                $(event.target).parent().parent().find('#state').text("Одобрено");
            })
            .catch((error) => {
                console.log(error);
            })
    })

    $(".decline").click((event) => {
        var recordId = $(event.target).parent().parent().attr('data-id');

        fetch(`/SubscriptionsManagement/Decline/${recordId}`, {
            method: 'GET',
            credentials: 'include',
            //data: numberOfDownloads,

        })
            .then(respone => respone.text())
            .then(data => {
                $(event.target).parent().parent().remove();
            })
            .catch((error) => {
                console.log(error);
            })
    })
});