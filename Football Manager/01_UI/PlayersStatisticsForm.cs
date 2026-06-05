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
        private DataTable _dtTopCards;

        public PlayersStatisticsForm()
        {
            InitializeComponent();
        }

        private void PlayersStatisticsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _dtTopScorers = _playerService.GetTop3Scorers();
                _dtTopCards = _playerService.GetTopPlayersByCards();

                panelPodium.Invalidate();
                panelMostCards.Invalidate();
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
            int maxBarHeight = totalHeight - 140;
            int baseline = totalHeight - 60;

            int x2 = 10;
            int x1 = columnWidth + 30;
            int x3 = (columnWidth * 2) + 50;

            g.DrawLine(Pens.DarkGray, 5, baseline, totalWidth - 5, baseline);

            int rowsCount = (_dtTopScorers != null) ? _dtTopScorers.Rows.Count : 0;
            int leaderGoals = 1;
            if (rowsCount > 0 && _dtTopScorers.Rows[0]["goals_count"] != DBNull.Value)
            {
                leaderGoals = Convert.ToInt32(_dtTopScorers.Rows[0]["goals_count"]);
                if (leaderGoals <= 0) leaderGoals = 1;
            }

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };

            // Шрифтове за подиума (Номер 1 е с по-голям размер от останалите)
            Font fontLeaderName = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontLeaderGoals = new Font("Segoe UI", 15, FontStyle.Bold);

            Font fontStandardName = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fontStandardGoals = new Font("Segoe UI", 13, FontStyle.Bold);

            // 1. ПЪРВО МЯСТО (ЗЛАТО)
            int h1 = maxBarHeight;
            Rectangle rect1 = new Rectangle(x1, baseline - h1, columnWidth, h1);
            using (Brush goldBrush = new SolidBrush(Color.FromArgb(255, 215, 0))) g.FillRectangle(goldBrush, rect1);
            g.DrawRectangle(Pens.Orange, rect1);

            if (rowsCount > 0)
            {
                DataRow r1 = _dtTopScorers.Rows[0];
                g.DrawString($"{r1["goals_count"]} гола", fontLeaderGoals, Brushes.DarkGoldenrod, x1 + (columnWidth / 2), baseline - h1 - 30, sf);
                g.DrawString(r1["full_name"].ToString(), fontLeaderName, Brushes.Black, x1 + (columnWidth / 2), baseline + 10, sf);
            }

            // 2. ВТОРО МЯСТО (СРЕБРО)
            int h2 = (rowsCount > 1) ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[1]["goals_count"]) / leaderGoals * (maxBarHeight * 0.8)) : (int)(maxBarHeight * 0.75);
            if (h2 < 30) h2 = 30;
            Rectangle rect2 = new Rectangle(x2, baseline - h2, columnWidth, h2);
            g.FillRectangle(Brushes.Silver, rect2);
            g.DrawRectangle(Pens.DarkGray, rect2);

            if (rowsCount > 1)
            {
                DataRow r2 = _dtTopScorers.Rows[1];
                g.DrawString($"{r2["goals_count"]} гола", fontStandardGoals, Brushes.DimGray, x2 + (columnWidth / 2), baseline - h2 - 28, sf);
                g.DrawString(r2["full_name"].ToString(), fontStandardName, Brushes.Black, x2 + (columnWidth / 2), baseline + 10, sf);
            }

            // 3. ТРЕТО МЯСТО (БРОНЗ)
            int h3 = (rowsCount > 2) ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[2]["goals_count"]) / leaderGoals * (maxBarHeight * 0.6)) : (int)(maxBarHeight * 0.5);
            if (h3 < 20) h3 = 20;
            Rectangle rect3 = new Rectangle(x3, baseline - h3, columnWidth, h3);
            using (Brush bronzeBrush = new SolidBrush(Color.FromArgb(205, 127, 50))) g.FillRectangle(bronzeBrush, rect3);
            g.DrawRectangle(Pens.SaddleBrown, rect3);

            if (rowsCount > 2)
            {
                DataRow r3 = _dtTopScorers.Rows[2];
                g.DrawString($"{r3["goals_count"]} гола", fontStandardGoals, Brushes.SaddleBrown, x3 + (columnWidth / 2), baseline - h3 - 28, sf);
                g.DrawString(r3["full_name"].ToString(), fontStandardName, Brushes.Black, x3 + (columnWidth / 2), baseline + 10, sf);
            }
        }

        private void panelMostCards_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_dtTopCards == null || _dtTopCards.Rows.Count == 0)
            {
                using (Font infoFont = new Font("Segoe UI", 14, FontStyle.Italic))
                {
                    g.DrawString("Няма налични данни за картони.", infoFont, Brushes.Gray, 15, 15);
                }
                return;
            }

            int totalWidth = panelMostCards.Width;
            int totalHeight = panelMostCards.Height;

            // Шрифтове за картони (Премахнато е Underline и MidnightBlue от лидера)
            Font fontNameLeader = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontNumberLeader = new Font("Segoe UI", 15, FontStyle.Bold);

            Font fontNameStandard = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fontNumberStandard = new Font("Segoe UI", 13, FontStyle.Bold);

            int startY = 20;
            int rowGap = 85;
            int barHeight = 30;
            int barGap = 6;
            int maxRows = Math.Min(5, _dtTopCards.Rows.Count);

            int nameX = 15;
            int barStartX = 210; // Увеличено, за да не се застъпва от по-големия шрифт на имената
            int barAreaWidth = totalWidth - barStartX - 70;

            int maxCount = 1;
            for (int i = 0; i < maxRows; i++)
            {
                int y = Convert.ToInt32(_dtTopCards.Rows[i]["yellow_cards"]);
                int r = Convert.ToInt32(_dtTopCards.Rows[i]["red_cards"]);
                if (y > maxCount) maxCount = y;
                if (r > maxCount) maxCount = r;
            }

            for (int i = 0; i < maxRows; i++)
            {
                DataRow row = _dtTopCards.Rows[i];
                string fullName = row["full_name"].ToString();
                int yellow = Convert.ToInt32(row["yellow_cards"]);
                int red = Convert.ToInt32(row["red_cards"]);

                int playerY = startY + (i * rowGap);

                Font currentNameFont = (i == 0) ? fontNameLeader : fontNameStandard;
                Font currentNumberFont = (i == 0) ? fontNumberLeader : fontNumberStandard;

                g.DrawString(fullName, currentNameFont, Brushes.Black, nameX, playerY + 15);

                // Червен стълб
                int redBarW = (red > 0) ? Math.Max(12, (int)((double)red / maxCount * barAreaWidth)) : 0;
                int redY = playerY;

                if (redBarW > 0)
                {
                    Rectangle redRect = new Rectangle(barStartX, redY, redBarW, barHeight);
                    using (Brush b = new SolidBrush(Color.FromArgb(220, 53, 69))) g.FillRectangle(b, redRect);
                }

                int redNumberX = barStartX + redBarW + 10;
                int redNumberY = redY + (barHeight / 2) - (currentNumberFont.Height / 2);
                g.DrawString(red.ToString(), currentNumberFont, new SolidBrush(Color.FromArgb(220, 53, 69)), redNumberX, redNumberY);

                // Жълт стълб
                int yellowBarW = (yellow > 0) ? Math.Max(12, (int)((double)yellow / maxCount * barAreaWidth)) : 0;
                int yellowY = playerY + barHeight + barGap;

                if (yellowBarW > 0)
                {
                    Rectangle yellowRect = new Rectangle(barStartX, yellowY, yellowBarW, barHeight);
                    using (Brush b = new SolidBrush(Color.FromArgb(255, 204, 0))) g.FillRectangle(b, yellowRect);
                }

                int yellowNumberX = barStartX + yellowBarW + 10;
                int yellowNumberY = yellowY + (barHeight / 2) - (currentNumberFont.Height / 2);
                g.DrawString(yellow.ToString(), currentNumberFont, new SolidBrush(Color.FromArgb(190, 140, 0)), yellowNumberX, yellowNumberY);

                int lineY = yellowY + barHeight + 12;
                using (Pen linePen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    g.DrawLine(linePen, nameX, lineY, totalWidth - 15, lineY);
                }
            }
        }
    }
}