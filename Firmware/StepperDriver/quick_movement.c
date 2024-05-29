#include "stepper_control.h"
#include "quick_movement.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

extern AppRegs app_regs;

extern bool motor_is_running[MOTORS_QUANTITY];

/************************************************************************/
/* Global variables                                                     */
/************************************************************************/
bool m1_quick_parameters_loaded = false;
bool m2_quick_parameters_loaded = false;

uint16_t m1_acc_number_of_steps;
uint16_t m2_acc_number_of_steps;

uint8_t m1_quick_count_down = 0;
uint8_t m2_quick_count_down = 0;

uint16_t m1_quick_relative_steps;
uint16_t m2_quick_relative_steps;

uint16_t m1_quick_timer_per;
uint16_t m2_quick_timer_per;
uint8_t m1_quick_state_ctrl;
uint8_t m2_quick_state_ctrl;
uint16_t m1_quick_stop_decreasing_interval;
uint16_t m2_quick_stop_decreasing_interval;
uint16_t m1_quick_start_increasing_interval;
uint16_t m2_quick_start_increasing_interval;

uint16_t m1_quick_acc_interval;
uint16_t m2_quick_acc_interval;
uint16_t m1_quick_step_interval;
uint16_t m2_quick_step_interval;


/************************************************************************/
/* Quick movement routines                                              */
/************************************************************************/
bool m1_quick_load_parameters (void)
{
	uint16_t motor1_positive_steps = (uint16_t)((app_regs.REG_MOTOR1_QUICK_STEPS > 0) ? app_regs.REG_MOTOR1_QUICK_STEPS : -app_regs.REG_MOTOR1_QUICK_STEPS);

	m1_quick_parameters_loaded = true;
	
	float m1_acc_number_of_steps_float = (float)(app_regs.REG_MOTOR1_QUICK_MAXIMUM_STEP_INTERVAL - app_regs.REG_MOTOR1_QUICK_NOMINAL_STEP_INTERVAL) / (float)app_regs.REG_MOTOR1_QUICK_STEP_ACCELERATION_INTERVAL;
	
	m1_acc_number_of_steps = m1_acc_number_of_steps_float - ((uint16_t)m1_acc_number_of_steps_float) > 0 ? (uint16_t)m1_acc_number_of_steps_float + 1 : (uint16_t)m1_acc_number_of_steps_float;
	
	m1_quick_timer_per = app_regs.REG_MOTOR1_QUICK_MAXIMUM_STEP_INTERVAL + app_regs.REG_MOTOR1_QUICK_STEP_ACCELERATION_INTERVAL;
	m1_quick_step_interval = app_regs.REG_MOTOR1_QUICK_NOMINAL_STEP_INTERVAL;
	m1_quick_state_ctrl = 0;
	m1_quick_stop_decreasing_interval = abs(app_regs.REG_MOTOR1_QUICK_STEPS) - m1_acc_number_of_steps;
	m1_quick_start_increasing_interval = m1_acc_number_of_steps;
	
	m1_quick_acc_interval = app_regs.REG_MOTOR1_QUICK_STEP_ACCELERATION_INTERVAL;
	
	if (m1_acc_number_of_steps <= 2)
	{
		return false;
	}
	
	if (m1_acc_number_of_steps * 2 < abs(app_regs.REG_MOTOR1_QUICK_STEPS))
	{
		//return false;
	}
	
	if (app_regs.REG_MOTOR1_QUICK_STEP_ACCELERATION_INTERVAL < app_regs.REG_MOTOR1_QUICK_NOMINAL_STEP_INTERVAL)
	{
		//return false;
	}
	
	return true;
}

