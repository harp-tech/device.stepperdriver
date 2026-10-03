---
uid: Harp.StepperDriver.Device
---

<table>
  <thead>
    <tr><th colspan="2">StepperDriver</th></tr>
  </thead>
  <tbody>
    <tr><td>whoAmI</td><td>1130</td></tr>
    <tr><td>firmwareVersion</td><td>0.9</td></tr>
    <tr><td>hardwareTargets</td><td>1.1</td></tr>
  </tbody>
</table>

### Registers

| name | address | type | length | access | description | range | interfaceType |
|-|-|-|-|-|-|-|-|
| [EnableDriver](xref:Harp.StepperDriver.EnableDriver) | 32 | U8 |  | Write | Enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled. |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [DisableDriver](xref:Harp.StepperDriver.DisableDriver) | 33 | U8 |  | Write | Disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled. |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [EnableEncoders](xref:Harp.StepperDriver.EnableEncoders) | 34 | U8 |  | Write | Specifies a set of port quadrature counters to enable in the device. |  | [QuadratureEncoders](xref:Harp.StepperDriver.QuadratureEncoders) |
| [DisableEncoders](xref:Harp.StepperDriver.DisableEncoders) | 35 | U8 |  | Write | Specifies a set of port quadrature counters to disable in the device. |  | [QuadratureEncoders](xref:Harp.StepperDriver.QuadratureEncoders) |
| [EnableDigitalInputs](xref:Harp.StepperDriver.EnableDigitalInputs) | 36 | U8 |  | Write | Specifies a set of digital inputs to enable in the device. |  | [DigitalInputs](xref:Harp.StepperDriver.DigitalInputs) |
| [DisableDigitalInputs](xref:Harp.StepperDriver.DisableDigitalInputs) | 37 | U8 |  | Write | Specifies a set of digital inputs to disable in the device. |  | [DigitalInputs](xref:Harp.StepperDriver.DigitalInputs) |
| [Motor0OperationMode](xref:Harp.StepperDriver.Motor0OperationMode) | 38 | U8 |  | Write | Configures the operation mode for motor 0. |  | [MotorOperationMode](xref:Harp.StepperDriver.MotorOperationMode) |
| [Motor1OperationMode](xref:Harp.StepperDriver.Motor1OperationMode) | 39 | U8 |  | Write | Configures the operation mode for motor 1. |  | [MotorOperationMode](xref:Harp.StepperDriver.MotorOperationMode) |
| [Motor2OperationMode](xref:Harp.StepperDriver.Motor2OperationMode) | 40 | U8 |  | Write | Configures the operation mode for motor 2. |  | [MotorOperationMode](xref:Harp.StepperDriver.MotorOperationMode) |
| [Motor3OperationMode](xref:Harp.StepperDriver.Motor3OperationMode) | 41 | U8 |  | Write | Configures the operation mode for motor 3. |  | [MotorOperationMode](xref:Harp.StepperDriver.MotorOperationMode) |
| [Motor0MicrostepResolution](xref:Harp.StepperDriver.Motor0MicrostepResolution) | 42 | U8 |  | Write | Configures the microstep resolution for motor 0. |  | [MicrostepResolution](xref:Harp.StepperDriver.MicrostepResolution) |
| [Motor1MicrostepResolution](xref:Harp.StepperDriver.Motor1MicrostepResolution) | 43 | U8 |  | Write | Configures the microstep resolution for motor 1. |  | [MicrostepResolution](xref:Harp.StepperDriver.MicrostepResolution) |
| [Motor2MicrostepResolution](xref:Harp.StepperDriver.Motor2MicrostepResolution) | 44 | U8 |  | Write | Configures the microstep resolution for motor 2. |  | [MicrostepResolution](xref:Harp.StepperDriver.MicrostepResolution) |
| [Motor3MicrostepResolution](xref:Harp.StepperDriver.Motor3MicrostepResolution) | 45 | U8 |  | Write | Configures the microstep resolution for motor 3. |  | [MicrostepResolution](xref:Harp.StepperDriver.MicrostepResolution) |
| [Motor0MaximumRunCurrent](xref:Harp.StepperDriver.Motor0MaximumRunCurrent) | 46 | Float |  | Write | Configures the maximum run RMS current per phase for motor 0. | 0.2 [0.139:2.1] | |
| [Motor1MaximumRunCurrent](xref:Harp.StepperDriver.Motor1MaximumRunCurrent) | 47 | Float |  | Write | Configures the maximum run RMS current per phase for motor 1. | 0.2 [0.139:2.1] | |
| [Motor2MaximumRunCurrent](xref:Harp.StepperDriver.Motor2MaximumRunCurrent) | 48 | Float |  | Write | Configures the maximum run RMS current per phase for motor 2. | 0.2 [0.139:2.1] | |
| [Motor3MaximumRunCurrent](xref:Harp.StepperDriver.Motor3MaximumRunCurrent) | 49 | Float |  | Write | Configures the maximum run RMS current per phase for motor 3. | 0.2 [0.139:2.1] | |
| [Motor0HoldCurrentReduction](xref:Harp.StepperDriver.Motor0HoldCurrentReduction) | 50 | U8 |  | Write | Configures the hold current reduction for motor 0. |  | [HoldCurrentReduction](xref:Harp.StepperDriver.HoldCurrentReduction) |
| [Motor1HoldCurrentReduction](xref:Harp.StepperDriver.Motor1HoldCurrentReduction) | 51 | U8 |  | Write | Configures the hold current reduction for motor 1. |  | [HoldCurrentReduction](xref:Harp.StepperDriver.HoldCurrentReduction) |
| [Motor2HoldCurrentReduction](xref:Harp.StepperDriver.Motor2HoldCurrentReduction) | 52 | U8 |  | Write | Configures the hold current reduction for motor 2. |  | [HoldCurrentReduction](xref:Harp.StepperDriver.HoldCurrentReduction) |
| [Motor3HoldCurrentReduction](xref:Harp.StepperDriver.Motor3HoldCurrentReduction) | 53 | U8 |  | Write | Configures the hold current reduction for motor 3. |  | [HoldCurrentReduction](xref:Harp.StepperDriver.HoldCurrentReduction) |
| [Motor0StepInterval](xref:Harp.StepperDriver.Motor0StepInterval) | 54 | U16 |  | Write | Configures the time between step motor pulses (us) when running at nominal speed for motor 0. | 250 [100:20000] | |
| [Motor1StepInterval](xref:Harp.StepperDriver.Motor1StepInterval) | 55 | U16 |  | Write | Configures the time between step motor pulses (us) when running at nominal speed for motor 1. | 250 [100:20000] | |
| [Motor2StepInterval](xref:Harp.StepperDriver.Motor2StepInterval) | 56 | U16 |  | Write | Configures the time between step motor pulses (us) when running at nominal speed for motor 2. | 250 [100:20000] | |
| [Motor3StepInterval](xref:Harp.StepperDriver.Motor3StepInterval) | 57 | U16 |  | Write | Configures the time between step motor pulses (us) when running at nominal speed for motor 3. | 250 [100:20000] | |
| [Motor0MaximumStepInterval](xref:Harp.StepperDriver.Motor0MaximumStepInterval) | 58 | U16 |  | Write | Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0. | 2000 [100:20000] | |
| [Motor1MaximumStepInterval](xref:Harp.StepperDriver.Motor1MaximumStepInterval) | 59 | U16 |  | Write | Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1. | 2000 [100:20000] | |
| [Motor2MaximumStepInterval](xref:Harp.StepperDriver.Motor2MaximumStepInterval) | 60 | U16 |  | Write | Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2. | 2000 [100:20000] | |
| [Motor3MaximumStepInterval](xref:Harp.StepperDriver.Motor3MaximumStepInterval) | 61 | U16 |  | Write | Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3. | 2000 [100:20000] | |
| [Motor0StepAccelerationInterval](xref:Harp.StepperDriver.Motor0StepAccelerationInterval) | 62 | U16 |  | Write | Configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating. | 10 [2:2000] | |
| [Motor1StepAccelerationInterval](xref:Harp.StepperDriver.Motor1StepAccelerationInterval) | 63 | U16 |  | Write | Configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating. | 10 [2:2000] | |
| [Motor2StepAccelerationInterval](xref:Harp.StepperDriver.Motor2StepAccelerationInterval) | 64 | U16 |  | Write | Configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating. | 10 [2:2000] | |
| [Motor3StepAccelerationInterval](xref:Harp.StepperDriver.Motor3StepAccelerationInterval) | 65 | U16 |  | Write | Configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating. | 10 [2:2000] | |
| [EncoderMode](xref:Harp.StepperDriver.EncoderMode) | 66 | U8 |  | Write | Configures the operation mode of the quadrature encoder. |  | [EncoderModeConfig](xref:Harp.StepperDriver.EncoderModeConfig) |
| [EncoderSamplingRate](xref:Harp.StepperDriver.EncoderSamplingRate) | 67 | U8 |  | Write | Configures the sampling rate of the quadrature encoder event. |  | [EncoderSamplingRateConfig](xref:Harp.StepperDriver.EncoderSamplingRateConfig) |
| [Input0OpMode](xref:Harp.StepperDriver.Input0OpMode) | 68 | U8 |  | Write | Configures the operation mode for digital input 0. |  | [InputOpModeConfig](xref:Harp.StepperDriver.InputOpModeConfig) |
| [Input1OpMode](xref:Harp.StepperDriver.Input1OpMode) | 69 | U8 |  | Write | Configures the operation mode for digital input 1. |  | [InputOpModeConfig](xref:Harp.StepperDriver.InputOpModeConfig) |
| [Input2OpMode](xref:Harp.StepperDriver.Input2OpMode) | 70 | U8 |  | Write | Configures the operation mode for digital input 2. |  | [InputOpModeConfig](xref:Harp.StepperDriver.InputOpModeConfig) |
| [Input3OpMode](xref:Harp.StepperDriver.Input3OpMode) | 71 | U8 |  | Write | Configures the operation mode for digital input 3. |  | [InputOpModeConfig](xref:Harp.StepperDriver.InputOpModeConfig) |
| [InterlockEnabled](xref:Harp.StepperDriver.InterlockEnabled) | 72 | U8 |  | Write | Configures the external interlock connector state required for the device to be enabled. |  | [InterlockEnabledConfig](xref:Harp.StepperDriver.InterlockEnabledConfig) |
| [AccumulatedStepsSamplingRate](xref:Harp.StepperDriver.AccumulatedStepsSamplingRate) | 73 | U8 |  | Write | Configures the dispatch rate of the accumulated steps event. |  | [AccumulatedStepsSamplingRateConfig](xref:Harp.StepperDriver.AccumulatedStepsSamplingRateConfig) |
| [MotorStopped](xref:Harp.StepperDriver.MotorStopped) | 74 | U8 |  | Event | Emitted when any of the motors stops. |  | [MotorStoppedPayload](xref:Harp.StepperDriver.MotorStoppedPayload) |
| [MotorOverVoltageDetection](xref:Harp.StepperDriver.MotorOverVoltageDetection) | 75 | U8 |  | Event | Contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented). |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [MotorRaisedError](xref:Harp.StepperDriver.MotorRaisedError) | 76 | U8 |  | Event | Contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius. |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [Encoders](xref:Harp.StepperDriver.Encoders) | 77 | S16 | 3 | Event, Write | Contains the quadrature encoder readings. |  | [EncodersPayload](xref:Harp.StepperDriver.EncodersPayload) |
| [DigitalInputState](xref:Harp.StepperDriver.DigitalInputState) | 78 | U8 |  | Event | Reflects the state of the digital input lines. |  | [DigitalInputStates](xref:Harp.StepperDriver.DigitalInputStates) |
| [DeviceState](xref:Harp.StepperDriver.DeviceState) | 79 | U8 |  | Event | Contains the state of the device. |  | [DeviceStateMode](xref:Harp.StepperDriver.DeviceStateMode) |
| [MoveRelative](xref:Harp.StepperDriver.MoveRelative) | 80 | S32 | 4 | Write | Moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0. |  | [MoveRelativePayload](xref:Harp.StepperDriver.MoveRelativePayload) |
| [Motor0MoveRelative](xref:Harp.StepperDriver.Motor0MoveRelative) | 81 | S32 |  | Write | Moves motor 0 by the number of steps written in this register and set the direction according to the value's signal. |  | |
| [Motor1MoveRelative](xref:Harp.StepperDriver.Motor1MoveRelative) | 82 | S32 |  | Write | Moves motor 1 by the number of steps written in this register and set the direction according to the value's signal. |  | |
| [Motor2MoveRelative](xref:Harp.StepperDriver.Motor2MoveRelative) | 83 | S32 |  | Write | Moves motor 2 by the number of steps written in this register and set the direction according to the value's signal. |  | |
| [Motor3MoveRelative](xref:Harp.StepperDriver.Motor3MoveRelative) | 84 | S32 |  | Write | Moves motor 3 by the number of steps written in this register and set the direction according to the value's signal. |  | |
| [MoveAbsolute](xref:Harp.StepperDriver.MoveAbsolute) | 85 | S32 | 4 | Write | Moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0. |  | [MoveAbsolutePayload](xref:Harp.StepperDriver.MoveAbsolutePayload) |
| [Motor0MoveAbsolute](xref:Harp.StepperDriver.Motor0MoveAbsolute) | 86 | S32 |  | Write | Moves motor 0 to the absolute position written in this register. |  | |
| [Motor1MoveAbsolute](xref:Harp.StepperDriver.Motor1MoveAbsolute) | 87 | S32 |  | Write | Moves motor 1 to the absolute position written in this register. |  | |
| [Motor2MoveAbsolute](xref:Harp.StepperDriver.Motor2MoveAbsolute) | 88 | S32 |  | Write | Moves motor 2 to the absolute position written in this register. |  | |
| [Motor3MoveAbsolute](xref:Harp.StepperDriver.Motor3MoveAbsolute) | 89 | S32 |  | Write | Moves motor 3 to the absolute position written in this register. |  | |
| [AccumulatedSteps](xref:Harp.StepperDriver.AccumulatedSteps) | 90 | S32 | 4 | Event, Write | Contains the accumulated steps of all motors. Write a value to set the accumulated steps. An event will be emitted with a dispatch rate defined in AccumulatedStepsSamplingRate. |  | [AccumulatedStepsPayload](xref:Harp.StepperDriver.AccumulatedStepsPayload) |
| [Motor0AccumulatedSteps](xref:Harp.StepperDriver.Motor0AccumulatedSteps) | 91 | S32 |  | Write | Contains the accumulated number of steps of motor 0. Write a value to set the current number of accumulated steps. |  | |
| [Motor1AccumulatedSteps](xref:Harp.StepperDriver.Motor1AccumulatedSteps) | 92 | S32 |  | Write | Contains the accumulated number of steps of motor 1. Write a value to set the current number of accumulated steps. |  | |
| [Motor2AccumulatedSteps](xref:Harp.StepperDriver.Motor2AccumulatedSteps) | 93 | S32 |  | Write | Contains the accumulated number of steps of motor 2. Write a value to set the current number of accumulated steps. |  | |
| [Motor3AccumulatedSteps](xref:Harp.StepperDriver.Motor3AccumulatedSteps) | 94 | S32 |  | Write | Contains the accumulated number of steps of motor 3. Write a value to set the current number of accumulated steps. |  | |
| [MaxPosition](xref:Harp.StepperDriver.MaxPosition) | 95 | S32 | 4 | Write | Defines the limit of the accumulated steps for the positive movement of all motors. The device will not let the motors move further than this value. A value equal to 0 will disable the limits. |  | [MaxPositionPayload](xref:Harp.StepperDriver.MaxPositionPayload) |
| [Motor0MaxPosition](xref:Harp.StepperDriver.Motor0MaxPosition) | 96 | S32 |  | Write | Specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor1MaxPosition](xref:Harp.StepperDriver.Motor1MaxPosition) | 97 | S32 |  | Write | Specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor2MaxPosition](xref:Harp.StepperDriver.Motor2MaxPosition) | 98 | S32 |  | Write | Specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor3MaxPosition](xref:Harp.StepperDriver.Motor3MaxPosition) | 99 | S32 |  | Write | Specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [MinPosition](xref:Harp.StepperDriver.MinPosition) | 100 | S32 | 4 | Write | Defines the limit of the accumulated steps for the negative movement of all motors. The device will not let the motors move further than this value. A value equal to 0 will disable the limits. |  | [MinPositionPayload](xref:Harp.StepperDriver.MinPositionPayload) |
| [Motor0MinPosition](xref:Harp.StepperDriver.Motor0MinPosition) | 101 | S32 |  | Write | Specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor1MinPosition](xref:Harp.StepperDriver.Motor1MinPosition) | 102 | S32 |  | Write | Specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor2MinPosition](xref:Harp.StepperDriver.Motor2MinPosition) | 103 | S32 |  | Write | Specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [Motor3MinPosition](xref:Harp.StepperDriver.Motor3MinPosition) | 104 | S32 |  | Write | Specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature. |  | |
| [StepRelative](xref:Harp.StepperDriver.StepRelative) | 105 | S32 | 4 | Write | Starts the movement of all motors with the step interval defined by this array. The value's signal defines the direction. |  | [StepRelativePayload](xref:Harp.StepperDriver.StepRelativePayload) |
| [Motor0StepRelative](xref:Harp.StepperDriver.Motor0StepRelative) | 106 | S32 |  | Write | Starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction. |  | |
| [Motor1StepRelative](xref:Harp.StepperDriver.Motor1StepRelative) | 107 | S32 |  | Write | Starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction. |  | |
| [Motor2StepRelative](xref:Harp.StepperDriver.Motor2StepRelative) | 108 | S32 |  | Write | Starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction. |  | |
| [Motor3StepRelative](xref:Harp.StepperDriver.Motor3StepRelative) | 109 | S32 |  | Write | Starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction. |  | |
| [StopMotors](xref:Harp.StepperDriver.StopMotors) | 110 | U8 |  | Write | Stops the motors selected in the bit-mask immediately. |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [ResetEncoders](xref:Harp.StepperDriver.ResetEncoders) | 112 | U8 |  | Write | Resets the encoder. |  | [QuadratureEncoders](xref:Harp.StepperDriver.QuadratureEncoders) |
| [TriggerQuickMovement](xref:Harp.StepperDriver.TriggerQuickMovement) | 130 | U8 |  | Write | Triggers the quick movement in the correspondent motor with the currently configured settings. |  | [StepperMotors](xref:Harp.StepperDriver.StepperMotors) |
| [Motor1QuickMovementPulseDistance](xref:Harp.StepperDriver.Motor1QuickMovementPulseDistance) | 131 | Float |  | Write | Sets the single step pulse distance for a quick movement, in micrometers, for the Motor 1. |  | |
| [Motor2QuickMovementPulseDistance](xref:Harp.StepperDriver.Motor2QuickMovementPulseDistance) | 132 | Float |  | Write | Sets the single step pulse distance for a quick movement, in micrometers, for the Motor 2. |  | |
| [Motor1QuickMovementNominalSpeed](xref:Harp.StepperDriver.Motor1QuickMovementNominalSpeed) | 133 | Float |  | Write | Sets the target speed for a quick movement, in millimeters per second, for the Motor 1. |  | |
| [Motor2QuickMovementNominalSpeed](xref:Harp.StepperDriver.Motor2QuickMovementNominalSpeed) | 134 | Float |  | Write | Sets the target speed for a quick movement, in millimeters per second, for the Motor 2. |  | |
| [Motor1QuickMovementInitialSpeed](xref:Harp.StepperDriver.Motor1QuickMovementInitialSpeed) | 135 | Float |  | Write | Sets the initial speed for a quick movement, in millimeters per second, for the Motor 1. |  | |
| [Motor2QuickMovementInitialSpeed](xref:Harp.StepperDriver.Motor2QuickMovementInitialSpeed) | 136 | Float |  | Write | Sets the initial speed for a quick movement, in millimeters per second, for the Motor 2. |  | |
| [Motor1QuickMovementAcceleration](xref:Harp.StepperDriver.Motor1QuickMovementAcceleration) | 137 | Float |  | Write | Sets the acceleration for a quick movement, in meters per second^2, for the Motor 1. |  | |
| [Motor2QuickMovementAcceleration](xref:Harp.StepperDriver.Motor2QuickMovementAcceleration) | 138 | Float |  | Write | Sets the acceleration for a quick movement, in meters per second^2, for the Motor 2. |  | |
| [Motor1QuickMovementDistance](xref:Harp.StepperDriver.Motor1QuickMovementDistance) | 139 | Float |  | Write | Sets the travel distance of a quick movement, in millimeters, for the Motor 1. |  | |
| [Motor2QuickMovementDistance](xref:Harp.StepperDriver.Motor2QuickMovementDistance) | 140 | Float |  | Write | Sets the travel distance of a quick movement, in millimeters, for the Motor 1. |  | |
