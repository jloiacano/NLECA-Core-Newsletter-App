using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        public int NewsletterId { get; set; }
        public int Sequence { get; set; }
        public string ImageFileLocation { get; set; }
        public int ArticleType { get; set; }
        public string Text { get; set; }
        public int AddedBy { get; set; }
        public DateTime DateAdded { get; set; }


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
                && article1.Sequence == article2.Sequence
                && article1.ImageFileLocation == article2.ImageFileLocation
                && article1.ArticleType == article2.ArticleType
                && article1.Text == article2.Text
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
                && Sequence == article2.Sequence
                && ImageFileLocation == article2.ImageFileLocation
                && ArticleType == article2.ArticleType
                && Text == article2.Text
                );
            return isEqual;
        }

        public override int GetHashCode()
        {
            var toReturn = 
                ArticleId.GetHashCode() ^ 
                NewsletterId.GetHashCode() ^ 
                Sequence.GetHashCode() ^ 
                ImageFileLocation.GetHashCode() ^ 
                ArticleType.GetHashCode() ^ 
                Text.GetHashCode();
            return toReturn;
        }
        #endregion
    }
}
