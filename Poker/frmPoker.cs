using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// 玩家本局的押注金額（0 表示尚未下注）
        /// </summary>
        int betAmount = 0;

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
                // 預設牌桌上的牌不可點擊
                pic[i].Enabled = false;
                // 預設牌桌上的牌的 Tag 為 "back"，表示牌面朝下
                pic[i].Tag = "back";
                pic[i].Visible = true;

                // 將 pic 丟至到 grpPorker 內
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
        /// <param name="name">string 的牌名 </param>
        /// <returns></returns>
        private Image GetImage(string name)
        {
            return Properties.Resources.ResourceManager.GetObject(name) as Image;
        }

        /// <summary>
        /// 取得圖片資源
        /// </summary>
        /// <param name="num">撲克牌編號</param>
        /// <returns></returns>
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
        /// <param name="handName">牌型名稱（包含在 result 字串中）</param>
        /// <returns>賠率倍數，0 表示沒有中獎</returns>
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
            return 0; // 雜牌
        }

        #endregion


        #region 事件處理程序

        /// <summary>
        /// 牌桌上的牌被按下時，顯示訊息框告訴使用者按下了哪一張牌
        /// </summary>
        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;

            int index = int.Parse(pic.Name.Replace("pic", ""));
            int cardNum = playerPoker[index] + 1;

            // 如果牌面朝下，則翻開牌面；如果牌面朝上，則翻回背面
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

        /// <summary>
        /// 當按下發牌按鈕時，隨機產生五個1~52的數字，並將對應的圖片顯示在牌桌上
        /// </summary>
        private async void btnDealCard_Click(object sender, EventArgs e)
        {
            // 將上一把玩的結果清除
            this.lblResult.Text = "";

            // 將牌桌上的牌重置為背面圖
            for (int i = 0; i < pic.Length; i++)
            {
                pic[i].Image = GetImage("back");
            }

            // 將所有牌的編號從 0 到 51 填入 allPoker 陣列
            for (int i = 0; i < allPoker.Length; i++)
            {
                allPoker[i] = i;
            }

            // 洗牌
            this.Shuffle();

            // 暫停500ms
            await Task.Delay(500);

            // 發前五張牌給玩家，並將對應的牌面圖顯示在牌桌上
            for (int i = 0; i < playerPoker.Length; i++)
            {
                playerPoker[i] = allPoker[i];
            }

            // 將對應的牌面圖顯示在牌桌上
            this.ShowCards();

            // 啟用所有牌的點擊事件
            for (int i = 0; i < pic.Length; i++)
            {
                pic[i].Enabled = true;
                pic[i].Tag = "front";
            }

            // 啟用換牌按鈕
            btnChangeCard.Enabled = true;
            btnDealCard.Enabled = false;
        }

        /// <summary>
        /// 當按下換牌按鈕時，將玩家手牌中被選中的牌換成新的牌，並將對應的圖片顯示在牌桌上
        /// </summary>
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
            {
                pic[i].Enabled = false;
            }

            this.btnChangeCard.Enabled = false;
            this.btnCheck.Enabled = true;
        }

        /// <summary>
        /// 當按下押注按鈕時，驗證押注金額並確認下注
        /// </summary>
        private void btnBet_Click(object sender, EventArgs e)
        {
            // 驗證輸入是否為有效數字
            if (!int.TryParse(txtBetAmount.Text.Trim(), out int inputBet))
            {
                MessageBox.Show("請輸入有效的押注金額（整數）！", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBetAmount.Focus();
                return;
            }

            // 驗證範圍：0 < betAmount <= totalFund
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

            // 儲存押注金額
            betAmount = inputBet;
            MessageBox.Show($"押注成功！本局押注金額為 {betAmount:N0} 元，祝你好運！", "押注成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 鎖定押注區，等判斷牌型結束後才能再次下注
            txtBetAmount.Enabled = false;
            btnBet.Enabled = false;
        }

        /// <summary>
        /// 當按下判斷牌型按鈕時，根據玩家手牌的編號，判斷玩家的牌型，並顯示在 lblResult 上
        /// </summary>
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

            if (isRoyalFlush)
                result = $"{colorList[0]} 同花大順";
            else if (isStraightFlush)
                result = $"{colorList[0]} 同花順";
            else if (isStraight)
                result = "順子";
            else if (isFourOfAKind)
                result = $"{pointList[0]} 鐵支";
            else if (isFullHouse)
                result = $"{pointList[0]}三張{pointList[1]}兩張 葫蘆";
            else if (isFlush)
                result = $"{colorList[0]} 同花";
            else if (isThreeOfAKind)
                result = $"{pointList[0]} 三條";
            else if (isTwoPair)
                result = $"{pointList[0]},{pointList[1]} 兩對";
            else if (isOnePair)
                result = $"{pointList[0]} 一對";
            else
                result = "雜牌";

            lblResult.Text = result;
            btnChangeCard.Enabled = false;
            btnCheck.Enabled = false;
            btnDealCard.Enabled = true;

            // ── 計算賠率與結算 ──────────────────────────────────────
            // 若玩家本局沒有下注，跳過結算
            if (betAmount <= 0)
            {
                betAmount = 0;
                return;
            }

            int odds = GetOdds(result);

            if (odds > 0)
            {
                // 有賺錢：贏得 betAmount * odds 元
                int winAmount = betAmount * odds;
                totalFund += winAmount;
                lblTotalFund.Text = totalFund.ToString("N0");

                MessageBox.Show(
                    $"恭喜你！\n本次押注金額為 {betAmount:N0} 元\n根據牌型「{result}」（賠率 x{odds}）\n賺到 {winAmount:N0} 元\n目前總資金為 {totalFund:N0} 元",
                    "恭喜獲勝 🎉", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // 沒有中獎：扣除押注金額
                totalFund -= betAmount;
                lblTotalFund.Text = totalFund.ToString("N0");

                MessageBox.Show(
                    $"好可惜，差一點點就下注成功了！\n本次押注金額為 {betAmount:N0} 元\n根據牌型「{result}」\n損失 {betAmount:N0} 元\n目前總資金為 {totalFund:N0} 元",
                    "很可惜 😢", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // 若資金歸零，提示玩家
                if (totalFund <= 0)
                {
                    totalFund = 0;
                    lblTotalFund.Text = "0";
                    MessageBox.Show("你已經破產了！遊戲結束。", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDealCard.Enabled = false;
                    btnBet.Enabled = false;
                }
            }

            // 每局結束後重置押注金額，解鎖押注區讓玩家重新下注
            betAmount = 0;
            txtBetAmount.Text = "";
            txtBetAmount.Enabled = true;
            btnBet.Enabled = true;
        }

        /// <summary>
        /// 當表單被按下鍵盤時觸發（測試用快捷鍵）
        /// </summary>
        private void frmPoker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.btnDealCard.Enabled == false)
            {
                switch (e.KeyChar)
                {
                    case 'q':
                        // 同花大順
                        playerPoker[0] = 51;
                        playerPoker[1] = 47;
                        playerPoker[2] = 43;
                        playerPoker[3] = 39;
                        playerPoker[4] = 3;
                        break;
                    case 'w':
                        // 同花順
                        playerPoker[0] = 37;
                        playerPoker[1] = 33;
                        playerPoker[2] = 29;
                        playerPoker[3] = 25;
                        playerPoker[4] = 21;
                        break;
                    case 'e':
                        // 同花
                        playerPoker[0] = 50;
                        playerPoker[1] = 38;
                        playerPoker[2] = 34;
                        playerPoker[3] = 22;
                        playerPoker[4] = 18;
                        break;
                    case 'r':
                        // 鐵支
                        playerPoker[0] = 48;
                        playerPoker[1] = 39;
                        playerPoker[2] = 38;
                        playerPoker[3] = 37;
                        playerPoker[4] = 36;
                        break;
                    case 't':
                        // 葫蘆
                        playerPoker[0] = 30;
                        playerPoker[1] = 29;
                        playerPoker[2] = 6;
                        playerPoker[3] = 5;
                        playerPoker[4] = 4;
                        break;
                    case 'y':
                        // 三條
                        playerPoker[0] = 48;
                        playerPoker[1] = 39;
                        playerPoker[2] = 15;
                        playerPoker[3] = 14;
                        playerPoker[4] = 13;
                        break;
                }

                this.ShowCards();
            }
        }

        #endregion
    }
}