using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.StepperDriver
{
    /// <summary>
    /// Generates events and processes commands for the StepperDriver device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the StepperDriver device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="StepperDriver"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1130;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(StepperDriver);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(EnableDriver) },
            { 33, typeof(DisableDriver) },
            { 34, typeof(EnableEncoders) },
            { 35, typeof(DisableEncoders) },
            { 36, typeof(EnableDigitalInputs) },
            { 37, typeof(DisableDigitalInputs) },
            { 38, typeof(Motor0OperationMode) },
            { 39, typeof(Motor1OperationMode) },
            { 40, typeof(Motor2OperationMode) },
            { 41, typeof(Motor3OperationMode) },
            { 42, typeof(Motor0MicrostepResolution) },
            { 43, typeof(Motor1MicrostepResolution) },
            { 44, typeof(Motor2MicrostepResolution) },
            { 45, typeof(Motor3MicrostepResolution) },
            { 46, typeof(Motor0MaximumRunCurrent) },
            { 47, typeof(Motor1MaximumRunCurrent) },
            { 48, typeof(Motor2MaximumRunCurrent) },
            { 49, typeof(Motor3MaximumRunCurrent) },
            { 50, typeof(Motor0HoldCurrentReduction) },
            { 51, typeof(Motor1HoldCurrentReduction) },
            { 52, typeof(Motor2HoldCurrentReduction) },
            { 53, typeof(Motor3HoldCurrentReduction) },
            { 54, typeof(Motor0StepInterval) },
            { 55, typeof(Motor1StepInterval) },
            { 56, typeof(Motor2StepInterval) },
            { 57, typeof(Motor3StepInterval) },
            { 58, typeof(Motor0MaximumStepInterval) },
            { 59, typeof(Motor1MaximumStepInterval) },
            { 60, typeof(Motor2MaximumStepInterval) },
            { 61, typeof(Motor3MaximumStepInterval) },
            { 62, typeof(Motor0StepAccelerationInterval) },
            { 63, typeof(Motor1StepAccelerationInterval) },
            { 64, typeof(Motor2StepAccelerationInterval) },
            { 65, typeof(Motor3StepAccelerationInterval) },
            { 66, typeof(EncoderMode) },
            { 67, typeof(EncoderSamplingRate) },
            { 68, typeof(Input0OpMode) },
            { 69, typeof(Input1OpMode) },
            { 70, typeof(Input2OpMode) },
            { 71, typeof(Input3OpMode) },
            { 72, typeof(Input0Trigger) },
            { 73, typeof(Input1Trigger) },
            { 74, typeof(Input2Trigger) },
            { 75, typeof(Input3Trigger) },
            { 76, typeof(InterlockEnabled) },
            { 77, typeof(AccumulatedStepsSamplingRate) },
            { 78, typeof(MotorStopped) },
            { 79, typeof(MotorOverVoltageDetection) },
            { 80, typeof(MotorRaisedError) },
            { 81, typeof(Encoders) },
            { 82, typeof(DigitalInputState) },
            { 83, typeof(DeviceState) },
            { 84, typeof(MoveRelative) },
            { 85, typeof(Motor0MoveRelative) },
            { 86, typeof(Motor1MoveRelative) },
            { 87, typeof(Motor2MoveRelative) },
            { 88, typeof(Motor3MoveRelative) },
            { 89, typeof(MoveAbsolute) },
            { 90, typeof(Motor0MoveAbsolute) },
            { 91, typeof(Motor1MoveAbsolute) },
            { 92, typeof(Motor2MoveAbsolute) },
            { 93, typeof(Motor3MoveAbsolute) },
            { 94, typeof(AccumulatedSteps) },
            { 95, typeof(Motor0MaxPosition) },
            { 96, typeof(Motor1MaxPosition) },
            { 97, typeof(Motor2MaxPosition) },
            { 98, typeof(Motor3MaxPosition) },
            { 99, typeof(Motor0MinPosition) },
            { 100, typeof(Motor1MinPosition) },
            { 101, typeof(Motor2MinPosition) },
            { 102, typeof(Motor3MinPosition) },
            { 103, typeof(Motor0StepRelative) },
            { 104, typeof(Motor1StepRelative) },
            { 105, typeof(Motor2StepRelative) },
            { 106, typeof(Motor3StepRelative) },
            { 107, typeof(StopMotors) },
            { 109, typeof(ResetEncoders) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="StepperDriver"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of StepperDriver messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="StepperDriver"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="StepperDriver"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="StepperDriver"/> device.
    /// </summary>
    /// <seealso cref="EnableDriver"/>
    /// <seealso cref="DisableDriver"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableDigitalInputs"/>
    /// <seealso cref="DisableDigitalInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumRunCurrent"/>
    /// <seealso cref="Motor1MaximumRunCurrent"/>
    /// <seealso cref="Motor2MaximumRunCurrent"/>
    /// <seealso cref="Motor3MaximumRunCurrent"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0StepInterval"/>
    /// <seealso cref="Motor1StepInterval"/>
    /// <seealso cref="Motor2StepInterval"/>
    /// <seealso cref="Motor3StepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderSamplingRate"/>
    /// <seealso cref="Input0OpMode"/>
    /// <seealso cref="Input1OpMode"/>
    /// <seealso cref="Input2OpMode"/>
    /// <seealso cref="Input3OpMode"/>
    /// <seealso cref="Input0Trigger"/>
    /// <seealso cref="Input1Trigger"/>
    /// <seealso cref="Input2Trigger"/>
    /// <seealso cref="Input3Trigger"/>
    /// <seealso cref="InterlockEnabled"/>
    /// <seealso cref="AccumulatedStepsSamplingRate"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOverVoltageDetection"/>
    /// <seealso cref="MotorRaisedError"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DeviceState"/>
    /// <seealso cref="MoveRelative"/>
    /// <seealso cref="Motor0MoveRelative"/>
    /// <seealso cref="Motor1MoveRelative"/>
    /// <seealso cref="Motor2MoveRelative"/>
    /// <seealso cref="Motor3MoveRelative"/>
    /// <seealso cref="MoveAbsolute"/>
    /// <seealso cref="Motor0MoveAbsolute"/>
    /// <seealso cref="Motor1MoveAbsolute"/>
    /// <seealso cref="Motor2MoveAbsolute"/>
    /// <seealso cref="Motor3MoveAbsolute"/>
    /// <seealso cref="AccumulatedSteps"/>
    /// <seealso cref="Motor0MaxPosition"/>
    /// <seealso cref="Motor1MaxPosition"/>
    /// <seealso cref="Motor2MaxPosition"/>
    /// <seealso cref="Motor3MaxPosition"/>
    /// <seealso cref="Motor0MinPosition"/>
    /// <seealso cref="Motor1MinPosition"/>
    /// <seealso cref="Motor2MinPosition"/>
    /// <seealso cref="Motor3MinPosition"/>
    /// <seealso cref="Motor0StepRelative"/>
    /// <seealso cref="Motor1StepRelative"/>
    /// <seealso cref="Motor2StepRelative"/>
    /// <seealso cref="Motor3StepRelative"/>
    /// <seealso cref="StopMotors"/>
    /// <seealso cref="ResetEncoders"/>
    [XmlInclude(typeof(EnableDriver))]
    [XmlInclude(typeof(DisableDriver))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableDigitalInputs))]
    [XmlInclude(typeof(DisableDigitalInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumRunCurrent))]
    [XmlInclude(typeof(Motor1MaximumRunCurrent))]
    [XmlInclude(typeof(Motor2MaximumRunCurrent))]
    [XmlInclude(typeof(Motor3MaximumRunCurrent))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0StepInterval))]
    [XmlInclude(typeof(Motor1StepInterval))]
    [XmlInclude(typeof(Motor2StepInterval))]
    [XmlInclude(typeof(Motor3StepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderSamplingRate))]
    [XmlInclude(typeof(Input0OpMode))]
    [XmlInclude(typeof(Input1OpMode))]
    [XmlInclude(typeof(Input2OpMode))]
    [XmlInclude(typeof(Input3OpMode))]
    [XmlInclude(typeof(Input0Trigger))]
    [XmlInclude(typeof(Input1Trigger))]
    [XmlInclude(typeof(Input2Trigger))]
    [XmlInclude(typeof(Input3Trigger))]
    [XmlInclude(typeof(InterlockEnabled))]
    [XmlInclude(typeof(AccumulatedStepsSamplingRate))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOverVoltageDetection))]
    [XmlInclude(typeof(MotorRaisedError))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DeviceState))]
    [XmlInclude(typeof(MoveRelative))]
    [XmlInclude(typeof(Motor0MoveRelative))]
    [XmlInclude(typeof(Motor1MoveRelative))]
    [XmlInclude(typeof(Motor2MoveRelative))]
    [XmlInclude(typeof(Motor3MoveRelative))]
    [XmlInclude(typeof(MoveAbsolute))]
    [XmlInclude(typeof(Motor0MoveAbsolute))]
    [XmlInclude(typeof(Motor1MoveAbsolute))]
    [XmlInclude(typeof(Motor2MoveAbsolute))]
    [XmlInclude(typeof(Motor3MoveAbsolute))]
    [XmlInclude(typeof(AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaxPosition))]
    [XmlInclude(typeof(Motor1MaxPosition))]
    [XmlInclude(typeof(Motor2MaxPosition))]
    [XmlInclude(typeof(Motor3MaxPosition))]
    [XmlInclude(typeof(Motor0MinPosition))]
    [XmlInclude(typeof(Motor1MinPosition))]
    [XmlInclude(typeof(Motor2MinPosition))]
    [XmlInclude(typeof(Motor3MinPosition))]
    [XmlInclude(typeof(Motor0StepRelative))]
    [XmlInclude(typeof(Motor1StepRelative))]
    [XmlInclude(typeof(Motor2StepRelative))]
    [XmlInclude(typeof(Motor3StepRelative))]
    [XmlInclude(typeof(StopMotors))]
    [XmlInclude(typeof(ResetEncoders))]
    [Description("Filters register-specific messages reported by the StepperDriver device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new EnableDriver();
        }

        string INamedElement.Name
        {
            get => $"{nameof(StepperDriver)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the StepperDriver device.
    /// </summary>
    /// <seealso cref="EnableDriver"/>
    /// <seealso cref="DisableDriver"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableDigitalInputs"/>
    /// <seealso cref="DisableDigitalInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumRunCurrent"/>
    /// <seealso cref="Motor1MaximumRunCurrent"/>
    /// <seealso cref="Motor2MaximumRunCurrent"/>
    /// <seealso cref="Motor3MaximumRunCurrent"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0StepInterval"/>
    /// <seealso cref="Motor1StepInterval"/>
    /// <seealso cref="Motor2StepInterval"/>
    /// <seealso cref="Motor3StepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderSamplingRate"/>
    /// <seealso cref="Input0OpMode"/>
    /// <seealso cref="Input1OpMode"/>
    /// <seealso cref="Input2OpMode"/>
    /// <seealso cref="Input3OpMode"/>
    /// <seealso cref="Input0Trigger"/>
    /// <seealso cref="Input1Trigger"/>
    /// <seealso cref="Input2Trigger"/>
    /// <seealso cref="Input3Trigger"/>
    /// <seealso cref="InterlockEnabled"/>
    /// <seealso cref="AccumulatedStepsSamplingRate"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOverVoltageDetection"/>
    /// <seealso cref="MotorRaisedError"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DeviceState"/>
    /// <seealso cref="MoveRelative"/>
    /// <seealso cref="Motor0MoveRelative"/>
    /// <seealso cref="Motor1MoveRelative"/>
    /// <seealso cref="Motor2MoveRelative"/>
    /// <seealso cref="Motor3MoveRelative"/>
    /// <seealso cref="MoveAbsolute"/>
    /// <seealso cref="Motor0MoveAbsolute"/>
    /// <seealso cref="Motor1MoveAbsolute"/>
    /// <seealso cref="Motor2MoveAbsolute"/>
    /// <seealso cref="Motor3MoveAbsolute"/>
    /// <seealso cref="AccumulatedSteps"/>
    /// <seealso cref="Motor0MaxPosition"/>
    /// <seealso cref="Motor1MaxPosition"/>
    /// <seealso cref="Motor2MaxPosition"/>
    /// <seealso cref="Motor3MaxPosition"/>
    /// <seealso cref="Motor0MinPosition"/>
    /// <seealso cref="Motor1MinPosition"/>
    /// <seealso cref="Motor2MinPosition"/>
    /// <seealso cref="Motor3MinPosition"/>
    /// <seealso cref="Motor0StepRelative"/>
    /// <seealso cref="Motor1StepRelative"/>
    /// <seealso cref="Motor2StepRelative"/>
    /// <seealso cref="Motor3StepRelative"/>
    /// <seealso cref="StopMotors"/>
    /// <seealso cref="ResetEncoders"/>
    [XmlInclude(typeof(EnableDriver))]
    [XmlInclude(typeof(DisableDriver))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableDigitalInputs))]
    [XmlInclude(typeof(DisableDigitalInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumRunCurrent))]
    [XmlInclude(typeof(Motor1MaximumRunCurrent))]
    [XmlInclude(typeof(Motor2MaximumRunCurrent))]
    [XmlInclude(typeof(Motor3MaximumRunCurrent))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0StepInterval))]
    [XmlInclude(typeof(Motor1StepInterval))]
    [XmlInclude(typeof(Motor2StepInterval))]
    [XmlInclude(typeof(Motor3StepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderSamplingRate))]
    [XmlInclude(typeof(Input0OpMode))]
    [XmlInclude(typeof(Input1OpMode))]
    [XmlInclude(typeof(Input2OpMode))]
    [XmlInclude(typeof(Input3OpMode))]
    [XmlInclude(typeof(Input0Trigger))]
    [XmlInclude(typeof(Input1Trigger))]
    [XmlInclude(typeof(Input2Trigger))]
    [XmlInclude(typeof(Input3Trigger))]
    [XmlInclude(typeof(InterlockEnabled))]
    [XmlInclude(typeof(AccumulatedStepsSamplingRate))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOverVoltageDetection))]
    [XmlInclude(typeof(MotorRaisedError))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DeviceState))]
    [XmlInclude(typeof(MoveRelative))]
    [XmlInclude(typeof(Motor0MoveRelative))]
    [XmlInclude(typeof(Motor1MoveRelative))]
    [XmlInclude(typeof(Motor2MoveRelative))]
    [XmlInclude(typeof(Motor3MoveRelative))]
    [XmlInclude(typeof(MoveAbsolute))]
    [XmlInclude(typeof(Motor0MoveAbsolute))]
    [XmlInclude(typeof(Motor1MoveAbsolute))]
    [XmlInclude(typeof(Motor2MoveAbsolute))]
    [XmlInclude(typeof(Motor3MoveAbsolute))]
    [XmlInclude(typeof(AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaxPosition))]
    [XmlInclude(typeof(Motor1MaxPosition))]
    [XmlInclude(typeof(Motor2MaxPosition))]
    [XmlInclude(typeof(Motor3MaxPosition))]
    [XmlInclude(typeof(Motor0MinPosition))]
    [XmlInclude(typeof(Motor1MinPosition))]
    [XmlInclude(typeof(Motor2MinPosition))]
    [XmlInclude(typeof(Motor3MinPosition))]
    [XmlInclude(typeof(Motor0StepRelative))]
    [XmlInclude(typeof(Motor1StepRelative))]
    [XmlInclude(typeof(Motor2StepRelative))]
    [XmlInclude(typeof(Motor3StepRelative))]
    [XmlInclude(typeof(StopMotors))]
    [XmlInclude(typeof(ResetEncoders))]
    [XmlInclude(typeof(TimestampedEnableDriver))]
    [XmlInclude(typeof(TimestampedDisableDriver))]
    [XmlInclude(typeof(TimestampedEnableEncoders))]
    [XmlInclude(typeof(TimestampedDisableEncoders))]
    [XmlInclude(typeof(TimestampedEnableDigitalInputs))]
    [XmlInclude(typeof(TimestampedDisableDigitalInputs))]
    [XmlInclude(typeof(TimestampedMotor0OperationMode))]
    [XmlInclude(typeof(TimestampedMotor1OperationMode))]
    [XmlInclude(typeof(TimestampedMotor2OperationMode))]
    [XmlInclude(typeof(TimestampedMotor3OperationMode))]
    [XmlInclude(typeof(TimestampedMotor0MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor1MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor2MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor3MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor0MaximumRunCurrent))]
    [XmlInclude(typeof(TimestampedMotor1MaximumRunCurrent))]
    [XmlInclude(typeof(TimestampedMotor2MaximumRunCurrent))]
    [XmlInclude(typeof(TimestampedMotor3MaximumRunCurrent))]
    [XmlInclude(typeof(TimestampedMotor0HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor1HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor2HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor3HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor0StepInterval))]
    [XmlInclude(typeof(TimestampedMotor1StepInterval))]
    [XmlInclude(typeof(TimestampedMotor2StepInterval))]
    [XmlInclude(typeof(TimestampedMotor3StepInterval))]
    [XmlInclude(typeof(TimestampedMotor0MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor1MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor2MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor3MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor0StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor1StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor2StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor3StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedEncoderMode))]
    [XmlInclude(typeof(TimestampedEncoderSamplingRate))]
    [XmlInclude(typeof(TimestampedInput0OpMode))]
    [XmlInclude(typeof(TimestampedInput1OpMode))]
    [XmlInclude(typeof(TimestampedInput2OpMode))]
    [XmlInclude(typeof(TimestampedInput3OpMode))]
    [XmlInclude(typeof(TimestampedInput0Trigger))]
    [XmlInclude(typeof(TimestampedInput1Trigger))]
    [XmlInclude(typeof(TimestampedInput2Trigger))]
    [XmlInclude(typeof(TimestampedInput3Trigger))]
    [XmlInclude(typeof(TimestampedInterlockEnabled))]
    [XmlInclude(typeof(TimestampedAccumulatedStepsSamplingRate))]
    [XmlInclude(typeof(TimestampedMotorStopped))]
    [XmlInclude(typeof(TimestampedMotorOverVoltageDetection))]
    [XmlInclude(typeof(TimestampedMotorRaisedError))]
    [XmlInclude(typeof(TimestampedEncoders))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedDeviceState))]
    [XmlInclude(typeof(TimestampedMoveRelative))]
    [XmlInclude(typeof(TimestampedMotor0MoveRelative))]
    [XmlInclude(typeof(TimestampedMotor1MoveRelative))]
    [XmlInclude(typeof(TimestampedMotor2MoveRelative))]
    [XmlInclude(typeof(TimestampedMotor3MoveRelative))]
    [XmlInclude(typeof(TimestampedMoveAbsolute))]
    [XmlInclude(typeof(TimestampedMotor0MoveAbsolute))]
    [XmlInclude(typeof(TimestampedMotor1MoveAbsolute))]
    [XmlInclude(typeof(TimestampedMotor2MoveAbsolute))]
    [XmlInclude(typeof(TimestampedMotor3MoveAbsolute))]
    [XmlInclude(typeof(TimestampedAccumulatedSteps))]
    [XmlInclude(typeof(TimestampedMotor0MaxPosition))]
    [XmlInclude(typeof(TimestampedMotor1MaxPosition))]
    [XmlInclude(typeof(TimestampedMotor2MaxPosition))]
    [XmlInclude(typeof(TimestampedMotor3MaxPosition))]
    [XmlInclude(typeof(TimestampedMotor0MinPosition))]
    [XmlInclude(typeof(TimestampedMotor1MinPosition))]
    [XmlInclude(typeof(TimestampedMotor2MinPosition))]
    [XmlInclude(typeof(TimestampedMotor3MinPosition))]
    [XmlInclude(typeof(TimestampedMotor0StepRelative))]
    [XmlInclude(typeof(TimestampedMotor1StepRelative))]
    [XmlInclude(typeof(TimestampedMotor2StepRelative))]
    [XmlInclude(typeof(TimestampedMotor3StepRelative))]
    [XmlInclude(typeof(TimestampedStopMotors))]
    [XmlInclude(typeof(TimestampedResetEncoders))]
    [Description("Filters and selects specific messages reported by the StepperDriver device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new EnableDriver();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// StepperDriver register messages.
    /// </summary>
    /// <seealso cref="EnableDriver"/>
    /// <seealso cref="DisableDriver"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableDigitalInputs"/>
    /// <seealso cref="DisableDigitalInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumRunCurrent"/>
    /// <seealso cref="Motor1MaximumRunCurrent"/>
    /// <seealso cref="Motor2MaximumRunCurrent"/>
    /// <seealso cref="Motor3MaximumRunCurrent"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0StepInterval"/>
    /// <seealso cref="Motor1StepInterval"/>
    /// <seealso cref="Motor2StepInterval"/>
    /// <seealso cref="Motor3StepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderSamplingRate"/>
    /// <seealso cref="Input0OpMode"/>
    /// <seealso cref="Input1OpMode"/>
    /// <seealso cref="Input2OpMode"/>
    /// <seealso cref="Input3OpMode"/>
    /// <seealso cref="Input0Trigger"/>
    /// <seealso cref="Input1Trigger"/>
    /// <seealso cref="Input2Trigger"/>
    /// <seealso cref="Input3Trigger"/>
    /// <seealso cref="InterlockEnabled"/>
    /// <seealso cref="AccumulatedStepsSamplingRate"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOverVoltageDetection"/>
    /// <seealso cref="MotorRaisedError"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="DeviceState"/>
    /// <seealso cref="MoveRelative"/>
    /// <seealso cref="Motor0MoveRelative"/>
    /// <seealso cref="Motor1MoveRelative"/>
    /// <seealso cref="Motor2MoveRelative"/>
    /// <seealso cref="Motor3MoveRelative"/>
    /// <seealso cref="MoveAbsolute"/>
    /// <seealso cref="Motor0MoveAbsolute"/>
    /// <seealso cref="Motor1MoveAbsolute"/>
    /// <seealso cref="Motor2MoveAbsolute"/>
    /// <seealso cref="Motor3MoveAbsolute"/>
    /// <seealso cref="AccumulatedSteps"/>
    /// <seealso cref="Motor0MaxPosition"/>
    /// <seealso cref="Motor1MaxPosition"/>
    /// <seealso cref="Motor2MaxPosition"/>
    /// <seealso cref="Motor3MaxPosition"/>
    /// <seealso cref="Motor0MinPosition"/>
    /// <seealso cref="Motor1MinPosition"/>
    /// <seealso cref="Motor2MinPosition"/>
    /// <seealso cref="Motor3MinPosition"/>
    /// <seealso cref="Motor0StepRelative"/>
    /// <seealso cref="Motor1StepRelative"/>
    /// <seealso cref="Motor2StepRelative"/>
    /// <seealso cref="Motor3StepRelative"/>
    /// <seealso cref="StopMotors"/>
    /// <seealso cref="ResetEncoders"/>
    [XmlInclude(typeof(EnableDriver))]
    [XmlInclude(typeof(DisableDriver))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableDigitalInputs))]
    [XmlInclude(typeof(DisableDigitalInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumRunCurrent))]
    [XmlInclude(typeof(Motor1MaximumRunCurrent))]
    [XmlInclude(typeof(Motor2MaximumRunCurrent))]
    [XmlInclude(typeof(Motor3MaximumRunCurrent))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0StepInterval))]
    [XmlInclude(typeof(Motor1StepInterval))]
    [XmlInclude(typeof(Motor2StepInterval))]
    [XmlInclude(typeof(Motor3StepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderSamplingRate))]
    [XmlInclude(typeof(Input0OpMode))]
    [XmlInclude(typeof(Input1OpMode))]
    [XmlInclude(typeof(Input2OpMode))]
    [XmlInclude(typeof(Input3OpMode))]
    [XmlInclude(typeof(Input0Trigger))]
    [XmlInclude(typeof(Input1Trigger))]
    [XmlInclude(typeof(Input2Trigger))]
    [XmlInclude(typeof(Input3Trigger))]
    [XmlInclude(typeof(InterlockEnabled))]
    [XmlInclude(typeof(AccumulatedStepsSamplingRate))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOverVoltageDetection))]
    [XmlInclude(typeof(MotorRaisedError))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(DeviceState))]
    [XmlInclude(typeof(MoveRelative))]
    [XmlInclude(typeof(Motor0MoveRelative))]
    [XmlInclude(typeof(Motor1MoveRelative))]
    [XmlInclude(typeof(Motor2MoveRelative))]
    [XmlInclude(typeof(Motor3MoveRelative))]
    [XmlInclude(typeof(MoveAbsolute))]
    [XmlInclude(typeof(Motor0MoveAbsolute))]
    [XmlInclude(typeof(Motor1MoveAbsolute))]
    [XmlInclude(typeof(Motor2MoveAbsolute))]
    [XmlInclude(typeof(Motor3MoveAbsolute))]
    [XmlInclude(typeof(AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaxPosition))]
    [XmlInclude(typeof(Motor1MaxPosition))]
    [XmlInclude(typeof(Motor2MaxPosition))]
    [XmlInclude(typeof(Motor3MaxPosition))]
    [XmlInclude(typeof(Motor0MinPosition))]
    [XmlInclude(typeof(Motor1MinPosition))]
    [XmlInclude(typeof(Motor2MinPosition))]
    [XmlInclude(typeof(Motor3MinPosition))]
    [XmlInclude(typeof(Motor0StepRelative))]
    [XmlInclude(typeof(Motor1StepRelative))]
    [XmlInclude(typeof(Motor2StepRelative))]
    [XmlInclude(typeof(Motor3StepRelative))]
    [XmlInclude(typeof(StopMotors))]
    [XmlInclude(typeof(ResetEncoders))]
    [Description("Formats a sequence of values as specific StepperDriver register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new EnableDriver();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
    /// </summary>
    [Description("Enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.")]
    public partial class EnableDriver
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableDriver"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableDriver"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableDriver"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableDriver"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableDriver"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableDriver"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableDriver"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableDriver register.
    /// </summary>
    /// <seealso cref="EnableDriver"/>
    [Description("Filters and selects timestamped messages from the EnableDriver register.")]
    public partial class TimestampedEnableDriver
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableDriver"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableDriver.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return EnableDriver.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
    /// </summary>
    [Description("Disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.")]
    public partial class DisableDriver
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableDriver"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableDriver"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableDriver"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableDriver"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableDriver"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableDriver"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableDriver"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableDriver register.
    /// </summary>
    /// <seealso cref="DisableDriver"/>
    [Description("Filters and selects timestamped messages from the DisableDriver register.")]
    public partial class TimestampedDisableDriver
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableDriver"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableDriver.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableDriver"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return DisableDriver.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of port quadrature counters to enable in the device.
    /// </summary>
    [Description("Specifies a set of port quadrature counters to enable in the device.")]
    public partial class EnableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static QuadratureEncoders GetPayload(HarpMessage message)
        {
            return (QuadratureEncoders)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((QuadratureEncoders)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEncoders"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableEncoders"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEncoders"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableEncoders register.
    /// </summary>
    /// <seealso cref="EnableEncoders"/>
    [Description("Filters and selects timestamped messages from the EnableEncoders register.")]
    public partial class TimestampedEnableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableEncoders.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetPayload(HarpMessage message)
        {
            return EnableEncoders.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of port quadrature counters to disable in the device.
    /// </summary>
    [Description("Specifies a set of port quadrature counters to disable in the device.")]
    public partial class DisableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableEncoders"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableEncoders"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static QuadratureEncoders GetPayload(HarpMessage message)
        {
            return (QuadratureEncoders)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((QuadratureEncoders)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableEncoders"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableEncoders"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableEncoders"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableEncoders"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableEncoders register.
    /// </summary>
    /// <seealso cref="DisableEncoders"/>
    [Description("Filters and selects timestamped messages from the DisableEncoders register.")]
    public partial class TimestampedDisableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableEncoders.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetPayload(HarpMessage message)
        {
            return DisableEncoders.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of digital inputs to enable in the device.
    /// </summary>
    [Description("Specifies a set of digital inputs to enable in the device.")]
    public partial class EnableDigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableDigitalInputs"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableDigitalInputs"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableDigitalInputs"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableDigitalInputs"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableDigitalInputs register.
    /// </summary>
    /// <seealso cref="EnableDigitalInputs"/>
    [Description("Filters and selects timestamped messages from the EnableDigitalInputs register.")]
    public partial class TimestampedEnableDigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableDigitalInputs.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return EnableDigitalInputs.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [Description("Specifies a set of digital inputs to disable in the device.")]
    public partial class DisableDigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableDigitalInputs"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableDigitalInputs"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableDigitalInputs"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableDigitalInputs"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableDigitalInputs register.
    /// </summary>
    /// <seealso cref="DisableDigitalInputs"/>
    [Description("Filters and selects timestamped messages from the DisableDigitalInputs register.")]
    public partial class TimestampedDisableDigitalInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableDigitalInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableDigitalInputs.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableDigitalInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DisableDigitalInputs.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for motor 0.
    /// </summary>
    [Description("Configures the operation mode for motor 0.")]
    public partial class Motor0OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MotorOperationMode GetPayload(HarpMessage message)
        {
            return (MotorOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MotorOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0OperationMode register.
    /// </summary>
    /// <seealso cref="Motor0OperationMode"/>
    [Description("Filters and selects timestamped messages from the Motor0OperationMode register.")]
    public partial class TimestampedMotor0OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetPayload(HarpMessage message)
        {
            return Motor0OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for motor 1.
    /// </summary>
    [Description("Configures the operation mode for motor 1.")]
    public partial class Motor1OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MotorOperationMode GetPayload(HarpMessage message)
        {
            return (MotorOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MotorOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1OperationMode register.
    /// </summary>
    /// <seealso cref="Motor1OperationMode"/>
    [Description("Filters and selects timestamped messages from the Motor1OperationMode register.")]
    public partial class TimestampedMotor1OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetPayload(HarpMessage message)
        {
            return Motor1OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for motor 2.
    /// </summary>
    [Description("Configures the operation mode for motor 2.")]
    public partial class Motor2OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MotorOperationMode GetPayload(HarpMessage message)
        {
            return (MotorOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MotorOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2OperationMode register.
    /// </summary>
    /// <seealso cref="Motor2OperationMode"/>
    [Description("Filters and selects timestamped messages from the Motor2OperationMode register.")]
    public partial class TimestampedMotor2OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetPayload(HarpMessage message)
        {
            return Motor2OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for motor 3.
    /// </summary>
    [Description("Configures the operation mode for motor 3.")]
    public partial class Motor3OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MotorOperationMode GetPayload(HarpMessage message)
        {
            return (MotorOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MotorOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MotorOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3OperationMode register.
    /// </summary>
    /// <seealso cref="Motor3OperationMode"/>
    [Description("Filters and selects timestamped messages from the Motor3OperationMode register.")]
    public partial class TimestampedMotor3OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MotorOperationMode> GetPayload(HarpMessage message)
        {
            return Motor3OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the microstep resolution for motor 0.
    /// </summary>
    [Description("Configures the microstep resolution for motor 0.")]
    public partial class Motor0MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MicrostepResolution GetPayload(HarpMessage message)
        {
            return (MicrostepResolution)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MicrostepResolution)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MicrostepResolution"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MicrostepResolution"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MicrostepResolution"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MicrostepResolution"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MicrostepResolution register.
    /// </summary>
    /// <seealso cref="Motor0MicrostepResolution"/>
    [Description("Filters and selects timestamped messages from the Motor0MicrostepResolution register.")]
    public partial class TimestampedMotor0MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MicrostepResolution.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetPayload(HarpMessage message)
        {
            return Motor0MicrostepResolution.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the microstep resolution for motor 1.
    /// </summary>
    [Description("Configures the microstep resolution for motor 1.")]
    public partial class Motor1MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MicrostepResolution GetPayload(HarpMessage message)
        {
            return (MicrostepResolution)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MicrostepResolution)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MicrostepResolution"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MicrostepResolution"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MicrostepResolution"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MicrostepResolution"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MicrostepResolution register.
    /// </summary>
    /// <seealso cref="Motor1MicrostepResolution"/>
    [Description("Filters and selects timestamped messages from the Motor1MicrostepResolution register.")]
    public partial class TimestampedMotor1MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MicrostepResolution.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetPayload(HarpMessage message)
        {
            return Motor1MicrostepResolution.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the microstep resolution for motor 2.
    /// </summary>
    [Description("Configures the microstep resolution for motor 2.")]
    public partial class Motor2MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MicrostepResolution GetPayload(HarpMessage message)
        {
            return (MicrostepResolution)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MicrostepResolution)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MicrostepResolution"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MicrostepResolution"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MicrostepResolution"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MicrostepResolution"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MicrostepResolution register.
    /// </summary>
    /// <seealso cref="Motor2MicrostepResolution"/>
    [Description("Filters and selects timestamped messages from the Motor2MicrostepResolution register.")]
    public partial class TimestampedMotor2MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MicrostepResolution.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetPayload(HarpMessage message)
        {
            return Motor2MicrostepResolution.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the microstep resolution for motor 3.
    /// </summary>
    [Description("Configures the microstep resolution for motor 3.")]
    public partial class Motor3MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MicrostepResolution GetPayload(HarpMessage message)
        {
            return (MicrostepResolution)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MicrostepResolution)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MicrostepResolution"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MicrostepResolution"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MicrostepResolution"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MicrostepResolution"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MicrostepResolution value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MicrostepResolution register.
    /// </summary>
    /// <seealso cref="Motor3MicrostepResolution"/>
    [Description("Filters and selects timestamped messages from the Motor3MicrostepResolution register.")]
    public partial class TimestampedMotor3MicrostepResolution
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MicrostepResolution"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MicrostepResolution.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MicrostepResolution"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MicrostepResolution> GetPayload(HarpMessage message)
        {
            return Motor3MicrostepResolution.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum run RMS current per phase for motor 0.
    /// </summary>
    [Description("Configures the maximum run RMS current per phase for motor 0.")]
    public partial class Motor0MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MaximumRunCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumRunCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MaximumRunCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumRunCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MaximumRunCurrent register.
    /// </summary>
    /// <seealso cref="Motor0MaximumRunCurrent"/>
    [Description("Filters and selects timestamped messages from the Motor0MaximumRunCurrent register.")]
    public partial class TimestampedMotor0MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MaximumRunCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor0MaximumRunCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum run RMS current per phase for motor 1.
    /// </summary>
    [Description("Configures the maximum run RMS current per phase for motor 1.")]
    public partial class Motor1MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MaximumRunCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumRunCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MaximumRunCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumRunCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MaximumRunCurrent register.
    /// </summary>
    /// <seealso cref="Motor1MaximumRunCurrent"/>
    [Description("Filters and selects timestamped messages from the Motor1MaximumRunCurrent register.")]
    public partial class TimestampedMotor1MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MaximumRunCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor1MaximumRunCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum run RMS current per phase for motor 2.
    /// </summary>
    [Description("Configures the maximum run RMS current per phase for motor 2.")]
    public partial class Motor2MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MaximumRunCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumRunCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MaximumRunCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumRunCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MaximumRunCurrent register.
    /// </summary>
    /// <seealso cref="Motor2MaximumRunCurrent"/>
    [Description("Filters and selects timestamped messages from the Motor2MaximumRunCurrent register.")]
    public partial class TimestampedMotor2MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MaximumRunCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor2MaximumRunCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum run RMS current per phase for motor 3.
    /// </summary>
    [Description("Configures the maximum run RMS current per phase for motor 3.")]
    public partial class Motor3MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MaximumRunCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumRunCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MaximumRunCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumRunCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MaximumRunCurrent register.
    /// </summary>
    /// <seealso cref="Motor3MaximumRunCurrent"/>
    [Description("Filters and selects timestamped messages from the Motor3MaximumRunCurrent register.")]
    public partial class TimestampedMotor3MaximumRunCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumRunCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MaximumRunCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MaximumRunCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor3MaximumRunCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the hold current reduction for motor 0.
    /// </summary>
    [Description("Configures the hold current reduction for motor 0.")]
    public partial class Motor0HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor0HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HoldCurrentReduction GetPayload(HarpMessage message)
        {
            return (HoldCurrentReduction)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HoldCurrentReduction)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0HoldCurrentReduction"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0HoldCurrentReduction"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0HoldCurrentReduction"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0HoldCurrentReduction"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0HoldCurrentReduction register.
    /// </summary>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    [Description("Filters and selects timestamped messages from the Motor0HoldCurrentReduction register.")]
    public partial class TimestampedMotor0HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0HoldCurrentReduction.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetPayload(HarpMessage message)
        {
            return Motor0HoldCurrentReduction.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the hold current reduction for motor 1.
    /// </summary>
    [Description("Configures the hold current reduction for motor 1.")]
    public partial class Motor1HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor1HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HoldCurrentReduction GetPayload(HarpMessage message)
        {
            return (HoldCurrentReduction)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HoldCurrentReduction)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1HoldCurrentReduction"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1HoldCurrentReduction"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1HoldCurrentReduction"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1HoldCurrentReduction"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1HoldCurrentReduction register.
    /// </summary>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    [Description("Filters and selects timestamped messages from the Motor1HoldCurrentReduction register.")]
    public partial class TimestampedMotor1HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1HoldCurrentReduction.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetPayload(HarpMessage message)
        {
            return Motor1HoldCurrentReduction.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the hold current reduction for motor 2.
    /// </summary>
    [Description("Configures the hold current reduction for motor 2.")]
    public partial class Motor2HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor2HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HoldCurrentReduction GetPayload(HarpMessage message)
        {
            return (HoldCurrentReduction)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HoldCurrentReduction)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2HoldCurrentReduction"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2HoldCurrentReduction"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2HoldCurrentReduction"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2HoldCurrentReduction"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2HoldCurrentReduction register.
    /// </summary>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    [Description("Filters and selects timestamped messages from the Motor2HoldCurrentReduction register.")]
    public partial class TimestampedMotor2HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2HoldCurrentReduction.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetPayload(HarpMessage message)
        {
            return Motor2HoldCurrentReduction.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the hold current reduction for motor 3.
    /// </summary>
    [Description("Configures the hold current reduction for motor 3.")]
    public partial class Motor3HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Motor3HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HoldCurrentReduction GetPayload(HarpMessage message)
        {
            return (HoldCurrentReduction)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HoldCurrentReduction)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3HoldCurrentReduction"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3HoldCurrentReduction"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3HoldCurrentReduction"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3HoldCurrentReduction"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HoldCurrentReduction value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3HoldCurrentReduction register.
    /// </summary>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    [Description("Filters and selects timestamped messages from the Motor3HoldCurrentReduction register.")]
    public partial class TimestampedMotor3HoldCurrentReduction
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3HoldCurrentReduction"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3HoldCurrentReduction.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3HoldCurrentReduction"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HoldCurrentReduction> GetPayload(HarpMessage message)
        {
            return Motor3HoldCurrentReduction.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) when running at nominal speed for motor 0.")]
    public partial class Motor0StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0StepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor0StepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0StepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0StepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0StepInterval register.
    /// </summary>
    /// <seealso cref="Motor0StepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor0StepInterval register.")]
    public partial class TimestampedMotor0StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0StepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor0StepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) when running at nominal speed for motor 1.")]
    public partial class Motor1StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1StepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor1StepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1StepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1StepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1StepInterval register.
    /// </summary>
    /// <seealso cref="Motor1StepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor1StepInterval register.")]
    public partial class TimestampedMotor1StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1StepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor1StepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) when running at nominal speed for motor 2.")]
    public partial class Motor2StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2StepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor2StepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2StepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2StepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2StepInterval register.
    /// </summary>
    /// <seealso cref="Motor2StepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor2StepInterval register.")]
    public partial class TimestampedMotor2StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2StepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor2StepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) when running at nominal speed for motor 3.")]
    public partial class Motor3StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3StepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor3StepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3StepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3StepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3StepInterval register.
    /// </summary>
    /// <seealso cref="Motor3StepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor3StepInterval register.")]
    public partial class TimestampedMotor3StepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3StepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3StepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor3StepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.")]
    public partial class Motor0MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MaximumStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MaximumStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MaximumStepInterval register.
    /// </summary>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor0MaximumStepInterval register.")]
    public partial class TimestampedMotor0MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MaximumStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor0MaximumStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.")]
    public partial class Motor1MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MaximumStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MaximumStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MaximumStepInterval register.
    /// </summary>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor1MaximumStepInterval register.")]
    public partial class TimestampedMotor1MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MaximumStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor1MaximumStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.")]
    public partial class Motor2MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MaximumStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MaximumStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MaximumStepInterval register.
    /// </summary>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor2MaximumStepInterval register.")]
    public partial class TimestampedMotor2MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MaximumStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor2MaximumStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
    /// </summary>
    [Description("Configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.")]
    public partial class Motor3MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MaximumStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MaximumStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MaximumStepInterval register.
    /// </summary>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor3MaximumStepInterval register.")]
    public partial class TimestampedMotor3MaximumStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MaximumStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MaximumStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor3MaximumStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [Description("Configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class Motor0StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor0StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0StepAccelerationInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepAccelerationInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0StepAccelerationInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepAccelerationInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0StepAccelerationInterval register.
    /// </summary>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    [Description("Filters and selects timestamped messages from the Motor0StepAccelerationInterval register.")]
    public partial class TimestampedMotor0StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0StepAccelerationInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor0StepAccelerationInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [Description("Configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class Motor1StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor1StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1StepAccelerationInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepAccelerationInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1StepAccelerationInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepAccelerationInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1StepAccelerationInterval register.
    /// </summary>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    [Description("Filters and selects timestamped messages from the Motor1StepAccelerationInterval register.")]
    public partial class TimestampedMotor1StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1StepAccelerationInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor1StepAccelerationInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [Description("Configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class Motor2StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 64;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor2StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2StepAccelerationInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepAccelerationInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2StepAccelerationInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepAccelerationInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2StepAccelerationInterval register.
    /// </summary>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    [Description("Filters and selects timestamped messages from the Motor2StepAccelerationInterval register.")]
    public partial class TimestampedMotor2StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2StepAccelerationInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor2StepAccelerationInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [Description("Configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class Motor3StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor3StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3StepAccelerationInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepAccelerationInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3StepAccelerationInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepAccelerationInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3StepAccelerationInterval register.
    /// </summary>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    [Description("Filters and selects timestamped messages from the Motor3StepAccelerationInterval register.")]
    public partial class TimestampedMotor3StepAccelerationInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepAccelerationInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3StepAccelerationInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3StepAccelerationInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor3StepAccelerationInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode of the quadrature QuadratureEncoders.
    /// </summary>
    [Description("Configures the operation mode of the quadrature QuadratureEncoders.")]
    public partial class EncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 66;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderModeConfig GetPayload(HarpMessage message)
        {
            return (EncoderModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderMode register.
    /// </summary>
    /// <seealso cref="EncoderMode"/>
    [Description("Filters and selects timestamped messages from the EncoderMode register.")]
    public partial class TimestampedEncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModeConfig> GetPayload(HarpMessage message)
        {
            return EncoderMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the reading rate of the QuadratureEncoders' event.
    /// </summary>
    [Description("Configures the reading rate of the QuadratureEncoders' event.")]
    public partial class EncoderSamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderSamplingRate"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderSamplingRateConfig GetPayload(HarpMessage message)
        {
            return (EncoderSamplingRateConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingRateConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderSamplingRateConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderSamplingRate"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSamplingRate"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderSamplingRateConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderSamplingRate"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSamplingRate"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderSamplingRateConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderSamplingRate register.
    /// </summary>
    /// <seealso cref="EncoderSamplingRate"/>
    [Description("Filters and selects timestamped messages from the EncoderSamplingRate register.")]
    public partial class TimestampedEncoderSamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderSamplingRate.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingRateConfig> GetPayload(HarpMessage message)
        {
            return EncoderSamplingRate.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 0.
    /// </summary>
    [Description("Configures the operation mode for digital input 0.")]
    public partial class Input0OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="Input0OpMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input0OpMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input0OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOpModeConfig GetPayload(HarpMessage message)
        {
            return (InputOpModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input0OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOpModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input0OpMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0OpMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input0OpMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0OpMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input0OpMode register.
    /// </summary>
    /// <seealso cref="Input0OpMode"/>
    [Description("Filters and selects timestamped messages from the Input0OpMode register.")]
    public partial class TimestampedInput0OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input0OpMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input0OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetPayload(HarpMessage message)
        {
            return Input0OpMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 1.
    /// </summary>
    [Description("Configures the operation mode for digital input 1.")]
    public partial class Input1OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="Input1OpMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input1OpMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input1OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOpModeConfig GetPayload(HarpMessage message)
        {
            return (InputOpModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input1OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOpModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input1OpMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1OpMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input1OpMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1OpMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input1OpMode register.
    /// </summary>
    /// <seealso cref="Input1OpMode"/>
    [Description("Filters and selects timestamped messages from the Input1OpMode register.")]
    public partial class TimestampedInput1OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input1OpMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input1OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetPayload(HarpMessage message)
        {
            return Input1OpMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 2.
    /// </summary>
    [Description("Configures the operation mode for digital input 2.")]
    public partial class Input2OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="Input2OpMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input2OpMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input2OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOpModeConfig GetPayload(HarpMessage message)
        {
            return (InputOpModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input2OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOpModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input2OpMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2OpMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input2OpMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2OpMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input2OpMode register.
    /// </summary>
    /// <seealso cref="Input2OpMode"/>
    [Description("Filters and selects timestamped messages from the Input2OpMode register.")]
    public partial class TimestampedInput2OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input2OpMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input2OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetPayload(HarpMessage message)
        {
            return Input2OpMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 3.
    /// </summary>
    [Description("Configures the operation mode for digital input 3.")]
    public partial class Input3OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="Input3OpMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input3OpMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input3OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOpModeConfig GetPayload(HarpMessage message)
        {
            return (InputOpModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input3OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOpModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input3OpMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3OpMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input3OpMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3OpMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOpModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input3OpMode register.
    /// </summary>
    /// <seealso cref="Input3OpMode"/>
    [Description("Filters and selects timestamped messages from the Input3OpMode register.")]
    public partial class TimestampedInput3OpMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3OpMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input3OpMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input3OpMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOpModeConfig> GetPayload(HarpMessage message)
        {
            return Input3OpMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the trigger mode for digital input 0.
    /// </summary>
    [Description("Configures the trigger mode for digital input 0.")]
    public partial class Input0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="Input0Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input0Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input0Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input0Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input0Trigger register.
    /// </summary>
    /// <seealso cref="Input0Trigger"/>
    [Description("Filters and selects timestamped messages from the Input0Trigger register.")]
    public partial class TimestampedInput0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Input0Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return Input0Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the trigger mode for digital input 1.
    /// </summary>
    [Description("Configures the trigger mode for digital input 1.")]
    public partial class Input1Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="Input1Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input1Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input1Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input1Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input1Trigger register.
    /// </summary>
    /// <seealso cref="Input1Trigger"/>
    [Description("Filters and selects timestamped messages from the Input1Trigger register.")]
    public partial class TimestampedInput1Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Input1Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input1Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return Input1Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the trigger mode for digital input 2.
    /// </summary>
    [Description("Configures the trigger mode for digital input 2.")]
    public partial class Input2Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="Input2Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input2Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input2Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input2Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input2Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input2Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input2Trigger register.
    /// </summary>
    /// <seealso cref="Input2Trigger"/>
    [Description("Filters and selects timestamped messages from the Input2Trigger register.")]
    public partial class TimestampedInput2Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Input2Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input2Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return Input2Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the trigger mode for digital input 3.
    /// </summary>
    [Description("Configures the trigger mode for digital input 3.")]
    public partial class Input3Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="Input3Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input3Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input3Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input3Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input3Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input3Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input3Trigger register.
    /// </summary>
    /// <seealso cref="Input3Trigger"/>
    [Description("Filters and selects timestamped messages from the Input3Trigger register.")]
    public partial class TimestampedInput3Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = Input3Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input3Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return Input3Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the external interlock connector state required for the device to be enabled.
    /// </summary>
    [Description("Configures the external interlock connector state required for the device to be enabled.")]
    public partial class InterlockEnabled
    {
        /// <summary>
        /// Represents the address of the <see cref="InterlockEnabled"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="InterlockEnabled"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="InterlockEnabled"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InterlockEnabled"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InterlockEnabledConfig GetPayload(HarpMessage message)
        {
            return (InterlockEnabledConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InterlockEnabled"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InterlockEnabledConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InterlockEnabledConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InterlockEnabled"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InterlockEnabled"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InterlockEnabledConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InterlockEnabled"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InterlockEnabled"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InterlockEnabledConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InterlockEnabled register.
    /// </summary>
    /// <seealso cref="InterlockEnabled"/>
    [Description("Filters and selects timestamped messages from the InterlockEnabled register.")]
    public partial class TimestampedInterlockEnabled
    {
        /// <summary>
        /// Represents the address of the <see cref="InterlockEnabled"/> register. This field is constant.
        /// </summary>
        public const int Address = InterlockEnabled.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InterlockEnabled"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InterlockEnabledConfig> GetPayload(HarpMessage message)
        {
            return InterlockEnabled.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the dispatch rate of the accumulated steps event.
    /// </summary>
    [Description("Configures the dispatch rate of the accumulated steps event.")]
    public partial class AccumulatedStepsSamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="AccumulatedStepsSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="AccumulatedStepsSamplingRate"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AccumulatedStepsSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AccumulatedStepsSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AccumulatedStepsSamplingRateConfig GetPayload(HarpMessage message)
        {
            return (AccumulatedStepsSamplingRateConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AccumulatedStepsSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AccumulatedStepsSamplingRateConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AccumulatedStepsSamplingRateConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AccumulatedStepsSamplingRate"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AccumulatedStepsSamplingRate"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AccumulatedStepsSamplingRateConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AccumulatedStepsSamplingRate"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AccumulatedStepsSamplingRate"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AccumulatedStepsSamplingRateConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AccumulatedStepsSamplingRate register.
    /// </summary>
    /// <seealso cref="AccumulatedStepsSamplingRate"/>
    [Description("Filters and selects timestamped messages from the AccumulatedStepsSamplingRate register.")]
    public partial class TimestampedAccumulatedStepsSamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="AccumulatedStepsSamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = AccumulatedStepsSamplingRate.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AccumulatedStepsSamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AccumulatedStepsSamplingRateConfig> GetPayload(HarpMessage message)
        {
            return AccumulatedStepsSamplingRate.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [Description("Emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.")]
    public partial class MotorStopped
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorStopped"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="MotorStopped"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MotorStopped"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MotorStopped"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MotorStopped"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MotorStopped"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorStopped"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MotorStopped"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorStopped"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MotorStopped register.
    /// </summary>
    /// <seealso cref="MotorStopped"/>
    [Description("Filters and selects timestamped messages from the MotorStopped register.")]
    public partial class TimestampedMotorStopped
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorStopped"/> register. This field is constant.
        /// </summary>
        public const int Address = MotorStopped.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MotorStopped"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return MotorStopped.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
    /// </summary>
    [Description("Contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).")]
    public partial class MotorOverVoltageDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorOverVoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="MotorOverVoltageDetection"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MotorOverVoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MotorOverVoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MotorOverVoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MotorOverVoltageDetection"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorOverVoltageDetection"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MotorOverVoltageDetection"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorOverVoltageDetection"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MotorOverVoltageDetection register.
    /// </summary>
    /// <seealso cref="MotorOverVoltageDetection"/>
    [Description("Filters and selects timestamped messages from the MotorOverVoltageDetection register.")]
    public partial class TimestampedMotorOverVoltageDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorOverVoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = MotorOverVoltageDetection.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MotorOverVoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return MotorOverVoltageDetection.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
    /// </summary>
    [Description("Contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.")]
    public partial class MotorRaisedError
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorRaisedError"/> register. This field is constant.
        /// </summary>
        public const int Address = 80;

        /// <summary>
        /// Represents the payload type of the <see cref="MotorRaisedError"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MotorRaisedError"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MotorRaisedError"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MotorRaisedError"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MotorRaisedError"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorRaisedError"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MotorRaisedError"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorRaisedError"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MotorRaisedError register.
    /// </summary>
    /// <seealso cref="MotorRaisedError"/>
    [Description("Filters and selects timestamped messages from the MotorRaisedError register.")]
    public partial class TimestampedMotorRaisedError
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorRaisedError"/> register. This field is constant.
        /// </summary>
        public const int Address = MotorRaisedError.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MotorRaisedError"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return MotorRaisedError.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the quadrature encoder readings.
    /// </summary>
    [Description("Contains the quadrature encoder readings.")]
    public partial class Encoders
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoders"/> register. This field is constant.
        /// </summary>
        public const int Address = 81;

        /// <summary>
        /// Represents the payload type of the <see cref="Encoders"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="Encoders"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 3;

        static EncodersPayload ParsePayload(short[] payload)
        {
            EncodersPayload result;
            result.Encoder0 = payload[0];
            result.Encoder1 = payload[1];
            result.Encoder2 = payload[2];
            return result;
        }

        static short[] FormatPayload(EncodersPayload value)
        {
            short[] result;
            result = new short[3];
            result[0] = value.Encoder0;
            result[1] = value.Encoder1;
            result[2] = value.Encoder2;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="Encoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncodersPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<short>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Encoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncodersPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<short>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Encoders"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoders"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncodersPayload value)
        {
            return HarpMessage.FromInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Encoders"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoders"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncodersPayload value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Encoders register.
    /// </summary>
    /// <seealso cref="Encoders"/>
    [Description("Filters and selects timestamped messages from the Encoders register.")]
    public partial class TimestampedEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoders"/> register. This field is constant.
        /// </summary>
        public const int Address = Encoders.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Encoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncodersPayload> GetPayload(HarpMessage message)
        {
            return Encoders.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reflects the state of the digital input lines.
    /// </summary>
    [Description("Reflects the state of the digital input lines.")]
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the state of the device.
    /// </summary>
    [Description("Contains the state of the device.")]
    public partial class DeviceState
    {
        /// <summary>
        /// Represents the address of the <see cref="DeviceState"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="DeviceState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DeviceState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DeviceState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DeviceStateMode GetPayload(HarpMessage message)
        {
            return (DeviceStateMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DeviceState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DeviceStateMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DeviceStateMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DeviceState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DeviceState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DeviceStateMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DeviceState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DeviceState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DeviceStateMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DeviceState register.
    /// </summary>
    /// <seealso cref="DeviceState"/>
    [Description("Filters and selects timestamped messages from the DeviceState register.")]
    public partial class TimestampedDeviceState
    {
        /// <summary>
        /// Represents the address of the <see cref="DeviceState"/> register. This field is constant.
        /// </summary>
        public const int Address = DeviceState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DeviceState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DeviceStateMode> GetPayload(HarpMessage message)
        {
            return DeviceState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [Description("Moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.")]
    public partial class MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="MoveRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 4;

        static MoveRelativePayload ParsePayload(int[] payload)
        {
            MoveRelativePayload result;
            result.Motor0 = payload[0];
            result.Motor1 = payload[1];
            result.Motor2 = payload[2];
            result.Motor3 = payload[3];
            return result;
        }

        static int[] FormatPayload(MoveRelativePayload value)
        {
            int[] result;
            result = new int[4];
            result[0] = value.Motor0;
            result[1] = value.Motor1;
            result[2] = value.Motor2;
            result[3] = value.Motor3;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MoveRelativePayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<int>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MoveRelativePayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<int>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MoveRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MoveRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MoveRelativePayload value)
        {
            return HarpMessage.FromInt32(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MoveRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MoveRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MoveRelativePayload value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MoveRelative register.
    /// </summary>
    /// <seealso cref="MoveRelative"/>
    [Description("Filters and selects timestamped messages from the MoveRelative register.")]
    public partial class TimestampedMoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = MoveRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MoveRelativePayload> GetPayload(HarpMessage message)
        {
            return MoveRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor0MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MoveRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MoveRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MoveRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MoveRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MoveRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MoveRelative register.
    /// </summary>
    /// <seealso cref="Motor0MoveRelative"/>
    [Description("Filters and selects timestamped messages from the Motor0MoveRelative register.")]
    public partial class TimestampedMotor0MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MoveRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MoveRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor1MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MoveRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MoveRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MoveRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MoveRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MoveRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MoveRelative register.
    /// </summary>
    /// <seealso cref="Motor1MoveRelative"/>
    [Description("Filters and selects timestamped messages from the Motor1MoveRelative register.")]
    public partial class TimestampedMotor1MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MoveRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MoveRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor2MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MoveRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MoveRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MoveRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MoveRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MoveRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MoveRelative register.
    /// </summary>
    /// <seealso cref="Motor2MoveRelative"/>
    [Description("Filters and selects timestamped messages from the Motor2MoveRelative register.")]
    public partial class TimestampedMotor2MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MoveRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MoveRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor3MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 88;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MoveRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MoveRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MoveRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MoveRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MoveRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MoveRelative register.
    /// </summary>
    /// <seealso cref="Motor3MoveRelative"/>
    [Description("Filters and selects timestamped messages from the Motor3MoveRelative register.")]
    public partial class TimestampedMotor3MoveRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MoveRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MoveRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MoveRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MoveRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [Description("Moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.")]
    public partial class MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = 89;

        /// <summary>
        /// Represents the payload type of the <see cref="MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 4;

        static MoveAbsolutePayload ParsePayload(int[] payload)
        {
            MoveAbsolutePayload result;
            result.Motor0 = payload[0];
            result.Motor1 = payload[1];
            result.Motor2 = payload[2];
            result.Motor3 = payload[3];
            return result;
        }

        static int[] FormatPayload(MoveAbsolutePayload value)
        {
            int[] result;
            result = new int[4];
            result[0] = value.Motor0;
            result[1] = value.Motor1;
            result[2] = value.Motor2;
            result[3] = value.Motor3;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MoveAbsolutePayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<int>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MoveAbsolutePayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<int>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MoveAbsolute"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MoveAbsolute"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MoveAbsolutePayload value)
        {
            return HarpMessage.FromInt32(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MoveAbsolute"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MoveAbsolute"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MoveAbsolutePayload value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MoveAbsolute register.
    /// </summary>
    /// <seealso cref="MoveAbsolute"/>
    [Description("Filters and selects timestamped messages from the MoveAbsolute register.")]
    public partial class TimestampedMoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = MoveAbsolute.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MoveAbsolutePayload> GetPayload(HarpMessage message)
        {
            return MoveAbsolute.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 0 to the absolute position written in this register.
    /// </summary>
    [Description("Moves motor 0 to the absolute position written in this register.")]
    public partial class Motor0MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MoveAbsolute"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MoveAbsolute"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MoveAbsolute"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MoveAbsolute"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MoveAbsolute register.
    /// </summary>
    /// <seealso cref="Motor0MoveAbsolute"/>
    [Description("Filters and selects timestamped messages from the Motor0MoveAbsolute register.")]
    public partial class TimestampedMotor0MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MoveAbsolute.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MoveAbsolute.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 1 to the absolute position written in this register.
    /// </summary>
    [Description("Moves motor 1 to the absolute position written in this register.")]
    public partial class Motor1MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = 91;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MoveAbsolute"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MoveAbsolute"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MoveAbsolute"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MoveAbsolute"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MoveAbsolute register.
    /// </summary>
    /// <seealso cref="Motor1MoveAbsolute"/>
    [Description("Filters and selects timestamped messages from the Motor1MoveAbsolute register.")]
    public partial class TimestampedMotor1MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MoveAbsolute.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MoveAbsolute.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 2 to the absolute position written in this register.
    /// </summary>
    [Description("Moves motor 2 to the absolute position written in this register.")]
    public partial class Motor2MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = 92;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MoveAbsolute"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MoveAbsolute"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MoveAbsolute"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MoveAbsolute"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MoveAbsolute register.
    /// </summary>
    /// <seealso cref="Motor2MoveAbsolute"/>
    [Description("Filters and selects timestamped messages from the Motor2MoveAbsolute register.")]
    public partial class TimestampedMotor2MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MoveAbsolute.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MoveAbsolute.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 3 to the absolute position written in this register.
    /// </summary>
    [Description("Moves motor 3 to the absolute position written in this register.")]
    public partial class Motor3MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = 93;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MoveAbsolute"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MoveAbsolute"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MoveAbsolute"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MoveAbsolute"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MoveAbsolute register.
    /// </summary>
    /// <seealso cref="Motor3MoveAbsolute"/>
    [Description("Filters and selects timestamped messages from the Motor3MoveAbsolute register.")]
    public partial class TimestampedMotor3MoveAbsolute
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MoveAbsolute"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MoveAbsolute.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MoveAbsolute"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MoveAbsolute.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the accumulated steps of all motors.
    /// </summary>
    [Description("Contains the accumulated steps of all motors.")]
    public partial class AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 94;

        /// <summary>
        /// Represents the payload type of the <see cref="AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 4;

        static AccumulatedStepsPayload ParsePayload(int[] payload)
        {
            AccumulatedStepsPayload result;
            result.Motor0 = payload[0];
            result.Motor1 = payload[1];
            result.Motor2 = payload[2];
            result.Motor3 = payload[3];
            return result;
        }

        static int[] FormatPayload(AccumulatedStepsPayload value)
        {
            int[] result;
            result = new int[4];
            result[0] = value.Motor0;
            result[1] = value.Motor1;
            result[2] = value.Motor2;
            result[3] = value.Motor3;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AccumulatedStepsPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<int>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AccumulatedStepsPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<int>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AccumulatedSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AccumulatedSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AccumulatedStepsPayload value)
        {
            return HarpMessage.FromInt32(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AccumulatedSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AccumulatedSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AccumulatedStepsPayload value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AccumulatedSteps register.
    /// </summary>
    /// <seealso cref="AccumulatedSteps"/>
    [Description("Filters and selects timestamped messages from the AccumulatedSteps register.")]
    public partial class TimestampedAccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = AccumulatedSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AccumulatedStepsPayload> GetPayload(HarpMessage message)
        {
            return AccumulatedSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor0MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 95;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MaxPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MaxPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaxPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MaxPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaxPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MaxPosition register.
    /// </summary>
    /// <seealso cref="Motor0MaxPosition"/>
    [Description("Filters and selects timestamped messages from the Motor0MaxPosition register.")]
    public partial class TimestampedMotor0MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MaxPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MaxPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor1MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 96;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MaxPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MaxPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaxPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MaxPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaxPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MaxPosition register.
    /// </summary>
    /// <seealso cref="Motor1MaxPosition"/>
    [Description("Filters and selects timestamped messages from the Motor1MaxPosition register.")]
    public partial class TimestampedMotor1MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MaxPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MaxPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor2MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 97;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MaxPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MaxPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaxPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MaxPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaxPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MaxPosition register.
    /// </summary>
    /// <seealso cref="Motor2MaxPosition"/>
    [Description("Filters and selects timestamped messages from the Motor2MaxPosition register.")]
    public partial class TimestampedMotor2MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MaxPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MaxPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor3MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 98;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MaxPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MaxPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaxPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MaxPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaxPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MaxPosition register.
    /// </summary>
    /// <seealso cref="Motor3MaxPosition"/>
    [Description("Filters and selects timestamped messages from the Motor3MaxPosition register.")]
    public partial class TimestampedMotor3MaxPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaxPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MaxPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MaxPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MaxPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor0MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 99;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MinPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MinPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MinPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MinPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MinPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MinPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MinPosition register.
    /// </summary>
    /// <seealso cref="Motor0MinPosition"/>
    [Description("Filters and selects timestamped messages from the Motor0MinPosition register.")]
    public partial class TimestampedMotor0MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MinPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MinPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor1MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 100;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MinPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MinPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MinPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MinPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MinPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MinPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MinPosition register.
    /// </summary>
    /// <seealso cref="Motor1MinPosition"/>
    [Description("Filters and selects timestamped messages from the Motor1MinPosition register.")]
    public partial class TimestampedMotor1MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MinPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MinPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor2MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 101;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MinPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MinPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MinPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MinPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MinPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MinPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MinPosition register.
    /// </summary>
    /// <seealso cref="Motor2MinPosition"/>
    [Description("Filters and selects timestamped messages from the Motor2MinPosition register.")]
    public partial class TimestampedMotor2MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MinPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MinPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor3MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = 102;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MinPosition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MinPosition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MinPosition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MinPosition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MinPosition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MinPosition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MinPosition register.
    /// </summary>
    /// <seealso cref="Motor3MinPosition"/>
    [Description("Filters and selects timestamped messages from the Motor3MinPosition register.")]
    public partial class TimestampedMotor3MinPosition
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MinPosition"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MinPosition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MinPosition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MinPosition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class Motor0StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 103;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0StepRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0StepRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0StepRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0StepRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0StepRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0StepRelative register.
    /// </summary>
    /// <seealso cref="Motor0StepRelative"/>
    [Description("Filters and selects timestamped messages from the Motor0StepRelative register.")]
    public partial class TimestampedMotor0StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0StepRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0StepRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class Motor1StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 104;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1StepRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1StepRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1StepRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1StepRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1StepRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1StepRelative register.
    /// </summary>
    /// <seealso cref="Motor1StepRelative"/>
    [Description("Filters and selects timestamped messages from the Motor1StepRelative register.")]
    public partial class TimestampedMotor1StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1StepRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1StepRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class Motor2StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 105;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2StepRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2StepRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2StepRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2StepRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2StepRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2StepRelative register.
    /// </summary>
    /// <seealso cref="Motor2StepRelative"/>
    [Description("Filters and selects timestamped messages from the Motor2StepRelative register.")]
    public partial class TimestampedMotor2StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2StepRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2StepRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class Motor3StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = 106;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3StepRelative"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3StepRelative"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3StepRelative"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepRelative"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3StepRelative"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3StepRelative"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3StepRelative register.
    /// </summary>
    /// <seealso cref="Motor3StepRelative"/>
    [Description("Filters and selects timestamped messages from the Motor3StepRelative register.")]
    public partial class TimestampedMotor3StepRelative
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3StepRelative"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3StepRelative.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3StepRelative"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3StepRelative.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the motors selected in the bit-mask immediately.
    /// </summary>
    [Description("Stops the motors selected in the bit-mask immediately.")]
    public partial class StopMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = 107;

        /// <summary>
        /// Represents the payload type of the <see cref="StopMotors"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StopMotors"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StopMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StopMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StopMotors"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotors"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StopMotors"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotors"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StopMotors register.
    /// </summary>
    /// <seealso cref="StopMotors"/>
    [Description("Filters and selects timestamped messages from the StopMotors register.")]
    public partial class TimestampedStopMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = StopMotors.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StopMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return StopMotors.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that resets the encoder.
    /// </summary>
    [Description("Resets the encoder.")]
    public partial class ResetEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = 109;

        /// <summary>
        /// Represents the payload type of the <see cref="ResetEncoders"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ResetEncoders"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ResetEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static QuadratureEncoders GetPayload(HarpMessage message)
        {
            return (QuadratureEncoders)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ResetEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((QuadratureEncoders)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ResetEncoders"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetEncoders"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ResetEncoders"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetEncoders"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ResetEncoders register.
    /// </summary>
    /// <seealso cref="ResetEncoders"/>
    [Description("Filters and selects timestamped messages from the ResetEncoders register.")]
    public partial class TimestampedResetEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = ResetEncoders.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ResetEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetPayload(HarpMessage message)
        {
            return ResetEncoders.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// StepperDriver device.
    /// </summary>
    /// <seealso cref="CreateEnableDriverPayload"/>
    /// <seealso cref="CreateDisableDriverPayload"/>
    /// <seealso cref="CreateEnableEncodersPayload"/>
    /// <seealso cref="CreateDisableEncodersPayload"/>
    /// <seealso cref="CreateEnableDigitalInputsPayload"/>
    /// <seealso cref="CreateDisableDigitalInputsPayload"/>
    /// <seealso cref="CreateMotor0OperationModePayload"/>
    /// <seealso cref="CreateMotor1OperationModePayload"/>
    /// <seealso cref="CreateMotor2OperationModePayload"/>
    /// <seealso cref="CreateMotor3OperationModePayload"/>
    /// <seealso cref="CreateMotor0MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor1MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor2MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor3MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor0MaximumRunCurrentPayload"/>
    /// <seealso cref="CreateMotor1MaximumRunCurrentPayload"/>
    /// <seealso cref="CreateMotor2MaximumRunCurrentPayload"/>
    /// <seealso cref="CreateMotor3MaximumRunCurrentPayload"/>
    /// <seealso cref="CreateMotor0HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor1HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor2HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor3HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor0StepIntervalPayload"/>
    /// <seealso cref="CreateMotor1StepIntervalPayload"/>
    /// <seealso cref="CreateMotor2StepIntervalPayload"/>
    /// <seealso cref="CreateMotor3StepIntervalPayload"/>
    /// <seealso cref="CreateMotor0MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor1MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor2MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor3MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor0StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor1StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor2StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor3StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateEncoderModePayload"/>
    /// <seealso cref="CreateEncoderSamplingRatePayload"/>
    /// <seealso cref="CreateInput0OpModePayload"/>
    /// <seealso cref="CreateInput1OpModePayload"/>
    /// <seealso cref="CreateInput2OpModePayload"/>
    /// <seealso cref="CreateInput3OpModePayload"/>
    /// <seealso cref="CreateInput0TriggerPayload"/>
    /// <seealso cref="CreateInput1TriggerPayload"/>
    /// <seealso cref="CreateInput2TriggerPayload"/>
    /// <seealso cref="CreateInput3TriggerPayload"/>
    /// <seealso cref="CreateInterlockEnabledPayload"/>
    /// <seealso cref="CreateAccumulatedStepsSamplingRatePayload"/>
    /// <seealso cref="CreateMotorStoppedPayload"/>
    /// <seealso cref="CreateMotorOverVoltageDetectionPayload"/>
    /// <seealso cref="CreateMotorRaisedErrorPayload"/>
    /// <seealso cref="CreateEncodersPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateDeviceStatePayload"/>
    /// <seealso cref="CreateMoveRelativePayload"/>
    /// <seealso cref="CreateMotor0MoveRelativePayload"/>
    /// <seealso cref="CreateMotor1MoveRelativePayload"/>
    /// <seealso cref="CreateMotor2MoveRelativePayload"/>
    /// <seealso cref="CreateMotor3MoveRelativePayload"/>
    /// <seealso cref="CreateMoveAbsolutePayload"/>
    /// <seealso cref="CreateMotor0MoveAbsolutePayload"/>
    /// <seealso cref="CreateMotor1MoveAbsolutePayload"/>
    /// <seealso cref="CreateMotor2MoveAbsolutePayload"/>
    /// <seealso cref="CreateMotor3MoveAbsolutePayload"/>
    /// <seealso cref="CreateAccumulatedStepsPayload"/>
    /// <seealso cref="CreateMotor0MaxPositionPayload"/>
    /// <seealso cref="CreateMotor1MaxPositionPayload"/>
    /// <seealso cref="CreateMotor2MaxPositionPayload"/>
    /// <seealso cref="CreateMotor3MaxPositionPayload"/>
    /// <seealso cref="CreateMotor0MinPositionPayload"/>
    /// <seealso cref="CreateMotor1MinPositionPayload"/>
    /// <seealso cref="CreateMotor2MinPositionPayload"/>
    /// <seealso cref="CreateMotor3MinPositionPayload"/>
    /// <seealso cref="CreateMotor0StepRelativePayload"/>
    /// <seealso cref="CreateMotor1StepRelativePayload"/>
    /// <seealso cref="CreateMotor2StepRelativePayload"/>
    /// <seealso cref="CreateMotor3StepRelativePayload"/>
    /// <seealso cref="CreateStopMotorsPayload"/>
    /// <seealso cref="CreateResetEncodersPayload"/>
    [XmlInclude(typeof(CreateEnableDriverPayload))]
    [XmlInclude(typeof(CreateDisableDriverPayload))]
    [XmlInclude(typeof(CreateEnableEncodersPayload))]
    [XmlInclude(typeof(CreateDisableEncodersPayload))]
    [XmlInclude(typeof(CreateEnableDigitalInputsPayload))]
    [XmlInclude(typeof(CreateDisableDigitalInputsPayload))]
    [XmlInclude(typeof(CreateMotor0OperationModePayload))]
    [XmlInclude(typeof(CreateMotor1OperationModePayload))]
    [XmlInclude(typeof(CreateMotor2OperationModePayload))]
    [XmlInclude(typeof(CreateMotor3OperationModePayload))]
    [XmlInclude(typeof(CreateMotor0MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor1MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor2MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor3MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor0MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateMotor1MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateMotor2MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateMotor3MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateMotor0HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor1HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor2HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor3HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor0StepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1StepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2StepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3StepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor0MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor0StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateEncoderModePayload))]
    [XmlInclude(typeof(CreateEncoderSamplingRatePayload))]
    [XmlInclude(typeof(CreateInput0OpModePayload))]
    [XmlInclude(typeof(CreateInput1OpModePayload))]
    [XmlInclude(typeof(CreateInput2OpModePayload))]
    [XmlInclude(typeof(CreateInput3OpModePayload))]
    [XmlInclude(typeof(CreateInput0TriggerPayload))]
    [XmlInclude(typeof(CreateInput1TriggerPayload))]
    [XmlInclude(typeof(CreateInput2TriggerPayload))]
    [XmlInclude(typeof(CreateInput3TriggerPayload))]
    [XmlInclude(typeof(CreateInterlockEnabledPayload))]
    [XmlInclude(typeof(CreateAccumulatedStepsSamplingRatePayload))]
    [XmlInclude(typeof(CreateMotorStoppedPayload))]
    [XmlInclude(typeof(CreateMotorOverVoltageDetectionPayload))]
    [XmlInclude(typeof(CreateMotorRaisedErrorPayload))]
    [XmlInclude(typeof(CreateEncodersPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateDeviceStatePayload))]
    [XmlInclude(typeof(CreateMoveRelativePayload))]
    [XmlInclude(typeof(CreateMotor0MoveRelativePayload))]
    [XmlInclude(typeof(CreateMotor1MoveRelativePayload))]
    [XmlInclude(typeof(CreateMotor2MoveRelativePayload))]
    [XmlInclude(typeof(CreateMotor3MoveRelativePayload))]
    [XmlInclude(typeof(CreateMoveAbsolutePayload))]
    [XmlInclude(typeof(CreateMotor0MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateMotor1MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateMotor2MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateMotor3MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateAccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateMotor0MaxPositionPayload))]
    [XmlInclude(typeof(CreateMotor1MaxPositionPayload))]
    [XmlInclude(typeof(CreateMotor2MaxPositionPayload))]
    [XmlInclude(typeof(CreateMotor3MaxPositionPayload))]
    [XmlInclude(typeof(CreateMotor0MinPositionPayload))]
    [XmlInclude(typeof(CreateMotor1MinPositionPayload))]
    [XmlInclude(typeof(CreateMotor2MinPositionPayload))]
    [XmlInclude(typeof(CreateMotor3MinPositionPayload))]
    [XmlInclude(typeof(CreateMotor0StepRelativePayload))]
    [XmlInclude(typeof(CreateMotor1StepRelativePayload))]
    [XmlInclude(typeof(CreateMotor2StepRelativePayload))]
    [XmlInclude(typeof(CreateMotor3StepRelativePayload))]
    [XmlInclude(typeof(CreateStopMotorsPayload))]
    [XmlInclude(typeof(CreateResetEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableDriverPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableDriverPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableDigitalInputsPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableDigitalInputsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaximumRunCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0StepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1StepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2StepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3StepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderModePayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderSamplingRatePayload))]
    [XmlInclude(typeof(CreateTimestampedInput0OpModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput1OpModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput2OpModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput3OpModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput0TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedInput1TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedInput2TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedInput3TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedInterlockEnabledPayload))]
    [XmlInclude(typeof(CreateTimestampedAccumulatedStepsSamplingRatePayload))]
    [XmlInclude(typeof(CreateTimestampedMotorStoppedPayload))]
    [XmlInclude(typeof(CreateTimestampedMotorOverVoltageDetectionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotorRaisedErrorPayload))]
    [XmlInclude(typeof(CreateTimestampedEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedDeviceStatePayload))]
    [XmlInclude(typeof(CreateTimestampedMoveRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MoveRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MoveRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MoveRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MoveRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMoveAbsolutePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MoveAbsolutePayload))]
    [XmlInclude(typeof(CreateTimestampedAccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaxPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaxPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaxPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaxPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MinPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MinPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MinPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MinPositionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0StepRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1StepRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2StepRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3StepRelativePayload))]
    [XmlInclude(typeof(CreateTimestampedStopMotorsPayload))]
    [XmlInclude(typeof(CreateTimestampedResetEncodersPayload))]
    [Description("Creates standard message payloads for the StepperDriver device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateEnableDriverPayload();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
    /// </summary>
    [DisplayName("EnableDriverPayload")]
    [Description("Creates a message payload that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.")]
    public partial class CreateEnableDriverPayload
    {
        /// <summary>
        /// Gets or sets the value that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
        /// </summary>
        [Description("The value that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.")]
        public StepperMotors EnableDriver { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableDriver register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return EnableDriver;
        }

        /// <summary>
        /// Creates a message that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableDriver register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EnableDriver.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
    /// </summary>
    [DisplayName("TimestampedEnableDriverPayload")]
    [Description("Creates a timestamped message payload that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.")]
    public partial class CreateTimestampedEnableDriverPayload : CreateEnableDriverPayload
    {
        /// <summary>
        /// Creates a timestamped message that enables the driver for a specific motor. If the driver is already enabled, the driver remains enabled.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableDriver register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EnableDriver.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
    /// </summary>
    [DisplayName("DisableDriverPayload")]
    [Description("Creates a message payload that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.")]
    public partial class CreateDisableDriverPayload
    {
        /// <summary>
        /// Gets or sets the value that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
        /// </summary>
        [Description("The value that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.")]
        public StepperMotors DisableDriver { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableDriver register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return DisableDriver;
        }

        /// <summary>
        /// Creates a message that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableDriver register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DisableDriver.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
    /// </summary>
    [DisplayName("TimestampedDisableDriverPayload")]
    [Description("Creates a timestamped message payload that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.")]
    public partial class CreateTimestampedDisableDriverPayload : CreateDisableDriverPayload
    {
        /// <summary>
        /// Creates a timestamped message that disables the driver for a specific stepper motor, which prevents current from being sent to the motor or load. If the driver is already disabled, the driver remains disabled.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableDriver register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DisableDriver.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of port quadrature counters to enable in the device.
    /// </summary>
    [DisplayName("EnableEncodersPayload")]
    [Description("Creates a message payload that specifies a set of port quadrature counters to enable in the device.")]
    public partial class CreateEnableEncodersPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of port quadrature counters to enable in the device.
        /// </summary>
        [Description("The value that specifies a set of port quadrature counters to enable in the device.")]
        public QuadratureEncoders EnableEncoders { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableEncoders register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public QuadratureEncoders GetPayload()
        {
            return EnableEncoders;
        }

        /// <summary>
        /// Creates a message that specifies a set of port quadrature counters to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableEncoders register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EnableEncoders.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of port quadrature counters to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableEncodersPayload")]
    [Description("Creates a timestamped message payload that specifies a set of port quadrature counters to enable in the device.")]
    public partial class CreateTimestampedEnableEncodersPayload : CreateEnableEncodersPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of port quadrature counters to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableEncoders register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EnableEncoders.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of port quadrature counters to disable in the device.
    /// </summary>
    [DisplayName("DisableEncodersPayload")]
    [Description("Creates a message payload that specifies a set of port quadrature counters to disable in the device.")]
    public partial class CreateDisableEncodersPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of port quadrature counters to disable in the device.
        /// </summary>
        [Description("The value that specifies a set of port quadrature counters to disable in the device.")]
        public QuadratureEncoders DisableEncoders { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableEncoders register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public QuadratureEncoders GetPayload()
        {
            return DisableEncoders;
        }

        /// <summary>
        /// Creates a message that specifies a set of port quadrature counters to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableEncoders register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DisableEncoders.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of port quadrature counters to disable in the device.
    /// </summary>
    [DisplayName("TimestampedDisableEncodersPayload")]
    [Description("Creates a timestamped message payload that specifies a set of port quadrature counters to disable in the device.")]
    public partial class CreateTimestampedDisableEncodersPayload : CreateDisableEncodersPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of port quadrature counters to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableEncoders register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DisableEncoders.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of digital inputs to enable in the device.
    /// </summary>
    [DisplayName("EnableDigitalInputsPayload")]
    [Description("Creates a message payload that specifies a set of digital inputs to enable in the device.")]
    public partial class CreateEnableDigitalInputsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of digital inputs to enable in the device.
        /// </summary>
        [Description("The value that specifies a set of digital inputs to enable in the device.")]
        public DigitalInputs EnableDigitalInputs { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableDigitalInputs register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return EnableDigitalInputs;
        }

        /// <summary>
        /// Creates a message that specifies a set of digital inputs to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableDigitalInputs register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EnableDigitalInputs.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of digital inputs to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableDigitalInputsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of digital inputs to enable in the device.")]
    public partial class CreateTimestampedEnableDigitalInputsPayload : CreateEnableDigitalInputsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of digital inputs to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableDigitalInputs register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EnableDigitalInputs.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [DisplayName("DisableDigitalInputsPayload")]
    [Description("Creates a message payload that specifies a set of digital inputs to disable in the device.")]
    public partial class CreateDisableDigitalInputsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of digital inputs to disable in the device.
        /// </summary>
        [Description("The value that specifies a set of digital inputs to disable in the device.")]
        public DigitalInputs DisableDigitalInputs { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableDigitalInputs register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DisableDigitalInputs;
        }

        /// <summary>
        /// Creates a message that specifies a set of digital inputs to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableDigitalInputs register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DisableDigitalInputs.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [DisplayName("TimestampedDisableDigitalInputsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of digital inputs to disable in the device.")]
    public partial class CreateTimestampedDisableDigitalInputsPayload : CreateDisableDigitalInputsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of digital inputs to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableDigitalInputs register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DisableDigitalInputs.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for motor 0.
    /// </summary>
    [DisplayName("Motor0OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for motor 0.")]
    public partial class CreateMotor0OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for motor 0.
        /// </summary>
        [Description("The value that configures the operation mode for motor 0.")]
        public MotorOperationMode Motor0OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MotorOperationMode GetPayload()
        {
            return Motor0OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for motor 0.")]
    public partial class CreateTimestampedMotor0OperationModePayload : CreateMotor0OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for motor 1.
    /// </summary>
    [DisplayName("Motor1OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for motor 1.")]
    public partial class CreateMotor1OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for motor 1.
        /// </summary>
        [Description("The value that configures the operation mode for motor 1.")]
        public MotorOperationMode Motor1OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MotorOperationMode GetPayload()
        {
            return Motor1OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for motor 1.")]
    public partial class CreateTimestampedMotor1OperationModePayload : CreateMotor1OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for motor 2.
    /// </summary>
    [DisplayName("Motor2OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for motor 2.")]
    public partial class CreateMotor2OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for motor 2.
        /// </summary>
        [Description("The value that configures the operation mode for motor 2.")]
        public MotorOperationMode Motor2OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MotorOperationMode GetPayload()
        {
            return Motor2OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for motor 2.")]
    public partial class CreateTimestampedMotor2OperationModePayload : CreateMotor2OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for motor 3.
    /// </summary>
    [DisplayName("Motor3OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for motor 3.")]
    public partial class CreateMotor3OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for motor 3.
        /// </summary>
        [Description("The value that configures the operation mode for motor 3.")]
        public MotorOperationMode Motor3OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MotorOperationMode GetPayload()
        {
            return Motor3OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for motor 3.")]
    public partial class CreateTimestampedMotor3OperationModePayload : CreateMotor3OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the microstep resolution for motor 0.
    /// </summary>
    [DisplayName("Motor0MicrostepResolutionPayload")]
    [Description("Creates a message payload that configures the microstep resolution for motor 0.")]
    public partial class CreateMotor0MicrostepResolutionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the microstep resolution for motor 0.
        /// </summary>
        [Description("The value that configures the microstep resolution for motor 0.")]
        public MicrostepResolution Motor0MicrostepResolution { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MicrostepResolution register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MicrostepResolution GetPayload()
        {
            return Motor0MicrostepResolution;
        }

        /// <summary>
        /// Creates a message that configures the microstep resolution for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MicrostepResolution register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MicrostepResolution.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the microstep resolution for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0MicrostepResolutionPayload")]
    [Description("Creates a timestamped message payload that configures the microstep resolution for motor 0.")]
    public partial class CreateTimestampedMotor0MicrostepResolutionPayload : CreateMotor0MicrostepResolutionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the microstep resolution for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MicrostepResolution register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MicrostepResolution.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the microstep resolution for motor 1.
    /// </summary>
    [DisplayName("Motor1MicrostepResolutionPayload")]
    [Description("Creates a message payload that configures the microstep resolution for motor 1.")]
    public partial class CreateMotor1MicrostepResolutionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the microstep resolution for motor 1.
        /// </summary>
        [Description("The value that configures the microstep resolution for motor 1.")]
        public MicrostepResolution Motor1MicrostepResolution { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MicrostepResolution register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MicrostepResolution GetPayload()
        {
            return Motor1MicrostepResolution;
        }

        /// <summary>
        /// Creates a message that configures the microstep resolution for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MicrostepResolution register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MicrostepResolution.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the microstep resolution for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1MicrostepResolutionPayload")]
    [Description("Creates a timestamped message payload that configures the microstep resolution for motor 1.")]
    public partial class CreateTimestampedMotor1MicrostepResolutionPayload : CreateMotor1MicrostepResolutionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the microstep resolution for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MicrostepResolution register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MicrostepResolution.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the microstep resolution for motor 2.
    /// </summary>
    [DisplayName("Motor2MicrostepResolutionPayload")]
    [Description("Creates a message payload that configures the microstep resolution for motor 2.")]
    public partial class CreateMotor2MicrostepResolutionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the microstep resolution for motor 2.
        /// </summary>
        [Description("The value that configures the microstep resolution for motor 2.")]
        public MicrostepResolution Motor2MicrostepResolution { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MicrostepResolution register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MicrostepResolution GetPayload()
        {
            return Motor2MicrostepResolution;
        }

        /// <summary>
        /// Creates a message that configures the microstep resolution for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MicrostepResolution register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MicrostepResolution.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the microstep resolution for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2MicrostepResolutionPayload")]
    [Description("Creates a timestamped message payload that configures the microstep resolution for motor 2.")]
    public partial class CreateTimestampedMotor2MicrostepResolutionPayload : CreateMotor2MicrostepResolutionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the microstep resolution for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MicrostepResolution register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MicrostepResolution.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the microstep resolution for motor 3.
    /// </summary>
    [DisplayName("Motor3MicrostepResolutionPayload")]
    [Description("Creates a message payload that configures the microstep resolution for motor 3.")]
    public partial class CreateMotor3MicrostepResolutionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the microstep resolution for motor 3.
        /// </summary>
        [Description("The value that configures the microstep resolution for motor 3.")]
        public MicrostepResolution Motor3MicrostepResolution { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MicrostepResolution register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MicrostepResolution GetPayload()
        {
            return Motor3MicrostepResolution;
        }

        /// <summary>
        /// Creates a message that configures the microstep resolution for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MicrostepResolution register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MicrostepResolution.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the microstep resolution for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3MicrostepResolutionPayload")]
    [Description("Creates a timestamped message payload that configures the microstep resolution for motor 3.")]
    public partial class CreateTimestampedMotor3MicrostepResolutionPayload : CreateMotor3MicrostepResolutionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the microstep resolution for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MicrostepResolution register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MicrostepResolution.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum run RMS current per phase for motor 0.
    /// </summary>
    [DisplayName("Motor0MaximumRunCurrentPayload")]
    [Description("Creates a message payload that configures the maximum run RMS current per phase for motor 0.")]
    public partial class CreateMotor0MaximumRunCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum run RMS current per phase for motor 0.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum run RMS current per phase for motor 0.")]
        public float Motor0MaximumRunCurrent { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor0MaximumRunCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor0MaximumRunCurrent;
        }

        /// <summary>
        /// Creates a message that configures the maximum run RMS current per phase for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumRunCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum run RMS current per phase for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0MaximumRunCurrentPayload")]
    [Description("Creates a timestamped message payload that configures the maximum run RMS current per phase for motor 0.")]
    public partial class CreateTimestampedMotor0MaximumRunCurrentPayload : CreateMotor0MaximumRunCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum run RMS current per phase for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumRunCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum run RMS current per phase for motor 1.
    /// </summary>
    [DisplayName("Motor1MaximumRunCurrentPayload")]
    [Description("Creates a message payload that configures the maximum run RMS current per phase for motor 1.")]
    public partial class CreateMotor1MaximumRunCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum run RMS current per phase for motor 1.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum run RMS current per phase for motor 1.")]
        public float Motor1MaximumRunCurrent { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor1MaximumRunCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor1MaximumRunCurrent;
        }

        /// <summary>
        /// Creates a message that configures the maximum run RMS current per phase for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumRunCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum run RMS current per phase for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1MaximumRunCurrentPayload")]
    [Description("Creates a timestamped message payload that configures the maximum run RMS current per phase for motor 1.")]
    public partial class CreateTimestampedMotor1MaximumRunCurrentPayload : CreateMotor1MaximumRunCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum run RMS current per phase for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumRunCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum run RMS current per phase for motor 2.
    /// </summary>
    [DisplayName("Motor2MaximumRunCurrentPayload")]
    [Description("Creates a message payload that configures the maximum run RMS current per phase for motor 2.")]
    public partial class CreateMotor2MaximumRunCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum run RMS current per phase for motor 2.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum run RMS current per phase for motor 2.")]
        public float Motor2MaximumRunCurrent { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor2MaximumRunCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor2MaximumRunCurrent;
        }

        /// <summary>
        /// Creates a message that configures the maximum run RMS current per phase for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumRunCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum run RMS current per phase for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2MaximumRunCurrentPayload")]
    [Description("Creates a timestamped message payload that configures the maximum run RMS current per phase for motor 2.")]
    public partial class CreateTimestampedMotor2MaximumRunCurrentPayload : CreateMotor2MaximumRunCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum run RMS current per phase for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumRunCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum run RMS current per phase for motor 3.
    /// </summary>
    [DisplayName("Motor3MaximumRunCurrentPayload")]
    [Description("Creates a message payload that configures the maximum run RMS current per phase for motor 3.")]
    public partial class CreateMotor3MaximumRunCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum run RMS current per phase for motor 3.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum run RMS current per phase for motor 3.")]
        public float Motor3MaximumRunCurrent { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor3MaximumRunCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor3MaximumRunCurrent;
        }

        /// <summary>
        /// Creates a message that configures the maximum run RMS current per phase for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumRunCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum run RMS current per phase for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3MaximumRunCurrentPayload")]
    [Description("Creates a timestamped message payload that configures the maximum run RMS current per phase for motor 3.")]
    public partial class CreateTimestampedMotor3MaximumRunCurrentPayload : CreateMotor3MaximumRunCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum run RMS current per phase for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MaximumRunCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumRunCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the hold current reduction for motor 0.
    /// </summary>
    [DisplayName("Motor0HoldCurrentReductionPayload")]
    [Description("Creates a message payload that configures the hold current reduction for motor 0.")]
    public partial class CreateMotor0HoldCurrentReductionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the hold current reduction for motor 0.
        /// </summary>
        [Description("The value that configures the hold current reduction for motor 0.")]
        public HoldCurrentReduction Motor0HoldCurrentReduction { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0HoldCurrentReduction register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HoldCurrentReduction GetPayload()
        {
            return Motor0HoldCurrentReduction;
        }

        /// <summary>
        /// Creates a message that configures the hold current reduction for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0HoldCurrentReduction.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the hold current reduction for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0HoldCurrentReductionPayload")]
    [Description("Creates a timestamped message payload that configures the hold current reduction for motor 0.")]
    public partial class CreateTimestampedMotor0HoldCurrentReductionPayload : CreateMotor0HoldCurrentReductionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the hold current reduction for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0HoldCurrentReduction.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the hold current reduction for motor 1.
    /// </summary>
    [DisplayName("Motor1HoldCurrentReductionPayload")]
    [Description("Creates a message payload that configures the hold current reduction for motor 1.")]
    public partial class CreateMotor1HoldCurrentReductionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the hold current reduction for motor 1.
        /// </summary>
        [Description("The value that configures the hold current reduction for motor 1.")]
        public HoldCurrentReduction Motor1HoldCurrentReduction { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1HoldCurrentReduction register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HoldCurrentReduction GetPayload()
        {
            return Motor1HoldCurrentReduction;
        }

        /// <summary>
        /// Creates a message that configures the hold current reduction for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1HoldCurrentReduction.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the hold current reduction for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1HoldCurrentReductionPayload")]
    [Description("Creates a timestamped message payload that configures the hold current reduction for motor 1.")]
    public partial class CreateTimestampedMotor1HoldCurrentReductionPayload : CreateMotor1HoldCurrentReductionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the hold current reduction for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1HoldCurrentReduction.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the hold current reduction for motor 2.
    /// </summary>
    [DisplayName("Motor2HoldCurrentReductionPayload")]
    [Description("Creates a message payload that configures the hold current reduction for motor 2.")]
    public partial class CreateMotor2HoldCurrentReductionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the hold current reduction for motor 2.
        /// </summary>
        [Description("The value that configures the hold current reduction for motor 2.")]
        public HoldCurrentReduction Motor2HoldCurrentReduction { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2HoldCurrentReduction register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HoldCurrentReduction GetPayload()
        {
            return Motor2HoldCurrentReduction;
        }

        /// <summary>
        /// Creates a message that configures the hold current reduction for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2HoldCurrentReduction.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the hold current reduction for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2HoldCurrentReductionPayload")]
    [Description("Creates a timestamped message payload that configures the hold current reduction for motor 2.")]
    public partial class CreateTimestampedMotor2HoldCurrentReductionPayload : CreateMotor2HoldCurrentReductionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the hold current reduction for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2HoldCurrentReduction.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the hold current reduction for motor 3.
    /// </summary>
    [DisplayName("Motor3HoldCurrentReductionPayload")]
    [Description("Creates a message payload that configures the hold current reduction for motor 3.")]
    public partial class CreateMotor3HoldCurrentReductionPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the hold current reduction for motor 3.
        /// </summary>
        [Description("The value that configures the hold current reduction for motor 3.")]
        public HoldCurrentReduction Motor3HoldCurrentReduction { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3HoldCurrentReduction register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HoldCurrentReduction GetPayload()
        {
            return Motor3HoldCurrentReduction;
        }

        /// <summary>
        /// Creates a message that configures the hold current reduction for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3HoldCurrentReduction.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the hold current reduction for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3HoldCurrentReductionPayload")]
    [Description("Creates a timestamped message payload that configures the hold current reduction for motor 3.")]
    public partial class CreateTimestampedMotor3HoldCurrentReductionPayload : CreateMotor3HoldCurrentReductionPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the hold current reduction for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3HoldCurrentReduction register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3HoldCurrentReduction.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
    /// </summary>
    [DisplayName("Motor0StepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 0.")]
    public partial class CreateMotor0StepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) when running at nominal speed for motor 0.")]
        public ushort Motor0StepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor0StepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor0StepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0StepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0StepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 0.")]
    public partial class CreateTimestampedMotor0StepIntervalPayload : CreateMotor0StepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) when running at nominal speed for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0StepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
    /// </summary>
    [DisplayName("Motor1StepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 1.")]
    public partial class CreateMotor1StepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) when running at nominal speed for motor 1.")]
        public ushort Motor1StepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor1StepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor1StepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1StepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1StepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 1.")]
    public partial class CreateTimestampedMotor1StepIntervalPayload : CreateMotor1StepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) when running at nominal speed for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1StepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
    /// </summary>
    [DisplayName("Motor2StepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 2.")]
    public partial class CreateMotor2StepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) when running at nominal speed for motor 2.")]
        public ushort Motor2StepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor2StepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor2StepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2StepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2StepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 2.")]
    public partial class CreateTimestampedMotor2StepIntervalPayload : CreateMotor2StepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) when running at nominal speed for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2StepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
    /// </summary>
    [DisplayName("Motor3StepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 3.")]
    public partial class CreateMotor3StepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) when running at nominal speed for motor 3.")]
        public ushort Motor3StepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor3StepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor3StepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3StepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3StepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) when running at nominal speed for motor 3.")]
    public partial class CreateTimestampedMotor3StepIntervalPayload : CreateMotor3StepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) when running at nominal speed for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3StepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
    /// </summary>
    [DisplayName("Motor0MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.")]
    public partial class CreateMotor0MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.")]
        public ushort Motor0MaximumStepInterval { get; set; } = 2000;

        /// <summary>
        /// Creates a message payload for the Motor0MaximumStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor0MaximumStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.")]
    public partial class CreateTimestampedMotor0MaximumStepIntervalPayload : CreateMotor0MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
    /// </summary>
    [DisplayName("Motor1MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.")]
    public partial class CreateMotor1MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.")]
        public ushort Motor1MaximumStepInterval { get; set; } = 2000;

        /// <summary>
        /// Creates a message payload for the Motor1MaximumStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor1MaximumStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.")]
    public partial class CreateTimestampedMotor1MaximumStepIntervalPayload : CreateMotor1MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
    /// </summary>
    [DisplayName("Motor2MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.")]
    public partial class CreateMotor2MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.")]
        public ushort Motor2MaximumStepInterval { get; set; } = 2000;

        /// <summary>
        /// Creates a message payload for the Motor2MaximumStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor2MaximumStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.")]
    public partial class CreateTimestampedMotor2MaximumStepIntervalPayload : CreateMotor2MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
    /// </summary>
    [DisplayName("Motor3MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.")]
    public partial class CreateMotor3MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.")]
        public ushort Motor3MaximumStepInterval { get; set; } = 2000;

        /// <summary>
        /// Creates a message payload for the Motor3MaximumStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor3MaximumStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.")]
    public partial class CreateTimestampedMotor3MaximumStepIntervalPayload : CreateMotor3MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses (us) used when starting or stopping a movement for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MaximumStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("Motor0StepAccelerationIntervalPayload")]
    [Description("Creates a message payload that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateMotor0StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        [Range(min: 2, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
        public ushort Motor0StepAccelerationInterval { get; set; } = 10;

        /// <summary>
        /// Creates a message payload for the Motor0StepAccelerationInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor0StepAccelerationInterval;
        }

        /// <summary>
        /// Creates a message that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepAccelerationInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("TimestampedMotor0StepAccelerationIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateTimestampedMotor0StepAccelerationIntervalPayload : CreateMotor0StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the acceleration for motor 0. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepAccelerationInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("Motor1StepAccelerationIntervalPayload")]
    [Description("Creates a message payload that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateMotor1StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        [Range(min: 2, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
        public ushort Motor1StepAccelerationInterval { get; set; } = 10;

        /// <summary>
        /// Creates a message payload for the Motor1StepAccelerationInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor1StepAccelerationInterval;
        }

        /// <summary>
        /// Creates a message that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepAccelerationInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("TimestampedMotor1StepAccelerationIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateTimestampedMotor1StepAccelerationIntervalPayload : CreateMotor1StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the acceleration for motor 1. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepAccelerationInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("Motor2StepAccelerationIntervalPayload")]
    [Description("Creates a message payload that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateMotor2StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        [Range(min: 2, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
        public ushort Motor2StepAccelerationInterval { get; set; } = 10;

        /// <summary>
        /// Creates a message payload for the Motor2StepAccelerationInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor2StepAccelerationInterval;
        }

        /// <summary>
        /// Creates a message that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepAccelerationInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("TimestampedMotor2StepAccelerationIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateTimestampedMotor2StepAccelerationIntervalPayload : CreateMotor2StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the acceleration for motor 2. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepAccelerationInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("Motor3StepAccelerationIntervalPayload")]
    [Description("Creates a message payload that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateMotor3StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        [Range(min: 2, max: 2000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
        public ushort Motor3StepAccelerationInterval { get; set; } = 10;

        /// <summary>
        /// Creates a message payload for the Motor3StepAccelerationInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor3StepAccelerationInterval;
        }

        /// <summary>
        /// Creates a message that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepAccelerationInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
    /// </summary>
    [DisplayName("TimestampedMotor3StepAccelerationIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.")]
    public partial class CreateTimestampedMotor3StepAccelerationIntervalPayload : CreateMotor3StepAccelerationIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the acceleration for motor 3. The time between step pulses is decreased by this value when accelerating and increased when decelerating.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3StepAccelerationInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepAccelerationInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode of the quadrature QuadratureEncoders.
    /// </summary>
    [DisplayName("EncoderModePayload")]
    [Description("Creates a message payload that configures the operation mode of the quadrature QuadratureEncoders.")]
    public partial class CreateEncoderModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode of the quadrature QuadratureEncoders.
        /// </summary>
        [Description("The value that configures the operation mode of the quadrature QuadratureEncoders.")]
        public EncoderModeConfig EncoderMode { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderModeConfig GetPayload()
        {
            return EncoderMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode of the quadrature QuadratureEncoders.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EncoderMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode of the quadrature QuadratureEncoders.
    /// </summary>
    [DisplayName("TimestampedEncoderModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode of the quadrature QuadratureEncoders.")]
    public partial class CreateTimestampedEncoderModePayload : CreateEncoderModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode of the quadrature QuadratureEncoders.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EncoderMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the reading rate of the QuadratureEncoders' event.
    /// </summary>
    [DisplayName("EncoderSamplingRatePayload")]
    [Description("Creates a message payload that configures the reading rate of the QuadratureEncoders' event.")]
    public partial class CreateEncoderSamplingRatePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the reading rate of the QuadratureEncoders' event.
        /// </summary>
        [Description("The value that configures the reading rate of the QuadratureEncoders' event.")]
        public EncoderSamplingRateConfig EncoderSamplingRate { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderSamplingRate register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderSamplingRateConfig GetPayload()
        {
            return EncoderSamplingRate;
        }

        /// <summary>
        /// Creates a message that configures the reading rate of the QuadratureEncoders' event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderSamplingRate register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EncoderSamplingRate.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the reading rate of the QuadratureEncoders' event.
    /// </summary>
    [DisplayName("TimestampedEncoderSamplingRatePayload")]
    [Description("Creates a timestamped message payload that configures the reading rate of the QuadratureEncoders' event.")]
    public partial class CreateTimestampedEncoderSamplingRatePayload : CreateEncoderSamplingRatePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the reading rate of the QuadratureEncoders' event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderSamplingRate register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EncoderSamplingRate.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 0.
    /// </summary>
    [DisplayName("Input0OpModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 0.")]
    public partial class CreateInput0OpModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 0.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 0.")]
        public InputOpModeConfig Input0OpMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input0OpMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOpModeConfig GetPayload()
        {
            return Input0OpMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input0OpMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input0OpMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 0.
    /// </summary>
    [DisplayName("TimestampedInput0OpModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 0.")]
    public partial class CreateTimestampedInput0OpModePayload : CreateInput0OpModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input0OpMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input0OpMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 1.
    /// </summary>
    [DisplayName("Input1OpModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 1.")]
    public partial class CreateInput1OpModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 1.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 1.")]
        public InputOpModeConfig Input1OpMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input1OpMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOpModeConfig GetPayload()
        {
            return Input1OpMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input1OpMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input1OpMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 1.
    /// </summary>
    [DisplayName("TimestampedInput1OpModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 1.")]
    public partial class CreateTimestampedInput1OpModePayload : CreateInput1OpModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input1OpMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input1OpMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 2.
    /// </summary>
    [DisplayName("Input2OpModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 2.")]
    public partial class CreateInput2OpModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 2.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 2.")]
        public InputOpModeConfig Input2OpMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input2OpMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOpModeConfig GetPayload()
        {
            return Input2OpMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input2OpMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input2OpMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 2.
    /// </summary>
    [DisplayName("TimestampedInput2OpModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 2.")]
    public partial class CreateTimestampedInput2OpModePayload : CreateInput2OpModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input2OpMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input2OpMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 3.
    /// </summary>
    [DisplayName("Input3OpModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 3.")]
    public partial class CreateInput3OpModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 3.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 3.")]
        public InputOpModeConfig Input3OpMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input3OpMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOpModeConfig GetPayload()
        {
            return Input3OpMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input3OpMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input3OpMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 3.
    /// </summary>
    [DisplayName("TimestampedInput3OpModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 3.")]
    public partial class CreateTimestampedInput3OpModePayload : CreateInput3OpModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input3OpMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input3OpMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the trigger mode for digital input 0.
    /// </summary>
    [DisplayName("Input0TriggerPayload")]
    [Description("Creates a message payload that configures the trigger mode for digital input 0.")]
    public partial class CreateInput0TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the trigger mode for digital input 0.
        /// </summary>
        [Description("The value that configures the trigger mode for digital input 0.")]
        public TriggerConfig Input0Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Input0Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return Input0Trigger;
        }

        /// <summary>
        /// Creates a message that configures the trigger mode for digital input 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input0Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input0Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the trigger mode for digital input 0.
    /// </summary>
    [DisplayName("TimestampedInput0TriggerPayload")]
    [Description("Creates a timestamped message payload that configures the trigger mode for digital input 0.")]
    public partial class CreateTimestampedInput0TriggerPayload : CreateInput0TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the trigger mode for digital input 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input0Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input0Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the trigger mode for digital input 1.
    /// </summary>
    [DisplayName("Input1TriggerPayload")]
    [Description("Creates a message payload that configures the trigger mode for digital input 1.")]
    public partial class CreateInput1TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the trigger mode for digital input 1.
        /// </summary>
        [Description("The value that configures the trigger mode for digital input 1.")]
        public TriggerConfig Input1Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Input1Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return Input1Trigger;
        }

        /// <summary>
        /// Creates a message that configures the trigger mode for digital input 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input1Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input1Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the trigger mode for digital input 1.
    /// </summary>
    [DisplayName("TimestampedInput1TriggerPayload")]
    [Description("Creates a timestamped message payload that configures the trigger mode for digital input 1.")]
    public partial class CreateTimestampedInput1TriggerPayload : CreateInput1TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the trigger mode for digital input 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input1Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input1Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the trigger mode for digital input 2.
    /// </summary>
    [DisplayName("Input2TriggerPayload")]
    [Description("Creates a message payload that configures the trigger mode for digital input 2.")]
    public partial class CreateInput2TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the trigger mode for digital input 2.
        /// </summary>
        [Description("The value that configures the trigger mode for digital input 2.")]
        public TriggerConfig Input2Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Input2Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return Input2Trigger;
        }

        /// <summary>
        /// Creates a message that configures the trigger mode for digital input 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input2Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input2Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the trigger mode for digital input 2.
    /// </summary>
    [DisplayName("TimestampedInput2TriggerPayload")]
    [Description("Creates a timestamped message payload that configures the trigger mode for digital input 2.")]
    public partial class CreateTimestampedInput2TriggerPayload : CreateInput2TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the trigger mode for digital input 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input2Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input2Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the trigger mode for digital input 3.
    /// </summary>
    [DisplayName("Input3TriggerPayload")]
    [Description("Creates a message payload that configures the trigger mode for digital input 3.")]
    public partial class CreateInput3TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the trigger mode for digital input 3.
        /// </summary>
        [Description("The value that configures the trigger mode for digital input 3.")]
        public TriggerConfig Input3Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the Input3Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return Input3Trigger;
        }

        /// <summary>
        /// Creates a message that configures the trigger mode for digital input 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input3Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input3Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the trigger mode for digital input 3.
    /// </summary>
    [DisplayName("TimestampedInput3TriggerPayload")]
    [Description("Creates a timestamped message payload that configures the trigger mode for digital input 3.")]
    public partial class CreateTimestampedInput3TriggerPayload : CreateInput3TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the trigger mode for digital input 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input3Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input3Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the external interlock connector state required for the device to be enabled.
    /// </summary>
    [DisplayName("InterlockEnabledPayload")]
    [Description("Creates a message payload that configures the external interlock connector state required for the device to be enabled.")]
    public partial class CreateInterlockEnabledPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the external interlock connector state required for the device to be enabled.
        /// </summary>
        [Description("The value that configures the external interlock connector state required for the device to be enabled.")]
        public InterlockEnabledConfig InterlockEnabled { get; set; }

        /// <summary>
        /// Creates a message payload for the InterlockEnabled register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InterlockEnabledConfig GetPayload()
        {
            return InterlockEnabled;
        }

        /// <summary>
        /// Creates a message that configures the external interlock connector state required for the device to be enabled.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the InterlockEnabled register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.InterlockEnabled.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the external interlock connector state required for the device to be enabled.
    /// </summary>
    [DisplayName("TimestampedInterlockEnabledPayload")]
    [Description("Creates a timestamped message payload that configures the external interlock connector state required for the device to be enabled.")]
    public partial class CreateTimestampedInterlockEnabledPayload : CreateInterlockEnabledPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the external interlock connector state required for the device to be enabled.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the InterlockEnabled register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.InterlockEnabled.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the dispatch rate of the accumulated steps event.
    /// </summary>
    [DisplayName("AccumulatedStepsSamplingRatePayload")]
    [Description("Creates a message payload that configures the dispatch rate of the accumulated steps event.")]
    public partial class CreateAccumulatedStepsSamplingRatePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the dispatch rate of the accumulated steps event.
        /// </summary>
        [Description("The value that configures the dispatch rate of the accumulated steps event.")]
        public AccumulatedStepsSamplingRateConfig AccumulatedStepsSamplingRate { get; set; }

        /// <summary>
        /// Creates a message payload for the AccumulatedStepsSamplingRate register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AccumulatedStepsSamplingRateConfig GetPayload()
        {
            return AccumulatedStepsSamplingRate;
        }

        /// <summary>
        /// Creates a message that configures the dispatch rate of the accumulated steps event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AccumulatedStepsSamplingRate register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.AccumulatedStepsSamplingRate.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the dispatch rate of the accumulated steps event.
    /// </summary>
    [DisplayName("TimestampedAccumulatedStepsSamplingRatePayload")]
    [Description("Creates a timestamped message payload that configures the dispatch rate of the accumulated steps event.")]
    public partial class CreateTimestampedAccumulatedStepsSamplingRatePayload : CreateAccumulatedStepsSamplingRatePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the dispatch rate of the accumulated steps event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AccumulatedStepsSamplingRate register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.AccumulatedStepsSamplingRate.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [DisplayName("MotorStoppedPayload")]
    [Description("Creates a message payload that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.")]
    public partial class CreateMotorStoppedPayload
    {
        /// <summary>
        /// Gets or sets the value that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
        /// </summary>
        [Description("The value that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.")]
        public StepperMotors MotorStopped { get; set; }

        /// <summary>
        /// Creates a message payload for the MotorStopped register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return MotorStopped;
        }

        /// <summary>
        /// Creates a message that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MotorStopped register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MotorStopped.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [DisplayName("TimestampedMotorStoppedPayload")]
    [Description("Creates a timestamped message payload that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.")]
    public partial class CreateTimestampedMotorStoppedPayload : CreateMotorStoppedPayload
    {
        /// <summary>
        /// Creates a timestamped message that emitted when any of the motor state changes. Contains a bit mask specifying the motor that stopped the movement.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MotorStopped register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MotorStopped.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
    /// </summary>
    [DisplayName("MotorOverVoltageDetectionPayload")]
    [Description("Creates a message payload that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).")]
    public partial class CreateMotorOverVoltageDetectionPayload
    {
        /// <summary>
        /// Gets or sets the value that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
        /// </summary>
        [Description("The value that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).")]
        public StepperMotors MotorOverVoltageDetection { get; set; }

        /// <summary>
        /// Creates a message payload for the MotorOverVoltageDetection register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return MotorOverVoltageDetection;
        }

        /// <summary>
        /// Creates a message that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MotorOverVoltageDetection register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MotorOverVoltageDetection.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
    /// </summary>
    [DisplayName("TimestampedMotorOverVoltageDetectionPayload")]
    [Description("Creates a timestamped message payload that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).")]
    public partial class CreateTimestampedMotorOverVoltageDetectionPayload : CreateMotorOverVoltageDetectionPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains a bit mask specifying the motor where the over voltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls (not implemented).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MotorOverVoltageDetection register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MotorOverVoltageDetection.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
    /// </summary>
    [DisplayName("MotorRaisedErrorPayload")]
    [Description("Creates a message payload that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.")]
    public partial class CreateMotorRaisedErrorPayload
    {
        /// <summary>
        /// Gets or sets the value that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
        /// </summary>
        [Description("The value that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.")]
        public StepperMotors MotorRaisedError { get; set; }

        /// <summary>
        /// Creates a message payload for the MotorRaisedError register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return MotorRaisedError;
        }

        /// <summary>
        /// Creates a message that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MotorRaisedError register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MotorRaisedError.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
    /// </summary>
    [DisplayName("TimestampedMotorRaisedErrorPayload")]
    [Description("Creates a timestamped message payload that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.")]
    public partial class CreateTimestampedMotorRaisedErrorPayload : CreateMotorRaisedErrorPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degrees celsius.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MotorRaisedError register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MotorRaisedError.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the quadrature encoder readings.
    /// </summary>
    [DisplayName("EncodersPayload")]
    [Description("Creates a message payload that contains the quadrature encoder readings.")]
    public partial class CreateEncodersPayload
    {
        /// <summary>
        /// Gets or sets a value that the quadrature counter on port ENC 0.
        /// </summary>
        [Description("The quadrature counter on port ENC 0.")]
        public short Encoder0 { get; set; }

        /// <summary>
        /// Gets or sets a value that the quadrature counter on port ENC 1.
        /// </summary>
        [Description("The quadrature counter on port ENC 1.")]
        public short Encoder1 { get; set; }

        /// <summary>
        /// Gets or sets a value that the quadrature counter on port ENC 2.
        /// </summary>
        [Description("The quadrature counter on port ENC 2.")]
        public short Encoder2 { get; set; }

        /// <summary>
        /// Creates a message payload for the Encoders register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncodersPayload GetPayload()
        {
            EncodersPayload value;
            value.Encoder0 = Encoder0;
            value.Encoder1 = Encoder1;
            value.Encoder2 = Encoder2;
            return value;
        }

        /// <summary>
        /// Creates a message that contains the quadrature encoder readings.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Encoders register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Encoders.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the quadrature encoder readings.
    /// </summary>
    [DisplayName("TimestampedEncodersPayload")]
    [Description("Creates a timestamped message payload that contains the quadrature encoder readings.")]
    public partial class CreateTimestampedEncodersPayload : CreateEncodersPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the quadrature encoder readings.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Encoders register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Encoders.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reflects the state of the digital input lines.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [Description("Creates a message payload that reflects the state of the digital input lines.")]
    public partial class CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that reflects the state of the digital input lines.
        /// </summary>
        [Description("The value that reflects the state of the digital input lines.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputState;
        }

        /// <summary>
        /// Creates a message that reflects the state of the digital input lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DigitalInputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reflects the state of the digital input lines.
    /// </summary>
    [DisplayName("TimestampedDigitalInputStatePayload")]
    [Description("Creates a timestamped message payload that reflects the state of the digital input lines.")]
    public partial class CreateTimestampedDigitalInputStatePayload : CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that reflects the state of the digital input lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DigitalInputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the state of the device.
    /// </summary>
    [DisplayName("DeviceStatePayload")]
    [Description("Creates a message payload that contains the state of the device.")]
    public partial class CreateDeviceStatePayload
    {
        /// <summary>
        /// Gets or sets the value that contains the state of the device.
        /// </summary>
        [Description("The value that contains the state of the device.")]
        public DeviceStateMode DeviceState { get; set; }

        /// <summary>
        /// Creates a message payload for the DeviceState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DeviceStateMode GetPayload()
        {
            return DeviceState;
        }

        /// <summary>
        /// Creates a message that contains the state of the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DeviceState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DeviceState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the state of the device.
    /// </summary>
    [DisplayName("TimestampedDeviceStatePayload")]
    [Description("Creates a timestamped message payload that contains the state of the device.")]
    public partial class CreateTimestampedDeviceStatePayload : CreateDeviceStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the state of the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DeviceState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DeviceState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [DisplayName("MoveRelativePayload")]
    [Description("Creates a message payload that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.")]
    public partial class CreateMoveRelativePayload
    {
        /// <summary>
        /// Gets or sets a value that contains the number of steps used to move motor 0.
        /// </summary>
        [Description("Contains the number of steps used to move motor 0.")]
        public int Motor0 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the number of steps used to move motor 1.
        /// </summary>
        [Description("Contains the number of steps used to move motor 1.")]
        public int Motor1 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the number of steps used to move motor 2.
        /// </summary>
        [Description("Contains the number of steps used to move motor 2.")]
        public int Motor2 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the number of steps used to move motor 3.
        /// </summary>
        [Description("Contains the number of steps used to move motor 3.")]
        public int Motor3 { get; set; }

        /// <summary>
        /// Creates a message payload for the MoveRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MoveRelativePayload GetPayload()
        {
            MoveRelativePayload value;
            value.Motor0 = Motor0;
            value.Motor1 = Motor1;
            value.Motor2 = Motor2;
            value.Motor3 = Motor3;
            return value;
        }

        /// <summary>
        /// Creates a message that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MoveRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MoveRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [DisplayName("TimestampedMoveRelativePayload")]
    [Description("Creates a timestamped message payload that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.")]
    public partial class CreateTimestampedMoveRelativePayload : CreateMoveRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves all motors by the number of steps written in this array register and set the direction according to the value's signal. If a motor is disable, the user should set the value to 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MoveRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MoveRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor0MoveRelativePayload")]
    [Description("Creates a message payload that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor0MoveRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor0MoveRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MoveRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MoveRelative;
        }

        /// <summary>
        /// Creates a message that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MoveRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MoveRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor0MoveRelativePayload")]
    [Description("Creates a timestamped message payload that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor0MoveRelativePayload : CreateMotor0MoveRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MoveRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MoveRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor1MoveRelativePayload")]
    [Description("Creates a message payload that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor1MoveRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor1MoveRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MoveRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MoveRelative;
        }

        /// <summary>
        /// Creates a message that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MoveRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MoveRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor1MoveRelativePayload")]
    [Description("Creates a timestamped message payload that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor1MoveRelativePayload : CreateMotor1MoveRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MoveRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MoveRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor2MoveRelativePayload")]
    [Description("Creates a message payload that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor2MoveRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor2MoveRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MoveRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MoveRelative;
        }

        /// <summary>
        /// Creates a message that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MoveRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MoveRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor2MoveRelativePayload")]
    [Description("Creates a timestamped message payload that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor2MoveRelativePayload : CreateMotor2MoveRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MoveRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MoveRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor3MoveRelativePayload")]
    [Description("Creates a message payload that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor3MoveRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor3MoveRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MoveRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MoveRelative;
        }

        /// <summary>
        /// Creates a message that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MoveRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MoveRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor3MoveRelativePayload")]
    [Description("Creates a timestamped message payload that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor3MoveRelativePayload : CreateMotor3MoveRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MoveRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MoveRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [DisplayName("MoveAbsolutePayload")]
    [Description("Creates a message payload that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.")]
    public partial class CreateMoveAbsolutePayload
    {
        /// <summary>
        /// Gets or sets a value that contains the absolute position to move motor 0.
        /// </summary>
        [Description("Contains the absolute position to move motor 0.")]
        public int Motor0 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the absolute position to move motor 1.
        /// </summary>
        [Description("Contains the absolute position to move motor 1.")]
        public int Motor1 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the absolute position to move motor 2.
        /// </summary>
        [Description("Contains the absolute position to move motor 2.")]
        public int Motor2 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the absolute position to move motor 3.
        /// </summary>
        [Description("Contains the absolute position to move motor 3.")]
        public int Motor3 { get; set; }

        /// <summary>
        /// Creates a message payload for the MoveAbsolute register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MoveAbsolutePayload GetPayload()
        {
            MoveAbsolutePayload value;
            value.Motor0 = Motor0;
            value.Motor1 = Motor1;
            value.Motor2 = Motor2;
            value.Motor3 = Motor3;
            return value;
        }

        /// <summary>
        /// Creates a message that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MoveAbsolute register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MoveAbsolute.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.
    /// </summary>
    [DisplayName("TimestampedMoveAbsolutePayload")]
    [Description("Creates a timestamped message payload that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.")]
    public partial class CreateTimestampedMoveAbsolutePayload : CreateMoveAbsolutePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves all motors to the absolute position written in this array register. If a motor is disable, the user should set the value to 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MoveAbsolute register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MoveAbsolute.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 0 to the absolute position written in this register.
    /// </summary>
    [DisplayName("Motor0MoveAbsolutePayload")]
    [Description("Creates a message payload that moves motor 0 to the absolute position written in this register.")]
    public partial class CreateMotor0MoveAbsolutePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 0 to the absolute position written in this register.
        /// </summary>
        [Description("The value that moves motor 0 to the absolute position written in this register.")]
        public int Motor0MoveAbsolute { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MoveAbsolute register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MoveAbsolute;
        }

        /// <summary>
        /// Creates a message that moves motor 0 to the absolute position written in this register.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MoveAbsolute register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MoveAbsolute.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 0 to the absolute position written in this register.
    /// </summary>
    [DisplayName("TimestampedMotor0MoveAbsolutePayload")]
    [Description("Creates a timestamped message payload that moves motor 0 to the absolute position written in this register.")]
    public partial class CreateTimestampedMotor0MoveAbsolutePayload : CreateMotor0MoveAbsolutePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 0 to the absolute position written in this register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MoveAbsolute register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MoveAbsolute.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 1 to the absolute position written in this register.
    /// </summary>
    [DisplayName("Motor1MoveAbsolutePayload")]
    [Description("Creates a message payload that moves motor 1 to the absolute position written in this register.")]
    public partial class CreateMotor1MoveAbsolutePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 1 to the absolute position written in this register.
        /// </summary>
        [Description("The value that moves motor 1 to the absolute position written in this register.")]
        public int Motor1MoveAbsolute { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MoveAbsolute register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MoveAbsolute;
        }

        /// <summary>
        /// Creates a message that moves motor 1 to the absolute position written in this register.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MoveAbsolute register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MoveAbsolute.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 1 to the absolute position written in this register.
    /// </summary>
    [DisplayName("TimestampedMotor1MoveAbsolutePayload")]
    [Description("Creates a timestamped message payload that moves motor 1 to the absolute position written in this register.")]
    public partial class CreateTimestampedMotor1MoveAbsolutePayload : CreateMotor1MoveAbsolutePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 1 to the absolute position written in this register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MoveAbsolute register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MoveAbsolute.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 2 to the absolute position written in this register.
    /// </summary>
    [DisplayName("Motor2MoveAbsolutePayload")]
    [Description("Creates a message payload that moves motor 2 to the absolute position written in this register.")]
    public partial class CreateMotor2MoveAbsolutePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 2 to the absolute position written in this register.
        /// </summary>
        [Description("The value that moves motor 2 to the absolute position written in this register.")]
        public int Motor2MoveAbsolute { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MoveAbsolute register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MoveAbsolute;
        }

        /// <summary>
        /// Creates a message that moves motor 2 to the absolute position written in this register.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MoveAbsolute register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MoveAbsolute.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 2 to the absolute position written in this register.
    /// </summary>
    [DisplayName("TimestampedMotor2MoveAbsolutePayload")]
    [Description("Creates a timestamped message payload that moves motor 2 to the absolute position written in this register.")]
    public partial class CreateTimestampedMotor2MoveAbsolutePayload : CreateMotor2MoveAbsolutePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 2 to the absolute position written in this register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MoveAbsolute register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MoveAbsolute.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 3 to the absolute position written in this register.
    /// </summary>
    [DisplayName("Motor3MoveAbsolutePayload")]
    [Description("Creates a message payload that moves motor 3 to the absolute position written in this register.")]
    public partial class CreateMotor3MoveAbsolutePayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 3 to the absolute position written in this register.
        /// </summary>
        [Description("The value that moves motor 3 to the absolute position written in this register.")]
        public int Motor3MoveAbsolute { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MoveAbsolute register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MoveAbsolute;
        }

        /// <summary>
        /// Creates a message that moves motor 3 to the absolute position written in this register.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MoveAbsolute register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MoveAbsolute.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 3 to the absolute position written in this register.
    /// </summary>
    [DisplayName("TimestampedMotor3MoveAbsolutePayload")]
    [Description("Creates a timestamped message payload that moves motor 3 to the absolute position written in this register.")]
    public partial class CreateTimestampedMotor3MoveAbsolutePayload : CreateMotor3MoveAbsolutePayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 3 to the absolute position written in this register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MoveAbsolute register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MoveAbsolute.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the accumulated steps of all motors.
    /// </summary>
    [DisplayName("AccumulatedStepsPayload")]
    [Description("Creates a message payload that contains the accumulated steps of all motors.")]
    public partial class CreateAccumulatedStepsPayload
    {
        /// <summary>
        /// Gets or sets a value that contains the accumulated steps of motor 0.
        /// </summary>
        [Description("Contains the accumulated steps of motor 0.")]
        public int Motor0 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the accumulated steps of motor 1.
        /// </summary>
        [Description("Contains the accumulated steps of motor 1.")]
        public int Motor1 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the accumulated steps of motor 2.
        /// </summary>
        [Description("Contains the accumulated steps of motor 2.")]
        public int Motor2 { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the accumulated steps of motor 3.
        /// </summary>
        [Description("Contains the accumulated steps of motor 3.")]
        public int Motor3 { get; set; }

        /// <summary>
        /// Creates a message payload for the AccumulatedSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AccumulatedStepsPayload GetPayload()
        {
            AccumulatedStepsPayload value;
            value.Motor0 = Motor0;
            value.Motor1 = Motor1;
            value.Motor2 = Motor2;
            value.Motor3 = Motor3;
            return value;
        }

        /// <summary>
        /// Creates a message that contains the accumulated steps of all motors.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.AccumulatedSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the accumulated steps of all motors.
    /// </summary>
    [DisplayName("TimestampedAccumulatedStepsPayload")]
    [Description("Creates a timestamped message payload that contains the accumulated steps of all motors.")]
    public partial class CreateTimestampedAccumulatedStepsPayload : CreateAccumulatedStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the accumulated steps of all motors.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.AccumulatedSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor0MaxPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor0MaxPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor0MaxPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MaxPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MaxPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MaxPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaxPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor0MaxPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor0MaxPositionPayload : CreateMotor0MaxPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MaxPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaxPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor1MaxPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor1MaxPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor1MaxPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MaxPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MaxPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MaxPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaxPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor1MaxPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor1MaxPositionPayload : CreateMotor1MaxPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MaxPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaxPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor2MaxPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor2MaxPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor2MaxPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MaxPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MaxPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MaxPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaxPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor2MaxPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor2MaxPositionPayload : CreateMotor2MaxPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MaxPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaxPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor3MaxPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor3MaxPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor3MaxPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MaxPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MaxPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MaxPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaxPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor3MaxPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor3MaxPositionPayload : CreateMotor3MaxPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MaxPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaxPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor0MinPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor0MinPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor0MinPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MinPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MinPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MinPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MinPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor0MinPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor0MinPositionPayload : CreateMotor0MinPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MinPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MinPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor1MinPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor1MinPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor1MinPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MinPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MinPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MinPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MinPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor1MinPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor1MinPositionPayload : CreateMotor1MinPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MinPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MinPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor2MinPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor2MinPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor2MinPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MinPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MinPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MinPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MinPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor2MinPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor2MinPositionPayload : CreateMotor2MinPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MinPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MinPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor3MinPositionPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor3MinPositionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor3MinPosition { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MinPosition register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MinPosition;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MinPosition register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MinPosition.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor3MinPositionPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor3MinPositionPayload : CreateMotor3MinPositionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MinPosition register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MinPosition.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor0StepRelativePayload")]
    [Description("Creates a message payload that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateMotor0StepRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
        public int Motor0StepRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0StepRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0StepRelative;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0StepRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor0StepRelativePayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor0StepRelativePayload : CreateMotor0StepRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 0 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0StepRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0StepRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor1StepRelativePayload")]
    [Description("Creates a message payload that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateMotor1StepRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
        public int Motor1StepRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1StepRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1StepRelative;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1StepRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor1StepRelativePayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor1StepRelativePayload : CreateMotor1StepRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 1 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1StepRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1StepRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor2StepRelativePayload")]
    [Description("Creates a message payload that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateMotor2StepRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
        public int Motor2StepRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2StepRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2StepRelative;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2StepRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor2StepRelativePayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor2StepRelativePayload : CreateMotor2StepRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 2 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2StepRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2StepRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor3StepRelativePayload")]
    [Description("Creates a message payload that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateMotor3StepRelativePayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
        public int Motor3StepRelative { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3StepRelative register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3StepRelative;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3StepRelative register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepRelative.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor3StepRelativePayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor3StepRelativePayload : CreateMotor3StepRelativePayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 3 with the step interval defined by this register, disregarding the acceleration protocol. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3StepRelative register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3StepRelative.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that stops the motors selected in the bit-mask immediately.
    /// </summary>
    [DisplayName("StopMotorsPayload")]
    [Description("Creates a message payload that stops the motors selected in the bit-mask immediately.")]
    public partial class CreateStopMotorsPayload
    {
        /// <summary>
        /// Gets or sets the value that stops the motors selected in the bit-mask immediately.
        /// </summary>
        [Description("The value that stops the motors selected in the bit-mask immediately.")]
        public StepperMotors StopMotors { get; set; }

        /// <summary>
        /// Creates a message payload for the StopMotors register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return StopMotors;
        }

        /// <summary>
        /// Creates a message that stops the motors selected in the bit-mask immediately.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the StopMotors register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.StopMotors.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that stops the motors selected in the bit-mask immediately.
    /// </summary>
    [DisplayName("TimestampedStopMotorsPayload")]
    [Description("Creates a timestamped message payload that stops the motors selected in the bit-mask immediately.")]
    public partial class CreateTimestampedStopMotorsPayload : CreateStopMotorsPayload
    {
        /// <summary>
        /// Creates a timestamped message that stops the motors selected in the bit-mask immediately.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the StopMotors register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.StopMotors.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that resets the encoder.
    /// </summary>
    [DisplayName("ResetEncodersPayload")]
    [Description("Creates a message payload that resets the encoder.")]
    public partial class CreateResetEncodersPayload
    {
        /// <summary>
        /// Gets or sets the value that resets the encoder.
        /// </summary>
        [Description("The value that resets the encoder.")]
        public QuadratureEncoders ResetEncoders { get; set; }

        /// <summary>
        /// Creates a message payload for the ResetEncoders register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public QuadratureEncoders GetPayload()
        {
            return ResetEncoders;
        }

        /// <summary>
        /// Creates a message that resets the encoder.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ResetEncoders register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.ResetEncoders.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that resets the encoder.
    /// </summary>
    [DisplayName("TimestampedResetEncodersPayload")]
    [Description("Creates a timestamped message payload that resets the encoder.")]
    public partial class CreateTimestampedResetEncodersPayload : CreateResetEncodersPayload
    {
        /// <summary>
        /// Creates a timestamped message that resets the encoder.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ResetEncoders register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.ResetEncoders.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents the payload of the Encoders register.
    /// </summary>
    public struct EncodersPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncodersPayload"/> structure.
        /// </summary>
        /// <param name="encoder0">The quadrature counter on port ENC 0.</param>
        /// <param name="encoder1">The quadrature counter on port ENC 1.</param>
        /// <param name="encoder2">The quadrature counter on port ENC 2.</param>
        public EncodersPayload(
            short encoder0,
            short encoder1,
            short encoder2)
        {
            Encoder0 = encoder0;
            Encoder1 = encoder1;
            Encoder2 = encoder2;
        }

        /// <summary>
        /// The quadrature counter on port ENC 0.
        /// </summary>
        public short Encoder0;

        /// <summary>
        /// The quadrature counter on port ENC 1.
        /// </summary>
        public short Encoder1;

        /// <summary>
        /// The quadrature counter on port ENC 2.
        /// </summary>
        public short Encoder2;
    }

    /// <summary>
    /// Represents the payload of the MoveRelative register.
    /// </summary>
    public struct MoveRelativePayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveRelativePayload"/> structure.
        /// </summary>
        /// <param name="motor0">Contains the number of steps used to move motor 0.</param>
        /// <param name="motor1">Contains the number of steps used to move motor 1.</param>
        /// <param name="motor2">Contains the number of steps used to move motor 2.</param>
        /// <param name="motor3">Contains the number of steps used to move motor 3.</param>
        public MoveRelativePayload(
            int motor0,
            int motor1,
            int motor2,
            int motor3)
        {
            Motor0 = motor0;
            Motor1 = motor1;
            Motor2 = motor2;
            Motor3 = motor3;
        }

        /// <summary>
        /// Contains the number of steps used to move motor 0.
        /// </summary>
        public int Motor0;

        /// <summary>
        /// Contains the number of steps used to move motor 1.
        /// </summary>
        public int Motor1;

        /// <summary>
        /// Contains the number of steps used to move motor 2.
        /// </summary>
        public int Motor2;

        /// <summary>
        /// Contains the number of steps used to move motor 3.
        /// </summary>
        public int Motor3;
    }

    /// <summary>
    /// Represents the payload of the MoveAbsolute register.
    /// </summary>
    public struct MoveAbsolutePayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveAbsolutePayload"/> structure.
        /// </summary>
        /// <param name="motor0">Contains the absolute position to move motor 0.</param>
        /// <param name="motor1">Contains the absolute position to move motor 1.</param>
        /// <param name="motor2">Contains the absolute position to move motor 2.</param>
        /// <param name="motor3">Contains the absolute position to move motor 3.</param>
        public MoveAbsolutePayload(
            int motor0,
            int motor1,
            int motor2,
            int motor3)
        {
            Motor0 = motor0;
            Motor1 = motor1;
            Motor2 = motor2;
            Motor3 = motor3;
        }

        /// <summary>
        /// Contains the absolute position to move motor 0.
        /// </summary>
        public int Motor0;

        /// <summary>
        /// Contains the absolute position to move motor 1.
        /// </summary>
        public int Motor1;

        /// <summary>
        /// Contains the absolute position to move motor 2.
        /// </summary>
        public int Motor2;

        /// <summary>
        /// Contains the absolute position to move motor 3.
        /// </summary>
        public int Motor3;
    }

    /// <summary>
    /// Represents the payload of the AccumulatedSteps register.
    /// </summary>
    public struct AccumulatedStepsPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatedStepsPayload"/> structure.
        /// </summary>
        /// <param name="motor0">Contains the accumulated steps of motor 0.</param>
        /// <param name="motor1">Contains the accumulated steps of motor 1.</param>
        /// <param name="motor2">Contains the accumulated steps of motor 2.</param>
        /// <param name="motor3">Contains the accumulated steps of motor 3.</param>
        public AccumulatedStepsPayload(
            int motor0,
            int motor1,
            int motor2,
            int motor3)
        {
            Motor0 = motor0;
            Motor1 = motor1;
            Motor2 = motor2;
            Motor3 = motor3;
        }

        /// <summary>
        /// Contains the accumulated steps of motor 0.
        /// </summary>
        public int Motor0;

        /// <summary>
        /// Contains the accumulated steps of motor 1.
        /// </summary>
        public int Motor1;

        /// <summary>
        /// Contains the accumulated steps of motor 2.
        /// </summary>
        public int Motor2;

        /// <summary>
        /// Contains the accumulated steps of motor 3.
        /// </summary>
        public int Motor3;
    }

    /// <summary>
    /// Specifies the index of each motor.
    /// </summary>
    [Flags]
    public enum StepperMotors : byte
    {
        None = 0x0,
        Motor0 = 0x1,
        Motor1 = 0x2,
        Motor2 = 0x4,
        Motor3 = 0x8
    }

    /// <summary>
    /// Specifies the index of each motor.
    /// </summary>
    [Flags]
    public enum QuadratureEncoders : byte
    {
        None = 0x0,
        Encoder0 = 0x1,
        Encoder1 = 0x2,
        Encoder2 = 0x4
    }

    /// <summary>
    /// Specifies the digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        Input0 = 0x1,
        Input1 = 0x2,
        Input2 = 0x4,
        Input3 = 0x8
    }

    /// <summary>
    /// Specifies the type of reading made from the quadrature QuadratureEncoders.
    /// </summary>
    public enum EncoderModeConfig : byte
    {
        Position = 0,
        Displacement = 1
    }

    /// <summary>
    /// Specifies the rate of the events from the quadrature QuadratureEncoders.
    /// </summary>
    public enum EncoderSamplingRateConfig : byte
    {
        Rate100Hz = 0,
        Rate200Hz = 1,
        Rate250Hz = 2,
        Rate500Hz = 3
    }

    /// <summary>
    /// Specifies the motor operation mode.
    /// </summary>
    public enum MotorOperationMode : byte
    {
        QuietMode = 0,
        DynamicMovements = 1
    }

    /// <summary>
    /// Specifies the microstep resolution for each step pulse.
    /// </summary>
    public enum MicrostepResolution : byte
    {
        Microstep8 = 0,
        Microstep16 = 1,
        Microstep32 = 2,
        Microstep64 = 3
    }

    /// <summary>
    /// Specifies the hold current reduction.
    /// </summary>
    public enum HoldCurrentReduction : byte
    {
        ReductionTo50Percent = 0,
        ReductionTo25Percent = 1,
        ReductionTo12Percent = 2,
        NoReduction = 3
    }

    /// <summary>
    /// Specifies the inputs operation mode.
    /// </summary>
    public enum InputOpModeConfig : byte
    {
        EventOnly = 0,
        EventAndStopMotor0 = 1,
        EventAndStopMotor1 = 2,
        EventAndStopMotor2 = 3,
        EventAndStopMotor3 = 4
    }

    /// <summary>
    /// Specifies the input trigger configuration.
    /// </summary>
    public enum TriggerConfig : byte
    {
        FallingEdge = 0,
        RisingEdge = 1
    }

    /// <summary>
    /// Specifies the external connector state that enables the device.
    /// </summary>
    public enum InterlockEnabledConfig : byte
    {
        Closed = 0,
        Open = 1
    }

    /// <summary>
    /// Specifies the rate of the accumulated steps events.
    /// </summary>
    public enum AccumulatedStepsSamplingRateConfig : byte
    {
        Disabled = 0,
        Rate10Hz = 1,
        Rate50Hz = 2,
        Rate100Hz = 3
    }

    /// <summary>
    /// Specifies the current state of the device.
    /// </summary>
    public enum DeviceStateMode : byte
    {
        Disabled = 0,
        Enabled = 1
    }
}
