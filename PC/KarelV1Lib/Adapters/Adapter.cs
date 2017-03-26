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

using KarelV1Lib.Events;
using System;

namespace KarelV1Lib.Adapters
{
    public abstract class Adapter : IDisposable
    {

        #region Properties

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public abstract bool IsConnected { get; protected set; }

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public abstract int MaxTimeout { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        public abstract event EventHandler<StringEventArgs> OnMessage;

        #endregion

        /// <summary>
        /// Connect
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// Disconnect
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Reset the Robot.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Send request to the device.
        /// </summary>
        /// <param name="command"></param>
        public abstract void SendRequest(string command);

        public abstract void Dispose();
 
    }
}
