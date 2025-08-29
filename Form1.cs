using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortuneTeller
{
    public partial class Form1 : Form
    {
        List<string> results;
        public Form1()
        {
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            try
            {
                string filename = "results.csv";
                results = File.ReadAllLines(filename).ToList();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"파일을 불러올 수 없습니다. \n{ex.Message}", "파일 없음!");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"파일에 접근권한이 없습니다. \n{ex.Message}", "권한 문제!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류가 발생했습니다. \n{ex.Message}", "알 수 없는 오류!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 상ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 상담내역불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistory form = Application.OpenForms["FormHistory"] as FormHistory;
            if (form != null)
            {
                form.Activate();
            }
            else
            {
                form = new FormHistory(this);
                form.Show();
            }

        }
        
        private string GetBible()
        {
            Random random = new Random();
            int index = random.Next(0, results.Count);
            return results[index];
        }

        private void 끝내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 포츈텔러정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        private void btnShowResult_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string worth = tbWorth.Text;
            string result = GetBible();
            string message = $"{name}님이 중요하게 생각하는 가치는 {worth}입니다!\n\n 하나님께서 {name}님께 해주시는 성경 말씀:\n {result}";
            tbResult.Text = message;
            SaveHistory($"{name}님이 중요하게 생각하는 가치는 {worth}");
            FormHistory form = Application.OpenForms["FormHistory"] as FormHistory;
            if (form != null)
            {
                form.UpdateHistory();
            }
        }

        
        private void SaveHistory(string history)
        {
            try
            {
                string filename = "history.csv";
                File.AppendAllText(filename, history + Environment.NewLine);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"파일에 접근권한이 없습니다. \n{ex.Message}", "권한 문제!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류가 발생했습니다. \n{ex.Message}", "알 수 없는 오류!");
            }   
        }

        internal void LoadHistory(string history)
        {
            string name = history.Split('|')[0].Split(' ')[0];
            tbName.Text = name;
            string worth = history.Split('|')[1].Split(' ')[1];
            tbWorth.Text = worth;
            string result = history.Split('|')[1];
            string message = history.Split('|')[2];
            tbResult.Text = message;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
