using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileJump_Mobile
{
    public interface IPhoneMediaPicker
    {
        Task<IEnumerable<MediaFile>> PickPhotosAsync(string intentTitle);
    }
}
