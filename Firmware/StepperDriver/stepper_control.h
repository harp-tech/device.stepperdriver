#ifndef _STEPPER_CONTROL_H_
#define _STEPPER_CONTROL_H_
#include <avr/io.h>

// Define if not defined
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
	#define false 0
#endif

/************************************************************************/
/* User mandatory definitions                                           */
/************************************************************************/
// Define number of available motors
#define MOTORS_QUANTITY 4

/************************************************************************/
/* Update global electrical pulse parameters                            */
/************************************************************************/
bool initialize_motors (void);

bool update_nominal_pulse_interval (uint16_t time_us, uint8_t motor_index);
bool update_initial_pulse_interval (uint16_t time_us, uint8_t motor_index);
bool update_pulse_step_interval (uint16_t time_us, uint8_t motor_index);
bool update_pulse_period (uint16_t time_us, uint8_t motor_index);

/************************************************************************/
/* Start & Stop functions                                               */
/************************************************************************/
void start_rotation (int32_t requested_steps, uint8_t motor_index);
void stop_rotation (uint8_t motor_index);
void reduce_until_stop_rotation (uint8_t motor_index);

bool if_moving_stop_rotation (uint8_t motor_index);

bool is_timer_ready (uint8_t motor_index);

/************************************************************************/
/* Update motion                                                        */
/************************************************************************/
int32_t user_sent_request (int32_t requested_steps, uint8_t motor_index);

/************************************************************************/
/* Manage boundaries                                                    */
/************************************************************************/
void manage_step_boundaries (uint8_t motor_index);

#endif /* _STEPPER_CONTROL_H_ */