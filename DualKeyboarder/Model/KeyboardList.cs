using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualKeyboarder.Model
{
    /// <summary>
    /// キーボードのインデックス
    /// </summary>
    public enum KeyboardIndex
    {
        Left = 0,
        Right = 1,
        None = KeyboardList.maxSize,
    }

    /// <summary>
    /// 接続されているキーボードの配列を扱うクラス
    /// </summary>
    internal static class KeyboardList
    {
        /// <summary>
        /// 管理対象のキーボードの最大数
        /// </summary>
        public const int maxSize = 2;

        /// <summary>
        /// キーボードIDのリスト
        /// </summary>
        private static IList<int> keyboardIdList = new List<int>();

        /// <summary>
        /// 指定のキーボードIDに対応するキーボードのインデックスを取得する
        /// </summary>
        /// <param name="keyboardId">指定のキーボードID</param>
        /// <returns>適合するキーボードのインデックス</returns>
        public static KeyboardIndex GetKeyboardIndex(int keyboardId)
        {
            if (keyboardIdList == null)
            {
                return KeyboardIndex.None;
            }

            for (var i = 0; i < keyboardIdList.Count; i++)
            {
                if (keyboardIdList[i] == keyboardId)
                {
                    return (KeyboardIndex)i;
                }
            }

            return KeyboardIndex.None;
        }

        /// <summary>
        /// デュアルモードであるかを判定する
        /// </summary>
        /// <returns>デュアルモードの場合True</returns>
        public static bool IsDualMode()
        {
            return keyboardIdList != null &&
                keyboardIdList.Count == maxSize &&
                keyboardIdList[(int)KeyboardIndex.Left] != keyboardIdList[(int)KeyboardIndex.Right];
        }

        /// <summary>
        /// シングルモードであるかを判定する
        /// </summary>
        public static bool IsSingleMode()
        {
            return !IsDualMode();
        }

        /// <summary>
        /// キーボードIDを設定する
        /// </summary>
        /// <param name="leftKeyboardId">左キーボードのキーボードID</param>
        /// <param name="rightKeyboardId">右キーボードのキーボードID</param>
        public static void SetKeyboardIds(int leftKeyboardId, int rightKeyboardId)
        {
            keyboardIdList.Clear();
            keyboardIdList.Add(leftKeyboardId);
            keyboardIdList.Add(rightKeyboardId);
        }
    }
}
