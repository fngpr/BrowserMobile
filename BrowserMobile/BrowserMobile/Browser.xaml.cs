using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrowserMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browser : ContentPage
    {
        private bool isPortrait;
        Button tagasi, home, forward, favourites, historybtn;
        Grid gridTop;
        Entry addressEntry;
        Picker picker, pickerHistory;
        WebView webView;
        StackLayout st;
        List<string> lehed = new List<string> { "https://moodle.edu.ee/", "https://www.tthk.ee/", "https://github.com/", "https://www.w3schools.com/" };
        List<string> nimetused = new List<string> { "Moodle", "TTHK", "GitHub", "W3Schools" };
        List<string> history = new List<string>();
        string homeURL = "https://www.tthk.ee/";
        string result = "";
        public Browser()
        {
            // Заголовок страницы
            Title = "Browser";
            // Создание выпадающего списка для выбора страниц
            picker = new Picker
            {
                Title = "Browser",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            isPortrait = true;

            pickerHistory = new Picker
            {

            };

            // Верхняя сетка для размещения элементов управления
            gridTop = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = GridLength.Auto }, // Поле ввода URL
                    new RowDefinition { Height = GridLength.Auto }, // Выпадающий список
                    new RowDefinition { Height = GridLength.Auto }  // Кнопки
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };


            // Кнопка "Назад"
            tagasi = new Button
            {
                Text = "<",
                BackgroundColor = Color.FromHex("#3498db"),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                CornerRadius = 5,
            };
            tagasi.Clicked += Tagasi_Clicked;

            // Кнопка "Домой"
            home = new Button
            {
                Text = "|",
                BackgroundColor = Color.FromHex("#2ecc71"),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                CornerRadius = 5,
            };
            home.Clicked += Home_Clicked;

            // Кнопка "Вперед"
            forward = new Button
            {
                Text = ">",
                BackgroundColor = Color.FromHex("#e74c3c"),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                CornerRadius = 5,
            };
            forward.Clicked += Forward_Clicked;


            // Кнопка "Избранное"
            favourites = new Button
            {
                Text = "*",
                BackgroundColor = Color.FromHex("#f39c12"),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                CornerRadius = 5,
            };
            favourites.Clicked += Favourites_Clicked;

            // Кнопка "История"
            historybtn = new Button
            {
                Text = "?",
                BackgroundColor = Color.FromHex("#9b59b6"),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                CornerRadius = 5,
            };
            historybtn.Clicked += Historybtn_Clicked;


            // Поле ввода URL
            addressEntry = new Entry
            {
                Placeholder = "Enter URL",
                ReturnType = ReturnType.Go,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            addressEntry.Completed += AddressEntry_Completed;

            // Добавление элементов в выпадающий список
            foreach (string leht in nimetused)
            {
                picker.Items.Add(leht);
            }

            // Веб-просмотрщик
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = "https://www.tthk.ee/" },
                HeightRequest = 400,
                WidthRequest = 200,
            };

            // Обработчики жестов
            SwipeGestureRecognizer swipe_right = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            swipe_right.Swiped += Swipe_up_Swiped;
            SwipeGestureRecognizer swipe_left = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipe_left.Swiped += Swipe_up_Swiped;
            SwipeGestureRecognizer swipe_up = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            swipe_up.Swiped += Swipe_up_Swiped;

            // Размещение элементов в сетке
            gridTop.Children.Add(addressEntry, 0, 0); // Entry occupies column 0
            Grid.SetColumnSpan(addressEntry, 5); // Entry spans across all columns
            gridTop.Children.Add(picker, 0, 1); // Picker occupies column 0
            Grid.SetColumnSpan(picker, 5); // Picker spans across all columns
            gridTop.Children.Add(tagasi, 0, 2); // Добавляем кнопку "Назад" в третью строку сетки
            gridTop.Children.Add(home, 1, 2);   // Добавляем кнопку "Домой" в третью строку сетки
            gridTop.Children.Add(forward, 2, 2); // Добавляем кнопку "Вперед" в третью строку сетки
            gridTop.Children.Add(favourites, 3, 2); // Добавляем кнопку "Избранное" в третью строку сетки
            gridTop.Children.Add(historybtn, 4, 2);

            // Горизонтальное расположение элемента ввода URL
            addressEntry.HorizontalOptions = LayoutOptions.FillAndExpand;

            var longPressGesture = new TapGestureRecognizer();
            longPressGesture.NumberOfTapsRequired = 1;
            longPressGesture.Tapped += LongPressGesture_Tapped;
            picker.GestureRecognizers.Add(longPressGesture);

            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

            // Размещение элементов в вертикальном стеке
            st = new StackLayout
            {
                Children = { gridTop, webView },
            };

            // Установка распределения по горизонтали
            st.HorizontalOptions = LayoutOptions.FillAndExpand;
            st.VerticalOptions = LayoutOptions.FillAndExpand;

            // Установка содержимого страницы
            Content = st;
        }

        // Обработчик события выбора элемента в выпадающем списке
        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pickedItem = picker.SelectedItem as string;
            var confirm = await DisplayAlert("Kustuta", $"Kas soovite kustutada {pickedItem}?", "Jah", "Ei");
            if (confirm)
            {
                if (lehed.Contains(pickedItem))
                {
                    lehed.Remove(pickedItem);
                    nimetused.Remove(pickedItem);
                    picker.Items.Remove(pickedItem);

                    webView.Source = new UrlWebViewSource { Url = homeURL };
                }
            }
        }

        private void LongPressGesture_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Обработчик жеста свайпа
        private void Swipe_up_Swiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    // Переход на предыдущую страницу
                    if (webView.CanGoBack)
                    {
                        webView.GoBack();
                        UpdatePickerSelection();
                    }
                    break;
                case SwipeDirection.Left:
                    // Переход на следующую страницу
                    if (webView.CanGoForward)
                    {
                        webView.GoForward();
                        UpdatePickerSelection();
                    }
                    break;
                case SwipeDirection.Up:
                    // Переход на домашнюю страницу
                    webView.Source = new UrlWebViewSource { Url = homeURL };
                    break;
                default: break;
            }
        }

        // Обработчик завершения ввода URL
        private void AddressEntry_Completed(object sender, EventArgs e)
        {
            string url = addressEntry.Text;

            if (!string.IsNullOrWhiteSpace(url))
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "https://" + url;
                }

                webView.Source = new UrlWebViewSource { Url = url };
                AddToHistory(url);
            }
        }

        // Обработчик события нажатия на кнопку "История"
        private async void Historybtn_Clicked(object sender, EventArgs e)
        {
            if (history.Count > 0)
            {
                string selectedUrl = await DisplayActionSheet("Recent Pages", "Cancel", null, history.ToArray());

                if (selectedUrl != "Cancel")
                {
                    webView.Source = new UrlWebViewSource { Url = selectedUrl };
                }
            }
            else
            {
                await DisplayAlert("History", "No recent pages", "OK");
            }
        }

        // Обработчик события нажатия на кнопку "Избранное"
        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            if (webView.Source is UrlWebViewSource urlWebViewSource)
            {
                string currentUrl = urlWebViewSource.Url;

                lehed.Add(currentUrl);

                result = await DisplayPromptAsync("Vali uus nimi", "Uus nimi");
                nimetused.Add(result);
                picker.Items.Add(result);
                picker.SelectedIndex = nimetused.IndexOf(result);

                UpdateWebViewSource();
            }
            else
            {
                await DisplayAlert("Error", "The current source is not a URL", "OK");
            }
        }

        // Обработчик события нажатия на кнопку "Вперед"
        private void Forward_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        // Обработчик события нажатия на кнопку "Домой"
        private void Home_Clicked(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = "https://www.tthk.ee/" };
        }

        // Обработчик события нажатия на кнопку "Назад"
        private void Tagasi_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        // Обновление выбранного элемента в выпадающем списке
        private void UpdatePickerSelection()
        {
            var currentUrl = webView.Source?.ToString();
            var index = lehed.IndexOf(currentUrl);
            if (index >= 0 && index < picker.Items.Count)
            {
                picker.SelectedIndex = index;
            }
        }

        // Добавление URL в историю
        private void AddToHistory(string url)
        {
            if (!history.Contains(url))
            {
                history.Add(url);
            }

            if (history.Count > 5)
            {
                history.RemoveAt(0);
            }
        }

        // Обновление источника веб-просмотрщика
        private void UpdateWebViewSource()
        {
            if (picker.SelectedIndex >= 0 && picker.SelectedIndex < lehed.Count)
            {
                string selectedUrl = lehed[picker.SelectedIndex];
                if (!string.IsNullOrEmpty(selectedUrl))
                {
                    webView.Source = new UrlWebViewSource { Url = selectedUrl };
                    AddToHistory(selectedUrl);
                }
            }
        }
    }
}
