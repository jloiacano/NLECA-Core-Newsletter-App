var ArticleImageManager = ArticleImageManager || {};

$(document).ready(function () {
    ArticleImageManager.init();
});

ArticleImageManager = {
    init: function () {
        $('.removeArticleImage').click(function (e) {
            ArticleImageManager.removeArticleImage(e);
        });
        $('.deleteArticleImage').click(function (e) {
            ArticleImageManager.deleteArticleImage(e);
        });
        $('.useArticleImage').click(function (e) {
            ArticleImageManager.useArticleImage(e);
        });
    },

    useArticleImage: function (clicked) {
        var articleId = $('#ArticleId').val();
        var imageLocation = clicked.target.dataset.touse;
        window.location.href = '/ArticleImage/UseArticleImageInArticle/?articleId=' + articleId + '&imageLocation=' + imageLocation;
    },

    removeArticleImage: function (clicked) {
        var imageLocation = clicked.target.dataset.toremove;
        window.location.href = '/ArticleImage/RemoveArticleImageFromUse/?imageLocation=' + imageLocation;
    },

    deleteArticleImage: function (clicked) {
        var imageLocation = clicked.target.dataset.todelete;
        window.location.href = '/ArticleImage/DeleteArticleImage/?imageLocation=' + imageLocation;
    },
}