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

        private List<FileResult> SelectedFiles = new List<FileResult>();

        private int TransferType { get; set; } // 1 = file, 2 = text

        private string MessageText { get; set; }


        public FilesProcessorPage(List<FileResult> fileResults)
		{
            InitializeComponent();
            TransferType = 1;

            label_AviableDevices.Text = "Who do you want to send the file(s) to?";

            // Only need this while debugging
            NetComm.InitializeNetwork();

            DataProcessor.NetworkDiscoveryEvent += NetworkDiscoveryReceived;



            NetComm.ScoutNetworkDevices();

            btn_RefreshDevices.Clicked += btn_RefreshDevices_Click;


            SelectedFiles = fileResults;


        }

        public FilesProcessorPage(string text)
        {
            InitializeComponent();
            TransferType = 2;
            MessageText = text;
            label_AviableDevices.Text = "Who do you want to send it to?";

            // Only need this while debugging
            NetComm.InitializeNetwork();

            DataProcessor.NetworkDiscoveryEvent += NetworkDiscoveryReceived;



            NetComm.ScoutNetworkDevices();

            btn_RefreshDevices.Clicked += btn_RefreshDevices_Click;
        }

        private void NetworkDiscoveryReceived(object sender, NetworkDiscoveryEventArgs e)
        {
           AddNetworkDevice(e.device);
        }

        private void AddNetworkDevice(NetworkDevice device)
        {
            StackLayout stack = new StackLayout()
            {

            };


            Button btn_Device = new Button()
            {
                BackgroundColor = Color.Transparent,
                ImageSource = GetCorrectDeviceIcon(device.Type)
                
            };

            btn_Device.Clicked += (sender, args) =>
            {
                Device_Clicked(device);
            };

            Label lbl = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = device.Name,
                WidthRequest = 80,
                LineBreakMode = LineBreakMode.TailTruncation,
                MaxLines = 2,
                TextColor = Color.Black
            };

            stack.Children.Add(btn_Device);
            stack.Children.Add(lbl);

            

            DevicesStackLayout.Children.Add(stack);
            //DeviceScrollView.Content = stack;
            

        }

        private void Device_Clicked(NetworkDevice device)
        {
            if(TransferType == 1)
            {
                Application.Current.MainPage = new FileSendPage(SelectedFiles, device.EndPoint);
            } 
            else
            {
                DataProcessor.SendTextMessage(MessageText, device.EndPoint);
                Application.Current.MainPage = new MainPage();
            } 

            
        }

        private void btn_RefreshDevices_Click(object sender, EventArgs args)
        {
            DevicesStackLayout.Children.Clear();
            NetComm.ScoutNetworkDevices();
        }

        /// <summary>
        /// Returns the icon image associated with the specified device type
        /// </summary>
        /// <param name="dType"></param>
        /// <returns></returns>
        private string GetCorrectDeviceIcon(NetworkDeviceType dType)
        {
            switch (dType)
            {
                case NetworkDeviceType.Desktop:
                    return "icon_desktop_small";

                case NetworkDeviceType.MobilePhone:
                    return "icon_phone_small";

                default:
                    return null;
            }
        }


    }
}