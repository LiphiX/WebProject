window.addEventListener("DOMContentLoaded", () => {

    function uploadData(number) {
        console.log("error");
        fetch(`/SubscriptionManagement//${numberOfDownloads}`, {
            method: 'GET',
            credentials: 'include',
            //data: numberOfDownloads,

        })
            .then(respone => respone.text())
            .then(data => {
                $("#publications").append(data);
                numberOfDownloads++;
            })
            .catch((error) => {
                console.log(error);
            })
    }

    $("#approve").click((event) => {
        uploadData(numberOfDownloads);
    })

});