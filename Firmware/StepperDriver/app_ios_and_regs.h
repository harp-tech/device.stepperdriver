#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// ENCODER0_A             Description: Encoder 0 A
// ENCODER0_B             Description: Encoder 0 B
// ENCODER1_A             Description: Encoder 1 A
// ENCODER1_B             Description: Encoder 1 B
// ENCODER2_A             Description: Encoder 2 A
// ENCODER2_B             Description: Encoder 3 B
// INPUT0                 Description: Digital input 0
// INPUT1                 Description: Digital input 1
// INPUT2                 Description: Digital input 2
// INPUT3                 Description: Digital input 3
// ERROR_M0               Description: Error report for motor 0
// ERROR_M1               Description: Error report for motor 1
// ERROR_M2               Description: Error report for motor 2
// ERROR_M3               Description: Error report for motor 3
// EMERGENCY              Description: Emergency input

#define read_ENCODER0_A read_io(PORTD, 4)       // ENCODER0_A
#define read_ENCODER0_B read_io(PORTD, 5)       // ENCODER0_B
#define read_ENCODER1_A read_io(PORTE, 4)       // ENCODER1_A
#define read_ENCODER1_B read_io(PORTE, 5)       // ENCODER1_B
#define read_ENCODER2_A read_io(PORTF, 4)       // ENCODER2_A
#define read_ENCODER2_B read_io(PORTF, 5)       // ENCODER2_B
#define read_INPUT0 read_io(PORTK, 5)           // INPUT0
#define read_INPUT1 read_io(PORTQ, 2)           // INPUT1
#define read_INPUT2 read_io(PORTC, 5)           // INPUT2
#define read_INPUT3 read_io(PORTH, 7)           // INPUT3
#define read_ERROR_M0 read_io(PORTC, 2)         // ERROR_M0
#define read_ERROR_M1 read_io(PORTD, 2)         // ERROR_M1
#define read_ERROR_M2 read_io(PORTE, 2)         // ERROR_M2
#define read_ERROR_M3 read_io(PORTJ, 5)         // ERROR_M3
#define read_EMERGENCY read_io(PORTQ, 0)        // EMERGENCY

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// LED_STATE              Description: State LED
// LED_POWER              Description: Power LED
// LED_M0                 Description: Motor 0 LED
// LED_M1                 Description: Motor 1 LED
// LED_M2                 Description: Motor 2 LED
// LED_M3                 Description: Motor 3 LED (DON'T USE YET)
// STEP_M0                Description: Motor 0 step control
// STEP_M1                Description: Motor 1 step control
// STEP_M2                Description: Motor 2 step control
// STEP_M3                Description: Motor 3 step control
// DIR_M0                 Description: Motor 0 direction control
// DIR_M1                 Description: Motor 1 direction control
// DIR_M2                 Description: Motor 2 direction control
// DIR_M3                 Description: Motor 3 direction control
// CFG0_M0                Description: Motor 0 configuration pin 0
// CFG1_M0                Description: Motor 0 configuration pin 1
// CFG2_M0                Description: Motor 0 configuration pin 2
// CFG3_M0                Description: Motor 0 configuration pin 3
// CFG5_M0                Description: Motor 0 configuration pin 5
// CFG6_M0                Description: Motor 0 configuration pin 6
// CFG7_M0                Description: Motor 0 configuration pin 7
// DRIVE_ENABLE_M0        Description: Motor 0 drive enable
// CFG0_M1                Description: Motor 1 configuration pin 0
// CFG1_M1                Description: Motor 1 configuration pin 1
// CFG2_M1                Description: Motor 1 configuration pin 2
// CFG3_M1                Description: Motor 1 configuration pin 3
// CFG5_M1                Description: Motor 1 configuration pin 5
// CFG6_M1                Description: Motor 1 configuration pin 6
// CFG7_M1                Description: Motor 1 configuration pin 7
// DRIVE_ENABLE_M1        Description: Motor 1 drive enable
// CFG0_M2                Description: Motor 2 configuration pin 0
// CFG1_M2                Description: Motor 2 configuration pin 1
// CFG2_M2                Description: Motor 2 configuration pin 2
// CFG3_M2                Description: Motor 2 configuration pin 3
// CFG5_M2                Description: Motor 2 configuration pin 5
// CFG6_M2                Description: Motor 2 configuration pin 6
// CFG7_M2                Description: Motor 2 configuration pin 7
// DRIVE_ENABLE_M2        Description: Motor 2 drive enable
// CFG0_M3                Description: Motor 3 configuration pin 0
// CFG1_M3                Description: Motor 3 configuration pin 1
// CFG2_M3                Description: Motor 3 configuration pin 2
// CFG3_M3                Description: Motor 3 configuration pin 3
// CFG5_M3                Description: Motor 3 configuration pin 5
// CFG6_M3                Description: Motor 3 configuration pin 6
// CFG7_M3                Description: Motor 3 configuration pin 7
// DRIVE_ENABLE_M3        Description: Motor 3 drive enable

