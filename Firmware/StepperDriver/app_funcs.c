#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"


/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_ENABLE_MOTORS,
	&app_read_REG_DISABLE_MOTORS,
	&app_read_REG_ENABLE_ENCODERS,
	&app_read_REG_DISABLE_ENCODERS,
	&app_read_REG_ENABLE_INPUTS,
	&app_read_REG_DISABLE_INPUTS,
	&app_read_REG_MOTOR0_OPERATION_MODE,
	&app_read_REG_MOTOR1_OPERATION_MODE,
	&app_read_REG_MOTOR2_OPERATION_MODE,
	&app_read_REG_MOTOR3_OPERATION_MODE,
	&app_read_REG_MOTOR0_MICROSTEP_RESOLUTION,
	&app_read_REG_MOTOR1_MICROSTEP_RESOLUTION,
	&app_read_REG_MOTOR2_MICROSTEP_RESOLUTION,
	&app_read_REG_MOTOR3_MICROSTEP_RESOLUTION,
	&app_read_REG_MOTOR0_MAXIMUM_CURRENT_RMS,
	&app_read_REG_MOTOR1_MAXIMUM_CURRENT_RMS,
	&app_read_REG_MOTOR2_MAXIMUM_CURRENT_RMS,
	&app_read_REG_MOTOR3_MAXIMUM_CURRENT_RMS,
	&app_read_REG_MOTOR0_NOMINAL_STEP_INTERVAL,
	&app_read_REG_MOTOR1_NOMINAL_STEP_INTERVAL,
	&app_read_REG_MOTOR2_NOMINAL_STEP_INTERVAL,
	&app_read_REG_MOTOR3_NOMINAL_STEP_INTERVAL,
	&app_read_REG_MOTOR0_MAXIMUM_STEP_INTERVAL,
	&app_read_REG_MOTOR1_MAXIMUM_STEP_INTERVAL,
	&app_read_REG_MOTOR2_MAXIMUM_STEP_INTERVAL,
	&app_read_REG_MOTOR3_MAXIMUM_STEP_INTERVAL,
	&app_read_REG_MOTOR0_STEP_ACCELERATION_INTERVAL,
	&app_read_REG_MOTOR1_STEP_ACCELERATION_INTERVAL,
	&app_read_REG_MOTOR2_STEP_ACCELERATION_INTERVAL,
	&app_read_REG_MOTOR3_STEP_ACCELERATION_INTERVAL,
	&app_read_REG_ENCODERS_MODE,
	&app_read_REG_ENCODERS_UPDATE_RATE,
	&app_read_REG_INPUT0_OPERATION_MODE,
	&app_read_REG_INPUT1_OPERATION_MODE,
	&app_read_REG_INPUT2_OPERATION_MODE,
	&app_read_REG_INPUT3_OPERATION_MODE,
	&app_read_REG_INPUT0_SENSE_MODE,
	&app_read_REG_INPUT1_SENSE_MODE,
	&app_read_REG_INPUT2_SENSE_MODE,
	&app_read_REG_INPUT3_SENSE_MODE,
	&app_read_REG_EMERGENCY_DETECTION_MODE,
	&app_read_REG_MOTORS_STOPPED,
	&app_read_REG_MOTORS_OVERVOLTAGE_DETECTION,
	&app_read_REG_MOTORS_ERROR_DETECTTION,
	&app_read_REG_ENCODERS,
	&app_read_REG_DIGITAL_INPUTS_STATE,
	&app_read_REG_EMERGENCY_DETECTION,
	&app_read_REG_MOTOR0_STEPS,
	&app_read_REG_MOTOR1_STEPS,
	&app_read_REG_MOTOR2_STEPS,
	&app_read_REG_MOTOR3_STEPS,
	&app_read_REG_MOTOR0_MAX_STEPS_INTEGRATION,
	&app_read_REG_MOTOR1_MAX_STEPS_INTEGRATION,
	&app_read_REG_MOTOR2_MAX_STEPS_INTEGRATION,
	&app_read_REG_MOTOR3_MAX_STEPS_INTEGRATION,
	&app_read_REG_MOTOR0_MIN_STEPS_INTEGRATION,
	&app_read_REG_MOTOR1_MIN_STEPS_INTEGRATION,
	&app_read_REG_MOTOR2_MIN_STEPS_INTEGRATION,
	&app_read_REG_MOTOR3_MIN_STEPS_INTEGRATION,
	&app_read_REG_MOTOR0_IMMEDIATE_STEPS,
	&app_read_REG_MOTOR1_IMMEDIATE_STEPS,
	&app_read_REG_MOTOR2_IMMEDIATE_STEPS,
	&app_read_REG_MOTOR3_IMMEDIATE_STEPS,
	&app_read_REG_STOP_MOTORS_SUDENTLY,
	&app_read_REG_STOP_MOTORS_BY_DECELERATION,
	&app_read_REG_RESET_MOTORS_ERROR_DETECTION,
	&app_read_REG_RESET_ENCODERS,
	&app_read_REG_RESERVED0,
	&app_read_REG_RESERVED1,
	&app_read_REG_RESERVED2,
	&app_read_REG_RESERVED3,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_RESERVED6,
	&app_read_REG_RESERVED7
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_ENABLE_MOTORS,
	&app_write_REG_DISABLE_MOTORS,
	&app_write_REG_ENABLE_ENCODERS,
	&app_write_REG_DISABLE_ENCODERS,
	&app_write_REG_ENABLE_INPUTS,
	&app_write_REG_DISABLE_INPUTS,
	&app_write_REG_MOTOR0_OPERATION_MODE,
	&app_write_REG_MOTOR1_OPERATION_MODE,
	&app_write_REG_MOTOR2_OPERATION_MODE,
	&app_write_REG_MOTOR3_OPERATION_MODE,
	&app_write_REG_MOTOR0_MICROSTEP_RESOLUTION,
	&app_write_REG_MOTOR1_MICROSTEP_RESOLUTION,
	&app_write_REG_MOTOR2_MICROSTEP_RESOLUTION,
	&app_write_REG_MOTOR3_MICROSTEP_RESOLUTION,
	&app_write_REG_MOTOR0_MAXIMUM_CURRENT_RMS,
	&app_write_REG_MOTOR1_MAXIMUM_CURRENT_RMS,
	&app_write_REG_MOTOR2_MAXIMUM_CURRENT_RMS,
	&app_write_REG_MOTOR3_MAXIMUM_CURRENT_RMS,
	&app_write_REG_MOTOR0_NOMINAL_STEP_INTERVAL,
	&app_write_REG_MOTOR1_NOMINAL_STEP_INTERVAL,
	&app_write_REG_MOTOR2_NOMINAL_STEP_INTERVAL,
	&app_write_REG_MOTOR3_NOMINAL_STEP_INTERVAL,
	&app_write_REG_MOTOR0_MAXIMUM_STEP_INTERVAL,
	&app_write_REG_MOTOR1_MAXIMUM_STEP_INTERVAL,
	&app_write_REG_MOTOR2_MAXIMUM_STEP_INTERVAL,
	&app_write_REG_MOTOR3_MAXIMUM_STEP_INTERVAL,
	&app_write_REG_MOTOR0_STEP_ACCELERATION_INTERVAL,
	&app_write_REG_MOTOR1_STEP_ACCELERATION_INTERVAL,
	&app_write_REG_MOTOR2_STEP_ACCELERATION_INTERVAL,
	&app_write_REG_MOTOR3_STEP_ACCELERATION_INTERVAL,
	&app_write_REG_ENCODERS_MODE,
	&app_write_REG_ENCODERS_UPDATE_RATE,
	&app_write_REG_INPUT0_OPERATION_MODE,
	&app_write_REG_INPUT1_OPERATION_MODE,
	&app_write_REG_INPUT2_OPERATION_MODE,
	&app_write_REG_INPUT3_OPERATION_MODE,
	&app_write_REG_INPUT0_SENSE_MODE,
	&app_write_REG_INPUT1_SENSE_MODE,
	&app_write_REG_INPUT2_SENSE_MODE,
	&app_write_REG_INPUT3_SENSE_MODE,
	&app_write_REG_EMERGENCY_DETECTION_MODE,
	&app_write_REG_MOTORS_STOPPED,
	&app_write_REG_MOTORS_OVERVOLTAGE_DETECTION,
	&app_write_REG_MOTORS_ERROR_DETECTTION,
	&app_write_REG_ENCODERS,
	&app_write_REG_DIGITAL_INPUTS_STATE,
	&app_write_REG_EMERGENCY_DETECTION,
	&app_write_REG_MOTOR0_STEPS,
	&app_write_REG_MOTOR1_STEPS,
	&app_write_REG_MOTOR2_STEPS,
	&app_write_REG_MOTOR3_STEPS,
	&app_write_REG_MOTOR0_MAX_STEPS_INTEGRATION,
	&app_write_REG_MOTOR1_MAX_STEPS_INTEGRATION,
	&app_write_REG_MOTOR2_MAX_STEPS_INTEGRATION,
	&app_write_REG_MOTOR3_MAX_STEPS_INTEGRATION,
	&app_write_REG_MOTOR0_MIN_STEPS_INTEGRATION,
	&app_write_REG_MOTOR1_MIN_STEPS_INTEGRATION,
	&app_write_REG_MOTOR2_MIN_STEPS_INTEGRATION,
	&app_write_REG_MOTOR3_MIN_STEPS_INTEGRATION,
	&app_write_REG_MOTOR0_IMMEDIATE_STEPS,
	&app_write_REG_MOTOR1_IMMEDIATE_STEPS,
	&app_write_REG_MOTOR2_IMMEDIATE_STEPS,
	&app_write_REG_MOTOR3_IMMEDIATE_STEPS,
	&app_write_REG_STOP_MOTORS_SUDENTLY,
	&app_write_REG_STOP_MOTORS_BY_DECELERATION,
	&app_write_REG_RESET_MOTORS_ERROR_DETECTION,
	&app_write_REG_RESET_ENCODERS,
	&app_write_REG_RESERVED0,
	&app_write_REG_RESERVED1,
	&app_write_REG_RESERVED2,
	&app_write_REG_RESERVED3,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_RESERVED6,
	&app_write_REG_RESERVED7
};


/************************************************************************/
/* REG_ENABLE_MOTORS                                                    */
/************************************************************************/
void app_read_REG_ENABLE_MOTORS(void)
{
	//app_regs.REG_ENABLE_MOTORS = 0;

}

bool app_write_REG_ENABLE_MOTORS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_ENABLE_MOTORS = reg;
	return true;
}


/************************************************************************/
/* REG_DISABLE_MOTORS                                                   */
/************************************************************************/
void app_read_REG_DISABLE_MOTORS(void)
{
	//app_regs.REG_DISABLE_MOTORS = 0;

}

bool app_write_REG_DISABLE_MOTORS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DISABLE_MOTORS = reg;
	return true;
}


/************************************************************************/
/* REG_ENABLE_ENCODERS                                                  */
/************************************************************************/
void app_read_REG_ENABLE_ENCODERS(void)
{
	//app_regs.REG_ENABLE_ENCODERS = 0;

}

bool app_write_REG_ENABLE_ENCODERS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_ENABLE_ENCODERS = reg;
	return true;
}


/************************************************************************/
/* REG_DISABLE_ENCODERS                                                 */
/************************************************************************/
void app_read_REG_DISABLE_ENCODERS(void)
{
	//app_regs.REG_DISABLE_ENCODERS = 0;

}

bool app_write_REG_DISABLE_ENCODERS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DISABLE_ENCODERS = reg;
	return true;
}


/************************************************************************/
/* REG_ENABLE_INPUTS                                                    */
/************************************************************************/
void app_read_REG_ENABLE_INPUTS(void)
{
	//app_regs.REG_ENABLE_INPUTS = 0;

}

bool app_write_REG_ENABLE_INPUTS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_ENABLE_INPUTS = reg;
	return true;
}


/************************************************************************/
/* REG_DISABLE_INPUTS                                                   */
/************************************************************************/
void app_read_REG_DISABLE_INPUTS(void)
{
	//app_regs.REG_DISABLE_INPUTS = 0;

}

bool app_write_REG_DISABLE_INPUTS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DISABLE_INPUTS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_MOTOR0_OPERATION_MODE(void)
{
	//app_regs.REG_MOTOR0_OPERATION_MODE = 0;

}

bool app_write_REG_MOTOR0_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR0_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_MOTOR1_OPERATION_MODE(void)
{
	//app_regs.REG_MOTOR1_OPERATION_MODE = 0;

}

bool app_write_REG_MOTOR1_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR1_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_MOTOR2_OPERATION_MODE(void)
{
	//app_regs.REG_MOTOR2_OPERATION_MODE = 0;

}

