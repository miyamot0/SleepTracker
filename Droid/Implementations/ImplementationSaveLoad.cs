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
using SleepTracker.Interfaces;
using Xamarin.Forms;
using SkiaSharp;
using SleepTracker.Droid.Implementations;

[assembly: Dependency(typeof(ImplementationSaveLoad))]
namespace SleepTracker.Droid.Implementations
{
    public class ImplementationSaveLoad : InterfaceSaveLoad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string LoadFile(string filename)
        {
            var path = CreatePathToFile(filename);

            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="text"></param>
        public void SaveFile(string filename, string text)
        {
            var path = CreatePathToFile(filename);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string CreatePathToFile(string filename)
        {
            var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        [Obsolete]
        public void InstallLocationFile(string filename)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="title"></param>
        public void SaveTempImage(SKData image, string title)
        {
            //            throw new NotImplementedException();

            string path = Path.Combine(Android.OS.Environment.DirectoryDownloads, title);

            using (var memoryStream = new MemoryStream())
            {
                image.SaveTo(memoryStream);

                using (var fileStream = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] bytes = new byte[memoryStream.Length];
                    memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                    fileStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Close();
                }
            }
        }
    }
}
