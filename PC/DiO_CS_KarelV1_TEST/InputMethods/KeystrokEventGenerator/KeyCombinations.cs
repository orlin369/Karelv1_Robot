using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
