using FileJump.Network;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using FileJump_Mobile.Pages;

namespace FileJump_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Initialize the network listener
            NetComm.InitializeNetwork();

            PickPhotosButton.Clicked += btn_PickPhoto_Clicked;
        }


        

        private void btn_PickPhoto_Clicked(object sender, EventArgs e)
        {
            PickPhotos();
            /*
            List<FileResult> fakePaths = new List<FileResult>();

            fakePaths.Add(new FileResult("C:/dev/test/1.png"));
            fakePaths.Add(new FileResult("C:/dev/test/2.png"));
            fakePaths.Add(new FileResult("C:/dev/test/3.png"));
            fakePaths.Add(new FileResult("C:/dev/test/4.png"));
            Application.Current.MainPage = new FilesProcessorPage(fakePaths);
            */

        }

        private async void PickPhotos()
        {
            var results = await FilePicker.PickMultipleAsync();

            if(results == null)
            {
                return;
            }

            List<FileResult> res = new List<FileResult>();

            foreach(FileResult f in results)
            {
                res.Add(f);
            }

            Application.Current.MainPage = new FilesProcessorPage(res);
 
        }

    }
}
