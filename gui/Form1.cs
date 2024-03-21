using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        private TextBox textArea;
        private string lastSavedFilePath = null;
        private Stack<string> textHistory = new Stack<string>();
        private int searchIndex = 0;
        private const float DefaultZoomLevel = 12.0f;
        public Form1()
        {
            InitializeComponent();
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            fontToolStripMenuItem.Click += fontToolStripMenuItem_Click;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textArea = new TextBox();
            textArea.Multiline = true;
            textArea.Dock = DockStyle.Fill;
            textArea.TextChanged += TextArea_TextChanged;
            this.Controls.Add(textArea);
            textArea.BringToFront();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textArea = new TextBox();
            textArea.Multiline = true;
            textArea.Dock = DockStyle.Fill;
            textArea.TextChanged += TextArea_TextChanged;
            this.Controls.Add(textArea);
            textArea.BringToFront();
        }
        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            textHistory.Push(textArea.Text);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (textArea == null)
                {
                    textArea = new TextBox();
                    textArea.Multiline = true;
                    textArea.Dock = DockStyle.Fill;
                    this.Controls.Add(textArea);
                    textArea.BringToFront();
                }
                string filePath = openFileDialog.FileName;

                try
                {
                    string fileContent = File.ReadAllText(filePath);
                    textArea.Text = fileContent;

                    lastSavedFilePath = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null)
            {
                if (lastSavedFilePath != null)
                {
                    try
                    {

                        File.WriteAllText(lastSavedFilePath, textArea.Text);
                        MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Save File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            lastSavedFilePath = saveFileDialog.FileName;

                            File.WriteAllText(lastSavedFilePath, textArea.Text);
                            MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No text to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, textArea.Text);
                        MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No text to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private Font selectedFont;
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedFont == null)
            {
                FontDialog fontDialog = new FontDialog();

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFont = fontDialog.Font;
                    if (textArea != null)
                    {
                        textArea.Font = selectedFont;
                    }
                }
            }
            else
            {
                if (textArea != null)
                {
                    textArea.Font = selectedFont;
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textHistory.Count > 1)
            {
                textHistory.Pop();
                textArea.Text = textHistory.Peek(); 
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null && textArea.SelectionLength > 0)
            {
                textArea.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null && textArea.SelectionLength > 0)
            {
                textArea.Copy(); 
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null)
            {
                textArea.Paste(); 
            }
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = PromptUserForSearchText("Replace");
            if (!string.IsNullOrEmpty(searchText))
            {
                searchIndex = textArea.Text.IndexOf(searchText, searchIndex + 1, StringComparison.CurrentCultureIgnoreCase);
                if (searchIndex >= 0)
                {
                    textArea.Select(searchIndex, searchText.Length);
                    textArea.ScrollToCaret();
                }
                else
                {
                    MessageBox.Show("Text not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    searchIndex = 0;
                }
            }
        }
        private string PromptUserForSearchText(string v)
        {
            string searchText = null;
            using (var dialog = new Form())
            {
                dialog.Text = "Find";
                dialog.Size = new System.Drawing.Size(300, 150);

                var label = new Label();
                label.Text = "Enter text to find:";
                label.Location = new System.Drawing.Point(20, 20);
                dialog.Controls.Add(label);

                var textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(20, 50);
                dialog.Controls.Add(textBox);

                var findButton = new Button();
                findButton.Text = "Find Next";
                findButton.Location = new System.Drawing.Point(20, 80);
                findButton.DialogResult = DialogResult.OK;
                dialog.Controls.Add(findButton);

                dialog.AcceptButton = findButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchText = textBox.Text;
                }
            }
            return searchText;
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = PromptUserForSearchText("Replace");
            if (!string.IsNullOrEmpty(searchText))
            {
                int index = textArea.Text.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {
                    string replacementText = PromptUserForReplacementText(searchText);
                    if (!string.IsNullOrEmpty(replacementText))
                    {
                        textArea.Text = textArea.Text.Remove(index, searchText.Length).Insert(index, replacementText);
                        searchIndex = index + replacementText.Length;
                    }
                }
                else
                {
                    MessageBox.Show("Text not found.", "Replace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private string PromptUserForReplacementText(string searchText)
        {

            string replacementText = null;
            using (var dialog = new Form())
            {
                dialog.Text = "Replace";
                dialog.Size = new System.Drawing.Size(300, 200);

                var labelReplace = new Label();
                labelReplace.Text = $"Enter replacement text for \"{searchText}\":";
                labelReplace.Location = new System.Drawing.Point(20, 20);
                dialog.Controls.Add(labelReplace);

                var textBoxReplace = new TextBox();
                textBoxReplace.Location = new System.Drawing.Point(20, 50);
                dialog.Controls.Add(textBoxReplace);

                var replaceButton = new Button();
                replaceButton.Text = "Replace";
                replaceButton.Location = new System.Drawing.Point(20, 80);
                replaceButton.DialogResult = DialogResult.OK;
                dialog.Controls.Add(replaceButton);

                dialog.AcceptButton = replaceButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    replacementText = textBoxReplace.Text;
                }
            }
            return replacementText;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null)
            {
                float currentSize = textArea.Font.Size;
                float newSize = currentSize * 2.2f; 
                textArea.Font = new Font(textArea.Font.FontFamily, newSize);
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentSize = textArea.Font.Size;
            float newSize = currentSize / 2.2f;
            textArea.Font = new Font(textArea.Font.FontFamily, newSize);
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textArea != null)
            {
                textArea.Font = new Font(textArea.Font.FontFamily, DefaultZoomLevel);
            }
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://support.microsoft.com/en-us/windows/help-in-notepad-4d68c388-2ff2-0e7f-b706-35fb2ab88a8c";
            Process.Start(url);
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendFeedback s  = new sendFeedback();
            s.Show();
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadDevelopers n = new notepadDevelopers();
            n.Show();
        }
    }
}
