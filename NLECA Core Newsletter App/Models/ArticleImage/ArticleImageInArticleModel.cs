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
            ArticleId = (dataRow["ArticleId"] == DBNull.Value) ? -1
                : (int)dataRow["ArticleId"];

            ArticleTitle = (dataRow["ArticleTitle"] == DBNull.Value) ? string.Empty 
                : dataRow["ArticleTitle"].ToString();

            NewsletterId = (dataRow["NewsletterId"] == DBNull.Value) ? -1
                : (int)dataRow["NewsletterId"];

            NewsletterDisplayDate = (dataRow["DisplayDate"] == DBNull.Value) ? string.Empty
                : dataRow["DisplayDate"].ToString();

            NewsletterMemo = (dataRow["Memo"] == DBNull.Value) ? string.Empty
                : dataRow["Memo"].ToString();

            UploadedByUserId = dataRow["UploadedByUserId"].ToString();
            UploadedByUserName = dataRow["UploadedByUserName"].ToString();
            IsCurrent = string.Equals(dataRow["IsCurrent"].ToString(), "1") ? true : false;
            ImageName = dataRow["ImageName"].ToString();
            ImageLocation = dataRow["ImageLocation"].ToString();
        }
    }
}
