/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

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
