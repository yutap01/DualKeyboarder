using DualKeyboarder.Guardian;
using DualKeyboarder.Model;
using DualKeyboarder.Utilities;
using DualKeyboarder.Window;
using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualKeyboarder.Forms
{
    public partial class KeyboardSettingForm : Form
    {
        /// <summary>
        /// 設定対象のキーボードインデックス
        /// </summary>
        private KeyboardIndex targetKeyboardIndex = KeyboardIndex.None;

        public KeyboardSettingForm()
        {
            InitializeComponent();
            InputGuardian.ActionEvent += OnActionEvent;
        }

        /// <summary>
        /// [ハンドラ]更新ボタン押下時の処理
        /// </summary>
        /// <param name="sender">イベント送信者</param>
        /// <param name="e">イベントパラメータ</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblLeftKeyboardId.Text = string.Empty;
            lblRightKeyboardId.Text = string.Empty;
            UpdateKeyboard();
        }

        private void OnActionEvent(ActionEventArgs e)
        {
            var message = $"KeyboardId:{e.KeyboardId}, ScanCodeKey:{e.ScanCodeKey.ToString("X")}, IsExtention:{e.IsExtention}\n";
            switch (targetKeyboardIndex)
            {
                case KeyboardIndex.Left:
                    lblLeftKeyboardId.Text = e.KeyboardId.ToString();
                    lblMessage.Text = "右のキーボードの任意のキーを押下して離してください。";
                    targetKeyboardIndex = KeyboardIndex.Right;
                    break;
                case KeyboardIndex.Right:
                    lblRightKeyboardId.Text = e.KeyboardId.ToString();
                    lblMessage.Text = "設定が完了しました。";
                    targetKeyboardIndex = KeyboardIndex.None;
                    btnOk.Enabled = true;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// キーボードの更新処理
        /// </summary>
        private void UpdateKeyboard()
        {
            btnOk.Enabled = false;
            Log.Trace("キーボードの更新");
            // 左のキーボードの更新
            targetKeyboardIndex = KeyboardIndex.Left;
            lblMessage.Text = "左のキーボードの任意のキーを押下して離してください。";
        }

        /// <summary>
        /// [ハンドラ]OKボタン押下時の処理
        /// </summary>
        /// <param name="sender">イベント送信者</param>
        /// <param name="e">イベントパラメータ</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            KeyboardList.SetKeyboardIds(int.Parse(lblLeftKeyboardId.Text), int.Parse(lblRightKeyboardId.Text));
            InputGuardian.ActionEvent -= OnActionEvent;
            Close();
        }

        /// <summary>
        /// [ハンドラ]キャンセルボタン押下時の処理
        /// </summary>
        /// <param name="sender">イベント送信者</param>
        /// <param name="e">イベントパラメータ</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
