#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTD, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER0_A
	io_pin2in(&PORTD, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER0_B
	io_pin2in(&PORTE, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER1_A
	io_pin2in(&PORTE, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER1_B
	io_pin2in(&PORTF, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER2_A
	io_pin2in(&PORTF, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // ENCODER2_B

	/* Configure input interrupts */

	/* Configure output pins */
	io_pin2out(&PORTA, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_STATE
	io_pin2out(&PORTJ, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_POWER
	io_pin2out(&PORTH, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M0
	io_pin2out(&PORTH, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M1
	io_pin2out(&PORTJ, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M2
	//io_pin2out(&PORTQ, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M3
	io_pin2out(&PORTC, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // STEP_M0
	io_pin2out(&PORTD, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // STEP_M1
	io_pin2out(&PORTE, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // STEP_M2
	io_pin2out(&PORTF, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // STEP_M3
	io_pin2out(&PORTC, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DIR_M0
	io_pin2out(&PORTD, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DIR_M1
	io_pin2out(&PORTE, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DIR_M2
	io_pin2out(&PORTF, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DIR_M3
	io_pin2out(&PORTA, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG0_M0
	io_pin2out(&PORTA, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG1_M0
	io_pin2out(&PORTA, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG2_M0
	io_pin2out(&PORTA, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG3_M0
	io_pin2out(&PORTA, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG5_M0
	io_pin2out(&PORTA, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG6_M0
	io_pin2out(&PORTC, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG7_M0
	io_pin2out(&PORTC, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DRIVE_ENABLE_M0

	/* Initialize output pins */
	clr_LED_STATE;
	clr_LED_POWER;
	clr_LED_M0;
	clr_LED_M1;
	clr_LED_M2;
	clr_LED_M3;
	clr_STEP_M0;
	clr_STEP_M1;
	clr_STEP_M2;
	clr_STEP_M3;
	clr_DIR_M0;
	clr_DIR_M1;
	clr_DIR_M2;
	clr_DIR_M3;
	clr_CFG0_M0;
	clr_CFG1_M0;
	clr_CFG2_M0;
	clr_CFG3_M0;
	set_CFG5_M0;
	clr_CFG6_M0;
	clr_CFG7_M0;
	clr_DRIVE_ENABLE_M0;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_FLOAT,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_U32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	3,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_ENABLE_MOTORS),
	(uint8_t*)(&app_regs.REG_DISABLE_MOTORS),
	(uint8_t*)(&app_regs.REG_ENABLE_ENCODERS),
	(uint8_t*)(&app_regs.REG_DISABLE_ENCODERS),
	(uint8_t*)(&app_regs.REG_ENABLE_INPUTS),
	(uint8_t*)(&app_regs.REG_DISABLE_INPUTS),
	(uint8_t*)(&app_regs.REG_MOTOR0_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_MOTOR1_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_MOTOR2_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_MOTOR3_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_MOTOR0_MICROSTEP_RESOLUTION),
	(uint8_t*)(&app_regs.REG_MOTOR1_MICROSTEP_RESOLUTION),
	(uint8_t*)(&app_regs.REG_MOTOR2_MICROSTEP_RESOLUTION),
	(uint8_t*)(&app_regs.REG_MOTOR3_MICROSTEP_RESOLUTION),
	(uint8_t*)(&app_regs.REG_MOTOR0_MAXIMUM_CURRENT_RMS),
	(uint8_t*)(&app_regs.REG_MOTOR1_MAXIMUM_CURRENT_RMS),
	(uint8_t*)(&app_regs.REG_MOTOR2_MAXIMUM_CURRENT_RMS),
	(uint8_t*)(&app_regs.REG_MOTOR3_MAXIMUM_CURRENT_RMS),
	(uint8_t*)(&app_regs.REG_MOTOR0_NOMINAL_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR1_NOMINAL_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR2_NOMINAL_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR3_NOMINAL_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR0_MAXIMUM_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR1_MAXIMUM_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR2_MAXIMUM_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR3_MAXIMUM_STEP_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR0_STEP_ACCELERATION_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR1_STEP_ACCELERATION_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR2_STEP_ACCELERATION_INTERVAL),
	(uint8_t*)(&app_regs.REG_MOTOR3_STEP_ACCELERATION_INTERVAL),
	(uint8_t*)(&app_regs.REG_ENCODERS_MODE),
	(uint8_t*)(&app_regs.REG_ENCODERS_UPDATE_RATE),
	(uint8_t*)(&app_regs.REG_INPUT0_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_INPUT1_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_INPUT2_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_INPUT3_OPERATION_MODE),
	(uint8_t*)(&app_regs.REG_INPUT0_SENSE_MODE),
	(uint8_t*)(&app_regs.REG_INPUT1_SENSE_MODE),
	(uint8_t*)(&app_regs.REG_INPUT2_SENSE_MODE),
	(uint8_t*)(&app_regs.REG_INPUT3_SENSE_MODE),
	(uint8_t*)(&app_regs.REG_EMERGENCY_DETECTION_MODE),
	(uint8_t*)(&app_regs.REG_MOTORS_STOPPED),
	(uint8_t*)(&app_regs.REG_MOTORS_OVERVOLTAGE_DETECTION),
	(uint8_t*)(&app_regs.REG_MOTORS_ERROR_DETECTTION),
	(uint8_t*)(app_regs.REG_ENCODERS),
	(uint8_t*)(&app_regs.REG_DIGITAL_INPUTS_STATE),
	(uint8_t*)(&app_regs.REG_EMERGENCY_DETECTION),
	(uint8_t*)(&app_regs.REG_MOTOR0_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR1_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR2_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR3_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR1_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR2_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR3_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR1_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR2_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR3_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR0_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR1_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR2_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR3_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_STOP_MOTORS_SUDENTLY),
	(uint8_t*)(&app_regs.REG_STOP_MOTORS_BY_DECELERATION),
	(uint8_t*)(&app_regs.REG_RESET_MOTORS_ERROR_DETECTION),
	(uint8_t*)(&app_regs.REG_RESET_ENCODERS),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_RESERVED2),
	(uint8_t*)(&app_regs.REG_RESERVED3),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_RESERVED5),
	(uint8_t*)(&app_regs.REG_RESERVED6),
	(uint8_t*)(&app_regs.REG_RESERVED7)
};