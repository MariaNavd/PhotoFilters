using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoFilter
{
    public partial class Form1 : Form
    {
        Bitmap image;
        int[,] structElem;
        protected int iterations = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp; *.jpeg; | All files (*.*) | *.*";
            dialog.ShowDialog();
            if (dialog.FileName != null)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void invertingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new InvertFilter();
            List<Filters> filtersList = new List<Filters>() { new InvertFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = image;
            Bitmap oldImage = image;
            List<Filters> filtersList = e.Argument as List<Filters>;
            
            foreach (Filters filter in filtersList)
            {
                newImage = filter.processImage(oldImage, backgroundWorker1, structElem);
                oldImage = newImage;
            }
            //Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1, structElem);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void shadesOfGreyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new ShadesOfGreyFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new SepiaFilter(10) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new BlackAndWhiteFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new BrightnessFilter(30) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void sharpnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new SharpnessFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void sobelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new SobelFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void prewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new PrewittFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void scharrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new ScharrFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new BlurFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void motionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new MotionBlurFilter(5) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void glassEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new GlassEffectFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void wavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new WavesFilter() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new RotationFilter(0.88) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void rows_TextChanged(object sender, EventArgs e)
        {
            int rowsCount;
            if (int.TryParse(rows.Text, out rowsCount))
                if (rowsCount > 0)
                    dataGridView1.RowCount = rowsCount;
        }

        private void columns_TextChanged(object sender, EventArgs e)
        {
            int columnsCount;
            if (int.TryParse(columns.Text, out columnsCount))
                if (columnsCount > 0)
                {
                    dataGridView1.ColumnCount = columnsCount;
                    //foreach (DataGridViewColumn column in dataGridView1.Columns)
                       // column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            structElem = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
            StreamWriter sw = new StreamWriter("C:\\Users\\m.navdaeva\\Documents\\Инженерная графика\\arr.txt");

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                {
                    int.TryParse(dataGridView1.Rows[i].Cells[j].Value.ToString(),
                            out structElem[i, j]);
                    //sw.Write(structElem[i, j] + " ");
                }
                //sw.Write("\n");
            }

            sw.Close();
            mathematicalMorphologyToolStripMenuItem.Enabled = true;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                    dataGridView1.Rows[i].Cells[j].Value = "";
        }

        private void upload_Click(object sender, EventArgs e)
        {
            StreamReader sr;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt; *.log; | All files (*.*) | *.*";
            dialog.ShowDialog();
            if (dialog.FileName != null)
            {
                sr = new StreamReader(dialog.FileName);
                string line = sr.ReadToEnd();

                string[] rows = line.Split('\n');
                dataGridView1.RowCount = rows.Length;
                dataGridView1.ColumnCount = rows[0].Split(' ').Length;

                for (int i = 0; i < rows.Length; i++)
                {
                    string[] cols = rows[i].Split(' ');
                    for (int j = 0; j < cols.Length; j++)
                        dataGridView1.Rows[i].Cells[j].Value = cols[j];
                }
                sr.Close();
            } 
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new Dilation() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new Erosion() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new Erosion(), new Dilation() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter1 = new Dilation();
            //backgroundWorker1.RunWorkerAsync(filter1);

            //while(backgroundWorker1.IsBusy)
            //  Application.DoEvents();

            //Filters filter2 = new Erosion();
            //if(!backgroundWorker1.IsBusy)
            //backgroundWorker1.RunWorkerAsync(filter2);

            List<Filters> filtersList = new List<Filters>() { new Dilation(), new Erosion() };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void perfectReflectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new PerfectReflectionFilter(image) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }

        private void greyWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Filters> filtersList = new List<Filters>() { new GreyWorldFilter(image) };
            backgroundWorker1.RunWorkerAsync(filtersList);
        }
    }
}
