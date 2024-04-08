using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace EnglishScience
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DictionaryManager dictionaryManager;
        SortedDictionary<string, string> currentDictionary;
        List<string> translateHistory;

        public MainWindow()
        {
            InitializeComponent();
            dictionaryManager = new DictionaryManager();
            translateHistory = new List<string>();
            SetEnglishLanguage_Click(null, null);
        }

        private void SetEnglishLanguage_Click(object? sender, RoutedEventArgs? e)
        {
            currentDictionary = dictionaryManager.EnglishToPolishDictionary;
            SetResourceDictionary("english");
        }

        private void SetPolishLanguage_Click(object sender, RoutedEventArgs e)
        {
            currentDictionary = dictionaryManager.PolishToEnglishDictionary;
            SetResourceDictionary("polish");
        }

        private void SetResourceDictionary(string language)
        {
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"Languages/" + language + ".xaml", UriKind.Relative) });
            UpdateListView();
            ClearFields();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string btnContent = (string)((Button)e.OriginalSource).Content;

            switch(btnContent)
            {
                case "Launch Test":
                    LanguageSelectMessageBox languageMB = new LanguageSelectMessageBox("Please, select test's language.", "Language selection", this);

                    if (languageMB.SelectedLanguage != null)
                    {
                        QuizWindow quiz = new QuizWindow(languageMB.SelectedLanguage == "english" ? dictionaryManager.EnglishToPolishDictionary : dictionaryManager.PolishToEnglishDictionary, 10);
                        quiz.ShowDialog();
                    }
                        
                    break;

                case "History":
                    _ = new HistoryMessageBox(translateHistory, this);
                    break;

                case "Add word":
                    if (IsEmptyAnyField())
                    {
                        _ = new CustomMessageBox("Please, input words and try again.", "Some fields are empty.", MessageType.Error, MessageButtons.Ok, this);
                    }
                    else
                    {
                        string englishWord = (currentDictionary == dictionaryManager.EnglishToPolishDictionary) ? firstTextBox.Text.ToLower() : secondTextBox.Text.ToLower();
                        string polishWord = (currentDictionary == dictionaryManager.EnglishToPolishDictionary) ? secondTextBox.Text.ToLower() : firstTextBox.Text.ToLower();

                        if (!dictionaryManager.AddTranslation(englishWord, polishWord))
                            _ = new CustomMessageBox("Please, input another word and try again.", "This word is already declared.", MessageType.Error, MessageButtons.Ok, this);
                        
                        ClearFields();
                        UpdateListView();
                    }
                    break;

                case "Delete word":
                    if (!IsSelectedAnyWord())
                    {
                        _ = new CustomMessageBox("Please, select item and try again.", "Any item is not selected.", MessageType.Error, MessageButtons.Ok, this);
                    }
                    else
                    {
                        string englishKey = (currentDictionary == dictionaryManager.EnglishToPolishDictionary) ? GetKeyFromListView() : GetValueFromListView();
                        string polishKey = (currentDictionary == dictionaryManager.EnglishToPolishDictionary) ? GetValueFromListView() : GetKeyFromListView();

                        dictionaryManager.DeleteTranslation(englishKey, polishKey);
                        ClearFields();
                        UpdateListView();
                    }
                    break;
            }
        }

        private string GetKeyFromListView()
        {
            return ((KeyValuePair<string, string>)dictionaryListView.SelectedItem).Key;
        }

        private string GetValueFromListView()
        {
            return ((KeyValuePair<string, string>)dictionaryListView.SelectedItem).Value;
        }

        private bool IsEmptyAnyField()
        {
            return firstTextBox.Text == "" || secondTextBox.Text == "";

        }

        private bool IsSelectedAnyWord()
        {
            return dictionaryListView.SelectedIndex != -1;
        }

        private void UpdateListView()
        {
            dictionaryListView.ItemsSource = new Dictionary<string, string>(currentDictionary);
        }

        private void ClearFields()
        {
            firstTextBox.Text = "";
            secondTextBox.Text = "";
        }

        private void DictionaryListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSelectedAnyWord())
            {
                firstTextBox.Text = GetKeyFromListView();
                secondTextBox.Text = GetValueFromListView();
            }
            else
            {
                ClearFields();
            }
        }

        private void SearchButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (firstTextBox.Text == "")
            {
                _ = new CustomMessageBox("Please, input word to translate and try again.", "Some fields are empty.", MessageType.Error, MessageButtons.Ok, this);
            }
            else if (!currentDictionary.ContainsKey(firstTextBox.Text))
            {
                _ = new CustomMessageBox("Please, input a correct word and try again.", "The word hasn't been found.", MessageType.Error, MessageButtons.Ok, this);
            }
            else
            {
                secondTextBox.Text = currentDictionary[firstTextBox.Text];
                translateHistory.Add(firstTextBox.Text + "  -  " + secondTextBox.Text);
            }
        }
    }
}
