var EditAlert = EditAlert || {};

$(document).ready(function () {
    EditAlert.init();
});

EditAlert = {

    init: function () {
        console.log('Alert Editor Initialized');

        $('#UpdateArticleButton').click(function () {
            $('#UpdateArticleForm').submit();
        })
    }

}