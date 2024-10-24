#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

#include "stepper_control.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)


uint8_t inputs_previous_read = 0;

/************************************************************************/ 
/* INPUT0                                                               */
/************************************************************************/
ISR(PORTK_INT0_vect, ISR_NAKED)
{
	uint8_t inputs_current_read = inputs_previous_read;
	
	uint8_t motor_stopped_mask = 0;
	
	if (read_INPUT0)
	{
		inputs_current_read &= ~B_INPUT0;
		
		if (app_regs.REG_INPUT0_OPERATION_MODE & 0x20)	// Means it's configured to stop when falling
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT0_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT0_OPERATION_MODE & 0x0F) : 0;
		}
	}
	else
	{
		inputs_current_read |= B_INPUT0;
		
		if (app_regs.REG_INPUT0_OPERATION_MODE & 0x10)	// Means it's configured to stop when rising
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT0_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT0_OPERATION_MODE & 0x0F) : 0;
		}
	}
	
	if (inputs_current_read != inputs_previous_read)
	{
		app_regs.REG_DIGITAL_INPUTS_STATE = ((inputs_previous_read ^ inputs_current_read) << 4) | (inputs_current_read & 0x0F);
		core_func_send_event(ADD_REG_DIGITAL_INPUTS_STATE, true);
		
		inputs_previous_read = inputs_current_read;
	}
	
	if (motor_stopped_mask)
	{
		send_motors_stopped_event(motor_stopped_mask);
	}
	
	reti();
}

/************************************************************************/ 
/* INPUT1                                                               */
/************************************************************************/
ISR(PORTQ_INT0_vect, ISR_NAKED)
{
	uint8_t inputs_current_read = inputs_previous_read;
	
	uint8_t motor_stopped_mask = 0;
	
	if (read_INPUT1)
	{
		inputs_current_read &= ~B_INPUT1;
		
		if (app_regs.REG_INPUT1_OPERATION_MODE & 0x20)	// Means it's configured to stop when falling
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT1_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT1_OPERATION_MODE & 0x0F) : 0;
		}
	}
	else
	{
		inputs_current_read |= B_INPUT1;
		
		if (app_regs.REG_INPUT1_OPERATION_MODE & 0x10)	// Means it's configured to stop when rising
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT1_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT1_OPERATION_MODE & 0x0F) : 0;
		}
	}
	
	if (inputs_current_read != inputs_previous_read)
	{
		app_regs.REG_DIGITAL_INPUTS_STATE = ((inputs_previous_read ^ inputs_current_read) << 4) | (inputs_current_read & 0x0F);
		core_func_send_event(ADD_REG_DIGITAL_INPUTS_STATE, true);
		
		inputs_previous_read = inputs_current_read;
	}
	
	if (motor_stopped_mask)
	{
		send_motors_stopped_event(motor_stopped_mask);
	}
	
	reti();
}

/************************************************************************/ 
/* INPUT2                                                               */
/************************************************************************/
ISR(PORTC_INT0_vect, ISR_NAKED)
{
	uint8_t inputs_current_read = inputs_previous_read;
	
	uint8_t motor_stopped_mask = 0;
	
	if (read_INPUT2)
	{
		inputs_current_read &= ~B_INPUT2;
		
		if (app_regs.REG_INPUT2_OPERATION_MODE & 0x20)	// Means it's configured to stop when falling
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT2_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT2_OPERATION_MODE & 0x0F) : 0;
		}
	}
	else
	{
		inputs_current_read |= B_INPUT2;
		
		if (app_regs.REG_INPUT2_OPERATION_MODE & 0x10)	// Means it's configured to stop when rising
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT2_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT2_OPERATION_MODE & 0x0F) : 0;
		}
	}
	
	if (inputs_current_read != inputs_previous_read)
	{
		app_regs.REG_DIGITAL_INPUTS_STATE = ((inputs_previous_read ^ inputs_current_read) << 4) | (inputs_current_read & 0x0F);
		core_func_send_event(ADD_REG_DIGITAL_INPUTS_STATE, true);
		
		inputs_previous_read = inputs_current_read;
	}
	
	if (motor_stopped_mask)
	{
		send_motors_stopped_event(motor_stopped_mask);
	}
	
	reti();
}

