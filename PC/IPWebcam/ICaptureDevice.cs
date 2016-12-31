/*
    This file is part of Nine Men's Morris.
    Nine Men's Morris is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    Nine Men's Morris is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with Nine Men's Morris.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IPWebcam
{
    /// <summary>
    /// Capture image from imiging device. 
    /// </summary>
    public interface ICaptureDevice
    {
        Bitmap Capture();
    }
}
