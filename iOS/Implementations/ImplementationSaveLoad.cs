/*

Copyright (c) 2018 Shawn Patrick Gilroy, www.smallnstats.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

using System;
using System.IO;
using Foundation;
using SkiaSharp;
using SleepTracker.Interfaces;
using SleepTracker.iOS.Implementations;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImplementationSaveLoad))]
namespace SleepTracker.iOS.Implementations
{
    public class ImplementationSaveLoad : InterfaceSaveLoad
    {
        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <returns><c>true</c>, if exists was filed, <c>false</c> otherwise.</returns>
        /// <param name="filename">Filename.</param>
        public bool FileExists(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the local file path.
        /// </summary>
        /// <returns>The local file path.</returns>
        /// <param name="filename">Filename.</param>
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        /// <summary>
        /// Installs the location file.
        /// </summary>
        /// <param name="filename">Filename.</param>
        public void InstallLocationFile(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <returns>The file.</returns>
        /// <param name="filename">Filename.</param>
        public string LoadFile(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <param name="text">Text.</param>
        public void SaveFile(string filename, string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="title"></param>
        public void SaveTempImage(SKData image, string title)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docFolder, title);

            using (var memoryStream = new MemoryStream())
            {
                image.SaveTo(memoryStream);

                NSData appleData = NSData.FromArray(memoryStream.ToArray());
                UIImage appleImg = UIImage.LoadFromData(appleData);

                appleImg.SaveToPhotosAlbum((img, error) => {
                    var o = img as UIImage;
                    Console.WriteLine("error:" + error);
                });
            }
        }
    }
}
