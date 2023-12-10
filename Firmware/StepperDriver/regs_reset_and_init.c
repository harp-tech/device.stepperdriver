#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

extern AppRegs app_regs;

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	app_regs.REG_ENABLE_MOTORS = 0;
	app_regs.REG_ENABLE_INPUTS = 0;
	app_regs.REG_ENABLE_ENCODERS = 0;
	
	
	app_regs.REG_MOTOR0_OPERATION_MODE = GM_QUIET_MODE;
	app_regs.REG_MOTOR0_MICROSTEP_RESOLUTION = GM_MICROSTEPS_8;
	app_regs.REG_MOTOR0_MAXIMUM_CURRENT_RMS = 0.2;
	app_regs.REG_MOTOR0_HOLD_CURRENT_REDUCTION = GM_REDUCTION_TO_50PCT;
	app_regs.REG_MOTOR0_NOMINAL_STEP_INTERVAL = 250;
	app_regs.REG_MOTOR0_MAXIMUM_STEP_INTERVAL = 2000;
	app_regs.REG_MOTOR0_STEP_ACCELERATION_INTERVAL = 10;
	
	app_regs.REG_MOTOR1_OPERATION_MODE = GM_QUIET_MODE;
	app_regs.REG_MOTOR1_MICROSTEP_RESOLUTION = GM_MICROSTEPS_8;
	app_regs.REG_MOTOR1_MAXIMUM_CURRENT_RMS = 0.2;
	app_regs.REG_MOTOR1_HOLD_CURRENT_REDUCTION = GM_REDUCTION_TO_50PCT;
	app_regs.REG_MOTOR1_NOMINAL_STEP_INTERVAL = 250;
	app_regs.REG_MOTOR1_MAXIMUM_STEP_INTERVAL = 2000;
	app_regs.REG_MOTOR1_STEP_ACCELERATION_INTERVAL = 10;
	
	app_regs.REG_MOTOR2_OPERATION_MODE = GM_QUIET_MODE;
	app_regs.REG_MOTOR2_MICROSTEP_RESOLUTION = GM_MICROSTEPS_8;
	app_regs.REG_MOTOR2_MAXIMUM_CURRENT_RMS = 0.2;
	app_regs.REG_MOTOR2_HOLD_CURRENT_REDUCTION = GM_REDUCTION_TO_50PCT;
	app_regs.REG_MOTOR2_NOMINAL_STEP_INTERVAL = 250;
	app_regs.REG_MOTOR2_MAXIMUM_STEP_INTERVAL = 2000;
	app_regs.REG_MOTOR2_STEP_ACCELERATION_INTERVAL = 10;
	
	app_regs.REG_MOTOR3_OPERATION_MODE = GM_QUIET_MODE;
	app_regs.REG_MOTOR3_MICROSTEP_RESOLUTION = GM_MICROSTEPS_8;
	app_regs.REG_MOTOR3_MAXIMUM_CURRENT_RMS = 0.2;
	app_regs.REG_MOTOR3_HOLD_CURRENT_REDUCTION = GM_REDUCTION_TO_50PCT;
	app_regs.REG_MOTOR3_NOMINAL_STEP_INTERVAL = 250;
	app_regs.REG_MOTOR3_MAXIMUM_STEP_INTERVAL = 2000;
	app_regs.REG_MOTOR3_STEP_ACCELERATION_INTERVAL = 10;
	

	app_regs.REG_INPUT0_OPERATION_MODE = GM_EVENT_AND_STOP_MOTOR0;
	app_regs.REG_INPUT0_SENSE_MODE = GM_FALLING_EDGE;
	
	app_regs.REG_INPUT1_OPERATION_MODE = GM_EVENT_AND_STOP_MOTOR1;
	app_regs.REG_INPUT1_SENSE_MODE = GM_FALLING_EDGE;
	
	app_regs.REG_INPUT2_OPERATION_MODE = GM_EVENT_AND_STOP_MOTOR2;
	app_regs.REG_INPUT2_SENSE_MODE = GM_FALLING_EDGE;
	
	app_regs.REG_INPUT3_OPERATION_MODE = GM_EVENT_AND_STOP_MOTOR3;
	app_regs.REG_INPUT3_SENSE_MODE = GM_FALLING_EDGE;
	
	app_regs.REG_MOTORS_ERROR_DETECTTION = 0;
		
	app_regs.REG_MOTOR0_ACCUMULATED_STEPS = 0;
	app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION = 0;
	app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION = 0;
	
	app_regs.REG_MOTOR1_ACCUMULATED_STEPS = 0;
	app_regs.REG_MOTOR1_MAX_STEPS_INTEGRATION = 0;
	app_regs.REG_MOTOR1_MIN_STEPS_INTEGRATION = 0;
	
	app_regs.REG_MOTOR2_ACCUMULATED_STEPS = 0;
	app_regs.REG_MOTOR2_MAX_STEPS_INTEGRATION = 0;
	app_regs.REG_MOTOR2_MIN_STEPS_INTEGRATION = 0;
	
	app_regs.REG_MOTOR3_ACCUMULATED_STEPS = 0;
	app_regs.REG_MOTOR3_MAX_STEPS_INTEGRATION = 0;
	app_regs.REG_MOTOR3_MIN_STEPS_INTEGRATION = 0;
}

