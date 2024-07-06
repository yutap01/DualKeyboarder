namespace DualKeyboarder.Forms
{
    partial class KeyboardSettingForm
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
            btnUpdate = new Button();
            lblMessage = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            lblLeftKeyboard = new Label();
            lblRightKeyboard = new Label();
            lblLeftKeyboardId = new Label();
            lblRightKeyboardId = new Label();
            SuspendLayout();
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(26, 23);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(433, 35);
            btnUpdate.TabIndex = 0;
            btnUpdate.Text = "更新";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(26, 75);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(280, 15);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "キーボードを更新する場合、更新ボタンをクリックしてください";
            // 
            // btnOk
            // 
            btnOk.Enabled = false;
            btnOk.Location = new Point(303, 206);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 2;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(384, 206);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblLeftKeyboard
            // 
            lblLeftKeyboard.AutoSize = true;
            lblLeftKeyboard.Location = new Point(54, 112);
            lblLeftKeyboard.Name = "lblLeftKeyboard";
            lblLeftKeyboard.Size = new Size(86, 15);
            lblLeftKeyboard.TabIndex = 4;
            lblLeftKeyboard.Text = "左のキーボードID:";
            // 
            // lblRightKeyboard
            // 
            lblRightKeyboard.AutoSize = true;
            lblRightKeyboard.Location = new Point(54, 149);
            lblRightKeyboard.Name = "lblRightKeyboard";
            lblRightKeyboard.Size = new Size(86, 15);
            lblRightKeyboard.TabIndex = 5;
            lblRightKeyboard.Text = "右のキーボードID:";
            // 
            // lblLeftKeyboardId
            // 
            lblLeftKeyboardId.AutoSize = true;
            lblLeftKeyboardId.Location = new Point(171, 112);
            lblLeftKeyboardId.Name = "lblLeftKeyboardId";
            lblLeftKeyboardId.Size = new Size(0, 15);
            lblLeftKeyboardId.TabIndex = 6;
            // 
            // lblRightKeyboardId
            // 
            lblRightKeyboardId.AutoSize = true;
            lblRightKeyboardId.Location = new Point(171, 149);
            lblRightKeyboardId.Name = "lblRightKeyboardId";
            lblRightKeyboardId.Size = new Size(0, 15);
            lblRightKeyboardId.TabIndex = 7;
            // 
            // KeyboardSettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(485, 253);
            Controls.Add(lblRightKeyboardId);
            Controls.Add(lblLeftKeyboardId);
            Controls.Add(lblRightKeyboard);
            Controls.Add(lblLeftKeyboard);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lblMessage);
            Controls.Add(btnUpdate);
            Name = "KeyboardSettingForm";
            Text = "キーボード更新フォーム";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUpdate;
        private Label lblMessage;
        private Button btnOk;
        private Button btnCancel;
        private Label lblLeftKeyboard;
        private Label lblRightKeyboard;
        private Label lblLeftKeyboardId;
        private Label lblRightKeyboardId;
    }
}