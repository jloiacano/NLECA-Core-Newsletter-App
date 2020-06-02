using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class ArticleImageInArticleModel
    {
        public int ArticleId { get; private set; }
        public string ArticleTitle { get; set; }
        public int NewsletterId { get; set; }
        public string NewsletterDisplayDate { get; set; }
        public string NewsletterMemo { get; set; }
        public string UploadedByUserId { get; set; }
        public string UploadedByUserName { get; set; }
        public bool IsCurrent { get; set; }
        public string ImageName { get; private set; }
        public string ImageLocation { get; private set; }

        public ArticleImageInArticleModel(DataRow dataRow)
        {
            ArticleId = Int32.Parse(dataRow["ArticleId"].ToString());
            ArticleTitle = dataRow["ArticleTitle"].ToString();
            NewsletterId = Int32.Parse(dataRow["NewsletterId"].ToString());
            NewsletterDisplayDate = dataRow["DisplayDate"].ToString();
            NewsletterMemo = dataRow["Memo"].ToString();
            UploadedByUserId = dataRow["UploadedByUserId"].ToString();
            UploadedByUserName = dataRow["UploadedByUserName"].ToString();
            IsCurrent = string.Equals(dataRow["IsCurrent"].ToString(), "1") ? true : false;
            ImageName = dataRow["ImageName"].ToString();
            ImageLocation = dataRow["ImageLocation"].ToString();
        }
    }
}
