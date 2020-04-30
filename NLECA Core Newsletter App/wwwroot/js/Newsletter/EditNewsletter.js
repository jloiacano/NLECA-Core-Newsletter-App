var NewsletterEditor = NewsletterEditor || {};

$(document).ready(function () {
    NewsletterEditor.init();
});

NewsletterEditor = {
    init: function () {
        $('#memoEditButton').click(function () {
            $('#memoEditArea').toggle();
            $('#memoArea').toggle();
        })
        $('#displayDateEditButton').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        })
        $('#memoEditCancel').click(function () {
            $('#memoEditArea').toggle();
            $('#memoArea').toggle();
        })
        $('#displayDateEditCancel').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        })
    }
}