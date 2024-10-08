using Gtk;
using Gdk;
using Explorator.GUI;
using Explorator.Models;
using System.Security.Cryptography;

namespace Explorator.Controllers
{
    public class MainController
    {
        private MainWindow _mainWindow;

        public MainController()
        {
            _mainWindow = new MainWindow();
            _mainWindow.DragDataReceived += OnFileDropped;
            _mainWindow.OpenFileClicked += OnOpenFileClicked;
        }

        public void ShowMainWindow()
        {
            _mainWindow.Show();
        }

        private void OnOpenFileClicked(object? sender, EventArgs e)
        {
            FileChooserDialog fileChooser = new FileChooserDialog(
                "Select a file", _mainWindow, FileChooserAction.Open,
                "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept
            );

            if (fileChooser.Run() == (int)ResponseType.Accept)
            {
                string filePath = fileChooser.Filename;
                ProcessFile(filePath);
            }
            fileChooser.Destroy();
        }

        private void OnFileDropped(object o, DragDataReceivedArgs args)
        {
            string filePath = GetFilePathFromUri(args.SelectionData.Text);
            ProcessFile(filePath);
        }

        private void ProcessFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            FileInformation fileInformation = new FileInformation
            {
                Name = fileInfo.Name,
                Path = fileInfo.FullName,
                Size = fileInfo.Length.ToString(),
                Type = fileInfo.Extension,
                LastModified = fileInfo.LastWriteTime,
                HashMD5 = CalculateFileHash(filePath, MD5.Create()),
                HashSHA1 = CalculateFileHash(filePath, SHA1.Create()),
                HashSHA256 = CalculateFileHash(filePath, SHA256.Create())
            };

            // Generate file icon preview
            Pixbuf fileIcon = GetFileIcon(filePath);

            // Update the window with file information
            _mainWindow.UpdateForFileInformation(fileInformation, fileIcon);
        }

        private string GetFilePathFromUri(string uri)
        {
            return new Uri(uri).LocalPath;
        }

        private string CalculateFileHash(string filePath, HashAlgorithm algorithm)
        {
            using FileStream stream = File.OpenRead(filePath);
            byte[] hashBytes = algorithm.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        private Pixbuf GetFileIcon(string filePath)
        {
            IconTheme theme = IconTheme.Default;

            try
            {
                // Attempt to load the requested icon
                return theme.LoadIcon("text-x-generic", 64, IconLookupFlags.UseBuiltin);
            }
            catch (GLib.GException)
            {
                // If the icon is not found, use a fallback icon
                return theme.LoadIcon("image-missing", 64, IconLookupFlags.UseBuiltin);
            }
        }
    }
}
