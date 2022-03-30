using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helper;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace EvernoteClone.View
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        NotesVM VM;
        public NotesWindow()
        {
            InitializeComponent();

            VM = Resources["vm"] as NotesVM;

            container.DataContext = VM;

            VM.NotesChanged += VM_NotesChanged; ;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);

            fontfamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>() { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 28, 48 };

            fontSizeComboBox.ItemsSource = fontSizes;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                VM.GetNotebooks();
            }
        }

        private void VM_NotesChanged(object? sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();

            if (VM.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(VM.SelectedNote.FileLocation))
                {
                    FileStream fileStream = new FileStream(VM.SelectedNote.FileLocation, FileMode.Open);

                    var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

                    contents.Load(fileStream, DataFormats.Rtf);

                    fileStream.Close();
                };

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void GravarButton_Click(object sender, RoutedEventArgs e)
        {
            string region = "brazilsouth";
            string key = "0938cf2e412e48e4b77c44f36bf644ed";


            var speechConfig = SpeechConfig.FromSubscription(key, region);

            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
                }
            }

        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountCharacters = (new TextRange(contentRichTextBox.Document.ContentEnd, contentRichTextBox.Document.ContentStart)).Text.Length;
            statusTextBlock.Text = $"Tamanho do coumento: {ammountCharacters} caracteres";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);

        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);

            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicdButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoretion = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoretion != DependencyProperty.UnsetValue) && (selectedDecoretion.Equals(TextDecorations.Underline));

            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
            fontfamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);

        }

        private void underlinedButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);

                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }

        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)

                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }


        }

        private void fontfamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontfamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontfamilyComboBox.SelectedItem);
            }

        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedValue != null)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);


        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{VM.SelectedNote.Id}.rtf");

            VM.SelectedNote.FileLocation = rtfFile;


            DataBaseHelper.Update(VM.SelectedNote);

            FileStream fileStream = new FileStream(rtfFile, FileMode.Create);

            var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

            contents.Save(fileStream, DataFormats.Rtf);

            fileStream.Close();

        }

        private void DeletarButton_Click(object sender, RoutedEventArgs e)
        {
            // a criar
        }
    }
}
