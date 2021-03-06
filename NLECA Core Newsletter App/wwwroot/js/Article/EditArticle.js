﻿var EditArticle = EditArticle || {};

$(document).ready(function () {
    EditArticle.init();
});

EditArticle = {
    currentArticleImageUrl: "",

    originalRedirect: "",
    originalArticleType: -1,
    originalArticleTableOfContentsText: "",
    originalArticleTitle: "",
    originalArticleText: "",

    init: function () {
        // DIALOGS
        $('#DeleteArticleDialog').dialog({
            autoOpen: false
            , modal: true
            , hide: "slide"
            , show: "slide"
            , buttons: {
                "YES, DELETE IT": function () {
                    var newsletterId = $(this).data('newsletterId');
                    var articleId = $(this).data('articleId');
                    EditArticle.DeleteArticle(newsletterId, articleId);
                }
                , "NO": function () { $(this).dialog("close"); }
            },
        });

        // CKEDITOR
        CKEDITOR.replace('ArticleText');

        // ORIGINAL VALUES
        EditArticle.originalRedirect = $('#redirect').val();
        EditArticle.originalArticleType = $('#ArticleTypeDropdown').val();
        EditArticle.originalArticleTableOfContentsText = $('.articleTableOfContentsText').val();
        EditArticle.originalArticleTitle = $('.articleTitle').val();
        EditArticle.originalArticleText = $('.articleText').val();

        if ($('#articleImageFileLocation').val() != '') {
            currentArticleImageUrl = $('#articleImageFileLocation').val();
        }
        else {
            currentArticleImageUrl = '../../Images/ArticleImages/draganddropimage.png';
        }

        EditArticle.ShowCorrectArticleImageLocation($('#ArticleTypeDropdown').val());

        EditArticle.SetUpImageUploader();

        $('#ArticleTypeDropdown').change(function () {
            console.log($('#ArticleTypeDropdown option:selected').text());
            EditArticle.ShowCorrectArticleImageLocation($(this).val());
        });

        $('#ArticleEditCancelButton').click(function () {
            history.back();
        });

        $('#ArticleDeleteButton').click(function (e) {
            var newsletterId = $(e.target).data('newsletterid');
            var articleId = $(e.target).data('articleid');
            $('#DeleteArticleDialog')
                .data('newsletterId', newsletterId)
                .data('articleId', articleId)
                .dialog('open');
        });

        $('.dragAndDropImageArea').click(function () {
            var articleId = $('#ModelArticleId').val();
            if (EditArticle.CheckForUnsavedChanges()) {
                $('#ArticleUpdater').submit();
            }
            else {
                window.location.href = '/ArticleImage/ArticleImageManager/?articleId=' + articleId;
            }
            
        });
    },

    HideAllImages: function () {
        $('.leftArticleImage').hide();
        $('.rightArticleImage').hide();
        $('.topArticleImage').hide();
        $('.bottomArticleImage').hide();

    },

    SetUpImageUploader: function () {

        var dragoverImageAltUrl = '../../Images/ArticleImages/draganddropimagealt.png';

        $('.dragAndDropImageArea').bind({
            dragover: function (e) {
                e.preventDefault();
                $('.dragAndDropImage').attr("src", dragoverImageAltUrl);
            },
            dragleave: function () {
                $('.dragAndDropImage').attr("src", currentArticleImageUrl);
            },
            drop: function (e) {
                e.preventDefault();
                $('.dragAndDropImage').attr("src", currentArticleImageUrl);
                var imageToUpload = e.originalEvent.dataTransfer.files;
                document.querySelector('#ImageFileInput').files = imageToUpload;
                EditArticle.CheckIfFileExists(e);
            },
        })
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

    CheckForUnsavedChanges: function () {
        if ($('#ArticleTypeDropdown').val() != EditArticle.originalArticleType
            || $('#ArticleTableOfContentsText').val() != EditArticle.originalArticleTableOfContentsText
            || $('#ArticleTitle').val() != EditArticle.originalArticleTitle
            || CKEDITOR.instances.ArticleText.getData() != EditArticle.originalArticleText)
        {
            $('#redirect').val("ArticleImageManager");
            return true;
        }
        else {
            $('#redirect').val(EditArticle.originalRedirect);
            return false;
        }
    },

    DeleteArticle: function (newsletterId, articleId) {
        window.location.href = '/Article/RemoveArticle/?newsletterId=' + newsletterId + '&articleId=' + articleId;
    },

    CheckIfFileExists: function (e) {
        var file = $('#ImageFileInput')[0].files[0];

        var reader = new FileReader();
        reader.onload = function (e) {
            var fileBytesArray = new Uint8Array(this.result),
                simpleCheckSum = GetImageByteArraySimpleCheckSum(fileBytesArray);

            $.ajax({
                url: '/ArticleImage/CheckForArticleImage',
                type: 'POST',
                data: {
                    'simpleCheckSum': simpleCheckSum
                },
                dataType: 'json',
                success: function (response) {
                    if (response.success == true) {
                        if (response.fileexists == false) {
                            $('#ArticleUpdater').submit();
                        }
                        else if (response.fileexists == true) {
                            // TODO - J - Add this response in the view instead of an alert
                            var images = JSON.parse(response.images);
                            var image = images[0];
                            var imagesExistString = "This image has already been uploaded with the name \"" +
                                image.ImageName + "\" by " + image.UploadedByUserName + " and is these articles:\n"
                            for (var i = 0; i < images.length; i++) {
                                imagesExistString += images[i].ArticleTitle + " in " + images[i].NewsletterDisplayDate +
                                    "'s newsletter: " + images[i].NewsletterMemo + "\n";
                            }
                            alert(imagesExistString);
                        }
                    }

                },
                error: function (request, error) {
                    alert("Request: " + JSON.stringify(request));
                }
            });
        }
        reader.readAsArrayBuffer(file);

        // DO NOT USE FOR HASHING ANYTHING IMPORTANT. THIS IS ONLY FOR CHECKSUMS!!
        function GetImageByteArraySimpleCheckSum(filebytes) {
            var hash = "",
                hashInts = new Array(30).fill(0),
                index = 0,
                lastRemainder = 0;

            for (var i = 0; i < filebytes.length; i++) {
                var dividend = Math.floor(filebytes[i] / 7),
                    remainder = filebytes[i] % 7;
                hashInts[index] = hashInts[index] + lastRemainder + dividend;
                lastRemainder = remainder;
                index++;
                if (index == 30) {
                    index = 0;
                }
            }

            for (var i = 0; i < hashInts.length; i++)
            {
                var preAsciiConversionDigit = hashInts[i] % 62,
                    asciiCharDigit = 0;
                if (preAsciiConversionDigit < 10) {
                    asciiCharDigit = preAsciiConversionDigit + 48;
                }
                else if (preAsciiConversionDigit < 36) {
                    asciiCharDigit = preAsciiConversionDigit + 55;
                }
                else {
                    asciiCharDigit = preAsciiConversionDigit + 61;
                }
                var charToAddToHash = String.fromCharCode(asciiCharDigit);

                hash += charToAddToHash;
            }

            return hash;
        }
    }
}