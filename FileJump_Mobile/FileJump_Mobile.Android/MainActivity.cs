using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;
using Android.Content;
using Xamarin.Essentials;
using System.Collections.Generic;
using Android.Provider;
using Android.Database;
using Android.Graphics;
using System.Collections;


namespace FileJump_Mobile.Droid
{
    [Activity(Label = "FileJump_Mobile", Icon = "@mipmap/icon", Name = "com.companyname.filejump_mobile.MinActivity", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //FileJump.Network.Data

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            string action = Intent.Action;
            string type = Intent.Type;

            if(Intent.ActionSend.Equals(action) && type != null)
            {
                if ("text/plain".Equals(type))
                {
                    string text = HandleSentText();
                    LoadApplication(new App(text));
                    return;
                } 
                else
                {
                    List<FileResult> results =  HandleSentFile();
                    LoadApplication(new App(results));
                    return;
                }
            } else if (Intent.ActionSendMultiple.Equals(action) && type != null)
            {
                List<FileResult> results = HandleSentMultipleFiles();
                LoadApplication(new App(results));
                return;

            }

            LoadApplication(new App());


                
        }

        private List<FileResult> HandleSentMultipleFiles()
        {
            
            var list = Intent.GetParcelableArrayListExtra(Intent.ExtraStream);

            List<FileResult> results = new List<FileResult>();

            for (int i = 0; i < list.Count; i++)
            {
                string path = FilesHelper.GetActualPathForFile((Android.Net.Uri)list[i], ApplicationContext);
                results.Add(new FileResult(path));
                Console.WriteLine(path);
            }

            return results;
        }

        private List<FileResult> HandleSentFile()
        {
            Android.Net.Uri uri = (Android.Net.Uri)Intent.GetParcelableExtra(Intent.ExtraStream);
            string path = FilesHelper.GetActualPathForFile(uri, ApplicationContext);
            FileResult fr = new FileResult(path);

            List<FileResult> list = new List<FileResult>();
            list.Add(fr);

            return list;
        }


        private string HandleSentText()
        {
            return Intent.GetStringExtra(Intent.ExtraText);
            
        }

        

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            

            List<FileResult> list = new List<FileResult>();

            list.Add(new FileResult("buabua"));

            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}