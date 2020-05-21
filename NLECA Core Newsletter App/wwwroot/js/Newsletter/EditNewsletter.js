﻿var NewsletterEditor = NewsletterEditor || {};

$(document).ready(function () {
    NewsletterEditor.init();
});

NewsletterEditor = {

    currentlySorting: false,

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
        $('#sortableArticles')
            .sortable({
                axis: "y",
                placeholder: "articlePlaceHolder",
                opacity: 0.6
            });
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
    }

}
