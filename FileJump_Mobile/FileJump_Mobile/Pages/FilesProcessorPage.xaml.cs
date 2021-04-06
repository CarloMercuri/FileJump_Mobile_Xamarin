using FileJump.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FileJump_Mobile.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilesProcessorPage : ContentPage
	{
		public FilesProcessorPage(List<FileResult> fileResults)
		{
            // Only need this while debugging
            NetComm.InitializeNetwork();

            DataProcessor.NetworkDiscoveryEvent += NetworkDiscoveryReceived;

            InitializeComponent();

            NetComm.ScoutNetworkDevices();

		}

        private void NetworkDiscoveryReceived(object sender, NetworkDiscoveryEventArgs e)
        {
           // AddNetworkDevice(e.device);
        }

        private void AddNetworkDevice(NetworkDevice device)
        {
            StackLayout stack = new StackLayout()
            {

            };


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

            DevicesStackLayout.Children.Add(stack);


        }

        private void Btn_Device_Clicked(object sender, EventArgs args)
        {

        }

        private void btn_RefreshDevices_Click(object sender, EventArgs args)
        {

        }
	}
}