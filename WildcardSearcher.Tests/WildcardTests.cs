using Microsoft.Extensions.DependencyInjection;
using WildcardSearcher.Common.Interfaces;
using WildcardSearcher.Lucene.Extensions;

namespace WildcardSearcher.Tests
{
    public class WildcardTests
    {
        private IWildcardSearcher _wildcardSearcher;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.RegisterWildcardSearcher();
            var serviceProvider = services.BuildServiceProvider();

            _wildcardSearcher = serviceProvider.GetService<IWildcardSearcher>();
        }

        [Test]
        public void search()
        {
            _wildcardSearcher.AddWord("test");

            var firstResult = _wildcardSearcher.SearchWords("test").ToList();
            Assert.That(firstResult, Has.Count.EqualTo(1), "No results");
        }

        [Test]
        public void search_with_question_mark()
        {
            _wildcardSearcher.AddWord("пост");
            _wildcardSearcher.AddWord("порт");
            _wildcardSearcher.AddWord("пот");

            var res = _wildcardSearcher.SearchWords("по?т").ToList();
            Assert.That(res, Is.EqualTo(new[] { "пост", "порт" }), "List contains wrong values");
        }

        [Test]
        public void search_with_asterisk()
        {
            _wildcardSearcher.AddWord("здоровье");
            _wildcardSearcher.AddWord("здоров");
            _wildcardSearcher.AddWord("здорово");
            _wildcardSearcher.AddWord("дорова");

            var firstResult = _wildcardSearcher.SearchWords("здоров*").ToList();
            Assert.That(firstResult, Is.EqualTo(new[] { "здоровье", "здоров", "здорово" }), "List contains wrong values");
        }

        [Test]
        public void search_with_asterisk_and_question_mark()
        {
            _wildcardSearcher.AddWord("здравый");
            _wildcardSearcher.AddWord("здравие");
            _wildcardSearcher.AddWord("здравствуй");

            var firstResult = _wildcardSearcher.SearchWords("здрав?й*").ToList();
            Assert.That(firstResult, Is.EqualTo(new[] { "здравый" }), "List contains wrong values");
        }
    }
}