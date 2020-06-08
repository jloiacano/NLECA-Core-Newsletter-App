var EditNewsletter = EditNewsletter || {};

$(document).ready(function () {
    EditNewsletter.init();
});

EditNewsletter = {

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
                $('#memoSaveChanges').show();
            }
            else {
                $('#memoSaveChanges').hide();
            }
        });

        $('#memoEditCancel').click(function () {
            $('input[name="memo"]').val($('input[name="oldMemo"]').val())
            $('#memoSaveChanges').hide();
            $('#memoEditArea').toggle();
            $('#memoArea').toggle();
        });

        $('#displayDateEditButton').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        });

        $('.newsletterDate').click(function () {
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        });

        $('input[name="displayDate"]').change(function () {
            if ($('input[name="displayDate"]').val() != $('input[name="oldDisplayDate"]').val()) {
                $('#displayDateSaveChanges').show();
            }
            else {
                $('#displayDateSaveChanges').hide();
            }
        });

        $('#displayDateEditCancel').click(function () {
            $('input[name="displayDate"]').val($('input[name="oldDisplayDate"]').val())
            $('#displayDateSaveChanges').hide();
            $('#displayDateEditArea').toggle();
            $('#displayDateArea').toggle();
        });

        $('.changeArticleOrderButton').click(function () {
            EditNewsletter.setupSortableArticles();
        });

        $('.saveArticleOrderButton').click(function () {
            EditNewsletter.saveArticleOrder();
        });

        $('.articleContent').click(function (x) {
            EditNewsletter.editArticle(this);
        });

        $('.addArticleButton').click(function () {
            $('#AddArticleForm').submit();
        });

        $('.eventArea').click(function () {
            $('.eventDateRangeEditArea').show();
        });

        EditNewsletter.SetUpEventDateRangeInputs();

        $('.saveEventDateRangeButton').click(function () {
            EditNewsletter.UpdateEventDates();
        });

        $('#ManageEventsButton').click(function () {
            window.location.href = '/Event/EventManager/';
        });
    },

    setupSortableArticles: function () {
        $('.changeArticleOrderButton').hide();
        $('.saveArticleOrderButton').show();
        $('#TableOfContents').addClass('sorting');

        $('#Articles')
            .sortable({
                axis: "y",
                placeholder: "articlePlaceHolder",
                opacity: 0.6,
                start: function (event, ui) {
                    EditNewsletter.sortingPrechangeIndex = ui.item.index();
                },
                stop: function (event, ui) {
                    EditNewsletter.sortingPostchangeIndex = ui.item.index();
                    EditNewsletter.changeOtherSortableArticleOrder('#sortableTableOfContents')
                }
            }).disableSelection();
        $('#sortableTableOfContents')
            .sortable({
                axis: "y",
                placeholder: "tableOfContentsPlaceHolder",
                opacity: 0.6,
                start: function (event, ui) {
                    EditNewsletter.sortingPrechangeIndex = ui.item.index();
                },
                stop: function (event, ui) {
                    EditNewsletter.sortingPostchangeIndex = ui.item.index();
                    EditNewsletter.changeOtherSortableArticleOrder('#Articles')
                }
            }).disableSelection();
        EditNewsletter.currentlySorting = true;
    },

    saveArticleOrder: function () {
        //get the new article order;
        var newArticleOrder = $("#Articles").sortable("toArray");
        var articleIds = EditNewsletter.getNewOrderOfArticleIds(newArticleOrder);
        if (EditNewsletter.articleOrderChanged(newArticleOrder)) {
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
        $('#Articles').sortable("destroy");
        $('.changeArticleOrderButton').show();
        $('.saveArticleOrderButton').hide();
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
        var pre = EditNewsletter.sortingPrechangeIndex;
        var post = EditNewsletter.sortingPostchangeIndex;
        if (post > pre) {
            $(listToChange + ' .ui-sortable-handle:eq(' + pre + ')').insertAfter(listToChange + ' .ui-sortable-handle:eq(' + post + ')');
        } else {
            $(listToChange + ' .ui-sortable-handle:eq(' + pre + ')').insertBefore(listToChange + ' .ui-sortable-handle:eq(' + post + ')');
        }
        EditNewsletter.sortingPrechangeIndex = 0;
        EditNewsletter.sortingPostchangeIndex = 0;
    },

    editArticle: function (articleDiv) {
        var articleId = $(articleDiv).data('articleid');
        $('#EditArticleFormInput').val(articleId);
        $('#EditArticleForm').submit();
    },

    SetUpEventDateRangeInputs: function () {

        $('#EventDateRangeStartInput').change(function () {
            var changedDateTime = new Date().FromHTMLInputValue($(this).val()),
                dateTimeToCheck = new Date().FromHTMLInputValue($('#EventDateRangeEndInput').val()),
                comparableChangedDate = new Date(changedDateTime).stripTime(),
                comparableDateToCheck = new Date(dateTimeToCheck).stripTime();

            if (comparableChangedDate >= comparableDateToCheck) {
                var differenceOfDays = changedDateTime.getDifferenceOfDays(dateTimeToCheck);
                var newEventDateEnd = dateTimeToCheck.addDays(differenceOfDays + 1).toDateHTMLInputValue();
                $('#EventDateRangeEndInput').val(newEventDateEnd);
            }
        });
        $('#EventDateRangeEndInput').change(function () {
            var changedDateTime = new Date().FromHTMLInputValue($(this).val()),
                dateTimeToCheck = new Date().FromHTMLInputValue($('#EventDateRangeStartInput').val()),
                comparableChangedDate = new Date(changedDateTime).stripTime(),
                comparableDateToCheck = new Date(dateTimeToCheck).stripTime();

            if (comparableChangedDate <= comparableDateToCheck) {
                var differenceOfDays = changedDateTime.getDifferenceOfDays(dateTimeToCheck);
                var newEventDate = dateTimeToCheck.addDays(-differenceOfDays - 1).toDateHTMLInputValue();
                $('#EventDateRangeStartInput').val(newEventDate);
            }
        });
    },

    UpdateEventDates: function () {
        var start = $('#EventDateRangeStartInput').val(),
            end = $('#EventDateRangeEndInput').val(),
            newsletterId = $("input[name='newsletterId']").val(),
            updateDateRangeLocation = '/Newsletter/UpdateNewsletterDateRange/?'
                + 'start=' + start
                + '&end=' + end
                + '&newsletterId=' + newsletterId;
        window.location.href = updateDateRangeLocation;
    }
}