bool app_write_REG_MOTOR2_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR2_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_MOTOR3_OPERATION_MODE(void)
{
	//app_regs.REG_MOTOR3_OPERATION_MODE = 0;

}

bool app_write_REG_MOTOR3_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR3_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_MICROSTEP_RESOLUTION                                      */
/************************************************************************/
void app_read_REG_MOTOR0_MICROSTEP_RESOLUTION(void)
{
	//app_regs.REG_MOTOR0_MICROSTEP_RESOLUTION = 0;

}

bool app_write_REG_MOTOR0_MICROSTEP_RESOLUTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR0_MICROSTEP_RESOLUTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_MICROSTEP_RESOLUTION                                      */
/************************************************************************/
void app_read_REG_MOTOR1_MICROSTEP_RESOLUTION(void)
{
	//app_regs.REG_MOTOR1_MICROSTEP_RESOLUTION = 0;

}

bool app_write_REG_MOTOR1_MICROSTEP_RESOLUTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR1_MICROSTEP_RESOLUTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_MICROSTEP_RESOLUTION                                      */
/************************************************************************/
void app_read_REG_MOTOR2_MICROSTEP_RESOLUTION(void)
{
	//app_regs.REG_MOTOR2_MICROSTEP_RESOLUTION = 0;

}

bool app_write_REG_MOTOR2_MICROSTEP_RESOLUTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR2_MICROSTEP_RESOLUTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_MICROSTEP_RESOLUTION                                      */
/************************************************************************/
void app_read_REG_MOTOR3_MICROSTEP_RESOLUTION(void)
{
	//app_regs.REG_MOTOR3_MICROSTEP_RESOLUTION = 0;

}

bool app_write_REG_MOTOR3_MICROSTEP_RESOLUTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTOR3_MICROSTEP_RESOLUTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_MAXIMUM_CURRENT_RMS                                       */
/************************************************************************/
void app_read_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void)
{
	//app_regs.REG_MOTOR0_MAXIMUM_CURRENT_RMS = 0;

}

