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
    public partial class Picker_Page : ContentPage
    {
        Picker picker;
        WebView webView;
        StackLayout stackLayout;
        Frame frame;
        string[] lehed = new string[4] { "https://www.youtube.com/", "https://www.pinterest.com/", "https://www.tthk.ee/", "https://mcdonalds.ee/" };

        public Picker_Page()
        {
            picker = new Picker()
            {
                Title = "Weblehed"
            };
            picker.Items.Add("YouTube");
            picker.Items.Add("Pinterest");
            picker.Items.Add("TTHK");
            picker.Items.Add("McDonalds");
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            webView = new WebView { };
            frame = new Frame
            {
                BorderColor = Color.AliceBlue,
                CornerRadius = 20,
                HeightRequest = 20, WidthRequest = 400,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HasShadow = true
            };
            stackLayout = new StackLayout { Children = { picker } };
            Content = stackLayout;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (webView!=null)
            {
                stackLayout.Children.Remove(webView);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            stackLayout.Children.Add(webView);
        }
    }
}