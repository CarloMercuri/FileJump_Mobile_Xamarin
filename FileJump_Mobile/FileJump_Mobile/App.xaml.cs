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
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            List<FileResult> fakePaths = new List<FileResult>();

            fakePaths.Add(new FileResult("fakePath"));
            fakePaths.Add(new FileResult("fakePath"));
            fakePaths.Add(new FileResult("fakePath"));

            MainPage = new FilesProcessorPage(fakePaths);

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
