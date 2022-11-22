using Lucene.Net.Documents;

namespace WildcardSearcher.Lucene.Wrappers
{
    internal class LuceneDocument
    {
        public const string WordField = "Word";

        public Document Doc { get; }
        public TextField Word { get; }

        public LuceneDocument()
        {
            Doc = new Document()
            {
                (Word = new TextField(WordField, string.Empty, Field.Store.YES))
            };
        }
    }
}
