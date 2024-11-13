#include "stepper_control.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#include <stdlib.h>


/************************************************************************/
/* User mandatory definitions                                           */
/************************************************************************/
// Define direction port and pin
PORT_t* motor_peripherals_dir_port[MOTORS_QUANTITY] = {&PORTC, &PORTD, &PORTE, &PORTF};
const uint8_t motor_peripherals_dir_pin_index[MOTORS_QUANTITY] = {1, 1, 1, 1};

// Define timer used (only timer type 0 are accepted)
TC0_t* motor_peripherals_timer[MOTORS_QUANTITY] = {&TCC0, &TCD0, &TCE0, &TCF0};

// Define direction port and pin
PORT_t* motor_peripherals_led_port[MOTORS_QUANTITY] = {&PORTH, &PORTH, &PORTJ, &PORTQ};
const uint8_t motor_peripherals_led_pin_index[MOTORS_QUANTITY] = {3, 4, 0, 1};

/************************************************************************/
/* Global electrical pulse parameters                                   */
/************************************************************************/
extern AppRegs app_regs;

uint16_t m_pulse_period_us[MOTORS_QUANTITY];
uint16_t m_min_pulse_interval_us[MOTORS_QUANTITY];
uint16_t m_max_pulse_interval_us[MOTORS_QUANTITY];
uint16_t m_pulse_step_interval_us[MOTORS_QUANTITY];

int16_t ramp_steps[MOTORS_QUANTITY];

/************************************************************************/
/* Global motion parameters                                             */
/************************************************************************/
uint16_t motor_target[MOTORS_QUANTITY];
//uint16_t remaining[MOTORS_QUANTITY];

uint32_t steps_target[MOTORS_QUANTITY];
uint32_t steps_count[MOTORS_QUANTITY];
uint32_t steps_remaining[MOTORS_QUANTITY];

bool motor_is_running[MOTORS_QUANTITY];
bool moving_positive[MOTORS_QUANTITY];
bool decreasing_speed[MOTORS_QUANTITY];

int32_t user_requested_steps[MOTORS_QUANTITY];

bool send_motor_stopped_notification[MOTORS_QUANTITY];

/************************************************************************/
/* Quick movement globals                                               */
/************************************************************************/
extern uint8_t m1_quick_count_down;
extern uint8_t m2_quick_count_down;

extern uint16_t m1_quick_relative_steps;
extern uint16_t m2_quick_relative_steps;


/************************************************************************/
/* Update global electrical pulse parameters                            */
/************************************************************************/
bool initialize_motors (void)
{
	/* Update defaults for all the necessary variables */
	for (uint8_t i = 0; i < MOTORS_QUANTITY; i++)
	{
		update_nominal_pulse_interval(250, i);	// 250 us  between pulse at nominal speed
		update_initial_pulse_interval(2000, i);	//   2 ms  for the first and last pulse
		update_pulse_step_interval(10, i);		//  10 us  step increment and decrement
		update_pulse_period(20, i);				//  20 us  period of high value in the pulse line
		
		motor_is_running[i] = false;
		user_requested_steps[i] = 0;
		send_motor_stopped_notification[i] = false;
	}
	
	return true;
}

bool update_nominal_pulse_interval (uint16_t time_us, uint8_t motor_index)
{
	if (motor_index >= MOTORS_QUANTITY)
	{
		return false;
	}
	
	m_min_pulse_interval_us[motor_index] = time_us >> 1;
	
	ramp_steps[motor_index] = (m_max_pulse_interval_us[motor_index] - m_min_pulse_interval_us[motor_index]) / m_pulse_step_interval_us[motor_index];
	
	return true;
}

bool update_initial_pulse_interval (uint16_t time_us, uint8_t motor_index)
{
	if (motor_index >= MOTORS_QUANTITY)
	{
		return false;
	}
	
	m_max_pulse_interval_us[motor_index] = time_us >> 1;
	
	ramp_steps[motor_index] = (m_max_pulse_interval_us[motor_index] - m_min_pulse_interval_us[motor_index]) / m_pulse_step_interval_us[motor_index];
	
	return true;
}

