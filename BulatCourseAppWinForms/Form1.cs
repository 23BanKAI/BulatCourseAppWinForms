using System.Collections.Generic;

namespace BulatCourseAppWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            DialogResult result = openFileDialog1.ShowDialog();
            // If the user clicks OK
            if (result == DialogResult.OK)
            {
                // Get the filename
                string filename = openFileDialog1.FileName;

                // Open the file using the default application
                textBox1.Text = filename;

                using (StreamReader reader = new StreamReader(filename))
                {
                    string fileContent = File.ReadAllText(filename);
                    string[] rows = fileContent.Split('\n');
                    int rowCount = rows.Length;

                    int columnCount = rows[0].Split(',').Length;
                    int[,] array = new int[rowCount, columnCount];

                    for (int i = 0; i < rowCount; i++)
                    {
                        string[] columns = rows[i].Split(',');
                        for (int j = 0; j < columnCount; j++)
                        {
                            array[i, j] = int.Parse(columns[j]);
                        }
                    }

                    // Call the method that processes the array
                    HungarianAlgorithm hungarianAlgorithm = new HungarianAlgorithm();
                    hungarianAlgorithm.Algorithm(array);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Filter = "Text Files|*.txt|All Files|*.*";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileContents = File.ReadAllText(fileDialog.FileName);
            richTextBox1.Text = fileContents;
        }
    }
}