/* LED_STATE */
#define set_LED_STATE set_io(PORTA, 6)
#define clr_LED_STATE clear_io(PORTA, 6)
#define tgl_LED_STATE toggle_io(PORTA, 6)
#define read_LED_STATE read_io(PORTA, 6)

/* LED_POWER */
#define set_LED_POWER set_io(PORTJ, 2)
#define clr_LED_POWER clear_io(PORTJ, 2)
#define tgl_LED_POWER toggle_io(PORTJ, 2)
#define read_LED_POWER read_io(PORTJ, 2)

/* LED_M0 */
#define set_LED_M0 set_io(PORTH, 3)
#define clr_LED_M0 clear_io(PORTH, 3)
#define tgl_LED_M0 toggle_io(PORTH, 3)
#define read_LED_M0 read_io(PORTH, 3)

/* LED_M1 */
#define set_LED_M1 set_io(PORTH, 4)
#define clr_LED_M1 clear_io(PORTH, 4)
#define tgl_LED_M1 toggle_io(PORTH, 4)
#define read_LED_M1 read_io(PORTH, 4)

/* LED_M2 */
#define set_LED_M2 set_io(PORTJ, 0)
#define clr_LED_M2 clear_io(PORTJ, 0)
#define tgl_LED_M2 toggle_io(PORTJ, 0)
#define read_LED_M2 read_io(PORTJ, 0)

/* LED_M3 */
#define set_LED_M3 set_io(PORTQ, 1)
#define clr_LED_M3 clear_io(PORTQ, 1)
#define tgl_LED_M3 toggle_io(PORTQ, 1)
#define read_LED_M3 read_io(PORTQ, 1)

/* STEP_M0 */
#define set_STEP_M0 set_io(PORTC, 0)
#define clr_STEP_M0 clear_io(PORTC, 0)
#define tgl_STEP_M0 toggle_io(PORTC, 0)
#define read_STEP_M0 read_io(PORTC, 0)

/* STEP_M1 */
#define set_STEP_M1 set_io(PORTD, 0)
#define clr_STEP_M1 clear_io(PORTD, 0)
#define tgl_STEP_M1 toggle_io(PORTD, 0)
#define read_STEP_M1 read_io(PORTD, 0)

/* STEP_M2 */
#define set_STEP_M2 set_io(PORTE, 0)
#define clr_STEP_M2 clear_io(PORTE, 0)
#define tgl_STEP_M2 toggle_io(PORTE, 0)
#define read_STEP_M2 read_io(PORTE, 0)

/* STEP_M3 */
#define set_STEP_M3 set_io(PORTF, 0)
#define clr_STEP_M3 clear_io(PORTF, 0)
#define tgl_STEP_M3 toggle_io(PORTF, 0)
#define read_STEP_M3 read_io(PORTF, 0)

/* DIR_M0 */
#define set_DIR_M0 set_io(PORTC, 1)
#define clr_DIR_M0 clear_io(PORTC, 1)
#define tgl_DIR_M0 toggle_io(PORTC, 1)
#define read_DIR_M0 read_io(PORTC, 1)

/* DIR_M1 */
#define set_DIR_M1 set_io(PORTD, 1)
#define clr_DIR_M1 clear_io(PORTD, 1)
#define tgl_DIR_M1 toggle_io(PORTD, 1)
#define read_DIR_M1 read_io(PORTD, 1)

/* DIR_M2 */
#define set_DIR_M2 set_io(PORTE, 1)
#define clr_DIR_M2 clear_io(PORTE, 1)
#define tgl_DIR_M2 toggle_io(PORTE, 1)
#define read_DIR_M2 read_io(PORTE, 1)

/* DIR_M3 */
#define set_DIR_M3 set_io(PORTF, 1)
#define clr_DIR_M3 clear_io(PORTF, 1)
#define tgl_DIR_M3 toggle_io(PORTF, 1)
#define read_DIR_M3 read_io(PORTF, 1)

/* CFG0_M0 */
#define set_CFG0_M0 set_io(PORTA, 0)
#define clr_CFG0_M0 clear_io(PORTA, 0)
#define tgl_CFG0_M0 toggle_io(PORTA, 0)
#define read_CFG0_M0 read_io(PORTA, 0)

/* CFG1_M0 */
#define set_CFG1_M0 set_io(PORTA, 1)
#define clr_CFG1_M0 clear_io(PORTA, 1)
#define tgl_CFG1_M0 toggle_io(PORTA, 1)
#define read_CFG1_M0 read_io(PORTA, 1)

/* CFG2_M0 */
#define set_CFG2_M0 set_io(PORTA, 2)
#define clr_CFG2_M0 clear_io(PORTA, 2)
#define tgl_CFG2_M0 toggle_io(PORTA, 2)
#define read_CFG2_M0 read_io(PORTA, 2)

/* CFG3_M0 */
#define set_CFG3_M0 set_io(PORTA, 3)
#define clr_CFG3_M0 clear_io(PORTA, 3)
#define tgl_CFG3_M0 toggle_io(PORTA, 3)
#define read_CFG3_M0 read_io(PORTA, 3)

