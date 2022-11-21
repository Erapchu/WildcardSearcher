using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using WildcardSearcher.Interfaces;
using WildcardSearcher.Wrappers;

namespace WildcardSearcher.Services
{
    internal class LuceneWildcardSearcher : IWildcardSearcher
    {
        private readonly IndexWriter _indexWriter;
        private readonly Lazy<LuceneDocument> _documentWrapperCache = new(() => new LuceneDocument());
        private readonly SearcherManager _searcherManager;

        public LuceneWildcardSearcher()
        {
            var directory = new RAMDirectory();
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
            _indexWriter = new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer));
            _searcherManager = new SearcherManager(directory, new SearcherFactory());
        }

        public void AddWord(string word)
        {
            var documentWrapper = _documentWrapperCache.Value;
            documentWrapper.Word.SetStringValue(word);
            _indexWriter.AddDocument(documentWrapper.Doc);
            _indexWriter.Commit();
        }

        public IEnumerable<string> SearchWords(string pattern)
        {
            IndexSearcher? indexSearcher = null;
            try
            {
                indexSearcher = _searcherManager.Acquire();
                var query = new WildcardQuery(new Term(LuceneDocument.WordField, pattern));
                var topDocs = indexSearcher.Search(query, int.MaxValue);
                return topDocs.ScoreDocs.Select(sd => indexSearcher
                    .Doc(sd.Doc)
                    .GetField(LuceneDocument.WordField)
                    .GetStringValue());
            }
            finally
            {
                if (indexSearcher != null)
                {
                    _searcherManager.Release(indexSearcher);
                    _searcherManager.MaybeRefresh();
                }
            }
        }
    }
}
