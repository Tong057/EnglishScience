using EnglishScience.Languages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EnglishScience
{
    /// <summary>
    /// Логика взаимодействия для QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private readonly WordTestManager testManager;
        private string? currentQuestion;

        public QuizWindow(SortedDictionary<string, string> dictionary, int numberOfQuestions)
        {
            InitializeComponent();
            testManager = new WordTestManager(dictionary, numberOfQuestions);
            LoadNextQuestion();
        }

        private void LoadNextQuestion()
        {
            currentQuestion = testManager.GetRandomQuestion();
            if (currentQuestion != null)
            {
                questionNumberTextBlock.Text = $"№{testManager.UniqueQuestions.Count}/{testManager.NumberOfQuestions}";
                wordKeyTextBlock.Text = currentQuestion;

                List<string> answerOptions = testManager.GetRandomFourAnswersWithOneCorrect(currentQuestion);
                firstOptionButton.Content = answerOptions[0];
                secondOptionButton.Content = answerOptions[1];
                thirdOptionButton.Content = answerOptions[2];
                fourthOptionButton.Content = answerOptions[3];
            }
            else
            {
                _ = new CustomMessageBox("Your result is: " + testManager.Score + "/" + testManager.NumberOfQuestions, "Congratulations!", MessageType.Custom, MessageButtons.Ok, this);
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedOption = (string)((Button)sender).Content;
            if (currentQuestion != null && selectedOption == testManager.Dictionary[currentQuestion])
            {
                testManager.Score++;
            }

            scoreTextBlock.Text = "Score: " + testManager.Score;
            LoadNextQuestion();
        }
    }
}
