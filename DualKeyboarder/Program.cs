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
        #region �����o

        /// <summary>
        /// �^�X�N�g���C�̃A�C�R��
        /// </summary>
        private static NotifyIcon notifyIcon;

        #endregion

        /// <summary>
        /// ���C���G���g���|�C���g
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Trace("���d�N���`�F�b�N");
            if (MultiInvocationGuardian.IsAlreadyRunning())
            {
                // TODO:�폜
                MessageBox.Show(errorMessageOfMultipleInvocation);

                // TODO:FunctionForm�̕\��
                return;
            }

            try
            {
                Log.Trace("�A�v���P�[�V�����ݒ�̏�����");
                ApplicationConfiguration.Initialize();

                Log.Trace("�A�v���P�[�V�����̏�����");
                Initialize();

                Log.Trace("�A�v���P�[�V�����̎��s");
                new KeyboardSettingForm().ShowDialog();
                Application.Run();
            } finally
            {
                Log.Trace("�㏈��");
                PostProcessing();
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="mainWindow">���C���E�B���h�E</param>
        private static void Initialize()
        {
            Log.IndentUp();

            // TODO:�ݒ�̓ǂݍ���

            Log.Trace("���͊Ď��E�B���h�E�̏�����");
            var window = new RawInputReceiverWindow();
            window.Input += InputGuardian.OnKeyInput;
            RawInputDevice.RegisterDevice(
                HidUsageAndPage.Keyboard,
                RawInputDeviceFlags.ExInputSink | RawInputDeviceFlags.NoLegacy,
                window.Handle);

            Log.Trace("���͊Ď��̊J�n");
            InputGuardian.ActionEvent += OnActionEvent;
            InputGuardian.ConsoleEvent += OnConsoleEvent;

            Log.IndentDown();
        }

        
        /// <summary>
        /// �㏈��
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
        /// [�n���h��]�R���\�[���C�x���g�n���h��
        /// </summary>
        private static void OnConsoleEvent()
        {
            Log.IndentUp();
            Log.Debug("�R���\�[���C�x���g");
            Log.IndentDown();
        }

        /// <summary>
        /// [�n���h��]�A�N�V�����C�x���g�n���h��
        /// </summary>
        /// <param name="e">�C�x���g�p�����[�^</param>
        private static void OnActionEvent(ActionEventArgs e)
        {
            Log.IndentUp();
            Log.Debug("�A�N�V�����C�x���g");
            var message = $"KeyboardId:{e.KeyboardId}, ScanCodeKey:{e.ScanCodeKey.ToString("X")}, IsExtention:{e.IsExtention}\n";
            Log.Debug(message);
            Debug.Print(message);
            Log.IndentDown();
        }
    }
}