bool update_pulse_step_interval (uint16_t time_us, uint8_t motor_index)
{
	if (motor_index >= MOTORS_QUANTITY)
	{
		return false;
	}
	
	m_pulse_step_interval_us[motor_index] = time_us >> 1;
	
	ramp_steps[motor_index] = (m_max_pulse_interval_us[motor_index] - m_min_pulse_interval_us[motor_index]) / m_pulse_step_interval_us[motor_index];
	
	return true;
}

bool update_pulse_period (uint16_t time_us, uint8_t motor_index)
{
	if (motor_index >= MOTORS_QUANTITY)
	{
		return false;
	}
	
	m_pulse_period_us[motor_index] = time_us >> 1;
	
	ramp_steps[motor_index] = (m_max_pulse_interval_us[motor_index] - m_min_pulse_interval_us[motor_index]) / m_pulse_step_interval_us[motor_index];
	
	return true;
}

/************************************************************************/
/* Start & Stop functions                                               */
/************************************************************************/
void start_rotation (int32_t requested_steps, uint8_t motor_index)
{
	if (requested_steps > 0)
	{
		motor_peripherals_dir_port[motor_index]->OUTSET = (1<<motor_peripherals_dir_pin_index[motor_index]);
		moving_positive[motor_index] = true;
		steps_target[motor_index] = (uint32_t)requested_steps;
	}
	else
	{
		motor_peripherals_dir_port[motor_index]->OUTCLR = (1<<motor_peripherals_dir_pin_index[motor_index]);
		moving_positive[motor_index] = false;
		steps_target[motor_index] = (uint32_t)(~requested_steps + 1);
	}
	
	steps_count[motor_index] = 0;		// Reset steps counter
	steps_remaining[motor_index] = 0;	// Reset remaining steps
	
	decreasing_speed[motor_index] = false;	// Reset decreasing speed flag
	motor_is_running[motor_index] = true;	// Update global with motor state
	
	/* Start the generation of pulses */
	timer_type0_pwm(motor_peripherals_timer[motor_index], TIMER_PRESCALER_DIV64, m_max_pulse_interval_us[motor_index], m_pulse_period_us[motor_index], INT_LEVEL_MED, INT_LEVEL_MED);
	
	if (core_bool_is_visual_enabled())
	{
		motor_peripherals_led_port[motor_index]->OUTSET = (1<<motor_peripherals_led_pin_index[motor_index]);
	}
}

void stop_rotation (uint8_t motor_index)
{
 	timer_type0_stop(motor_peripherals_timer[motor_index]);
 	motor_is_running[motor_index] = false;
	 
 	if (motor_index == 1) m1_quick_count_down = 0;
 	if (motor_index == 2) m2_quick_count_down = 0;
	
	motor_peripherals_led_port[motor_index]->OUTCLR = (1<<motor_peripherals_led_pin_index[motor_index]);
}

void reduce_until_stop_rotation (uint8_t motor_index)
{
 	/* 
	   To do: Implement a calculation to update the steps
	   in a way that will decrease steps until stop.
	   Meanwhile, the stop_rotation() is here to stop the motor.
	*/
	 stop_rotation(motor_index);
}

bool if_moving_stop_rotation (uint8_t motor_index)
{
	if (motor_peripherals_timer[motor_index]->CTRLA != 0)
	{
		stop_rotation(motor_index);
		return true;
	}
	else
	{
		return false;
	}
}

bool is_timer_ready (uint8_t motor_index)
{
	if (motor_peripherals_timer[motor_index]->CTRLA == 0)
	{
		return true;
	}
	
	return (motor_peripherals_timer[motor_index]->INTCTRLB == 0) ? false : true;
}

void send_motors_stopped_event (uint8_t motor_stop_bit_mask)
{
	/* Note: This function doesn't turn the motor off, it should be done before calling this function */
	
	app_regs.REG_MOTORS_STOPPED = (motor_stop_bit_mask << 4);
	
	if (motor_peripherals_timer[0]->CTRLA == 0) app_regs.REG_MOTORS_STOPPED |= B_MOTOR0;
	if (motor_peripherals_timer[1]->CTRLA == 0) app_regs.REG_MOTORS_STOPPED |= B_MOTOR1;
	if (motor_peripherals_timer[2]->CTRLA == 0) app_regs.REG_MOTORS_STOPPED |= B_MOTOR2;
	if (motor_peripherals_timer[3]->CTRLA == 0) app_regs.REG_MOTORS_STOPPED |= B_MOTOR3;
	
	core_func_send_event(ADD_REG_MOTORS_STOPPED, true);
};