bool m2_quick_load_parameters (void)
{
	m2_quick_parameters_loaded = true;
	
	float m2_acc_number_of_steps_float = (float)(app_regs.REG_MOTOR2_QUICK_MAXIMUM_STEP_INTERVAL - app_regs.REG_MOTOR2_QUICK_NOMINAL_STEP_INTERVAL) / (float)app_regs.REG_MOTOR2_QUICK_STEP_ACCELERATION_INTERVAL;
	
	m2_acc_number_of_steps = m2_acc_number_of_steps_float - ((uint16_t)m2_acc_number_of_steps_float) > 0 ? (uint16_t)m2_acc_number_of_steps_float + 1 : (uint16_t)m2_acc_number_of_steps_float;
	
	m2_quick_timer_per = app_regs.REG_MOTOR2_QUICK_MAXIMUM_STEP_INTERVAL + app_regs.REG_MOTOR2_QUICK_STEP_ACCELERATION_INTERVAL;
	m2_quick_step_interval = app_regs.REG_MOTOR2_QUICK_NOMINAL_STEP_INTERVAL;
	m2_quick_state_ctrl = 0;
	m2_quick_stop_decreasing_interval = abs(app_regs.REG_MOTOR2_QUICK_STEPS) - m2_acc_number_of_steps;
	m2_quick_start_increasing_interval = m2_acc_number_of_steps;
	
	m2_quick_acc_interval = app_regs.REG_MOTOR2_QUICK_STEP_ACCELERATION_INTERVAL;
	
	if (m2_acc_number_of_steps <= 2)
	{
		return false;
	}
	
	if (m2_acc_number_of_steps * 2 < abs(app_regs.REG_MOTOR2_QUICK_STEPS))
	{
		//return false;
	}
	
	if (app_regs.REG_MOTOR2_QUICK_STEP_ACCELERATION_INTERVAL < app_regs.REG_MOTOR2_QUICK_NOMINAL_STEP_INTERVAL)
	{
		//return false;
	}
	
	return true;
}

bool m1_quick_launch_movement (void)
{	
	if (read_DRIVE_ENABLE_M1)
	{
		return false;
	}
	
	if (m1_quick_parameters_loaded == false)
	{
		return false;
	}
	
	m1_quick_timer_per = app_regs.REG_MOTOR1_QUICK_MAXIMUM_STEP_INTERVAL + app_regs.REG_MOTOR1_QUICK_STEP_ACCELERATION_INTERVAL;
	m1_quick_state_ctrl = 0;
	
	/* Only executes the movement if the motor is stopped */
	if_moving_stop_rotation(1);	
	
	if (app_regs.REG_MOTOR1_QUICK_STEPS > 0)
	{
		set_DIR_M1;
	}
	else
	{
		clr_DIR_M1;
	}
	
	// 4 Stop motor if moving
	// 3 Wait 1 ms
	// 2 Star m1_start_quick_movement()
	// 1 Moving
	// 0 Stopped
	m1_quick_count_down = 4;
	
	m1_quick_relative_steps = abs(app_regs.REG_MOTOR1_QUICK_STEPS);
	
	return true;
}

bool m2_quick_launch_movement (void)
{
	if (read_DRIVE_ENABLE_M2)
	{
		return false;
	}
	
	if (m2_quick_parameters_loaded == false)
	{
		return false;
	}
	
	m2_quick_timer_per = app_regs.REG_MOTOR2_QUICK_MAXIMUM_STEP_INTERVAL + app_regs.REG_MOTOR2_QUICK_STEP_ACCELERATION_INTERVAL;
	m2_quick_state_ctrl = 0;
	
	/* Only executes the movement if the motor is stopped */
	if_moving_stop_rotation(2);
	
	if (app_regs.REG_MOTOR2_QUICK_STEPS > 0)
	{
		set_DIR_M2;
	}
	else
	{
		clr_DIR_M2;
	}
	
	// 4 Stop motor if moving
	// 3 Wait 1 ms
	// 2 Star m1_start_quick_movement()
	// 1 Moving
	// 0 Stopped
	m2_quick_count_down = 4;
	
	m2_quick_relative_steps = abs(app_regs.REG_MOTOR2_QUICK_STEPS);
	
	return true;
}

void m1_start_quick_movement (void)
{	
	if (read_DRIVE_ENABLE_M1)
	{
		return;
	}
	
	/* Start the generation of pulses */
	timer_type0_pwm(&TCD0, TIMER_PRESCALER_DIV64, (app_regs.REG_MOTOR1_QUICK_MAXIMUM_STEP_INTERVAL - 1) >> 1, 2 >> 1, INT_LEVEL_MED, INT_LEVEL_MED);
	
	motor_is_running[1] = true;
	
	
	if (core_bool_is_visual_enabled())
	{
		set_LED_M1;
	}
}

void m2_start_quick_movement (void)
{	
	if (read_DRIVE_ENABLE_M2)
	{
		return;
	}
	
	/* Start the generation of pulses */
	timer_type0_pwm(&TCE0, TIMER_PRESCALER_DIV64, (app_regs.REG_MOTOR2_QUICK_MAXIMUM_STEP_INTERVAL - 1) >> 1, 2 >> 1, INT_LEVEL_MED, INT_LEVEL_MED);
	
	motor_is_running[2] = true;
	
	
	if (core_bool_is_visual_enabled())
	{
		set_LED_M2;
	}
}