#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#define F_CPU 32000000
#include <util/delay.h>

#include "i2c.h"
#include "stepper_control.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "StepperDriver";

void hwbp_app_initialize(void)
{
    /* Define versions */
    uint8_t hwH = 0;
    uint8_t hwL = 2;
    uint8_t fwH = 0;
    uint8_t fwL = 2;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        1130,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name,
		  true,	// This device is able to repeat the harp timestamp clock
		  false,	// The device is not able to generate the harp timestamp clock
		  0		// Default timestamp offset
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	/* Stop all motors */
	timer_type0_stop(&TCC0);
	timer_type0_stop(&TCD0);
	timer_type0_stop(&TCE0);
	timer_type0_stop(&TCF0);
	
	/* Disable motor drivers */
	clr_DRIVE_ENABLE_M0;
	clr_DRIVE_ENABLE_M1;
	clr_DRIVE_ENABLE_M2;
	clr_DRIVE_ENABLE_M3;
	
	/* Turn LEDs off */
	clr_LED_M0;
	clr_LED_M1;
	clr_LED_M2;
	clr_LED_M3;
	set_LED_POWER;
	
	/* Blink State LED forever in case of catastrophic error */
	while(1)
	{
		tgl_LED_STATE;
		_delay_ms(50*2);
	}
}

/************************************************************************/
/* User functions                                                       */
/************************************************************************/
/* Add your functions here or load external functions if needed */

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_define_clock_default(void) {}

#define T_STARTUP_ON  50
#define T_STARTUP_OFF 0

extern i2c_dev_t digi_pot_M0_M1;
extern i2c_dev_t digi_pot_M2_M3;

uint8_t i2c_test;

