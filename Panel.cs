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
        #region variables
        private string currentLocation;
        private string targetLocation;
        private string currentlySelectedItemName;
        private ListView viewPanel;
        private Label fileName;
        private TextBox location;
        Stack<string> previousLocations;
        Stack<string> forwardLocations;
        private static ImageList iconList;
        #endregion
        #region properties
        static public ImageList IconList
        {
            private get { return iconList; }
            set 
            { 
                if(value.Tag.Equals("iconList"))
                    iconList = value; 
            }
        }
        #endregion
        #region constructors
        static Panel()
        {
            iconList = new ImageList();
        }
        public Panel(string currentLocation, ListView viewPanel, Label fileName, TextBox location)
        {
            this.currentLocation = currentLocation;
            this.targetLocation = "";
            this.currentlySelectedItemName = "";
            this.viewPanel = viewPanel;
            this.fileName = fileName;
            this.location = location;
            this.previousLocations = new Stack<string>();
            this.forwardLocations = new Stack<string>();
            location.Text = currentLocation;
        }
        #endregion
        #region methods
        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            FileAttributes fileAttr;
            try
            {
                if (targetLocation != "")
                {
                    fileAttr = File.GetAttributes(targetLocation);
                    currentLocation = targetLocation;
                }
                else
                {
                    fileAttr = File.GetAttributes(currentLocation);
                    targetLocation = currentLocation;
                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(targetLocation);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();

                    viewPanel.Items.Clear();
                    viewPanel.LargeImageList = IconList;

                    foreach (var file in files)
                    {
                        viewPanel.Items.Add(file.Name, 0);
                    }
                    foreach (var dir in dirs)
                    {
                        viewPanel.Items.Add(dir.Name, 1);
                    }
                    location.Text = currentLocation;
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
                List<string> pathSplit = currentLocation.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                currentLocation = string.Join("\\", pathSplit);

            }
        }
        public void ItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            string tempMainDir;
            try
            {
                currentlySelectedItemName = e.Item.Text;
                fileName.Text = currentlySelectedItemName;
                if (currentLocation.Equals("C:\\") ||
                    currentLocation.Equals("D:\\"))
                        currentLocation = currentLocation.Trim('\\');
                tempMainDir = currentLocation + "\\";
                FileAttributes fileAttr = File.GetAttributes(currentLocation + "\\" + currentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    targetLocation = currentLocation + "\\" + currentlySelectedItemName;
                }
                else
                {
                    location.Text = tempMainDir;
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
                List<string> pathSplit = currentLocation.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                currentLocation = string.Join("\\", pathSplit);
            }

        }
        public void OpenButtonAction()
        {
            if(forwardLocations.Count != 0) forwardLocations.Clear();
            previousLocations.Push(currentLocation);
            LoadFilesAndDirectories();
        }
        public void GoBack()
        {
            if(previousLocations.Count != 0)
            {
                forwardLocations.Push(currentLocation);
                currentLocation = previousLocations.Pop();
                if (currentLocation == "C:" || currentLocation == "D:") currentLocation = currentLocation + "\\";
                targetLocation = currentLocation;
                LoadFilesAndDirectories();
            }
        }
        public void ReturnFromGoBack()
        {
            if(forwardLocations.Count != 0)
            {
                previousLocations.Push(currentLocation);
                currentLocation = forwardLocations.Pop();
                if (currentLocation == "C:" || currentLocation == "D:") currentLocation = currentLocation + "\\";
                targetLocation = currentLocation;
                LoadFilesAndDirectories();
            }
        }
        #endregion
    }
}
