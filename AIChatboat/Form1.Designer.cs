namespace AIChatboat
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ── Form ────────────────────────────────────────────
            this.Text = "AI Chatbot — Powered by Google Gemini";
            this.Size = new System.Drawing.Size(700, 600);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ── panelTop (title bar) ─────────────────────────────
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelTop.Controls.Add(this.lblTitle);

            // ── lblTitle ─────────────────────────────────────────
            this.lblTitle.Text = "🤖  AI Chatbot  —  Powered by Google Gemini";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F,
                                        System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── rtbChat (chat display) ────────────────────────────
            this.rtbChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChat.ReadOnly = true;
            this.rtbChat.BackColor = System.Drawing.Color.White;
            this.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChat.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.rtbChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbChat.Padding = new System.Windows.Forms.Padding(10);

            // ── panelBottom (input area) ──────────────────────────
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 100;
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(224, 231, 243);
            this.panelBottom.Padding = new System.Windows.Forms.Padding(10);
            this.panelBottom.Controls.Add(this.txtInput);
            this.panelBottom.Controls.Add(this.btnSend);
            this.panelBottom.Controls.Add(this.btnClear);
            this.panelBottom.Controls.Add(this.lblStatus);

            // ── txtInput ─────────────────────────────────────────
            this.txtInput.Location = new System.Drawing.Point(10, 15);
            this.txtInput.Size = new System.Drawing.Size(460, 32);
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.BackColor = System.Drawing.Color.White;
            
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(
                                            this.txtInput_KeyDown);

            // ── btnSend ──────────────────────────────────────────
            this.btnSend.Location = new System.Drawing.Point(480, 13);
            this.btnSend.Size = new System.Drawing.Size(90, 36);
            this.btnSend.Text = "Send ➤";
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F,
                                        System.Drawing.FontStyle.Bold);
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // ── btnClear ─────────────────────────────────────────
            this.btnClear.Location = new System.Drawing.Point(580, 13);
            this.btnClear.Size = new System.Drawing.Size(95, 36);
            this.btnClear.Text = "🗑 Clear";
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F,
                                        System.Drawing.FontStyle.Bold);
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // ── lblStatus ────────────────────────────────────────
            this.lblStatus.Location = new System.Drawing.Point(12, 60);
            this.lblStatus.Size = new System.Drawing.Size(660, 22);
            this.lblStatus.Text = "✅  Ready — Type a message and click Send!";
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F,
                                        System.Drawing.FontStyle.Italic);

            // ── Add controls to Form ─────────────────────────────
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);

            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        // Control declarations
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
    }
}