using System;
using Linearstar.Windows.RawInput;

namespace DualKeyboarder.Window
{
    /// <summary>
    /// キーボード入力時に発行されるイベントパラメータ
    /// </summary>
    internal class RawInputEventArgs : EventArgs
    {
        /// <summary>
        /// 入力の内容を表すデータ
        /// </summary>
        public RawInputData Data { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">入力の内容を表すデータ</param>
        public RawInputEventArgs(RawInputData data)
        {
            Data = data;
        }
    }
}