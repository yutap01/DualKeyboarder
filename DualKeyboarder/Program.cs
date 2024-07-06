using DualKeyboarder.Forms;
using DualKeyboarder.Guardian;
using DualKeyboarder.Utilities;
using DualKeyboarder.Window;
using Linearstar.Windows.RawInput;
using System.Diagnostics;
using static DualKeyboarder.Define.Define;

namespace DualKeyboarder
{
    internal static class Program
    {
        #region メンバ

        /// <summary>
        /// タスクトレイのアイコン
        /// </summary>
        private static NotifyIcon notifyIcon;

        #endregion

        /// <summary>
        /// メインエントリポイント
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Trace("多重起動チェック");
            if (MultiInvocationGuardian.IsAlreadyRunning())
            {
                // TODO:削除
                MessageBox.Show(errorMessageOfMultipleInvocation);

                // TODO:FunctionFormの表示
                return;
            }

            try
            {
                Log.Trace("アプリケーション設定の初期化");
                ApplicationConfiguration.Initialize();

                Log.Trace("アプリケーションの初期化");
                Initialize();

                Log.Trace("アプリケーションの実行");
                new KeyboardSettingForm().ShowDialog();
                Application.Run();
            } finally
            {
                Log.Trace("後処理");
                PostProcessing();
            }
        }


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="mainWindow">メインウィンドウ</param>
        private static void Initialize()
        {
            Log.IndentUp();

            // TODO:設定の読み込み

            Log.Trace("入力監視ウィンドウの初期化");
            var window = new RawInputReceiverWindow();
            window.Input += InputGuardian.OnKeyInput;
            RawInputDevice.RegisterDevice(
                HidUsageAndPage.Keyboard,
                RawInputDeviceFlags.ExInputSink | RawInputDeviceFlags.NoLegacy,
                window.Handle);

            Log.Trace("入力監視の開始");
            InputGuardian.ActionEvent += OnActionEvent;
            InputGuardian.ConsoleEvent += OnConsoleEvent;

            Log.IndentDown();
        }

        
        /// <summary>
        /// 後処理
        /// </summary>
        private static void PostProcessing()
        {
            Log.IndentUp();

            notifyIcon.Dispose();
            MultiInvocationGuardian.Dispose();
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
            Log.IndentDown();

            Log.Shutdown();
        }

        /// <summary>
        /// [ハンドラ]コンソールイベントハンドラ
        /// </summary>
        private static void OnConsoleEvent()
        {
            Log.IndentUp();
            Log.Debug("コンソールイベント");
            Log.IndentDown();
        }

        /// <summary>
        /// [ハンドラ]アクションイベントハンドラ
        /// </summary>
        /// <param name="e">イベントパラメータ</param>
        private static void OnActionEvent(ActionEventArgs e)
        {
            Log.IndentUp();
            Log.Debug("アクションイベント");
            var message = $"KeyboardId:{e.KeyboardId}, ScanCodeKey:{e.ScanCodeKey.ToString("X")}, IsExtention:{e.IsExtention}\n";
            Log.Debug(message);
            Debug.Print(message);
            Log.IndentDown();
        }
    }
}