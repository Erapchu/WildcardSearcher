using System.Collections.Generic;

namespace WildcardSearcher.Interfaces
{
    public interface IWildcardSearcher
    {
        /// <summary>
        /// Add word to dictionary.
        /// </summary>
        /// <param name="word">Source word</param>
        void AddWord(string word);

        /// <summary>
        /// Search for words using wildcard pattern.
        /// </summary>
        /// <param name="pattern">Pattern contains ? and *.</param>
        /// <returns>Array of suitable words</returns>
        IEnumerable<string> SearchWords(string pattern);
    }
}