/* CFG5_M0 */
#define set_CFG5_M0 set_io(PORTA, 5)
#define clr_CFG5_M0 clear_io(PORTA, 5)
#define tgl_CFG5_M0 toggle_io(PORTA, 5)
#define read_CFG5_M0 read_io(PORTA, 5)

/* CFG6_M0 */
#define set_CFG6_M0 set_io(PORTA, 7)
#define clr_CFG6_M0 clear_io(PORTA, 7)
#define tgl_CFG6_M0 toggle_io(PORTA, 7)
#define read_CFG6_M0 read_io(PORTA, 7)

/* CFG7_M0 */
#define set_CFG7_M0 set_io(PORTC, 4)
#define clr_CFG7_M0 clear_io(PORTC, 4)
#define tgl_CFG7_M0 toggle_io(PORTC, 4)
#define read_CFG7_M0 read_io(PORTC, 4)

/* DRIVE_ENABLE_M0 */
#define set_DRIVE_ENABLE_M0 clear_io(PORTC, 3)
#define clr_DRIVE_ENABLE_M0 set_io(PORTC, 3)
#define tgl_DRIVE_ENABLE_M0 toggle_io(PORTC, 3)
#define read_DRIVE_ENABLE_M0 read_io(PORTC, 3)

/* CFG0_M1 */
#define set_CFG0_M1 set_io(PORTB, 0)
#define clr_CFG0_M1 clear_io(PORTB, 0)
#define tgl_CFG0_M1 toggle_io(PORTB, 0)
#define read_CFG0_M1 read_io(PORTB, 0)

/* CFG1_M1 */
#define set_CFG1_M1 set_io(PORTB, 1)
#define clr_CFG1_M1 clear_io(PORTB, 1)
#define tgl_CFG1_M1 toggle_io(PORTB, 1)
#define read_CFG1_M1 read_io(PORTB, 1)

/* CFG2_M1 */
#define set_CFG2_M1 set_io(PORTB, 2)
#define clr_CFG2_M1 clear_io(PORTB, 2)
#define tgl_CFG2_M1 toggle_io(PORTB, 2)
#define read_CFG2_M1 read_io(PORTB, 2)

/* CFG3_M1 */
#define set_CFG3_M1 set_io(PORTB, 3)
#define clr_CFG3_M1 clear_io(PORTB, 3)
#define tgl_CFG3_M1 toggle_io(PORTB, 3)
#define read_CFG3_M1 read_io(PORTB, 3)

/* CFG5_M1 */
#define set_CFG5_M1 set_io(PORTB, 5)
#define clr_CFG5_M1 clear_io(PORTB, 5)
#define tgl_CFG5_M1 toggle_io(PORTB, 5)
#define read_CFG5_M1 read_io(PORTB, 5)

/* CFG6_M1 */
#define set_CFG6_M1 set_io(PORTB, 6)
#define clr_CFG6_M1 clear_io(PORTB, 6)
#define tgl_CFG6_M1 toggle_io(PORTB, 6)
#define read_CFG6_M1 read_io(PORTB, 6)

/* CFG7_M1 */
#define set_CFG7_M1 set_io(PORTB, 7)
#define clr_CFG7_M1 clear_io(PORTB, 7)
#define tgl_CFG7_M1 toggle_io(PORTB, 7)
#define read_CFG7_M1 read_io(PORTB, 7)

/* DRIVE_ENABLE_M1 */
#define set_DRIVE_ENABLE_M1 clear_io(PORTD, 3)
#define clr_DRIVE_ENABLE_M1 set_io(PORTD, 3)
#define tgl_DRIVE_ENABLE_M1 toggle_io(PORTD, 3)
#define read_DRIVE_ENABLE_M1 read_io(PORTD, 3)

/* CFG0_M2 */
#define set_CFG0_M2 set_io(PORTD, 6)
#define clr_CFG0_M2 clear_io(PORTD, 6)
#define tgl_CFG0_M2 toggle_io(PORTD, 6)
#define read_CFG0_M2 read_io(PORTD, 6)

/* CFG1_M2 */
#define set_CFG1_M2 set_io(PORTD, 7)
#define clr_CFG1_M2 clear_io(PORTD, 7)
#define tgl_CFG1_M2 toggle_io(PORTD, 7)
#define read_CFG1_M2 read_io(PORTD, 7)

/* CFG2_M2 */
#define set_CFG2_M2 set_io(PORTE, 6)
#define clr_CFG2_M2 clear_io(PORTE, 6)
#define tgl_CFG2_M2 toggle_io(PORTE, 6)
#define read_CFG2_M2 read_io(PORTE, 6)

/* CFG3_M2 */
#define set_CFG3_M2 set_io(PORTE, 7)
#define clr_CFG3_M2 clear_io(PORTE, 7)
#define tgl_CFG3_M2 toggle_io(PORTE, 7)
#define read_CFG3_M2 read_io(PORTE, 7)

