#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_ENABLE_MOTORS(void);
void app_read_REG_DISABLE_MOTORS(void);
void app_read_REG_ENABLE_ENCODERS(void);
void app_read_REG_DISABLE_ENCODERS(void);
void app_read_REG_ENABLE_INPUTS(void);
void app_read_REG_DISABLE_INPUTS(void);
void app_read_REG_MOTOR0_OPERATION_MODE(void);
void app_read_REG_MOTOR1_OPERATION_MODE(void);
void app_read_REG_MOTOR2_OPERATION_MODE(void);
void app_read_REG_MOTOR3_OPERATION_MODE(void);
void app_read_REG_MOTOR0_MICROSTEP_RESOLUTION(void);
void app_read_REG_MOTOR1_MICROSTEP_RESOLUTION(void);
void app_read_REG_MOTOR2_MICROSTEP_RESOLUTION(void);
void app_read_REG_MOTOR3_MICROSTEP_RESOLUTION(void);
void app_read_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void);
void app_read_REG_MOTOR1_MAXIMUM_CURRENT_RMS(void);
void app_read_REG_MOTOR2_MAXIMUM_CURRENT_RMS(void);
void app_read_REG_MOTOR3_MAXIMUM_CURRENT_RMS(void);
void app_read_REG_MOTOR0_HOLD_CURRENT_REDUCTION(void);
void app_read_REG_MOTOR1_HOLD_CURRENT_REDUCTION(void);
void app_read_REG_MOTOR2_HOLD_CURRENT_REDUCTION(void);
void app_read_REG_MOTOR3_HOLD_CURRENT_REDUCTION(void);
void app_read_REG_MOTOR0_NOMINAL_STEP_INTERVAL(void);
void app_read_REG_MOTOR1_NOMINAL_STEP_INTERVAL(void);
void app_read_REG_MOTOR2_NOMINAL_STEP_INTERVAL(void);
void app_read_REG_MOTOR3_NOMINAL_STEP_INTERVAL(void);
void app_read_REG_MOTOR0_MAXIMUM_STEP_INTERVAL(void);
void app_read_REG_MOTOR1_MAXIMUM_STEP_INTERVAL(void);
void app_read_REG_MOTOR2_MAXIMUM_STEP_INTERVAL(void);
void app_read_REG_MOTOR3_MAXIMUM_STEP_INTERVAL(void);
void app_read_REG_MOTOR0_STEP_ACCELERATION_INTERVAL(void);
void app_read_REG_MOTOR1_STEP_ACCELERATION_INTERVAL(void);
void app_read_REG_MOTOR2_STEP_ACCELERATION_INTERVAL(void);
void app_read_REG_MOTOR3_STEP_ACCELERATION_INTERVAL(void);
void app_read_REG_ENCODERS_MODE(void);
void app_read_REG_ENCODERS_UPDATE_RATE(void);
void app_read_REG_INPUT0_OPERATION_MODE(void);
void app_read_REG_INPUT1_OPERATION_MODE(void);
void app_read_REG_INPUT2_OPERATION_MODE(void);
void app_read_REG_INPUT3_OPERATION_MODE(void);
void app_read_REG_EMERGENCY_DETECTION_MODE(void);
void app_read_REG_ACCUMULATED_STEPS_UPDATE_RATE(void);
void app_read_REG_MOTORS_STOPPED(void);
void app_read_REG_MOTORS_OVERVOLTAGE_DETECTION(void);
void app_read_REG_MOTORS_ERROR_DETECTTION(void);
void app_read_REG_ENCODERS(void);
void app_read_REG_DIGITAL_INPUTS_STATE(void);
void app_read_REG_EMERGENCY_DETECTION(void);
void app_read_REG_MOTORS_STEPS(void);
void app_read_REG_MOTOR0_STEPS(void);
void app_read_REG_MOTOR1_STEPS(void);
void app_read_REG_MOTOR2_STEPS(void);
void app_read_REG_MOTOR3_STEPS(void);
void app_read_REG_MOTORS_ABSOLUTE_STEPS(void);
void app_read_REG_MOTOR0_ABSOLUTE_STEPS(void);
void app_read_REG_MOTOR1_ABSOLUTE_STEPS(void);
void app_read_REG_MOTOR2_ABSOLUTE_STEPS(void);
void app_read_REG_MOTOR3_ABSOLUTE_STEPS(void);
void app_read_REG_ACCUMULATED_STEPS(void);
void app_read_REG_MOTOR0_ACCUMULATED_STEPS(void);
void app_read_REG_MOTOR1_ACCUMULATED_STEPS(void);
void app_read_REG_MOTOR2_ACCUMULATED_STEPS(void);
void app_read_REG_MOTOR3_ACCUMULATED_STEPS(void);
void app_read_REG_MOTORS_MAX_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR0_MAX_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR1_MAX_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR2_MAX_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR3_MAX_STEPS_INTEGRATION(void);
void app_read_REG_MOTORS_MIN_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR0_MIN_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR1_MIN_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR2_MIN_STEPS_INTEGRATION(void);
void app_read_REG_MOTOR3_MIN_STEPS_INTEGRATION(void);
void app_read_REG_MOTORS_IMMEDIATE_STEPS(void);
void app_read_REG_MOTOR0_IMMEDIATE_STEPS(void);
void app_read_REG_MOTOR1_IMMEDIATE_STEPS(void);
void app_read_REG_MOTOR2_IMMEDIATE_STEPS(void);
void app_read_REG_MOTOR3_IMMEDIATE_STEPS(void);
void app_read_REG_STOP_MOTORS_SUDENTLY(void);
void app_read_REG_RESET_MOTORS_ERROR_DETECTION(void);
void app_read_REG_RESET_ENCODERS(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_RESERVED1(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_RESERVED6(void);
void app_read_REG_RESERVED7(void);
void app_read_REG_RESERVED8(void);
void app_read_REG_RESERVED9(void);
void app_read_REG_RESERVED10(void);
void app_read_REG_RESERVED11(void);
void app_read_REG_RESERVED12(void);
void app_read_REG_RESERVED13(void);
void app_read_REG_RESERVED14(void);
void app_read_REG_RESERVED15(void);
void app_read_REG_RESERVED16(void);
void app_read_REG_START_QUICK_MOVEMENT(void);
void app_read_REG_MOTOR1_QUICK_PULSE_DISTANCE(void);
void app_read_REG_MOTOR2_QUICK_PULSE_DISTANCE(void);
void app_read_REG_MOTOR1_QUICK_NOMINAL_SPEED(void);
void app_read_REG_MOTOR2_QUICK_NOMINAL_SPEED(void);
void app_read_REG_MOTOR1_QUICK_START_SPEED(void);
void app_read_REG_MOTOR2_QUICK_START_SPEED(void);
void app_read_REG_MOTOR1_QUICK_ACCELERATION(void);
void app_read_REG_MOTOR2_QUICK_ACCELERATION(void);
void app_read_REG_MOTOR1_QUICK_DISTANCE(void);
void app_read_REG_MOTOR2_QUICK_DISTANCE(void);

bool app_write_REG_ENABLE_MOTORS(void *a);
bool app_write_REG_DISABLE_MOTORS(void *a);
bool app_write_REG_ENABLE_ENCODERS(void *a);
bool app_write_REG_DISABLE_ENCODERS(void *a);
bool app_write_REG_ENABLE_INPUTS(void *a);
bool app_write_REG_DISABLE_INPUTS(void *a);
bool app_write_REG_MOTOR0_OPERATION_MODE(void *a);
bool app_write_REG_MOTOR1_OPERATION_MODE(void *a);
bool app_write_REG_MOTOR2_OPERATION_MODE(void *a);
bool app_write_REG_MOTOR3_OPERATION_MODE(void *a);
bool app_write_REG_MOTOR0_MICROSTEP_RESOLUTION(void *a);
bool app_write_REG_MOTOR1_MICROSTEP_RESOLUTION(void *a);
bool app_write_REG_MOTOR2_MICROSTEP_RESOLUTION(void *a);
bool app_write_REG_MOTOR3_MICROSTEP_RESOLUTION(void *a);
bool app_write_REG_MOTOR0_MAXIMUM_CURRENT_RMS(void *a);
bool app_write_REG_MOTOR1_MAXIMUM_CURRENT_RMS(void *a);
bool app_write_REG_MOTOR2_MAXIMUM_CURRENT_RMS(void *a);
bool app_write_REG_MOTOR3_MAXIMUM_CURRENT_RMS(void *a);
bool app_write_REG_MOTOR0_HOLD_CURRENT_REDUCTION(void *a);
bool app_write_REG_MOTOR1_HOLD_CURRENT_REDUCTION(void *a);
bool app_write_REG_MOTOR2_HOLD_CURRENT_REDUCTION(void *a);
bool app_write_REG_MOTOR3_HOLD_CURRENT_REDUCTION(void *a);
bool app_write_REG_MOTOR0_NOMINAL_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR1_NOMINAL_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR2_NOMINAL_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR3_NOMINAL_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR0_MAXIMUM_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR1_MAXIMUM_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR2_MAXIMUM_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR3_MAXIMUM_STEP_INTERVAL(void *a);
bool app_write_REG_MOTOR0_STEP_ACCELERATION_INTERVAL(void *a);
bool app_write_REG_MOTOR1_STEP_ACCELERATION_INTERVAL(void *a);
bool app_write_REG_MOTOR2_STEP_ACCELERATION_INTERVAL(void *a);
bool app_write_REG_MOTOR3_STEP_ACCELERATION_INTERVAL(void *a);
bool app_write_REG_ENCODERS_MODE(void *a);
bool app_write_REG_ENCODERS_UPDATE_RATE(void *a);
bool app_write_REG_INPUT0_OPERATION_MODE(void *a);
bool app_write_REG_INPUT1_OPERATION_MODE(void *a);
bool app_write_REG_INPUT2_OPERATION_MODE(void *a);
bool app_write_REG_INPUT3_OPERATION_MODE(void *a);
bool app_write_REG_EMERGENCY_DETECTION_MODE(void *a);
bool app_write_REG_ACCUMULATED_STEPS_UPDATE_RATE(void *a);
bool app_write_REG_MOTORS_STOPPED(void *a);
bool app_write_REG_MOTORS_OVERVOLTAGE_DETECTION(void *a);
bool app_write_REG_MOTORS_ERROR_DETECTTION(void *a);
bool app_write_REG_ENCODERS(void *a);
bool app_write_REG_DIGITAL_INPUTS_STATE(void *a);
bool app_write_REG_EMERGENCY_DETECTION(void *a);
bool app_write_REG_MOTORS_STEPS(void *a);
bool app_write_REG_MOTOR0_STEPS(void *a);
bool app_write_REG_MOTOR1_STEPS(void *a);
bool app_write_REG_MOTOR2_STEPS(void *a);
bool app_write_REG_MOTOR3_STEPS(void *a);
bool app_write_REG_MOTORS_ABSOLUTE_STEPS(void *a);
bool app_write_REG_MOTOR0_ABSOLUTE_STEPS(void *a);
bool app_write_REG_MOTOR1_ABSOLUTE_STEPS(void *a);
bool app_write_REG_MOTOR2_ABSOLUTE_STEPS(void *a);
bool app_write_REG_MOTOR3_ABSOLUTE_STEPS(void *a);
bool app_write_REG_ACCUMULATED_STEPS(void *a);
bool app_write_REG_MOTOR0_ACCUMULATED_STEPS(void *a);
bool app_write_REG_MOTOR1_ACCUMULATED_STEPS(void *a);
bool app_write_REG_MOTOR2_ACCUMULATED_STEPS(void *a);
bool app_write_REG_MOTOR3_ACCUMULATED_STEPS(void *a);
bool app_write_REG_MOTORS_MAX_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR0_MAX_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR1_MAX_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR2_MAX_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR3_MAX_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTORS_MIN_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR0_MIN_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR1_MIN_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR2_MIN_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTOR3_MIN_STEPS_INTEGRATION(void *a);
bool app_write_REG_MOTORS_IMMEDIATE_STEPS(void *a);
bool app_write_REG_MOTOR0_IMMEDIATE_STEPS(void *a);
bool app_write_REG_MOTOR1_IMMEDIATE_STEPS(void *a);
bool app_write_REG_MOTOR2_IMMEDIATE_STEPS(void *a);
bool app_write_REG_MOTOR3_IMMEDIATE_STEPS(void *a);
bool app_write_REG_STOP_MOTORS_SUDENTLY(void *a);
bool app_write_REG_RESET_MOTORS_ERROR_DETECTION(void *a);
bool app_write_REG_RESET_ENCODERS(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_RESERVED1(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_RESERVED6(void *a);
bool app_write_REG_RESERVED7(void *a);
bool app_write_REG_RESERVED8(void *a);
bool app_write_REG_RESERVED9(void *a);
bool app_write_REG_RESERVED10(void *a);
bool app_write_REG_RESERVED11(void *a);
bool app_write_REG_RESERVED12(void *a);
bool app_write_REG_RESERVED13(void *a);
bool app_write_REG_RESERVED14(void *a);
bool app_write_REG_RESERVED15(void *a);
bool app_write_REG_RESERVED16(void *a);
bool app_write_REG_START_QUICK_MOVEMENT(void *a);
bool app_write_REG_MOTOR1_QUICK_PULSE_DISTANCE(void *a);
bool app_write_REG_MOTOR2_QUICK_PULSE_DISTANCE(void *a);
bool app_write_REG_MOTOR1_QUICK_NOMINAL_SPEED(void *a);
bool app_write_REG_MOTOR2_QUICK_NOMINAL_SPEED(void *a);
bool app_write_REG_MOTOR1_QUICK_START_SPEED(void *a);
bool app_write_REG_MOTOR2_QUICK_START_SPEED(void *a);
bool app_write_REG_MOTOR1_QUICK_ACCELERATION(void *a);
bool app_write_REG_MOTOR2_QUICK_ACCELERATION(void *a);
bool app_write_REG_MOTOR1_QUICK_DISTANCE(void *a);
bool app_write_REG_MOTOR2_QUICK_DISTANCE(void *a);


#endif /* _APP_FUNCTIONS_H_ */