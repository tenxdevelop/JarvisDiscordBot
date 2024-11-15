/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System.Threading.Tasks;

namespace JarvisDiscordBot
{
    public interface IControllerIO
    {
        string GetFullPath(string currentDirectory);

        string GetCurrentDirectory();

        bool IsHaveDirectory(string pathDirectory);

        bool CreateDirectory(string pathDirectory);

        bool IsHaveFile(string filePath, string fileName);

        bool DeleteFile(string filePath, string fileName);

        bool WriteToFile(string message, string filePath, string fileName, bool isNewFile);
        Task<string> ReadFromFileAsync(string filePath);
    }
}