void core_callback_initialize_hardware(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Initialize hardware */
	
	/* LED startup sequence */
	for (uint8_t i = 0; i < 4; i++)
	{
		set_LED_M0;  _delay_ms(T_STARTUP_ON); clr_LED_M0;  _delay_ms(T_STARTUP_OFF);
		set_LED_M1;  _delay_ms(T_STARTUP_ON); clr_LED_M1;  _delay_ms(T_STARTUP_OFF);
		set_LED_M2;  _delay_ms(T_STARTUP_ON); clr_LED_M2;  _delay_ms(T_STARTUP_OFF);
		set_LED_M3;  _delay_ms(T_STARTUP_ON); clr_LED_M3;  _delay_ms(T_STARTUP_OFF);
		set_LED_POWER;  _delay_ms(T_STARTUP_ON); clr_LED_POWER;  _delay_ms(T_STARTUP_OFF);
		set_LED_STATE;  _delay_ms(T_STARTUP_ON); clr_LED_STATE;  _delay_ms(T_STARTUP_OFF);
	}
	_delay_ms(T_STARTUP_ON*2);

	for (uint8_t i = 0; i < 2; i++)
	{
		set_LED_M0;
		set_LED_M1;
		set_LED_M2;
		set_LED_M3;
		set_LED_POWER;
		set_LED_STATE;
		_delay_ms(T_STARTUP_ON*2);
		
		clr_LED_M0;
		clr_LED_M1;
		clr_LED_M2;
		clr_LED_M3;
		clr_LED_POWER;
		clr_LED_STATE;
		_delay_ms(T_STARTUP_ON*2);
	}
	
	_delay_ms(500);
	set_LED_POWER;
	
	/* Initialize I2C */
	i2c0_init();
	digi_pot_M0_M1.add = 0x2C;
	digi_pot_M2_M3.add = 0x2D;
	
	/* Initialize motor control */
	if (initialize_motors() == false)
	{
		while (1)
		{
			_delay_ms(100);
			tgl_LED_STATE;
		}
	}
	
	/* Remove interrupts from inputs */
	/* The registers will configure accordingly */
	io_pin2in(&PORTK, 5, PULL_IO_UP, SENSE_IO_NO_INT_USED);              // INPUT0
	io_pin2in(&PORTQ, 2, PULL_IO_UP, SENSE_IO_NO_INT_USED);              // INPUT1
	io_pin2in(&PORTC, 5, PULL_IO_UP, SENSE_IO_NO_INT_USED);              // INPUT2
	io_pin2in(&PORTH, 7, PULL_IO_UP, SENSE_IO_NO_INT_USED);              // INPUT3	
	io_set_int(&PORTK, INT_LEVEL_OFF, 0, (1<<5), false);                 // INPUT0
	io_set_int(&PORTQ, INT_LEVEL_OFF, 0, (1<<2), false);                 // INPUT1
	io_set_int(&PORTC, INT_LEVEL_OFF, 0, (1<<5), false);                 // INPUT2
	io_set_int(&PORTH, INT_LEVEL_OFF, 0, (1<<7), false);                 // INPUT3
	
	/* Initialize encoders */
	/* Set up quadrature decoding event */
	EVSYS_CH0MUX = EVSYS_CHMUX_PORTD_PIN4_gc;
	EVSYS_CH4MUX = EVSYS_CHMUX_PORTE_PIN4_gc;
	EVSYS_CH2MUX = EVSYS_CHMUX_PORTF_PIN4_gc;
	EVSYS_CH0CTRL = EVSYS_QDEN_bm | EVSYS_DIGFILT_2SAMPLES_gc;
	EVSYS_CH4CTRL = EVSYS_QDEN_bm | EVSYS_DIGFILT_2SAMPLES_gc;
	EVSYS_CH2CTRL = EVSYS_QDEN_bm | EVSYS_DIGFILT_2SAMPLES_gc;			
	/* Stop and reset timer */
	TCD1_CTRLA = TC_CLKSEL_OFF_gc;
	TCE1_CTRLA = TC_CLKSEL_OFF_gc;
	TCF1_CTRLA = TC_CLKSEL_OFF_gc;
	TCD1_CTRLFSET = TC_CMD_RESET_gc;
	TCE1_CTRLFSET = TC_CMD_RESET_gc;
	TCF1_CTRLFSET = TC_CMD_RESET_gc;			
	/* Configure timer */
	TCD1_CTRLD = TC_EVACT_QDEC_gc | TC_EVSEL_CH0_gc;
	TCE1_CTRLD = TC_EVACT_QDEC_gc | TC_EVSEL_CH4_gc;
	TCF1_CTRLD = TC_EVACT_QDEC_gc | TC_EVSEL_CH2_gc;
	TCD1_PER = 0xFFFF;
	TCE1_PER = 0xFFFF;
	TCF1_PER = 0xFFFF;
	TCD1_CNT = 0x8000;
	TCE1_CNT = 0x8000;
	TCF1_CNT = 0x8000;			
	/* Start timer */
	TCD1_CTRLA = TC_CLKSEL_DIV1_gc;
	TCE1_CTRLA = TC_CLKSEL_DIV1_gc;
	TCF1_CTRLA = TC_CLKSEL_DIV1_gc;
}

/* Functions core_callback_reset_registers(void) and
   core_callback_registers_were_reinitialized(void) moved to file regs_reset_and_init.c
*/

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
#define MOTOR_ENABLED_LED_PULSE_DURATION_MS 50
#define ENABLE_DEVICE_DEBOUCE_PERIOD_MS 250

uint16_t acquisition_counter = 0;

extern uint8_t enable_counter;
extern void enable_motors (void);

extern int32_t user_requested_steps[];
extern bool send_motor_stopped_notification[];

