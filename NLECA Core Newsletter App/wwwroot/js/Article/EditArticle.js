var EditArticle = EditArticle || {};

$(document).ready(function () {
    EditArticle.init();
});

EditArticle = {

    init: function () {
        CKEDITOR.replace('ArticleText');

        EditArticle.ShowCorrectArticleImageLocation($('#ArticleTypeDropdown').val());

        EditArticle.SetUpImageUploader();

        $('#ArticleTypeDropdown').change(function () {
            console.log($('#ArticleTypeDropdown option:selected').text());
            EditArticle.ShowCorrectArticleImageLocation($(this).val());
        });

        $('#ArticleEditCancelButton').click(function () {
            history.back();
        });

        $('#ArticleDeleteButton').click(function () {
            EditArticle.DeleteArticle();
        });

        $('.articleImage').click(function () {
            // TODO - J - Set up Images Controller
            alert('clicked image');
        });
    },

    HideAllImages: function () {
        $('.leftArticleImage').hide();
        $('.rightArticleImage').hide();
        $('.topArticleImage').hide();
        $('.bottomArticleImage').hide();

    },

    SetUpImageUploader: function () {

        var dragoverImageUrl = '../../Images/ArticleImages/draganddropimage.png';
        var dragoverImageAltUrl = '../../Images/ArticleImages/draganddropimagealt.png';

        $('.dragAndDropImageArea').on('dragover', function (e) {
            e.preventDefault();
            $('.dragAndDropImageArea').css("background-image", "url(" + dragoverImageAltUrl + ")");
        });

        $('.dragAndDropImageArea').on('dragleave', function () {
            $('.dragAndDropImageArea').css("background-image", "url(" + dragoverImageUrl + ")");
        });

        $('.dragAndDropImageArea').on('drop', function (e) {
            e.preventDefault();
            $('.dragAndDropImageArea').css("background-image", "url(" + dragoverImageUrl + ")");

            var imageToUpload = e.originalEvent.dataTransfer.files;
            document.querySelector('#ImageFileInput').files = imageToUpload;
            $('#UpdateArticleImageForm').submit();
        });
    },

    ShowCorrectArticleImageLocation: function (articleTypeInt) {
        switch (articleTypeInt) {
            case '1':
                //show left
                EditArticle.HideAllImages();
                $('.leftArticleImage').show()
                break;
            case '2':
                //show right
                EditArticle.HideAllImages();
                $('.rightArticleImage').show()
                break;
            case '3':
                //show top
                EditArticle.HideAllImages();
                $('.topArticleImage').show()
                break;
            case '4':
                //show bottom
                EditArticle.HideAllImages();
                $('.bottomArticleImage').show()
                break;
            default:
                //do no image;
                EditArticle.HideAllImages();
        }
    },

    DeleteArticle: function () {
        // TODO - J - Maybe add confirmation dialog.... ?
        $('#DeleteArticleForm').submit();
    }
}