# Control Motor

The following exercises demonstrate how to control motor speed and rotational position with the [Harp StepperDriver](https://github.com/harp-tech/device.stepperdriver).

> [!WARNING]
> Ensure that you are familiar with the operation of the `StepperDriver` and motors before connecting external loads. Consider using end-of-travel switches in conjunction with a [digital input](xref:Harp.StepperDriver.CreateEnableDigitalInputsPayload) [configuration](xref:Harp.StepperDriver.CreateInput0OpModePayload). Improper use of the `StepperDriver` and motors may result in damage to equipment.

## Prerequisites

- Install the `Bonsai.Windows.Input` package from the Bonsai [package manager](https://bonsai-rx.org/docs/articles/packages.html).
- Connect a stepper motor to the `Stepper 1` output on the `StepperDriver`, set up the [device pattern](./stepperdriver-configuration.md#device-pattern), [configure](./stepperdriver-configuration.md) the device, and [enable](./stepperdriver-configuration.md#enable-and-disable-motor-drivers) the `Motor1` stepper driver.

## Visualize position

For these exercises, it helps to track the rotational position of the motor. The `StepperDriver` can broadcast a stream of [`AccumulatedSteps`] events, which can be displayed in a visualizer for this purpose.

:::workflow
![StepperDriver Position Visualizer](../workflows/stepperdriver-controlmotor-visualizer.bonsai)
:::

Enable the [`AccumulatedSteps`] event register and configure the dispatch rate:

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator and configure these properties: 
   - `Payload` - Select [`AccumulatedStepsSamplingRatePayload`].
   - `AccumulatedStepsSamplingRate` - Set the desired sampling rate (e.g. `Rate10Hz` for coarse movements).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Extract the [`AccumulatedSteps`] events and format them for display in the visualizer:

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Parse`] operator and set the `Register` property to [`AccumulatedSteps`].
- Right-click on the [`Parse`] operator, select "Output (Harp.StepperDriver.AccumulatedStepsPayload)" > "Motor1" from the context menu.
- Insert a [`VisualizerWindow`] node.

## Exercise 1: Set acceleration profile

Motor motion is driven by a series of step pulses, and speed can be controlled by specifying the interval between steps (in μs). The acceleration profile is defined by three parameters:

- the initial/final step interval
- the target step interval at nominal speed
- the change in interval per step during the acceleration/deceleration phase

:::workflow
![StepperDriver Acceleration Profile](../workflows/stepperdriver-controlmotor-acceleration.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `3`. 
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MaximumStepIntervalPayload`].
   - `Motor1MaximumStepInterval` - Set the initial/final step interval (e.g. 2000).
- Insert a [`CreateMessage`] operator on a new branch and configure these properties:
   - `Payload` - Select [`Motor1StepIntervalPayload`].
   - `Motor1StepInterval` - Set the target step interval (e.g. 250).
- Insert a [`CreateMessage`] operator on a new branch and configure these properties:
   - `Payload` - Select [`Motor1StepAccelerationIntervalPayload`].
   - `Motor1StepAcceleration` - Set the change in step interval (e.g. 10).
- Combine the three messages with a [`Merge`] combinator.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>3</kbd> to set the acceleration profile. 

While the workflow is running, you can also adjust the properties, and press <kbd>3</kbd> to update the acceleration profile and test different settings.

## Exercise 2: Move relative steps

The [`MoveRelative`] register moves the motor by a specified number of steps from its current position.

:::workflow
![StepperDriver Move Relative](../workflows/stepperdriver-controlmotor-moverelative.bonsai)
:::

To move the motor in the positive direction:

- Insert a [`KeyDown`] source and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MoveRelativePayload`].
   - `Motor1MoveRelative` - Set the number of steps to move (e.g. 3000).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

To move the motor in the negative direction, set up a separate pipeline:

- Insert a [`KeyDown`] source and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MoveRelativePayload`].
   - `Motor1MoveRelative` - Set to a negative value (e.g. -3000).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>A</kbd> to move the motor forward and <kbd>S</kbd> to move it back. Observe the accumulated steps in the visualizer to track the motor's position.

> [!TIP]
> To move multiple motors simultaneously, use the [`MoveRelativePayload`].

## Exercise 3: Move to absolute step position

The [`MoveAbsolute`] register moves the motor to an absolute step position based on the [`AccumulatedSteps`] counter.

:::workflow
![StepperDriver Move Absolute](../workflows/stepperdriver-controlmotor-moveabsolute.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MoveAbsolutePayload`].
   - `Motor1MoveAbsolute` - Set the target step position (e.g. 2000).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>D</kbd> to move the motor to the target position. Observe the accumulated steps in the visualizer to confirm the motor reaches the specified position.

> [!TIP]
> To move multiple motors simultaneously, use the [`MoveAbsolutePayload`].

## Exercise 4: Set position limits

Position limits restrict the range of motion by defining a minimum and maximum step position based on the [`AccumulatedSteps`] counter. The motor will stop automatically if it reaches either limit.

:::workflow
![StepperDriver Position Limit](../workflows/stepperdriver-controlmotor-positionlimit.bonsai)
:::

Set the minimum position limit:

- Insert a [`KeyDown`] source and set the `Filter` property to `4`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MinPositionPayload`].
   - `Motor1MinPosition` - Set the minimum step position (e.g. 1000).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Set the maximum position limit in a separate pipeline:

- Insert a [`KeyDown`] source and set the `Filter` property to `5`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1MaxPositionPayload`].
   - `Motor1MaxPosition` - Set the maximum step position (e.g. 4000).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>4</kbd> to set the minimum limit and <kbd>5</kbd> to set the maximum limit. Use the move commands from the previous exercises to verify that the motor stops at each limit.

While the workflow is running, you can also adjust the properties and press <kbd>4</kbd> or <kbd>5</kbd> to update the position limits.

> [!TIP]
> To set position limits for all motors, use the [`MinPositionPayload`] and [`MaxPositionPayload`].

## Exercise 5: Stop motors

The [`StopMotors`] register immediately halts motors regardless of any current move command and can be used as an emergency stop.

:::workflow
![StepperDriver Stop Motors](../workflows/stepperdriver-controlmotor-stopmotors.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`StopMotorsPayload`].
   - `StopMotors` - Select the motor to stop (e.g. `Motor1`).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and use the move commands from the previous exercises to set the motor in motion. Press <kbd>F</kbd> to immediately stop the motor.

> [!TIP]
> To target multiple motors, enter their names, separated by a comma, for the `StopMotors` property (e.g. `Motor0`, `Motor1`).

## Exercise 6: Move continuously

The [`StepRelative`] register ignores the acceleration profile and moves the motor continuously at a fixed speed determined by the step interval and a direction indicated by the sign of the value. 

> [!WARNING]
> Ensure that position limits or the stop command are in place before trying this exercise.

:::workflow
![StepperDriver Move Continuously](../workflows/stepperdriver-controlmotor-movecontinuous.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`Motor1StepRelativePayload`].
   - `Motor1` - Set the step interval and direction (e.g. 1000 for forward).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>G</kbd> to start continuous motor motion. Press <kbd>F</kbd> from the previous exercise to stop the motor.

> [!TIP]
> To move multiple motors simultaneously, use the [`StepRelativePayload`].

## Exercise 7: Reset position

Writing new values to the [`AccumulatedSteps`] register resets the position tracking to a new origin.

:::workflow
![StepperDriver Reset Position](../workflows/stepperdriver-controlmotor-resetposition.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `6`.
- Insert a [`CreateMessage`] operator and configure these properties:
   - `Payload` - Select [`AccumulatedStepsPayload`].
   - `Motor1` - Set to 0.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>6</kbd> to reset the accumulated steps. Observe the accumulated steps in the visualizer to verify that the position has been reset.

> [!WARNING]
> Note that any absolute position moves and position limits set in the previous exercises will now refer to a different physical location.

<!--Reference Style Links -->
[`AccumulatedSteps`]: xref:Harp.StepperDriver.AccumulatedSteps
[`AccumulatedStepsPayload`]: xref:Harp.StepperDriver.CreateAccumulatedStepsPayload
[`AccumulatedStepsSamplingRatePayload`]: xref:Harp.StepperDriver.CreateAccumulatedStepsSamplingRatePayload
[`CreateMessage`]: xref:Harp.StepperDriver.CreateMessage
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`Merge`]: xref:Bonsai.Reactive.Merge
[`MoveRelative`]: xref:Harp.StepperDriver.MoveRelative
[`MoveRelativePayload`]: xref:Harp.StepperDriver.CreateMoveRelativePayload
[`MoveAbsolute`]: xref:Harp.StepperDriver.MoveAbsolute
[`MoveAbsolutePayload`]: xref:Harp.StepperDriver.CreateMoveAbsolutePayload
[`MaxPositionPayload`]: xref:Harp.StepperDriver.CreateMaxPositionPayload
[`StopMotors`]: xref:Harp.StepperDriver.StopMotors
[`StopMotorsPayload`]: xref:Harp.StepperDriver.CreateStopMotorsPayload
[`MinPositionPayload`]: xref:Harp.StepperDriver.CreateMinPositionPayload
[`Motor1MaxPositionPayload`]: xref:Harp.StepperDriver.CreateMotor1MaxPositionPayload
[`Motor1MinPositionPayload`]: xref:Harp.StepperDriver.CreateMotor1MinPositionPayload
[`Motor1MoveAbsolutePayload`]: xref:Harp.StepperDriver.CreateMotor1MoveAbsolutePayload
[`Motor1MoveRelativePayload`]: xref:Harp.StepperDriver.CreateMotor1MoveRelativePayload
[`Motor1StepAccelerationIntervalPayload`]: xref:Harp.StepperDriver.CreateMotor1StepAccelerationIntervalPayload
[`Motor1StepRelativePayload`]: xref:Harp.StepperDriver.CreateMotor1StepRelativePayload
[`Motor1MaximumStepIntervalPayload`]: xref:Harp.StepperDriver.CreateMotor1MaximumStepIntervalPayload
[`Motor1StepIntervalPayload`]: xref:Harp.StepperDriver.CreateMotor1StepIntervalPayload
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`Parse`]: xref:Harp.StepperDriver.Parse
[`StepRelative`]: xref:Harp.StepperDriver.StepRelative
[`StepRelativePayload`]: xref:Harp.StepperDriver.CreateStepRelativePayload
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Take`]: xref:Bonsai.Reactive.Take
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow