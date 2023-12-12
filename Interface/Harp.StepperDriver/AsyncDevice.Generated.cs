using Bonsai.Harp;
using System.Threading;
using System.Threading.Tasks;

namespace Harp.StepperDriver
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with StepperDriver devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a StepperDriver device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with StepperDriver devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadEnableMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableMotors.Address), cancellationToken);
            return EnableMotors.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedEnableMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableMotors.Address), cancellationToken);
            return EnableMotors.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableMotors register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableMotorsAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = EnableMotors.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadDisableMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableMotors.Address), cancellationToken);
            return DisableMotors.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedDisableMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableMotors.Address), cancellationToken);
            return DisableMotors.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableMotors register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableMotorsAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = DisableMotors.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<QuadratureEncoders> ReadEnableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address), cancellationToken);
            return EnableEncoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<QuadratureEncoders>> ReadTimestampedEnableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address), cancellationToken);
            return EnableEncoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableEncoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableEncodersAsync(QuadratureEncoders value, CancellationToken cancellationToken = default)
        {
            var request = EnableEncoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<QuadratureEncoders> ReadDisableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableEncoders.Address), cancellationToken);
            return DisableEncoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<QuadratureEncoders>> ReadTimestampedDisableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableEncoders.Address), cancellationToken);
            return DisableEncoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableEncoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableEncodersAsync(QuadratureEncoders value, CancellationToken cancellationToken = default)
        {
            var request = DisableEncoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadEnableInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableInputs.Address), cancellationToken);
            return EnableInputs.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedEnableInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableInputs.Address), cancellationToken);
            return EnableInputs.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableInputs register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableInputsAsync(DigitalInputs value, CancellationToken cancellationToken = default)
        {
            var request = EnableInputs.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDisableInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableInputs.Address), cancellationToken);
            return DisableInputs.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDisableInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableInputs.Address), cancellationToken);
            return DisableInputs.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableInputs register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableInputsAsync(DigitalInputs value, CancellationToken cancellationToken = default)
        {
            var request = DisableInputs.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MotorOperationMode> ReadMotor0OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0OperationMode.Address), cancellationToken);
            return Motor0OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MotorOperationMode>> ReadTimestampedMotor0OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0OperationMode.Address), cancellationToken);
            return Motor0OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0OperationModeAsync(MotorOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Motor0OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MotorOperationMode> ReadMotor1OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1OperationMode.Address), cancellationToken);
            return Motor1OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MotorOperationMode>> ReadTimestampedMotor1OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1OperationMode.Address), cancellationToken);
            return Motor1OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1OperationModeAsync(MotorOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Motor1OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MotorOperationMode> ReadMotor2OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2OperationMode.Address), cancellationToken);
            return Motor2OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MotorOperationMode>> ReadTimestampedMotor2OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2OperationMode.Address), cancellationToken);
            return Motor2OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2OperationModeAsync(MotorOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Motor2OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MotorOperationMode> ReadMotor3OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3OperationMode.Address), cancellationToken);
            return Motor3OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MotorOperationMode>> ReadTimestampedMotor3OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3OperationMode.Address), cancellationToken);
            return Motor3OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3OperationModeAsync(MotorOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Motor3OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MicrostepResolution> ReadMotor0MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0MicrostepResolution.Address), cancellationToken);
            return Motor0MicrostepResolution.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MicrostepResolution>> ReadTimestampedMotor0MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0MicrostepResolution.Address), cancellationToken);
            return Motor0MicrostepResolution.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MicrostepResolution register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MicrostepResolutionAsync(MicrostepResolution value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MicrostepResolution.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MicrostepResolution> ReadMotor1MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1MicrostepResolution.Address), cancellationToken);
            return Motor1MicrostepResolution.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MicrostepResolution>> ReadTimestampedMotor1MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1MicrostepResolution.Address), cancellationToken);
            return Motor1MicrostepResolution.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MicrostepResolution register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MicrostepResolutionAsync(MicrostepResolution value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MicrostepResolution.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MicrostepResolution> ReadMotor2MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2MicrostepResolution.Address), cancellationToken);
            return Motor2MicrostepResolution.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MicrostepResolution>> ReadTimestampedMotor2MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2MicrostepResolution.Address), cancellationToken);
            return Motor2MicrostepResolution.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MicrostepResolution register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MicrostepResolutionAsync(MicrostepResolution value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MicrostepResolution.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MicrostepResolution> ReadMotor3MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3MicrostepResolution.Address), cancellationToken);
            return Motor3MicrostepResolution.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MicrostepResolution register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MicrostepResolution>> ReadTimestampedMotor3MicrostepResolutionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3MicrostepResolution.Address), cancellationToken);
            return Motor3MicrostepResolution.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MicrostepResolution register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MicrostepResolutionAsync(MicrostepResolution value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MicrostepResolution.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor0MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor0MaximumCurrentRms.Address), cancellationToken);
            return Motor0MaximumCurrentRms.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor0MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor0MaximumCurrentRms.Address), cancellationToken);
            return Motor0MaximumCurrentRms.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MaximumCurrentRms register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MaximumCurrentRmsAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MaximumCurrentRms.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor1MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor1MaximumCurrentRms.Address), cancellationToken);
            return Motor1MaximumCurrentRms.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor1MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor1MaximumCurrentRms.Address), cancellationToken);
            return Motor1MaximumCurrentRms.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MaximumCurrentRms register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MaximumCurrentRmsAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MaximumCurrentRms.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor2MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor2MaximumCurrentRms.Address), cancellationToken);
            return Motor2MaximumCurrentRms.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor2MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor2MaximumCurrentRms.Address), cancellationToken);
            return Motor2MaximumCurrentRms.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MaximumCurrentRms register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MaximumCurrentRmsAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MaximumCurrentRms.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor3MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor3MaximumCurrentRms.Address), cancellationToken);
            return Motor3MaximumCurrentRms.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MaximumCurrentRms register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor3MaximumCurrentRmsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor3MaximumCurrentRms.Address), cancellationToken);
            return Motor3MaximumCurrentRms.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MaximumCurrentRms register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MaximumCurrentRmsAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MaximumCurrentRms.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HoldCurrentReduction> ReadMotor0HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0HoldCurrentReduction.Address), cancellationToken);
            return Motor0HoldCurrentReduction.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HoldCurrentReduction>> ReadTimestampedMotor0HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor0HoldCurrentReduction.Address), cancellationToken);
            return Motor0HoldCurrentReduction.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0HoldCurrentReduction register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0HoldCurrentReductionAsync(HoldCurrentReduction value, CancellationToken cancellationToken = default)
        {
            var request = Motor0HoldCurrentReduction.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HoldCurrentReduction> ReadMotor1HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1HoldCurrentReduction.Address), cancellationToken);
            return Motor1HoldCurrentReduction.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HoldCurrentReduction>> ReadTimestampedMotor1HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor1HoldCurrentReduction.Address), cancellationToken);
            return Motor1HoldCurrentReduction.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1HoldCurrentReduction register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1HoldCurrentReductionAsync(HoldCurrentReduction value, CancellationToken cancellationToken = default)
        {
            var request = Motor1HoldCurrentReduction.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HoldCurrentReduction> ReadMotor2HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2HoldCurrentReduction.Address), cancellationToken);
            return Motor2HoldCurrentReduction.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HoldCurrentReduction>> ReadTimestampedMotor2HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor2HoldCurrentReduction.Address), cancellationToken);
            return Motor2HoldCurrentReduction.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2HoldCurrentReduction register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2HoldCurrentReductionAsync(HoldCurrentReduction value, CancellationToken cancellationToken = default)
        {
            var request = Motor2HoldCurrentReduction.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HoldCurrentReduction> ReadMotor3HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3HoldCurrentReduction.Address), cancellationToken);
            return Motor3HoldCurrentReduction.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3HoldCurrentReduction register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HoldCurrentReduction>> ReadTimestampedMotor3HoldCurrentReductionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Motor3HoldCurrentReduction.Address), cancellationToken);
            return Motor3HoldCurrentReduction.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3HoldCurrentReduction register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3HoldCurrentReductionAsync(HoldCurrentReduction value, CancellationToken cancellationToken = default)
        {
            var request = Motor3HoldCurrentReduction.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor0NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0NominalStepInterval.Address), cancellationToken);
            return Motor0NominalStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor0NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0NominalStepInterval.Address), cancellationToken);
            return Motor0NominalStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0NominalStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0NominalStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor0NominalStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor1NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1NominalStepInterval.Address), cancellationToken);
            return Motor1NominalStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor1NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1NominalStepInterval.Address), cancellationToken);
            return Motor1NominalStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1NominalStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1NominalStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor1NominalStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor2NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2NominalStepInterval.Address), cancellationToken);
            return Motor2NominalStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor2NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2NominalStepInterval.Address), cancellationToken);
            return Motor2NominalStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2NominalStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2NominalStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor2NominalStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor3NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3NominalStepInterval.Address), cancellationToken);
            return Motor3NominalStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3NominalStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor3NominalStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3NominalStepInterval.Address), cancellationToken);
            return Motor3NominalStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3NominalStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3NominalStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor3NominalStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor0MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0MaximumStepInterval.Address), cancellationToken);
            return Motor0MaximumStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor0MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0MaximumStepInterval.Address), cancellationToken);
            return Motor0MaximumStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MaximumStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MaximumStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MaximumStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor1MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1MaximumStepInterval.Address), cancellationToken);
            return Motor1MaximumStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor1MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1MaximumStepInterval.Address), cancellationToken);
            return Motor1MaximumStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MaximumStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MaximumStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MaximumStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor2MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2MaximumStepInterval.Address), cancellationToken);
            return Motor2MaximumStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor2MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2MaximumStepInterval.Address), cancellationToken);
            return Motor2MaximumStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MaximumStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MaximumStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MaximumStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor3MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3MaximumStepInterval.Address), cancellationToken);
            return Motor3MaximumStepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MaximumStepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor3MaximumStepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3MaximumStepInterval.Address), cancellationToken);
            return Motor3MaximumStepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MaximumStepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MaximumStepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MaximumStepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor0StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0StepAccelerationInterval.Address), cancellationToken);
            return Motor0StepAccelerationInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor0StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0StepAccelerationInterval.Address), cancellationToken);
            return Motor0StepAccelerationInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0StepAccelerationInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0StepAccelerationIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor0StepAccelerationInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor1StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1StepAccelerationInterval.Address), cancellationToken);
            return Motor1StepAccelerationInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor1StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1StepAccelerationInterval.Address), cancellationToken);
            return Motor1StepAccelerationInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1StepAccelerationInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1StepAccelerationIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor1StepAccelerationInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor2StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2StepAccelerationInterval.Address), cancellationToken);
            return Motor2StepAccelerationInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor2StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2StepAccelerationInterval.Address), cancellationToken);
            return Motor2StepAccelerationInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2StepAccelerationInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2StepAccelerationIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor2StepAccelerationInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor3StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3StepAccelerationInterval.Address), cancellationToken);
            return Motor3StepAccelerationInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3StepAccelerationInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor3StepAccelerationIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3StepAccelerationInterval.Address), cancellationToken);
            return Motor3StepAccelerationInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3StepAccelerationInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3StepAccelerationIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor3StepAccelerationInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderModeConfig> ReadEncoderModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderMode.Address), cancellationToken);
            return EncoderMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderModeConfig>> ReadTimestampedEncoderModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderMode.Address), cancellationToken);
            return EncoderMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderModeAsync(EncoderModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = EncoderMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderRateConfig> ReadEncoderRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderRate.Address), cancellationToken);
            return EncoderRate.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderRateConfig>> ReadTimestampedEncoderRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderRate.Address), cancellationToken);
            return EncoderRate.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderRate register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderRateAsync(EncoderRateConfig value, CancellationToken cancellationToken = default)
        {
            var request = EncoderRate.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input0OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOperationMode> ReadInput0OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0OperationMode.Address), cancellationToken);
            return Input0OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input0OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOperationMode>> ReadTimestampedInput0OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0OperationMode.Address), cancellationToken);
            return Input0OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input0OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput0OperationModeAsync(InputOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Input0OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input1OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOperationMode> ReadInput1OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1OperationMode.Address), cancellationToken);
            return Input1OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input1OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOperationMode>> ReadTimestampedInput1OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1OperationMode.Address), cancellationToken);
            return Input1OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input1OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput1OperationModeAsync(InputOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Input1OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input2OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOperationMode> ReadInput2OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2OperationMode.Address), cancellationToken);
            return Input2OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input2OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOperationMode>> ReadTimestampedInput2OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2OperationMode.Address), cancellationToken);
            return Input2OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input2OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput2OperationModeAsync(InputOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Input2OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input3OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOperationMode> ReadInput3OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3OperationMode.Address), cancellationToken);
            return Input3OperationMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input3OperationMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOperationMode>> ReadTimestampedInput3OperationModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3OperationMode.Address), cancellationToken);
            return Input3OperationMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input3OperationMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput3OperationModeAsync(InputOperationMode value, CancellationToken cancellationToken = default)
        {
            var request = Input3OperationMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input0TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerMode> ReadInput0TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0TriggerMode.Address), cancellationToken);
            return Input0TriggerMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input0TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerMode>> ReadTimestampedInput0TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0TriggerMode.Address), cancellationToken);
            return Input0TriggerMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input0TriggerMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput0TriggerModeAsync(TriggerMode value, CancellationToken cancellationToken = default)
        {
            var request = Input0TriggerMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input1TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerMode> ReadInput1TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1TriggerMode.Address), cancellationToken);
            return Input1TriggerMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input1TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerMode>> ReadTimestampedInput1TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1TriggerMode.Address), cancellationToken);
            return Input1TriggerMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input1TriggerMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput1TriggerModeAsync(TriggerMode value, CancellationToken cancellationToken = default)
        {
            var request = Input1TriggerMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input2TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerMode> ReadInput2TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2TriggerMode.Address), cancellationToken);
            return Input2TriggerMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input2TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerMode>> ReadTimestampedInput2TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2TriggerMode.Address), cancellationToken);
            return Input2TriggerMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input2TriggerMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput2TriggerModeAsync(TriggerMode value, CancellationToken cancellationToken = default)
        {
            var request = Input2TriggerMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input3TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerMode> ReadInput3TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3TriggerMode.Address), cancellationToken);
            return Input3TriggerMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input3TriggerMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerMode>> ReadTimestampedInput3TriggerModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3TriggerMode.Address), cancellationToken);
            return Input3TriggerMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input3TriggerMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput3TriggerModeAsync(TriggerMode value, CancellationToken cancellationToken = default)
        {
            var request = Input3TriggerMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DeviceEnableMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableMode> ReadDeviceEnableModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DeviceEnableMode.Address), cancellationToken);
            return DeviceEnableMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DeviceEnableMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableMode>> ReadTimestampedDeviceEnableModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DeviceEnableMode.Address), cancellationToken);
            return DeviceEnableMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DeviceEnableMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDeviceEnableModeAsync(EnableMode value, CancellationToken cancellationToken = default)
        {
            var request = DeviceEnableMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MotorStopped register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadMotorStoppedAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorStopped.Address), cancellationToken);
            return MotorStopped.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MotorStopped register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedMotorStoppedAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorStopped.Address), cancellationToken);
            return MotorStopped.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MotorErrorDetection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadMotorErrorDetectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorErrorDetection.Address), cancellationToken);
            return MotorErrorDetection.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MotorErrorDetection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedMotorErrorDetectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorErrorDetection.Address), cancellationToken);
            return MotorErrorDetection.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Encoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncodersPayload> ReadEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(Encoders.Address), cancellationToken);
            return Encoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Encoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncodersPayload>> ReadTimestampedEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(Encoders.Address), cancellationToken);
            return Encoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DeviceState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DeviceStateMode> ReadDeviceStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DeviceState.Address), cancellationToken);
            return DeviceState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DeviceState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DeviceStateMode>> ReadTimestampedDeviceStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DeviceState.Address), cancellationToken);
            return DeviceState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0Steps.Address), cancellationToken);
            return Motor0Steps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0Steps.Address), cancellationToken);
            return Motor0Steps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0Steps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0StepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0Steps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1Steps.Address), cancellationToken);
            return Motor1Steps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1Steps.Address), cancellationToken);
            return Motor1Steps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1Steps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1StepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1Steps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2Steps.Address), cancellationToken);
            return Motor2Steps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2Steps.Address), cancellationToken);
            return Motor2Steps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2Steps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2StepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2Steps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3Steps.Address), cancellationToken);
            return Motor3Steps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3Steps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3StepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3Steps.Address), cancellationToken);
            return Motor3Steps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3Steps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3StepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3Steps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0AccumulatedSteps.Address), cancellationToken);
            return Motor0AccumulatedSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0AccumulatedSteps.Address), cancellationToken);
            return Motor0AccumulatedSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0AccumulatedSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0AccumulatedStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0AccumulatedSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1AccumulatedSteps.Address), cancellationToken);
            return Motor1AccumulatedSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1AccumulatedSteps.Address), cancellationToken);
            return Motor1AccumulatedSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1AccumulatedSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1AccumulatedStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1AccumulatedSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2AccumulatedSteps.Address), cancellationToken);
            return Motor2AccumulatedSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2AccumulatedSteps.Address), cancellationToken);
            return Motor2AccumulatedSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2AccumulatedSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2AccumulatedStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2AccumulatedSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3AccumulatedSteps.Address), cancellationToken);
            return Motor3AccumulatedSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3AccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3AccumulatedSteps.Address), cancellationToken);
            return Motor3AccumulatedSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3AccumulatedSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3AccumulatedStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3AccumulatedSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MaximumStepsIntegration.Address), cancellationToken);
            return Motor0MaximumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MaximumStepsIntegration.Address), cancellationToken);
            return Motor0MaximumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MaximumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MaximumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MaximumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MaximumStepsIntegration.Address), cancellationToken);
            return Motor1MaximumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MaximumStepsIntegration.Address), cancellationToken);
            return Motor1MaximumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MaximumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MaximumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MaximumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MaximumStepsIntegration.Address), cancellationToken);
            return Motor2MaximumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MaximumStepsIntegration.Address), cancellationToken);
            return Motor2MaximumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MaximumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MaximumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MaximumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MaximumStepsIntegration.Address), cancellationToken);
            return Motor3MaximumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MaximumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MaximumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MaximumStepsIntegration.Address), cancellationToken);
            return Motor3MaximumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MaximumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MaximumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MaximumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MinimumStepsIntegration.Address), cancellationToken);
            return Motor0MinimumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MinimumStepsIntegration.Address), cancellationToken);
            return Motor0MinimumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MinimumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MinimumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MinimumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MinimumStepsIntegration.Address), cancellationToken);
            return Motor1MinimumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MinimumStepsIntegration.Address), cancellationToken);
            return Motor1MinimumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MinimumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MinimumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MinimumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MinimumStepsIntegration.Address), cancellationToken);
            return Motor2MinimumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MinimumStepsIntegration.Address), cancellationToken);
            return Motor2MinimumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MinimumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MinimumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MinimumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MinimumStepsIntegration.Address), cancellationToken);
            return Motor3MinimumStepsIntegration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MinimumStepsIntegration register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MinimumStepsIntegrationAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MinimumStepsIntegration.Address), cancellationToken);
            return Motor3MinimumStepsIntegration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MinimumStepsIntegration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MinimumStepsIntegrationAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MinimumStepsIntegration.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0ImmediateSteps.Address), cancellationToken);
            return Motor0ImmediateSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0ImmediateSteps.Address), cancellationToken);
            return Motor0ImmediateSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0ImmediateSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0ImmediateStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0ImmediateSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1ImmediateSteps.Address), cancellationToken);
            return Motor1ImmediateSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1ImmediateSteps.Address), cancellationToken);
            return Motor1ImmediateSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1ImmediateSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1ImmediateStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1ImmediateSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2ImmediateSteps.Address), cancellationToken);
            return Motor2ImmediateSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2ImmediateSteps.Address), cancellationToken);
            return Motor2ImmediateSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2ImmediateSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2ImmediateStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2ImmediateSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3ImmediateSteps.Address), cancellationToken);
            return Motor3ImmediateSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3ImmediateSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3ImmediateStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3ImmediateSteps.Address), cancellationToken);
            return Motor3ImmediateSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3ImmediateSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3ImmediateStepsAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3ImmediateSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StopMotorSuddenly register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadStopMotorSuddenlyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopMotorSuddenly.Address), cancellationToken);
            return StopMotorSuddenly.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StopMotorSuddenly register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedStopMotorSuddenlyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopMotorSuddenly.Address), cancellationToken);
            return StopMotorSuddenly.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StopMotorSuddenly register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStopMotorSuddenlyAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = StopMotorSuddenly.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ResetEncoder register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<QuadratureEncoders> ReadResetEncoderAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ResetEncoder.Address), cancellationToken);
            return ResetEncoder.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ResetEncoder register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<QuadratureEncoders>> ReadTimestampedResetEncoderAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ResetEncoder.Address), cancellationToken);
            return ResetEncoder.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ResetEncoder register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteResetEncoderAsync(QuadratureEncoders value, CancellationToken cancellationToken = default)
        {
            var request = ResetEncoder.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }
    }
}