/* CFG5_M2 */
#define set_CFG5_M2 set_io(PORTH, 1)
#define clr_CFG5_M2 clear_io(PORTH, 1)
#define tgl_CFG5_M2 toggle_io(PORTH, 1)
#define read_CFG5_M2 read_io(PORTH, 1)

/* CFG6_M2 */
#define set_CFG6_M2 set_io(PORTH, 2)
#define clr_CFG6_M2 clear_io(PORTH, 2)
#define tgl_CFG6_M2 toggle_io(PORTH, 2)
#define read_CFG6_M2 read_io(PORTH, 2)

/* CFG7_M2 */
#define set_CFG7_M2 set_io(PORTH, 5)
#define clr_CFG7_M2 clear_io(PORTH, 5)
#define tgl_CFG7_M2 toggle_io(PORTH, 5)
#define read_CFG7_M2 read_io(PORTH, 5)

/* DRIVE_ENABLE_M2 */
#define set_DRIVE_ENABLE_M2 clear_io(PORTE, 3)
#define clr_DRIVE_ENABLE_M2 set_io(PORTE, 3)
#define tgl_DRIVE_ENABLE_M2 toggle_io(PORTE, 3)
#define read_DRIVE_ENABLE_M2 read_io(PORTE, 3)

/* CFG0_M3 */
#define set_CFG0_M3 set_io(PORTH, 6)
#define clr_CFG0_M3 clear_io(PORTH, 6)
#define tgl_CFG0_M3 toggle_io(PORTH, 6)
#define read_CFG0_M3 read_io(PORTH, 6)

/* CFG1_M3 */
#define set_CFG1_M3 set_io(PORTJ, 1)
#define clr_CFG1_M3 clear_io(PORTJ, 1)
#define tgl_CFG1_M3 toggle_io(PORTJ, 1)
#define read_CFG1_M3 read_io(PORTJ, 1)

/* CFG2_M3 */
#define set_CFG2_M3 set_io(PORTJ, 3)
#define clr_CFG2_M3 clear_io(PORTJ, 3)
#define tgl_CFG2_M3 toggle_io(PORTJ, 3)
#define read_CFG2_M3 read_io(PORTJ, 3)

/* CFG3_M3 */
#define set_CFG3_M3 set_io(PORTJ, 4)
#define clr_CFG3_M3 clear_io(PORTJ, 4)
#define tgl_CFG3_M3 toggle_io(PORTJ, 4)
#define read_CFG3_M3 read_io(PORTJ, 4)

/* CFG5_M3 */
#define set_CFG5_M3 set_io(PORTJ, 7)
#define clr_CFG5_M3 clear_io(PORTJ, 7)
#define tgl_CFG5_M3 toggle_io(PORTJ, 7)
#define read_CFG5_M3 read_io(PORTJ, 7)

/* CFG6_M3 */
#define set_CFG6_M3 set_io(PORTK, 3)
#define clr_CFG6_M3 clear_io(PORTK, 3)
#define tgl_CFG6_M3 toggle_io(PORTK, 3)
#define read_CFG6_M3 read_io(PORTK, 3)

/* CFG7_M3 */
#define set_CFG7_M3 set_io(PORTK, 4)
#define clr_CFG7_M3 clear_io(PORTK, 4)
#define tgl_CFG7_M3 toggle_io(PORTK, 4)
#define read_CFG7_M3 read_io(PORTK, 4)

