namespace FileExplorer
{
    public partial class Form1 : Form
    {
        private string currentLeftLocation = "C:\\";
        private string currentRightLocation = "D:\\";
        private string previousLeftLocation = "";
        private string previousRightLocation = "";
        private string currentlySelectedLeftItemName = "";
        private string currentlySelectedRightItemName = "";
        private bool isLeftFile = false;
        private bool isRightFile = false;
        public Form1()
        {
            InitializeComponent();
            
            leftLocation.Text = "C:";
            rightLocation.Text = "D:";
            loadFilesAndDirectories(currentLeftLocation, isLeftFile, currentlySelectedLeftItemName, leftView);
            loadFilesAndDirectories(currentRightLocation, isRightFile, currentlySelectedRightItemName, rightView);
        }

        private void leftBack_Click(object sender, EventArgs e)
        {
            List<string> pathSplit = leftLocation.Text.Split('\\').ToList();
            if (pathSplit.Count != 1) pathSplit.RemoveAt(pathSplit.Count - 1);
            currentLeftLocation = string.Join("\\", pathSplit);
            currentlySelectedLeftItemName = "";
            leftLocation.Text = currentLeftLocation;
            loadFilesAndDirectories(currentLeftLocation, isLeftFile, currentlySelectedLeftItemName, leftView);
            isLeftFile = false;
        }

        private void leftForward_Click(object sender, EventArgs e)
        {

        }

        private void rightBack_Click(object sender, EventArgs e)
        {
            List<string> pathSplit = rightLocation.Text.Split('\\').ToList();
            if(pathSplit.Count != 1) pathSplit.RemoveAt(pathSplit.Count - 1);
            currentRightLocation= string.Join("\\", pathSplit);
            currentlySelectedRightItemName = "";
            rightLocation.Text = currentRightLocation;
            loadFilesAndDirectories(currentRightLocation, isRightFile, currentlySelectedRightItemName, rightView);
            isLeftFile = false;

        }
        private void loadFilesAndDirectories(string currentLocation, bool isFile, string currentlySelectedItemName, ListView view)
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = currentLocation + "\\" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    if (view.Name == leftView.Name) leftFileType.Text = fileDetails.Extension;
                    else rightFileType.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(currentLocation);
                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    currentLocation = currentLocation + "\\";
                    fileList = new DirectoryInfo(currentLocation);
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
                    if (view.Name == leftView.Name) leftFileType.Text = this.currentlySelectedLeftItemName;
                    else rightFileType.Text = this.currentlySelectedRightItemName;
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
                List<string> pathSplit = currentLocation.Split('\\').ToList();
                pathSplit.RemoveRange(pathSplit.Count - 2, pathSplit.Count - 1);
                if (view.Name == leftView.Name) currentLeftLocation = string.Join("\\", pathSplit);
                else currentRightLocation = string.Join("\\", pathSplit);

            }
        }
        private void leftOpen_Click(object sender, EventArgs e)
        {
            OpenButtonAction(ref currentLeftLocation, ref isLeftFile, currentlySelectedLeftItemName, leftLocation, leftView);
        }

        private void rightOpen_Click(object sender, EventArgs e)
        {
            OpenButtonAction(ref currentRightLocation, ref isRightFile, currentlySelectedRightItemName, rightLocation, rightView);
        }
        private void OpenButtonAction(ref string currentLocation, ref bool isFile, string currentlySelectedItemName, TextBox text, ListView view)
        {
                if (view.Name == leftView.Name) currentLocation = leftLocation.Text;
                else currentLocation = rightLocation.Text;
                loadFilesAndDirectories(currentLocation, isFile, currentlySelectedItemName, view);
                isFile = false;
        }
        private void leftView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            itemSelectionChanged(e, ref currentlySelectedLeftItemName, ref currentLeftLocation, ref isLeftFile, leftLocation);
        }

        private void rightView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            itemSelectionChanged(e, ref currentlySelectedRightItemName, ref currentRightLocation, ref isRightFile, rightLocation);
        }
        private void itemSelectionChanged(ListViewItemSelectionChangedEventArgs e, ref string currentlySelectedItemName, ref string currentLocation, ref bool isFile, TextBox text)
        {
            try
            {
                currentlySelectedItemName = e.Item.Text;
                if (currentLocation.Equals("C:\\")) currentLocation = "C:";
                if (currentLocation.Equals("D:\\")) currentLocation = "D:";
                FileAttributes fileAttr = File.GetAttributes(currentLocation + "\\" + currentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFile = false;
                    text.Text = currentLocation + "\\" + currentlySelectedItemName;
                }
                else
                {
                    isFile = true;
                }
            } 
            catch(Exception iSCerror)
            {
                MessageBox.Show(
                    iSCerror.Message,
                    "Error accessing a directory!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                List<string> pathSplit = currentLocation.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                currentLocation = string.Join("\\", pathSplit);
            }
            
        }

        private void leftView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenButtonAction(ref currentLeftLocation, ref isLeftFile, currentlySelectedLeftItemName, leftLocation, leftView);
        }

        private void rightView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenButtonAction(ref currentRightLocation, ref isRightFile, currentlySelectedRightItemName, rightLocation, rightView);
        }
    }
}