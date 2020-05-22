using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Data
{
    public class NewsletterArticleType
    {
        private NewsletterArticleType(int intValue, string text) { IntValue = intValue;  Text = text; }
        private NewsletterArticleType() { Text = string.Empty; }
        public int IntValue { get; set; }
        public string Text { get; set; }

        public static NewsletterArticleType NoImage { get { return new NewsletterArticleType(0, "No Image"); } }
        public static NewsletterArticleType LeftImage { get { return new NewsletterArticleType(1, "Left Image"); } }
        public static NewsletterArticleType RightImage { get { return new NewsletterArticleType(2, "Right Image"); } }
        public static NewsletterArticleType TopImage { get { return new NewsletterArticleType(3, "Top Image"); } }
        public static NewsletterArticleType BottomImage { get { return new NewsletterArticleType(4, "Bottom Image"); } }


        public static IEnumerable<NewsletterArticleType> GetArticleTypes()
        {
            List<NewsletterArticleType> list = new List<NewsletterArticleType>();

            list.Add(NewsletterArticleType.NoImage);
            list.Add(NewsletterArticleType.LeftImage);
            list.Add(NewsletterArticleType.RightImage);
            list.Add(NewsletterArticleType.TopImage);
            list.Add(NewsletterArticleType.BottomImage);

            return list.AsEnumerable();
        }

        public static explicit operator NewsletterArticleType(int i)
        {
            NewsletterArticleType toReturn = new NewsletterArticleType();
            IEnumerable<NewsletterArticleType> articleTypes = NewsletterArticleType.GetArticleTypes();

            foreach (NewsletterArticleType articleType in articleTypes)
            {
                if (i == articleType.IntValue)
                {
                    toReturn = articleType;
                }
            }

            if (string.IsNullOrEmpty(toReturn.Text))
            {
                throw new IndexOutOfRangeException(string.Format("There is no ArticleType corresponding to {0}", i.ToString()));
            }

            return toReturn;
        }

        public static explicit operator int(NewsletterArticleType articletype)
        {
            return articletype.IntValue;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
