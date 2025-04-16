namespace EQUISync
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            btnBrowse = new Button();
            eqFolderBrowserDialog = new FolderBrowserDialog();
            lblSelectEQDir = new Label();
            cBoxSrcChar = new ComboBox();
            uiFileBindingSource = new BindingSource(components);
            btnSync = new Button();
            lblSourceChar = new Label();
            lblTgtChars = new Label();
            listBoxLogger = new ListBox();
            lblLog = new Label();
            chkListBoxTgtChars = new CheckedListBox();
            chkBoxSelectAll = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)uiFileBindingSource).BeginInit();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(180, 5);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblSelectEQDir
            // 
            lblSelectEQDir.AutoSize = true;
            lblSelectEQDir.Location = new Point(12, 9);
            lblSelectEQDir.Name = "lblSelectEQDir";
            lblSelectEQDir.Size = new Size(162, 15);
            lblSelectEQDir.TabIndex = 2;
            lblSelectEQDir.Text = "Select an Everquest Directory:";
            // 
            // cBoxSrcChar
            // 
            cBoxSrcChar.DataSource = uiFileBindingSource;
            cBoxSrcChar.DisplayMember = "ServerCharacterName";
            cBoxSrcChar.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxSrcChar.Enabled = false;
            cBoxSrcChar.FormattingEnabled = true;
            cBoxSrcChar.Location = new Point(12, 53);
            cBoxSrcChar.Name = "cBoxSrcChar";
            cBoxSrcChar.Size = new Size(228, 23);
            cBoxSrcChar.TabIndex = 3;
            cBoxSrcChar.ValueMember = "Path";
            cBoxSrcChar.SelectedIndexChanged += cBoxSrcChar_SelectedIndexChanged;
            // 
            // uiFileBindingSource
            // 
            uiFileBindingSource.DataSource = typeof(UIFile);
            // 
            // btnSync
            // 
            btnSync.Enabled = false;
            btnSync.Location = new Point(152, 263);
            btnSync.Name = "btnSync";
            btnSync.Size = new Size(88, 23);
            btnSync.TabIndex = 5;
            btnSync.Text = "Sync UI Files";
            btnSync.UseVisualStyleBackColor = true;
            btnSync.Click += btnSync_Click;
            // 
            // lblSourceChar
            // 
            lblSourceChar.AutoSize = true;
            lblSourceChar.Location = new Point(12, 35);
            lblSourceChar.Name = "lblSourceChar";
            lblSourceChar.Size = new Size(114, 15);
            lblSourceChar.TabIndex = 7;
            lblSourceChar.Text = "Source Character UI:";
            // 
            // lblTgtChars
            // 
            lblTgtChars.AutoSize = true;
            lblTgtChars.Location = new Point(12, 89);
            lblTgtChars.Name = "lblTgtChars";
            lblTgtChars.Size = new Size(124, 15);
            lblTgtChars.TabIndex = 8;
            lblTgtChars.Text = "Target Character(s) UI:";
            // 
            // listBoxLogger
            // 
            listBoxLogger.FormattingEnabled = true;
            listBoxLogger.HorizontalScrollbar = true;
            listBoxLogger.ItemHeight = 15;
            listBoxLogger.Location = new Point(261, 27);
            listBoxLogger.Name = "listBoxLogger";
            listBoxLogger.Size = new Size(520, 259);
            listBoxLogger.TabIndex = 9;
            // 
            // lblLog
            // 
            lblLog.AutoSize = true;
            lblLog.Location = new Point(261, 9);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(30, 15);
            lblLog.TabIndex = 10;
            lblLog.Text = "Log:";
            // 
            // chkListBoxTgtChars
            // 
            chkListBoxTgtChars.CheckOnClick = true;
            chkListBoxTgtChars.Enabled = false;
            chkListBoxTgtChars.FormattingEnabled = true;
            chkListBoxTgtChars.HorizontalScrollbar = true;
            chkListBoxTgtChars.Location = new Point(12, 107);
            chkListBoxTgtChars.Name = "chkListBoxTgtChars";
            chkListBoxTgtChars.Size = new Size(228, 148);
            chkListBoxTgtChars.TabIndex = 11;
            chkListBoxTgtChars.SelectedIndexChanged += chkListBoxTgtChars_SelectedIndexChanged;
            chkListBoxTgtChars.DoubleClick += chkListBoxTgtChars_DoubleClick;
            // 
            // chkBoxSelectAll
            // 
            chkBoxSelectAll.AutoSize = true;
            chkBoxSelectAll.Enabled = false;
            chkBoxSelectAll.Location = new Point(12, 266);
            chkBoxSelectAll.Name = "chkBoxSelectAll";
            chkBoxSelectAll.Size = new Size(74, 19);
            chkBoxSelectAll.TabIndex = 12;
            chkBoxSelectAll.Text = "Select All";
            chkBoxSelectAll.UseVisualStyleBackColor = true;
            chkBoxSelectAll.Click += chkBoxSelectAll_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(793, 298);
            Controls.Add(chkBoxSelectAll);
            Controls.Add(chkListBoxTgtChars);
            Controls.Add(lblLog);
            Controls.Add(listBoxLogger);
            Controls.Add(lblTgtChars);
            Controls.Add(lblSourceChar);
            Controls.Add(btnSync);
            Controls.Add(cBoxSrcChar);
            Controls.Add(lblSelectEQDir);
            Controls.Add(btnBrowse);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EverQuest UI Sync";
            ((System.ComponentModel.ISupportInitialize)uiFileBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog eqFolderBrowserDialog;
        private BindingSource uiFileBindingSource;
        private Label lblSelectEQDir;
        private Label lblSourceChar;
        private Label lblTgtChars;
        private Label lblLog;
        private Button btnBrowse;
        private Button btnSync;
        private ComboBox cBoxSrcChar;
        private CheckBox chkBoxSelectAll;
        private CheckedListBox chkListBoxTgtChars;
        private ListBox listBoxLogger;
    }
}
