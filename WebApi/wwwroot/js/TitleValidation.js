





const form = document.getElementById('form_with_title');
form.addEventListener('submit', async function (event) {
    var error = $('#error')
    error.empty();
    var title = $("#title").val();
    if (title.trim() === '') {
        event.preventDefault();
        error.append('<div class="alert alert-danger alert-dismissible fade show" role="alert">\n' +
            '    <strong>Похоже, что в полях есть ошибки:</strong> <br/> \n' + 
            '    Название не может быть пустым 🔤\n' +
            '    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>\n' +
            '</div>')
    }
});