/************************************************************************/
/* Update motion                                                        */
/************************************************************************/
int32_t user_sent_request (int32_t requested_steps, uint8_t motor_index)
{
	if (!motor_is_running[motor_index])
	{
		start_rotation(requested_steps, motor_index);
		return 0;
	}
	else
	{
		if ((requested_steps > 0) && (moving_positive[motor_index] == true))
		{
			steps_target[motor_index] += requested_steps;
			return 0;
		}
		
		if ((requested_steps < 0) && (moving_positive[motor_index] == false))
		{
			steps_target[motor_index] += (uint32_t)(~requested_steps + 1);
			return 0;
		}
		
		if ((requested_steps > 0) && (moving_positive[motor_index] == false))
		{
			if (decreasing_speed[motor_index])
			{
				return requested_steps;
			}
			else if (steps_count[motor_index] <= ramp_steps[motor_index])
			{
				return requested_steps;
			}
			else
			{
				uint32_t available_steps_to_decrease = steps_remaining[motor_index] - ramp_steps[motor_index] - 1;
				
				if (requested_steps <= available_steps_to_decrease)
				{
					steps_target[motor_index] -= requested_steps;
					return 0;
				}
				else
				{
					steps_target[motor_index] -= available_steps_to_decrease;
					return requested_steps - available_steps_to_decrease;
				}
			}
		}
		
		if ((requested_steps < 0) && (moving_positive[motor_index] == true))
		{
			if (decreasing_speed[motor_index])
			{
				return requested_steps;
			}
			else if (steps_count[motor_index] <= ramp_steps[motor_index])
			{
				return requested_steps;
			}
			else
			{
				uint32_t available_steps_to_decrease = steps_remaining[motor_index] - ramp_steps[motor_index] - 1;
				
				if ((~requested_steps+1) <= available_steps_to_decrease)
				{
					steps_target[motor_index] -= (uint32_t)(~requested_steps + 1);
					return 0;
				}
				else
				{
					steps_target[motor_index] -= available_steps_to_decrease;
					return requested_steps + available_steps_to_decrease;
				}
			}
		}
	}
}

/************************************************************************/
/* Manage boundaries                                                    */
/************************************************************************/
void manage_step_boundaries (uint8_t motor_index)
{
	if (motor_peripherals_dir_port[motor_index]->IN & (1<<motor_peripherals_dir_pin_index[motor_index]))
	{
		*(app_regs.REG_ACCUMULATED_STEPS + motor_index) += 1;
		
		if (*((&app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION) + motor_index) != 0)
		{
			if (*(app_regs.REG_ACCUMULATED_STEPS + motor_index) >= *((&app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION) + motor_index))
			{
				stop_rotation(motor_index);
				
				/* Since this is used at MID level interrupts, send an event from here can happen in the middle of other event */
				send_motor_stopped_notification[motor_index] = true;
			}
		}
	}
	else
	{
		*(app_regs.REG_ACCUMULATED_STEPS + motor_index) -= 1;
		
		if (*((&app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION) + motor_index) != 0)
		{
			if (*(app_regs.REG_ACCUMULATED_STEPS + motor_index) <= *((&app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION) + motor_index))
			{
				stop_rotation(motor_index);
				
				/* Since this is used at MID level interrupts, send an event from here can happen in the middle of other event */
				send_motor_stopped_notification[motor_index] = true;
			}
		}
	}
}

