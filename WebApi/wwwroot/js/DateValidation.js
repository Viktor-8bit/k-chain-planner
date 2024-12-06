






const form = document.getElementById('form_with_date');
form.addEventListener('submit', async function (event) {
    var error = $('#error')
    error.empty();
    var date_one = $("#date_one").val();
    var date_two = $("#date_two").val();
    var title = $("#title").val();
    var description = $("#description").val();
    var error_string = '';
    if (date_one > date_two) {
        event.preventDefault();
        error_string += 'Дата начала не должна быть больше даты конца 📅; <br/>'
    }
    if (title.trim() === '') {
        event.preventDefault();
        error_string += 'Название не может быть пустым 🔤; <br/>'
    }
    if (description.trim() === '') {
        event.preventDefault();
        error_string += 'Описание не может быть пустым 🔡;  <br/>'
    }
    if (!(error_string.trim() === '')) {
        error.append('<div class="alert alert-danger alert-dismissible fade show" role="alert">\n' +
            '    <strong>Похоже, что в полях есть ошибки:</strong> <br/> \n' + 
            error_string +
            '    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>\n' +
            '</div>')
    }
});


