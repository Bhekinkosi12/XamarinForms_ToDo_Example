using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ToDoList.Database;
using UIKit;

namespace ToDoList.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libFolder = System.IO.Path.Combine(docFolder, "..", "Library", "Databases");

            if (!System.IO.Directory.Exists(libFolder)){
                System.IO.Directory.CreateDirectory(libFolder);
            }

            TodoDatabase.Root = libFolder;

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
