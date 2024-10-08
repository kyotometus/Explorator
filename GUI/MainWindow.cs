using Gtk;
using Gdk;
using Explorator.Models;

namespace Explorator.GUI
{
    public class MainWindow : Gtk.Window
    {
        private VBox _mainBox;

        public event EventHandler OpenFileClicked;

        public MainWindow() : base("Explorator - 0.0.1")
        {
            SetDefaultSize(500, 600);
            SetPosition(WindowPosition.Center);

            Label dragDropLabel = new Label("Drag or upload a file");
            Button openFileButton = new Button("Open File");

            openFileButton.Clicked += (sender, e) => OpenFileClicked?.Invoke(this, EventArgs.Empty);

            _mainBox = new VBox(false, 10);
            _mainBox.PackStart(dragDropLabel, false, false, 0);
            _mainBox.PackStart(openFileButton, false, false, 0);

            HBox alignmentBox = new HBox(true, 0);
            alignmentBox.PackStart(_mainBox, false, false, 0);
            Add(alignmentBox);

            ShowAll();
        }


        public void UpdateForFileInformation(FileInformation fileInfo, Pixbuf fileIcon)
        {
            // Clear the current content
            foreach (var child in _mainBox.Children)
            {
                _mainBox.Remove(child);
            }

            // File icon (preview)
            Image filePreview = new Image(fileIcon);
            _mainBox.PackStart(filePreview, false, false, 0);

            // File information labels and textboxes
            _mainBox.PackStart(new Label($"Name: {fileInfo.Name}"), false, false, 0);
            _mainBox.PackStart(new Label($"Size: {fileInfo.Size}"), false, false, 0);
            _mainBox.PackStart(new Label($"Type: {fileInfo.Type}"), false, false, 0);
            _mainBox.PackStart(new Label($"Last Modified: {fileInfo.LastModified}"), false, false, 0);

            // Disabled TextBoxes for path and hashes
            TextView pathTextView = CreateDisabledTextView(fileInfo.Path);
            TextView md5TextView = CreateDisabledTextView(fileInfo.HashMD5);
            TextView sha1TextView = CreateDisabledTextView(fileInfo.HashSHA1);
            TextView sha256TextView = CreateDisabledTextView(fileInfo.HashSHA256);

            _mainBox.PackStart(new Label("Path:"), false, false, 0);
            _mainBox.PackStart(pathTextView, false, false, 0);

            _mainBox.PackStart(new Label("MD5:"), false, false, 0);
            _mainBox.PackStart(md5TextView, false, false, 0);

            _mainBox.PackStart(new Label("SHA1:"), false, false, 0);
            _mainBox.PackStart(sha1TextView, false, false, 0);

            _mainBox.PackStart(new Label("SHA256:"), false, false, 0);
            _mainBox.PackStart(sha256TextView, false, false, 0);

            ShowAll(); // Re-render the UI
        }

        private TextView CreateDisabledTextView(string content)
        {
            TextView textView = new TextView();
            textView.Buffer.Text = content;

            textView.Editable = false;  // Prevent editing
            textView.Sensitive = true;  // Allow user interaction (select and copy)

            textView.WrapMode = WrapMode.Word;

            return textView;
        }

    }
}
