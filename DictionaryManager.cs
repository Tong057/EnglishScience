
using System.Collections.Generic;

namespace EnglishScience
{
    class DictionaryManager
    {
        public SortedDictionary<string, string> EnglishToPolishDictionary { get; set; }
        public SortedDictionary<string, string> PolishToEnglishDictionary { get; set; }

        public DictionaryManager()
        {
            string[] startEnglishWords = { "cake", "wife", "cat", "dog", "book", "apple", "car", 
                "bird", "river", "house", "friend", "sun", "moon", "flower", "computer" };
             
            string[] startPolishWords = { "ciasto", "żona", "kot", "pies", "książka", "jabłko", "samochód", 
                "ptak", "rzeka", "dom", "przyjaciel", "słońce", "księżyc", "kwiat", "komputer" };

            EnglishToPolishDictionary = new SortedDictionary<string, string>();
            PolishToEnglishDictionary = new SortedDictionary<string, string>();

            for (int i = 0; i < startEnglishWords.Length; ++i)
            {
                AddTranslation(startEnglishWords[i], startPolishWords[i]);
            }
   
        }

        public bool AddTranslation(string englishWord, string polishWord)
        {
            if (!EnglishToPolishDictionary.ContainsKey(englishWord) && !PolishToEnglishDictionary.ContainsKey(polishWord))
            {
                EnglishToPolishDictionary.Add(englishWord, polishWord);
                PolishToEnglishDictionary.Add(polishWord, englishWord);
                return true;
            }
            else
            {
                return false;
            }
     
        }

        public void DeleteTranslation(string englishKey, string polishKey)
        {
            EnglishToPolishDictionary.Remove(englishKey);
            PolishToEnglishDictionary.Remove(polishKey);
        }

    }
}
