using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    public class Panel
    {
        private string currentLocation;
        private string currentlySelectedItemName;
        private bool isFile;
        private ListView viewPanel;
        private Label fileType;
        private TextBox location;
        Stack<string> previousLocations;
        Stack<string> forwardLocations;
        private static ImageList iconList = new ImageList();
        public string CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }
        public string CurrentlySelectedItemName
        {
            get { return currentlySelectedItemName; }
            set { currentlySelectedItemName = value; }
        }
        public bool IsFile
        {
            get { return isFile; }
            set { isFile = value;  }
        }
        public ListView ViewPanel 
        { 
            get { return viewPanel; }
            set { viewPanel = value; }
        }
        public Label FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }
        public TextBox Location
        {
            get { return location; }
            set { location = value; }
        }
        static public ImageList IconList
        {
            get { return iconList; }
            set 
            { 
                if(value.Tag.Equals("iconList"))
                    iconList = value; 
            }
        }
        public Panel()
        {
            this.currentLocation = "";
            this.currentlySelectedItemName = "";
            this.isFile = false;
            this.viewPanel = new ListView();
            this.fileType = new Label();
            this.location = new TextBox();
            this.previousLocations = new Stack<string>();
            this.forwardLocations = new Stack<string>();
        }
        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (IsFile)
                {
                    tempFilePath = CurrentLocation + "\\" + CurrentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    FileType.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(CurrentLocation);
                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(CurrentLocation);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();

                    ViewPanel.Items.Clear();
                    ViewPanel.LargeImageList = iconList;

                    foreach (var file in files)
                    {
                        ViewPanel.Items.Add(file.Name, 0);
                    }
                    foreach (var dir in dirs)
                    {
                        ViewPanel.Items.Add(dir.Name, 1);
                    }
                }
                else
                {
                    FileType.Text = this.CurrentlySelectedItemName;
                }
            }
            catch (Exception LFADError)
            {
                MessageBox.Show(
                    LFADError.Message,
                    "Error opening a directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                 );
                List<string> pathSplit = CurrentLocation.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                CurrentLocation = string.Join("\\", pathSplit);

            }
        }
        public void ItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                CurrentlySelectedItemName = e.Item.Text;
                if (CurrentLocation.Equals("C:\\") ||
                    CurrentLocation.Equals("D:\\"))
                        CurrentLocation = CurrentLocation.Trim('\\');
                FileAttributes fileAttr = File.GetAttributes(CurrentLocation + "\\" + CurrentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    IsFile = false;
                    Location.Text = CurrentLocation + "\\" + CurrentlySelectedItemName;
                }
                else
                {
                    IsFile = true;
                }
            }
            catch (Exception ISCerror)
            {
                MessageBox.Show(
                    ISCerror.Message,
                    "Error accessing a directory!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                List<string> pathSplit = CurrentLocation.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                CurrentLocation = string.Join("\\", pathSplit);
            }

        }
        public void OpenButtonAction()
        {
            previousLocations.Push(CurrentLocation);
            CurrentLocation = Location.Text;
            LoadFilesAndDirectories();
            IsFile = false;
        }
        public void GoBack()
        {
            if(previousLocations.Count != 0)
            {
                forwardLocations.Push(CurrentLocation);
                CurrentLocation = previousLocations.Pop();
                if (CurrentLocation == "C:" || CurrentLocation == "D:") CurrentLocation = CurrentLocation + "\\";
                Location.Text = CurrentLocation;
                LoadFilesAndDirectories();
                IsFile = false;
            }
        }
       /* public void ReturnFromGoBack()
        {
            if(forwardLocations.Count != 0)
            {
                CurrentLocation = forwardLocations.Pop();
                previousLocations.Push(CurrentLocation);
                if (CurrentLocation == "C:" || CurrentLocation == "D:") CurrentLocation = CurrentLocation + "\\";
                Location.Text = CurrentLocation;
                LoadFilesAndDirectories();
                IsFile = false;
            }
        }*/
    }
}
