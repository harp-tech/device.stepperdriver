#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#define F_CPU 32000000
#include <util/delay.h>

#include "AD5048A.h"
#include "PAA5100JE.h"

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
    uint8_t hwL = 1;
    uint8_t fwH = 0;
    uint8_t fwL = 1;
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
	clr_LED_M0;
	clr_LED_M1;
	clr_LED_M2;
	clr_LED_M3;
}

/************************************************************************/
/* User functions                                                       */
/************************************************************************/
/* Add your functions here or load external functions if needed */

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_define_clock_default(void) {}

#define T_STARTUP_ON  100
#define T_STARTUP_OFF 0

void core_callback_initialize_hardware(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Initialize hardware */
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
		_delay_ms(T_STARTUP_ON);
		
		clr_LED_M0;
		clr_LED_M1;
		clr_LED_M2;
		clr_LED_M3;
		clr_LED_POWER;
		clr_LED_STATE;
		_delay_ms(T_STARTUP_ON);
	}
	
	_delay_ms(500);
	set_LED_POWER;
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */		
}

void core_callback_registers_were_reinitialized(void)
{
	/* Update registers if needed */
}

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
void core_callback_t_before_exec(void) {}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void) {}

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