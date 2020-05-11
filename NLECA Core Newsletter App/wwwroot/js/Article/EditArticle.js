var EditArticle = EditArticle || {};

$(document).ready(function () {
    EditArticle.init();
});

EditArticle = {

    init: function () {
        var articleEditor = ClassicEditor
            .create(document.querySelector('#articleEditor'))
            .then(editor => {
                console.log(editor);
            })
            .catch(error => {
                console.error(error);
            });
    }
}