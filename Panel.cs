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
        private Label fileName;
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
        public Label FileName
        {
            get { return fileName; }
            set { fileName = value; }
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
            this.fileName = new Label();
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
                    FileName.Text = fileDetails.Extension;
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
                    FileName.Text = this.CurrentlySelectedItemName;
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
            string tempMainDir;
            try
            {
                CurrentlySelectedItemName = e.Item.Text;
                FileName.Text = CurrentlySelectedItemName;
                if (CurrentLocation.Equals("C:\\") ||
                    CurrentLocation.Equals("D:\\"))
                        CurrentLocation = CurrentLocation.Trim('\\');
                tempMainDir = CurrentLocation + "\\";
                FileAttributes fileAttr = File.GetAttributes(CurrentLocation + "\\" + CurrentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    IsFile = false;
                    Location.Text = CurrentLocation + "\\" + CurrentlySelectedItemName;
                }
                else
                {
                    IsFile = true;
                    Location.Text = tempMainDir;
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
            if(forwardLocations.Count != 0) forwardLocations.Clear();
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
                IsFile = false;
                LoadFilesAndDirectories();
            }
        }
        public void ReturnFromGoBack()
        {
            if(forwardLocations.Count != 0)
            {
                previousLocations.Push(CurrentLocation);
                CurrentLocation = forwardLocations.Pop();
                if (CurrentLocation == "C:" || CurrentLocation == "D:") CurrentLocation = CurrentLocation + "\\";
                Location.Text = CurrentLocation;
                IsFile = false;
                LoadFilesAndDirectories();
            }
        }
    }
}
