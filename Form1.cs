using System.Windows.Forms;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        Panel leftPanel = new Panel();
        Panel rightPanel = new Panel();
        
        public Form1()
        {
            InitializeComponent();
            InitializePanel(leftPanel, "C:\\", leftView, leftFileType, leftLocation);
            InitializePanel(rightPanel, "D:\\", rightView, rightFileType, rightLocation);
            Panel.IconList = iconList;
            leftLocation.Text = leftPanel.CurrentLocation;
            rightLocation.Text = rightPanel.CurrentLocation;
            leftPanel.LoadFilesAndDirectories();
            rightPanel.LoadFilesAndDirectories();
        }
        private void InitializePanel(Panel panel, string currentLocation, ListView view, Label label, TextBox location)
        {
            panel.CurrentLocation = currentLocation;
            panel.ViewPanel = view;
            panel.FileType = label;
            panel.Location = location;
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
          //  leftPanel.ReturnFromGoBack();
        }
        private void rightForward_Click(object sender, EventArgs e)
        {
           // rightPanel.ReturnFromGoBack();
        }
        private void leftOpen_Click(object sender, EventArgs e)
        {
            leftPanel.OpenButtonAction();
         }

        private void rightOpen_Click(object sender, EventArgs e)
        {
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

       
    }
}