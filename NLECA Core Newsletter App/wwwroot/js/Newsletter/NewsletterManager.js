var NewsletterManager = NewsletterManager || {};

$(document).ready(function () {
    NewsletterManager.init();
});

NewsletterManager = {

    init: function () {

        // DIALOGS
        $('#UnpublishNewsletterDialog').dialog({
            autoOpen: false
            , modal: true
            , hide: "slide"
            , show: "slide"
            , buttons: {
                YES: function () {
                    NewsletterManager.UnpublishNewsletter($(this).data('newsletterId'));
                }
                ,NO: function () { $(this).dialog("close"); }
            },
        });

        $('#PublishNewsletterDialog').dialog({
            autoOpen: false
            , modal: true
            , hide: "slide"
            , show: "slide"
            , buttons: {
                YES: function () {
                    NewsletterManager.PublishNewsletter($(this).data('newsletterId'));
                }
                , NO: function () { $(this).dialog("close"); }
            },
        });

        $('#DeleteNewsletterDialog').dialog({
            autoOpen: false
            , modal: true
            , hide: "slide"
            , show: "slide"
            , buttons: {
                "YES, DELETE IT": function () {
                    NewsletterManager.DeleteNewsletter($(this).data('newsletterId'));
                }
                , "NO": function () { $(this).dialog("close"); }
            },
        });

        // BUTTONS
        $('#AddNewsletterButton').click(function () {
            window.location.href = '/Newsletter/AddNewsletter/';
        });

        $('#EditNewsletterButton').click(function (e) {
            var newsletterId = $(e.target).closest('.individualNewletter').data('newsletterid');
            window.location.href = '/Newsletter/EditNewsletter/?newsletterId=' + newsletterId;
        });

        $('#DeleteNewsletterButton').click(function (e) {
            $("#DeleteNewsletterDialog")
                .data('newsletterId', $(e.target).closest('.individualNewletter').data('newsletterid'))
                .dialog('open');
        });

        $('#PublishNewsletterButton').click(function (e) {
            $("#PublishNewsletterDialog")
                .data('newsletterId', $(e.target).closest('.individualNewletter').data('newsletterid'))
                .dialog('open');
        });

        $('#UnpublishNewsletterButton').click(function (e) {
            $("#UnpublishNewsletterDialog")
                .data('newsletterId', $(e.target).closest('.individualNewletter').data('newsletterid'))
                .dialog('open');
        });
    },

    PublishNewsletter: function (newsletterId) {
        window.location.href = '/Newsletter/PublishNewsletter/?newsletterId=' + newsletterId;
    },

    UnpublishNewsletter: function (newsletterId) {
        window.location.href = '/Newsletter/UnpublishNewsletter/?newsletterId=' + newsletterId;
    },

    DeleteNewsletter: function (newsletterId) {
        window.location.href = '/Newsletter/RemoveNewsletter/?newsletterId=' + newsletterId;
    }
}