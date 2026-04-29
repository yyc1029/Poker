namespace Poker
{
    partial class frmPoker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpPoker = new System.Windows.Forms.GroupBox();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnChangeCard = new System.Windows.Forms.Button();
            this.btnDealCard = new System.Windows.Forms.Button();
            this.groupBet = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalFund = new System.Windows.Forms.Label();
            this.lblBetAmount = new System.Windows.Forms.Label();
            this.txtBetAmount = new System.Windows.Forms.TextBox();
            this.btmBet = new System.Windows.Forms.Button();
            this.grpButton.SuspendLayout();
            this.groupBet.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPoker
            // 
            this.grpPoker.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPoker.Location = new System.Drawing.Point(26, 24);
            this.grpPoker.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.grpPoker.Name = "grpPoker";
            this.grpPoker.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.grpPoker.Size = new System.Drawing.Size(1051, 320);
            this.grpPoker.TabIndex = 0;
            this.grpPoker.TabStop = false;
            this.grpPoker.Text = "牌桌";
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.lblResult);
            this.grpButton.Controls.Add(this.btnCheck);
            this.grpButton.Controls.Add(this.btnChangeCard);
            this.grpButton.Controls.Add(this.btnDealCard);
            this.grpButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpButton.Location = new System.Drawing.Point(26, 530);
            this.grpButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.grpButton.Name = "grpButton";
            this.grpButton.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.grpButton.Size = new System.Drawing.Size(1051, 160);
            this.grpButton.TabIndex = 1;
            this.grpButton.TabStop = false;
            this.grpButton.Text = "功能";
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResult.Location = new System.Drawing.Point(546, 56);
            this.lblResult.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(483, 72);
            this.lblResult.TabIndex = 3;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCheck
            // 
            this.btnCheck.Enabled = false;
            this.btnCheck.Location = new System.Drawing.Point(355, 56);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(178, 72);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "判斷牌型";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnChangeCard
            // 
            this.btnChangeCard.Enabled = false;
            this.btnChangeCard.Location = new System.Drawing.Point(204, 56);
            this.btnChangeCard.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnChangeCard.Name = "btnChangeCard";
            this.btnChangeCard.Size = new System.Drawing.Size(139, 72);
            this.btnChangeCard.TabIndex = 1;
            this.btnChangeCard.Text = "換牌";
            this.btnChangeCard.UseVisualStyleBackColor = true;
            this.btnChangeCard.Click += new System.EventHandler(this.btnChangeCard_Click);
            // 
            // btnDealCard
            // 
            this.btnDealCard.Location = new System.Drawing.Point(46, 56);
            this.btnDealCard.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDealCard.Name = "btnDealCard";
            this.btnDealCard.Size = new System.Drawing.Size(145, 72);
            this.btnDealCard.TabIndex = 0;
            this.btnDealCard.Text = "發牌";
            this.btnDealCard.UseVisualStyleBackColor = true;
            this.btnDealCard.Click += new System.EventHandler(this.btnDealCard_Click);
            // 
            // groupBet
            // 
            this.groupBet.Controls.Add(this.btmBet);
            this.groupBet.Controls.Add(this.txtBetAmount);
            this.groupBet.Controls.Add(this.lblBetAmount);
            this.groupBet.Controls.Add(this.lblTotalFund);
            this.groupBet.Controls.Add(this.lblTotal);
            this.groupBet.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBet.Location = new System.Drawing.Point(26, 354);
            this.groupBet.Name = "groupBet";
            this.groupBet.Size = new System.Drawing.Size(1051, 167);
            this.groupBet.TabIndex = 2;
            this.groupBet.TabStop = false;
            this.groupBet.Text = "下注";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(58, 75);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(113, 40);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "總資金";
            // 
            // lblTotalFund
            // 
            this.lblTotalFund.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFund.Location = new System.Drawing.Point(180, 67);
            this.lblTotalFund.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotalFund.Name = "lblTotalFund";
            this.lblTotalFund.Size = new System.Drawing.Size(245, 67);
            this.lblTotalFund.TabIndex = 4;
            this.lblTotalFund.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBetAmount
            // 
            this.lblBetAmount.AutoSize = true;
            this.lblBetAmount.Location = new System.Drawing.Point(434, 75);
            this.lblBetAmount.Name = "lblBetAmount";
            this.lblBetAmount.Size = new System.Drawing.Size(145, 40);
            this.lblBetAmount.TabIndex = 5;
            this.lblBetAmount.Text = "押注金額";
            // 
            // txtBetAmount
            // 
            this.txtBetAmount.Location = new System.Drawing.Point(585, 65);
            this.txtBetAmount.Multiline = true;
            this.txtBetAmount.Name = "txtBetAmount";
            this.txtBetAmount.Size = new System.Drawing.Size(261, 69);
            this.txtBetAmount.TabIndex = 6;
            // 
            // btmBet
            // 
            this.btmBet.Location = new System.Drawing.Point(878, 69);
            this.btmBet.Name = "btmBet";
            this.btmBet.Size = new System.Drawing.Size(138, 63);
            this.btmBet.TabIndex = 7;
            this.btmBet.Text = "押注";
            this.btmBet.UseVisualStyleBackColor = true;
            this.btmBet.Click += new System.EventHandler(this.btmBet_Click); // ← 補上事件綁定
            // 
            // frmPoker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 720);
            this.Controls.Add(this.groupBet);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.grpPoker);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmPoker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "五張撲克牌";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPoker_KeyPress);
            this.grpButton.ResumeLayout(false);
            this.groupBet.ResumeLayout(false);
            this.groupBet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPoker;
        private System.Windows.Forms.GroupBox grpButton;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnChangeCard;
        private System.Windows.Forms.Button btnDealCard;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.GroupBox groupBet;
        private System.Windows.Forms.TextBox txtBetAmount;
        private System.Windows.Forms.Label lblBetAmount;
        private System.Windows.Forms.Label lblTotalFund;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btmBet;
    }
}