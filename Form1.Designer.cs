﻿namespace FileExplorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.leftBack = new System.Windows.Forms.Button();
            this.leftForward = new System.Windows.Forms.Button();
            this.leftOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.leftView = new System.Windows.Forms.ListView();
            this.rightView = new System.Windows.Forms.ListView();
            this.rightBack = new System.Windows.Forms.Button();
            this.rightForward = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rightOpen = new System.Windows.Forms.Button();
            this.moveToRight = new System.Windows.Forms.Button();
            this.moveToLeft = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.leftFile = new System.Windows.Forms.Label();
            this.rightFile = new System.Windows.Forms.Label();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.TipForMovingFiles = new System.Windows.Forms.ToolTip(this.components);
            this.TipForOpen = new System.Windows.Forms.ToolTip(this.components);
            this.TipForLocationChanges = new System.Windows.Forms.ToolTip(this.components);
            this.TipForNames = new System.Windows.Forms.ToolTip(this.components);
            this.leftPath = new System.Windows.Forms.ComboBox();
            this.rightPath = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // leftBack
            // 
            this.leftBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftBack.Location = new System.Drawing.Point(12, 11);
            this.leftBack.Name = "leftBack";
            this.leftBack.Size = new System.Drawing.Size(30, 23);
            this.leftBack.TabIndex = 0;
            this.leftBack.Text = "<-";
            this.TipForLocationChanges.SetToolTip(this.leftBack, "Back");
            this.leftBack.UseVisualStyleBackColor = true;
            this.leftBack.Click += new System.EventHandler(this.leftBack_Click);
            // 
            // leftForward
            // 
            this.leftForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftForward.Location = new System.Drawing.Point(48, 11);
            this.leftForward.Name = "leftForward";
            this.leftForward.Size = new System.Drawing.Size(30, 23);
            this.leftForward.TabIndex = 1;
            this.leftForward.Text = "->";
            this.TipForLocationChanges.SetToolTip(this.leftForward, "Next");
            this.leftForward.UseVisualStyleBackColor = true;
            this.leftForward.Click += new System.EventHandler(this.leftForward_Click);
            // 
            // leftOpen
            // 
            this.leftOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftOpen.Location = new System.Drawing.Point(322, 11);
            this.leftOpen.Name = "leftOpen";
            this.leftOpen.Size = new System.Drawing.Size(50, 23);
            this.leftOpen.TabIndex = 3;
            this.leftOpen.Text = "Open";
            this.TipForOpen.SetToolTip(this.leftOpen, "Move to provided path");
            this.leftOpen.UseVisualStyleBackColor = true;
            this.leftOpen.Click += new System.EventHandler(this.leftOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Path:";
            // 
            // leftView
            // 
            this.leftView.Location = new System.Drawing.Point(12, 40);
            this.leftView.Name = "leftView";
            this.leftView.Size = new System.Drawing.Size(360, 368);
            this.leftView.TabIndex = 5;
            this.leftView.UseCompatibleStateImageBehavior = false;
            this.leftView.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.leftView_ItemMouseHover);
            this.leftView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.leftView_ItemSelectionChanged);
            this.leftView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.leftView_KeyUp);
            this.leftView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.leftView_MouseDoubleClick);
            // 
            // rightView
            // 
            this.rightView.Location = new System.Drawing.Point(428, 40);
            this.rightView.Name = "rightView";
            this.rightView.Size = new System.Drawing.Size(360, 368);
            this.rightView.TabIndex = 6;
            this.rightView.UseCompatibleStateImageBehavior = false;
            this.rightView.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.rightView_ItemMouseHover);
            this.rightView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.rightView_ItemSelectionChanged);
            this.rightView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rightView_KeyUp);
            this.rightView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rightView_MouseDoubleClick);
            // 
            // rightBack
            // 
            this.rightBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rightBack.Location = new System.Drawing.Point(428, 11);
            this.rightBack.Name = "rightBack";
            this.rightBack.Size = new System.Drawing.Size(30, 23);
            this.rightBack.TabIndex = 7;
            this.rightBack.Text = "<-";
            this.TipForLocationChanges.SetToolTip(this.rightBack, "Back");
            this.rightBack.UseVisualStyleBackColor = true;
            this.rightBack.Click += new System.EventHandler(this.rightBack_Click);
            // 
            // rightForward
            // 
            this.rightForward.Location = new System.Drawing.Point(464, 11);
            this.rightForward.Name = "rightForward";
            this.rightForward.Size = new System.Drawing.Size(30, 23);
            this.rightForward.TabIndex = 8;
            this.rightForward.Text = "->";
            this.TipForLocationChanges.SetToolTip(this.rightForward, "Next");
            this.rightForward.UseVisualStyleBackColor = true;
            this.rightForward.Click += new System.EventHandler(this.rightForward_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Path:";
            // 
            // rightOpen
            // 
            this.rightOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rightOpen.Location = new System.Drawing.Point(738, 11);
            this.rightOpen.Name = "rightOpen";
            this.rightOpen.Size = new System.Drawing.Size(50, 23);
            this.rightOpen.TabIndex = 11;
            this.rightOpen.Text = "Open";
            this.TipForOpen.SetToolTip(this.rightOpen, "Move to provided path");
            this.rightOpen.UseVisualStyleBackColor = true;
            this.rightOpen.Click += new System.EventHandler(this.rightOpen_Click);
            // 
            // moveToRight
            // 
            this.moveToRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.moveToRight.Location = new System.Drawing.Point(378, 185);
            this.moveToRight.Name = "moveToRight";
            this.moveToRight.Size = new System.Drawing.Size(44, 25);
            this.moveToRight.TabIndex = 12;
            this.moveToRight.Text = ">>";
            this.TipForMovingFiles.SetToolTip(this.moveToRight, "Move selected file from the left panel to the right one");
            this.moveToRight.UseVisualStyleBackColor = true;
            this.moveToRight.Click += new System.EventHandler(this.moveToRight_Click);
            // 
            // moveToLeft
            // 
            this.moveToLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.moveToLeft.Location = new System.Drawing.Point(378, 216);
            this.moveToLeft.Name = "moveToLeft";
            this.moveToLeft.Size = new System.Drawing.Size(44, 25);
            this.moveToLeft.TabIndex = 13;
            this.moveToLeft.Text = "<<";
            this.TipForMovingFiles.SetToolTip(this.moveToLeft, "Move selected file from the right panel to the left one");
            this.moveToLeft.UseVisualStyleBackColor = true;
            this.moveToLeft.Click += new System.EventHandler(this.moveToLeft_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "File:";
            this.TipForNames.SetToolTip(this.label3, "Name of the selected file");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(428, 421);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "File:";
            this.TipForNames.SetToolTip(this.label4, "Name of the selected file");
            // 
            // leftFile
            // 
            this.leftFile.AutoSize = true;
            this.leftFile.Location = new System.Drawing.Point(46, 421);
            this.leftFile.Name = "leftFile";
            this.leftFile.Size = new System.Drawing.Size(17, 15);
            this.leftFile.TabIndex = 16;
            this.leftFile.Text = "--";
            // 
            // rightFile
            // 
            this.rightFile.AutoSize = true;
            this.rightFile.Location = new System.Drawing.Point(462, 421);
            this.rightFile.Name = "rightFile";
            this.rightFile.Size = new System.Drawing.Size(17, 15);
            this.rightFile.TabIndex = 17;
            this.rightFile.Text = "--";
            // 
            // iconList
            // 
            this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.Tag = "iconList";
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "File_icon_64.png");
            this.iconList.Images.SetKeyName(1, "Folder_icon_64.png");
            // 
            // leftPath
            // 
            this.leftPath.FormattingEnabled = true;
            this.leftPath.Location = new System.Drawing.Point(124, 11);
            this.leftPath.Name = "leftPath";
            this.leftPath.Size = new System.Drawing.Size(192, 23);
            this.leftPath.TabIndex = 18;
            this.leftPath.SelectedIndexChanged += new System.EventHandler(this.leftPath_SelectedIndexChanged);
            this.leftPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.leftPath_KeyUp);
            // 
            // rightPath
            // 
            this.rightPath.FormattingEnabled = true;
            this.rightPath.Location = new System.Drawing.Point(540, 12);
            this.rightPath.Name = "rightPath";
            this.rightPath.Size = new System.Drawing.Size(192, 23);
            this.rightPath.TabIndex = 19;
            this.rightPath.SelectedIndexChanged += new System.EventHandler(this.rightPath_SelectedIndexChanged);
            this.rightPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rightPath_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.rightPath);
            this.Controls.Add(this.leftPath);
            this.Controls.Add(this.rightFile);
            this.Controls.Add(this.leftFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.moveToLeft);
            this.Controls.Add(this.moveToRight);
            this.Controls.Add(this.rightOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rightForward);
            this.Controls.Add(this.rightBack);
            this.Controls.Add(this.rightView);
            this.Controls.Add(this.leftView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.leftOpen);
            this.Controls.Add(this.leftForward);
            this.Controls.Add(this.leftBack);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "Form1";
            this.Text = "File Explorer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button leftBack;
        private Button leftForward;
        private Button leftOpen;
        private Label label1;
        private ListView leftView;
        private ListView rightView;
        private Button rightBack;
        private Button rightForward;
        private Label label2;
        private Button rightOpen;
        private Button moveToRight;
        private Button moveToLeft;
        private Label label3;
        private Label label4;
        private Label leftFile;
        private Label rightFile;
        public ImageList iconList;
        private ToolTip TipForMovingFiles;
        private ToolTip TipForOpen;
        private ToolTip TipForLocationChanges;
        private ToolTip TipForNames;
        private ComboBox leftPath;
        private ComboBox rightPath;
    }
}