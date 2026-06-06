using Football_Manager.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class PlayersStatisticsForm : Form
    {
        private readonly PlayerService _playerService = new PlayerService();
        private DataTable _dtTopScorers;
        private DataTable _dtTopCards;
        private DataTable _dtTopFouls;

        // Модерна светла премиум палитра
        private readonly Color _formBgColor = Color.FromArgb(245, 247, 250);     // Мек сив фон за формата
        private readonly Color _textColorDark = Color.FromArgb(43, 48, 58);       // Дълбоко тъмно сиво за основни текстове
        private readonly Color _textColorMuted = Color.FromArgb(140, 148, 160);   // Деликатно сиво за отборите

        public PlayersStatisticsForm()
        {
            InitializeComponent();
            ApplyLightPremiumStyles();
        }

        private void ApplyLightPremiumStyles()
        {
            this.BackColor = _formBgColor;

            // Настройване на заглавията над панелите
            Font headerFont = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            label1.ForeColor = _textColorDark;
            label1.Font = headerFont;

            label2.ForeColor = _textColorDark;
            label2.Font = headerFont;

            label3.ForeColor = _textColorDark;
            label3.Font = headerFont;
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
                _dtTopFouls = _playerService.GetTop3ByFouls();

                panelPodium.Invalidate();
                panelMostCards.Invalidate();
                panelMostFauls.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на статистиката: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. ПОДИУМ (СВЕТЪЛ СТИЛ + РЪЧНО НАРИСУВАНА КОРОНА)
        private void panelPodium_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int totalWidth = panelPodium.Width;
            int totalHeight = panelPodium.Height;

            g.Clear(panelPodium.BackColor);

            int columnWidth = (totalWidth / 3) - 20;
            int maxBarHeight = totalHeight - 140;
            int baseline = totalHeight - 70;

            int x2 = 10;
            int x1 = columnWidth + 30;
            int x3 = (columnWidth * 2) + 50;

            // Елегантна тънка линия за основа
            using (Pen linePen = new Pen(Color.FromArgb(220, 224, 230), 2))
            {
                g.DrawLine(linePen, 5, baseline, totalWidth - 5, baseline);
            }

            int rowsCount = (_dtTopScorers != null) ? _dtTopScorers.Rows.Count : 0;
            int leaderGoals = 1;
            if (rowsCount > 0 && _dtTopScorers.Rows[0]["goals_count"] != DBNull.Value)
            {
                leaderGoals = Convert.ToInt32(_dtTopScorers.Rows[0]["goals_count"]);
                if (leaderGoals <= 0) leaderGoals = 1;
            }

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };

            Font fontLeaderName = new Font("Segoe UI", 12F, FontStyle.Bold);
            Font fontLeaderGoals = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontClub = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            Font fontStandardName = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fontStandardGoals = new Font("Segoe UI", 12.5F, FontStyle.Bold);

            // --- 1-во Място (Златен мек градиент) ---
            int h1 = maxBarHeight;
            Rectangle rect1 = new Rectangle(x1, baseline - h1, columnWidth, h1);
            using (LinearGradientBrush goldGrad = new LinearGradientBrush(rect1, Color.FromArgb(255, 223, 100), Color.FromArgb(245, 175, 25), LinearGradientMode.Vertical))
            {
                FillRoundedTopRectangle(g, goldGrad, rect1, 10);
            }

            if (rowsCount > 0)
            {
                DataRow r1 = _dtTopScorers.Rows[0];

                // РЪЧНО РИСУВАНЕ НА ЗЛАТНА КОРОНА (вместо емоджи)
                int crownWidth = 24; int crownHeight = 16;
                int crownX = x1 + (columnWidth / 2) - (crownWidth / 2);
                int crownY = baseline - h1 - 50;
                Point[] crownPoints = {
                    new Point(crownX, crownY + crownHeight),
                    new Point(crownX, crownY),
                    new Point(crownX + 6, crownY + 8),
                    new Point(crownX + 12, crownY),
                    new Point(crownX + 18, crownY + 8),
                    new Point(crownX + crownWidth, crownY),
                    new Point(crownX + crownWidth, crownY + crownHeight)
                };
                using (SolidBrush crownBrush = new SolidBrush(Color.FromArgb(245, 175, 25))) g.FillPolygon(crownBrush, crownPoints);

                g.DrawString($"{r1["goals_count"]} гола", fontLeaderGoals, new SolidBrush(Color.FromArgb(210, 130, 0)), x1 + (columnWidth / 2), baseline - h1 - 30, sf);
                g.DrawString(r1["full_name"].ToString(), fontLeaderName, new SolidBrush(_textColorDark), x1 + (columnWidth / 2), baseline + 8, sf);
                g.DrawString(r1["club_name"].ToString(), fontClub, new SolidBrush(_textColorMuted), x1 + (columnWidth / 2), baseline + 28, sf);
            }

            // --- 2-ро Място (Сребърен лек градиент) ---
            int h2 = (rowsCount > 1) ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[1]["goals_count"]) / leaderGoals * (maxBarHeight * 0.8)) : (int)(maxBarHeight * 0.75);
            if (h2 < 30) h2 = 30;
            Rectangle rect2 = new Rectangle(x2, baseline - h2, columnWidth, h2);
            using (LinearGradientBrush silverGrad = new LinearGradientBrush(rect2, Color.FromArgb(230, 235, 240), Color.FromArgb(185, 195, 205), LinearGradientMode.Vertical))
            {
                FillRoundedTopRectangle(g, silverGrad, rect2, 10);
            }

            if (rowsCount > 1)
            {
                DataRow r2 = _dtTopScorers.Rows[1];
                g.DrawString($"{r2["goals_count"]} гола", fontStandardGoals, new SolidBrush(Color.FromArgb(110, 125, 140)), x2 + (columnWidth / 2), baseline - h2 - 28, sf);
                g.DrawString(r2["full_name"].ToString(), fontStandardName, new SolidBrush(_textColorDark), x2 + (columnWidth / 2), baseline + 8, sf);
                g.DrawString(r2["club_name"].ToString(), fontClub, new SolidBrush(_textColorMuted), x2 + (columnWidth / 2), baseline + 28, sf);
            }

            // --- 3-то Място (Пастелен бронзов градиент) ---
            int h3 = (rowsCount > 2) ? (int)((double)Convert.ToInt32(_dtTopScorers.Rows[2]["goals_count"]) / leaderGoals * (maxBarHeight * 0.6)) : (int)(maxBarHeight * 0.5);
            if (h3 < 20) h3 = 20;
            Rectangle rect3 = new Rectangle(x3, baseline - h3, columnWidth, h3);
            using (LinearGradientBrush bronzeGrad = new LinearGradientBrush(rect3, Color.FromArgb(245, 190, 150), Color.FromArgb(200, 120, 70), LinearGradientMode.Vertical))
            {
                FillRoundedTopRectangle(g, bronzeGrad, rect3, 10);
            }

            if (rowsCount > 2)
            {
                DataRow r3 = _dtTopScorers.Rows[2];
                g.DrawString($"{r3["goals_count"]} гола", fontStandardGoals, new SolidBrush(Color.FromArgb(165, 90, 45)), x3 + (columnWidth / 2), baseline - h3 - 28, sf);
                g.DrawString(r3["full_name"].ToString(), fontStandardName, new SolidBrush(_textColorDark), x3 + (columnWidth / 2), baseline + 8, sf);
                g.DrawString(r3["club_name"].ToString(), fontClub, new SolidBrush(_textColorMuted), x3 + (columnWidth / 2), baseline + 28, sf);
            }
        }

        // 2. ХОРИЗОНТАЛНИ КАРТОНИ (ИЗЧИСТЕНИ ЛЕНТИ БЕЗ ЕМОДЖИТА)
        private void panelMostCards_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_dtTopCards == null || _dtTopCards.Rows.Count == 0)
            {
                using (Font infoFont = new Font("Segoe UI", 13, FontStyle.Italic))
                {
                    g.DrawString("Няма налични данни за картони.", infoFont, new SolidBrush(_textColorMuted), 15, 15);
                }
                return;
            }

            int totalWidth = panelMostCards.Width;
            Font fontNameLeader = new Font("Segoe UI", 11F, FontStyle.Bold);
            Font fontNumberLeader = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            Font fontNameStandard = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            Font fontNumberStandard = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            Font fontClub = new Font("Segoe UI", 9.5F, FontStyle.Italic);

            int startY = 20;
            int rowGap = 85;
            int barHeight = 22;
            int barGap = 6;
            int maxRows = Math.Min(5, _dtTopCards.Rows.Count);

            int nameX = 15;
            int barStartX = 210;
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
                string clubName = row["club_name"].ToString();
                int yellow = Convert.ToInt32(row["yellow_cards"]);
                int red = Convert.ToInt32(row["red_cards"]);

                int playerY = startY + (i * rowGap);

                Font currentNameFont = (i == 0) ? fontNameLeader : fontNameStandard;
                Font currentNumberFont = (i == 0) ? fontNumberLeader : fontNumberStandard;

                // Маркираме лидера с лек червен нюанс в текста, за да хваща окото без емоджи
                Brush nameColor = (i == 0) ? new SolidBrush(Color.FromArgb(220, 50, 70)) : new SolidBrush(_textColorDark);

                g.DrawString(fullName, currentNameFont, nameColor, nameX, playerY + 4);
                g.DrawString(clubName, fontClub, new SolidBrush(_textColorMuted), nameX, playerY + 26);

                // Червен картон (Гладка заоблена пастелна лента)
                int redBarW = (red > 0) ? Math.Max(16, (int)((double)red / maxCount * barAreaWidth)) : 0;
                int redY = playerY;
                if (redBarW > 0)
                {
                    Rectangle redRect = new Rectangle(barStartX, redY, redBarW, barHeight);
                    using (LinearGradientBrush redGrad = new LinearGradientBrush(redRect, Color.FromArgb(255, 110, 120), Color.FromArgb(230, 45, 60), LinearGradientMode.Horizontal))
                    using (GraphicsPath p = GetFullyRoundedRectPath(redRect, 7))
                    {
                        g.FillPath(redGrad, p);
                    }
                }
                g.DrawString(red.ToString(), currentNumberFont, new SolidBrush(Color.FromArgb(230, 45, 60)), barStartX + redBarW + 10, redY + (barHeight / 2) - (currentNumberFont.Height / 2));

                // Жълт картон (Гладка заоблена пастелна лента)
                int yellowBarW = (yellow > 0) ? Math.Max(16, (int)((double)yellow / maxCount * barAreaWidth)) : 0;
                int yellowY = playerY + barHeight + barGap;
                if (yellowBarW > 0)
                {
                    Rectangle yellowRect = new Rectangle(barStartX, yellowY, yellowBarW, barHeight);
                    using (LinearGradientBrush yellowGrad = new LinearGradientBrush(yellowRect, Color.FromArgb(255, 225, 60), Color.FromArgb(240, 180, 0), LinearGradientMode.Horizontal))
                    using (GraphicsPath p = GetFullyRoundedRectPath(yellowRect, 7))
                    {
                        g.FillPath(yellowGrad, p);
                    }
                }
                g.DrawString(yellow.ToString(), currentNumberFont, new SolidBrush(Color.FromArgb(210, 150, 0)), barStartX + yellowBarW + 10, yellowY + (barHeight / 2) - (currentNumberFont.Height / 2));

                // Много фина разделителна линия
                int lineY = yellowY + barHeight + 12;
                using (Pen linePen = new Pen(Color.FromArgb(240, 242, 245), 1))
                {
                    g.DrawLine(linePen, nameX, lineY, totalWidth - 15, lineY);
                }
            }
        }

        // 3. ФАЛОВЕ (СВЕТЛО СИНЬО + ВНИМАНИЕ: РЪЧНО НАРИСУВАН ТРИЪГЪЛНИК)
        private void panelMostFauls_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int totalWidth = panelMostFauls.Width;
            int totalHeight = panelMostFauls.Height;

            g.Clear(panelMostFauls.BackColor);

            if (_dtTopFouls == null || _dtTopFouls.Rows.Count == 0)
            {
                using (Font infoFont = new Font("Segoe UI", 13, FontStyle.Italic))
                {
                    g.DrawString("Няма налични данни за фалове.", infoFont, new SolidBrush(_textColorMuted), 15, 15);
                }
                return;
            }

            int rowsCount = _dtTopFouls.Rows.Count;
            int maxFouls = 1;
            if (rowsCount > 0 && _dtTopFouls.Rows[0]["fouls_count"] != DBNull.Value)
            {
                maxFouls = Convert.ToInt32(_dtTopFouls.Rows[0]["fouls_count"]);
                if (maxFouls <= 0) maxFouls = 1;
            }

            int baseline = totalHeight - 70;
            int maxBarHeight = totalHeight - 150;
            int columnWidth = totalWidth / 3;
            int barWidth = 44;

            Font fontLeaderName = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fontLeaderCount = new Font("Segoe UI", 14.5F, FontStyle.Bold);
            Font fontStandardName = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fontStandardCount = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            Font fontClub = new Font("Segoe UI", 9.5F, FontStyle.Italic);

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };

            using (Pen linePen = new Pen(Color.FromArgb(220, 224, 230), 2))
            {
                g.DrawLine(linePen, 5, baseline, totalWidth - 5, baseline);
            }

            for (int i = 0; i < rowsCount; i++)
            {
                DataRow row = _dtTopFouls.Rows[i];
                string fullName = row["full_name"].ToString();
                string clubName = row["club_name"].ToString();
                int fouls = Convert.ToInt32(row["fouls_count"]);

                int barHeight = (int)((double)fouls / maxFouls * maxBarHeight);
                if (barHeight < 25) barHeight = 25;

                int colX = i * columnWidth;
                int barX = colX + (columnWidth - barWidth) / 2;
                int barY = baseline - barHeight;
                Rectangle barRect = new Rectangle(barX, barY, barWidth, barHeight);

                // Премиум Океанско син градиент за фаловете
                using (LinearGradientBrush blueGrad = new LinearGradientBrush(barRect, Color.FromArgb(70, 190, 255), Color.FromArgb(30, 130, 230), LinearGradientMode.Vertical))
                {
                    FillRoundedTopRectangle(g, blueGrad, barRect, 10);
                }

                Font currentNameFont = (i == 0) ? fontLeaderName : fontStandardName;
                Font currentCountFont = (i == 0) ? fontLeaderCount : fontStandardCount;

                Color countColor = (i == 0) ? Color.FromArgb(0, 120, 230) : Color.FromArgb(70, 100, 130);
                Color nameColor = (i == 0) ? Color.FromArgb(0, 100, 200) : _textColorDark;

                // РЪЧНО РИСУВАНЕ НА МАЛЪК ПРЕДУПРЕДИТЕЛЕН ТРИЪГЪЛНИК пред числото на лидера
                if (i == 0)
                {
                    int triSize = 12;
                    int triX = colX + (columnWidth / 2) - 34; // Отместване вляво от текста
                    int triY = barY - 22;
                    Point[] triangle = {
                        new Point(triX + (triSize / 2), triY),
                        new Point(triX, triY + triSize),
                        new Point(triX + triSize, triY + triSize)
                    };
                    using (SolidBrush triBrush = new SolidBrush(Color.FromArgb(240, 150, 0))) g.FillPolygon(triBrush, triangle);
                }

                // Изчертаване на цифрата с брой фалове
                g.DrawString(fouls.ToString(), currentCountFont, new SolidBrush(countColor), colX + (columnWidth / 2), barY - 28, sf);

                // Име на играча и отбор
                g.DrawString(fullName, currentNameFont, new SolidBrush(nameColor), colX + (columnWidth / 2), baseline + 8, sf);
                g.DrawString(clubName, fontClub, new SolidBrush(_textColorMuted), colX + (columnWidth / 2), baseline + 28, sf);
            }
        }

        // =========================================================================
        // ПОМОЩНИ МЕТОДИ ЗА ГРАФИКА БЕЗ СИМВОЛИ (GDI+)
        // =========================================================================

        private void FillRoundedTopRectangle(Graphics g, Brush brush, Rectangle bounds, int radius)
        {
            using (GraphicsPath path = GetRoundedTopRectPath(bounds, radius))
            {
                g.FillPath(brush, path);
            }
        }

        private GraphicsPath GetRoundedTopRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (diameter > bounds.Width) diameter = bounds.Width;
            if (diameter > bounds.Height) diameter = bounds.Height;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddLine(bounds.Right, bounds.Bottom, bounds.X, bounds.Bottom);

            path.CloseFigure();
            return path;
        }

        private GraphicsPath GetFullyRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (diameter > bounds.Height) diameter = bounds.Height;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}