using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    public class Panel
    {
        private string currentLocation;
        private string previousLocation;
        private string currentlySelectedItemName;
        private bool isFile;
        public string CurrentLocation
        {
            get { return currentLocation; }
            private set { currentLocation = value; }
        }
        public string PreviousLocation
        {
            get { return previousLocation; }
            private set { previousLocation = value; }
        }
        public string CurrentlySelectedItemName
        {
            get { return currentlySelectedItemName; }
            private set { currentlySelectedItemName = value; }
        }
        public bool IsFile
        {
            get { return isFile; }
            private set { isFile = value;  }
        }
        public Panel(string currentLocation)
        {
            this.currentLocation = currentLocation;
            this.previousLocation = "";
            this.currentlySelectedItemName = "";
            this.isFile = false;
        }
        public void loadFilesAndDirectories(ListView view, Label fileType, ImageList iconList)
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
                    fileType.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(CurrentLocation);
                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    CurrentLocation = CurrentLocation + "\\";
                    fileList = new DirectoryInfo(CurrentLocation);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();

                    view.Items.Clear();
                    view.LargeImageList = iconList;
                    foreach (var file in files)
                    {
                        view.Items.Add(file.Name, 0);
                    }
                    foreach (var dir in dirs)
                    {
                        view.Items.Add(dir.Name, 1);
                    }
                }
                else
                {
                    fileType.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception lFADError)
            {
                MessageBox.Show(
                    lFADError.Message,
                    "Error opening a directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                 );
                List<string> pathSplit = CurrentLocation.Split('\\').ToList();
                pathSplit.RemoveRange(pathSplit.Count - 2, pathSplit.Count - 1);
                CurrentLocation = string.Join("\\", pathSplit);

            }
        }
    }
}
