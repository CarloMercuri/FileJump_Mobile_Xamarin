using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FileJump.Network;
using System.IO;
using System.Collections.ObjectModel;

namespace FileJump_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileSendPage : ContentPage
    {
        private List<StackLayout> FilesStacks = new List<StackLayout>();

        private List<FileResult> fileres = new List<FileResult>();

        private string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".bmp", ".png" };


       // private List<FileData> FileDataList = new List<FileData>();
        public ObservableCollection<FileData> FileDataList { get; set; }
        private IPEndPoint TargetEndPoint { get; set; }


        public FileSendPage(List<FileResult> files, IPEndPoint _targetEndPoint)
        {
            InitializeComponent();
            FileDataList = new ObservableCollection<FileData>();

            TargetEndPoint = _targetEndPoint;

            btn_SendFiles.Clicked += SendButtonClicked;


            // Add the files to the FileData list
            foreach (FileResult f in files)
            {
                FileInfo fInfo = new FileInfo(f.FullPath);

                FileData fData = new FileData();

                fData.FileStructure = new FileStructure()
                {
                    FileExtension = fInfo.Extension,
                    FileName = fInfo.Name,
                    FilePath = fInfo.FullName,
                    FileSize = fInfo.Length
                };

                fData.FileImage = GetFileImage(fInfo);

                FileDataList.Add(fData);

            }

            FilesView.ItemsSource = FileDataList;

            DataProcessor.OutboundTransferFinished += OutBoundTransferFinished;

        }

        private void OutBoundTransferFinished(object sender, OutTransferEventArgs args)
        {
            for (int i = 0; i < FileDataList.Count; i++)
            {
                if(FileDataList[i].FileStructure.FilePath == args.FilePath)
                {
                    FileDataList[i].FileImage = new Image()
                    {
                        Source = ImageSource.FromFile("icon_desktop_small")
                    };
                }
            }
        }

        private void SendButtonClicked(object sender, EventArgs args)
        {
            List<FileStructure> fl = new List<FileStructure>();

            foreach(FileData f in FileDataList)
            {
                fl.Add(f.FileStructure);
            }

            DataProcessor.SendFiles(fl, TargetEndPoint);
        }

        private Image GetFileImage(FileInfo file_info)
        {

            // If it's an image, create a thumbnail from it
            if(imageExtensions.Contains(file_info.Extension))
            {
                Image img = new Image()
                {
                    Source = ImageSource.FromFile(file_info.FullName)
                };

                return img;
            }

            // Lastly, generate a generic file icon

            Image generic = new Image();
            generic.Source = "generic_file_icon";

            generic.WidthRequest = 70;
            generic.HeightRequest = 70;


            return generic;


        }

        private void AddItemsToCollectionView()
        {
            Image img = new Image()
            {
                Source = ImageSource.FromFile("icon_desktop_small"),    //ImageSource.FromFile(fileres[0].FullPath),
                WidthRequest = 50,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End
            };

            StackLayout stack = new StackLayout();

            stack.Children.Add(img);

            FilesStacks.Add(stack);

           // FilesCollectionView.ItemsSource = FilesStacks;
              
        }

       
    }
}