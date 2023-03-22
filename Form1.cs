using System.Windows.Forms;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        Panel leftPanel;
        Panel rightPanel;
        
        public Form1()
        {
            InitializeComponent();
            leftPanel = new Panel("C:\\", leftView, leftFile, leftLocation);
            rightPanel = new Panel("D:\\", rightView, rightFile, rightLocation);
            Panel.IconList = iconList;
            leftPanel.LoadFilesAndDirectories();
            rightPanel.LoadFilesAndDirectories();
        }
        private void leftBack_Click(object sender, EventArgs e)
        {
            leftPanel.GoBack();
        }
        private void rightBack_Click(object sender, EventArgs e)
        {
            rightPanel.GoBack();
        }
        private void leftForward_Click(object sender, EventArgs e)
        {
            leftPanel.ReturnFromGoBack();
        }
        private void rightForward_Click(object sender, EventArgs e)
        {
            rightPanel.ReturnFromGoBack();
        }
        private void leftOpen_Click(object sender, EventArgs e)
        {
            leftPanel.FromTextBox = true;
            leftPanel.OpenButtonAction();
         }

        private void rightOpen_Click(object sender, EventArgs e)
        {
            rightPanel.FromTextBox = true;
            rightPanel.OpenButtonAction();
        }
       
        private void leftView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            leftPanel.ItemSelectionChanged(e);
        }

        private void rightView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            rightPanel.ItemSelectionChanged(e);
        }
        private void leftView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            leftPanel.OpenButtonAction();
        }
        private void rightView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            rightPanel.OpenButtonAction();
        }

        private void moveToRight_Click(object sender, EventArgs e)
        {
            leftPanel.MoveToOtherPanel(rightPanel.CurrentLocation);
            rightPanel.LoadFilesAndDirectories();
        }

        private void moveToLeft_Click(object sender, EventArgs e)
        {
            rightPanel.MoveToOtherPanel(leftPanel.CurrentLocation);
            leftPanel.LoadFilesAndDirectories();
        }
    }
}