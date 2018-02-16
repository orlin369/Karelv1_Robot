/*

Copyright (c) [2018] [Orlin Dimitrov]

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

#include "DifferentialModel.h"

/** @brief Convert distance to steps.
 *  @param distance, Distance [mm]
 *  @return int, Steps.
 *
 *  @see http://www.education.rec.ri.cmu.edu/previews/rcx_products/robotics_educator_workbook/content/mech/pages/Diameter_Distance_TraveledTEACH.pdf
 *  
 *  In this case:
 *  WheelCircumferenceL = 3.14 * 53.5;
 *  RevolutionsL = 10 / WheelCircumferenceL; [distance = 10mm]
 *  StepsL = RevolutionsL * 200 * 1;
 *  StepsL = 
 *
 */
int MmToStep(double distance)
{
	double WheelCircumferenceL = PI * WHEEL_DIAMETTER;
	double RevolutionsL = distance / WheelCircumferenceL;
	double StepsL = RevolutionsL * MOTOR_STEPS_COUNT * POST_SCALER;
	return (int)round(StepsL);
}

/** @brief Convert degree to steps.
 *  @param degrees, Degrees [deg]
 *  @return int, Steps.
 *  @see https://www.mathopenref.com/arclength.html
 */
int DegToStep(double degrees)
{
	// Length of base wheel.
	double PerimetterCircumferenceL = (DISTANCE_BETWEEN_WHEELS * PI);
	
	// Calculate mm per degree coefficient.
	double ArcLengthL = PerimetterCircumferenceL * (degrees / 360.0F);

	// Calculate steps.
	return MmToStep(ArcLengthL);
}
