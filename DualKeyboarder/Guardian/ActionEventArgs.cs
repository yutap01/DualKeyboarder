using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualKeyboarder.Guardian
{
    internal class ActionEventArgs
    {
        public nint KeyboardId { get; }
        public int ScanCodeKey { get; }
        public bool IsExtention { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="keyboardId">キーボードID</param>
        /// <param name="scanCode">スキャンコード</param>
        /// <param name="isExtention">拡張アクションか否か</param>
        public ActionEventArgs(nint keyboardId, int scanCode, bool isExtention)
        {
            KeyboardId = keyboardId;
            ScanCodeKey = scanCode;
            IsExtention = isExtention;
        }
    }
}
