namespace VisualStudioAutomation
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using TestStack.White;

    public class WhiteTest
    {
        private const int SwShowmaximized = 3;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public static void Main()
        {
            Console.WriteLine("Hello");
            
            var processStartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe")
            {
                WorkingDirectory = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE"
            };

            var application = Application.Launch(processStartInfo);

            var windows = application.GetWindows();

            var window = windows.FirstOrDefault(x => x.Name.EndsWith("Visual Studio", true, CultureInfo.CurrentCulture));
            while (window == null)
            {
                Thread.Sleep(1000);

                window = application.GetWindows().FirstOrDefault(x => x.Name.EndsWith("Visual Studio", true, CultureInfo.CurrentCulture));
            }

            // Sample usage
            ShowWindow(application.Process.MainWindowHandle, SwShowmaximized);

            var mainWindow = new MainWindow(window);

            var newProjectDialog = mainWindow.ClickNewProject();

            newProjectDialog.SelectType("WPF Application");

            newProjectDialog.SetName("MyAutomatedProject");

            newProjectDialog.ClickOk();

            mainWindow.AddReference("Microsoft.Build");

            application.Close();

            Console.ReadKey();
        }
    }
}
