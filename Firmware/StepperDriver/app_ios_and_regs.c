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
	io_pin2in(&PORTK, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // INPUT0
	io_pin2in(&PORTQ, 2, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // INPUT1
	io_pin2in(&PORTC, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // INPUT2
	io_pin2in(&PORTH, 7, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // INPUT3
	io_pin2in(&PORTC, 2, PULL_IO_UP, SENSE_IO_EDGE_FALLING);             // ERROR_M0
	io_pin2in(&PORTD, 2, PULL_IO_UP, SENSE_IO_EDGE_FALLING);             // ERROR_M1
	io_pin2in(&PORTE, 2, PULL_IO_UP, SENSE_IO_EDGE_FALLING);             // ERROR_M2
	io_pin2in(&PORTJ, 5, PULL_IO_UP, SENSE_IO_EDGE_FALLING);             // ERROR_M3
	io_pin2in(&PORTQ, 0, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // EMERGENCY

	/* Configure input interrupts */
	io_set_int(&PORTK, INT_LEVEL_LOW, 0, (1<<5), false);                 // INPUT0
	io_set_int(&PORTQ, INT_LEVEL_LOW, 0, (1<<2), false);                 // INPUT1
	io_set_int(&PORTC, INT_LEVEL_LOW, 0, (1<<5), false);                 // INPUT2
	io_set_int(&PORTH, INT_LEVEL_LOW, 0, (1<<7), false);                 // INPUT3
	io_set_int(&PORTC, INT_LEVEL_LOW, 1, (1<<2), false);                 // ERROR_M0
	io_set_int(&PORTD, INT_LEVEL_LOW, 1, (1<<2), false);                 // ERROR_M1
	io_set_int(&PORTE, INT_LEVEL_LOW, 1, (1<<2), false);                 // ERROR_M2
	io_set_int(&PORTJ, INT_LEVEL_LOW, 1, (1<<5), false);                 // ERROR_M3
	io_set_int(&PORTQ, INT_LEVEL_LOW, 1, (1<<0), false);                 // EMERGENCY

	/* Configure output pins */
	io_pin2out(&PORTA, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_STATE
	io_pin2out(&PORTJ, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_POWER
	io_pin2out(&PORTH, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M0
	io_pin2out(&PORTH, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M1
	io_pin2out(&PORTJ, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M2
	io_pin2out(&PORTQ, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_M3
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
	io_pin2out(&PORTB, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG0_M1
	io_pin2out(&PORTB, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG1_M1
	io_pin2out(&PORTB, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG2_M1
	io_pin2out(&PORTB, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG3_M1
	io_pin2out(&PORTB, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG5_M1
	io_pin2out(&PORTB, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG6_M1
	io_pin2out(&PORTB, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG7_M1
	io_pin2out(&PORTD, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DRIVE_ENABLE_M1
	io_pin2out(&PORTD, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG0_M2
	io_pin2out(&PORTD, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG1_M2
	io_pin2out(&PORTE, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG2_M2
	io_pin2out(&PORTE, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG3_M2
	io_pin2out(&PORTH, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG5_M2
	io_pin2out(&PORTH, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG6_M2
	io_pin2out(&PORTH, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG7_M2
	io_pin2out(&PORTE, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DRIVE_ENABLE_M2
	io_pin2out(&PORTH, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG0_M3
	io_pin2out(&PORTJ, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG1_M3
	io_pin2out(&PORTJ, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG2_M3
	io_pin2out(&PORTJ, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG3_M3
	io_pin2out(&PORTJ, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG5_M3
	io_pin2out(&PORTK, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG6_M3
	io_pin2out(&PORTK, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CFG7_M3
	io_pin2out(&PORTF, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DRIVE_ENABLE_M3

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
	clr_CFG5_M0;
	clr_CFG6_M0;
	clr_CFG7_M0;
	clr_DRIVE_ENABLE_M0;
	clr_CFG0_M1;
	clr_CFG1_M1;
	clr_CFG2_M1;
	clr_CFG3_M1;
	clr_CFG5_M1;
	clr_CFG6_M1;
	clr_CFG7_M1;
	clr_DRIVE_ENABLE_M1;
	clr_CFG0_M2;
	clr_CFG1_M2;
	clr_CFG2_M2;
	clr_CFG3_M2;
	clr_CFG5_M2;
	clr_CFG6_M2;
	clr_CFG7_M2;
	clr_DRIVE_ENABLE_M2;
	clr_CFG0_M3;
	clr_CFG1_M3;
	clr_CFG2_M3;
	clr_CFG3_M3;
	clr_CFG5_M3;
	clr_CFG6_M3;
	clr_CFG7_M3;
	clr_DRIVE_ENABLE_M3;
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
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
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
	TYPE_U8,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
	TYPE_I32,
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
	1,
	1,
	1,
	1,
	1,
	3,
	1,
	1,
	4,
	1,
	1,
	1,
	1,
	4,
	1,
	1,
	1,
	1,
	4,
	4,
	1,
	1,
	1,
	1,
	4,
	1,
	1,
	1,
	1,
	4,
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
	(uint8_t*)(&app_regs.REG_MOTOR0_HOLD_CURRENT_REDUCTION),
	(uint8_t*)(&app_regs.REG_MOTOR1_HOLD_CURRENT_REDUCTION),
	(uint8_t*)(&app_regs.REG_MOTOR2_HOLD_CURRENT_REDUCTION),
	(uint8_t*)(&app_regs.REG_MOTOR3_HOLD_CURRENT_REDUCTION),
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
	(uint8_t*)(&app_regs.REG_ACCUMULATED_STEPS_UPDATE_RATE),
	(uint8_t*)(&app_regs.REG_MOTORS_STOPPED),
	(uint8_t*)(&app_regs.REG_MOTORS_OVERVOLTAGE_DETECTION),
	(uint8_t*)(&app_regs.REG_MOTORS_ERROR_DETECTTION),
	(uint8_t*)(app_regs.REG_ENCODERS),
	(uint8_t*)(&app_regs.REG_DIGITAL_INPUTS_STATE),
	(uint8_t*)(&app_regs.REG_EMERGENCY_DETECTION),
	(uint8_t*)(app_regs.REG_MOTORS_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR0_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR1_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR2_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR3_STEPS),
	(uint8_t*)(app_regs.REG_MOTORS_ABSOLUTE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR0_ABSOLUTE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR1_ABSOLUTE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR2_ABSOLUTE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR3_ABSOLUTE_STEPS),
	(uint8_t*)(app_regs.REG_ACCUMULATED_STEPS),
	(uint8_t*)(app_regs.REG_MOTORS_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR0_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR1_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR2_MAX_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR3_MAX_STEPS_INTEGRATION),
	(uint8_t*)(app_regs.REG_MOTORS_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR0_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR1_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR2_MIN_STEPS_INTEGRATION),
	(uint8_t*)(&app_regs.REG_MOTOR3_MIN_STEPS_INTEGRATION),
	(uint8_t*)(app_regs.REG_MOTORS_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR0_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR1_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR2_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_MOTOR3_IMMEDIATE_STEPS),
	(uint8_t*)(&app_regs.REG_STOP_MOTORS_SUDENTLY),
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