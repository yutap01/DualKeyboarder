using DualKeyboarder.Utilities;
using DualKeyboarder.Window;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DualKeyboarder.Guardian
{
    internal static class InputGuardian
    {
        /// <summary>
        /// コンソール起動イベントデリゲート
        /// </summary>
        public delegate void ConsoleEventHandler();

        /// <summary>
        /// コンソール起動イベント
        /// </summary>
        public static event ConsoleEventHandler? ConsoleEvent;

        /// <summary>
        /// アクションイベントデリゲート
        /// </summary>
        /// <param name="e">イベントパラメータ</param>
        public delegate void ActionEventHandler(ActionEventArgs e);

        /// <summary>
        /// アクションイベント
        /// </summary>
        public static event ActionEventHandler? ActionEvent;

        /// <summary>
        /// Shiftキーの仮想キーコード
        /// </summary>
        private const int vkShift = 0x10;

        /// <summary>
        /// 拡張アクションの対象となる修飾キーの仮想キーコードリスト
        /// </summary>
        /// <remarks>
        /// ScanCodeではなく仮想キーコードを使用する
        /// </remarks>
        private static IList<int> extentionVirtualKeyCodeList = new List<int>()
        {
            vkShift,
        };

        /// <summary>
        /// カーソルキー(左)の仮想キーコード
        /// </summary>
        private const int vkCursorLeft = 0x25;

        /// <summary>
        /// カーソルキー(右)の仮想キーコード
        /// </summary>
        private const int vkCursorRight = 0x27;

        /// <summary>
        /// コンソールアクションの対象となる仮想キーコードのペアのリスト
        /// </summary>
        private static IList<List<int>> consoleVirtualKeyCodesList = new List<List<int>>()
        {
            new List<int>(){vkCursorLeft,vkCursorRight},
        };

        /// <summary>
        /// [ハンドラ]キー押下イベント
        /// </summary>
        /// <param name="sender">イベント発行オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        public static void OnKeyInput(object? sender, RawInputEventArgs e)
        {
            var data = e.Data as RawInputKeyboardData;
            if(data == null)
            {
                Log.Error("キーボード以外の入力を受け付けています");
                Log.Error(e.ToString()!);
                throw new Exception("キーボード以外の入力を受け付けています");
            }

            Log.IndentUp();
            Log.Debug(data!.ToString()!);
            Log.IndentDown();

            // イベント判定と発火
            if(IsConsoleEventInput(data))
            {
                // コンソール起動
                ConsoleEvent?.Invoke();
            }
            else if(IsActionEventInput(data))
            {
                // アクション実行
                var keyboardId = RawInputDeviceHandle.GetRawValue(data.Header.DeviceHandle);
                var scanCode = data.Keyboard.ScanCode;
                var actionEventArgs = new ActionEventArgs(keyboardId, scanCode, IsExtentionKeyDown());
                ActionEvent?.Invoke(actionEventArgs);
            }
        }

        /// <summary>
        /// コンソールイベント判定
        /// </summary>
        /// <param name="data">入力データ</param>
        /// <returns>コンソールイベント発行入力か否か</returns>
        private static bool IsConsoleEventInput(RawInputKeyboardData data)
        {
            // Downのときのみ判定
            if((data.Keyboard.Flags & RawKeyboardFlags.Up) != 0)
            {
                return false;
            }
            
            return IsConsoleKeysDown();
        }

        /// <summary>
        /// アクションイベント判定
        /// </summary>
        /// <param name="data">入力データ</param>
        /// <returns>アクションイベント発行入力か否か</returns>
        private static bool IsActionEventInput(RawInputKeyboardData data)
        {
            // キーがUpされている場合True
            return (data.Keyboard.Flags & RawKeyboardFlags.Up) != 0;
        }

        /// <summary>
        /// 修飾キー判定
        /// </summary>
        /// <returns>修飾キーが押されている場合True</returns>
        private static bool IsExtentionKeyDown()
        {
            foreach(var virtualKeyCode in extentionVirtualKeyCodeList)
            {
                if(IsDown(virtualKeyCode))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// コンソールキー判定
        /// </summary>
        /// <returns>コンソールアクション用のキーが押下されている場合True</returns>
        private static bool IsConsoleKeysDown()
        {
            // 押されているキーの数の取得
            var downKeyCount = GetDownKeyCount();

            foreach (var virtualKeyCodes in consoleVirtualKeyCodesList)
            {
                // 押下しているキーの数が一致しない場合は次のリストへ
                if(virtualKeyCodes.Count != downKeyCount)
                {
                    continue;
                }

                foreach (var virtualKeyCode in virtualKeyCodes)
                {
                    if (!IsDown(virtualKeyCode))
                    {
                        continue;
                    }
                }
                return true;
            }
            return false;
        }

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// 指定仮想キーコードのキーが押されているか否かを判定する
        /// </summary>
        /// <param name="virtualKeyCode">仮想キーコード</param>
        /// <returns>押下されている場合True</returns>
        private static bool IsDown(int virtualKeyCode)
        {
            var keyState = new byte[256];
            return GetKeyboardState(keyState) && (keyState[virtualKeyCode] & 0x80) != 0;
        }

        /// <summary>
        /// 押されているキーの数を取得する
        /// </summary>
        private static int GetDownKeyCount()
        {
            var keyState = new byte[256];
            GetKeyboardState(keyState);
            return keyState.Count(x => (x & 0x80) != 0);
        }

    }
}
