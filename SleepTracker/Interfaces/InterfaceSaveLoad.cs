using System;
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
