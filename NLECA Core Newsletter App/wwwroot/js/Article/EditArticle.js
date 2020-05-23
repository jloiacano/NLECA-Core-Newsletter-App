var EditArticle = EditArticle || {};

$(document).ready(function () {
    EditArticle.init();
});

EditArticle = {

    init: function () {
        CKEDITOR.replace('articleEditor');

        $('#ArticleEditCancelButton').click(function () {
            // TODO - J - go back
        });

        $('#ArticleDeleteButton').click(function () {
            EditArticle.DeleteArticle();
        });
    },

    DeleteArticle: function () {
        // TODO - J - Maybe add confirmation dialog.... ?
        $('#DeleteArticleForm').submit();
    }
}