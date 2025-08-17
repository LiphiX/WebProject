window.addEventListener("DOMContentLoaded", () => {
    $("select").change((event) => {
        var option = $("option:selected", event.target);

        var idOfSection = $(event.target).parent().parent().attr('data-id');
        var idOfPostman = option.val();

        fetch(`/Sections/Appointment/`, {
            method: 'POST',
            credentials: 'include',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                sectionId: idOfSection,
                postmanId: idOfPostman,
            })
        })
            .then(response => response.text())
            .then(data => {
                console.log("Запрос успешно принят.");
            })
            .catch(error => {
                console.log("Возникла ошибка!");
            })
    })

    downloadNumber = 1;
    $("#uploadButton").click((event) => {
        fetch(`/Sections/UploadData/${downloadNumber}`, {
            method: 'GET',
            credentials: 'include',
        })
            .then(response => response.text())
            .then(data => {
                downloadNumber++;
                $(".table tbody").append(data);
            })
            .catch(error => {
                console.log(error);
            })
    })
 });