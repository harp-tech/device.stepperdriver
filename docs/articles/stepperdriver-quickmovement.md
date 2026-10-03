# QuickMovement

The [Harp StepperDriver](https://github.com/harp-tech/device.stepperdriver) features a special `QuickMovement` mode that can be used for extremely fast movements with low trigger latency. It requires calibration and preloading of speed, acceleration and distance parameters.

> [!TIP]
> For some applications requiring fast movements, [`StepRelative`](./stepperdriver-controlmotor.md#exercise-6-move-continuously) with position limits configured may be sufficient.

> [!WARNING]
> Ensure that you are familiar with the operation of the `StepperDriver` and motors before connecting external loads. Consider using end-of-travel switches in conjunction with a [digital input](xref:Harp.StepperDriver.CreateEnableDigitalInputsPayload) [configuration](xref:Harp.StepperDriver.CreateInput0OpModePayload). Improper use of the `StepperDriver` and motors may result in damage to equipment, especially with `QuickMovement`.

## Prerequisites

- `QuickMovement` requires firmware `fw0.7-harp1.14` and later. Download the firmware from the [release](https://github.com/harp-tech/device.stepperdriver/releases) page and update it with the [Harp Toolkit](https://github.com/harp-tech/toolkit).
- `QuickMovement` requires `Harp.StepperDriver` package `0.4.0` and later, update it with the Bonsai [package manager](https://bonsai-rx.org/docs/articles/packages.html).
- `QuickMovement` is only supported on `Stepper 1` and `Stepper 2` drivers. Connect a stepper motor (`Motor1`) to `Stepper 1` output for these exercises.
- Install the `Bonsai.Windows.Input` package from the Bonsai [package manager](https://bonsai-rx.org/docs/articles/packages.html).
- Set up the [device pattern](./stepperdriver-configuration.md#device-pattern), and [enable](./stepperdriver-configuration.md#enable-and-disable-motor-drivers) the `Motor1` stepper driver.
- Use the [position visualizer](./stepperdriver-controlmotor.md#visualize-position) to monitor the movement.

## Configure Driver

Configure the following registers for `QuickMovement`. For all other parameters, refer to the [configuration](./stepperdriver-configuration.md) guide.

:::workflow
![StepperDriver QuickMovement Driver Configuration](../workflows/stepperdriver-quickmovement-driverconfiguration.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator and configure these properties: 
   - `Payload` - Select [`Motor1MicrostepResolutionPayload`].
   - `Motor1MicrostepResolution` - Select `Microstep8` to achieve the highest speed.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1OperationModePayload`].
   - `Motor1OperationMode` - Select `DynamicMovements`.
- Insert a [`CreateMessage`] operator on another branch and configure these properties:
   - `Payload` - Select [`AccumulatedStepsSamplingRatePayload`].
   - `AccumulatedStepsSamplingRate` - Select `Rate100Hz` for a higher position visualizer refresh rate.
- Combine the two messages with a [`Merge`] combinator.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

## Exercise 1: Calibrate QuickMovement

Calibrate `QuickMovement` before use by measuring the distance an external load travels per step pulse in micrometers (μm). 

:::workflow
![StepperDriver QuickMovement Calibration](../workflows/stepperdriver-quickmovement-calibration.bonsai)
:::

Connect the external load to the stepper motor, and move the motor a set number of steps. Make sure the path is clear or set [position limits](./stepperdriver-controlmotor.md#exercise-4-set-position-limits).

- Insert a [`KeyDown`] source and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MoveRelativePayload`].
   - `Motor1MoveRelative` - Set the number of steps to move (e.g. 100).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>A</kbd> to move the external load. Measure the distance traveled and divide by the number of steps to get the distance per step pulse.

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1QuickMovementPulseDistancePayload`].
   - `Motor1QuickMovementPulseDistance` - Set the calibrated step pulse distance, in μm (e.g. 100).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

## Exercise 2: Arm QuickMovement

Preload movement parameters to arm the `QuickMovement` for rapid execution. Instead of steps or step intervals, movement parameters will use SI units: 
- millimeters (mm) for distance.
- millimeters per second (mm/s) for speed.
- meters per second squared (m/s²) for acceleration.

:::workflow
![StepperDriver QuickMovement Arming](../workflows/stepperdriver-quickmovement-arming.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `7`.
- Insert a [`CreateMessage`] operator on a separate branch and configure these properties:
   - `Payload` - Select [`Motor1QuickMovementInitialSpeedPayload`].
   - `Motor1QuickMovementInitialSpeed` - Set the initial speed in mm/s (e.g. 2).
- Insert a [`CreateMessage`] operator on a separate branch and configure these properties:
   - `Payload` - Select [`Motor1QuickMovementNominalSpeedPayload`].
   - `Motor1QuickMovementNominalSpeed` - Set the target speed in mm/s (e.g. 200).
- Insert a [`CreateMessage`] operator on a separate branch and configure these properties:
   - `Payload` - Select [`Motor1QuickMovementAccelerationPayload`].
   - `Motor1QuickMovementAcceleration` - Set the acceleration in m/s² (e.g. 5).
- Insert a [`CreateMessage`] operator on a separate branch and configure these properties:
   - `Payload` - Select [`Motor1QuickMovementDistancePayload`].
   - `Motor1QuickMovementDistance` - Set the travel distance in mm (e.g. 20 or -20, where the sign determines direction).
- Insert a [`Merge`] operator to combine all the commands.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

> [!TIP]
> The example values given above will move an external load 20 mm in ~ 100 ms (based on target speed). 

Run the workflow and press <kbd>7</kbd> to arm the `QuickMovement`. 

While the workflow is running, you can adjust the properties and press <kbd>7</kbd> to update the `QuickMovement` configuration to test different settings.

## Exercise 3: Trigger QuickMovement

Trigger `QuickMovement` with a single command.

:::workflow
![StepperDriver QuickMovement Trigger](../workflows/stepperdriver-quickmovement-triggering.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `H`.
- Insert a [`CreateMessage`] operator and configure the following properties:
   - `Payload` - Select [`TriggerQuickMovementPayload`].
   - `TriggerQuickMovement` - Select `Motor1`.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>H</kbd> to trigger the `QuickMovement`.

<!--Reference Style Links -->
[`AccumulatedSteps`]: xref:Harp.StepperDriver.AccumulatedSteps
[`AccumulatedStepsSamplingRatePayload`]: xref:Harp.StepperDriver.CreateAccumulatedStepsSamplingRatePayload
[`CreateMessage`]: xref:Harp.StepperDriver.CreateMessage
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`Merge`]: xref:Bonsai.Reactive.Merge
[`Motor1MicrostepResolutionPayload`]: xref:Harp.StepperDriver.CreateMotor1MicrostepResolutionPayload
[`Motor1MoveRelativePayload`]: xref:Harp.StepperDriver.CreateMotor1MoveRelativePayload
[`Motor1QuickMovementAccelerationPayload`]: xref:Harp.StepperDriver.CreateMotor1QuickMovementAccelerationPayload
[`Motor1QuickMovementInitialSpeedPayload`]: xref:Harp.StepperDriver.CreateMotor1QuickMovementInitialSpeedPayload
[`Motor1QuickMovementDistancePayload`]: xref:Harp.StepperDriver.CreateMotor1QuickMovementDistancePayload
[`Motor1QuickMovementPulseDistancePayload`]: xref:Harp.StepperDriver.CreateMotor1QuickMovementPulseDistancePayload
[`Motor1QuickMovementNominalSpeedPayload`]: xref:Harp.StepperDriver.CreateMotor1QuickMovementNominalSpeedPayload
[`Motor1OperationModePayload`]: xref:Harp.StepperDriver.CreateMotor1OperationModePayload
[`Motor1MicrostepResolution`]: xref:Harp.StepperDriver.Motor1MicrostepResolution
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Take`]: xref:Bonsai.Reactive.Take
[`TriggerQuickMovementPayload`]: xref:Harp.StepperDriver.CreateTriggerQuickMovementPayload