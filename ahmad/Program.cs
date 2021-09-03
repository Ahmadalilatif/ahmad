using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace fileSystem
{
    class Program
    {
        public object MessageBox { get; private set; }

        static void Main(string[] args)
        {

            var fileSystem = new FileSystemWatcher(@"C:\Users\ahmad\OneDrive\Desktop\New folder")
            {
                 //Filter="*.Jpg|",
              //  Filter = "JPG|*.jpg|PNG|*.png|Bitmap|*.bmp",
                NotifyFilter =NotifyFilters.FileName| NotifyFilters.Size | NotifyFilters.Attributes,
                EnableRaisingEvents=true
            };
            fileSystem.Changed += onfoloderpath;
            fileSystem.Created += onfoloderpath;
            fileSystem.Deleted += onfoloderpath;
            fileSystem.Renamed += onfoloder;

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }
        private static void onfoloderpath(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("some file change occur");
            Console.WriteLine(e.FullPath);
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(e.Name);
            var imge = e.FullPath;
            if (File.Exists(imge))
            {
               
                string dir = new FileInfo(imge).DirectoryName;

                string imagename = imge.Split(Path.DirectorySeparatorChar).Last();


                var name = imagename.Split('.');
                String fullimagename = name[0];



                string Location = dir;
                string pathfolder = System.IO.Path.Combine(Location, fullimagename);


                System.IO.Directory.CreateDirectory(pathfolder);


                string thumFilePath = Path.Combine(pathfolder, "thumbnail.jpeg");//mm
                System.Drawing.Image image = System.Drawing.Image.FromFile(imge);
                var thumImage = image.GetThumbnailImage(64, 64, new Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);
                thumImage.Save(thumFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                Console.WriteLine("your add only images");
            }
        }

        private static void onfoloder(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("some file change occur");
            Console.WriteLine($"old name=>{e.OldName}");

            Console.WriteLine($"new name=>{e.Name}");
            Console.WriteLine(e.Name);
        }
      
    }
}
