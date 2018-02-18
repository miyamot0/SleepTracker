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

using SkiaSharp;

namespace SleepTracker.Interfaces
{
    public interface InterfaceSaveLoad
    {
        /// <summary>
        /// Build local path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetLocalFilePath(string filename);

        /// <summary>
        /// Save text file to personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="text"></param>
        void SaveFile(string filename, string text);

        /// <summary>
        /// Saves the temp image.
        /// </summary>
        /// <param name="image">Image.</param>
        /// <param name="title">Title.</param>
        void SaveTempImage(SKData image, string title);

        /// <summary>
        /// Load text file from personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string LoadFile(string filename);

        /// <summary>
        /// Check if file exists in personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool FileExists(string filename);

        /// <summary>
        /// Install (android only)
        /// </summary>
        /// <param name="filename"></param>
        void InstallLocationFile(string filename);
    }
}
