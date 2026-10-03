# Configure Driver

Configuration parameters must be set for the [Harp StepperDriver](https://github.com/harp-tech/device.stepperdriver) before it can be used to drive stepper motors.

> [!WARNING]
> When adding these operators to the workflow, make sure to use the device-specific versions, e.g. `Device (Harp.StepperDriver)` instead of `Device (Harp)`. If correctly selected, the names of these operators in the workflow panel will change to reflect either the name of the device or the selected register/payload.

## Prerequisites

- Install the `Bonsai.Windows.Input` package from the Bonsai [package manager](https://bonsai-rx.org/docs/articles/packages.html).
- The steps listed below demonstrate driver configuration for a single stepper motor (`Motor1`) connected to the `Stepper 1` output on the `StepperDriver`. Adjust the relevant properties for your specific configuration.

## Device pattern

Set up the standard Harp [device pattern](https://harp-tech.org/articles/operators.html#device-pattern) to initialize the device, broadcast events, and send commands to the `StepperDriver`.

:::workflow
![StepperDriver Device Pattern](../workflows/stepperdriver-configuration-devicepattern.bonsai)
:::

- Insert a [`Device`] operator, set the `PortName` property to the communications port for the device.
- Insert a [`DeviceDataWriter`] sink and set the `Path` property (e.g. `StepperDriver.harp`). 
   - This will save the data in the standard Harp logging format, which can be loaded with [`harp-python`](https://harp-tech.org/articles/python.html).
- Insert a [`PublishSubject`] operator and name it `StepperDriver Events`.
- Right-click the [`Device`] operator, select "Create Source (Bonsai.Harp.HarpMessage)" > "BehaviorSubject". 
   - Name the generated [``BehaviorSubject`1``] [source subject](https://bonsai-rx.org/docs/articles/subjects.html#source-subjects) `StepperDriver Commands`. 
   - Connect it as input to the [`Device`] operator.

## Configure interlock state

The `StepperDriver` includes an external interlock terminal (labelled `Enable` on the device) designed for use with a safety switch. The interlock setting must be configured to enable the device.

:::workflow
![StepperDriver Interlock](../workflows/stepperdriver-configuration-interlock.bonsai)
:::

- Insert a [`SubscribeSubject`] operator and configure the `Name` property to `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator to construct a [`HarpMessage`] command to send to the device and configure these properties: 
   - `Payload` - Select [`InterlockEnabledPayload`] from the property drop down menu.
   - `InterlockEnabled` - Select `Open` to enable the device if the interlock is not in use, or `Closed` to enable the device with a connected interlock switch.
- Insert a [`MulticastSubject`] operator to send [`HarpMessage`] commands to named subjects, and configure the `Name` property to `StepperDriver Commands`.

> [!TIP]
> The `StepperDriver Events` > `Take(1)` ensures that configuration commands are sent only after the `StepperDriver` has initialized. It requires the `DumpRegisters` property to be set to `True` in the [`Device`] operator.

## Configure microstep resolution

The microstep resolution determines the size of each step, and directly affects the speed and distance of movement commands. It must be set individually for each motor driver. Values range from `Microstep8` (coarsest) to `Microstep64` (finest).

:::workflow
![StepperDriver Microstep Resolution](../workflows/stepperdriver-configuration-microstepresolution.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator and configure these properties: 
   - `Payload` - Select [`Motor1MicrostepResolutionPayload`].
   - `Motor1MicrostepResolution` - Select the desired microstep resolution (e.g. `Microstep8`).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

## Configure operation parameters

Other operation parameters to be set include the operation mode of the motor driver, maximum run current, as well as the hold current.

:::workflow
![StepperDriver Operation Parameters](../workflows/stepperdriver-configuration-operationparameters.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `StepperDriver Events`.
- Insert a [`Take`] combinator and set the `Count` property to 1.
- Insert a [`CreateMessage`] operator and configure these properties: 
   - `Payload` - Select [`Motor1OperationModePayload`].
   - `Motor1OperationMode` - Set `QuietMode` for regular operation and `DynamicMovements` for quick movements.
- Insert a [`CreateMessage`] operator on another branch, and configure these properties:
   - `Payload` - Select [`Motor1MaximumRunCurrentPayload`].
   - `Motor1MaximumRunCurrent` - Set the motor's rated phase current in amps (e.g. 1).
- Insert a [`CreateMessage`] operator on another branch, and configure these properties:
   - `Payload` - Select [`Motor1HoldCurrentReductionPayload`].
   - `Motor1HoldCurrentReduction` - Set to minimize heat generation and adjust the holding torque of the motor at rest (e.g. `ReductionTo25Percent` for low holding torque with light external loads). 
- Insert a [`Merge`] operator to combine all three commands into one [`HarpMessage`] stream.
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

> [!TIP]
> All configuration commands can be combined using [`Merge`] and sent into a single `StepperDriver Commands` pipeline.

## Enable and disable motor drivers

Motor drivers have to be enabled before use and can be disabled at any time.

:::workflow
![StepperDriver Toggle Motor](../workflows/stepperdriver-configuration-togglemotor.bonsai)
:::

- Insert a [`KeyDown`] source and set the `Filter` property to `1`. 
- Insert a [`CreateMessage`] operator and configure these properties:
    - `Payload` - Select [`EnableDriverPayload`].
    - `EnableDriver` - Select motor driver to enable (e.g. `Motor1`).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

To disable the driver, set up a separate pipeline:

- Insert a [`KeyDown`] source and set the `Filter` property to `2`. 
- Insert a [`CreateMessage`] operator and configure these properties:
    - `Payload` - Select [`DisableDriverPayload`].
    - `DisableDriver` - Select the motor driver to disable (e.g. `Motor1`).
- Insert a [`MulticastSubject`] operator named `StepperDriver Commands`.

Run the workflow and press <kbd>1</kbd> to enable the driver and <kbd>2</kbd> to disable the driver. When the driver is enabled, the LED above the driver connection on the device will blink red.

> [!TIP]
> To target multiple drivers, enter their names, separated by a comma, for the `EnableDriver` and `DisableDriver` properties (e.g. `Motor0`, `Motor1`).

<!--Reference Style Links -->
[``BehaviorSubject`1``]: xref:Bonsai.Reactive.BehaviorSubject
[`CreateMessage`]: xref:Harp.StepperDriver.CreateMessage
[`Device`]: xref:Harp.StepperDriver.Device
[`DeviceDataWriter`]: xref:Harp.StepperDriver.DeviceDataWriter
[`DisableDriverPayload`]: xref:Harp.StepperDriver.CreateDisableDriverPayload
[`EnableDriverPayload`]: xref:Harp.StepperDriver.CreateEnableDriverPayload
[`HarpMessage`]: xref:Bonsai.Harp.HarpMessage
[`InterlockEnabledPayload`]: xref:Harp.StepperDriver.CreateInterlockEnabledPayload
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`Merge`]: xref:Bonsai.Reactive.Merge
[`Motor1MaximumRunCurrentPayload`]: xref:Harp.StepperDriver.CreateMotor1MaximumRunCurrentPayload
[`Motor1HoldCurrentReductionPayload`]: xref:Harp.StepperDriver.CreateMotor1HoldCurrentReductionPayload
[`Motor1MicrostepResolutionPayload`]: xref:Harp.StepperDriver.CreateMotor1MicrostepResolutionPayload
[`Motor1OperationModePayload`]: xref:Harp.StepperDriver.CreateMotor1OperationModePayload
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Take`]: xref:Bonsai.Reactive.Take