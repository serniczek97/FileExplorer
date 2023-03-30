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
            leftPanel = new Panel("C:\\", leftView, leftFile, leftPath);
            rightPanel = new Panel("D:\\", rightView, rightFile, rightPath);
            Panel.IconList = iconList;
            leftPanel.LoadFilesAndDirectories();
            rightPanel.LoadFilesAndDirectories();
        }
        private void leftBack_Click(object sender, EventArgs e)
        {
            leftPanel.UserInput = false;
            leftPanel.GoBack();
        }
        private void rightBack_Click(object sender, EventArgs e)
        {
            rightPanel.UserInput = false;
            rightPanel.GoBack();
        }
        private void leftForward_Click(object sender, EventArgs e)
        {
            leftPanel.UserInput = false;
            leftPanel.ReturnFromGoBack();
        }
        private void rightForward_Click(object sender, EventArgs e)
        {
            rightPanel.UserInput = false;
            rightPanel.ReturnFromGoBack();
        }
        private void leftOpen_Click(object sender, EventArgs e)
        {
            leftPanel.FromComboBox = true;
            leftPanel.OpenButtonAction();
        }
        private void rightOpen_Click(object sender, EventArgs e)
        {
            rightPanel.FromComboBox = true;
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
        private void leftPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            leftPanel.PathChanged();
        }
        private void rightPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            rightPanel.PathChanged();
        }
        private void leftView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    leftPanel.OpenButtonAction();
                    break;
                case Keys.Back:
                    leftPanel.GoBack();
                    break;
                default: break;
            }
        }
        private void rightView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    rightPanel.OpenButtonAction();
                    break;
                case Keys.Back:
                    rightPanel.GoBack();
                    break;
                default: break;
            }
        }
        private void rightPath_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    rightPanel.FromComboBox = true;
                    rightPanel.OpenButtonAction();
                    break;
                default: break;
            }
        }
        private void leftPath_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    leftPanel.FromComboBox = true;
                    leftPanel.OpenButtonAction();
                    break;
                default: break;
            }
        }
        private void leftView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            leftPanel.ItemToolTip(e);
        }
        private void rightView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            rightPanel.ItemToolTip(e);
        }
    }
}