/* DRIVE_ENABLE_M3 */
#define set_DRIVE_ENABLE_M3 clear_io(PORTF, 7)
#define clr_DRIVE_ENABLE_M3 set_io(PORTF, 7)
#define tgl_DRIVE_ENABLE_M3 toggle_io(PORTF, 7)
#define read_DRIVE_ENABLE_M3 read_io(PORTF, 7)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_ENABLE_MOTORS;
	uint8_t REG_DISABLE_MOTORS;
	uint8_t REG_ENABLE_ENCODERS;
	uint8_t REG_DISABLE_ENCODERS;
	uint8_t REG_ENABLE_INPUTS;
	uint8_t REG_DISABLE_INPUTS;
	uint8_t REG_MOTOR0_OPERATION_MODE;
	uint8_t REG_MOTOR1_OPERATION_MODE;
	uint8_t REG_MOTOR2_OPERATION_MODE;
	uint8_t REG_MOTOR3_OPERATION_MODE;
	uint8_t REG_MOTOR0_MICROSTEP_RESOLUTION;
	uint8_t REG_MOTOR1_MICROSTEP_RESOLUTION;
	uint8_t REG_MOTOR2_MICROSTEP_RESOLUTION;
	uint8_t REG_MOTOR3_MICROSTEP_RESOLUTION;
	float REG_MOTOR0_MAXIMUM_CURRENT_RMS;
	float REG_MOTOR1_MAXIMUM_CURRENT_RMS;
	float REG_MOTOR2_MAXIMUM_CURRENT_RMS;
	float REG_MOTOR3_MAXIMUM_CURRENT_RMS;
	uint8_t REG_MOTOR0_HOLD_CURRENT_REDUCTION;
	uint8_t REG_MOTOR1_HOLD_CURRENT_REDUCTION;
	uint8_t REG_MOTOR2_HOLD_CURRENT_REDUCTION;
	uint8_t REG_MOTOR3_HOLD_CURRENT_REDUCTION;
	uint16_t REG_MOTOR0_NOMINAL_STEP_INTERVAL;
	uint16_t REG_MOTOR1_NOMINAL_STEP_INTERVAL;
	uint16_t REG_MOTOR2_NOMINAL_STEP_INTERVAL;
	uint16_t REG_MOTOR3_NOMINAL_STEP_INTERVAL;
	uint16_t REG_MOTOR0_MAXIMUM_STEP_INTERVAL;
	uint16_t REG_MOTOR1_MAXIMUM_STEP_INTERVAL;
	uint16_t REG_MOTOR2_MAXIMUM_STEP_INTERVAL;
	uint16_t REG_MOTOR3_MAXIMUM_STEP_INTERVAL;
	uint16_t REG_MOTOR0_STEP_ACCELERATION_INTERVAL;
	uint16_t REG_MOTOR1_STEP_ACCELERATION_INTERVAL;
	uint16_t REG_MOTOR2_STEP_ACCELERATION_INTERVAL;
	uint16_t REG_MOTOR3_STEP_ACCELERATION_INTERVAL;
	uint8_t REG_ENCODERS_MODE;
	uint8_t REG_ENCODERS_UPDATE_RATE;
	uint8_t REG_INPUT0_OPERATION_MODE;
	uint8_t REG_INPUT1_OPERATION_MODE;
	uint8_t REG_INPUT2_OPERATION_MODE;
	uint8_t REG_INPUT3_OPERATION_MODE;
	uint8_t REG_EMERGENCY_DETECTION_MODE;
	uint8_t REG_ACCUMULATED_STEPS_UPDATE_RATE;
	uint8_t REG_MOTORS_STOPPED;
	uint8_t REG_MOTORS_OVERVOLTAGE_DETECTION;
	uint8_t REG_MOTORS_ERROR_DETECTTION;
	int16_t REG_ENCODERS[3];
	uint8_t REG_DIGITAL_INPUTS_STATE;
	uint8_t REG_EMERGENCY_DETECTION;
	int32_t REG_MOTORS_STEPS[4];
	int32_t REG_MOTOR0_STEPS;
	int32_t REG_MOTOR1_STEPS;
	int32_t REG_MOTOR2_STEPS;
	int32_t REG_MOTOR3_STEPS;
	int32_t REG_MOTORS_ABSOLUTE_STEPS[4];
	int32_t REG_MOTOR0_ABSOLUTE_STEPS;
	int32_t REG_MOTOR1_ABSOLUTE_STEPS;
	int32_t REG_MOTOR2_ABSOLUTE_STEPS;
	int32_t REG_MOTOR3_ABSOLUTE_STEPS;
	int32_t REG_ACCUMULATED_STEPS[4];
	int32_t REG_MOTORS_MAX_STEPS_INTEGRATION[4];
	int32_t REG_MOTOR0_MAX_STEPS_INTEGRATION;
	int32_t REG_MOTOR1_MAX_STEPS_INTEGRATION;
	int32_t REG_MOTOR2_MAX_STEPS_INTEGRATION;
	int32_t REG_MOTOR3_MAX_STEPS_INTEGRATION;
	int32_t REG_MOTORS_MIN_STEPS_INTEGRATION[4];
	int32_t REG_MOTOR0_MIN_STEPS_INTEGRATION;
	int32_t REG_MOTOR1_MIN_STEPS_INTEGRATION;
	int32_t REG_MOTOR2_MIN_STEPS_INTEGRATION;
	int32_t REG_MOTOR3_MIN_STEPS_INTEGRATION;
	int32_t REG_MOTORS_IMMEDIATE_STEPS[4];
	int32_t REG_MOTOR0_IMMEDIATE_STEPS;
	int32_t REG_MOTOR1_IMMEDIATE_STEPS;
	int32_t REG_MOTOR2_IMMEDIATE_STEPS;
	int32_t REG_MOTOR3_IMMEDIATE_STEPS;
	uint8_t REG_STOP_MOTORS_SUDENTLY;
	uint8_t REG_RESET_MOTORS_ERROR_DETECTION;
	uint8_t REG_RESET_ENCODERS;
	uint8_t REG_RESERVED0;
	uint8_t REG_RESERVED1;
	uint8_t REG_RESERVED2;
	uint8_t REG_RESERVED3;
	uint8_t REG_RESERVED4;
	uint8_t REG_RESERVED5;
	uint8_t REG_RESERVED6;
	uint8_t REG_RESERVED7;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_ENABLE_MOTORS               32 // U8     Specifies the motors to enable in the device.
