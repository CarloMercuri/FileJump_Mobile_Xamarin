using FileJump_Mobile.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FileJump_Mobile
{
    public partial class App : Application
    {

        public App(string text)
        {
            InitializeComponent();

            if(text.Length > 0)
            {
                MainPage = new FilesProcessorPage(text);
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        public App(List<FileResult> results)
        {
            InitializeComponent();

            if(results == null || results.Count <= 0)
            {
                MainPage = new MainPage();
            }
            else
            {

                MainPage = new FilesProcessorPage(results);
            }
            


        }

        public App()
        {
            InitializeComponent();
            
            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