bool app_write_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void *a)
{
	float reg = *((float*)a);

	app_regs.REG_MOTOR0_MAXIMUM_CURRENT_RMS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_MAXIMUM_CURRENT_RMS                                       */
/************************************************************************/
void app_read_REG_MOTOR1_MAXIMUM_CURRENT_RMS(void)
{
	//app_regs.REG_MOTOR1_MAXIMUM_CURRENT_RMS = 0;

}

bool app_write_REG_MOTOR1_MAXIMUM_CURRENT_RMS(void *a)
{
	float reg = *((float*)a);

	app_regs.REG_MOTOR1_MAXIMUM_CURRENT_RMS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_MAXIMUM_CURRENT_RMS                                       */
/************************************************************************/
void app_read_REG_MOTOR2_MAXIMUM_CURRENT_RMS(void)
{
	//app_regs.REG_MOTOR2_MAXIMUM_CURRENT_RMS = 0;

}

bool app_write_REG_MOTOR2_MAXIMUM_CURRENT_RMS(void *a)
{
	float reg = *((float*)a);

	app_regs.REG_MOTOR2_MAXIMUM_CURRENT_RMS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_MAXIMUM_CURRENT_RMS                                       */
/************************************************************************/
void app_read_REG_MOTOR3_MAXIMUM_CURRENT_RMS(void)
{
	//app_regs.REG_MOTOR3_MAXIMUM_CURRENT_RMS = 0;

}

bool app_write_REG_MOTOR3_MAXIMUM_CURRENT_RMS(void *a)
{
	float reg = *((float*)a);

	app_regs.REG_MOTOR3_MAXIMUM_CURRENT_RMS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_NOMINAL_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR0_NOMINAL_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR0_NOMINAL_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR0_NOMINAL_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR0_NOMINAL_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_NOMINAL_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR1_NOMINAL_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR1_NOMINAL_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR1_NOMINAL_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR1_NOMINAL_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_NOMINAL_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR2_NOMINAL_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR2_NOMINAL_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR2_NOMINAL_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR2_NOMINAL_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_NOMINAL_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR3_NOMINAL_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR3_NOMINAL_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR3_NOMINAL_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR3_NOMINAL_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_MAXIMUM_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR0_MAXIMUM_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR0_MAXIMUM_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR0_MAXIMUM_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR0_MAXIMUM_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_MAXIMUM_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR1_MAXIMUM_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR1_MAXIMUM_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR1_MAXIMUM_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR1_MAXIMUM_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_MAXIMUM_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR2_MAXIMUM_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR2_MAXIMUM_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR2_MAXIMUM_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR2_MAXIMUM_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_MAXIMUM_STEP_INTERVAL                                     */
/************************************************************************/
void app_read_REG_MOTOR3_MAXIMUM_STEP_INTERVAL(void)
{
	//app_regs.REG_MOTOR3_MAXIMUM_STEP_INTERVAL = 0;

}

bool app_write_REG_MOTOR3_MAXIMUM_STEP_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR3_MAXIMUM_STEP_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_STEP_ACCELERATION_INTERVAL                                */
/************************************************************************/
void app_read_REG_MOTOR0_STEP_ACCELERATION_INTERVAL(void)
{
	//app_regs.REG_MOTOR0_STEP_ACCELERATION_INTERVAL = 0;

}

bool app_write_REG_MOTOR0_STEP_ACCELERATION_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR0_STEP_ACCELERATION_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_STEP_ACCELERATION_INTERVAL                                */
/************************************************************************/
void app_read_REG_MOTOR1_STEP_ACCELERATION_INTERVAL(void)
{
	//app_regs.REG_MOTOR1_STEP_ACCELERATION_INTERVAL = 0;

}

bool app_write_REG_MOTOR1_STEP_ACCELERATION_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR1_STEP_ACCELERATION_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_STEP_ACCELERATION_INTERVAL                                */
/************************************************************************/
void app_read_REG_MOTOR2_STEP_ACCELERATION_INTERVAL(void)
{
	//app_regs.REG_MOTOR2_STEP_ACCELERATION_INTERVAL = 0;

}

bool app_write_REG_MOTOR2_STEP_ACCELERATION_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR2_STEP_ACCELERATION_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_STEP_ACCELERATION_INTERVAL                                */
/************************************************************************/
void app_read_REG_MOTOR3_STEP_ACCELERATION_INTERVAL(void)
{
	//app_regs.REG_MOTOR3_STEP_ACCELERATION_INTERVAL = 0;

}

bool app_write_REG_MOTOR3_STEP_ACCELERATION_INTERVAL(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_MOTOR3_STEP_ACCELERATION_INTERVAL = reg;
	return true;
}


/************************************************************************/
/* REG_ENCODERS_MODE                                                    */
/************************************************************************/
void app_read_REG_ENCODERS_MODE(void)
{
	//app_regs.REG_ENCODERS_MODE = 0;

}

bool app_write_REG_ENCODERS_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_ENCODERS_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_ENCODERS_UPDATE_RATE                                             */
/************************************************************************/
void app_read_REG_ENCODERS_UPDATE_RATE(void)
{
	//app_regs.REG_ENCODERS_UPDATE_RATE = 0;

}

bool app_write_REG_ENCODERS_UPDATE_RATE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_ENCODERS_UPDATE_RATE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT0_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_INPUT0_OPERATION_MODE(void)
{
	//app_regs.REG_INPUT0_OPERATION_MODE = 0;

}

bool app_write_REG_INPUT0_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT0_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT1_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_INPUT1_OPERATION_MODE(void)
{
	//app_regs.REG_INPUT1_OPERATION_MODE = 0;

}

bool app_write_REG_INPUT1_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT1_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT2_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_INPUT2_OPERATION_MODE(void)
{
	//app_regs.REG_INPUT2_OPERATION_MODE = 0;

}

bool app_write_REG_INPUT2_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT2_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT3_OPERATION_MODE                                            */
/************************************************************************/
void app_read_REG_INPUT3_OPERATION_MODE(void)
{
	//app_regs.REG_INPUT3_OPERATION_MODE = 0;

}

bool app_write_REG_INPUT3_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT3_OPERATION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT0_SENSE_MODE                                                */
/************************************************************************/
void app_read_REG_INPUT0_SENSE_MODE(void)
{
	//app_regs.REG_INPUT0_SENSE_MODE = 0;

}

bool app_write_REG_INPUT0_SENSE_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT0_SENSE_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT1_SENSE_MODE                                                */
/************************************************************************/
void app_read_REG_INPUT1_SENSE_MODE(void)
{
	//app_regs.REG_INPUT1_SENSE_MODE = 0;

}

bool app_write_REG_INPUT1_SENSE_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT1_SENSE_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT2_SENSE_MODE                                                */
/************************************************************************/
void app_read_REG_INPUT2_SENSE_MODE(void)
{
	//app_regs.REG_INPUT2_SENSE_MODE = 0;

}

bool app_write_REG_INPUT2_SENSE_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT2_SENSE_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_INPUT3_SENSE_MODE                                                */
/************************************************************************/
void app_read_REG_INPUT3_SENSE_MODE(void)
{
	//app_regs.REG_INPUT3_SENSE_MODE = 0;

}

bool app_write_REG_INPUT3_SENSE_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUT3_SENSE_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_EMERGENCY_DETECTION_MODE                                         */
/************************************************************************/
void app_read_REG_EMERGENCY_DETECTION_MODE(void)
{
	//app_regs.REG_EMERGENCY_DETECTION_MODE = 0;

}

bool app_write_REG_EMERGENCY_DETECTION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EMERGENCY_DETECTION_MODE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTORS_STOPPED                                                   */
/************************************************************************/
void app_read_REG_MOTORS_STOPPED(void)
{
	//app_regs.REG_MOTORS_STOPPED = 0;

}

bool app_write_REG_MOTORS_STOPPED(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTORS_STOPPED = reg;
	return true;
}


/************************************************************************/
/* REG_MOTORS_OVERVOLTAGE_DETECTION                                     */
/************************************************************************/
void app_read_REG_MOTORS_OVERVOLTAGE_DETECTION(void)
{
	//app_regs.REG_MOTORS_OVERVOLTAGE_DETECTION = 0;

}

bool app_write_REG_MOTORS_OVERVOLTAGE_DETECTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTORS_OVERVOLTAGE_DETECTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTORS_ERROR_DETECTTION                                          */
/************************************************************************/
void app_read_REG_MOTORS_ERROR_DETECTTION(void)
{
	//app_regs.REG_MOTORS_ERROR_DETECTTION = 0;

}

bool app_write_REG_MOTORS_ERROR_DETECTTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MOTORS_ERROR_DETECTTION = reg;
	return true;
}


/************************************************************************/
/* REG_ENCODERS                                                         */
/************************************************************************/
// This register is an array with 3 positions
void app_read_REG_ENCODERS(void)
{
	//app_regs.REG_ENCODERS[0] = 0;

}

bool app_write_REG_ENCODERS(void *a)
{
	int16_t *reg = ((int16_t*)a);

	app_regs.REG_ENCODERS[0] = reg[0];
	return true;
}


/************************************************************************/
/* REG_DIGITAL_INPUTS_STATE                                             */
/************************************************************************/
void app_read_REG_DIGITAL_INPUTS_STATE(void)
{
	//app_regs.REG_DIGITAL_INPUTS_STATE = 0;

}

bool app_write_REG_DIGITAL_INPUTS_STATE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DIGITAL_INPUTS_STATE = reg;
	return true;
}


/************************************************************************/
/* REG_EMERGENCY_DETECTION                                              */
/************************************************************************/
void app_read_REG_EMERGENCY_DETECTION(void)
{
	//app_regs.REG_EMERGENCY_DETECTION = 0;

}

bool app_write_REG_EMERGENCY_DETECTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EMERGENCY_DETECTION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_STEPS                                                     */
/************************************************************************/
void app_read_REG_MOTOR0_STEPS(void)
{
	//app_regs.REG_MOTOR0_STEPS = 0;

}

bool app_write_REG_MOTOR0_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR0_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_STEPS                                                     */
/************************************************************************/
void app_read_REG_MOTOR1_STEPS(void)
{
	//app_regs.REG_MOTOR1_STEPS = 0;

}

bool app_write_REG_MOTOR1_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR1_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_STEPS                                                     */
/************************************************************************/
void app_read_REG_MOTOR2_STEPS(void)
{
	//app_regs.REG_MOTOR2_STEPS = 0;

}

bool app_write_REG_MOTOR2_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR2_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_STEPS                                                     */
/************************************************************************/
void app_read_REG_MOTOR3_STEPS(void)
{
	//app_regs.REG_MOTOR3_STEPS = 0;

}

bool app_write_REG_MOTOR3_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR3_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_MAX_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR0_MAX_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR0_MAX_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_MAX_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR1_MAX_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR1_MAX_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR1_MAX_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR1_MAX_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_MAX_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR2_MAX_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR2_MAX_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR2_MAX_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR2_MAX_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_MAX_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR3_MAX_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR3_MAX_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR3_MAX_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR3_MAX_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_MIN_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR0_MIN_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR0_MIN_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_MIN_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR1_MIN_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR1_MIN_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR1_MIN_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR1_MIN_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_MIN_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR2_MIN_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR2_MIN_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR2_MIN_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR2_MIN_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_MIN_STEPS_INTEGRATION                                     */
/************************************************************************/
void app_read_REG_MOTOR3_MIN_STEPS_INTEGRATION(void)
{
	//app_regs.REG_MOTOR3_MIN_STEPS_INTEGRATION = 0;

}

bool app_write_REG_MOTOR3_MIN_STEPS_INTEGRATION(void *a)
{
	uint32_t reg = *((uint32_t*)a);

	app_regs.REG_MOTOR3_MIN_STEPS_INTEGRATION = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR0_IMMEDIATE_STEPS                                           */
/************************************************************************/
void app_read_REG_MOTOR0_IMMEDIATE_STEPS(void)
{
	//app_regs.REG_MOTOR0_IMMEDIATE_STEPS = 0;

}

bool app_write_REG_MOTOR0_IMMEDIATE_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR0_IMMEDIATE_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR1_IMMEDIATE_STEPS                                           */
/************************************************************************/
void app_read_REG_MOTOR1_IMMEDIATE_STEPS(void)
{
	//app_regs.REG_MOTOR1_IMMEDIATE_STEPS = 0;

}

bool app_write_REG_MOTOR1_IMMEDIATE_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR1_IMMEDIATE_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR2_IMMEDIATE_STEPS                                           */
/************************************************************************/
void app_read_REG_MOTOR2_IMMEDIATE_STEPS(void)
{
	//app_regs.REG_MOTOR2_IMMEDIATE_STEPS = 0;

}

bool app_write_REG_MOTOR2_IMMEDIATE_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR2_IMMEDIATE_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR3_IMMEDIATE_STEPS                                           */
/************************************************************************/
void app_read_REG_MOTOR3_IMMEDIATE_STEPS(void)
{
	//app_regs.REG_MOTOR3_IMMEDIATE_STEPS = 0;

}

bool app_write_REG_MOTOR3_IMMEDIATE_STEPS(void *a)
{
	int32_t reg = *((int32_t*)a);

	app_regs.REG_MOTOR3_IMMEDIATE_STEPS = reg;
	return true;
}


/************************************************************************/
/* REG_STOP_MOTORS_SUDENTLY                                             */
/************************************************************************/
void app_read_REG_STOP_MOTORS_SUDENTLY(void)
{
	//app_regs.REG_STOP_MOTORS_SUDENTLY = 0;

}

bool app_write_REG_STOP_MOTORS_SUDENTLY(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_STOP_MOTORS_SUDENTLY = reg;
	return true;
}


/************************************************************************/
/* REG_STOP_MOTORS_BY_DECELERATION                                      */
/************************************************************************/
void app_read_REG_STOP_MOTORS_BY_DECELERATION(void)
{
	//app_regs.REG_STOP_MOTORS_BY_DECELERATION = 0;

}

bool app_write_REG_STOP_MOTORS_BY_DECELERATION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_STOP_MOTORS_BY_DECELERATION = reg;
	return true;
}


/************************************************************************/
/* REG_RESET_MOTORS_ERROR_DETECTION                                     */
/************************************************************************/
void app_read_REG_RESET_MOTORS_ERROR_DETECTION(void)
{
	//app_regs.REG_RESET_MOTORS_ERROR_DETECTION = 0;

}

bool app_write_REG_RESET_MOTORS_ERROR_DETECTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESET_MOTORS_ERROR_DETECTION = reg;
	return true;
}


/************************************************************************/
/* REG_RESET_ENCODERS                                                   */
/************************************************************************/
void app_read_REG_RESET_ENCODERS(void)
{
	//app_regs.REG_RESET_ENCODERS = 0;

}

bool app_write_REG_RESET_ENCODERS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESET_ENCODERS = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void)
{
	//app_regs.REG_RESERVED0 = 0;

}

bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED0 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void)
{
	//app_regs.REG_RESERVED1 = 0;

}

bool app_write_REG_RESERVED1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED1 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void)
{
	//app_regs.REG_RESERVED2 = 0;

}

bool app_write_REG_RESERVED2(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED2 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void)
{
	//app_regs.REG_RESERVED3 = 0;

}

bool app_write_REG_RESERVED3(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED3 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void)
{
	//app_regs.REG_RESERVED4 = 0;

}

bool app_write_REG_RESERVED4(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED4 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void)
{
	//app_regs.REG_RESERVED5 = 0;

}

bool app_write_REG_RESERVED5(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED5 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED6                                                        */
/************************************************************************/
void app_read_REG_RESERVED6(void)
{
	//app_regs.REG_RESERVED6 = 0;

}

bool app_write_REG_RESERVED6(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED6 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED7                                                        */
/************************************************************************/
void app_read_REG_RESERVED7(void)
{
	//app_regs.REG_RESERVED7 = 0;

}

bool app_write_REG_RESERVED7(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED7 = reg;
	return true;
}