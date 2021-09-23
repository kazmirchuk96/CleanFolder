using System;
using System.IO;
using IWshRuntimeLibrary;


namespace CleanFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = String.Empty;
            var pathes = new[]//возможные варианты размещения папки Для файлов
            {
                $"C:\\Users\\{Environment.UserName}\\Desktop\\Для файлов",
                $"C:\\Users\\{Environment.UserName}\\Documents\\Для файлов",
                "D:\\Для файлов",
                "E:\\Для файлов",
                "C:\\Для файлов"
            };

            for (int i = 0; i < pathes.Length; i++)
            {
                if (Directory.Exists(pathes[i]))
                {
                    path = pathes[i];
                    break;
                }
            }

            if (path != String.Empty)
            {
                var dir = new DirectoryInfo(path);
                if ((dir.GetFiles().Length + dir.GetDirectories().Length) > 10) //если количество элементов в папке больше 10, то удаляем все элементы (файлы, папки)
                {
                    foreach (var file in dir.GetFiles())//удаляем файлы
                    {
                        file.Delete();
                    }

                    foreach (var folder in dir.GetDirectories())//удаляем папки
                    {
                        folder.Delete(true);
                    }
                }
            }

            CreateShortcut("CleanFolder", Environment.GetFolderPath(Environment.SpecialFolder.Startup), @"C:\Program Files (x86)\CleanFolder\CleanFolder.exe");//добавление программы в автозагрузку
        }

        static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            var shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "My shortcut description";   // The description of the shortcut
            shortcut.IconLocation = @"c:\myicon.ico";           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }
    }
}
