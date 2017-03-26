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

using KarelV1Lib.Data;
using KarelV1Lib.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KarelV1Lib
{
    public class ProgramController
    {

        #region Variables

        /// <summary>
        /// List of commands.
        /// </summary>
        private Positions commands = new Positions();

        /// <summary>
        /// Current command index.
        /// </summary>
        private int commandIndex = 0;

        /// <summary>
        /// Automatic move thread.
        /// </summary>
        private Thread automaticMove = null;

        #endregion

        #region Properties

        /// <summary>
        /// True when robot is flowing the commands.
        /// Else false.
        /// </summary>
        public bool IsRuning
        {
            private set;
            get;
        }

        /// <summary>
        /// Set running in loop.
        /// </summary>
        public bool LoopProgram { get; set; }

        /// <summary>
        /// Set of commands.
        /// </summary>
        public Positions Commands
        {
            set
            {
                this.commands = value;
            }
            get
            {
                return this.commands;
            }
        }

        /// <summary>
        /// Current command index.
        /// </summary>
        public int CurrentCommandIndex
        {
            get
            {
                return this.commandIndex;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// On execution index changed.
        /// </summary>
        public event EventHandler<IntEventArgs> OnExecutionIndexCanged;

        /// <summary>
        /// On running thought the commands.
        /// </summary>
        public event EventHandler<EventArgs> OnRuning;

        /// <summary>
        /// On finishing running trough commands.
        /// </summary>
        public event EventHandler<EventArgs> OnFinish;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramController()
        {
            this.Commands.Clear();
        }

        #endregion

        #region Automatic Control

        /// <summary>
        /// Run exact list of commands.
        /// </summary>
        /// <param name="commands">List of commands.</param>
        /// <param name="commandIndex">Index to start from.</param>
        public void RunProgram(int commandIndex = 0)
        {
            if (this.automaticMove != null && this.commands.Count > 0)
            {
                if (this.automaticMove.ThreadState == ThreadState.Running)
                {
                    return;
                }
            }

            this.commandIndex = commandIndex;
            this.automaticMove = new Thread(new ThreadStart(this.RunTroughtCmd));
            this.automaticMove.Start();
        }

        /// <summary>
        /// Resume the program from where it stops.
        /// </summary>
        public void ResumeProgram()
        {
            if (this.automaticMove != null)
            {
                if (this.automaticMove.ThreadState == ThreadState.Running)
                {
                    return;
                }
            }

            this.automaticMove = new Thread(new ThreadStart(this.RunTroughtCmd));
            this.automaticMove.Start();
        }

        /// <summary>
        /// Stop the program.
        /// </summary>
        public void StopProgram()
        {
            this.IsRuning = false;
            this.automaticMove = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Running thought thread.
        /// </summary>
        private void RunTroughtCmd()
        {
            this.IsRuning = true;
            this.commandIndex = 0;
            if (this.OnRuning != null)
            {
                this.OnRuning(this, new EventArgs());
            }

            while (this.IsRuning)
            {
                // Get the command.
                Position command = this.commands[this.commandIndex];

                // Calculate the delay.
                double delay = 100;

                // Send it to the robot.
                //this.MoveRelative(command);

                // Send execution index to listeners.
                if (this.OnExecutionIndexCanged != null)
                {
                    this.OnExecutionIndexCanged(this.OnExecutionIndexCanged, new IntEventArgs(this.commandIndex));
                }

                // Wait the robot to finish the motion.
                Thread.Sleep((int)delay + 1000);

                // Increment the index.
                this.commandIndex++;

                // Check the command index and program loop bit.
                if (this.commandIndex == this.commands.Count)
                {
                    if (this.LoopProgram)
                    {
                        this.commandIndex = 0;
                        this.IsRuning = true;
                    }
                    else
                    {
                        this.IsRuning = false;
                        break;
                    }
                }
            }

            if (this.OnFinish != null)
            {
                this.OnFinish(this, new EventArgs());
            }

            this.automaticMove = null;
        }

        #endregion

    }
}
