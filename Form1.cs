using System;
using System.Drawing;
using System.Windows.Forms;
using MyHealthLib;

namespace HealthWinFormApp
{
    // Không cần Designer, tạo UI hoàn toàn bằng code.
    public class Form1 : Form
    {
        TextBox txtHeight, txtWeight, txtAdvice;
        Label lblBmi, lblCategory, lblIdeal;

        public Form1()
        {
            // KHÔNG gọi InitializeComponent(); vì ta không dùng Designer.
            this.Text = "HealthCheck WinForms - Dinhdat2k4";
            this.ClientSize = new Size(600, 320);
            this.StartPosition = FormStartPosition.CenterScreen;

            var label1 = new Label { Text = "Chiều cao (cm):", Location = new Point(24, 22), AutoSize = true };
            txtHeight = new TextBox { Location = new Point(120, 19), Size = new Size(100, 20) };

            var label2 = new Label { Text = "Cân nặng (kg):", Location = new Point(250, 22), AutoSize = true };
            txtWeight = new TextBox { Location = new Point(346, 19), Size = new Size(100, 20) };

            var btnCalc = new Button { Text = "Tính BMI", Location = new Point(470, 17), Size = new Size(90, 23) };
            btnCalc.Click += btnCalc_Click;

            lblBmi = new Label { Text = "BMI:", Location = new Point(24, 60), AutoSize = true };
            lblCategory = new Label { Text = "Phân loại:", Location = new Point(24, 84), AutoSize = true };
            lblIdeal = new Label { Text = "Cân nặng lý tưởng:", Location = new Point(24, 108), AutoSize = true };

            txtAdvice = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(27, 134),
                Size = new Size(533, 140)
            };

            var labelSig = new Label
            {
                Text = "WinForms by Dinhdat2k4",
                ForeColor = Color.RoyalBlue,
                Location = new Point(24, 287),
                AutoSize = true
            };

            // Thêm control vào form
            this.Controls.Add(label1);
            this.Controls.Add(txtHeight);
            this.Controls.Add(label2);
            this.Controls.Add(txtWeight);
            this.Controls.Add(btnCalc);
            this.Controls.Add(lblBmi);
            this.Controls.Add(lblCategory);
            this.Controls.Add(lblIdeal);
            this.Controls.Add(txtAdvice);
            this.Controls.Add(labelSig);
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            double h, w;
            if (!double.TryParse(txtHeight.Text.Trim(), out h) ||
                !double.TryParse(txtWeight.Text.Trim(), out w))
            {
                MessageBox.Show("Nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HealthChecker hc = new HealthChecker();
            hc.Signature = "WinForms by Dinhdat2k4";
            hc.HeightCm = h;
            hc.WeightKg = w;

            int code = hc.Process();
            if (code < 0)
            {
                MessageBox.Show(hc.LastError ?? "Lỗi không xác định", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblBmi.Text = "BMI: " + hc.BMI;
            lblCategory.Text = "Phân loại: " + hc.Category;
            lblIdeal.Text = "Cân nặng lý tưởng: " + hc.IdealMinKg + " - " + hc.IdealMaxKg + " kg";
            txtAdvice.Text = hc.Advice + Environment.NewLine + Environment.NewLine + hc.Signature;
        }
    }
}