/************************************************************************/
/* Interrupts                                                           */
/************************************************************************/
void timer_ovf_routine (uint8_t motor_index)
{	
	if (motor_peripherals_timer[motor_index]->INTCTRLB == 0)
	{
		manage_step_boundaries(motor_index);
		
		return;
	}
	
	manage_step_boundaries(motor_index);
	
	//remaining[0] = motor_target[0] - steps_count[0];
	
	
	steps_count[motor_index]++;
	
	steps_remaining[motor_index] = steps_target[motor_index] - steps_count[motor_index];
	
	if ((steps_remaining[motor_index] <= steps_count[motor_index]) && (steps_remaining[motor_index] <= ramp_steps[motor_index]))
	{
		decreasing_speed[motor_index] = true;
		
		/* Decrease motor speed */
		if (motor_peripherals_timer[motor_index]->PER < m_max_pulse_interval_us[motor_index])
		{
			motor_peripherals_timer[motor_index]->PER = (motor_peripherals_timer[motor_index]->PER + m_pulse_step_interval_us[motor_index] > m_max_pulse_interval_us[motor_index])? m_max_pulse_interval_us[motor_index] : motor_peripherals_timer[motor_index]->PER + m_pulse_step_interval_us[motor_index];
		}
	}
	else
	{
		decreasing_speed[motor_index] = false;
		
		/* Increase motor speed */
		if (motor_peripherals_timer[motor_index]->PER > m_min_pulse_interval_us[motor_index])
		{
			motor_peripherals_timer[motor_index]->PER = (motor_peripherals_timer[motor_index]->PER - m_pulse_step_interval_us[motor_index] < m_min_pulse_interval_us[motor_index])? m_min_pulse_interval_us[motor_index] : motor_peripherals_timer[motor_index]->PER - m_pulse_step_interval_us[motor_index];
		}
	}
}

void timer_cca_routine (uint8_t motor_index)
{
	if (steps_count[motor_index] == steps_target[motor_index])
	{
		/* Stop motor */
		stop_rotation(motor_index);
		
		/* Since this is used at MID level interrupts, send an event from here can happen in the middle of other event */
		send_motor_stopped_notification[motor_index] = true;
	}
}

ISR(TCC0_OVF_vect/*, ISR_NAKED*/)
{
	timer_ovf_routine(0);
}
ISR(TCC0_CCA_vect/*, ISR_NAKED*/)
{
	timer_cca_routine(0);
}

// Old way
extern uint16_t m1_quick_timer_per;
extern uint8_t m1_quick_state_ctrl;
extern uint16_t m1_quick_stop_decreasing_interval;
extern uint16_t m1_quick_start_increasing_interval;

extern uint16_t m1_quick_acc_interval;
extern uint16_t m1_quick_step_interval;

extern uint16_t m2_quick_timer_per;
extern uint8_t m2_quick_state_ctrl;
extern uint16_t m2_quick_stop_decreasing_interval;
extern uint16_t m2_quick_start_increasing_interval;

extern uint16_t m2_quick_acc_interval;
extern uint16_t m2_quick_step_interval;

// New way
extern uint32_t m1_speed_start;
extern uint32_t m1_speed_limit;
extern uint16_t m1_acc;
extern uint16_t m1_move_pulses;

extern uint32_t m1_speed;
extern uint32_t m1_delay;
extern bool m1_accelerating;
extern uint16_t m1_pulses_to_accelerate;
extern uint16_t m1_short_move_pulses;

extern uint32_t m1_timer_limit;
extern uint32_t m2_timer_limit;

extern uint8_t m1_exec_ctrl;

uint32_t m1_speed_temp;
uint32_t m1_delay_temp;
extern bool m1_use_steps;
extern bool m1_use_single_step;

uint16_t m1_single_step_per;

// Do nothing is 7.5 us max
// The minimum period between pulses is given by: (A + A/B*2 + C) / 2 = (A + A/8*2 + C) / 2 = (A + A/4 + C) / 2
//  A = maximum time of the slowest step
//  B = 8 = time in us needed to send or receive a byte
//  C = 4 = margin in us
//  The periods of all the other steps must be bellow the minimum time between pulses minus 4 us

#define STEPS_N 8
#define STEPS_N_SINGLE_STEP 128

#define STEPS_SPEED_CALC_1OF2 7
#define STEPS_SPEED_CALC_2OF2 6
#define STEPS_SPEED_CALC_3OF2 5
#define STEPS_DELAY_CALC 2
#define STEPS_UPDATE_TIMER 0

#define MOVE_TO_STEPS_PERIOD 50 // 500 // 50 // Equal to 30 is too low, it fails.
#define MOVE_TO_SINGLE_STEP 5 // 10 // This solution doesn't seem to work well, keep low value to not be used
/* MOVE_TO_STEPS_PERIOD  MOVE_TO_SINGLE_STEP  Notes
   50                    10                   Can go to 22 us interval, Motor1QuickNominalSpeed = 55
   500                   50                   Can go to 20 us interval, Motor1QuickNominalSpeed = 65
   */
   
   
