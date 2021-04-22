using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzeriChatMMC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            messagetxtbox.Focus();
        }
        private ScrollViewer CreateScrollViewer()
        {
            return new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };
        }
        private Grid CreateGridForSentImage()
        {
            Grid grid = new Grid();

            TextBlock textBlockFirst = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#31572c")),
                FontWeight = FontWeights.SemiBold,
                Text = $"{DateTime.Now.ToShortTimeString()}"
            };

            grid.Children.Add(textBlockFirst);

            return grid;
        }
        private List<ScrollViewer> WindowsSizeChangedAction()
        {
            List<ScrollViewer> scrollViewers = new List<ScrollViewer>();

            const int messageIndex = 1;

            for (int i = 0; i < messagelstbx.Items.Count; i++)
            {
                scrollViewers.Add(messagelstbx.Items[i] as ScrollViewer);
                Grid grid = scrollViewers[i].Content as Grid;
                FrameworkElement element = grid.Children[messageIndex] as FrameworkElement;
                element.Margin = new Thickness(scrlvwr.ActualWidth - 250, 0, 0, 0);
            }

            return scrollViewers;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            List<ScrollViewer> scrollViewers = WindowsSizeChangedAction();

            messagelstbx.Items.Clear();

            scrollViewers.ForEach(sv => messagelstbx.Items.Add(sv));

            if (messagelstbx.Items.Count != 0)
            {
                messagelstbx.ScrollIntoView(messagelstbx.Items[messagelstbx.Items.Count - 1]);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//sendbutton
        {
            if (!string.IsNullOrWhiteSpace(messagetxtbox.Text))
            {
                ScrollViewer scrollViewer = CreateScrollViewer();

                Grid grid = CreateGridForSentImage();

                var textBlockSecond = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 210,
                    Margin = new Thickness(scrlvwr.ActualWidth - 250, 0, 0, 0),
                    Text = $"{messagetxtbox.Text}"
                };

                grid.Children.Add(textBlockSecond);

                scrollViewer.Content = grid;

                messagetxtbox.Text = default;

                messagelstbx.Items.Add(scrollViewer);
                messagelstbx.ScrollIntoView(messagelstbx.Items[messagelstbx.Items.Count - 1]);
            }

            messagetxtbox.Focus();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//imagebutton
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                ScrollViewer scrollViewer = CreateScrollViewer();

                Grid grid = CreateGridForSentImage();

                Image image = new Image
                {
                    Width = 200,
                    Height = 100,
                    Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                    Margin = new Thickness(scrlvwr.ActualWidth - 250, 0, 0, 0)
                };

                grid.Children.Add(image);

                scrollViewer.Content = grid;

                messagelstbx.Items.Add(scrollViewer);
                messagelstbx.ScrollIntoView(messagelstbx.Items[messagelstbx.Items.Count - 1]);

            }

            messagetxtbox.Focus();
        }
    }
}
