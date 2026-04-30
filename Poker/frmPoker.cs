using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Poker
{
    public partial class frmPoker : Form
    {
        #region 欄位
        /// <summary>
        /// 用來存放牌桌上五張牌的 PictureBox 陣列
        /// </summary>
        PictureBox[] pic = new PictureBox[5];

        /// <summary>
        /// 所有的牌的編號，從 0 到 51，對應到 52 張牌
        /// </summary>
        int[] allPoker = new int[52];

        /// <summary>
        /// 記錄玩家手牌的編號，從 0 到 51，對應到 52 張牌
        /// </summary>
        int[] playerPoker = new int[5];

        /// <summary>
        /// 玩家目前的總資金
        /// </summary>
        int totalFund = 1000000;

        /// <summary>
        /// 起始資金，用來計算整局遊戲的盈虧
        /// </summary>
        const int initialFund = 1000000;

        /// <summary>
        /// 玩家本局的押注金額（0 表示尚未下注）
        /// </summary>
        int betAmount = 0;

        /// <summary>
        /// 每局結算紀錄（局數、牌型、押注金額、盈虧、結算後總資金）
        /// </summary>
        private List<(int round, string hand, int bet, int change, int fund)> history
            = new List<(int, string, int, int, int)>();

        /// <summary>
        /// 累計局數
        /// </summary>
        private int roundCount = 0;

        #endregion

        public frmPoker()
        {
            InitializeComponent();
            InitializePoker();

            // 顯示歡迎訊息並初始化總資金顯示
            MessageBox.Show($"歡迎來到 Poker！\n你的起始資金為 {totalFund:N0} 元", "歡迎", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblTotalFund.Text = totalFund.ToString("N0");
        }


        #region 自定義方法
        private void InitializePoker()
        {
            for (int i = 0; i < pic.Length; i++)
            {
                pic[i] = new PictureBox();
                pic[i].Image = GetImage("back");
                pic[i].Name = "pic" + i;
                pic[i].SizeMode = PictureBoxSizeMode.AutoSize;
                pic[i].Top = 30;
                pic[i].Left = 10 + ((pic[i].Width + 10) * i);
                pic[i].Enabled = false;
                pic[i].Tag = "back";
                pic[i].Visible = true;
                pic[i].TabStop = false;

                this.grpPoker.Controls.Add(pic[i]);
                pic[i].Click += Pic_Click;
            }
        }

        /// <summary>
        /// 顯示五張撲克牌到桌面上
        /// </summary>
        private void ShowCards()
        {
            for (int i = 0; i < playerPoker.Length; i++)
            {
                pic[i].Image = this.GetImage($"pic{playerPoker[i] + 1}");
            }
        }

        /// <summary>
        /// 取得圖片資源
        /// </summary>
        private Image GetImage(string name)
        {
            return Properties.Resources.ResourceManager.GetObject(name) as Image;
        }

        /// <summary>
        /// 取得圖片資源
        /// </summary>
        private Image GetImage(int num)
        {
            return GetImage($"pic{num}");
        }

        /// <summary>
        /// 將 allPoker 陣列中的牌隨機打亂，模擬洗牌的過程
        /// </summary>
        private void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int r = rand.Next(allPoker.Length);
                int temp = allPoker[r];
                allPoker[r] = allPoker[0];
                allPoker[0] = temp;
            }
        }

        /// <summary>
        /// 根據牌型名稱回傳對應的賠率倍數
        /// </summary>
        private int GetOdds(string handName)
        {
            if (handName.Contains("同花大順")) return 250;
            if (handName.Contains("同花順")) return 50;
            if (handName.Contains("鐵支")) return 25;
            if (handName.Contains("葫蘆")) return 9;
            if (handName.Contains("同花")) return 6;
            if (handName.Contains("順子")) return 4;
            if (handName.Contains("三條")) return 3;
            if (handName.Contains("兩對")) return 2;
            if (handName.Contains("一對")) return 1;
            return 0;
        }

        /// <summary>
        /// 根據盈虧金額組合統計訊息字串
        /// </summary>
        private string BuildSummary(int diff)
        {
            if (diff > 0)
                return $"本次遊戲共賺了 {diff:N0} 元 🎉\n起始資金：{initialFund:N0} 元　目前資金：{totalFund:N0} 元";
            else if (diff < 0)
                return $"本次遊戲共虧了 {Math.Abs(diff):N0} 元 😢\n起始資金：{initialFund:N0} 元　目前資金：{totalFund:N0} 元";
            else
                return $"本次遊戲不賺不虧，打平！\n起始資金：{initialFund:N0} 元　目前資金：{totalFund:N0} 元";
        }

        #endregion


        #region 事件處理程序

        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            int index = int.Parse(pic.Name.Replace("pic", ""));
            int cardNum = playerPoker[index] + 1;

            if (pic.Tag.ToString() == "back")
            {
                pic.Tag = "front";
                pic.Image = GetImage(cardNum);
            }
            else
            {
                pic.Tag = "back";
                pic.Image = GetImage("back");
            }
        }

        private async void btnDealCard_Click(object sender, EventArgs e)
        {
            this.lblResult.Text = "";

            for (int i = 0; i < pic.Length; i++)
                pic[i].Image = GetImage("back");

            for (int i = 0; i < allPoker.Length; i++)
                allPoker[i] = i;

            this.Shuffle();

            await Task.Delay(500);

            for (int i = 0; i < playerPoker.Length; i++)
                playerPoker[i] = allPoker[i];

            this.ShowCards();

            for (int i = 0; i < pic.Length; i++)
            {
                pic[i].Enabled = true;
                pic[i].Tag = "front";
            }

            btnChangeCard.Enabled = true;
            btnDealCard.Enabled = false;
        }

        private void btnChangeCard_Click(object sender, EventArgs e)
        {
            int startIndex = 5;

            for (int i = 0; i < playerPoker.Length; i++)
            {
                if (pic[i].Tag.ToString() == "back")
                {
                    playerPoker[i] = allPoker[startIndex];
                    pic[i].Image = GetImage(playerPoker[i] + 1);
                    pic[i].Tag = "front";
                    startIndex++;
                }
            }

            for (int i = 0; i < pic.Length; i++)
                pic[i].Enabled = false;

            this.btnChangeCard.Enabled = false;
            this.btnCheck.Enabled = true;
            this.btnCheck.Focus();
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBetAmount.Text.Trim(), out int inputBet))
            {
                MessageBox.Show("請輸入有效的押注金額（整數）！", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBetAmount.Focus();
                return;
            }

            if (inputBet <= 0)
            {
                MessageBox.Show("押注金額必須大於 0！", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBetAmount.Focus();
                return;
            }

            if (inputBet > totalFund)
            {
                MessageBox.Show($"押注金額不可超過總資金 {totalFund:N0} 元！", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBetAmount.Focus();
                return;
            }

            betAmount = inputBet;
            MessageBox.Show($"押注成功！本局押注金額為 {betAmount:N0} 元，祝你好運！", "押注成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtBetAmount.Enabled = false;
            btnBet.Enabled = false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string[] colorList = { "梅花", "方塊", "愛心", "黑桃" };
            string[] pointList = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            int[] pokerColor = new int[5];
            int[] pokerPoint = new int[5];

            for (int i = 0; i < playerPoker.Length; i++)
            {
                pokerColor[i] = playerPoker[i] % 4;
                pokerPoint[i] = playerPoker[i] / 4;
            }

            int[] colorCount = new int[4];
            int[] pointCount = new int[13];

            for (int i = 0; i < pokerColor.Length; i++)
            {
                colorCount[pokerColor[i]]++;
                pointCount[pokerPoint[i]]++;
            }

            Array.Sort(colorCount, colorList);
            Array.Reverse(colorCount);
            Array.Reverse(colorList);

            Array.Sort(pointCount, pointList);
            Array.Reverse(pointCount);
            Array.Reverse(pointList);

            bool isFlush = (colorCount[0] == 5);
            bool isSingle = (pointCount[0] == 1 && pointCount[1] == 1 && pointCount[2] == 1 && pointCount[3] == 1 && pointCount[4] == 1);
            bool isDiffFour = (pokerPoint.Max() - pokerPoint.Min() == 4);
            bool isRoyal = pokerPoint.Contains(0) && pokerPoint.Contains(9) && pokerPoint.Contains(10) && pokerPoint.Contains(11) && pokerPoint.Contains(12);
            bool isRoyalFlush = isFlush && isRoyal;
            bool isStraightFlush = isFlush && isSingle && isDiffFour;
            bool isStraight = isSingle && (isDiffFour || isRoyal);
            bool isFourOfAKind = (pointCount[0] == 4);
            bool isFullHouse = (pointCount[0] == 3 && pointCount[1] == 2);
            bool isThreeOfAKind = (pointCount[0] == 3 && pointCount[1] == 1);
            bool isTwoPair = (pointCount[0] == 2 && pointCount[1] == 2);
            bool isOnePair = (pointCount[0] == 2 && pointCount[1] == 1);

            string result = "";

            if (isRoyalFlush) result = $"{colorList[0]} 同花大順";
            else if (isStraightFlush) result = $"{colorList[0]} 同花順";
            else if (isStraight) result = "順子";
            else if (isFourOfAKind) result = $"{pointList[0]} 鐵支";
            else if (isFullHouse) result = $"{pointList[0]}三張{pointList[1]}兩張 葫蘆";
            else if (isFlush) result = $"{colorList[0]} 同花";
            else if (isThreeOfAKind) result = $"{pointList[0]} 三條";
            else if (isTwoPair) result = $"{pointList[0]},{pointList[1]} 兩對";
            else if (isOnePair) result = $"{pointList[0]} 一對";
            else result = "雜牌";

            lblResult.Text = result;
            btnChangeCard.Enabled = false;
            btnCheck.Enabled = false;
            btnDealCard.Enabled = true;

            // ── 計算賠率與結算 ──────────────────────────────────────
            if (betAmount <= 0)
            {
                // 未下注，仍記錄本局（盈虧為 0）
                roundCount++;
                history.Add((roundCount, result, 0, 0, totalFund));
                betAmount = 0;
                return;
            }

            int odds = GetOdds(result);
            int changeAmount;

            if (odds > 0)
            {
                changeAmount = betAmount * odds;
                totalFund += changeAmount;
                lblTotalFund.Text = totalFund.ToString("N0");

                MessageBox.Show(
                    $"恭喜你！\n本次押注金額為 {betAmount:N0} 元\n根據牌型「{result}」（賠率 x{odds}）\n賺到 {changeAmount:N0} 元\n目前總資金為 {totalFund:N0} 元",
                    "恭喜獲勝 🎉", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                changeAmount = -betAmount;
                totalFund += changeAmount;
                lblTotalFund.Text = totalFund.ToString("N0");

                MessageBox.Show(
                    $"好可惜，差一點點就下注成功了！\n本次押注金額為 {betAmount:N0} 元\n根據牌型「{result}」\n損失 {betAmount:N0} 元\n目前總資金為 {totalFund:N0} 元",
                    "很可惜 😢", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (totalFund <= 0)
                {
                    totalFund = 0;
                    lblTotalFund.Text = "0";
                    MessageBox.Show("你已經破產了！遊戲結束。", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDealCard.Enabled = false;
                    btnBet.Enabled = false;
                }
            }

            // 寫入歷史紀錄
            roundCount++;
            history.Add((roundCount, result, betAmount, changeAmount, totalFund));

            // 重置押注區
            betAmount = 0;
            txtBetAmount.Text = "";
            txtBetAmount.Enabled = true;
            btnBet.Enabled = true;
        }

        private void frmPoker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.btnDealCard.Enabled == false)
            {
                switch (e.KeyChar)
                {
                    case 'q':
                        playerPoker[0] = 51; playerPoker[1] = 47;
                        playerPoker[2] = 43; playerPoker[3] = 39; playerPoker[4] = 3;
                        break;
                    case 'w':
                        playerPoker[0] = 37; playerPoker[1] = 33;
                        playerPoker[2] = 29; playerPoker[3] = 25; playerPoker[4] = 21;
                        break;
                    case 'e':
                        playerPoker[0] = 50; playerPoker[1] = 38;
                        playerPoker[2] = 34; playerPoker[3] = 22; playerPoker[4] = 18;
                        break;
                    case 'r':
                        playerPoker[0] = 48; playerPoker[1] = 39;
                        playerPoker[2] = 38; playerPoker[3] = 37; playerPoker[4] = 36;
                        break;
                    case 't':
                        playerPoker[0] = 30; playerPoker[1] = 29;
                        playerPoker[2] = 6; playerPoker[3] = 5; playerPoker[4] = 4;
                        break;
                    case 'y':
                        playerPoker[0] = 48; playerPoker[1] = 39;
                        playerPoker[2] = 15; playerPoker[3] = 14; playerPoker[4] = 13;
                        break;
                }
                this.ShowCards();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            int diff = totalFund - initialFund;
            string summary = BuildSummary(diff);
            string confirm = summary + "\n\n確定要重新開始嗎？";

            DialogResult dr = MessageBox.Show(confirm, "重新開始", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            // 重置資金與歷史紀錄
            totalFund = initialFund;
            betAmount = 0;
            roundCount = 0;
            history.Clear();
            lblTotalFund.Text = totalFund.ToString("N0");

            // 重置押注區
            txtBetAmount.Text = "";
            txtBetAmount.Enabled = true;
            btnBet.Enabled = true;

            // 重置牌桌
            lblResult.Text = "";
            foreach (var p in pic)
            {
                p.Image = GetImage("back");
                p.Tag = "back";
                p.Enabled = false;
            }

            // 重置按鈕狀態
            btnDealCard.Enabled = true;
            btnChangeCard.Enabled = false;
            btnCheck.Enabled = false;
        }

        private void btnOver_Click(object sender, EventArgs e)
        {
            int diff = totalFund - initialFund;
            string summary = BuildSummary(diff);
            string confirm = summary + "\n\n確定要結束遊戲嗎？";

            DialogResult dr = MessageBox.Show(confirm, "結束遊戲", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void btnStatics_Click(object sender, EventArgs e)
        {
            if (history.Count == 0)
            {
                MessageBox.Show("目前還沒有任何遊玩紀錄！", "統計圖表", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form frmStats = new Form();
            frmStats.Text = "遊玩統計";
            frmStats.Size = new Size(900, 620);
            frmStats.MinimumSize = new Size(700, 450);
            frmStats.StartPosition = FormStartPosition.CenterParent;
            frmStats.Font = new Font("微軟正黑體", 11F);

            // ── 底部摘要 Label ──────────────────────────────────────
            int totalChange = totalFund - initialFund;
            int totalWin = history.Where(r => r.change > 0).Sum(r => r.change);
            int totalLoss = history.Where(r => r.change < 0).Sum(r => Math.Abs(r.change));
            int betRounds = history.Count(r => r.bet > 0);
            string trendIcon = totalChange > 0 ? "🎉" : totalChange < 0 ? "😢" : "😐";
            string changeSign = totalChange >= 0 ? $"+{totalChange:N0}" : $"{totalChange:N0}";

            Label lblSummary = new Label();
            lblSummary.Text = $"共 {history.Count} 局（下注 {betRounds} 局）　累計盈虧：{changeSign} 元 {trendIcon}　總贏：+{totalWin:N0}　總輸：-{totalLoss:N0}　目前總資金：{totalFund:N0} 元";
            lblSummary.Dock = DockStyle.Bottom;
            lblSummary.Height = 48;
            lblSummary.TextAlign = ContentAlignment.MiddleCenter;
            lblSummary.BackColor = Color.LightSteelBlue;
            lblSummary.Font = new Font("微軟正黑體", 10F, FontStyle.Bold);

            // ── TabControl ──────────────────────────────────────────
            TabControl tab = new TabControl();
            tab.Dock = DockStyle.Fill;
            tab.Font = new Font("微軟正黑體", 11F);

            // ── Tab 1：歷史紀錄表格 ─────────────────────────────────
            TabPage tabTable = new TabPage("📋 歷史紀錄");

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 11F, FontStyle.Bold);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            dgv.Columns.Add("round", "局數");
            dgv.Columns.Add("hand", "牌型");
            dgv.Columns.Add("bet", "押注金額");
            dgv.Columns.Add("change", "盈虧");
            dgv.Columns.Add("fund", "結算後總資金");

            dgv.Columns["round"].FillWeight = 50;
            dgv.Columns["bet"].FillWeight = 80;
            dgv.Columns["change"].FillWeight = 80;

            foreach (var r in history)
            {
                string betStr = r.bet == 0 ? "未下注" : $"{r.bet:N0}";
                string changeStr = r.change > 0 ? $"+{r.change:N0}"
                                 : r.change < 0 ? $"{r.change:N0}"
                                 : "-";

                int idx = dgv.Rows.Add($"第 {r.round} 局", r.hand, betStr, changeStr, $"{r.fund:N0}");

                if (r.change > 0) dgv.Rows[idx].Cells["change"].Style.ForeColor = Color.Green;
                else if (r.change < 0) dgv.Rows[idx].Cells["change"].Style.ForeColor = Color.Red;
            }

            tabTable.Controls.Add(dgv);

            // ── Tab 2：資金走勢折線圖 ───────────────────────────────
            TabPage tabChart = new TabPage("📈 資金走勢");

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.WhiteSmoke;

            ChartArea area = new ChartArea("main");
            area.BackColor = Color.White;
            area.AxisX.Title = "局數";
            area.AxisY.Title = "總資金（元）";
            area.AxisX.TitleFont = new Font("微軟正黑體", 10F);
            area.AxisY.TitleFont = new Font("微軟正黑體", 10F);
            area.AxisX.LabelStyle.Font = new Font("微軟正黑體", 9F);
            area.AxisY.LabelStyle.Font = new Font("微軟正黑體", 9F);
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Interval = 1;
            chart.ChartAreas.Add(area);

            // 起始資金作為第 0 點
            Series series = new Series("總資金");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = Color.SteelBlue;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;
            series.MarkerColor = Color.SteelBlue;
            series.IsValueShownAsLabel = true;
            series.Font = new Font("微軟正黑體", 8F);
            series.LabelForeColor = Color.DimGray;

            // 基準線（起始資金）
            Series baseline = new Series("起始資金");
            baseline.ChartType = SeriesChartType.Line;
            baseline.BorderWidth = 2;
            baseline.Color = Color.LightCoral;
            baseline.BorderDashStyle = ChartDashStyle.Dash;
            baseline.IsVisibleInLegend = true;

            // 加入起點（第 0 局）
            series.Points.AddXY(0, initialFund);
            baseline.Points.AddXY(0, initialFund);

            foreach (var r in history)
            {
                series.Points.AddXY(r.round, r.fund);
                baseline.Points.AddXY(r.round, initialFund);
            }

            Legend legend = new Legend();
            legend.Font = new Font("微軟正黑體", 10F);
            chart.Legends.Add(legend);

            chart.Series.Add(series);
            chart.Series.Add(baseline);

            tabChart.Controls.Add(chart);

            tab.TabPages.Add(tabTable);
            tab.TabPages.Add(tabChart);

            frmStats.Controls.Add(tab);
            frmStats.Controls.Add(lblSummary);
            frmStats.ShowDialog(this);
        }

        #endregion
    }
}