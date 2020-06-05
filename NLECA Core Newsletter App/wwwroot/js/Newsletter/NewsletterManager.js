var NewsletterManager = NewsletterManager || {};

$(document).ready(function () {
    NewsletterManager.init();
});

NewsletterManager = {
    init: function () {
        $('#AddNewsletterButton').click(function () {
            window.location.href = '/Newsletter/AddNewsletter/';
        });

        $('#EditNewsletterButton').click(function (e) {
            var newsletterId = $(e.target).closest('.individualNewletter').data('newsletterid');
            window.location.href = '/Newsletter/EditNewsletter/?newsletterId=' + newsletterId;
        });

        $('#DeleteNewsletterButton').click(function (e) {
            var newsletterId = $(e.target).closest('.individualNewletter').data('newsletterid');
            window.location.href = '/Newsletter/RemoveNewsletter/?newsletterId=' + newsletterId;
        });

        $('#PublishNewsletterButton').click(function (e) {
            var newsletterId = $(e.target).closest('.individualNewletter').data('newsletterid');
            window.location.href = '/Newsletter/PublishNewsletter/?newsletterId=' + newsletterId;
        });

        $('#UnpublishNewsletterButton').click(function (e) {
            var newsletterId = $(e.target).closest('.individualNewletter').data('newsletterid');
            window.location.href = '/Newsletter/UnpublishNewsletter/?newsletterId=' + newsletterId;
        });
    },
}