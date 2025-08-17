window.addEventListener("DOMContentLoaded", () => {
    var numberOfDownloads = 1;
    function uploadData(number) {
        fetch(`/Publications/UploadData/${numberOfDownloads}`, {
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

    $("#uploadButton").click((event) => {
        uploadData(numberOfDownloads);
    })

});