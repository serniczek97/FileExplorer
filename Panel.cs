using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private bool fromComboBox;
        private bool isFile;
        private ListView viewPanel;
        private Label fileName;
        private ComboBox path;
        private Stack<string> previousLocations;
        private Stack<string> forwardLocations;
        private DriveInfo[] allDrives;
        private static ImageList iconList;
        #endregion
        #region properties
        public string CurrentLocation
        {
            get { return currentLocation; }
        }
        public bool FromComboBox
        {
            get { return fromComboBox; }
            set { fromComboBox = value; }
        }
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
        public Panel(string currentLocation, ListView viewPanel, Label fileName, ComboBox path)
        {
            this.currentLocation = currentLocation;
            this.targetLocation = "";
            this.currentlySelectedItemName = "";
            this.fromComboBox = false;
            this.isFile = false;
            this.viewPanel = viewPanel;
            this.fileName = fileName;
            this.path = path;
            this.previousLocations = new Stack<string>();
            this.forwardLocations = new Stack<string>();
            this.allDrives = DriveInfo.GetDrives();
            path.Text = currentLocation;
            PartitionsLetters();
        }
        #endregion
        #region methods
        private void PartitionsLetters()
        {
            foreach(DriveInfo drive in allDrives)
            {
                if(drive.DriveType == DriveType.Fixed)
                    path.Items.Add(drive.Name);
            }
        }
        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            FileAttributes fileAttr;
            try
            {
                viewPanel.LargeImageList = IconList;
                viewPanel.ShowItemToolTips = true;

                if (targetLocation != "")
                {
                    fileAttr = File.GetAttributes(targetLocation);
                    currentLocation = targetLocation;
                    AddSlashes();
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
                    DirectoryInfo dirInfo;
                    FileInfo? fileInfo;

                    viewPanel.Items.Clear();
                    
                    foreach (FileInfo file in files)
                    {
                        if (file.Attributes.HasFlag(FileAttributes.Hidden) ||
                            file.Attributes.HasFlag(FileAttributes.System)) continue;
                        viewPanel.Items.Add(file.Name, 0);
                    }
                    foreach (DirectoryInfo dir in dirs)
                    {
                        if (dir.Attributes.HasFlag(FileAttributes.Hidden) ||
                            dir.Attributes.HasFlag(FileAttributes.System)) continue;
                        viewPanel.Items.Add(dir.Name, 1);
                    }
                    for (int i = 0; i < viewPanel.Items.Count; i++)
                    {
                        fileInfo = files.FirstOrDefault(file => file.Name.Equals(viewPanel.Items[i].Text));
                        if (fileInfo != null)
                        {
                            viewPanel.Items[i].ToolTipText = "Type: " + fileInfo.Extension + "\n" +
                                                          "Created: " + fileInfo.CreationTimeUtc + "\n" +
                                                          "Size: " + ChangeSizeFormat(fileInfo.Length);
                        }
                        else
                        {
                            dirInfo = dirs.First(dir => dir.Name.Equals(viewPanel.Items[i].Text));
                            viewPanel.Items[i].ToolTipText = "Type: Directory \n" +
                                                             "Created: " + dirInfo.CreationTime;
                        }
                    }
                    path.Text = currentLocation;
                    
                    string ChangeSizeFormat(long size)
                    {
                        decimal s = size;
                        if (s > 1024) s /= 1024;
                        else return s.ToString("0") + "B";
                        if(s > 1024) s /= 1024;
                        else return s.ToString("0") + "KB";
                        if (s > 1024) s /= 1024;
                        else return s.ToString("0") + "MB";
                        return s.ToString("0.00") + "GB";
                    }
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
                AddSlashes();
                tempMainDir = currentLocation;
                FileAttributes fileAttr = File.GetAttributes(currentLocation + "\\" + currentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFile = false;
                    currentLocation = currentLocation.Trim('\\');
                    targetLocation = currentLocation + "\\" + currentlySelectedItemName;
                }
                else
                {
                    isFile = true;
                    path.Text = tempMainDir;
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
            if(!isFile) previousLocations.Push(currentLocation);
            if (FromComboBox) targetLocation = path.Text;
            LoadFilesAndDirectories();
            FromComboBox = false;
        }
        public void GoBack()
        {
            if(previousLocations.Count != 1)
            {
                fileName.Text = "";
                forwardLocations.Push(currentLocation);
                currentLocation = previousLocations.Pop();
                AddSlashes();
                targetLocation = currentLocation;
                LoadFilesAndDirectories();
            }
        }
        public void ReturnFromGoBack()
        {
            if(forwardLocations.Count != 0)
            {
                fileName.Text = "";
                previousLocations.Push(currentLocation);
                currentLocation = forwardLocations.Pop();
                AddSlashes();
                targetLocation = currentLocation;
                LoadFilesAndDirectories();
            }
        }
        public void MoveToOtherPanel(string target)
        {
            try
            {
                string checkedFile = currentLocation + "\\" + currentlySelectedItemName;
                target = target + "\\" + currentlySelectedItemName;
                FileAttributes fileAttr = File.GetAttributes(checkedFile);
                if((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Move(checkedFile, target);
                }
                else
                {
                    File.Move(checkedFile, target);
                }
                targetLocation = "";
                LoadFilesAndDirectories();
            }
            catch(Exception MTOPError)
            {
                MessageBox.Show(
                    MTOPError.Message,
                    "Error moving a file/directory!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }
        public void PathChanged()
        {
            targetLocation = path.Text;
            previousLocations.Push(currentLocation);
            LoadFilesAndDirectories();
        }
        private void AddSlashes()
        {
            if (allDrives.Any(drive => drive.Name.Trim('\\').Equals(currentLocation)))
                currentLocation = currentLocation + "\\";
        }
        #endregion
    }
}