/************************************************************************/ 
/* INPUT3                                                               */
/************************************************************************/
ISR(PORTH_INT0_vect, ISR_NAKED)
{
	uint8_t inputs_current_read = inputs_previous_read;
	
	uint8_t motor_stopped_mask = 0;
	
	if (read_INPUT3)
	{
		inputs_current_read &= ~B_INPUT3;
		
		if (app_regs.REG_INPUT3_OPERATION_MODE & 0x20)	// Means it's configured to stop when falling
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT3_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT3_OPERATION_MODE & 0x0F) : 0;
		}
	}
	else
	{
		inputs_current_read |= B_INPUT3;
		
		if (app_regs.REG_INPUT3_OPERATION_MODE & 0x10)	// Means it's configured to stop when rising
		{
			motor_stopped_mask = (if_moving_stop_rotation(app_regs.REG_INPUT3_OPERATION_MODE & 0x0F)) ? (app_regs.REG_INPUT3_OPERATION_MODE & 0x0F) : 0;
		}
	}
	
	if (inputs_current_read != inputs_previous_read)
	{
		app_regs.REG_DIGITAL_INPUTS_STATE = ((inputs_previous_read ^ inputs_current_read) << 4) | (inputs_current_read & 0x0F);
		core_func_send_event(ADD_REG_DIGITAL_INPUTS_STATE, true);
		
		inputs_previous_read = inputs_current_read;
	}
	
	if (motor_stopped_mask)
	{
		send_motors_stopped_event(motor_stopped_mask);
	}
	
	reti();
}

/************************************************************************/ 
/* ERROR_M0                                                             */
/************************************************************************/
ISR(PORTC_INT1_vect, ISR_NAKED)
{
	io_pin2in(&PORTC, 2, PULL_IO_UP, SENSE_IO_NO_INT_USED);
	io_set_int(&PORTC, INT_LEVEL_OFF, 1, (1<<2), false);
	
	uint8_t prev_reg = app_regs.REG_MOTORS_ERROR_DETECTTION;	
	
	clr_DRIVE_ENABLE_M0;
	if (if_moving_stop_rotation(0))
	{
		send_motors_stopped_event(0);
	}
	clr_LED_M0;
		
	app_regs.REG_MOTORS_ERROR_DETECTTION |= B_MOTOR0;
		
	if (prev_reg != app_regs.REG_MOTORS_ERROR_DETECTTION)
	{
		core_func_send_event(ADD_REG_MOTORS_ERROR_DETECTTION, true);
	}
	
	reti();
}

/************************************************************************/ 
/* ERROR_M1                                                             */
/************************************************************************/
ISR(PORTD_INT1_vect, ISR_NAKED)
{
	io_pin2in(&PORTD, 2, PULL_IO_UP, SENSE_IO_NO_INT_USED);
	io_set_int(&PORTD, INT_LEVEL_OFF, 1, (1<<2), false);
	
	uint8_t prev_reg = app_regs.REG_MOTORS_ERROR_DETECTTION;
	
	clr_DRIVE_ENABLE_M1;
	if (if_moving_stop_rotation(1))
	{
		send_motors_stopped_event(1);
	}
	clr_LED_M1;
		
	app_regs.REG_MOTORS_ERROR_DETECTTION |= B_MOTOR1;
		
	if (prev_reg != app_regs.REG_MOTORS_ERROR_DETECTTION)
	{
		core_func_send_event(ADD_REG_MOTORS_ERROR_DETECTTION, true);
	}
	
	reti();
}

/************************************************************************/ 
/* ERROR_M2                                                             */
/************************************************************************/
ISR(PORTE_INT1_vect, ISR_NAKED)
{
	io_pin2in(&PORTE, 2, PULL_IO_UP, SENSE_IO_NO_INT_USED);
	io_set_int(&PORTE, INT_LEVEL_OFF, 1, (1<<2), false);
	
	uint8_t prev_reg = app_regs.REG_MOTORS_ERROR_DETECTTION;
	
	clr_DRIVE_ENABLE_M2;
	if (if_moving_stop_rotation(2))
	{
		send_motors_stopped_event(2);
	}
	clr_LED_M2;
	
	app_regs.REG_MOTORS_ERROR_DETECTTION |= B_MOTOR2;
	
	if (prev_reg != app_regs.REG_MOTORS_ERROR_DETECTTION)
	{
		core_func_send_event(ADD_REG_MOTORS_ERROR_DETECTTION, true);
	}
	
	reti();
}

