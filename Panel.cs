using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

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
        private bool userInput;
        private bool reCalcSize;
        private ListView viewPanel;
        private Label fileName;
        private ComboBox path;
        private Stack<string> previousLocations;
        private Stack<string> forwardLocations;
        private static DriveInfo[] allDrives;
        private static ImageList iconList;
        private static ConcurrentDictionary<string, long> dirSizes;
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
        public bool UserInput
        {
            get { return userInput; }
            set { userInput = value; }
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
            allDrives = DriveInfo.GetDrives();
            iconList = new ImageList();
            dirSizes = new ConcurrentDictionary<string, long>();
            
        }
        public Panel(string currentLocation, ListView viewPanel, Label fileName, ComboBox path)
        {
            this.currentLocation = currentLocation;
            this.targetLocation = "";
            this.currentlySelectedItemName = "";
            this.fromComboBox = false;
            this.isFile = false;
            this.userInput = true;
            this.reCalcSize = false;
            this.viewPanel = viewPanel;
            this.fileName = fileName;
            this.path = path;
            this.previousLocations = new Stack<string>();
            this.forwardLocations = new Stack<string>();
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
                fileName.Text = "--";
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
                    path.Text = currentLocation;
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
        public async void ItemToolTip(ListViewItemMouseHoverEventArgs e)
        {
            FileInfo fileInfo;
            DirectoryInfo dirInfo;
            FileAttributes fileAttr;
            long sizeInBytes;
            string pathToFile = currentLocation + "\\" + e.Item.Text;
            fileAttr = File.GetAttributes(pathToFile);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                dirInfo = new DirectoryInfo(pathToFile);
                bool noInfo = false;
                await Task.Run(() =>
                {
                    try
                    {
                        if (!dirSizes.ContainsKey(dirInfo.FullName) || reCalcSize)
                        {
                            sizeInBytes = Directory.EnumerateFiles(dirInfo.FullName, "*", SearchOption.AllDirectories)
                                                                .Sum(fileInfo => new FileInfo(fileInfo).Length);
                            if (!dirSizes.TryAdd(dirInfo.FullName, sizeInBytes))
                                dirSizes.TryUpdate(dirInfo.FullName, sizeInBytes, dirSizes.GetValueOrDefault(dirInfo.FullName));
                        }
                    }
                    catch
                    {
                        noInfo = true;
                        if (!dirSizes.ContainsKey(dirInfo.FullName)) dirSizes.TryAdd(dirInfo.FullName, -1);
                    }

                });
                if (noInfo || dirSizes.GetValueOrDefault(dirInfo.FullName) == -1)
                {
                    e.Item.ToolTipText = "Type: Directory \n" +
                                         "Created: " + dirInfo.CreationTime + "\n" +
                                         "Size unable to calculate. Not enough permissions!";
                }
                else
                {
                    dirSizes.TryGetValue(dirInfo.FullName, out sizeInBytes);
                    e.Item.ToolTipText = "Type: Directory \n" +
                                          "Created: " + dirInfo.CreationTimeUtc + "\n" +
                                           "Size: " + ChangeSizeFormat(sizeInBytes);
                }
                noInfo = false;
            }
            else
            {
                fileInfo = new FileInfo(pathToFile);
                e.Item.ToolTipText = "Type: " + fileInfo.Extension + "\n" +
                                      "Created: " + fileInfo.CreationTimeUtc + "\n" +
                                       "Size: " + ChangeSizeFormat(fileInfo.Length);
            }
            reCalcSize = false;
        }
        private string ChangeSizeFormat(long size)
        {
            decimal s = size;
            if (s > 1024) s /= 1024;
            else return s.ToString("0") + "B";
            if (s > 1024) s /= 1024;
            else return s.ToString("0") + "KB";
            if (s > 1024) s /= 1024;
            else return s.ToString("0") + "MB";
            return s.ToString("0.00") + "GB";
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
            if(previousLocations.Count != 0)
            {
                forwardLocations.Push(currentLocation);
                currentLocation = previousLocations.Pop();
                AddSlashes();
                targetLocation = currentLocation;
                LoadFilesAndDirectories();
                UserInput = true;
            }  
        }
        public void ReturnFromGoBack()
        {
            if(forwardLocations.Count != 0)
            {
                string? peek; 
                previousLocations.TryPeek(out peek);
                if (currentLocation != peek) previousLocations.Push(currentLocation);
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
                DirectoryInfo targetInfo = new DirectoryInfo(target);
                DirectoryInfo checkedFileInfo = new DirectoryInfo(currentLocation);
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
                reCalcSize = true;
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
            if (UserInput)
            {
                targetLocation = path.Text;
                string peek = previousLocations.Count == 0 ? "" : previousLocations.Peek();
                if (currentLocation != peek) previousLocations.Push(currentLocation);
                LoadFilesAndDirectories();
            }
            else UserInput = true;
        }
        private void AddSlashes()
        {
            if (allDrives.Any(drive => drive.Name.Trim('\\').Equals(currentLocation)))
                currentLocation = currentLocation + "\\";
        }
        #endregion
    }
}