#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

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
/* INPUT0                                                               */
/************************************************************************/
ISR(PORTK_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* INPUT1                                                               */
/************************************************************************/
ISR(PORTQ_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* INPUT2                                                               */
/************************************************************************/
ISR(PORTC_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* INPUT3                                                               */
/************************************************************************/
ISR(PORTH_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* ERROR_M0                                                             */
/************************************************************************/
ISR(PORTC_INT1_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* ERROR_M1                                                             */
/************************************************************************/
ISR(PORTD_INT1_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* ERROR_M2                                                             */
/************************************************************************/
ISR(PORTE_INT1_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* ERROR_M3                                                             */
/************************************************************************/
ISR(PORTJ_INT1_vect, ISR_NAKED)
{
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
				
				clr_DRIVE_ENABLE_M0; timer_type0_stop(&TCC0); clr_LED_M0;
				clr_DRIVE_ENABLE_M1; timer_type0_stop(&TCD0); clr_LED_M1;
				clr_DRIVE_ENABLE_M2; timer_type0_stop(&TCE0); clr_LED_M2;
				clr_DRIVE_ENABLE_M3; timer_type0_stop(&TCF0); clr_LED_M3;
				
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
				
				clr_DRIVE_ENABLE_M0; timer_type0_stop(&TCC0); clr_LED_M0;
				clr_DRIVE_ENABLE_M1; timer_type0_stop(&TCD0); clr_LED_M1;
				clr_DRIVE_ENABLE_M2; timer_type0_stop(&TCE0); clr_LED_M2;
				clr_DRIVE_ENABLE_M3; timer_type0_stop(&TCF0); clr_LED_M3;
				
				app_regs.REG_EMERGENCY_DETECTION = GM_DISABLED;
				core_func_send_event(ADD_REG_EMERGENCY_DETECTION, true);
			}
		}
		
	}
	reti();
}

