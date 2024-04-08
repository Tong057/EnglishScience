using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishScience.Languages
{
    class WordTestManager
    {
        public HashSet<string> UniqueQuestions { get; private set; }
        public SortedDictionary<string, string> Dictionary;
        public int NumberOfQuestions { get; set; }
        public Random Rand { get; set; }
        public int Score { get; set; }

        public WordTestManager(SortedDictionary<string, string> dictionary, int questionNumber)
        {
            Dictionary = dictionary;
            NumberOfQuestions = (questionNumber > Dictionary.Count) ? Dictionary.Count : questionNumber;
            UniqueQuestions = new HashSet<string>();
            Rand = new Random();
        }

        public string? GetRandomQuestion()
        {
            if (UniqueQuestions.Count >= NumberOfQuestions)
            {
                return null; // All questions are used
            }

            string[] keys = Dictionary.Keys.ToArray();
            string randomQuestion;

            do
            {
                randomQuestion = keys[Rand.Next(keys.Length)];
            } while (UniqueQuestions.Contains(randomQuestion));

            UniqueQuestions.Add(randomQuestion);
            return randomQuestion;
        }

        public List<string> GetRandomFourAnswersWithOneCorrect(string currentQuestion)
        {
            List<string> answerOptions = new List<string>();
            answerOptions.Add(Dictionary[currentQuestion]);

            while (answerOptions.Count < 4)
            {
                string randomWord = GetRandomValueFromDictionary();
                if (!answerOptions.Contains(randomWord))
                {
                    answerOptions.Add(randomWord);
                }
            }

            return ShuffleList(answerOptions);
        }

        private string GetRandomValueFromDictionary()
        {
            string[] values = Dictionary.Values.ToArray();
            int index = Rand.Next(values.Length);
            return values[index];
        }

        private List<string> ShuffleList(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Rand.Next(list.Count);
                string temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }

    }
}
