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

namespace FileJump_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Initialize the network listener
            NetComm.InitializeNetwork();

            // Subscribe to events
            DataProcessor.NetworkDiscoveryEvent += NetworkDiscoveryReceived;

            NetComm.ScoutNetworkDevices();

        }

        private void NetworkDiscoveryReceived(object sender, NetworkDiscoveryEventArgs e)
        {
            Console.WriteLine("received device");
            AddNetworkDevice(e.device);
        }

        private void AddNetworkDevice(NetworkDevice device)
        {
            StackLayout stack = new StackLayout()
            {
                
            };

            Grid.SetRow(stack, 0);
            Grid.SetColumn(stack, 0);

            Button btn_Device = new Button()
            {
                BackgroundColor = Color.Transparent,
                ImageSource = "icon_desktop_resized"
            };

            btn_Device.Clicked += Btn_Device_Clicked;

            Label lbl = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = device.Name
            };

            stack.Children.Add(btn_Device);
            stack.Children.Add(lbl);

            DevicesGrid.Children.Add(stack);

            
        }

        private void Btn_Device_Clicked(object sender, EventArgs e)
        {
            PickPhotos();
        }

        private async void PickPhotos()
        {
            var results = await FilePicker.PickMultipleAsync();

            if(results == null)
            {
                return;
            }

            var images = new List<ImageSource>();

            foreach(var item in results)
            {
                Console.WriteLine(item.FileName);
            }


            
        }

        public void button_DeviceName_OnClick(object sender, EventArgs args)
        {
            Console.WriteLine("click");
        }

        public void btn_RefreshDevices_Click(object sender, EventArgs args)
        {

        }

    }
}