ISR(TCD0_OVF_vect/*, ISR_NAKED*/)
{	
	if (m1_exec_ctrl == 5)
		set_STEP_M2;
	
	if (m1_quick_count_down)
	{
		/* Run time is 2 us for the entire interrupt */
		if (read_DIR_M1 > 0)
		{
			app_regs.REG_ACCUMULATED_STEPS[1]++;
		}
		else
		{
			app_regs.REG_ACCUMULATED_STEPS[1]--;
		}		

		// Old way
		/*
		if (m1_quick_state_ctrl)
		{
			if (m1_quick_relative_steps <= m1_quick_start_increasing_interval)
			{
				m1_quick_timer_per += m1_quick_acc_interval;
				TCD0_PER = (m1_quick_timer_per - 1) >> 1;
			}
		}
		else
		{			
			if (m1_quick_relative_steps == m1_quick_stop_decreasing_interval)
			{
				m1_quick_timer_per = m1_quick_step_interval;
				TCD0_PER = (m1_quick_timer_per - 1) >> 1;
				m1_quick_state_ctrl++;
			}
			else
			{
				m1_quick_timer_per -= m1_quick_acc_interval;
				TCD0_PER = (m1_quick_timer_per - 1) >> 1;
			}
		}
		
		m1_quick_relative_steps--; // old way
		*/
		
		// New way
		
		m1_move_pulses--; // new way	
		if (m1_use_single_step)
		{
			m1_exec_ctrl--;
		
			if (m1_accelerating)
			{
				if (TCD0_PER > m1_timer_limit)
				{
					// Accelerate
					if (m1_exec_ctrl == STEPS_UPDATE_TIMER)
					{
						m1_single_step_per = TCD0_PER - 1;
					}
					
					m1_pulses_to_accelerate++;
				}
				else
				{
					m1_single_step_per = m1_timer_limit;
				}
			}
			else
			{
				// Decelerate
				if (m1_exec_ctrl == STEPS_UPDATE_TIMER)
				{
					m1_single_step_per = TCD0_PER + 1;
				}
			}
		
			if (m1_accelerating)
			{
				if ((m1_move_pulses == m1_pulses_to_accelerate + 10) || (m1_short_move_pulses == m1_pulses_to_accelerate + 2*8))
				{
					m1_accelerating = false;
				}
			}
		
			if (m1_exec_ctrl == STEPS_UPDATE_TIMER)
			{
				TCD0_PER = m1_single_step_per;
			
				if (TCD0_PER >= m1_delay)
				{
					m1_use_single_step = false;
					m1_exec_ctrl = STEPS_N + 1;
				}
			}
			
			if (m1_exec_ctrl == 0)
			{
				m1_exec_ctrl = STEPS_N_SINGLE_STEP + 1;
			}
		
			if (0)//m1_exec_ctrl == 4)
			{
				//if (m1_delay < 6000 && m1_delay > 20)
				{
					app_regs.REG_MOTOR2_QUICK_DISTANCE = m1_delay;
					core_func_send_event(ADD_REG_MOTOR2_QUICK_DISTANCE, true);
				}
			}
		}		
		else if (m1_use_steps)
		{
			m1_exec_ctrl--;
		
			if (m1_accelerating)
			{
				if (m1_speed < m1_speed_limit)
				{
					// Accelerate
					//set_STEP_M2;
				
					//m1_speed = m1_speed + (uint32_t)((m1_acc * m1_delay) / 1000.0);	// 21.2 us
					//m1_speed = m1_speed + (uint32_t)(((uint32_t)(m1_acc * m1_delay)) / 1024);	// 4.8 us
				
					//m1_speed = m1_speed + (uint32_t)((m1_acc * (uint16_t)m1_delay) / 1000.0);	// 19.0 us
					//m1_speed = m1_speed + (uint32_t)((m1_acc * (uint16_t)m1_delay));	// 1.2 us
					//m1_speed = m1_speed + (uint32_t)((m1_acc * m1_delay));			// 2.6 us
					//m1_speed = m1_speed + div(m1_delay, 1000);	// 21.2 us
				
					//div_t a;							//
					//a = div(m1_acc * m1_delay, 1000);	//
					//m1_speed = m1_speed + a.quot;		// 9 us
				
					if (1)//!m1_use_single_step)
					{
						if (m1_exec_ctrl == STEPS_SPEED_CALC_1OF2)
							m1_speed_temp = m1_acc * m1_delay;
						if (m1_exec_ctrl == STEPS_SPEED_CALC_2OF2)
							m1_speed_temp = m1_speed + m1_speed_temp / 1024;
						if (m1_exec_ctrl == STEPS_SPEED_CALC_3OF2)
							m1_speed = m1_speed_temp;
					}
				
					//clr_STEP_M2;
					m1_pulses_to_accelerate++;
				}
				else
				{
					m1_speed = m1_speed_limit;
				}
			}
			else
			{
				// Decelerate
				if (m1_speed > m1_speed_start)
				{
					//m1_speed = m1_speed - (uint32_t)((m1_acc * m1_delay) / 1024);
					if (m1_exec_ctrl == STEPS_SPEED_CALC_1OF2)
						m1_speed_temp = m1_acc * m1_delay;
					if (m1_exec_ctrl == STEPS_SPEED_CALC_2OF2)
						m1_speed_temp = m1_speed - m1_speed_temp / 1024;
					if (m1_exec_ctrl == STEPS_SPEED_CALC_3OF2)
						m1_speed = m1_speed_temp;
				
				}
				else
				{
					m1_speed = m1_speed_start; // Shouldn't be necessary?
				}
			}
		
			if (m1_accelerating)
			{
				if ((m1_move_pulses == m1_pulses_to_accelerate + 10) || (m1_short_move_pulses == m1_pulses_to_accelerate + 2*8))
				{
					m1_accelerating = false;
				}
			}
		
		
			//m1_delay = (uint16_t)(1000000.0/m1_speed);	// 18.8us
			//m1_delay = (uint16_t)(1000000/m1_speed);	// 19.2us
			//m1_delay_temp = 1000000.0/m1_speed;	// 28.4us max
		
			if (m1_exec_ctrl == STEPS_DELAY_CALC)
				//m1_delay_temp = 1000000/m1_speed;	// 27.6us max
				m1_delay_temp = (uint16_t)(1000000.0/m1_speed);
		
			if (m1_exec_ctrl == STEPS_UPDATE_TIMER)
			{
				m1_delay = m1_delay_temp;
		
				//clr_STEP_M2;
				TCD0_PER = (m1_delay >> 1) - 1;
			
				if (m1_delay >= MOVE_TO_STEPS_PERIOD)
				{
					m1_use_steps = false;
				}
				
				if (m1_delay <= MOVE_TO_SINGLE_STEP)
				{
					m1_use_single_step = true;
					m1_exec_ctrl = STEPS_N_SINGLE_STEP + 1;
				}
			}
			
			if (m1_exec_ctrl == 0)
			{
				m1_exec_ctrl = STEPS_N + 1;
			}
		
			if (0)//m1_exec_ctrl == 4)
			{
				//if (m1_delay < 6000 && m1_delay > 20)
				{
					app_regs.REG_MOTOR2_QUICK_DISTANCE = m1_delay;
					core_func_send_event(ADD_REG_MOTOR2_QUICK_DISTANCE, true);
				}
			}
		}
		else
		{
			if (m1_accelerating)
			{
				if (m1_speed < m1_speed_limit)
				{
					// Accelerate
					
				
					//m1_speed = m1_speed + (uint32_t)((m1_acc * m1_delay) / 1000.0);	// 21.2 us
					m1_speed = m1_speed + (uint32_t)(((uint32_t)(m1_acc * m1_delay)) / 1024);	// 4.8 us
				
					//m1_speed = m1_speed + (uint32_t)((m1_acc * (uint16_t)m1_delay) / 1000.0);	// 19.0 us
					//m1_speed = m1_speed + (uint32_t)((m1_acc * (uint16_t)m1_delay));	// 1.2 us
					//m1_speed = m1_speed + (uint32_t)((m1_acc * m1_delay));			// 2.6 us
					//m1_speed = m1_speed + div(m1_delay, 1000);	// 21.2 us
				
					//div_t a;							//
					//a = div(m1_acc * m1_delay, 1000);	//
					//m1_speed = m1_speed + a.quot;		// 9 us
				
					//clr_STEP_M2;
					m1_pulses_to_accelerate++;
				}
				else
				{
					m1_speed = m1_speed_limit;
				}
			}
			else
			{
				// Decelerate
				if (m1_speed > m1_speed_start)
				{
					m1_speed = m1_speed - (uint32_t)((m1_acc * m1_delay) / 1024);				
				}
				else
				{
					m1_speed = m1_speed_start; // Shouldn't be necessary?
				}
			}
		
			if (m1_accelerating)
			{
				if ((m1_move_pulses == m1_pulses_to_accelerate+10) || (m1_short_move_pulses == m1_pulses_to_accelerate))
				{
					m1_accelerating = false;
				}
			}
		
		
			m1_delay = (uint16_t)(1000000.0/m1_speed);	// 18.8us
			//m1_delay = (uint16_t)(1000000/m1_speed);	// 19.2us
			//m1_delay_temp = 1000000.0/m1_speed;	// 28.4us max
		
			
			TCD0_PER = (m1_delay >> 1) - 1;
			
			if (m1_delay <= MOVE_TO_STEPS_PERIOD)
			{
				m1_use_steps = true;
				m1_exec_ctrl = STEPS_N + 1;
			}
			
			if (0)//m1_delay < 6000 && m1_delay > 20)
			{
				app_regs.REG_MOTOR2_QUICK_DISTANCE = m1_delay;
				core_func_send_event(ADD_REG_MOTOR2_QUICK_DISTANCE, true);
			}
		}
		
	}
	else
	{
		timer_ovf_routine(1);	
	}
	
	clr_STEP_M2;
}
ISR(TCD0_CCA_vect/*, ISR_NAKED*/)
{	
	if (m1_quick_count_down)
	{
		/* Run time is 500 ns for the entire interrupt */
		if (m1_move_pulses == 0)
		{			
			/* Stop motor */
			stop_rotation(1);
			
			/* Since this is used at MID level interrupts, send an event from here can happen in the middle of other event */
			send_motor_stopped_notification[1] = true;
		}
	}
	else
	{
		timer_cca_routine(1);
	}
}

