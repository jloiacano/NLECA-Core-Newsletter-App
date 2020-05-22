var NewsletterEditor = NewsletterEditor || {};

$(document).ready(function () {
    NewsletterEditor.init();
});

NewsletterEditor = {

    currentlySorting: false,
    sortingPrechangeIndex: 0,
    sortingPostchangeIndex: 0,

    init: function () {
        $('#memoEditButton').click(function () {
            $('#memoEditArea').toggle();
            $('#memoArea').toggle();
        });

        $('input[name="memo"]').keyup(function () {
            if ($('input[name="memo"]').val() != $('input[name="oldMemo"]').val()) {
                $('#memoAndDisplayDateSaveChanges').show();
            }
            else {
                $('#memoAndDisplayDateSaveChanges').hide();
            }
        });

        $('#memoEditCancel').click(function () {
            $('#memoEditArea').toggle();
            $('#memoArea').toggle();
        });

        $('#displayDateEditButton').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        });

        $('input[name="displayDate"]').change(function () {
            if ($('input[name="displayDate"]').val() != $('input[name="oldDisplayDate"]').val()) {
                $('#memoAndDisplayDateSaveChanges').show();
            }
            else {
                $('#memoAndDisplayDateSaveChanges').hide();
            }
        });

        $('#displayDateEditCancel').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        });

        $('.changeArticleOrderButton').click(function () {
            NewsletterEditor.setupSortableArticles();
        });

        $('.saveArticleOrderButton').click(function () {
            NewsletterEditor.saveArticleOrder();
        });
    },

    setupSortableArticles: function () {
        $('.changeArticleOrderButton').hide();
        $('.saveArticleOrderButton').show();
        $('.changeArticleOrderDirectionsSpan').show();
        $('#TableOfContents').addClass('sorting');

        $('#sortableArticles')
            .sortable({
                axis: "y",
                placeholder: "articlePlaceHolder",
                opacity: 0.6,
                start: function (event, ui) {
                    NewsletterEditor.sortingPrechangeIndex = ui.item.index();
                },
                stop: function (event, ui) {
                    NewsletterEditor.sortingPostchangeIndex = ui.item.index();
                    NewsletterEditor.changeOtherSortableArticleOrder('#sortableTableOfContents')
                }
            }).disableSelection();
        $('#sortableTableOfContents')
            .sortable({
                axis: "y",
                placeholder: "tableOfContentsPlaceHolder",
                opacity: 0.6,
                start: function (event, ui) {
                    NewsletterEditor.sortingPrechangeIndex = ui.item.index();
                },
                stop: function (event, ui) {
                    NewsletterEditor.sortingPostchangeIndex = ui.item.index();
                    NewsletterEditor.changeOtherSortableArticleOrder('#sortableArticles')
                }
            }).disableSelection();
        NewsletterEditor.currentlySorting = true;
    },

    saveArticleOrder: function () {
        var newsletterId = $('input[name="newsletterId"]').val();
        //get the new article order;
        var newArticleOrder = $("#sortableArticles").sortable("toArray");
        var articleIds = NewsletterEditor.getNewOrderOfArticleIds(newArticleOrder);
        if (NewsletterEditor.articleOrderChanged(newArticleOrder)) {
            $.ajax({
                url: '/Article/UpdateArticleOrder',
                type: 'POST',
                data: {
                    'articleIds': articleIds,
                    'newArticleOrder': newArticleOrder
                },
                dataType: 'json',
                success: function (response) {
                    if (response.success == false) {
                        alert(response.responseText);
                    }
                },
                error: function (request, error) {
                    alert("Request: " + JSON.stringify(request));
                }
            });
        }
        $('#sortableArticles').sortable("destroy");
        $('.changeArticleOrderButton').show();
        $('.saveArticleOrderButton').hide();
        $('.changeArticleOrderDirectionsSpan').hide();
        $('#TableOfContents').removeClass('sorting');
    },

    articleOrderChanged: function (newOrder) {
        for (var i = 0; i < newOrder.length; i++) {
            if (i + 1 != newOrder[i]) {
                return true;
            }
        }
        return false;
    },

    getNewOrderOfArticleIds: function (updatedArticleSequence) {
        var newOrderOfArticleIds = [];
        for (var i = 0; i < updatedArticleSequence.length; i++) {
            var updatedSequenceId = '#' + updatedArticleSequence[i]
            var articleId = $(updatedSequenceId).data('articleid');
            newOrderOfArticleIds.push(articleId);
        }
        return newOrderOfArticleIds;
    },

    changeOtherSortableArticleOrder: function (listToChange) {
        var pre = NewsletterEditor.sortingPrechangeIndex;
        var post = NewsletterEditor.sortingPostchangeIndex;
        if (post > pre) {
            $(listToChange + ' .ui-sortable-handle:eq(' + pre + ')').insertAfter(listToChange + ' .ui-sortable-handle:eq(' + post + ')');
        } else {
            $(listToChange + ' .ui-sortable-handle:eq(' + pre + ')').insertBefore(listToChange + ' .ui-sortable-handle:eq(' + post + ')');
        }
        NewsletterEditor.sortingPrechangeIndex = 0;
        NewsletterEditor.sortingPostchangeIndex = 0;
    }
}
