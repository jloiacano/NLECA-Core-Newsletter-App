var EditArticle = EditArticle || {};

$(document).ready(function () {
    EditArticle.init();
});

EditArticle = {

    init: function () {
        // This was for using CKEditor5, but it doesn't provide source edit..
        //var articleEditor = ClassicEditor
        //    .create(document.querySelector('#articleEditor'))
        //    .then(editor => {
        //        console.log(editor);
        //    })
        //    .catch(error => {
        //        console.error(error);
        //    });
        CKEDITOR.replace('articleEditor');
    }
}