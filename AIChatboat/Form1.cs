using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AIChatboat
{
    public partial class Form1 : Form
    {
        // =========================================================
        //  PASTE YOUR GEMINI API KEY BETWEEN THE QUOTES BELOW
        // =========================================================
        private const string API_KEY = " Your Gemini API Key";

        private const string API_URL =
            "https://generativelanguage.googleapis.com/v1beta/models/" +
            "gemini-flash-latest:generateContent?key=";

        private static readonly HttpClient httpClient = new HttpClient();

        // ── Constructor ───────────────────────────────────────────
        public Form1()
        {
            InitializeComponent();
            ShowWelcomeMessage();
        }

        // ── Welcome message on startup ────────────────────────────
        private void ShowWelcomeMessage()
        {
            AppendMessage("System",
                "👋 Welcome to AI Chatbot!\n" +
                "I am powered by Google Gemini AI.\n" +
                "Ask me anything — I am here to help! 😊",
                Color.FromArgb(22, 163, 74));

            AppendSeparator();
        }

        // ── Send button clicked ───────────────────────────────────
        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        // ── Enter key pressed in input box ────────────────────────
        private async void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SendMessage();
            }
        }

        // ── Clear chat button clicked ─────────────────────────────
        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();
            ShowWelcomeMessage();
            UpdateStatus("✅  Chat cleared — Ready for a new conversation!",
                Color.FromArgb(22, 163, 74));
        }

        // ── Core send logic ───────────────────────────────────────
        private async Task SendMessage()
        {
            string userMessage = txtInput.Text.Trim();

            if (string.IsNullOrEmpty(userMessage))
            {
                MessageBox.Show(
                    "Please type a message before sending!",
                    "Empty Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Display user message
            AppendMessage("You", userMessage, Color.FromArgb(37, 99, 235));
            txtInput.Clear();

            // Disable input while waiting
            SetInputEnabled(false);
            UpdateStatus("⏳  AI is thinking... please wait.", Color.Orange);

            try
            {
                string aiReply = await CallGeminiAPI(userMessage);
                AppendMessage("AI Bot", aiReply, Color.FromArgb(126, 34, 206));
                AppendSeparator();
                UpdateStatus("✅  Ready — Ask me anything!",
                    Color.FromArgb(22, 163, 74));
            }
            catch (Exception ex)
            {
                AppendMessage("⚠ Error", ex.Message, Color.Red);
                UpdateStatus("❌  Error occurred. Please try again.", Color.Red);
            }
            finally
            {
                SetInputEnabled(true);
                txtInput.Focus();
            }
        }

        // ── Call Gemini API ───────────────────────────────────────
        private async Task<string> CallGeminiAPI(string userMessage)
        {
            // Check if API key is set
            if (API_KEY == "YOUR_GEMINI_API_KEY_HERE" ||
                string.IsNullOrWhiteSpace(API_KEY))
            {
                throw new Exception(
                    "API Key is missing!\n\n" +
                    "Please open Form1.cs and replace\n" +
                    "YOUR_GEMINI_API_KEY_HERE\n" +
                    "with your actual Gemini API key from:\n" +
                    "https://aistudio.google.com/app/apikey");
            }

            // Build request
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = userMessage }
                        }
                    }
                }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var httpContent = new StringContent(
                jsonBody, Encoding.UTF8, "application/json");

            // Send request
            HttpResponseMessage response = await httpClient
                .PostAsync(API_URL + API_KEY, httpContent);

            string responseString = await response.Content.ReadAsStringAsync();

            // Handle errors
            if (!response.IsSuccessStatusCode)
            {
                JObject errorObj = JObject.Parse(responseString);
                string errorMsg = errorObj["error"]?["message"]?.ToString()
                                  ?? "Unknown error from API.";
                throw new Exception("API Error: " + errorMsg);
            }

            // Parse response
            JObject json = JObject.Parse(responseString);
            string aiText = json["candidates"]?[0]?["content"]?
                            ["parts"]?[0]?["text"]?.ToString();

            return string.IsNullOrWhiteSpace(aiText)
                ? "Sorry, I could not generate a response. Please try again."
                : aiText.Trim();
        }

        // ── Append a message to the chat box ─────────────────────
        private void AppendMessage(string sender, string message, Color nameColor)
        {
            // Sender label
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionColor = nameColor;
            rtbChat.SelectionFont = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            rtbChat.AppendText($"  {sender}\n");

            // Render formatted message
            RenderFormattedText(message);

            rtbChat.AppendText("\n");
            rtbChat.ScrollToCaret();
        }

        // ── Convert Markdown into RichTextBox formatting ──────────
        private void RenderFormattedText(string message)
        {
            string[] lines = message.Split('\n');

            foreach (string rawLine in lines)
            {
                string line = rawLine;

                // Empty line — just spacing
                if (string.IsNullOrWhiteSpace(line))
                {
                    rtbChat.AppendText("\n");
                    continue;
                }

                // ── ### Heading ───────────────────────────────────
                if (line.TrimStart().StartsWith("### "))
                {
                    string text = StripInlineMarkdown(
                        line.TrimStart().Substring(4).Trim());
                    rtbChat.SelectionStart = rtbChat.TextLength;
                    rtbChat.SelectionLength = 0;
                    rtbChat.SelectionColor = Color.FromArgb(37, 99, 235);
                    rtbChat.SelectionFont = new Font("Segoe UI", 12f, FontStyle.Bold);
                    rtbChat.AppendText($"  {text}\n");
                    continue;
                }

                // ── ## Heading ────────────────────────────────────
                if (line.TrimStart().StartsWith("## "))
                {
                    string text = StripInlineMarkdown(
                        line.TrimStart().Substring(3).Trim());
                    rtbChat.SelectionStart = rtbChat.TextLength;
                    rtbChat.SelectionLength = 0;
                    rtbChat.SelectionColor = Color.FromArgb(37, 99, 235);
                    rtbChat.SelectionFont = new Font("Segoe UI", 13f, FontStyle.Bold);
                    rtbChat.AppendText($"  {text}\n");
                    continue;
                }

                // ── # Heading ─────────────────────────────────────
                if (line.TrimStart().StartsWith("# "))
                {
                    string text = StripInlineMarkdown(
                        line.TrimStart().Substring(2).Trim());
                    rtbChat.SelectionStart = rtbChat.TextLength;
                    rtbChat.SelectionLength = 0;
                    rtbChat.SelectionColor = Color.FromArgb(37, 99, 235);
                    rtbChat.SelectionFont = new Font("Segoe UI", 14f, FontStyle.Bold);
                    rtbChat.AppendText($"  {text}\n");
                    continue;
                }

                // ── Bullet: * item or - item ──────────────────────
                if (line.TrimStart().StartsWith("* ") ||
                    line.TrimStart().StartsWith("- "))
                {
                    string bulletText = line.TrimStart().Substring(2).Trim();
                    AppendInlineFormatted($"    • {bulletText}\n");
                    continue;
                }

                // ── Sub bullet: + item ────────────────────────────
                if (line.TrimStart().StartsWith("+ "))
                {
                    string bulletText = line.TrimStart().Substring(2).Trim();
                    AppendInlineFormatted($"        ◦ {bulletText}\n");
                    continue;
                }

                // ── Numbered list: 1. item ────────────────────────
                if (Regex.IsMatch(line.TrimStart(), @"^\d+\.\s"))
                {
                    AppendInlineFormatted($"    {line.TrimStart()}\n");
                    continue;
                }

                // ── Horizontal rule ───────────────────────────────
                if (line.Trim() == "---" || line.Trim() == "***")
                {
                    rtbChat.SelectionColor = Color.FromArgb(180, 180, 180);
                    rtbChat.SelectionFont = new Font("Segoe UI", 7f);
                    rtbChat.AppendText(
                        "  ─────────────────────────────────────\n");
                    continue;
                }

                // ── Normal paragraph line ─────────────────────────
                AppendInlineFormatted($"  {line}\n");
            }
        }

        // ── Handle **bold**, *italic*, `code` in a single line ───
        private void AppendInlineFormatted(string line)
        {
            int i = 0;
            while (i < line.Length)
            {
                // Bold: **text**
                if (i + 1 < line.Length &&
                    line[i] == '*' && line[i + 1] == '*')
                {
                    int end = line.IndexOf("**", i + 2);
                    if (end != -1)
                    {
                        string boldText = line.Substring(i + 2, end - i - 2);
                        rtbChat.SelectionStart = rtbChat.TextLength;
                        rtbChat.SelectionLength = 0;
                        rtbChat.SelectionColor = Color.FromArgb(30, 30, 30);
                        rtbChat.SelectionFont =
                            new Font("Segoe UI", 10.5f, FontStyle.Bold);
                        rtbChat.AppendText(boldText);
                        i = end + 2;
                        continue;
                    }
                }

                // Italic: *text*
                if (line[i] == '*')
                {
                    int end = line.IndexOf('*', i + 1);
                    if (end != -1)
                    {
                        string italicText = line.Substring(i + 1, end - i - 1);
                        rtbChat.SelectionStart = rtbChat.TextLength;
                        rtbChat.SelectionLength = 0;
                        rtbChat.SelectionColor = Color.FromArgb(30, 30, 30);
                        rtbChat.SelectionFont =
                            new Font("Segoe UI", 10.5f, FontStyle.Italic);
                        rtbChat.AppendText(italicText);
                        i = end + 1;
                        continue;
                    }
                }

                // Code: `text`
                if (line[i] == '`')
                {
                    int end = line.IndexOf('`', i + 1);
                    if (end != -1)
                    {
                        string codeText = line.Substring(i + 1, end - i - 1);
                        rtbChat.SelectionStart = rtbChat.TextLength;
                        rtbChat.SelectionLength = 0;
                        rtbChat.SelectionColor = Color.FromArgb(180, 50, 50);
                        rtbChat.SelectionFont =
                            new Font("Courier New", 10f, FontStyle.Regular);
                        rtbChat.AppendText(codeText);
                        i = end + 1;
                        continue;
                    }
                }

                // Normal character
                rtbChat.SelectionStart = rtbChat.TextLength;
                rtbChat.SelectionLength = 0;
                rtbChat.SelectionColor = Color.FromArgb(30, 30, 30);
                rtbChat.SelectionFont =
                    new Font("Segoe UI", 10.5f, FontStyle.Regular);
                rtbChat.AppendText(line[i].ToString());
                i++;
            }
        }

        // ── Strip markdown symbols from heading text ──────────────
        private string StripInlineMarkdown(string text)
        {
            text = Regex.Replace(text, @"\*\*(.*?)\*\*", "$1");
            text = Regex.Replace(text, @"\*(.*?)\*", "$1");
            text = Regex.Replace(text, @"`(.*?)`", "$1");
            return text;
        }

        // ── Add a thin separator line ─────────────────────────────
        private void AppendSeparator()
        {
            rtbChat.SelectionColor = Color.FromArgb(200, 200, 200);
            rtbChat.SelectionFont = new Font("Segoe UI", 7f);
            rtbChat.AppendText(
                "  ─────────────────────────────────────────\n\n");
        }

        // ── Update status label ───────────────────────────────────
        private void UpdateStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        // ── Enable / disable input controls ──────────────────────
        private void SetInputEnabled(bool enabled)
        {
            txtInput.Enabled = enabled;
            btnSend.Enabled = enabled;
            btnClear.Enabled = enabled;
        }
    }
}