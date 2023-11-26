#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#include "i2c.h"

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

// https://www.amci.com/industrial-automation-resources/plc-automation-tutorials/stepper-motor-drivers-rms-or-peak-current/

#define MIN_RREF_VALUE 12.0
#define MAX_RREF_VALUE 60.0

#define KIFS_3_AMP_PEAK 36.0
#define KIFS_2_AMP_PEAK 24.0
#define KIFS_1_AMP_PEAK 11.75

#define RREF_HW_OFFSET 12.0

uint8_t calculate_max_current_configuration_data (uint8_t *cfg_2_and_3, float rms)
{
	float KFIS;
	float IFS;
	float RREF;
	
	float digi_pot_value;
	
	/* Calculate current peak */
	IFS = rms * 1.414213562373095 /* sqrt(2) */;
	
	/* Apply boundaries */
	if (IFS < KIFS_1_AMP_PEAK/MAX_RREF_VALUE) IFS = KIFS_1_AMP_PEAK/MAX_RREF_VALUE;
	if (IFS > KIFS_3_AMP_PEAK/MIN_RREF_VALUE) IFS = KIFS_3_AMP_PEAK/MIN_RREF_VALUE;
	
	/* Find KFIS and set CFG2 and CFG3 */
	if (IFS < 0.95)
	{
		KFIS = KIFS_1_AMP_PEAK;
		*cfg_2_and_3 = 0;
	}
	else if (IFS < 1.95)
	{
		KFIS = KIFS_2_AMP_PEAK;
		*cfg_2_and_3 = 1;
	}
	else
	{
		KFIS = KIFS_3_AMP_PEAK;		
		*cfg_2_and_3 = 2;
	}
	
	/* Calculate external resistor RREF value */
	RREF = KFIS / IFS;
	
	/* Apply boundaries */
	if (RREF < MIN_RREF_VALUE) RREF = MIN_RREF_VALUE;
	if (RREF > MAX_RREF_VALUE) RREF = MAX_RREF_VALUE;
	
	/* Calculate digital word to be written to digital potentiometer */
	digi_pot_value = (RREF - RREF_HW_OFFSET) * 5.12 - 1.6384;
	
	/* Apply boundaries */
	if (digi_pot_value < 0)   digi_pot_value = 0;
	if (digi_pot_value > 255) digi_pot_value = 255;
	
	return (uint8_t) digi_pot_value;
}

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
	
	if (reg & B_MOTOR0) set_DRIVE_ENABLE_M0;

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
	
	if (reg & B_MOTOR0) clr_DRIVE_ENABLE_M0;

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
void app_read_REG_MOTOR0_OPERATION_MODE(void) {}
bool app_write_REG_MOTOR0_OPERATION_MODE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg == GM_QUIET_MODE) set_CFG5_M0;
	else if (reg == GM_DYNAMIC_MOVEMENTS) clr_CFG5_M0;
	else return false;

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
void app_read_REG_MOTOR0_MICROSTEP_RESOLUTION(void) {}
bool app_write_REG_MOTOR0_MICROSTEP_RESOLUTION(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg == GM_MICROSTEPS_8) {clr_CFG0_M0; clr_CFG1_M0;}
	else if (reg == GM_MICROSTEPS_16) {set_CFG0_M0; clr_CFG1_M0;}
	else if (reg == GM_MICROSTEPS_32) {clr_CFG0_M0; set_CFG1_M0;}
	else if (reg == GM_MICROSTEPS_64) {set_CFG0_M0; set_CFG1_M0;}
	else return false;

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
void app_read_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void) {}
bool app_write_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void *a)
{
	float reg = *((float*)a);
	
	uint8_t cfg_2_and_3;
	uint8_t digital_pot;
	
	digital_pot = calculate_max_current_configuration_data (&cfg_2_and_3, reg);
	
	if (cfg_2_and_3 & 1) set_CFG2_M0; else clr_CFG2_M0;
	if (cfg_2_and_3 & 2) set_CFG3_M0; else clr_CFG3_M0;	

	app_write_REG_RESERVED4(&digital_pot);

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
	
	if (reg > -10 && reg < 10)
	{
		reg = 0;
	}
	
	if (reg == 0)
	{
		timer_type0_stop(&TCC0);
	}	
	else if (TCC0_CTRLA == 0)
	{
		if (reg > 0)
			set_DIR_M0;
		else
			clr_DIR_M0;
			
		if (reg < 0)
		{
			reg = -reg;
		}
			
		timer_type0_pwm(&TCC0, TIMER_PRESCALER_DIV64, reg >> 1, 3, INT_LEVEL_OFF, INT_LEVEL_OFF);
	}
	else
	{
		if (TCC0_INTCTRLB |= 0)
		{
			/* If running in normal mode, disable timer interrupts */
			TCC0_INTCTRLA = 0;
			TCC0_INTCTRLB = 0;			
		}
		
		if (reg > 0)
			set_DIR_M0;
		else
			clr_DIR_M0;
			
		if (reg < 0) reg = -reg;
		
		TCC0_PER = (reg >> 1) - 1;
	}

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
	app_regs.REG_RESERVED0 = 0;
	app_regs.REG_RESERVED0 |= (read_CFG0_M0) ? 1 : 0;
	app_regs.REG_RESERVED0 |= (read_CFG1_M0) ? 2 : 0;
	app_regs.REG_RESERVED0 |= (read_CFG2_M0) ? 4 : 0;
	app_regs.REG_RESERVED0 |= (read_CFG3_M0) ? 8 : 0;
	app_regs.REG_RESERVED0 |= 16;
	app_regs.REG_RESERVED0 |= (read_CFG5_M0) ? 32 : 0;
	app_regs.REG_RESERVED0 |= (read_CFG6_M0) ? 64 : 0;
	app_regs.REG_RESERVED0 |= (read_CFG7_M0) ? 128 : 0;
}

bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & 1)   set_CFG0_M0; else clr_CFG0_M0;
	if (reg & 2)   set_CFG1_M0; else set_CFG1_M0;
	if (reg & 4)   set_CFG2_M0; else set_CFG2_M0;
	if (reg & 8)   set_CFG3_M0; else set_CFG3_M0;
	if (reg & 32)  set_CFG5_M0; else set_CFG5_M0;
	if (reg & 64)  set_CFG6_M0; else set_CFG6_M0;
	if (reg & 128) set_CFG7_M0; else set_CFG7_M0;	

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
i2c_dev_t digi_pot_M0_M1;
i2c_dev_t digi_pot_M2_M3;

void app_read_REG_RESERVED4(void) {}
bool app_write_REG_RESERVED4(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	digi_pot_M0_M1.reg = 0x00;		// Pot 1
	digi_pot_M0_M1.reg_val = reg;
	
	bool ok = i2c0_wReg(&digi_pot_M0_M1);

	app_regs.REG_RESERVED4 = reg;
	return ok;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void) {}
bool app_write_REG_RESERVED5(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	digi_pot_M0_M1.reg = 0x80;		// Pot 2
	digi_pot_M0_M1.reg_val = reg;
	
	bool ok = i2c0_wReg(&digi_pot_M0_M1);

	app_regs.REG_RESERVED5 = reg;
	return ok;
}


/************************************************************************/
/* REG_RESERVED6                                                        */
/************************************************************************/
void app_read_REG_RESERVED6(void) {}
bool app_write_REG_RESERVED6(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	digi_pot_M2_M3.reg = 0x00;		// Pot 1
	digi_pot_M2_M3.reg_val = reg;
	
	bool ok = i2c0_wReg(&digi_pot_M2_M3);

	app_regs.REG_RESERVED6 = reg;
	return ok;
}


/************************************************************************/
/* REG_RESERVED7                                                        */
/************************************************************************/
void app_read_REG_RESERVED7(void) {}
bool app_write_REG_RESERVED7(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	digi_pot_M2_M3.reg = 0x80;		// Pot 2
	digi_pot_M2_M3.reg_val = reg;
	
	bool ok = i2c0_wReg(&digi_pot_M2_M3);

	app_regs.REG_RESERVED7 = reg;
	return ok;
}