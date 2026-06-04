using Football_Manager.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class PlayersStatisticsForm : Form
    {
        private readonly PlayerService _playerService = new PlayerService();
        private DataTable _dtTopScorers;

        public PlayersStatisticsForm()
        {
            InitializeComponent();
            panelPodium.Paint += panelPodium_Paint;
        }

        private void PlayersStatisticsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Вземаме данните от базата през BLL
                _dtTopScorers = _playerService.GetTop3Scorers();

                // Караме контролата да се пренарисува
                panelPodium.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на статистиката: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelPodium_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int totalWidth = panelPodium.Width;
            int totalHeight = panelPodium.Height;

            g.Clear(panelPodium.BackColor);

            int columnWidth = (totalWidth / 3) - 20;
            int maxBarHeight = totalHeight - 120;
            int baseline = totalHeight - 60;

            int x2 = 10;
            int x1 = columnWidth + 30;
            int x3 = (columnWidth * 2) + 50;

            Font nameFont = new Font("Segoe UI", 9, FontStyle.Regular);
            Font boldFont = new Font("Segoe UI", 11, FontStyle.Bold);

            g.DrawLine(Pens.DarkGray, 5, baseline, totalWidth - 5, baseline);

            // Вземаме реалния брой редове от базата данни
            int rowsCount = (_dtTopScorers != null) ? _dtTopScorers.Rows.Count : 0;

            int leaderGoals = 1;
            if (rowsCount > 0 && _dtTopScorers.Rows[0]["goals_count"] != DBNull.Value)
            {
                leaderGoals = Convert.ToInt32(_dtTopScorers.Rows[0]["goals_count"]);
                if (leaderGoals <= 0) leaderGoals = 1;
            }

            // 1. ПЪРВО МЯСТО (ЗЛАТО)
            int h1 = maxBarHeight;
            Rectangle rect1 = new Rectangle(x1, baseline - h1, columnWidth, h1);
            using (Brush goldBrush = new SolidBrush(Color.FromArgb(255, 215, 0)))
            {
                g.FillRectangle(goldBrush, rect1);
            }
            g.DrawRectangle(Pens.Orange, rect1);

            if (rowsCount > 0)
            {
                DataRow r1 = _dtTopScorers.Rows[0];
                g.DrawString($"{r1["goals_count"]} гола", boldFont, Brushes.DarkGoldenrod, x1 + 5, baseline - h1 - 25);
                g.DrawString(r1["full_name"].ToString(), new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, x1, baseline + 10);
            }
            else
            {
                g.DrawString("-", boldFont, Brushes.DarkGoldenrod, x1 + columnWidth / 2 - 5, baseline - h1 - 25);
                g.DrawString("-", nameFont, Brushes.Black, x1 + columnWidth / 2 - 5, baseline + 10);
            }

            // 2. ВТОРО МЯСТО (СРЕБРО)
            int h2 = (rowsCount > 1)
                ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[1]["goals_count"]) / leaderGoals * (maxBarHeight * 0.8))
                : (int)(maxBarHeight * 0.75);
            if (h2 < 30) h2 = 30;

            Rectangle rect2 = new Rectangle(x2, baseline - h2, columnWidth, h2);
            g.FillRectangle(Brushes.Silver, rect2);
            g.DrawRectangle(Pens.DarkGray, rect2);

            if (rowsCount > 1)
            {
                DataRow r2 = _dtTopScorers.Rows[1];
                g.DrawString($"{r2["goals_count"]} гола", boldFont, Brushes.DimGray, x2 + 5, baseline - h2 - 25);
                g.DrawString(r2["full_name"].ToString(), nameFont, Brushes.Black, x2, baseline + 10);
            }
            else
            {
                g.DrawString("-", boldFont, Brushes.DimGray, x2 + columnWidth / 2 - 5, baseline - h2 - 25);
                g.DrawString("-", nameFont, Brushes.Black, x2 + columnWidth / 2 - 5, baseline + 10);
            }

            // 3. ТРЕТО МЯСТО (БРОНЗ)
            int h3 = (rowsCount > 2)
                ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[2]["goals_count"]) / leaderGoals * (maxBarHeight * 0.6))
                : (int)(maxBarHeight * 0.5);
            if (h3 < 20) h3 = 20;

            Rectangle rect3 = new Rectangle(x3, baseline - h3, columnWidth, h3);
            using (Brush bronzeBrush = new SolidBrush(Color.FromArgb(205, 127, 50)))
            {
                g.FillRectangle(bronzeBrush, rect3);
            }
            g.DrawRectangle(Pens.SaddleBrown, rect3);

            if (rowsCount > 2)
            {
                DataRow r3 = _dtTopScorers.Rows[2];
                g.DrawString($"{r3["goals_count"]} гола", boldFont, Brushes.SaddleBrown, x3 + 5, baseline - h3 - 25);
                g.DrawString(r3["full_name"].ToString(), nameFont, Brushes.Black, x3, baseline + 10);
            }
            else
            {
                g.DrawString("-", boldFont, Brushes.SaddleBrown, x3 + columnWidth / 2 - 5, baseline - h3 - 25);
                g.DrawString("-", nameFont, Brushes.Black, x3 + columnWidth / 2 - 5, baseline + 10);
            }
        }
    }
}