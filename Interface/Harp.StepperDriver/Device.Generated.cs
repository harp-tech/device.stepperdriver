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
            { 32, typeof(EnableMotors) },
            { 33, typeof(DisableMotors) },
            { 34, typeof(EnableEncoders) },
            { 35, typeof(DisableEncoders) },
            { 36, typeof(EnableInputs) },
            { 37, typeof(DisableInputs) },
            { 38, typeof(Motor0OperationMode) },
            { 39, typeof(Motor1OperationMode) },
            { 40, typeof(Motor2OperationMode) },
            { 41, typeof(Motor3OperationMode) },
            { 42, typeof(Motor0MicrostepResolution) },
            { 43, typeof(Motor1MicrostepResolution) },
            { 44, typeof(Motor2MicrostepResolution) },
            { 45, typeof(Motor3MicrostepResolution) },
            { 46, typeof(Motor0MaximumCurrentRms) },
            { 47, typeof(Motor1MaximumCurrentRms) },
            { 48, typeof(Motor2MaximumCurrentRms) },
            { 49, typeof(Motor3MaximumCurrentRms) },
            { 50, typeof(Motor0HoldCurrentReduction) },
            { 51, typeof(Motor1HoldCurrentReduction) },
            { 52, typeof(Motor2HoldCurrentReduction) },
            { 53, typeof(Motor3HoldCurrentReduction) },
            { 54, typeof(Motor0NominalStepInterval) },
            { 55, typeof(Motor1NominalStepInterval) },
            { 56, typeof(Motor2NominalStepInterval) },
            { 57, typeof(Motor3NominalStepInterval) },
            { 58, typeof(Motor0MaximumStepInterval) },
            { 59, typeof(Motor1MaximumStepInterval) },
            { 60, typeof(Motor2MaximumStepInterval) },
            { 61, typeof(Motor3MaximumStepInterval) },
            { 62, typeof(Motor0StepAccelerationInterval) },
            { 63, typeof(Motor1StepAccelerationInterval) },
            { 64, typeof(Motor2StepAccelerationInterval) },
            { 65, typeof(Motor3StepAccelerationInterval) },
            { 66, typeof(EncoderMode) },
            { 67, typeof(EncoderRate) },
            { 68, typeof(Input0OperationMode) },
            { 69, typeof(Input1OperationMode) },
            { 70, typeof(Input2OperationMode) },
            { 71, typeof(Input3OperationMode) },
            { 72, typeof(Input0TriggerMode) },
            { 73, typeof(Input1TriggerMode) },
            { 74, typeof(Input2TriggerMode) },
            { 75, typeof(Input3TriggerMode) },
            { 76, typeof(EmergencyStopMode) },
            { 77, typeof(MotorStopped) },
            { 78, typeof(MotorOvervoltageDetection) },
            { 79, typeof(MotorErrorDetection) },
            { 80, typeof(Encoders) },
            { 81, typeof(DigitalInputState) },
            { 82, typeof(EmergencyStop) },
            { 83, typeof(Motor0Steps) },
            { 84, typeof(Motor1Steps) },
            { 85, typeof(Motor2Steps) },
            { 86, typeof(Motor3Steps) },
            { 87, typeof(Motor0AccumulatedSteps) },
            { 88, typeof(Motor1AccumulatedSteps) },
            { 89, typeof(Motor2AccumulatedSteps) },
            { 90, typeof(Motor3AccumulatedSteps) },
            { 91, typeof(Motor0MaximumStepsIntegration) },
            { 92, typeof(Motor1MaximumStepsIntegration) },
            { 93, typeof(Motor2MaximumStepsIntegration) },
            { 94, typeof(Motor3MaximumStepsIntegration) },
            { 95, typeof(Motor0MinimumStepsIntegration) },
            { 96, typeof(Motor1MinimumStepsIntegration) },
            { 97, typeof(Motor2MinimumStepsIntegration) },
            { 98, typeof(Motor3MinimumStepsIntegration) },
            { 99, typeof(Motor0ImmediateSteps) },
            { 100, typeof(Motor1ImmediateSteps) },
            { 101, typeof(Motor2ImmediateSteps) },
            { 102, typeof(Motor3ImmediateSteps) },
            { 103, typeof(StopMotorSuddenly) },
            { 104, typeof(StopMotorSmoothly) },
            { 105, typeof(ResetMotor) },
            { 106, typeof(ResetEncoder) },
            { 107, typeof(Reserved0) },
            { 108, typeof(Reserved1) },
            { 109, typeof(Reserved2) },
            { 110, typeof(Reserved3) },
            { 111, typeof(Reserved4) },
            { 112, typeof(Reserved5) },
            { 113, typeof(Reserved6) },
            { 114, typeof(Reserved7) }
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
    /// <seealso cref="EnableMotors"/>
    /// <seealso cref="DisableMotors"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableInputs"/>
    /// <seealso cref="DisableInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumCurrentRms"/>
    /// <seealso cref="Motor1MaximumCurrentRms"/>
    /// <seealso cref="Motor2MaximumCurrentRms"/>
    /// <seealso cref="Motor3MaximumCurrentRms"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0NominalStepInterval"/>
    /// <seealso cref="Motor1NominalStepInterval"/>
    /// <seealso cref="Motor2NominalStepInterval"/>
    /// <seealso cref="Motor3NominalStepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderRate"/>
    /// <seealso cref="Input0OperationMode"/>
    /// <seealso cref="Input1OperationMode"/>
    /// <seealso cref="Input2OperationMode"/>
    /// <seealso cref="Input3OperationMode"/>
    /// <seealso cref="Input0TriggerMode"/>
    /// <seealso cref="Input1TriggerMode"/>
    /// <seealso cref="Input2TriggerMode"/>
    /// <seealso cref="Input3TriggerMode"/>
    /// <seealso cref="EmergencyStopMode"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOvervoltageDetection"/>
    /// <seealso cref="MotorErrorDetection"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="EmergencyStop"/>
    /// <seealso cref="Motor0Steps"/>
    /// <seealso cref="Motor1Steps"/>
    /// <seealso cref="Motor2Steps"/>
    /// <seealso cref="Motor3Steps"/>
    /// <seealso cref="Motor0AccumulatedSteps"/>
    /// <seealso cref="Motor1AccumulatedSteps"/>
    /// <seealso cref="Motor2AccumulatedSteps"/>
    /// <seealso cref="Motor3AccumulatedSteps"/>
    /// <seealso cref="Motor0MaximumStepsIntegration"/>
    /// <seealso cref="Motor1MaximumStepsIntegration"/>
    /// <seealso cref="Motor2MaximumStepsIntegration"/>
    /// <seealso cref="Motor3MaximumStepsIntegration"/>
    /// <seealso cref="Motor0MinimumStepsIntegration"/>
    /// <seealso cref="Motor1MinimumStepsIntegration"/>
    /// <seealso cref="Motor2MinimumStepsIntegration"/>
    /// <seealso cref="Motor3MinimumStepsIntegration"/>
    /// <seealso cref="Motor0ImmediateSteps"/>
    /// <seealso cref="Motor1ImmediateSteps"/>
    /// <seealso cref="Motor2ImmediateSteps"/>
    /// <seealso cref="Motor3ImmediateSteps"/>
    /// <seealso cref="StopMotorSuddenly"/>
    /// <seealso cref="StopMotorSmoothly"/>
    /// <seealso cref="ResetMotor"/>
    /// <seealso cref="ResetEncoder"/>
    [XmlInclude(typeof(EnableMotors))]
    [XmlInclude(typeof(DisableMotors))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableInputs))]
    [XmlInclude(typeof(DisableInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumCurrentRms))]
    [XmlInclude(typeof(Motor1MaximumCurrentRms))]
    [XmlInclude(typeof(Motor2MaximumCurrentRms))]
    [XmlInclude(typeof(Motor3MaximumCurrentRms))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0NominalStepInterval))]
    [XmlInclude(typeof(Motor1NominalStepInterval))]
    [XmlInclude(typeof(Motor2NominalStepInterval))]
    [XmlInclude(typeof(Motor3NominalStepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderRate))]
    [XmlInclude(typeof(Input0OperationMode))]
    [XmlInclude(typeof(Input1OperationMode))]
    [XmlInclude(typeof(Input2OperationMode))]
    [XmlInclude(typeof(Input3OperationMode))]
    [XmlInclude(typeof(Input0TriggerMode))]
    [XmlInclude(typeof(Input1TriggerMode))]
    [XmlInclude(typeof(Input2TriggerMode))]
    [XmlInclude(typeof(Input3TriggerMode))]
    [XmlInclude(typeof(EmergencyStopMode))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOvervoltageDetection))]
    [XmlInclude(typeof(MotorErrorDetection))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(EmergencyStop))]
    [XmlInclude(typeof(Motor0Steps))]
    [XmlInclude(typeof(Motor1Steps))]
    [XmlInclude(typeof(Motor2Steps))]
    [XmlInclude(typeof(Motor3Steps))]
    [XmlInclude(typeof(Motor0AccumulatedSteps))]
    [XmlInclude(typeof(Motor1AccumulatedSteps))]
    [XmlInclude(typeof(Motor2AccumulatedSteps))]
    [XmlInclude(typeof(Motor3AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor1MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor2MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor3MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor0MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor1MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor2MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor3MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor0ImmediateSteps))]
    [XmlInclude(typeof(Motor1ImmediateSteps))]
    [XmlInclude(typeof(Motor2ImmediateSteps))]
    [XmlInclude(typeof(Motor3ImmediateSteps))]
    [XmlInclude(typeof(StopMotorSuddenly))]
    [XmlInclude(typeof(StopMotorSmoothly))]
    [XmlInclude(typeof(ResetMotor))]
    [XmlInclude(typeof(ResetEncoder))]
    [Description("Filters register-specific messages reported by the StepperDriver device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new EnableMotors();
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
    /// <seealso cref="EnableMotors"/>
    /// <seealso cref="DisableMotors"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableInputs"/>
    /// <seealso cref="DisableInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumCurrentRms"/>
    /// <seealso cref="Motor1MaximumCurrentRms"/>
    /// <seealso cref="Motor2MaximumCurrentRms"/>
    /// <seealso cref="Motor3MaximumCurrentRms"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0NominalStepInterval"/>
    /// <seealso cref="Motor1NominalStepInterval"/>
    /// <seealso cref="Motor2NominalStepInterval"/>
    /// <seealso cref="Motor3NominalStepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderRate"/>
    /// <seealso cref="Input0OperationMode"/>
    /// <seealso cref="Input1OperationMode"/>
    /// <seealso cref="Input2OperationMode"/>
    /// <seealso cref="Input3OperationMode"/>
    /// <seealso cref="Input0TriggerMode"/>
    /// <seealso cref="Input1TriggerMode"/>
    /// <seealso cref="Input2TriggerMode"/>
    /// <seealso cref="Input3TriggerMode"/>
    /// <seealso cref="EmergencyStopMode"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOvervoltageDetection"/>
    /// <seealso cref="MotorErrorDetection"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="EmergencyStop"/>
    /// <seealso cref="Motor0Steps"/>
    /// <seealso cref="Motor1Steps"/>
    /// <seealso cref="Motor2Steps"/>
    /// <seealso cref="Motor3Steps"/>
    /// <seealso cref="Motor0AccumulatedSteps"/>
    /// <seealso cref="Motor1AccumulatedSteps"/>
    /// <seealso cref="Motor2AccumulatedSteps"/>
    /// <seealso cref="Motor3AccumulatedSteps"/>
    /// <seealso cref="Motor0MaximumStepsIntegration"/>
    /// <seealso cref="Motor1MaximumStepsIntegration"/>
    /// <seealso cref="Motor2MaximumStepsIntegration"/>
    /// <seealso cref="Motor3MaximumStepsIntegration"/>
    /// <seealso cref="Motor0MinimumStepsIntegration"/>
    /// <seealso cref="Motor1MinimumStepsIntegration"/>
    /// <seealso cref="Motor2MinimumStepsIntegration"/>
    /// <seealso cref="Motor3MinimumStepsIntegration"/>
    /// <seealso cref="Motor0ImmediateSteps"/>
    /// <seealso cref="Motor1ImmediateSteps"/>
    /// <seealso cref="Motor2ImmediateSteps"/>
    /// <seealso cref="Motor3ImmediateSteps"/>
    /// <seealso cref="StopMotorSuddenly"/>
    /// <seealso cref="StopMotorSmoothly"/>
    /// <seealso cref="ResetMotor"/>
    /// <seealso cref="ResetEncoder"/>
    [XmlInclude(typeof(EnableMotors))]
    [XmlInclude(typeof(DisableMotors))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableInputs))]
    [XmlInclude(typeof(DisableInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumCurrentRms))]
    [XmlInclude(typeof(Motor1MaximumCurrentRms))]
    [XmlInclude(typeof(Motor2MaximumCurrentRms))]
    [XmlInclude(typeof(Motor3MaximumCurrentRms))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0NominalStepInterval))]
    [XmlInclude(typeof(Motor1NominalStepInterval))]
    [XmlInclude(typeof(Motor2NominalStepInterval))]
    [XmlInclude(typeof(Motor3NominalStepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderRate))]
    [XmlInclude(typeof(Input0OperationMode))]
    [XmlInclude(typeof(Input1OperationMode))]
    [XmlInclude(typeof(Input2OperationMode))]
    [XmlInclude(typeof(Input3OperationMode))]
    [XmlInclude(typeof(Input0TriggerMode))]
    [XmlInclude(typeof(Input1TriggerMode))]
    [XmlInclude(typeof(Input2TriggerMode))]
    [XmlInclude(typeof(Input3TriggerMode))]
    [XmlInclude(typeof(EmergencyStopMode))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOvervoltageDetection))]
    [XmlInclude(typeof(MotorErrorDetection))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(EmergencyStop))]
    [XmlInclude(typeof(Motor0Steps))]
    [XmlInclude(typeof(Motor1Steps))]
    [XmlInclude(typeof(Motor2Steps))]
    [XmlInclude(typeof(Motor3Steps))]
    [XmlInclude(typeof(Motor0AccumulatedSteps))]
    [XmlInclude(typeof(Motor1AccumulatedSteps))]
    [XmlInclude(typeof(Motor2AccumulatedSteps))]
    [XmlInclude(typeof(Motor3AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor1MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor2MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor3MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor0MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor1MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor2MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor3MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor0ImmediateSteps))]
    [XmlInclude(typeof(Motor1ImmediateSteps))]
    [XmlInclude(typeof(Motor2ImmediateSteps))]
    [XmlInclude(typeof(Motor3ImmediateSteps))]
    [XmlInclude(typeof(StopMotorSuddenly))]
    [XmlInclude(typeof(StopMotorSmoothly))]
    [XmlInclude(typeof(ResetMotor))]
    [XmlInclude(typeof(ResetEncoder))]
    [XmlInclude(typeof(TimestampedEnableMotors))]
    [XmlInclude(typeof(TimestampedDisableMotors))]
    [XmlInclude(typeof(TimestampedEnableEncoders))]
    [XmlInclude(typeof(TimestampedDisableEncoders))]
    [XmlInclude(typeof(TimestampedEnableInputs))]
    [XmlInclude(typeof(TimestampedDisableInputs))]
    [XmlInclude(typeof(TimestampedMotor0OperationMode))]
    [XmlInclude(typeof(TimestampedMotor1OperationMode))]
    [XmlInclude(typeof(TimestampedMotor2OperationMode))]
    [XmlInclude(typeof(TimestampedMotor3OperationMode))]
    [XmlInclude(typeof(TimestampedMotor0MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor1MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor2MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor3MicrostepResolution))]
    [XmlInclude(typeof(TimestampedMotor0MaximumCurrentRms))]
    [XmlInclude(typeof(TimestampedMotor1MaximumCurrentRms))]
    [XmlInclude(typeof(TimestampedMotor2MaximumCurrentRms))]
    [XmlInclude(typeof(TimestampedMotor3MaximumCurrentRms))]
    [XmlInclude(typeof(TimestampedMotor0HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor1HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor2HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor3HoldCurrentReduction))]
    [XmlInclude(typeof(TimestampedMotor0NominalStepInterval))]
    [XmlInclude(typeof(TimestampedMotor1NominalStepInterval))]
    [XmlInclude(typeof(TimestampedMotor2NominalStepInterval))]
    [XmlInclude(typeof(TimestampedMotor3NominalStepInterval))]
    [XmlInclude(typeof(TimestampedMotor0MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor1MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor2MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor3MaximumStepInterval))]
    [XmlInclude(typeof(TimestampedMotor0StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor1StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor2StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedMotor3StepAccelerationInterval))]
    [XmlInclude(typeof(TimestampedEncoderMode))]
    [XmlInclude(typeof(TimestampedEncoderRate))]
    [XmlInclude(typeof(TimestampedInput0OperationMode))]
    [XmlInclude(typeof(TimestampedInput1OperationMode))]
    [XmlInclude(typeof(TimestampedInput2OperationMode))]
    [XmlInclude(typeof(TimestampedInput3OperationMode))]
    [XmlInclude(typeof(TimestampedInput0TriggerMode))]
    [XmlInclude(typeof(TimestampedInput1TriggerMode))]
    [XmlInclude(typeof(TimestampedInput2TriggerMode))]
    [XmlInclude(typeof(TimestampedInput3TriggerMode))]
    [XmlInclude(typeof(TimestampedEmergencyStopMode))]
    [XmlInclude(typeof(TimestampedMotorStopped))]
    [XmlInclude(typeof(TimestampedMotorOvervoltageDetection))]
    [XmlInclude(typeof(TimestampedMotorErrorDetection))]
    [XmlInclude(typeof(TimestampedEncoders))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedEmergencyStop))]
    [XmlInclude(typeof(TimestampedMotor0Steps))]
    [XmlInclude(typeof(TimestampedMotor1Steps))]
    [XmlInclude(typeof(TimestampedMotor2Steps))]
    [XmlInclude(typeof(TimestampedMotor3Steps))]
    [XmlInclude(typeof(TimestampedMotor0AccumulatedSteps))]
    [XmlInclude(typeof(TimestampedMotor1AccumulatedSteps))]
    [XmlInclude(typeof(TimestampedMotor2AccumulatedSteps))]
    [XmlInclude(typeof(TimestampedMotor3AccumulatedSteps))]
    [XmlInclude(typeof(TimestampedMotor0MaximumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor1MaximumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor2MaximumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor3MaximumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor0MinimumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor1MinimumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor2MinimumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor3MinimumStepsIntegration))]
    [XmlInclude(typeof(TimestampedMotor0ImmediateSteps))]
    [XmlInclude(typeof(TimestampedMotor1ImmediateSteps))]
    [XmlInclude(typeof(TimestampedMotor2ImmediateSteps))]
    [XmlInclude(typeof(TimestampedMotor3ImmediateSteps))]
    [XmlInclude(typeof(TimestampedStopMotorSuddenly))]
    [XmlInclude(typeof(TimestampedStopMotorSmoothly))]
    [XmlInclude(typeof(TimestampedResetMotor))]
    [XmlInclude(typeof(TimestampedResetEncoder))]
    [Description("Filters and selects specific messages reported by the StepperDriver device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new EnableMotors();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// StepperDriver register messages.
    /// </summary>
    /// <seealso cref="EnableMotors"/>
    /// <seealso cref="DisableMotors"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="DisableEncoders"/>
    /// <seealso cref="EnableInputs"/>
    /// <seealso cref="DisableInputs"/>
    /// <seealso cref="Motor0OperationMode"/>
    /// <seealso cref="Motor1OperationMode"/>
    /// <seealso cref="Motor2OperationMode"/>
    /// <seealso cref="Motor3OperationMode"/>
    /// <seealso cref="Motor0MicrostepResolution"/>
    /// <seealso cref="Motor1MicrostepResolution"/>
    /// <seealso cref="Motor2MicrostepResolution"/>
    /// <seealso cref="Motor3MicrostepResolution"/>
    /// <seealso cref="Motor0MaximumCurrentRms"/>
    /// <seealso cref="Motor1MaximumCurrentRms"/>
    /// <seealso cref="Motor2MaximumCurrentRms"/>
    /// <seealso cref="Motor3MaximumCurrentRms"/>
    /// <seealso cref="Motor0HoldCurrentReduction"/>
    /// <seealso cref="Motor1HoldCurrentReduction"/>
    /// <seealso cref="Motor2HoldCurrentReduction"/>
    /// <seealso cref="Motor3HoldCurrentReduction"/>
    /// <seealso cref="Motor0NominalStepInterval"/>
    /// <seealso cref="Motor1NominalStepInterval"/>
    /// <seealso cref="Motor2NominalStepInterval"/>
    /// <seealso cref="Motor3NominalStepInterval"/>
    /// <seealso cref="Motor0MaximumStepInterval"/>
    /// <seealso cref="Motor1MaximumStepInterval"/>
    /// <seealso cref="Motor2MaximumStepInterval"/>
    /// <seealso cref="Motor3MaximumStepInterval"/>
    /// <seealso cref="Motor0StepAccelerationInterval"/>
    /// <seealso cref="Motor1StepAccelerationInterval"/>
    /// <seealso cref="Motor2StepAccelerationInterval"/>
    /// <seealso cref="Motor3StepAccelerationInterval"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="EncoderRate"/>
    /// <seealso cref="Input0OperationMode"/>
    /// <seealso cref="Input1OperationMode"/>
    /// <seealso cref="Input2OperationMode"/>
    /// <seealso cref="Input3OperationMode"/>
    /// <seealso cref="Input0TriggerMode"/>
    /// <seealso cref="Input1TriggerMode"/>
    /// <seealso cref="Input2TriggerMode"/>
    /// <seealso cref="Input3TriggerMode"/>
    /// <seealso cref="EmergencyStopMode"/>
    /// <seealso cref="MotorStopped"/>
    /// <seealso cref="MotorOvervoltageDetection"/>
    /// <seealso cref="MotorErrorDetection"/>
    /// <seealso cref="Encoders"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="EmergencyStop"/>
    /// <seealso cref="Motor0Steps"/>
    /// <seealso cref="Motor1Steps"/>
    /// <seealso cref="Motor2Steps"/>
    /// <seealso cref="Motor3Steps"/>
    /// <seealso cref="Motor0AccumulatedSteps"/>
    /// <seealso cref="Motor1AccumulatedSteps"/>
    /// <seealso cref="Motor2AccumulatedSteps"/>
    /// <seealso cref="Motor3AccumulatedSteps"/>
    /// <seealso cref="Motor0MaximumStepsIntegration"/>
    /// <seealso cref="Motor1MaximumStepsIntegration"/>
    /// <seealso cref="Motor2MaximumStepsIntegration"/>
    /// <seealso cref="Motor3MaximumStepsIntegration"/>
    /// <seealso cref="Motor0MinimumStepsIntegration"/>
    /// <seealso cref="Motor1MinimumStepsIntegration"/>
    /// <seealso cref="Motor2MinimumStepsIntegration"/>
    /// <seealso cref="Motor3MinimumStepsIntegration"/>
    /// <seealso cref="Motor0ImmediateSteps"/>
    /// <seealso cref="Motor1ImmediateSteps"/>
    /// <seealso cref="Motor2ImmediateSteps"/>
    /// <seealso cref="Motor3ImmediateSteps"/>
    /// <seealso cref="StopMotorSuddenly"/>
    /// <seealso cref="StopMotorSmoothly"/>
    /// <seealso cref="ResetMotor"/>
    /// <seealso cref="ResetEncoder"/>
    [XmlInclude(typeof(EnableMotors))]
    [XmlInclude(typeof(DisableMotors))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(DisableEncoders))]
    [XmlInclude(typeof(EnableInputs))]
    [XmlInclude(typeof(DisableInputs))]
    [XmlInclude(typeof(Motor0OperationMode))]
    [XmlInclude(typeof(Motor1OperationMode))]
    [XmlInclude(typeof(Motor2OperationMode))]
    [XmlInclude(typeof(Motor3OperationMode))]
    [XmlInclude(typeof(Motor0MicrostepResolution))]
    [XmlInclude(typeof(Motor1MicrostepResolution))]
    [XmlInclude(typeof(Motor2MicrostepResolution))]
    [XmlInclude(typeof(Motor3MicrostepResolution))]
    [XmlInclude(typeof(Motor0MaximumCurrentRms))]
    [XmlInclude(typeof(Motor1MaximumCurrentRms))]
    [XmlInclude(typeof(Motor2MaximumCurrentRms))]
    [XmlInclude(typeof(Motor3MaximumCurrentRms))]
    [XmlInclude(typeof(Motor0HoldCurrentReduction))]
    [XmlInclude(typeof(Motor1HoldCurrentReduction))]
    [XmlInclude(typeof(Motor2HoldCurrentReduction))]
    [XmlInclude(typeof(Motor3HoldCurrentReduction))]
    [XmlInclude(typeof(Motor0NominalStepInterval))]
    [XmlInclude(typeof(Motor1NominalStepInterval))]
    [XmlInclude(typeof(Motor2NominalStepInterval))]
    [XmlInclude(typeof(Motor3NominalStepInterval))]
    [XmlInclude(typeof(Motor0MaximumStepInterval))]
    [XmlInclude(typeof(Motor1MaximumStepInterval))]
    [XmlInclude(typeof(Motor2MaximumStepInterval))]
    [XmlInclude(typeof(Motor3MaximumStepInterval))]
    [XmlInclude(typeof(Motor0StepAccelerationInterval))]
    [XmlInclude(typeof(Motor1StepAccelerationInterval))]
    [XmlInclude(typeof(Motor2StepAccelerationInterval))]
    [XmlInclude(typeof(Motor3StepAccelerationInterval))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(EncoderRate))]
    [XmlInclude(typeof(Input0OperationMode))]
    [XmlInclude(typeof(Input1OperationMode))]
    [XmlInclude(typeof(Input2OperationMode))]
    [XmlInclude(typeof(Input3OperationMode))]
    [XmlInclude(typeof(Input0TriggerMode))]
    [XmlInclude(typeof(Input1TriggerMode))]
    [XmlInclude(typeof(Input2TriggerMode))]
    [XmlInclude(typeof(Input3TriggerMode))]
    [XmlInclude(typeof(EmergencyStopMode))]
    [XmlInclude(typeof(MotorStopped))]
    [XmlInclude(typeof(MotorOvervoltageDetection))]
    [XmlInclude(typeof(MotorErrorDetection))]
    [XmlInclude(typeof(Encoders))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(EmergencyStop))]
    [XmlInclude(typeof(Motor0Steps))]
    [XmlInclude(typeof(Motor1Steps))]
    [XmlInclude(typeof(Motor2Steps))]
    [XmlInclude(typeof(Motor3Steps))]
    [XmlInclude(typeof(Motor0AccumulatedSteps))]
    [XmlInclude(typeof(Motor1AccumulatedSteps))]
    [XmlInclude(typeof(Motor2AccumulatedSteps))]
    [XmlInclude(typeof(Motor3AccumulatedSteps))]
    [XmlInclude(typeof(Motor0MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor1MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor2MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor3MaximumStepsIntegration))]
    [XmlInclude(typeof(Motor0MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor1MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor2MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor3MinimumStepsIntegration))]
    [XmlInclude(typeof(Motor0ImmediateSteps))]
    [XmlInclude(typeof(Motor1ImmediateSteps))]
    [XmlInclude(typeof(Motor2ImmediateSteps))]
    [XmlInclude(typeof(Motor3ImmediateSteps))]
    [XmlInclude(typeof(StopMotorSuddenly))]
    [XmlInclude(typeof(StopMotorSmoothly))]
    [XmlInclude(typeof(ResetMotor))]
    [XmlInclude(typeof(ResetEncoder))]
    [Description("Formats a sequence of values as specific StepperDriver register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new EnableMotors();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that specifies a set of motors to enable in the device.
    /// </summary>
    [Description("Specifies a set of motors to enable in the device.")]
    public partial class EnableMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableMotors"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableMotors"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableMotors"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableMotors"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableMotors"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableMotors"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableMotors register.
    /// </summary>
    /// <seealso cref="EnableMotors"/>
    [Description("Filters and selects timestamped messages from the EnableMotors register.")]
    public partial class TimestampedEnableMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableMotors.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return EnableMotors.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of motors to disable in the device.
    /// </summary>
    [Description("Specifies a set of motors to disable in the device.")]
    public partial class DisableMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableMotors"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableMotors"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableMotors"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableMotors"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableMotors"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableMotors"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableMotors register.
    /// </summary>
    /// <seealso cref="DisableMotors"/>
    [Description("Filters and selects timestamped messages from the DisableMotors register.")]
    public partial class TimestampedDisableMotors
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableMotors"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableMotors.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableMotors"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return DisableMotors.GetTimestampedPayload(message);
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
    public partial class EnableInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableInputs"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableInputs"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableInputs"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableInputs"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableInputs"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableInputs"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableInputs register.
    /// </summary>
    /// <seealso cref="EnableInputs"/>
    [Description("Filters and selects timestamped messages from the EnableInputs register.")]
    public partial class TimestampedEnableInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableInputs.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return EnableInputs.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [Description("Specifies a set of digital inputs to disable in the device.")]
    public partial class DisableInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableInputs"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableInputs"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableInputs"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableInputs"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableInputs"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableInputs"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableInputs register.
    /// </summary>
    /// <seealso cref="DisableInputs"/>
    [Description("Filters and selects timestamped messages from the DisableInputs register.")]
    public partial class TimestampedDisableInputs
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableInputs"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableInputs.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableInputs"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DisableInputs.GetTimestampedPayload(message);
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
    /// Represents a register that configures the maximum RMS current per phase for motor 0.
    /// </summary>
    [Description("Configures the maximum RMS current per phase for motor 0.")]
    public partial class Motor0MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MaximumCurrentRms"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumCurrentRms"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MaximumCurrentRms"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumCurrentRms"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MaximumCurrentRms register.
    /// </summary>
    /// <seealso cref="Motor0MaximumCurrentRms"/>
    [Description("Filters and selects timestamped messages from the Motor0MaximumCurrentRms register.")]
    public partial class TimestampedMotor0MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MaximumCurrentRms.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor0MaximumCurrentRms.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum RMS current per phase for motor 1.
    /// </summary>
    [Description("Configures the maximum RMS current per phase for motor 1.")]
    public partial class Motor1MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MaximumCurrentRms"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumCurrentRms"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MaximumCurrentRms"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumCurrentRms"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MaximumCurrentRms register.
    /// </summary>
    /// <seealso cref="Motor1MaximumCurrentRms"/>
    [Description("Filters and selects timestamped messages from the Motor1MaximumCurrentRms register.")]
    public partial class TimestampedMotor1MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MaximumCurrentRms.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor1MaximumCurrentRms.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum RMS current per phase for motor 2.
    /// </summary>
    [Description("Configures the maximum RMS current per phase for motor 2.")]
    public partial class Motor2MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MaximumCurrentRms"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumCurrentRms"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MaximumCurrentRms"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumCurrentRms"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MaximumCurrentRms register.
    /// </summary>
    /// <seealso cref="Motor2MaximumCurrentRms"/>
    [Description("Filters and selects timestamped messages from the Motor2MaximumCurrentRms register.")]
    public partial class TimestampedMotor2MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MaximumCurrentRms.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor2MaximumCurrentRms.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the maximum RMS current per phase for motor 3.
    /// </summary>
    [Description("Configures the maximum RMS current per phase for motor 3.")]
    public partial class Motor3MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MaximumCurrentRms"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumCurrentRms"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MaximumCurrentRms"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumCurrentRms"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MaximumCurrentRms register.
    /// </summary>
    /// <seealso cref="Motor3MaximumCurrentRms"/>
    [Description("Filters and selects timestamped messages from the Motor3MaximumCurrentRms register.")]
    public partial class TimestampedMotor3MaximumCurrentRms
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumCurrentRms"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MaximumCurrentRms.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MaximumCurrentRms"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Motor3MaximumCurrentRms.GetTimestampedPayload(message);
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
    /// Represents a register that configures the time between step motor pulses when running at nominal speed for motor 0.
    /// </summary>
    [Description("Configures the time between step motor pulses when running at nominal speed for motor 0.")]
    public partial class Motor0NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor0NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0NominalStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0NominalStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0NominalStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0NominalStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0NominalStepInterval register.
    /// </summary>
    /// <seealso cref="Motor0NominalStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor0NominalStepInterval register.")]
    public partial class TimestampedMotor0NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0NominalStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor0NominalStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses when running at nominal speed for motor 1.
    /// </summary>
    [Description("Configures the time between step motor pulses when running at nominal speed for motor 1.")]
    public partial class Motor1NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor1NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1NominalStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1NominalStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1NominalStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1NominalStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1NominalStepInterval register.
    /// </summary>
    /// <seealso cref="Motor1NominalStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor1NominalStepInterval register.")]
    public partial class TimestampedMotor1NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1NominalStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor1NominalStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses when running at nominal speed for motor 2.
    /// </summary>
    [Description("Configures the time between step motor pulses when running at nominal speed for motor 2.")]
    public partial class Motor2NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor2NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2NominalStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2NominalStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2NominalStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2NominalStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2NominalStepInterval register.
    /// </summary>
    /// <seealso cref="Motor2NominalStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor2NominalStepInterval register.")]
    public partial class TimestampedMotor2NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2NominalStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor2NominalStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses when running at nominal speed for motor 3.
    /// </summary>
    [Description("Configures the time between step motor pulses when running at nominal speed for motor 3.")]
    public partial class Motor3NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Motor3NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3NominalStepInterval"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3NominalStepInterval"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3NominalStepInterval"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3NominalStepInterval"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3NominalStepInterval register.
    /// </summary>
    /// <seealso cref="Motor3NominalStepInterval"/>
    [Description("Filters and selects timestamped messages from the Motor3NominalStepInterval register.")]
    public partial class TimestampedMotor3NominalStepInterval
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3NominalStepInterval"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3NominalStepInterval.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3NominalStepInterval"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Motor3NominalStepInterval.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
    /// </summary>
    [Description("Configures the time between step motor pulses used when starting or stopping a movement for motor 0.")]
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
    /// Represents a register that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
    /// </summary>
    [Description("Configures the time between step motor pulses used when starting or stopping a movement for motor 1.")]
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
    /// Represents a register that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
    /// </summary>
    [Description("Configures the time between step motor pulses used when starting or stopping a movement for motor 2.")]
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
    /// Represents a register that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
    /// </summary>
    [Description("Configures the time between step motor pulses used when starting or stopping a movement for motor 3.")]
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
    /// Represents a register that configures the operation mode of the quadrature encoders.
    /// </summary>
    [Description("Configures the operation mode of the quadrature encoders.")]
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
    /// Represents a register that configures the reading rate of the encoders' event.
    /// </summary>
    [Description("Configures the reading rate of the encoders' event.")]
    public partial class EncoderRate
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderRate"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderRate"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderRate"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderRateConfig GetPayload(HarpMessage message)
        {
            return (EncoderRateConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderRateConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderRateConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderRate"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderRate"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderRateConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderRate"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderRate"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderRateConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderRate register.
    /// </summary>
    /// <seealso cref="EncoderRate"/>
    [Description("Filters and selects timestamped messages from the EncoderRate register.")]
    public partial class TimestampedEncoderRate
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderRate"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderRate.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderRateConfig> GetPayload(HarpMessage message)
        {
            return EncoderRate.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 0.
    /// </summary>
    [Description("Configures the operation mode for digital input 0.")]
    public partial class Input0OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="Input0OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOperationMode GetPayload(HarpMessage message)
        {
            return (InputOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input0OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input0OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input0OperationMode register.
    /// </summary>
    /// <seealso cref="Input0OperationMode"/>
    [Description("Filters and selects timestamped messages from the Input0OperationMode register.")]
    public partial class TimestampedInput0OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input0OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input0OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetPayload(HarpMessage message)
        {
            return Input0OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 1.
    /// </summary>
    [Description("Configures the operation mode for digital input 1.")]
    public partial class Input1OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="Input1OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOperationMode GetPayload(HarpMessage message)
        {
            return (InputOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input1OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input1OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input1OperationMode register.
    /// </summary>
    /// <seealso cref="Input1OperationMode"/>
    [Description("Filters and selects timestamped messages from the Input1OperationMode register.")]
    public partial class TimestampedInput1OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input1OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input1OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetPayload(HarpMessage message)
        {
            return Input1OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 2.
    /// </summary>
    [Description("Configures the operation mode for digital input 2.")]
    public partial class Input2OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="Input2OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOperationMode GetPayload(HarpMessage message)
        {
            return (InputOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input2OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input2OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input2OperationMode register.
    /// </summary>
    /// <seealso cref="Input2OperationMode"/>
    [Description("Filters and selects timestamped messages from the Input2OperationMode register.")]
    public partial class TimestampedInput2OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input2OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input2OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetPayload(HarpMessage message)
        {
            return Input2OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode for digital input 3.
    /// </summary>
    [Description("Configures the operation mode for digital input 3.")]
    public partial class Input3OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="Input3OperationMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputOperationMode GetPayload(HarpMessage message)
        {
            return (InputOperationMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputOperationMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input3OperationMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3OperationMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input3OperationMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3OperationMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputOperationMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input3OperationMode register.
    /// </summary>
    /// <seealso cref="Input3OperationMode"/>
    [Description("Filters and selects timestamped messages from the Input3OperationMode register.")]
    public partial class TimestampedInput3OperationMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3OperationMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input3OperationMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input3OperationMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputOperationMode> GetPayload(HarpMessage message)
        {
            return Input3OperationMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the sense mode for digital input 0.
    /// </summary>
    [Description("Configures the sense mode for digital input 0.")]
    public partial class Input0TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="Input0TriggerMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input0TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input0TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerMode GetPayload(HarpMessage message)
        {
            return (TriggerMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input0TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input0TriggerMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0TriggerMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input0TriggerMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input0TriggerMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input0TriggerMode register.
    /// </summary>
    /// <seealso cref="Input0TriggerMode"/>
    [Description("Filters and selects timestamped messages from the Input0TriggerMode register.")]
    public partial class TimestampedInput0TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input0TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input0TriggerMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input0TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetPayload(HarpMessage message)
        {
            return Input0TriggerMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the sense mode for digital input 1.
    /// </summary>
    [Description("Configures the sense mode for digital input 1.")]
    public partial class Input1TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="Input1TriggerMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input1TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input1TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerMode GetPayload(HarpMessage message)
        {
            return (TriggerMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input1TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input1TriggerMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1TriggerMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input1TriggerMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input1TriggerMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input1TriggerMode register.
    /// </summary>
    /// <seealso cref="Input1TriggerMode"/>
    [Description("Filters and selects timestamped messages from the Input1TriggerMode register.")]
    public partial class TimestampedInput1TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input1TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input1TriggerMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input1TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetPayload(HarpMessage message)
        {
            return Input1TriggerMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the sense mode for digital input 2.
    /// </summary>
    [Description("Configures the sense mode for digital input 2.")]
    public partial class Input2TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="Input2TriggerMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input2TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input2TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerMode GetPayload(HarpMessage message)
        {
            return (TriggerMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input2TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input2TriggerMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2TriggerMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input2TriggerMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input2TriggerMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input2TriggerMode register.
    /// </summary>
    /// <seealso cref="Input2TriggerMode"/>
    [Description("Filters and selects timestamped messages from the Input2TriggerMode register.")]
    public partial class TimestampedInput2TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input2TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input2TriggerMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input2TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetPayload(HarpMessage message)
        {
            return Input2TriggerMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the sense mode for digital input 3.
    /// </summary>
    [Description("Configures the sense mode for digital input 3.")]
    public partial class Input3TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="Input3TriggerMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Input3TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Input3TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerMode GetPayload(HarpMessage message)
        {
            return (TriggerMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Input3TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Input3TriggerMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3TriggerMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Input3TriggerMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Input3TriggerMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Input3TriggerMode register.
    /// </summary>
    /// <seealso cref="Input3TriggerMode"/>
    [Description("Filters and selects timestamped messages from the Input3TriggerMode register.")]
    public partial class TimestampedInput3TriggerMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Input3TriggerMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Input3TriggerMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Input3TriggerMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetPayload(HarpMessage message)
        {
            return Input3TriggerMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the edge detection mode for the emergency external button.
    /// </summary>
    [Description("Configures the edge detection mode for the emergency external button.")]
    public partial class EmergencyStopMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EmergencyStopMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="EmergencyStopMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EmergencyStopMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EmergencyStopMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerMode GetPayload(HarpMessage message)
        {
            return (TriggerMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EmergencyStopMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EmergencyStopMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EmergencyStopMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EmergencyStopMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EmergencyStopMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EmergencyStopMode register.
    /// </summary>
    /// <seealso cref="EmergencyStopMode"/>
    [Description("Filters and selects timestamped messages from the EmergencyStopMode register.")]
    public partial class TimestampedEmergencyStopMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EmergencyStopMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EmergencyStopMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EmergencyStopMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerMode> GetPayload(HarpMessage message)
        {
            return EmergencyStopMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [Description("Contains a bit mask specifying the motor that stopped the movement.")]
    public partial class MotorStopped
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorStopped"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

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
    /// Represents a register that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
    /// </summary>
    [Description("Contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.")]
    public partial class MotorOvervoltageDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorOvervoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="MotorOvervoltageDetection"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MotorOvervoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MotorOvervoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MotorOvervoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MotorOvervoltageDetection"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorOvervoltageDetection"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MotorOvervoltageDetection"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorOvervoltageDetection"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MotorOvervoltageDetection register.
    /// </summary>
    /// <seealso cref="MotorOvervoltageDetection"/>
    [Description("Filters and selects timestamped messages from the MotorOvervoltageDetection register.")]
    public partial class TimestampedMotorOvervoltageDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorOvervoltageDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = MotorOvervoltageDetection.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MotorOvervoltageDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return MotorOvervoltageDetection.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
    /// </summary>
    [Description("Contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.")]
    public partial class MotorErrorDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorErrorDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="MotorErrorDetection"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MotorErrorDetection"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MotorErrorDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MotorErrorDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MotorErrorDetection"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorErrorDetection"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MotorErrorDetection"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MotorErrorDetection"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MotorErrorDetection register.
    /// </summary>
    /// <seealso cref="MotorErrorDetection"/>
    [Description("Filters and selects timestamped messages from the MotorErrorDetection register.")]
    public partial class TimestampedMotorErrorDetection
    {
        /// <summary>
        /// Represents the address of the <see cref="MotorErrorDetection"/> register. This field is constant.
        /// </summary>
        public const int Address = MotorErrorDetection.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MotorErrorDetection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return MotorErrorDetection.GetTimestampedPayload(message);
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
        public const int Address = 80;

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
        public const int Address = 81;

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
    /// Represents a register that contains the state of the external emergency button.
    /// </summary>
    [Description("Contains the state of the external emergency button.")]
    public partial class EmergencyStop
    {
        /// <summary>
        /// Represents the address of the <see cref="EmergencyStop"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="EmergencyStop"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EmergencyStop"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EmergencyStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EmergencyStopState GetPayload(HarpMessage message)
        {
            return (EmergencyStopState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EmergencyStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EmergencyStopState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EmergencyStopState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EmergencyStop"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EmergencyStop"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EmergencyStopState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EmergencyStop"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EmergencyStop"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EmergencyStopState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EmergencyStop register.
    /// </summary>
    /// <seealso cref="EmergencyStop"/>
    [Description("Filters and selects timestamped messages from the EmergencyStop register.")]
    public partial class TimestampedEmergencyStop
    {
        /// <summary>
        /// Represents the address of the <see cref="EmergencyStop"/> register. This field is constant.
        /// </summary>
        public const int Address = EmergencyStop.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EmergencyStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EmergencyStopState> GetPayload(HarpMessage message)
        {
            return EmergencyStop.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor0Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0Steps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0Steps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0Steps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0Steps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0Steps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0Steps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0Steps register.
    /// </summary>
    /// <seealso cref="Motor0Steps"/>
    [Description("Filters and selects timestamped messages from the Motor0Steps register.")]
    public partial class TimestampedMotor0Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0Steps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0Steps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor1Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1Steps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1Steps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1Steps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1Steps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1Steps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1Steps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1Steps register.
    /// </summary>
    /// <seealso cref="Motor1Steps"/>
    [Description("Filters and selects timestamped messages from the Motor1Steps register.")]
    public partial class TimestampedMotor1Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1Steps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1Steps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor2Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2Steps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2Steps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2Steps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2Steps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2Steps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2Steps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2Steps register.
    /// </summary>
    /// <seealso cref="Motor2Steps"/>
    [Description("Filters and selects timestamped messages from the Motor2Steps register.")]
    public partial class TimestampedMotor2Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2Steps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2Steps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [Description("Moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class Motor3Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3Steps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3Steps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3Steps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3Steps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3Steps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3Steps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3Steps register.
    /// </summary>
    /// <seealso cref="Motor3Steps"/>
    [Description("Filters and selects timestamped messages from the Motor3Steps register.")]
    public partial class TimestampedMotor3Steps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3Steps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3Steps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3Steps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3Steps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the accumulated steps of motor 0.
    /// </summary>
    [Description("Contains the accumulated steps of motor 0.")]
    public partial class Motor0AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0AccumulatedSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0AccumulatedSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0AccumulatedSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0AccumulatedSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0AccumulatedSteps register.
    /// </summary>
    /// <seealso cref="Motor0AccumulatedSteps"/>
    [Description("Filters and selects timestamped messages from the Motor0AccumulatedSteps register.")]
    public partial class TimestampedMotor0AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0AccumulatedSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0AccumulatedSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the accumulated steps of motor 1.
    /// </summary>
    [Description("Contains the accumulated steps of motor 1.")]
    public partial class Motor1AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 88;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1AccumulatedSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1AccumulatedSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1AccumulatedSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1AccumulatedSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1AccumulatedSteps register.
    /// </summary>
    /// <seealso cref="Motor1AccumulatedSteps"/>
    [Description("Filters and selects timestamped messages from the Motor1AccumulatedSteps register.")]
    public partial class TimestampedMotor1AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1AccumulatedSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1AccumulatedSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the accumulated steps of motor 2.
    /// </summary>
    [Description("Contains the accumulated steps of motor 2.")]
    public partial class Motor2AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 89;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2AccumulatedSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2AccumulatedSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2AccumulatedSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2AccumulatedSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2AccumulatedSteps register.
    /// </summary>
    /// <seealso cref="Motor2AccumulatedSteps"/>
    [Description("Filters and selects timestamped messages from the Motor2AccumulatedSteps register.")]
    public partial class TimestampedMotor2AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2AccumulatedSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2AccumulatedSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the accumulated steps of motor 3.
    /// </summary>
    [Description("Contains the accumulated steps of motor 3.")]
    public partial class Motor3AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3AccumulatedSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3AccumulatedSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3AccumulatedSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3AccumulatedSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3AccumulatedSteps register.
    /// </summary>
    /// <seealso cref="Motor3AccumulatedSteps"/>
    [Description("Filters and selects timestamped messages from the Motor3AccumulatedSteps register.")]
    public partial class TimestampedMotor3AccumulatedSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3AccumulatedSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3AccumulatedSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3AccumulatedSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3AccumulatedSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor0MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 91;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MaximumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MaximumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MaximumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MaximumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor0MaximumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor0MaximumStepsIntegration register.")]
    public partial class TimestampedMotor0MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MaximumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MaximumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor1MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 92;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MaximumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MaximumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MaximumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MaximumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor1MaximumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor1MaximumStepsIntegration register.")]
    public partial class TimestampedMotor1MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MaximumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MaximumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor2MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 93;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MaximumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MaximumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MaximumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MaximumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor2MaximumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor2MaximumStepsIntegration register.")]
    public partial class TimestampedMotor2MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MaximumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MaximumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor3MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 94;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MaximumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MaximumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MaximumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MaximumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor3MaximumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor3MaximumStepsIntegration register.")]
    public partial class TimestampedMotor3MaximumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MaximumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MaximumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MaximumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MaximumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor0MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 95;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0MinimumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MinimumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0MinimumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0MinimumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0MinimumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor0MinimumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor0MinimumStepsIntegration register.")]
    public partial class TimestampedMotor0MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0MinimumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0MinimumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor1MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 96;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1MinimumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MinimumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1MinimumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1MinimumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1MinimumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor1MinimumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor1MinimumStepsIntegration register.")]
    public partial class TimestampedMotor1MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1MinimumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1MinimumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor2MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 97;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2MinimumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MinimumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2MinimumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2MinimumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2MinimumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor2MinimumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor2MinimumStepsIntegration register.")]
    public partial class TimestampedMotor2MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2MinimumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2MinimumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [Description("Specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class Motor3MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = 98;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3MinimumStepsIntegration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MinimumStepsIntegration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3MinimumStepsIntegration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3MinimumStepsIntegration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3MinimumStepsIntegration register.
    /// </summary>
    /// <seealso cref="Motor3MinimumStepsIntegration"/>
    [Description("Filters and selects timestamped messages from the Motor3MinimumStepsIntegration register.")]
    public partial class TimestampedMotor3MinimumStepsIntegration
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3MinimumStepsIntegration"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3MinimumStepsIntegration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3MinimumStepsIntegration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3MinimumStepsIntegration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class Motor0ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 99;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor0ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor0ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor0ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor0ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor0ImmediateSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0ImmediateSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor0ImmediateSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor0ImmediateSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor0ImmediateSteps register.
    /// </summary>
    /// <seealso cref="Motor0ImmediateSteps"/>
    [Description("Filters and selects timestamped messages from the Motor0ImmediateSteps register.")]
    public partial class TimestampedMotor0ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor0ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor0ImmediateSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor0ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor0ImmediateSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class Motor1ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 100;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor1ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor1ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor1ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor1ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor1ImmediateSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1ImmediateSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor1ImmediateSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor1ImmediateSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor1ImmediateSteps register.
    /// </summary>
    /// <seealso cref="Motor1ImmediateSteps"/>
    [Description("Filters and selects timestamped messages from the Motor1ImmediateSteps register.")]
    public partial class TimestampedMotor1ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor1ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor1ImmediateSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor1ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor1ImmediateSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class Motor2ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 101;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor2ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor2ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor2ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor2ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor2ImmediateSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2ImmediateSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor2ImmediateSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor2ImmediateSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor2ImmediateSteps register.
    /// </summary>
    /// <seealso cref="Motor2ImmediateSteps"/>
    [Description("Filters and selects timestamped messages from the Motor2ImmediateSteps register.")]
    public partial class TimestampedMotor2ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor2ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor2ImmediateSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor2ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor2ImmediateSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [Description("Starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class Motor3ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = 102;

        /// <summary>
        /// Represents the payload type of the <see cref="Motor3ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S32;

        /// <summary>
        /// Represents the length of the <see cref="Motor3ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Motor3ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static int GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt32();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Motor3ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt32();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Motor3ImmediateSteps"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3ImmediateSteps"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Motor3ImmediateSteps"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Motor3ImmediateSteps"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, int value)
        {
            return HarpMessage.FromInt32(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Motor3ImmediateSteps register.
    /// </summary>
    /// <seealso cref="Motor3ImmediateSteps"/>
    [Description("Filters and selects timestamped messages from the Motor3ImmediateSteps register.")]
    public partial class TimestampedMotor3ImmediateSteps
    {
        /// <summary>
        /// Represents the address of the <see cref="Motor3ImmediateSteps"/> register. This field is constant.
        /// </summary>
        public const int Address = Motor3ImmediateSteps.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Motor3ImmediateSteps"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<int> GetPayload(HarpMessage message)
        {
            return Motor3ImmediateSteps.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the motors immediately.
    /// </summary>
    [Description("Stops the motors immediately.")]
    public partial class StopMotorSuddenly
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotorSuddenly"/> register. This field is constant.
        /// </summary>
        public const int Address = 103;

        /// <summary>
        /// Represents the payload type of the <see cref="StopMotorSuddenly"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StopMotorSuddenly"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StopMotorSuddenly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StopMotorSuddenly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StopMotorSuddenly"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotorSuddenly"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StopMotorSuddenly"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotorSuddenly"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StopMotorSuddenly register.
    /// </summary>
    /// <seealso cref="StopMotorSuddenly"/>
    [Description("Filters and selects timestamped messages from the StopMotorSuddenly register.")]
    public partial class TimestampedStopMotorSuddenly
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotorSuddenly"/> register. This field is constant.
        /// </summary>
        public const int Address = StopMotorSuddenly.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StopMotorSuddenly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return StopMotorSuddenly.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that decelerate the motors until they stop according to configured intervals.
    /// </summary>
    [Description("Decelerate the motors until they stop according to configured intervals.")]
    public partial class StopMotorSmoothly
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotorSmoothly"/> register. This field is constant.
        /// </summary>
        public const int Address = 104;

        /// <summary>
        /// Represents the payload type of the <see cref="StopMotorSmoothly"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StopMotorSmoothly"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StopMotorSmoothly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StopMotorSmoothly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StopMotorSmoothly"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotorSmoothly"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StopMotorSmoothly"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopMotorSmoothly"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StopMotorSmoothly register.
    /// </summary>
    /// <seealso cref="StopMotorSmoothly"/>
    [Description("Filters and selects timestamped messages from the StopMotorSmoothly register.")]
    public partial class TimestampedStopMotorSmoothly
    {
        /// <summary>
        /// Represents the address of the <see cref="StopMotorSmoothly"/> register. This field is constant.
        /// </summary>
        public const int Address = StopMotorSmoothly.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StopMotorSmoothly"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return StopMotorSmoothly.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that resets the internal motor driver which also clears any eventual error.
    /// </summary>
    [Description("Resets the internal motor driver which also clears any eventual error.")]
    public partial class ResetMotor
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetMotor"/> register. This field is constant.
        /// </summary>
        public const int Address = 105;

        /// <summary>
        /// Represents the payload type of the <see cref="ResetMotor"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ResetMotor"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ResetMotor"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StepperMotors GetPayload(HarpMessage message)
        {
            return (StepperMotors)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ResetMotor"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StepperMotors)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ResetMotor"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetMotor"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ResetMotor"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetMotor"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StepperMotors value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ResetMotor register.
    /// </summary>
    /// <seealso cref="ResetMotor"/>
    [Description("Filters and selects timestamped messages from the ResetMotor register.")]
    public partial class TimestampedResetMotor
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetMotor"/> register. This field is constant.
        /// </summary>
        public const int Address = ResetMotor.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ResetMotor"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StepperMotors> GetPayload(HarpMessage message)
        {
            return ResetMotor.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that resets the encoder.
    /// </summary>
    [Description("Resets the encoder.")]
    public partial class ResetEncoder
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetEncoder"/> register. This field is constant.
        /// </summary>
        public const int Address = 106;

        /// <summary>
        /// Represents the payload type of the <see cref="ResetEncoder"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ResetEncoder"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ResetEncoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static QuadratureEncoders GetPayload(HarpMessage message)
        {
            return (QuadratureEncoders)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ResetEncoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((QuadratureEncoders)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ResetEncoder"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetEncoder"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ResetEncoder"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ResetEncoder"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, QuadratureEncoders value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ResetEncoder register.
    /// </summary>
    /// <seealso cref="ResetEncoder"/>
    [Description("Filters and selects timestamped messages from the ResetEncoder register.")]
    public partial class TimestampedResetEncoder
    {
        /// <summary>
        /// Represents the address of the <see cref="ResetEncoder"/> register. This field is constant.
        /// </summary>
        public const int Address = ResetEncoder.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ResetEncoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<QuadratureEncoders> GetPayload(HarpMessage message)
        {
            return ResetEncoder.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that contains the CFG configuration pins of the TMC2210 driver that controls motor 0.
    /// </summary>
    [Description("Contains the CFG configuration pins of the TMC2210 driver that controls motor 0.")]
    internal partial class Reserved0
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int Address = 107;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the CFG configuration pins of the TMC2210 driver that controls motor 1.
    /// </summary>
    [Description("Contains the CFG configuration pins of the TMC2210 driver that controls motor 1.")]
    internal partial class Reserved1
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int Address = 108;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the CFG configuration pins of the TMC2210 driver that controls motor 2.
    /// </summary>
    [Description("Contains the CFG configuration pins of the TMC2210 driver that controls motor 2.")]
    internal partial class Reserved2
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int Address = 109;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the CFG configuration pins of the TMC2210 driver that controls motor 3.
    /// </summary>
    [Description("Contains the CFG configuration pins of the TMC2210 driver that controls motor 3.")]
    internal partial class Reserved3
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int Address = 110;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the raw data of the digital potentiometer that controls current limit of motor 0.
    /// </summary>
    [Description("Contains the raw data of the digital potentiometer that controls current limit of motor 0.")]
    internal partial class Reserved4
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int Address = 111;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the raw data of the digital potentiometer that controls current limit of motor 1.
    /// </summary>
    [Description("Contains the raw data of the digital potentiometer that controls current limit of motor 1.")]
    internal partial class Reserved5
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int Address = 112;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the raw data of the digital potentiometer that controls current limit of motor 2.
    /// </summary>
    [Description("Contains the raw data of the digital potentiometer that controls current limit of motor 2.")]
    internal partial class Reserved6
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int Address = 113;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that contains the raw data of the digital potentiometer that controls current limit of motor 3.
    /// </summary>
    [Description("Contains the raw data of the digital potentiometer that controls current limit of motor 3.")]
    internal partial class Reserved7
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int Address = 114;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// StepperDriver device.
    /// </summary>
    /// <seealso cref="CreateEnableMotorsPayload"/>
    /// <seealso cref="CreateDisableMotorsPayload"/>
    /// <seealso cref="CreateEnableEncodersPayload"/>
    /// <seealso cref="CreateDisableEncodersPayload"/>
    /// <seealso cref="CreateEnableInputsPayload"/>
    /// <seealso cref="CreateDisableInputsPayload"/>
    /// <seealso cref="CreateMotor0OperationModePayload"/>
    /// <seealso cref="CreateMotor1OperationModePayload"/>
    /// <seealso cref="CreateMotor2OperationModePayload"/>
    /// <seealso cref="CreateMotor3OperationModePayload"/>
    /// <seealso cref="CreateMotor0MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor1MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor2MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor3MicrostepResolutionPayload"/>
    /// <seealso cref="CreateMotor0MaximumCurrentRmsPayload"/>
    /// <seealso cref="CreateMotor1MaximumCurrentRmsPayload"/>
    /// <seealso cref="CreateMotor2MaximumCurrentRmsPayload"/>
    /// <seealso cref="CreateMotor3MaximumCurrentRmsPayload"/>
    /// <seealso cref="CreateMotor0HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor1HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor2HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor3HoldCurrentReductionPayload"/>
    /// <seealso cref="CreateMotor0NominalStepIntervalPayload"/>
    /// <seealso cref="CreateMotor1NominalStepIntervalPayload"/>
    /// <seealso cref="CreateMotor2NominalStepIntervalPayload"/>
    /// <seealso cref="CreateMotor3NominalStepIntervalPayload"/>
    /// <seealso cref="CreateMotor0MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor1MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor2MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor3MaximumStepIntervalPayload"/>
    /// <seealso cref="CreateMotor0StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor1StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor2StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateMotor3StepAccelerationIntervalPayload"/>
    /// <seealso cref="CreateEncoderModePayload"/>
    /// <seealso cref="CreateEncoderRatePayload"/>
    /// <seealso cref="CreateInput0OperationModePayload"/>
    /// <seealso cref="CreateInput1OperationModePayload"/>
    /// <seealso cref="CreateInput2OperationModePayload"/>
    /// <seealso cref="CreateInput3OperationModePayload"/>
    /// <seealso cref="CreateInput0TriggerModePayload"/>
    /// <seealso cref="CreateInput1TriggerModePayload"/>
    /// <seealso cref="CreateInput2TriggerModePayload"/>
    /// <seealso cref="CreateInput3TriggerModePayload"/>
    /// <seealso cref="CreateEmergencyStopModePayload"/>
    /// <seealso cref="CreateMotorStoppedPayload"/>
    /// <seealso cref="CreateMotorOvervoltageDetectionPayload"/>
    /// <seealso cref="CreateMotorErrorDetectionPayload"/>
    /// <seealso cref="CreateEncodersPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateEmergencyStopPayload"/>
    /// <seealso cref="CreateMotor0StepsPayload"/>
    /// <seealso cref="CreateMotor1StepsPayload"/>
    /// <seealso cref="CreateMotor2StepsPayload"/>
    /// <seealso cref="CreateMotor3StepsPayload"/>
    /// <seealso cref="CreateMotor0AccumulatedStepsPayload"/>
    /// <seealso cref="CreateMotor1AccumulatedStepsPayload"/>
    /// <seealso cref="CreateMotor2AccumulatedStepsPayload"/>
    /// <seealso cref="CreateMotor3AccumulatedStepsPayload"/>
    /// <seealso cref="CreateMotor0MaximumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor1MaximumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor2MaximumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor3MaximumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor0MinimumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor1MinimumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor2MinimumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor3MinimumStepsIntegrationPayload"/>
    /// <seealso cref="CreateMotor0ImmediateStepsPayload"/>
    /// <seealso cref="CreateMotor1ImmediateStepsPayload"/>
    /// <seealso cref="CreateMotor2ImmediateStepsPayload"/>
    /// <seealso cref="CreateMotor3ImmediateStepsPayload"/>
    /// <seealso cref="CreateStopMotorSuddenlyPayload"/>
    /// <seealso cref="CreateStopMotorSmoothlyPayload"/>
    /// <seealso cref="CreateResetMotorPayload"/>
    /// <seealso cref="CreateResetEncoderPayload"/>
    [XmlInclude(typeof(CreateEnableMotorsPayload))]
    [XmlInclude(typeof(CreateDisableMotorsPayload))]
    [XmlInclude(typeof(CreateEnableEncodersPayload))]
    [XmlInclude(typeof(CreateDisableEncodersPayload))]
    [XmlInclude(typeof(CreateEnableInputsPayload))]
    [XmlInclude(typeof(CreateDisableInputsPayload))]
    [XmlInclude(typeof(CreateMotor0OperationModePayload))]
    [XmlInclude(typeof(CreateMotor1OperationModePayload))]
    [XmlInclude(typeof(CreateMotor2OperationModePayload))]
    [XmlInclude(typeof(CreateMotor3OperationModePayload))]
    [XmlInclude(typeof(CreateMotor0MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor1MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor2MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor3MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateMotor0MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateMotor1MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateMotor2MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateMotor3MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateMotor0HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor1HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor2HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor3HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateMotor0NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor0MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateMotor0StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor1StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor2StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateMotor3StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateEncoderModePayload))]
    [XmlInclude(typeof(CreateEncoderRatePayload))]
    [XmlInclude(typeof(CreateInput0OperationModePayload))]
    [XmlInclude(typeof(CreateInput1OperationModePayload))]
    [XmlInclude(typeof(CreateInput2OperationModePayload))]
    [XmlInclude(typeof(CreateInput3OperationModePayload))]
    [XmlInclude(typeof(CreateInput0TriggerModePayload))]
    [XmlInclude(typeof(CreateInput1TriggerModePayload))]
    [XmlInclude(typeof(CreateInput2TriggerModePayload))]
    [XmlInclude(typeof(CreateInput3TriggerModePayload))]
    [XmlInclude(typeof(CreateEmergencyStopModePayload))]
    [XmlInclude(typeof(CreateMotorStoppedPayload))]
    [XmlInclude(typeof(CreateMotorOvervoltageDetectionPayload))]
    [XmlInclude(typeof(CreateMotorErrorDetectionPayload))]
    [XmlInclude(typeof(CreateEncodersPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateEmergencyStopPayload))]
    [XmlInclude(typeof(CreateMotor0StepsPayload))]
    [XmlInclude(typeof(CreateMotor1StepsPayload))]
    [XmlInclude(typeof(CreateMotor2StepsPayload))]
    [XmlInclude(typeof(CreateMotor3StepsPayload))]
    [XmlInclude(typeof(CreateMotor0AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateMotor1AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateMotor2AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateMotor3AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateMotor0MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor1MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor2MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor3MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor0MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor1MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor2MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor3MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateMotor0ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateMotor1ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateMotor2ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateMotor3ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateStopMotorSuddenlyPayload))]
    [XmlInclude(typeof(CreateStopMotorSmoothlyPayload))]
    [XmlInclude(typeof(CreateResetMotorPayload))]
    [XmlInclude(typeof(CreateResetEncoderPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableMotorsPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableMotorsPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableInputsPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableInputsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MicrostepResolutionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaximumCurrentRmsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3HoldCurrentReductionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3NominalStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaximumStepIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3StepAccelerationIntervalPayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderModePayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderRatePayload))]
    [XmlInclude(typeof(CreateTimestampedInput0OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput1OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput2OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput3OperationModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput0TriggerModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput1TriggerModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput2TriggerModePayload))]
    [XmlInclude(typeof(CreateTimestampedInput3TriggerModePayload))]
    [XmlInclude(typeof(CreateTimestampedEmergencyStopModePayload))]
    [XmlInclude(typeof(CreateTimestampedMotorStoppedPayload))]
    [XmlInclude(typeof(CreateTimestampedMotorOvervoltageDetectionPayload))]
    [XmlInclude(typeof(CreateTimestampedMotorErrorDetectionPayload))]
    [XmlInclude(typeof(CreateTimestampedEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedEmergencyStopPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0StepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1StepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2StepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3StepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3AccumulatedStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MaximumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3MinimumStepsIntegrationPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor0ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor1ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor2ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedMotor3ImmediateStepsPayload))]
    [XmlInclude(typeof(CreateTimestampedStopMotorSuddenlyPayload))]
    [XmlInclude(typeof(CreateTimestampedStopMotorSmoothlyPayload))]
    [XmlInclude(typeof(CreateTimestampedResetMotorPayload))]
    [XmlInclude(typeof(CreateTimestampedResetEncoderPayload))]
    [Description("Creates standard message payloads for the StepperDriver device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateEnableMotorsPayload();
        }

        string INamedElement.Name => $"{nameof(StepperDriver)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of motors to enable in the device.
    /// </summary>
    [DisplayName("EnableMotorsPayload")]
    [Description("Creates a message payload that specifies a set of motors to enable in the device.")]
    public partial class CreateEnableMotorsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of motors to enable in the device.
        /// </summary>
        [Description("The value that specifies a set of motors to enable in the device.")]
        public StepperMotors EnableMotors { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableMotors register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return EnableMotors;
        }

        /// <summary>
        /// Creates a message that specifies a set of motors to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableMotors register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EnableMotors.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of motors to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableMotorsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of motors to enable in the device.")]
    public partial class CreateTimestampedEnableMotorsPayload : CreateEnableMotorsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of motors to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableMotors register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EnableMotors.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of motors to disable in the device.
    /// </summary>
    [DisplayName("DisableMotorsPayload")]
    [Description("Creates a message payload that specifies a set of motors to disable in the device.")]
    public partial class CreateDisableMotorsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of motors to disable in the device.
        /// </summary>
        [Description("The value that specifies a set of motors to disable in the device.")]
        public StepperMotors DisableMotors { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableMotors register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return DisableMotors;
        }

        /// <summary>
        /// Creates a message that specifies a set of motors to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableMotors register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DisableMotors.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of motors to disable in the device.
    /// </summary>
    [DisplayName("TimestampedDisableMotorsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of motors to disable in the device.")]
    public partial class CreateTimestampedDisableMotorsPayload : CreateDisableMotorsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of motors to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableMotors register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DisableMotors.FromPayload(timestamp, messageType, GetPayload());
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
    [DisplayName("EnableInputsPayload")]
    [Description("Creates a message payload that specifies a set of digital inputs to enable in the device.")]
    public partial class CreateEnableInputsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of digital inputs to enable in the device.
        /// </summary>
        [Description("The value that specifies a set of digital inputs to enable in the device.")]
        public DigitalInputs EnableInputs { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableInputs register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return EnableInputs;
        }

        /// <summary>
        /// Creates a message that specifies a set of digital inputs to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableInputs register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EnableInputs.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of digital inputs to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableInputsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of digital inputs to enable in the device.")]
    public partial class CreateTimestampedEnableInputsPayload : CreateEnableInputsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of digital inputs to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableInputs register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EnableInputs.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [DisplayName("DisableInputsPayload")]
    [Description("Creates a message payload that specifies a set of digital inputs to disable in the device.")]
    public partial class CreateDisableInputsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies a set of digital inputs to disable in the device.
        /// </summary>
        [Description("The value that specifies a set of digital inputs to disable in the device.")]
        public DigitalInputs DisableInputs { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableInputs register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DisableInputs;
        }

        /// <summary>
        /// Creates a message that specifies a set of digital inputs to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableInputs register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.DisableInputs.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies a set of digital inputs to disable in the device.
    /// </summary>
    [DisplayName("TimestampedDisableInputsPayload")]
    [Description("Creates a timestamped message payload that specifies a set of digital inputs to disable in the device.")]
    public partial class CreateTimestampedDisableInputsPayload : CreateDisableInputsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies a set of digital inputs to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableInputs register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.DisableInputs.FromPayload(timestamp, messageType, GetPayload());
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
    /// that configures the maximum RMS current per phase for motor 0.
    /// </summary>
    [DisplayName("Motor0MaximumCurrentRmsPayload")]
    [Description("Creates a message payload that configures the maximum RMS current per phase for motor 0.")]
    public partial class CreateMotor0MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum RMS current per phase for motor 0.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum RMS current per phase for motor 0.")]
        public float Motor0MaximumCurrentRms { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor0MaximumCurrentRms register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor0MaximumCurrentRms;
        }

        /// <summary>
        /// Creates a message that configures the maximum RMS current per phase for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumCurrentRms.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum RMS current per phase for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0MaximumCurrentRmsPayload")]
    [Description("Creates a timestamped message payload that configures the maximum RMS current per phase for motor 0.")]
    public partial class CreateTimestampedMotor0MaximumCurrentRmsPayload : CreateMotor0MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum RMS current per phase for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumCurrentRms.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum RMS current per phase for motor 1.
    /// </summary>
    [DisplayName("Motor1MaximumCurrentRmsPayload")]
    [Description("Creates a message payload that configures the maximum RMS current per phase for motor 1.")]
    public partial class CreateMotor1MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum RMS current per phase for motor 1.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum RMS current per phase for motor 1.")]
        public float Motor1MaximumCurrentRms { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor1MaximumCurrentRms register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor1MaximumCurrentRms;
        }

        /// <summary>
        /// Creates a message that configures the maximum RMS current per phase for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumCurrentRms.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum RMS current per phase for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1MaximumCurrentRmsPayload")]
    [Description("Creates a timestamped message payload that configures the maximum RMS current per phase for motor 1.")]
    public partial class CreateTimestampedMotor1MaximumCurrentRmsPayload : CreateMotor1MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum RMS current per phase for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumCurrentRms.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum RMS current per phase for motor 2.
    /// </summary>
    [DisplayName("Motor2MaximumCurrentRmsPayload")]
    [Description("Creates a message payload that configures the maximum RMS current per phase for motor 2.")]
    public partial class CreateMotor2MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum RMS current per phase for motor 2.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum RMS current per phase for motor 2.")]
        public float Motor2MaximumCurrentRms { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor2MaximumCurrentRms register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor2MaximumCurrentRms;
        }

        /// <summary>
        /// Creates a message that configures the maximum RMS current per phase for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumCurrentRms.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum RMS current per phase for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2MaximumCurrentRmsPayload")]
    [Description("Creates a timestamped message payload that configures the maximum RMS current per phase for motor 2.")]
    public partial class CreateTimestampedMotor2MaximumCurrentRmsPayload : CreateMotor2MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum RMS current per phase for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumCurrentRms.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the maximum RMS current per phase for motor 3.
    /// </summary>
    [DisplayName("Motor3MaximumCurrentRmsPayload")]
    [Description("Creates a message payload that configures the maximum RMS current per phase for motor 3.")]
    public partial class CreateMotor3MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the maximum RMS current per phase for motor 3.
        /// </summary>
        [Range(min: 0.139, max: 2.1)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the maximum RMS current per phase for motor 3.")]
        public float Motor3MaximumCurrentRms { get; set; } = 0.2F;

        /// <summary>
        /// Creates a message payload for the Motor3MaximumCurrentRms register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public float GetPayload()
        {
            return Motor3MaximumCurrentRms;
        }

        /// <summary>
        /// Creates a message that configures the maximum RMS current per phase for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumCurrentRms.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the maximum RMS current per phase for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3MaximumCurrentRmsPayload")]
    [Description("Creates a timestamped message payload that configures the maximum RMS current per phase for motor 3.")]
    public partial class CreateTimestampedMotor3MaximumCurrentRmsPayload : CreateMotor3MaximumCurrentRmsPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the maximum RMS current per phase for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MaximumCurrentRms register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumCurrentRms.FromPayload(timestamp, messageType, GetPayload());
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
    /// that configures the time between step motor pulses when running at nominal speed for motor 0.
    /// </summary>
    [DisplayName("Motor0NominalStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses when running at nominal speed for motor 0.")]
    public partial class CreateMotor0NominalStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses when running at nominal speed for motor 0.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses when running at nominal speed for motor 0.")]
        public ushort Motor0NominalStepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor0NominalStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor0NominalStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses when running at nominal speed for motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0NominalStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0NominalStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0NominalStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses when running at nominal speed for motor 0.")]
    public partial class CreateTimestampedMotor0NominalStepIntervalPayload : CreateMotor0NominalStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses when running at nominal speed for motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0NominalStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0NominalStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 1.
    /// </summary>
    [DisplayName("Motor1NominalStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses when running at nominal speed for motor 1.")]
    public partial class CreateMotor1NominalStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses when running at nominal speed for motor 1.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses when running at nominal speed for motor 1.")]
        public ushort Motor1NominalStepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor1NominalStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor1NominalStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses when running at nominal speed for motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1NominalStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1NominalStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1NominalStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses when running at nominal speed for motor 1.")]
    public partial class CreateTimestampedMotor1NominalStepIntervalPayload : CreateMotor1NominalStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses when running at nominal speed for motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1NominalStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1NominalStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 2.
    /// </summary>
    [DisplayName("Motor2NominalStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses when running at nominal speed for motor 2.")]
    public partial class CreateMotor2NominalStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses when running at nominal speed for motor 2.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses when running at nominal speed for motor 2.")]
        public ushort Motor2NominalStepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor2NominalStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor2NominalStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses when running at nominal speed for motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2NominalStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2NominalStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2NominalStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses when running at nominal speed for motor 2.")]
    public partial class CreateTimestampedMotor2NominalStepIntervalPayload : CreateMotor2NominalStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses when running at nominal speed for motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2NominalStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2NominalStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 3.
    /// </summary>
    [DisplayName("Motor3NominalStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses when running at nominal speed for motor 3.")]
    public partial class CreateMotor3NominalStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses when running at nominal speed for motor 3.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses when running at nominal speed for motor 3.")]
        public ushort Motor3NominalStepInterval { get; set; } = 250;

        /// <summary>
        /// Creates a message payload for the Motor3NominalStepInterval register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Motor3NominalStepInterval;
        }

        /// <summary>
        /// Creates a message that configures the time between step motor pulses when running at nominal speed for motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3NominalStepInterval register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3NominalStepInterval.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the time between step motor pulses when running at nominal speed for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3NominalStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses when running at nominal speed for motor 3.")]
    public partial class CreateTimestampedMotor3NominalStepIntervalPayload : CreateMotor3NominalStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses when running at nominal speed for motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3NominalStepInterval register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3NominalStepInterval.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
    /// </summary>
    [DisplayName("Motor0MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 0.")]
    public partial class CreateMotor0MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses used when starting or stopping a movement for motor 0.")]
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
        /// Creates a message that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 0.")]
    public partial class CreateTimestampedMotor0MaximumStepIntervalPayload : CreateMotor0MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses used when starting or stopping a movement for motor 0.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
    /// </summary>
    [DisplayName("Motor1MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 1.")]
    public partial class CreateMotor1MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses used when starting or stopping a movement for motor 1.")]
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
        /// Creates a message that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 1.")]
    public partial class CreateTimestampedMotor1MaximumStepIntervalPayload : CreateMotor1MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses used when starting or stopping a movement for motor 1.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
    /// </summary>
    [DisplayName("Motor2MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 2.")]
    public partial class CreateMotor2MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses used when starting or stopping a movement for motor 2.")]
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
        /// Creates a message that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 2.")]
    public partial class CreateTimestampedMotor2MaximumStepIntervalPayload : CreateMotor2MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses used when starting or stopping a movement for motor 2.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
    /// </summary>
    [DisplayName("Motor3MaximumStepIntervalPayload")]
    [Description("Creates a message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 3.")]
    public partial class CreateMotor3MaximumStepIntervalPayload
    {
        /// <summary>
        /// Gets or sets the value that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
        /// </summary>
        [Range(min: 100, max: 20000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that configures the time between step motor pulses used when starting or stopping a movement for motor 3.")]
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
        /// Creates a message that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
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
    /// that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3MaximumStepIntervalPayload")]
    [Description("Creates a timestamped message payload that configures the time between step motor pulses used when starting or stopping a movement for motor 3.")]
    public partial class CreateTimestampedMotor3MaximumStepIntervalPayload : CreateMotor3MaximumStepIntervalPayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the time between step motor pulses used when starting or stopping a movement for motor 3.
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
    /// that configures the operation mode of the quadrature encoders.
    /// </summary>
    [DisplayName("EncoderModePayload")]
    [Description("Creates a message payload that configures the operation mode of the quadrature encoders.")]
    public partial class CreateEncoderModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode of the quadrature encoders.
        /// </summary>
        [Description("The value that configures the operation mode of the quadrature encoders.")]
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
        /// Creates a message that configures the operation mode of the quadrature encoders.
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
    /// that configures the operation mode of the quadrature encoders.
    /// </summary>
    [DisplayName("TimestampedEncoderModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode of the quadrature encoders.")]
    public partial class CreateTimestampedEncoderModePayload : CreateEncoderModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode of the quadrature encoders.
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
    /// that configures the reading rate of the encoders' event.
    /// </summary>
    [DisplayName("EncoderRatePayload")]
    [Description("Creates a message payload that configures the reading rate of the encoders' event.")]
    public partial class CreateEncoderRatePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the reading rate of the encoders' event.
        /// </summary>
        [Description("The value that configures the reading rate of the encoders' event.")]
        public EncoderRateConfig EncoderRate { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderRate register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderRateConfig GetPayload()
        {
            return EncoderRate;
        }

        /// <summary>
        /// Creates a message that configures the reading rate of the encoders' event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderRate register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EncoderRate.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the reading rate of the encoders' event.
    /// </summary>
    [DisplayName("TimestampedEncoderRatePayload")]
    [Description("Creates a timestamped message payload that configures the reading rate of the encoders' event.")]
    public partial class CreateTimestampedEncoderRatePayload : CreateEncoderRatePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the reading rate of the encoders' event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderRate register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EncoderRate.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 0.
    /// </summary>
    [DisplayName("Input0OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 0.")]
    public partial class CreateInput0OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 0.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 0.")]
        public InputOperationMode Input0OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input0OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOperationMode GetPayload()
        {
            return Input0OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input0OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input0OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 0.
    /// </summary>
    [DisplayName("TimestampedInput0OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 0.")]
    public partial class CreateTimestampedInput0OperationModePayload : CreateInput0OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input0OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input0OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 1.
    /// </summary>
    [DisplayName("Input1OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 1.")]
    public partial class CreateInput1OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 1.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 1.")]
        public InputOperationMode Input1OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input1OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOperationMode GetPayload()
        {
            return Input1OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input1OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input1OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 1.
    /// </summary>
    [DisplayName("TimestampedInput1OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 1.")]
    public partial class CreateTimestampedInput1OperationModePayload : CreateInput1OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input1OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input1OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 2.
    /// </summary>
    [DisplayName("Input2OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 2.")]
    public partial class CreateInput2OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 2.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 2.")]
        public InputOperationMode Input2OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input2OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOperationMode GetPayload()
        {
            return Input2OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input2OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input2OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 2.
    /// </summary>
    [DisplayName("TimestampedInput2OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 2.")]
    public partial class CreateTimestampedInput2OperationModePayload : CreateInput2OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input2OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input2OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode for digital input 3.
    /// </summary>
    [DisplayName("Input3OperationModePayload")]
    [Description("Creates a message payload that configures the operation mode for digital input 3.")]
    public partial class CreateInput3OperationModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode for digital input 3.
        /// </summary>
        [Description("The value that configures the operation mode for digital input 3.")]
        public InputOperationMode Input3OperationMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input3OperationMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputOperationMode GetPayload()
        {
            return Input3OperationMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode for digital input 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input3OperationMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input3OperationMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode for digital input 3.
    /// </summary>
    [DisplayName("TimestampedInput3OperationModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode for digital input 3.")]
    public partial class CreateTimestampedInput3OperationModePayload : CreateInput3OperationModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode for digital input 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input3OperationMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input3OperationMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the sense mode for digital input 0.
    /// </summary>
    [DisplayName("Input0TriggerModePayload")]
    [Description("Creates a message payload that configures the sense mode for digital input 0.")]
    public partial class CreateInput0TriggerModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the sense mode for digital input 0.
        /// </summary>
        [Description("The value that configures the sense mode for digital input 0.")]
        public TriggerMode Input0TriggerMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input0TriggerMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerMode GetPayload()
        {
            return Input0TriggerMode;
        }

        /// <summary>
        /// Creates a message that configures the sense mode for digital input 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input0TriggerMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input0TriggerMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the sense mode for digital input 0.
    /// </summary>
    [DisplayName("TimestampedInput0TriggerModePayload")]
    [Description("Creates a timestamped message payload that configures the sense mode for digital input 0.")]
    public partial class CreateTimestampedInput0TriggerModePayload : CreateInput0TriggerModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the sense mode for digital input 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input0TriggerMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input0TriggerMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the sense mode for digital input 1.
    /// </summary>
    [DisplayName("Input1TriggerModePayload")]
    [Description("Creates a message payload that configures the sense mode for digital input 1.")]
    public partial class CreateInput1TriggerModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the sense mode for digital input 1.
        /// </summary>
        [Description("The value that configures the sense mode for digital input 1.")]
        public TriggerMode Input1TriggerMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input1TriggerMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerMode GetPayload()
        {
            return Input1TriggerMode;
        }

        /// <summary>
        /// Creates a message that configures the sense mode for digital input 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input1TriggerMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input1TriggerMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the sense mode for digital input 1.
    /// </summary>
    [DisplayName("TimestampedInput1TriggerModePayload")]
    [Description("Creates a timestamped message payload that configures the sense mode for digital input 1.")]
    public partial class CreateTimestampedInput1TriggerModePayload : CreateInput1TriggerModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the sense mode for digital input 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input1TriggerMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input1TriggerMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the sense mode for digital input 2.
    /// </summary>
    [DisplayName("Input2TriggerModePayload")]
    [Description("Creates a message payload that configures the sense mode for digital input 2.")]
    public partial class CreateInput2TriggerModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the sense mode for digital input 2.
        /// </summary>
        [Description("The value that configures the sense mode for digital input 2.")]
        public TriggerMode Input2TriggerMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input2TriggerMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerMode GetPayload()
        {
            return Input2TriggerMode;
        }

        /// <summary>
        /// Creates a message that configures the sense mode for digital input 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input2TriggerMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input2TriggerMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the sense mode for digital input 2.
    /// </summary>
    [DisplayName("TimestampedInput2TriggerModePayload")]
    [Description("Creates a timestamped message payload that configures the sense mode for digital input 2.")]
    public partial class CreateTimestampedInput2TriggerModePayload : CreateInput2TriggerModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the sense mode for digital input 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input2TriggerMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input2TriggerMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the sense mode for digital input 3.
    /// </summary>
    [DisplayName("Input3TriggerModePayload")]
    [Description("Creates a message payload that configures the sense mode for digital input 3.")]
    public partial class CreateInput3TriggerModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the sense mode for digital input 3.
        /// </summary>
        [Description("The value that configures the sense mode for digital input 3.")]
        public TriggerMode Input3TriggerMode { get; set; }

        /// <summary>
        /// Creates a message payload for the Input3TriggerMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerMode GetPayload()
        {
            return Input3TriggerMode;
        }

        /// <summary>
        /// Creates a message that configures the sense mode for digital input 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Input3TriggerMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Input3TriggerMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the sense mode for digital input 3.
    /// </summary>
    [DisplayName("TimestampedInput3TriggerModePayload")]
    [Description("Creates a timestamped message payload that configures the sense mode for digital input 3.")]
    public partial class CreateTimestampedInput3TriggerModePayload : CreateInput3TriggerModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the sense mode for digital input 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Input3TriggerMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Input3TriggerMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the edge detection mode for the emergency external button.
    /// </summary>
    [DisplayName("EmergencyStopModePayload")]
    [Description("Creates a message payload that configures the edge detection mode for the emergency external button.")]
    public partial class CreateEmergencyStopModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the edge detection mode for the emergency external button.
        /// </summary>
        [Description("The value that configures the edge detection mode for the emergency external button.")]
        public TriggerMode EmergencyStopMode { get; set; }

        /// <summary>
        /// Creates a message payload for the EmergencyStopMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerMode GetPayload()
        {
            return EmergencyStopMode;
        }

        /// <summary>
        /// Creates a message that configures the edge detection mode for the emergency external button.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EmergencyStopMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EmergencyStopMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the edge detection mode for the emergency external button.
    /// </summary>
    [DisplayName("TimestampedEmergencyStopModePayload")]
    [Description("Creates a timestamped message payload that configures the edge detection mode for the emergency external button.")]
    public partial class CreateTimestampedEmergencyStopModePayload : CreateEmergencyStopModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the edge detection mode for the emergency external button.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EmergencyStopMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EmergencyStopMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [DisplayName("MotorStoppedPayload")]
    [Description("Creates a message payload that contains a bit mask specifying the motor that stopped the movement.")]
    public partial class CreateMotorStoppedPayload
    {
        /// <summary>
        /// Gets or sets the value that contains a bit mask specifying the motor that stopped the movement.
        /// </summary>
        [Description("The value that contains a bit mask specifying the motor that stopped the movement.")]
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
        /// Creates a message that contains a bit mask specifying the motor that stopped the movement.
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
    /// that contains a bit mask specifying the motor that stopped the movement.
    /// </summary>
    [DisplayName("TimestampedMotorStoppedPayload")]
    [Description("Creates a timestamped message payload that contains a bit mask specifying the motor that stopped the movement.")]
    public partial class CreateTimestampedMotorStoppedPayload : CreateMotorStoppedPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains a bit mask specifying the motor that stopped the movement.
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
    /// that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
    /// </summary>
    [DisplayName("MotorOvervoltageDetectionPayload")]
    [Description("Creates a message payload that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.")]
    public partial class CreateMotorOvervoltageDetectionPayload
    {
        /// <summary>
        /// Gets or sets the value that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
        /// </summary>
        [Description("The value that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.")]
        public StepperMotors MotorOvervoltageDetection { get; set; }

        /// <summary>
        /// Creates a message payload for the MotorOvervoltageDetection register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return MotorOvervoltageDetection;
        }

        /// <summary>
        /// Creates a message that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MotorOvervoltageDetection register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MotorOvervoltageDetection.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
    /// </summary>
    [DisplayName("TimestampedMotorOvervoltageDetectionPayload")]
    [Description("Creates a timestamped message payload that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.")]
    public partial class CreateTimestampedMotorOvervoltageDetectionPayload : CreateMotorOvervoltageDetectionPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains a bit mask specifying the motor where the overvoltage detection and protection mechanism occurred, which can happen when the there's a quick deceleration from a high velocity or when the motor stalls.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MotorOvervoltageDetection register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MotorOvervoltageDetection.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
    /// </summary>
    [DisplayName("MotorErrorDetectionPayload")]
    [Description("Creates a message payload that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.")]
    public partial class CreateMotorErrorDetectionPayload
    {
        /// <summary>
        /// Gets or sets the value that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
        /// </summary>
        [Description("The value that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.")]
        public StepperMotors MotorErrorDetection { get; set; }

        /// <summary>
        /// Creates a message payload for the MotorErrorDetection register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return MotorErrorDetection;
        }

        /// <summary>
        /// Creates a message that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MotorErrorDetection register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.MotorErrorDetection.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
    /// </summary>
    [DisplayName("TimestampedMotorErrorDetectionPayload")]
    [Description("Creates a timestamped message payload that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.")]
    public partial class CreateTimestampedMotorErrorDetectionPayload : CreateMotorErrorDetectionPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains a bit mask specifying the motor that triggered the error which can be happen in case of short-circuit or driver temperature above 165 degress celsius.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MotorErrorDetection register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.MotorErrorDetection.FromPayload(timestamp, messageType, GetPayload());
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
    /// that contains the state of the external emergency button.
    /// </summary>
    [DisplayName("EmergencyStopPayload")]
    [Description("Creates a message payload that contains the state of the external emergency button.")]
    public partial class CreateEmergencyStopPayload
    {
        /// <summary>
        /// Gets or sets the value that contains the state of the external emergency button.
        /// </summary>
        [Description("The value that contains the state of the external emergency button.")]
        public EmergencyStopState EmergencyStop { get; set; }

        /// <summary>
        /// Creates a message payload for the EmergencyStop register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EmergencyStopState GetPayload()
        {
            return EmergencyStop;
        }

        /// <summary>
        /// Creates a message that contains the state of the external emergency button.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EmergencyStop register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.EmergencyStop.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the state of the external emergency button.
    /// </summary>
    [DisplayName("TimestampedEmergencyStopPayload")]
    [Description("Creates a timestamped message payload that contains the state of the external emergency button.")]
    public partial class CreateTimestampedEmergencyStopPayload : CreateEmergencyStopPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the state of the external emergency button.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EmergencyStop register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.EmergencyStop.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor0StepsPayload")]
    [Description("Creates a message payload that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor0StepsPayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor0Steps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0Steps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0Steps;
        }

        /// <summary>
        /// Creates a message that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0Steps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0Steps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor0StepsPayload")]
    [Description("Creates a timestamped message payload that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor0StepsPayload : CreateMotor0StepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 0 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0Steps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0Steps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor1StepsPayload")]
    [Description("Creates a message payload that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor1StepsPayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor1Steps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1Steps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1Steps;
        }

        /// <summary>
        /// Creates a message that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1Steps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1Steps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor1StepsPayload")]
    [Description("Creates a timestamped message payload that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor1StepsPayload : CreateMotor1StepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 1 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1Steps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1Steps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor2StepsPayload")]
    [Description("Creates a message payload that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor2StepsPayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor2Steps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2Steps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2Steps;
        }

        /// <summary>
        /// Creates a message that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2Steps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2Steps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor2StepsPayload")]
    [Description("Creates a timestamped message payload that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor2StepsPayload : CreateMotor2StepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 2 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2Steps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2Steps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("Motor3StepsPayload")]
    [Description("Creates a message payload that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateMotor3StepsPayload
    {
        /// <summary>
        /// Gets or sets the value that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        [Description("The value that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
        public int Motor3Steps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3Steps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3Steps;
        }

        /// <summary>
        /// Creates a message that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3Steps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3Steps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
    /// </summary>
    [DisplayName("TimestampedMotor3StepsPayload")]
    [Description("Creates a timestamped message payload that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.")]
    public partial class CreateTimestampedMotor3StepsPayload : CreateMotor3StepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that moves motor 3 by the number of steps written in this register and set the direction according to the value's signal.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3Steps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3Steps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the accumulated steps of motor 0.
    /// </summary>
    [DisplayName("Motor0AccumulatedStepsPayload")]
    [Description("Creates a message payload that contains the accumulated steps of motor 0.")]
    public partial class CreateMotor0AccumulatedStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that contains the accumulated steps of motor 0.
        /// </summary>
        [Description("The value that contains the accumulated steps of motor 0.")]
        public int Motor0AccumulatedSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0AccumulatedSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0AccumulatedSteps;
        }

        /// <summary>
        /// Creates a message that contains the accumulated steps of motor 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0AccumulatedSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the accumulated steps of motor 0.
    /// </summary>
    [DisplayName("TimestampedMotor0AccumulatedStepsPayload")]
    [Description("Creates a timestamped message payload that contains the accumulated steps of motor 0.")]
    public partial class CreateTimestampedMotor0AccumulatedStepsPayload : CreateMotor0AccumulatedStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the accumulated steps of motor 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0AccumulatedSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the accumulated steps of motor 1.
    /// </summary>
    [DisplayName("Motor1AccumulatedStepsPayload")]
    [Description("Creates a message payload that contains the accumulated steps of motor 1.")]
    public partial class CreateMotor1AccumulatedStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that contains the accumulated steps of motor 1.
        /// </summary>
        [Description("The value that contains the accumulated steps of motor 1.")]
        public int Motor1AccumulatedSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1AccumulatedSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1AccumulatedSteps;
        }

        /// <summary>
        /// Creates a message that contains the accumulated steps of motor 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1AccumulatedSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the accumulated steps of motor 1.
    /// </summary>
    [DisplayName("TimestampedMotor1AccumulatedStepsPayload")]
    [Description("Creates a timestamped message payload that contains the accumulated steps of motor 1.")]
    public partial class CreateTimestampedMotor1AccumulatedStepsPayload : CreateMotor1AccumulatedStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the accumulated steps of motor 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1AccumulatedSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the accumulated steps of motor 2.
    /// </summary>
    [DisplayName("Motor2AccumulatedStepsPayload")]
    [Description("Creates a message payload that contains the accumulated steps of motor 2.")]
    public partial class CreateMotor2AccumulatedStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that contains the accumulated steps of motor 2.
        /// </summary>
        [Description("The value that contains the accumulated steps of motor 2.")]
        public int Motor2AccumulatedSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2AccumulatedSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2AccumulatedSteps;
        }

        /// <summary>
        /// Creates a message that contains the accumulated steps of motor 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2AccumulatedSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the accumulated steps of motor 2.
    /// </summary>
    [DisplayName("TimestampedMotor2AccumulatedStepsPayload")]
    [Description("Creates a timestamped message payload that contains the accumulated steps of motor 2.")]
    public partial class CreateTimestampedMotor2AccumulatedStepsPayload : CreateMotor2AccumulatedStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the accumulated steps of motor 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2AccumulatedSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that contains the accumulated steps of motor 3.
    /// </summary>
    [DisplayName("Motor3AccumulatedStepsPayload")]
    [Description("Creates a message payload that contains the accumulated steps of motor 3.")]
    public partial class CreateMotor3AccumulatedStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that contains the accumulated steps of motor 3.
        /// </summary>
        [Description("The value that contains the accumulated steps of motor 3.")]
        public int Motor3AccumulatedSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3AccumulatedSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3AccumulatedSteps;
        }

        /// <summary>
        /// Creates a message that contains the accumulated steps of motor 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3AccumulatedSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that contains the accumulated steps of motor 3.
    /// </summary>
    [DisplayName("TimestampedMotor3AccumulatedStepsPayload")]
    [Description("Creates a timestamped message payload that contains the accumulated steps of motor 3.")]
    public partial class CreateTimestampedMotor3AccumulatedStepsPayload : CreateMotor3AccumulatedStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that contains the accumulated steps of motor 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3AccumulatedSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3AccumulatedSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor0MaximumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor0MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor0MaximumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MaximumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MaximumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor0MaximumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor0MaximumStepsIntegrationPayload : CreateMotor0MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MaximumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor1MaximumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor1MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor1MaximumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MaximumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MaximumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor1MaximumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor1MaximumStepsIntegrationPayload : CreateMotor1MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MaximumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor2MaximumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor2MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor2MaximumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MaximumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MaximumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor2MaximumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor2MaximumStepsIntegrationPayload : CreateMotor2MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MaximumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor3MaximumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor3MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor3MaximumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MaximumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MaximumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor3MaximumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor3MaximumStepsIntegrationPayload : CreateMotor3MaximumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the positive movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MaximumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MaximumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor0MinimumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor0MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor0MinimumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0MinimumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0MinimumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MinimumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor0MinimumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor0MinimumStepsIntegrationPayload : CreateMotor0MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 0. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0MinimumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor1MinimumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor1MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor1MinimumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1MinimumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1MinimumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MinimumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor1MinimumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor1MinimumStepsIntegrationPayload : CreateMotor1MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 1. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1MinimumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor2MinimumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor2MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor2MinimumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2MinimumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2MinimumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MinimumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor2MinimumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor2MinimumStepsIntegrationPayload : CreateMotor2MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 2. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2MinimumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("Motor3MinimumStepsIntegrationPayload")]
    [Description("Creates a message payload that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateMotor3MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        [Description("The value that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
        public int Motor3MinimumStepsIntegration { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3MinimumStepsIntegration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3MinimumStepsIntegration;
        }

        /// <summary>
        /// Creates a message that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MinimumStepsIntegration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
    /// </summary>
    [DisplayName("TimestampedMotor3MinimumStepsIntegrationPayload")]
    [Description("Creates a timestamped message payload that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.")]
    public partial class CreateTimestampedMotor3MinimumStepsIntegrationPayload : CreateMotor3MinimumStepsIntegrationPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the limit of the accumulated steps for the negative movement of motor 3. The device will not let the motor move further than this value. A value equal to 0 disables the feature.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3MinimumStepsIntegration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3MinimumStepsIntegration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor0ImmediateStepsPayload")]
    [Description("Creates a message payload that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateMotor0ImmediateStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.")]
        public int Motor0ImmediateSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor0ImmediateSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor0ImmediateSteps;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor0ImmediateSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor0ImmediateSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor0ImmediateStepsPayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor0ImmediateStepsPayload : CreateMotor0ImmediateStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 0 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor0ImmediateSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor0ImmediateSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor1ImmediateStepsPayload")]
    [Description("Creates a message payload that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateMotor1ImmediateStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.")]
        public int Motor1ImmediateSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor1ImmediateSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor1ImmediateSteps;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor1ImmediateSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor1ImmediateSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor1ImmediateStepsPayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor1ImmediateStepsPayload : CreateMotor1ImmediateStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 1 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor1ImmediateSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor1ImmediateSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor2ImmediateStepsPayload")]
    [Description("Creates a message payload that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateMotor2ImmediateStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.")]
        public int Motor2ImmediateSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor2ImmediateSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor2ImmediateSteps;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor2ImmediateSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor2ImmediateSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor2ImmediateStepsPayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor2ImmediateStepsPayload : CreateMotor2ImmediateStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 2 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor2ImmediateSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor2ImmediateSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("Motor3ImmediateStepsPayload")]
    [Description("Creates a message payload that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateMotor3ImmediateStepsPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        [Description("The value that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.")]
        public int Motor3ImmediateSteps { get; set; }

        /// <summary>
        /// Creates a message payload for the Motor3ImmediateSteps register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public int GetPayload()
        {
            return Motor3ImmediateSteps;
        }

        /// <summary>
        /// Creates a message that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Motor3ImmediateSteps register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.Motor3ImmediateSteps.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
    /// </summary>
    [DisplayName("TimestampedMotor3ImmediateStepsPayload")]
    [Description("Creates a timestamped message payload that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.")]
    public partial class CreateTimestampedMotor3ImmediateStepsPayload : CreateMotor3ImmediateStepsPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the movement of motor 3 with the step interval defined by this register. The sign of the value specifies the direction.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Motor3ImmediateSteps register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.Motor3ImmediateSteps.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that stops the motors immediately.
    /// </summary>
    [DisplayName("StopMotorSuddenlyPayload")]
    [Description("Creates a message payload that stops the motors immediately.")]
    public partial class CreateStopMotorSuddenlyPayload
    {
        /// <summary>
        /// Gets or sets the value that stops the motors immediately.
        /// </summary>
        [Description("The value that stops the motors immediately.")]
        public StepperMotors StopMotorSuddenly { get; set; }

        /// <summary>
        /// Creates a message payload for the StopMotorSuddenly register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return StopMotorSuddenly;
        }

        /// <summary>
        /// Creates a message that stops the motors immediately.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the StopMotorSuddenly register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.StopMotorSuddenly.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that stops the motors immediately.
    /// </summary>
    [DisplayName("TimestampedStopMotorSuddenlyPayload")]
    [Description("Creates a timestamped message payload that stops the motors immediately.")]
    public partial class CreateTimestampedStopMotorSuddenlyPayload : CreateStopMotorSuddenlyPayload
    {
        /// <summary>
        /// Creates a timestamped message that stops the motors immediately.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the StopMotorSuddenly register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.StopMotorSuddenly.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that decelerate the motors until they stop according to configured intervals.
    /// </summary>
    [DisplayName("StopMotorSmoothlyPayload")]
    [Description("Creates a message payload that decelerate the motors until they stop according to configured intervals.")]
    public partial class CreateStopMotorSmoothlyPayload
    {
        /// <summary>
        /// Gets or sets the value that decelerate the motors until they stop according to configured intervals.
        /// </summary>
        [Description("The value that decelerate the motors until they stop according to configured intervals.")]
        public StepperMotors StopMotorSmoothly { get; set; }

        /// <summary>
        /// Creates a message payload for the StopMotorSmoothly register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return StopMotorSmoothly;
        }

        /// <summary>
        /// Creates a message that decelerate the motors until they stop according to configured intervals.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the StopMotorSmoothly register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.StopMotorSmoothly.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that decelerate the motors until they stop according to configured intervals.
    /// </summary>
    [DisplayName("TimestampedStopMotorSmoothlyPayload")]
    [Description("Creates a timestamped message payload that decelerate the motors until they stop according to configured intervals.")]
    public partial class CreateTimestampedStopMotorSmoothlyPayload : CreateStopMotorSmoothlyPayload
    {
        /// <summary>
        /// Creates a timestamped message that decelerate the motors until they stop according to configured intervals.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the StopMotorSmoothly register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.StopMotorSmoothly.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that resets the internal motor driver which also clears any eventual error.
    /// </summary>
    [DisplayName("ResetMotorPayload")]
    [Description("Creates a message payload that resets the internal motor driver which also clears any eventual error.")]
    public partial class CreateResetMotorPayload
    {
        /// <summary>
        /// Gets or sets the value that resets the internal motor driver which also clears any eventual error.
        /// </summary>
        [Description("The value that resets the internal motor driver which also clears any eventual error.")]
        public StepperMotors ResetMotor { get; set; }

        /// <summary>
        /// Creates a message payload for the ResetMotor register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StepperMotors GetPayload()
        {
            return ResetMotor;
        }

        /// <summary>
        /// Creates a message that resets the internal motor driver which also clears any eventual error.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ResetMotor register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.ResetMotor.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that resets the internal motor driver which also clears any eventual error.
    /// </summary>
    [DisplayName("TimestampedResetMotorPayload")]
    [Description("Creates a timestamped message payload that resets the internal motor driver which also clears any eventual error.")]
    public partial class CreateTimestampedResetMotorPayload : CreateResetMotorPayload
    {
        /// <summary>
        /// Creates a timestamped message that resets the internal motor driver which also clears any eventual error.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ResetMotor register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.ResetMotor.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that resets the encoder.
    /// </summary>
    [DisplayName("ResetEncoderPayload")]
    [Description("Creates a message payload that resets the encoder.")]
    public partial class CreateResetEncoderPayload
    {
        /// <summary>
        /// Gets or sets the value that resets the encoder.
        /// </summary>
        [Description("The value that resets the encoder.")]
        public QuadratureEncoders ResetEncoder { get; set; }

        /// <summary>
        /// Creates a message payload for the ResetEncoder register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public QuadratureEncoders GetPayload()
        {
            return ResetEncoder;
        }

        /// <summary>
        /// Creates a message that resets the encoder.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ResetEncoder register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.StepperDriver.ResetEncoder.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that resets the encoder.
    /// </summary>
    [DisplayName("TimestampedResetEncoderPayload")]
    [Description("Creates a timestamped message payload that resets the encoder.")]
    public partial class CreateTimestampedResetEncoderPayload : CreateResetEncoderPayload
    {
        /// <summary>
        /// Creates a timestamped message that resets the encoder.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ResetEncoder register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.StepperDriver.ResetEncoder.FromPayload(timestamp, messageType, GetPayload());
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
    /// Specifies the type of reading made from the quadrature encoders.
    /// </summary>
    public enum EncoderModeConfig : byte
    {
        Position = 0,
        Displacement = 1
    }

    /// <summary>
    /// Specifies the rate of the events from the quadrature encoders.
    /// </summary>
    public enum EncoderRateConfig : byte
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
        NoReduction = 0,
        ReductionTo50Percent = 1,
        ReductionTo25Percent = 2,
        ReductionTo12Percent = 3
    }

    /// <summary>
    /// Specifies the inputs operation mode.
    /// </summary>
    public enum InputOperationMode : byte
    {
        EventOnly = 0,
        EventAndStopMotor0 = 1,
        EventAndStopMotor1 = 2,
        EventAndStopMotor2 = 3,
        EventAndStopMotor3 = 4
    }

    /// <summary>
    /// Specifies the input trigger mode.
    /// </summary>
    public enum TriggerMode : byte
    {
        RisingEdge = 0,
        FallingEdge = 1
    }

    /// <summary>
    /// Specifies the state of the external emergency button.
    /// </summary>
    public enum EmergencyStopState : byte
    {
        NoEmergency = 0,
        EmergencyDetected = 1
    }
}
