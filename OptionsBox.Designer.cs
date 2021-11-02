
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
            this.leftGestureText = new System.Windows.Forms.Label();
            this.rightGestureText = new System.Windows.Forms.Label();
            this.setLeftButton = new System.Windows.Forms.Button();
            this.setRightButton = new System.Windows.Forms.Button();
            this.rootLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.AutoSize = true;
            this.rootLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.leftLabel, 0, 0);
            this.rootLayout.Controls.Add(this.rightLabel, 0, 4);
            this.rootLayout.Controls.Add(this.leftGestureText, 0, 1);
            this.rootLayout.Controls.Add(this.rightGestureText, 0, 5);
            this.rootLayout.Controls.Add(this.setLeftButton, 0, 2);
            this.rootLayout.Controls.Add(this.setRightButton, 0, 6);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 7;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.rootLayout.Size = new System.Drawing.Size(494, 450);
            this.rootLayout.TabIndex = 0;
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.Location = new System.Drawing.Point(3, 0);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(261, 25);
            this.leftLabel.TabIndex = 0;
            this.leftLabel.Text = "Left as Primary Gesture";
            // 
            // rightLabel
            // 
            this.rightLabel.AutoSize = true;
            this.rightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightLabel.Location = new System.Drawing.Point(3, 256);
            this.rightLabel.Name = "rightLabel";
            this.rightLabel.Size = new System.Drawing.Size(276, 25);
            this.rightLabel.TabIndex = 1;
            this.rightLabel.Text = "Right as Primary Gesture";
            // 
            // leftGestureText
            // 
            this.leftGestureText.AutoSize = true;
            this.leftGestureText.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftGestureText.Location = new System.Drawing.Point(3, 64);
            this.leftGestureText.Name = "leftGestureText";
            this.leftGestureText.Size = new System.Drawing.Size(488, 25);
            this.leftGestureText.TabIndex = 2;
            this.leftGestureText.Text = "…";
            this.leftGestureText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rightGestureText
            // 
            this.rightGestureText.AutoSize = true;
            this.rightGestureText.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightGestureText.Location = new System.Drawing.Point(3, 320);
            this.rightGestureText.Name = "rightGestureText";
            this.rightGestureText.Size = new System.Drawing.Size(488, 25);
            this.rightGestureText.TabIndex = 3;
            this.rightGestureText.Text = "…";
            this.rightGestureText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // setLeftButton
            // 
            this.setLeftButton.AutoSize = true;
            this.setLeftButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.setLeftButton.Location = new System.Drawing.Point(3, 131);
            this.setLeftButton.Name = "setLeftButton";
            this.setLeftButton.Size = new System.Drawing.Size(488, 35);
            this.setLeftButton.TabIndex = 4;
            this.setLeftButton.Text = "Set";
            this.setLeftButton.UseVisualStyleBackColor = true;
            this.setLeftButton.Click += new System.EventHandler(this.setLeftButton_Click);
            // 
            // setRightButton
            // 
            this.setRightButton.AutoSize = true;
            this.setRightButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.setRightButton.Location = new System.Drawing.Point(3, 387);
            this.setRightButton.Name = "setRightButton";
            this.setRightButton.Size = new System.Drawing.Size(488, 35);
            this.setRightButton.TabIndex = 5;
            this.setRightButton.Text = "Set";
            this.setRightButton.UseVisualStyleBackColor = true;
            this.setRightButton.Click += new System.EventHandler(this.setRightButton_Click);
            // 
            // OptionsBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(494, 450);
            this.Controls.Add(this.rootLayout);
            this.Name = "OptionsBox";
            this.Text = "OptionsBox";
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.Label rightLabel;
        private System.Windows.Forms.Label leftGestureText;
        private System.Windows.Forms.Label rightGestureText;
        private System.Windows.Forms.Button setLeftButton;
        private System.Windows.Forms.Button setRightButton;
    }
}