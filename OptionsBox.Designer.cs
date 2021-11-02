
namespace Swapper
{
    partial class OptionsBox
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
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftLabel = new System.Windows.Forms.Label();
            this.rightLabel = new System.Windows.Forms.Label();
            this.rightGestureText = new System.Windows.Forms.Label();
            this.setLeftButton = new System.Windows.Forms.Button();
            this.setRightButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.leftGestureText = new System.Windows.Forms.Label();
            this.rootLayout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.AutoSize = true;
            this.rootLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rootLayout.ColumnCount = 3;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.rootLayout.Controls.Add(this.leftLabel, 0, 0);
            this.rootLayout.Controls.Add(this.rightLabel, 0, 1);
            this.rootLayout.Controls.Add(this.leftGestureText, 1, 0);
            this.rootLayout.Controls.Add(this.rightGestureText, 1, 1);
            this.rootLayout.Controls.Add(this.setLeftButton, 2, 0);
            this.rootLayout.Controls.Add(this.setRightButton, 2, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(3, 27);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 2;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rootLayout.Size = new System.Drawing.Size(577, 130);
            this.rootLayout.TabIndex = 0;
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.Location = new System.Drawing.Point(3, 0);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(167, 41);
            this.leftLabel.TabIndex = 0;
            this.leftLabel.Text = "Left Primary";
            this.leftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rightLabel
            // 
            this.rightLabel.AutoSize = true;
            this.rightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightLabel.Location = new System.Drawing.Point(3, 41);
            this.rightLabel.Name = "rightLabel";
            this.rightLabel.Size = new System.Drawing.Size(167, 89);
            this.rightLabel.TabIndex = 1;
            this.rightLabel.Text = "Right Primary";
            this.rightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rightGestureText
            // 
            this.rightGestureText.AutoSize = true;
            this.rightGestureText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightGestureText.Location = new System.Drawing.Point(176, 41);
            this.rightGestureText.Name = "rightGestureText";
            this.rightGestureText.Size = new System.Drawing.Size(282, 89);
            this.rightGestureText.TabIndex = 3;
            this.rightGestureText.Text = "…";
            this.rightGestureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // setLeftButton
            // 
            this.setLeftButton.AutoSize = true;
            this.setLeftButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.setLeftButton.Location = new System.Drawing.Point(464, 3);
            this.setLeftButton.Name = "setLeftButton";
            this.setLeftButton.Size = new System.Drawing.Size(110, 35);
            this.setLeftButton.TabIndex = 4;
            this.setLeftButton.Text = "Set";
            this.setLeftButton.UseVisualStyleBackColor = true;
            this.setLeftButton.Click += new System.EventHandler(this.setLeftButton_Click);
            // 
            // setRightButton
            // 
            this.setRightButton.AutoSize = true;
            this.setRightButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.setRightButton.Location = new System.Drawing.Point(464, 44);
            this.setRightButton.Name = "setRightButton";
            this.setRightButton.Size = new System.Drawing.Size(110, 35);
            this.setRightButton.TabIndex = 5;
            this.setRightButton.Text = "Set";
            this.setRightButton.UseVisualStyleBackColor = true;
            this.setRightButton.Click += new System.EventHandler(this.setRightButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.rootLayout);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 160);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hot Keys";
            // 
            // leftGestureText
            // 
            this.leftGestureText.AutoSize = true;
            this.leftGestureText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftGestureText.Location = new System.Drawing.Point(176, 0);
            this.leftGestureText.Name = "leftGestureText";
            this.leftGestureText.Size = new System.Drawing.Size(282, 41);
            this.leftGestureText.TabIndex = 2;
            this.leftGestureText.Text = "…";
            this.leftGestureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OptionsBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(603, 180);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsBox";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.Label rightLabel;
        private System.Windows.Forms.Label rightGestureText;
        private System.Windows.Forms.Button setLeftButton;
        private System.Windows.Forms.Button setRightButton;
        private System.Windows.Forms.Label leftGestureText;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}