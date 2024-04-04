using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrowserMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Button Ent_btn = new Button
            {
                Text = "Entry",
                BackgroundColor = Color.Firebrick,
            };

            Button Timer_btn = new Button
            {
                Text = "Timer",
                BackgroundColor = Color.Red,
            };

            StackLayout st = new StackLayout
            {
                Children =
                {
                    Ent_btn, Timer_btn
                }
            };

            st.BackgroundColor = Color.DarkRed;
            Content = st;
            Ent_btn.Clicked += Ent_btn_Clicked;
            Timer_btn.Clicked += Timer_btn_Clicked;
        }

        private void Timer_btn_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Ent_btn_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