/************************************************************************/ 
/* ERROR_M3                                                             */
/************************************************************************/
ISR(PORTJ_INT1_vect, ISR_NAKED)
{
	io_pin2in(&PORTJ, 5, PULL_IO_UP, SENSE_IO_NO_INT_USED);
	io_set_int(&PORTJ, INT_LEVEL_OFF, 1, (1<<5), false);
	
	uint8_t prev_reg = app_regs.REG_MOTORS_ERROR_DETECTTION;
		
	clr_DRIVE_ENABLE_M3;
	if (if_moving_stop_rotation(3))
	{
		send_motors_stopped_event(3);
	}
	clr_LED_M3;
		
	app_regs.REG_MOTORS_ERROR_DETECTTION |= B_MOTOR3;		
		
	if (prev_reg != app_regs.REG_MOTORS_ERROR_DETECTTION)
	{
		core_func_send_event(ADD_REG_MOTORS_ERROR_DETECTTION, true);
	}
	
	reti();
}

/************************************************************************/ 
/* EMERGENCY                                                            */
/************************************************************************/
extern uint8_t motors_enabled_mask;

uint8_t enable_counter = 1;

void enable_motors (void)
{
	if (motors_enabled_mask & B_MOTOR0) set_DRIVE_ENABLE_M0;
	if (motors_enabled_mask & B_MOTOR1) set_DRIVE_ENABLE_M1;
	if (motors_enabled_mask & B_MOTOR2) set_DRIVE_ENABLE_M2;
	if (motors_enabled_mask & B_MOTOR3) set_DRIVE_ENABLE_M3;
	
	app_regs.REG_EMERGENCY_DETECTION = GM_ENABLED;
	core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
}

ISR(PORTQ_INT1_vect, ISR_NAKED)
{
	if (read_EMERGENCY)	
	{
		/* Connector is open */
		
		if (app_regs.REG_EMERGENCY_DETECTION_MODE == GM_OPEN)
		{
			/* Enable the previous enabled motors */
		}
		else
		{
			if (enable_counter == 0)
			{
				uint8_t motor_stopped_mask = 0;
				
				enable_counter = 1;
				
				motor_stopped_mask |= (if_moving_stop_rotation(0)) ? B_MOTOR0 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(1)) ? B_MOTOR1 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(2)) ? B_MOTOR2 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(3)) ? B_MOTOR3 : 0;
				
				clr_DRIVE_ENABLE_M0; clr_LED_M0;
				clr_DRIVE_ENABLE_M1; clr_LED_M1;
				clr_DRIVE_ENABLE_M2; clr_LED_M2;
				clr_DRIVE_ENABLE_M3; clr_LED_M3;
				
				app_regs.REG_EMERGENCY_DETECTION = GM_DISABLED;
				core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
				
				if (motor_stopped_mask)
				{
					send_motors_stopped_event(motor_stopped_mask);
				}
			}
		}
	}
	else	
	{		
		/* Connector is close */
		
		if (app_regs.REG_EMERGENCY_DETECTION_MODE == GM_CLOSED)
		{
			/* Enable the previous enabled motors */
		}
		else
		{
			if (enable_counter == 0)
			{
				uint8_t motor_stopped_mask = 0;
				
				enable_counter = 1;
				
				motor_stopped_mask |= (if_moving_stop_rotation(0)) ? B_MOTOR0 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(1)) ? B_MOTOR1 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(2)) ? B_MOTOR2 : 0;
				motor_stopped_mask |= (if_moving_stop_rotation(3)) ? B_MOTOR3 : 0;
				
				clr_DRIVE_ENABLE_M0; clr_LED_M0;
				clr_DRIVE_ENABLE_M1; clr_LED_M1;
				clr_DRIVE_ENABLE_M2; clr_LED_M2;
				clr_DRIVE_ENABLE_M3; clr_LED_M3;
				
				app_regs.REG_EMERGENCY_DETECTION = GM_DISABLED;
				core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
				
				if (motor_stopped_mask)
				{
					send_motors_stopped_event(motor_stopped_mask);
				}
			}
		}
		
	}
	reti();
}

