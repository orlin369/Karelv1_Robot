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

using System.Windows.Forms;

namespace InputMethods.KeystrokEventGenerator
{
    class KeyCombinations
    {
        /// <summary>
        /// Open applicationConfiguration manager.
        /// </summary>
        public const int OPEN_CONFIGURATION_MANAGER = (int)(Keys.Control | Keys.Alt | Keys.C);

        /// <summary>
        /// Open applicationConfiguration manager.
        /// </summary>
        public const int CLOSE_CONFIGURATION_MANAGER = (int)Keys.Escape;

        /// <summary>
        /// Configuration manager key combination.
        /// </summary>
        public const int APPLICATION_EXIT = (int)(Keys.Control | Keys.Alt | Keys.X);

        /// <summary>
        /// Move robot forward.
        /// </summary>
        public const int FORWARD = (int)(Keys.Control | Keys.Alt | Keys.Up);
        
        /// <summary>
        /// Move robot down.
        /// </summary>
        public const int BACKWARD = (int)(Keys.Control | Keys.Alt | Keys.Down);
        
        /// <summary>
        /// Move robot left.
        /// </summary>
        public const int LEFT = (int)(Keys.Control | Keys.Alt | Keys.Left);

        /// <summary>
        /// Move robot right.
        /// </summary>
        public const int RIGHT = (int)(Keys.Control | Keys.Alt | Keys.Right);


        /*
        /// <summary>
        /// Copy
        /// </summary>
        public const int CTRL_C = (int)(Keys.Control | Keys.C);

        /// <summary>
        /// Cut
        /// </summary>
        public const int CTRL_X = (int)(Keys.Control | Keys.X);
        
        /// <summary>
        /// Paste
        /// </summary>
        public const int CTRL_V = (int)(Keys.Control | Keys.V);

        /// <summary>
        /// Arow up
        /// </summary>
        public const int AROW_UP = (int)Keys.Up;
        
        */


    }
}
