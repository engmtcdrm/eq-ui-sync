using System.Text.RegularExpressions;

namespace EQUISync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates the specified path is a valid EverQuest directory.
        /// </summary>
        /// <param name="path">The path to the EverQuest directory to valid.</param>
        /// <returns>true if it is a valid EverQuest directory and false otherwise.</returns>
        private bool ValidateEQPath(string path)
        {
            // Look for a few core EverQuest files to validate the path
            string[] requiredFiles = { "eqgame.exe", "eqclient.ini" };

            foreach (string file in requiredFiles)
            {
                if (!File.Exists(Path.Combine(path, file)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Updates the checked list box with the target characters based on the selected source character.
        /// </summary>
        private void UpdateCheckedBoxList()
        {
            chkListBoxTgtChars.Items.Clear();

            if (cBoxSrcChar.SelectedItem is UIFile selectedUIFile)
            {
                for (int i = 0; i < uiFileBindingSource.Count; i++)
                {
                    // Add only the target characters that are not the same as the selected source character
                    if (uiFileBindingSource[i] is UIFile uiFile && uiFile.Path != selectedUIFile.Path)
                    {
                        chkListBoxTgtChars.Items.Add(uiFileBindingSource[i], false);
                    }
                }
            }
        }

        /// <summary>
        /// Finds and loads UI files from the specified EverQuest directory.
        /// </summary>
        /// <param name="eqPath">The path to the EverQuest directory.</param>
        /// <returns>true if UI files were found and loaded successfully. false if there are no UI files found or there is an error.</returns>
        private bool FindAndLoadUIFiles(string eqPath)
        {
            // Get the list of UI files in the selected directory
            string[] foundUIFiles = Directory.GetFiles(eqPath, "UI_*.ini", SearchOption.AllDirectories);

            if (foundUIFiles.Length == 0)
            {
                listBoxLogger.Items.Add("No UI files found in the EverQuest directory. Please create a character or login as a character to create an initial file.");
                return false;
            }

            // Loop through each found UI file and add to appropriate controls and a list of all UI files
            for (int i = 0; i < foundUIFiles.Length; i++)
            {
                try
                {
                    UIFile newUIFile = new UIFile(foundUIFiles[i]);
                    uiFileBindingSource.Add(newUIFile);
                }
                catch (FormatException ex)
                {
                    listBoxLogger.Items.Add(ex.Message);
                    return false;
                }
            }

            if (uiFileBindingSource.Count == 0)
            {
                listBoxLogger.Items.Add("ERROR: No valid UI files found in the EverQuest directory.");
                return false;
            }

            // Populate the combo box with the UI files
            UpdateCheckedBoxList();

            listBoxLogger.Items.Add("Found UI files: " + Convert.ToString(uiFileBindingSource.Count));
            return true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Configure dialog properties (optional)
            eqFolderBrowserDialog.Description = "Select an EverQuest Folder";
            eqFolderBrowserDialog.UseDescriptionForTitle = true; // Only works on newer Windows versions
            eqFolderBrowserDialog.ShowNewFolderButton = false;

            if (eqFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Clear previous selections and logs
                listBoxLogger.Items.Clear();
                uiFileBindingSource.Clear();
                cBoxSrcChar.DataBindings.Clear();
                chkListBoxTgtChars.Items.Clear();

                // Validate the selected path
                if (!ValidateEQPath(eqFolderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show(this, "The selected folder is not a valid EverQuest Path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                listBoxLogger.Items.Add("Selected EQ Path: " + eqFolderBrowserDialog.SelectedPath);

                if (!FindAndLoadUIFiles(eqFolderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show(this, "An error has occurred see log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Enable various controls
                cBoxSrcChar.Enabled = true;
                chkListBoxTgtChars.Enabled = true;
                chkBoxSelectAll.Enabled = true;
            }
        }

        private void chkListBoxTgtChars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkListBoxTgtChars.CheckedItems.Count == 0)
            {
                btnSync.Enabled = false;
                chkBoxSelectAll.Checked = false;
                return;
            }

            if (chkListBoxTgtChars.CheckedItems.Count == chkListBoxTgtChars.Items.Count)
            {
                chkBoxSelectAll.Checked = true;
            }
            else
            {
                chkBoxSelectAll.Checked = false;
            }

            btnSync.Enabled = true;
        }

        private void cBoxSrcChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBoxSelectAll.Checked = false;
            UpdateCheckedBoxList();
        }

        private void chkBoxSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBoxTgtChars.Items.Count; i++)
            {
                chkListBoxTgtChars.SetItemChecked(i, chkBoxSelectAll.Checked);
            }

            if (chkListBoxTgtChars.Items.Count > 0 && chkBoxSelectAll.Checked)
            {
                btnSync.Enabled = true;
                return;
            }

            btnSync.Enabled = false;
        }

        /// <summary>
        /// Copies the source file to the target file path.
        /// </summary>
        /// <param name="srcPath">The source file path to copy.</param>
        /// <param name="tgtPath">The destination file path.</param>
        /// <returns>true if copy is successful, false otherwise.</returns>
        private bool CopyFile(string srcPath, string tgtPath)
        {
            try
            {
                File.Copy(srcPath, tgtPath, true);
                return true;
            }
            catch (IOException ex)
            {
                listBoxLogger.Items.Add("ERROR: " + ex.Message);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                listBoxLogger.Items.Add("ERROR: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                listBoxLogger.Items.Add("ERROR: " + ex.Message);
                return false;
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will overwrite the target UI files. Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.No)
            {
                return;
            }

            listBoxLogger.Items.Add("Syncing UI files...");
            listBoxLogger.Items.Add("");

            // Loop through each target character and copy the source character's UI file to the target character's UI file
            for (int i = 0; i < chkListBoxTgtChars.CheckedItems.Count; i++)
            {
                if (chkListBoxTgtChars.Items[i] is UIFile tgtFile && cBoxSrcChar.SelectedItem is UIFile srcFile)
                {
                    // If backup copy is successful, proceed to copy the source file to the target file
                    if (CopyFile(tgtFile.Path, tgtFile.Path + ".bak"))
                    {
                        listBoxLogger.Items.Add("Backup of '" + tgtFile.ServerCharacterName + "' UI File Successful!");

                        if (CopyFile(srcFile.Path, tgtFile.Path))
                        {
                            listBoxLogger.Items.Add("Sync of '" + srcFile.ServerCharacterName + "' UI File to '" + tgtFile.ServerCharacterName + "' UI File Successful!");
                        }
                        else
                        {
                            listBoxLogger.Items.Add("ERROR: Sync of '" + srcFile.ServerCharacterName + "' UI File to '" + tgtFile.ServerCharacterName + "' UI File Failed!");
                        }
                    }
                    else
                    {
                        listBoxLogger.Items.Add("ERROR: Backup of '" + tgtFile.ServerCharacterName + "' UI File Failed!");
                    }
                }

                listBoxLogger.Items.Add("");
            }

            listBoxLogger.Items.Add("Sync Complete!");
        }

        private void chkListBoxTgtChars_DoubleClick(object sender, EventArgs e)
        {
            if (chkListBoxTgtChars.CheckedItems.Count > 0)
            {
                btnSync.Enabled = true;
                return;
            }

            btnSync.Enabled = false;
        }
    }
}