void core_callback_t_before_exec(void)
{
	acquisition_counter++;
	
	if (app_regs.REG_ENABLE_ENCODERS)
	{
		if ((app_regs.REG_ENCODERS_UPDATE_RATE == GM_RATE_500HZ && ((acquisition_counter & 3) == 0)) ||
			(app_regs.REG_ENCODERS_UPDATE_RATE == GM_RATE_250HZ && ((acquisition_counter & 7) == 0)) ||
			(app_regs.REG_ENCODERS_UPDATE_RATE == GM_RATE_200HZ && ((acquisition_counter % 10) == 0)) ||
			(app_regs.REG_ENCODERS_UPDATE_RATE == GM_RATE_100HZ && ((acquisition_counter % 20) == 0)))
		{
			int16_t timer_tcd1_cnt = TCD1_CNT;
			int16_t timer_tce1_cnt = TCE1_CNT;
			int16_t timer_tcf1_cnt = TCF1_CNT;
			
			if (timer_tcd1_cnt > 32768)
				app_regs.REG_ENCODERS[2] = 0xFFFF - timer_tcd1_cnt;
			else
				app_regs.REG_ENCODERS[2] = (32768 - timer_tcd1_cnt) * -1;
			
			if (timer_tce1_cnt > 32768)
				app_regs.REG_ENCODERS[0] = 0xFFFF - timer_tce1_cnt;
			else
				app_regs.REG_ENCODERS[0] = (32768 - timer_tce1_cnt) * -1;
			
			if (timer_tcf1_cnt > 32768)
				app_regs.REG_ENCODERS[1] = 0xFFFF - timer_tcf1_cnt;
			else
				app_regs.REG_ENCODERS[1] = (32768 - timer_tcf1_cnt) * -1;
			
			core_func_send_event(ADD_REG_ENCODERS, true);
		}
	}
	
	if (enable_counter)
	{
		if ((read_EMERGENCY == false && app_regs.REG_EMERGENCY_DETECTION_MODE == GM_CLOSED) || (read_EMERGENCY == true && app_regs.REG_EMERGENCY_DETECTION_MODE == GM_OPEN))
		{		
			if ((acquisition_counter % 20) == 0) // 100 Hz meaning 10 ms period
			{
				enable_counter++;
			
				if (enable_counter > ENABLE_DEVICE_DEBOUCE_PERIOD_MS / 10)
				{
					enable_counter = 0;
				
					enable_motors();
				}
			}
		}
		else
		{
			enable_counter = 1;
		}
	}
	
	/* Notify that motor is stopped */
	uint8_t motors_mask = 0;	
	for (uint8_t i = 0; i < MOTORS_QUANTITY; i++)
	{
		
		
		if (send_motor_stopped_notification[i])
		{
			send_motor_stopped_notification[i] = false;
			
			motors_mask |= (1<<i);
		}		
	}	
	if (motors_mask)
	{		
		app_regs.REG_MOTORS_STOPPED = motors_mask;
		core_func_send_event(ADD_REG_MOTORS_STOPPED, true);
	}
	
	if (core_bool_is_visual_enabled())
	{
		if (acquisition_counter == 2)
		{
			if (read_DRIVE_ENABLE_M0 == false) set_LED_M0;
			if (read_DRIVE_ENABLE_M1 == false) set_LED_M1;
			if (read_DRIVE_ENABLE_M2 == false) set_LED_M2;
			if (read_DRIVE_ENABLE_M3 == false) set_LED_M3;
		}
	}
		
	if (acquisition_counter == 2 + MOTOR_ENABLED_LED_PULSE_DURATION_MS / 2)
	{
		if (read_DRIVE_ENABLE_M0 == false && TCC0_CTRLA == 0) clr_LED_M0;
		if (read_DRIVE_ENABLE_M1 == false && TCD0_CTRLA == 0) clr_LED_M1;
		if (read_DRIVE_ENABLE_M2 == false && TCE0_CTRLA == 0) clr_LED_M2;
		if (read_DRIVE_ENABLE_M3 == false && TCF0_CTRLA == 0) clr_LED_M3;
	}
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void)
{
	acquisition_counter = 0;
}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void)
{
// 	if ((app_regs.REG_CONTROL & B_ENABLE_MOTOR) == false)
// 	{
// 		/* Disable medium and high level interrupts */
// 		/* Medium are enough but we can win some precious cpu time here */
// 		PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm;
// 		
// 		/* Stop motor */
// 		stop_rotation();
// 		
// 		/* Re-enable all interrupt levels */
// 		PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm | PMIC_MEDLVLEN_bm | PMIC_HILVLEN_bm;
// 	}
	
	if (user_requested_steps[0] != 0)
	{
		/* Disable medium and high level interrupts */
		/* Medium are enough but we can win some precious cpu time here */
		PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm;
		
		/* Update steps with the user request */
		user_requested_steps[0] = user_sent_request(user_requested_steps[0], 0);
		
		/* Re-enable all interrupt levels */
		PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm | PMIC_MEDLVLEN_bm | PMIC_HILVLEN_bm;
	}
}

/************************************************************************/
/* Callbacks: clock control                                             */
/************************************************************************/
void core_callback_clock_to_repeater(void) {}
void core_callback_clock_to_generator(void) {}
void core_callback_clock_to_unlock(void) {}
void core_callback_clock_to_lock(void) {}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}