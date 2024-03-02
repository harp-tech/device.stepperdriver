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


/************************************************************************/ 
/* General function for input handling                                  */
/************************************************************************/
void stop_motor_and_send_events (uint8_t operation_mode, uint8_t input_bit_maks)
{
	uint8_t motor_stopped_mask = 0;
	
	switch (operation_mode)
	{
		case GM_EVENT_AND_STOP_MOTOR0:
			motor_stopped_mask = (if_moving_stop_rotation(0)) ? B_MOTOR0 : 0;
			break;
		case GM_EVENT_AND_STOP_MOTOR1:
			motor_stopped_mask = (if_moving_stop_rotation(1)) ? B_MOTOR1 : 1;
			break;
		case GM_EVENT_AND_STOP_MOTOR2:
			motor_stopped_mask = (if_moving_stop_rotation(2)) ? B_MOTOR2 : 2;
			break;
		case GM_EVENT_AND_STOP_MOTOR3:
			motor_stopped_mask = (if_moving_stop_rotation(3)) ? B_MOTOR3 : 3;
			break;
	}
	
	app_regs.REG_DIGITAL_INPUTS_STATE = input_bit_maks;
	core_func_send_event(ADD_REG_DIGITAL_INPUTS_STATE, true);
	
	if (motor_stopped_mask)	
	{
		send_motors_stopped_event(motor_stopped_mask);
	}
}


/************************************************************************/ 
/* INPUT0                                                               */
/************************************************************************/
ISR(PORTK_INT0_vect, ISR_NAKED)
{
	stop_motor_and_send_events(app_regs.REG_INPUT0_OPERATION_MODE, B_INPUT0);
	
	reti();
}

/************************************************************************/ 
/* INPUT1                                                               */
/************************************************************************/
ISR(PORTQ_INT0_vect, ISR_NAKED)
{
	stop_motor_and_send_events(app_regs.REG_INPUT1_OPERATION_MODE, B_INPUT1);
	
	reti();
}

/************************************************************************/ 
/* INPUT2                                                               */
/************************************************************************/
ISR(PORTC_INT0_vect, ISR_NAKED)
{
	stop_motor_and_send_events(app_regs.REG_INPUT2_OPERATION_MODE, B_INPUT2);
	
	reti();
}

/************************************************************************/ 
/* INPUT3                                                               */
/************************************************************************/
ISR(PORTH_INT0_vect, ISR_NAKED)
{
	stop_motor_and_send_events(app_regs.REG_INPUT3_OPERATION_MODE, B_INPUT3);
	
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
	if_moving_stop_rotation(0);
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
	if_moving_stop_rotation(1);
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
	if_moving_stop_rotation(2);
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
	if_moving_stop_rotation(3);
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
				enable_counter = 1;
				
				clr_DRIVE_ENABLE_M0; stop_rotation(0); clr_LED_M0;
				clr_DRIVE_ENABLE_M1; stop_rotation(1);; clr_LED_M1;
				clr_DRIVE_ENABLE_M2; stop_rotation(2);; clr_LED_M2;
				clr_DRIVE_ENABLE_M3; stop_rotation(3);; clr_LED_M3;
				
				app_regs.REG_EMERGENCY_DETECTION = GM_DISABLED;
				core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
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
				enable_counter = 1;
				
				clr_DRIVE_ENABLE_M0; stop_rotation(0); clr_LED_M0;
				clr_DRIVE_ENABLE_M1; stop_rotation(1); clr_LED_M1;
				clr_DRIVE_ENABLE_M2; stop_rotation(2); clr_LED_M2;
				clr_DRIVE_ENABLE_M3; stop_rotation(3); clr_LED_M3;
				
				app_regs.REG_EMERGENCY_DETECTION = GM_DISABLED;
				core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
			}
		}
		
	}
	reti();
}

