using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        public int NewsletterId { get; set; }
        public int ArticleSequence { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ImageFileLocation { get; set; }
        public int ArticleType { get; set; }
        public string ArticleTableOfContentsText { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleText { get; set; }
        public int AddedBy { get; set; }
        public DateTime DateAdded { get; set; }

        public ArticleModel() {}

        public ArticleModel(DataRow dataRow)
        {
            ArticleId = (int)dataRow["ArticleId"];
            NewsletterId = (int)dataRow["NewsletterId"];
            ArticleSequence = (int)dataRow["ArticleSequence"];
            ImageFileLocation = dataRow["ImageFileLocation"].ToString();
            ArticleType = (int)dataRow["ArticleType"];
            ArticleTableOfContentsText = dataRow["ArticleTableOfContentsText"].ToString();
            ArticleTitle = dataRow["ArticleTitle"].ToString();
            ArticleText = dataRow["ArticleText"].ToString();
            AddedBy = (int)dataRow["AddedBy"];
            DateAdded = (DateTime)dataRow["DateAdded"];
        }

        #region // operator overides
        public static bool operator == (ArticleModel article1, ArticleModel article2)
        {
            if ((object)article1 == null)
            {
                return (object)article2 == null;
            }

            if (
                article1.ArticleId == article2.ArticleId
                && article1.NewsletterId == article2.NewsletterId
                && article1.ArticleSequence == article2.ArticleSequence
                && article1.ImageFileLocation == article2.ImageFileLocation
                && article1.ArticleType == article2.ArticleType
                && article1.ArticleTableOfContentsText == article2.ArticleTableOfContentsText
                && article1.ArticleTitle == article2.ArticleTitle
                && article1.ArticleText == article2.ArticleText
                )
            {
                return true;
            }
            return false;
                
        }

        public static bool operator != (ArticleModel article1, ArticleModel article2)
        {
            return !(article1 == article2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var article2 = (ArticleModel)obj;
            bool isEqual = (
                ArticleId == article2.ArticleId 
                && NewsletterId == article2.NewsletterId
                && ArticleSequence == article2.ArticleSequence
                && ImageFileLocation == article2.ImageFileLocation
                && ArticleType == article2.ArticleType
                && ArticleTableOfContentsText == article2.ArticleTableOfContentsText
                && ArticleTitle == article2.ArticleTitle
                && ArticleText == article2.ArticleText
                );
            return isEqual;
        }

        public override int GetHashCode()
        {
            var toReturn = 
                ArticleId.GetHashCode() ^ 
                NewsletterId.GetHashCode() ^
                ArticleSequence.GetHashCode() ^ 
                ImageFileLocation.GetHashCode() ^ 
                ArticleType.GetHashCode() ^
                ArticleTableOfContentsText.GetHashCode() ^
                ArticleTitle.GetHashCode() ^
                ArticleText.GetHashCode();
            return toReturn;
        }
        #endregion
    }
}