ISR(TCE0_OVF_vect/*, ISR_NAKED*/)
{	
	if (m2_quick_count_down)
	{
		/* Run time is 2 us for the entire interrupt */
		if (read_DIR_M2 > 0)
		{
			app_regs.REG_ACCUMULATED_STEPS[2]++;
		}
		else
		{
			app_regs.REG_ACCUMULATED_STEPS[2]--;
		}

		if (m2_quick_state_ctrl)
		{
			if (m2_quick_relative_steps <= m2_quick_start_increasing_interval)
			{
				m2_quick_timer_per += m2_quick_acc_interval;
				TCE0_PER = (m2_quick_timer_per - 1) >> 1;
			}
		}
		else
		{
			if (m2_quick_relative_steps == m2_quick_stop_decreasing_interval)
			{
				m2_quick_timer_per = m2_quick_step_interval;
				TCE0_PER = (m2_quick_timer_per - 1) >> 1;
				m2_quick_state_ctrl++;
			}
			else
			{
				m2_quick_timer_per -= m2_quick_acc_interval;
				TCE0_PER = (m2_quick_timer_per - 1) >> 1;
			}
		}
		
		m2_quick_relative_steps--;
		
	}
	else
	{
		timer_ovf_routine(2);
	}
}
ISR(TCE0_CCA_vect/*, ISR_NAKED*/)
{
	if (m2_quick_count_down)
	{
		/* Run time is 500 ns for the entire interrupt */
		if (m2_quick_relative_steps == 0)
		{
			/* Stop motor */
			stop_rotation(2);
			
			/* Since this is used at MID level interrupts, send an event from here can happen in the middle of other event */
			send_motor_stopped_notification[2] = true;
		}
	}
	else
	{
		timer_cca_routine(1);
	}
}

ISR(TCF0_OVF_vect/*, ISR_NAKED*/)
{
	timer_ovf_routine(3);
}
ISR(TCF0_CCA_vect/*, ISR_NAKED*/)
{
	timer_cca_routine(3);
}