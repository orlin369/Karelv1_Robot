using System;
using System.Windows.Forms;

namespace InputMethods.KeystrokEventGenerator
{
    public class KeystrokMessageFilter : IMessageFilter
    {

        #region Events

        /// <summary>
        /// Calls when user need to open applicationConfiguration manager form.
        /// </summary>
        public event EventHandler<EventArgs> OnOpenConfigurationManager;

        /// <summary>
        /// Calls when user need to close applicationConfiguration manager form.
        /// </summary>
        public event EventHandler<EventArgs> OnCloseConfigurationManager;

        /// <summary>
        /// Close the application when we need.
        /// </summary>
        public event EventHandler<EventArgs> OnExitApplication;

        public event EventHandler<EventArgs> OnForward;
        public event EventHandler<EventArgs> OnBackward;
        public event EventHandler<EventArgs> OnLeft;
        public event EventHandler<EventArgs> OnRight;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public KeystrokMessageFilter()
        {
        
        }

        #endregion

        #region Implementation of IMessageFilter

        /// <summary>
        /// Filter of the key strokes.
        /// </summary>
        /// <param name="m">Message argument.</param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == 256 /*0x0100*/))
            {
                switch (((int)m.WParam) | ((int)Control.ModifierKeys))
                {
                    case KeyCombinations.OPEN_CONFIGURATION_MANAGER:
                        if(this.OnOpenConfigurationManager != null)
                        {
                            this.OnOpenConfigurationManager(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.CLOSE_CONFIGURATION_MANAGER:
                        if (this.OnCloseConfigurationManager != null)
                        {
                            this.OnCloseConfigurationManager(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.APPLICATION_EXIT:
                        if (this.OnExitApplication != null)
                        {
                            this.OnExitApplication(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.FORWARD:
                        if (this.OnForward != null)
                        {
                            this.OnForward(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.BACKWARD:
                        if (this.OnBackward != null)
                        {
                            this.OnBackward(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.LEFT:
                        if (this.OnLeft != null)
                        {
                            this.OnLeft(this, new EventArgs());
                        }

                        break;

                    case KeyCombinations.RIGHT:
                        if (this.OnRight != null)
                        {
                            this.OnRight(this, new EventArgs());
                        }

                        break;

                    //This does not work. It seems you can only check single character along with CTRL and ALT.
                    //case (int)(Keys.Control | Keys.Alt | Keys.K | Keys.P):
                    //    MessageBox.Show("You pressed ctrl + alt + k + p");
                    //    break;
                }
            }
            return false;
        }

        #endregion

    }
}