#define ADD_REG_DISABLE_MOTORS              33 // U8     Specifies the motors to disable in the device.
#define ADD_REG_ENABLE_ENCODERS             34 // U8     Specifies the port quadrature counters to enable in the device.
#define ADD_REG_DISABLE_ENCODERS            35 // U8     Specifies the port quadrature counters to disable in the device.
#define ADD_REG_ENABLE_INPUTS               36 // U8     Specifies the digital inputs to enable in the device.
#define ADD_REG_DISABLE_INPUTS              37 // U8     Specifies the digital inputs to disable in the device.
#define ADD_REG_MOTOR0_OPERATION_MODE       38 // U8     Configures the operation mode for motor 0.
#define ADD_REG_MOTOR1_OPERATION_MODE       39 // U8     Configures the operation mode for motor 1.
#define ADD_REG_MOTOR2_OPERATION_MODE       40 // U8     Configures the operation mode for motor 2.
#define ADD_REG_MOTOR3_OPERATION_MODE       41 // U8     Configures the operation mode for motor 3.
#define ADD_REG_MOTOR0_MICROSTEP_RESOLUTION 42 // U8     Configures the microstep resolution for motor 0.
#define ADD_REG_MOTOR1_MICROSTEP_RESOLUTION 43 // U8     Configures the microstep resolution for motor 1.
#define ADD_REG_MOTOR2_MICROSTEP_RESOLUTION 44 // U8     Configures the microstep resolution for motor 2.
#define ADD_REG_MOTOR3_MICROSTEP_RESOLUTION 45 // U8     Configures the microstep resolution for motor 3.
#define ADD_REG_MOTOR0_MAXIMUM_CURRENT_RMS  46 // FLOAT  Configures the maximum RMS current per phase for motor 0.
#define ADD_REG_MOTOR1_MAXIMUM_CURRENT_RMS  47 // FLOAT  Configures the maximum RMS current per phase for motor 1.
#define ADD_REG_MOTOR2_MAXIMUM_CURRENT_RMS  48 // FLOAT  Configures the maximum RMS current per phase for motor 2.
#define ADD_REG_MOTOR3_MAXIMUM_CURRENT_RMS  49 // FLOAT  Configures the maximum RMS current per phase for motor 3.
#define ADD_REG_MOTOR0_HOLD_CURRENT_REDUCTION 50 // U8     Configures the hold current reduction for motor 0.
#define ADD_REG_MOTOR1_HOLD_CURRENT_REDUCTION 51 // U8     Configures the hold current reduction for motor 1.
#define ADD_REG_MOTOR2_HOLD_CURRENT_REDUCTION 52 // U8     Configures the hold current reduction for motor 2.
#define ADD_REG_MOTOR3_HOLD_CURRENT_REDUCTION 53 // U8     Configures the hold current reduction for motor 3.
#define ADD_REG_MOTOR0_NOMINAL_STEP_INTERVAL 54 // U16    Configures the motor's step interval when running at nominal speed for motor 0.
#define ADD_REG_MOTOR1_NOMINAL_STEP_INTERVAL 55 // U16    Configures the motor's step interval when running at nominal speed for motor 1.
#define ADD_REG_MOTOR2_NOMINAL_STEP_INTERVAL 56 // U16    Configures the motor's step interval when running at nominal speed for motor 2.
#define ADD_REG_MOTOR3_NOMINAL_STEP_INTERVAL 57 // U16    Configures the motor's step interval when running at nominal speed for motor 3.
#define ADD_REG_MOTOR0_MAXIMUM_STEP_INTERVAL 58 // U16    Configures the motor's maximum step interval for motor 0, used as the first and last steo interval of a movement.
#define ADD_REG_MOTOR1_MAXIMUM_STEP_INTERVAL 59 // U16    Configures the motor's maximum step interval for motor 1, used as the first and last steo interval of a movement.
#define ADD_REG_MOTOR2_MAXIMUM_STEP_INTERVAL 60 // U16    Configures the motor's maximum step interval for motor 2, used as the first and last steo interval of a movement.
#define ADD_REG_MOTOR3_MAXIMUM_STEP_INTERVAL 61 // U16    Configures the motor's maximum step interval for motor 3, used as the first and last steo interval of a movement.
#define ADD_REG_MOTOR0_STEP_ACCELERATION_INTERVAL 62 // U16    Configures the acceleration for motor 0. The step's interval is decreased by this value when accelerating and increased when decelerating.
#define ADD_REG_MOTOR1_STEP_ACCELERATION_INTERVAL 63 // U16    Configures the acceleration for motor 1. The step's interval is decreased by this value when accelerating and increased when decelerating.
#define ADD_REG_MOTOR2_STEP_ACCELERATION_INTERVAL 64 // U16    Configures the acceleration for motor 2. The step's interval is decreased by this value when accelerating and increased when decelerating.
#define ADD_REG_MOTOR3_STEP_ACCELERATION_INTERVAL 65 // U16    Configures the acceleration for motor 3. The step's interval is decreased by this value when accelerating and increased when decelerating.
#define ADD_REG_ENCODERS_MODE               66 // U8     Configures the operation mode of the quadrature encoders.
#define ADD_REG_ENCODERS_UPDATE_RATE        67 // U8     Configures the reading rate of the encoders' event.
#define ADD_REG_INPUT0_OPERATION_MODE       68 // U8     Configures the operation mode for digital input 0.
#define ADD_REG_INPUT1_OPERATION_MODE       69 // U8     Configures the operation mode for digital input 1.
#define ADD_REG_INPUT2_OPERATION_MODE       70 // U8     Configures the operation mode for digital input 2.
#define ADD_REG_INPUT3_OPERATION_MODE       71 // U8     Configures the operation mode for digital input 3.
#define ADD_REG_EMERGENCY_DETECTION_MODE    72 // U8     Configures the edge detection mode for the emergency external button.
#define ADD_REG_ACCUMULATED_STEPS_UPDATE_RATE 73 // U8     Configures the reading rate of the accumulated steps event.
#define ADD_REG_MOTORS_STOPPED              74 // U8     
#define ADD_REG_MOTORS_OVERVOLTAGE_DETECTION 75 // U8     
#define ADD_REG_MOTORS_ERROR_DETECTTION     76 // U8     
#define ADD_REG_ENCODERS                    77 // I16    
#define ADD_REG_DIGITAL_INPUTS_STATE        78 // U8     
#define ADD_REG_EMERGENCY_DETECTION         79 // U8     
#define ADD_REG_MOTORS_STEPS                80 // I32    Moves all motors by the number of steps written in this array register and set the direction according to the value's signal.
#define ADD_REG_MOTOR0_STEPS                81 // I32    Moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
#define ADD_REG_MOTOR1_STEPS                82 // I32    Moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
#define ADD_REG_MOTOR2_STEPS                83 // I32    Moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
#define ADD_REG_MOTOR3_STEPS                84 // I32    Moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
#define ADD_REG_MOTORS_ABSOLUTE_STEPS       85 // I32    Moves all motors to the absolute position written in this array register.
#define ADD_REG_MOTOR0_ABSOLUTE_STEPS       86 // I32    Moves motor 0 to the absolute position written in this register.
#define ADD_REG_MOTOR1_ABSOLUTE_STEPS       87 // I32    Moves motor 1 to the absolute position written in this register.
#define ADD_REG_MOTOR2_ABSOLUTE_STEPS       88 // I32    Moves motor 2 to the absolute position written in this register.
#define ADD_REG_MOTOR3_ABSOLUTE_STEPS       89 // I32    Moves motor 3 to the absolute position written in this register.
#define ADD_REG_ACCUMULATED_STEPS           90 // I32    Contains the accumulated steps of all motors.
#define ADD_REG_MOTORS_MAX_STEPS_INTEGRATION 91 // I32    Defines the limit of the accumulated steps for the positive movement of all motors.. The device will not let the motors move further than this value.
#define ADD_REG_MOTOR0_MAX_STEPS_INTEGRATION 92 // I32    Defines the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR1_MAX_STEPS_INTEGRATION 93 // I32    Defines the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR2_MAX_STEPS_INTEGRATION 94 // I32    Defines the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR3_MAX_STEPS_INTEGRATION 95 // I32    Defines the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value.
#define ADD_REG_MOTORS_MIN_STEPS_INTEGRATION 96 // I32    Defines the limit of the accumulated steps for the negative movement of all motors. The device will not let the motors move further than this value.
#define ADD_REG_MOTOR0_MIN_STEPS_INTEGRATION 97 // I32    Defines the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR1_MIN_STEPS_INTEGRATION 98 // I32    Defines the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR2_MIN_STEPS_INTEGRATION 99 // I32    Defines the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value.
#define ADD_REG_MOTOR3_MIN_STEPS_INTEGRATION100 // I32    Defines the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value.
#define ADD_REG_MOTORS_IMMEDIATE_STEPS     101 // I32    Starts the movement of all motors with the step interval defined by this array. The value's signal defines the direction.
#define ADD_REG_MOTOR0_IMMEDIATE_STEPS     102 // I32    Starts the movement of motor 0 with the step interval defined by this register. The value's signal defines the direction.
#define ADD_REG_MOTOR1_IMMEDIATE_STEPS     103 // I32    Starts the movement of motor 1 with the step interval defined by this register. The value's signal defines the direction.
#define ADD_REG_MOTOR2_IMMEDIATE_STEPS     104 // I32    Starts the movement of motor 2 with the step interval defined by this register. The value's signal defines the direction.
#define ADD_REG_MOTOR3_IMMEDIATE_STEPS     105 // I32    Starts the movement of motor 3 with the step interval defined by this register. The value's signal defines the direction.
#define ADD_REG_STOP_MOTORS_SUDENTLY       106 // U8     Stops the motors immediately.
#define ADD_REG_RESET_MOTORS_ERROR_DETECTION107 // U8     Disables the current error and enables the driver.
#define ADD_REG_RESET_ENCODERS             108 // U8     Resets the encoder.
#define ADD_REG_RESERVED0                  109 // U8     Contains the CFG configuration pins of the TMC2210 driver that controls motor 0.
#define ADD_REG_RESERVED1                  110 // U8     Contains the CFG configuration pins of the TMC2210 driver that controls motor 1.
#define ADD_REG_RESERVED2                  111 // U8     Contains the CFG configuration pins of the TMC2210 driver that controls motor 2.
#define ADD_REG_RESERVED3                  112 // U8     Contains the CFG configuration pins of the TMC2210 driver that controls motor 3.
#define ADD_REG_RESERVED4                  113 // U8     Contains the raw data of the digital potentiometer that controls current limit of motor 0.
#define ADD_REG_RESERVED5                  114 // U8     Contains the raw data of the digital potentiometer that controls current limit of motor 1.
#define ADD_REG_RESERVED6                  115 // U8     Contains the raw data of the digital potentiometer that controls current limit of motor 2.
#define ADD_REG_RESERVED7                  116 // U8     Contains the raw data of the digital potentiometer that controls current limit of motor 3.

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x74
#define APP_NBYTES_OF_REG_BANK              264

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_MOTOR0                           (1<<0)       // Index of motor 0
#define B_MOTOR1                           (1<<1)       // Index of motor 1
#define B_MOTOR2                           (1<<2)       // Index of motor 2
#define B_MOTOR3                           (1<<3)       // Index of motor 3
#define B_ENCODER0                         (1<<0)       // Index of encoder 0
#define B_ENCODER1                         (1<<1)       // Index of encoder 1
#define B_ENCODER2                         (1<<2)       // Index of encoder 2
#define B_INPUT0                           (1<<0)       // Index of input 0
#define B_INPUT1                           (1<<1)       // Index of input 1
#define B_INPUT2                           (1<<2)       // Index of input 2
#define B_INPUT3                           (1<<3)       // Index of input 3
#define GM_QUIET_MODE                      0x00         // Specifies the motor operation mode to QuietMode
#define GM_DYNAMIC_MOVEMENTS               0x01         // Specifies the motor operation mode to DynamicMovements
#define GM_MICROSTEPS_8                    0x00         // Specifies the inputs operation mode to 8 microsteps
#define GM_MICROSTEPS_16                   0x01         // Specifies the inputs operation mode to 16 microsteps
#define GM_MICROSTEPS_32                   0x02         // Specifies the inputs operation mode to 32 microsteps
#define GM_MICROSTEPS_64                   0x03         // Specifies the inputs operation mode to 64 microsteps
#define GM_REDUCTION_TO_50PCT              0x00         // Hold current reduction to 50 %
#define GM_REDUCTION_TO_25PCT              0x01         // Hold current reduction to 25 %
#define GM_REDUCTION_TO_12PCT              0x02         // Hold current reduction to 12.5 %
#define GM_NO_REDUCTION                    0x03         // No hold current reduction.
#define GM_POSITION                        0x00         // Specifies the quadrature encoders reading to be Position
#define GM_DISPLACEMENT                    0x01         // Specifies the quadrature encoders reading to be Displacement
#define GM_RATE_100HZ                      0x00         // Specifies the quadrature encoders update rate to 100 Hz
#define GM_RATE_200HZ                      0x01         // Specifies the quadrature encoders update rate to 200 Hz
#define GM_RATE_250HZ                      0x02         // Specifies the quadrature encoders update rate to 250 Hz
#define GM_RATE_500HZ                      0x03         // Specifies the quadrature encoders update rate to 500 Hz
#define GM_EVENT_ONLY                      0x00         // Specifies the inputs operation mode to send event only
#define GM_STOP_MOTOR0_ON_RISING           0x10         // Specifies the inputs operation mode to stop motor 0 on digital rasing
#define GM_STOP_MOTOR1_ON_RISING           0x11         // Specifies the inputs operation mode to stop motor 1 on digital rasing
#define GM_STOP_MOTOR2_ON_RISING           0x12         // Specifies the inputs operation mode to stop motor 2 on digital rasing
#define GM_STOP_MOTOR3_ON_RISING           0x13         // Specifies the inputs operation mode to stop motor 3 on digital rasing
#define GM_STOP_MOTOR0_ON_FALLING          0x20         // Specifies the inputs operation mode to stop motor 0 on digital falling
#define GM_STOP_MOTOR1_ON_FALLING          0x21         // Specifies the inputs operation mode to stop motor 1 on digital falling
#define GM_STOP_MOTOR2_ON_FALLING          0x22         // Specifies the inputs operation mode to stop motor 2 on digital falling
#define GM_STOP_MOTOR3_ON_FALLING          0x23         // Specifies the inputs operation mode to stop motor 3 on digital falling
#define GM_CLOSED                          0x00         // 
#define GM_OPEN                            0x01         // 
#define GM_AS_DISABLED                     0x00         // Disables the accumulated steps events
#define GM_AS_RATE_10HZ                    0x01         // Specifies the accumulated steps update rate to 10 Hz
#define GM_AS_RATE_50HZ                    0x02         // Specifies the accumulated steps update rate to 50 Hz
#define GM_AS_RATE_100HZ                   0x03         // Specifies the accumulated steps update rate to 100 Hz
#define GM_DISABLED                        0x00         // 
#define GM_ENABLED                         0x01         // 

#endif /* _APP_REGS_H_ */