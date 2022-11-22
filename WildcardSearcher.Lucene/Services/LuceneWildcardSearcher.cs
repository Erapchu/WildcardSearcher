using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using WildcardSearcher.Common.Interfaces;
using WildcardSearcher.Lucene.Wrappers;

namespace WildcardSearcher.Lucene.Services
{
    internal class LuceneWildcardSearcher : IWildcardSearcher, IDisposable
    {
        private readonly Directory _directory;
        private readonly Analyzer _analyzer;
        private readonly IndexWriter _indexWriter;
        private readonly SearcherManager _searcherManager;
        private readonly Lazy<LuceneDocument> _documentWrapperCache = new(() => new LuceneDocument());

        public LuceneWildcardSearcher()
        {
            _directory = new RAMDirectory();
            _analyzer = new StandardAnalyzer(global::Lucene.Net.Util.LuceneVersion.LUCENE_48);
            _indexWriter = new IndexWriter(_directory, new IndexWriterConfig(global::Lucene.Net.Util.LuceneVersion.LUCENE_48, _analyzer));
            _indexWriter.Commit(); // allows create segments files
            _searcherManager = new SearcherManager(_directory, new SearcherFactory());
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
                _searcherManager.MaybeRefresh();
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
                }
            }
        }

        public void Dispose()
        {
            _searcherManager.Dispose();
            _indexWriter.Dispose();
            _analyzer.Dispose();
            _directory.Dispose();
        }
    }
}
