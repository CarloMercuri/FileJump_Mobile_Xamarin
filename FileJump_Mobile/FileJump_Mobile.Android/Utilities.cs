using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileJump_Mobile.Droid
{
    public static class Utilities
    {
        public static Bitmap GetFileThumbnail(string fullPath)
        {
            Bitmap bmp = BitmapFactory.DecodeFile(fullPath);

            return ThumbnailUtils.ExtractThumbnail(bmp, 50, 50);
        }
    }
}