void core_callback_registers_were_reinitialized(void)
{
	/* Update registers if needed */
	
	app_write_REG_ENABLE_MOTORS(&app_regs.REG_ENABLE_MOTORS);	// Motors are disabled by io default
	app_write_REG_ENABLE_INPUTS(&app_regs.REG_ENABLE_INPUTS);
	app_write_REG_ENABLE_ENCODERS(&app_regs.REG_ENABLE_ENCODERS);
	
	app_write_REG_MOTOR0_OPERATION_MODE(&app_regs.REG_MOTOR0_OPERATION_MODE);
	app_write_REG_MOTOR0_MICROSTEP_RESOLUTION(&app_regs.REG_MOTOR0_MICROSTEP_RESOLUTION);
	app_write_REG_MOTOR0_MAXIMUM_CURRENT_RMS(&app_regs.REG_MOTOR0_MAXIMUM_CURRENT_RMS);
	app_write_REG_MOTOR0_HOLD_CURRENT_REDUCTION(&app_regs.REG_MOTOR0_HOLD_CURRENT_REDUCTION);
	app_write_REG_MOTOR0_NOMINAL_STEP_INTERVAL(&app_regs.REG_MOTOR0_NOMINAL_STEP_INTERVAL);
	app_write_REG_MOTOR0_MAXIMUM_STEP_INTERVAL(&app_regs.REG_MOTOR0_MAXIMUM_STEP_INTERVAL);
	app_write_REG_MOTOR0_STEP_ACCELERATION_INTERVAL(&app_regs.REG_MOTOR0_STEP_ACCELERATION_INTERVAL);
	
	app_write_REG_MOTOR1_OPERATION_MODE(&app_regs.REG_MOTOR1_OPERATION_MODE);
	app_write_REG_MOTOR1_MICROSTEP_RESOLUTION(&app_regs.REG_MOTOR1_MICROSTEP_RESOLUTION);
	app_write_REG_MOTOR1_MAXIMUM_CURRENT_RMS(&app_regs.REG_MOTOR1_MAXIMUM_CURRENT_RMS);
	app_write_REG_MOTOR1_HOLD_CURRENT_REDUCTION(&app_regs.REG_MOTOR1_HOLD_CURRENT_REDUCTION);
	app_write_REG_MOTOR1_NOMINAL_STEP_INTERVAL(&app_regs.REG_MOTOR1_NOMINAL_STEP_INTERVAL);
	app_write_REG_MOTOR1_MAXIMUM_STEP_INTERVAL(&app_regs.REG_MOTOR1_MAXIMUM_STEP_INTERVAL);
	app_write_REG_MOTOR1_STEP_ACCELERATION_INTERVAL(&app_regs.REG_MOTOR1_STEP_ACCELERATION_INTERVAL);
	
	app_write_REG_MOTOR2_OPERATION_MODE(&app_regs.REG_MOTOR2_OPERATION_MODE);
	app_write_REG_MOTOR2_MICROSTEP_RESOLUTION(&app_regs.REG_MOTOR2_MICROSTEP_RESOLUTION);
	app_write_REG_MOTOR2_MAXIMUM_CURRENT_RMS(&app_regs.REG_MOTOR2_MAXIMUM_CURRENT_RMS);
	app_write_REG_MOTOR2_HOLD_CURRENT_REDUCTION(&app_regs.REG_MOTOR2_HOLD_CURRENT_REDUCTION);
	app_write_REG_MOTOR2_NOMINAL_STEP_INTERVAL(&app_regs.REG_MOTOR2_NOMINAL_STEP_INTERVAL);
	app_write_REG_MOTOR2_MAXIMUM_STEP_INTERVAL(&app_regs.REG_MOTOR2_MAXIMUM_STEP_INTERVAL);
	app_write_REG_MOTOR2_STEP_ACCELERATION_INTERVAL(&app_regs.REG_MOTOR2_STEP_ACCELERATION_INTERVAL);
	
	app_write_REG_MOTOR3_OPERATION_MODE(&app_regs.REG_MOTOR3_OPERATION_MODE);
	app_write_REG_MOTOR3_MICROSTEP_RESOLUTION(&app_regs.REG_MOTOR3_MICROSTEP_RESOLUTION);
	app_write_REG_MOTOR3_MAXIMUM_CURRENT_RMS(&app_regs.REG_MOTOR3_MAXIMUM_CURRENT_RMS);
	app_write_REG_MOTOR3_HOLD_CURRENT_REDUCTION(&app_regs.REG_MOTOR3_HOLD_CURRENT_REDUCTION);
	app_write_REG_MOTOR3_NOMINAL_STEP_INTERVAL(&app_regs.REG_MOTOR3_NOMINAL_STEP_INTERVAL);
	app_write_REG_MOTOR3_MAXIMUM_STEP_INTERVAL(&app_regs.REG_MOTOR3_MAXIMUM_STEP_INTERVAL);
	app_write_REG_MOTOR3_STEP_ACCELERATION_INTERVAL(&app_regs.REG_MOTOR3_STEP_ACCELERATION_INTERVAL);
	
	
	app_write_REG_INPUT0_OPERATION_MODE(&app_regs.REG_INPUT0_OPERATION_MODE);
	app_write_REG_INPUT0_SENSE_MODE(&app_regs.REG_INPUT0_SENSE_MODE);
	
	app_write_REG_INPUT0_OPERATION_MODE(&app_regs.REG_INPUT0_OPERATION_MODE);
	app_write_REG_INPUT0_SENSE_MODE(&app_regs.REG_INPUT0_SENSE_MODE);
	
	app_write_REG_INPUT0_OPERATION_MODE(&app_regs.REG_INPUT0_OPERATION_MODE);
	app_write_REG_INPUT0_SENSE_MODE(&app_regs.REG_INPUT0_SENSE_MODE);
	
	app_write_REG_INPUT0_OPERATION_MODE(&app_regs.REG_INPUT0_OPERATION_MODE);
	app_write_REG_INPUT0_SENSE_MODE(&app_regs.REG_INPUT0_SENSE_MODE);
	
	app_read_REG_MOTORS_ERROR_DETECTTION();
}