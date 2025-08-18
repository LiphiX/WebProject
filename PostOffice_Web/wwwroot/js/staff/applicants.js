window.addEventListener("DOMContentLoaded", () => {

    $(".dismiss").click((event) => {

        var recordId = $(event.target).parent().parent().attr('data-id');

        fetch(`/Staff/Accept/${recordId}`, {
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
})