using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Actuators
{
    class StepperMotor : Actuator
    {
        public StepperMotor()
        {
            this.Type = "StepperMotor";
            this.Unit = Scales.Steps;
        }
    }
}
