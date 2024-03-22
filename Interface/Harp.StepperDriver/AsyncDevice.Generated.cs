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
        /// Asynchronously reads the contents of the EnableDriver register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadEnableDriverAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableDriver.Address), cancellationToken);
            return EnableDriver.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableDriver register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedEnableDriverAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableDriver.Address), cancellationToken);
            return EnableDriver.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableDriver register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableDriverAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = EnableDriver.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableDriver register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadDisableDriverAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableDriver.Address), cancellationToken);
            return DisableDriver.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableDriver register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedDisableDriverAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableDriver.Address), cancellationToken);
            return DisableDriver.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableDriver register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableDriverAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = DisableDriver.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the EnableDigitalInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadEnableDigitalInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableDigitalInputs.Address), cancellationToken);
            return EnableDigitalInputs.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableDigitalInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedEnableDigitalInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableDigitalInputs.Address), cancellationToken);
            return EnableDigitalInputs.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableDigitalInputs register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableDigitalInputsAsync(DigitalInputs value, CancellationToken cancellationToken = default)
        {
            var request = EnableDigitalInputs.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableDigitalInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDisableDigitalInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableDigitalInputs.Address), cancellationToken);
            return DisableDigitalInputs.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableDigitalInputs register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDisableDigitalInputsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableDigitalInputs.Address), cancellationToken);
            return DisableDigitalInputs.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableDigitalInputs register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableDigitalInputsAsync(DigitalInputs value, CancellationToken cancellationToken = default)
        {
            var request = DisableDigitalInputs.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the Motor0MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor0MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor0MaximumRunCurrent.Address), cancellationToken);
            return Motor0MaximumRunCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor0MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor0MaximumRunCurrent.Address), cancellationToken);
            return Motor0MaximumRunCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MaximumRunCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MaximumRunCurrentAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MaximumRunCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor1MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor1MaximumRunCurrent.Address), cancellationToken);
            return Motor1MaximumRunCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor1MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor1MaximumRunCurrent.Address), cancellationToken);
            return Motor1MaximumRunCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MaximumRunCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MaximumRunCurrentAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MaximumRunCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor2MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor2MaximumRunCurrent.Address), cancellationToken);
            return Motor2MaximumRunCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor2MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor2MaximumRunCurrent.Address), cancellationToken);
            return Motor2MaximumRunCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MaximumRunCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MaximumRunCurrentAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MaximumRunCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadMotor3MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor3MaximumRunCurrent.Address), cancellationToken);
            return Motor3MaximumRunCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MaximumRunCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedMotor3MaximumRunCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Motor3MaximumRunCurrent.Address), cancellationToken);
            return Motor3MaximumRunCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MaximumRunCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MaximumRunCurrentAsync(float value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MaximumRunCurrent.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the Motor0StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor0StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0StepInterval.Address), cancellationToken);
            return Motor0StepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor0StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor0StepInterval.Address), cancellationToken);
            return Motor0StepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0StepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0StepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor0StepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor1StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1StepInterval.Address), cancellationToken);
            return Motor1StepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor1StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor1StepInterval.Address), cancellationToken);
            return Motor1StepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1StepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1StepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor1StepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor2StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2StepInterval.Address), cancellationToken);
            return Motor2StepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor2StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor2StepInterval.Address), cancellationToken);
            return Motor2StepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2StepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2StepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor2StepInterval.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMotor3StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3StepInterval.Address), cancellationToken);
            return Motor3StepInterval.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3StepInterval register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMotor3StepIntervalAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Motor3StepInterval.Address), cancellationToken);
            return Motor3StepInterval.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3StepInterval register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3StepIntervalAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Motor3StepInterval.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the EncoderSamplingRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderSamplingRateConfig> ReadEncoderSamplingRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingRate.Address), cancellationToken);
            return EncoderSamplingRate.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderSamplingRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderSamplingRateConfig>> ReadTimestampedEncoderSamplingRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingRate.Address), cancellationToken);
            return EncoderSamplingRate.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderSamplingRate register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderSamplingRateAsync(EncoderSamplingRateConfig value, CancellationToken cancellationToken = default)
        {
            var request = EncoderSamplingRate.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input0OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOpModeConfig> ReadInput0OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0OpMode.Address), cancellationToken);
            return Input0OpMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input0OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOpModeConfig>> ReadTimestampedInput0OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input0OpMode.Address), cancellationToken);
            return Input0OpMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input0OpMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput0OpModeAsync(InputOpModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = Input0OpMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input1OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOpModeConfig> ReadInput1OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1OpMode.Address), cancellationToken);
            return Input1OpMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input1OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOpModeConfig>> ReadTimestampedInput1OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input1OpMode.Address), cancellationToken);
            return Input1OpMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input1OpMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput1OpModeAsync(InputOpModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = Input1OpMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input2OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOpModeConfig> ReadInput2OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2OpMode.Address), cancellationToken);
            return Input2OpMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input2OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOpModeConfig>> ReadTimestampedInput2OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input2OpMode.Address), cancellationToken);
            return Input2OpMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input2OpMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput2OpModeAsync(InputOpModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = Input2OpMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Input3OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputOpModeConfig> ReadInput3OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3OpMode.Address), cancellationToken);
            return Input3OpMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Input3OpMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputOpModeConfig>> ReadTimestampedInput3OpModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Input3OpMode.Address), cancellationToken);
            return Input3OpMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Input3OpMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInput3OpModeAsync(InputOpModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = Input3OpMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the InterlockEnabled register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InterlockEnabledConfig> ReadInterlockEnabledAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InterlockEnabled.Address), cancellationToken);
            return InterlockEnabled.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InterlockEnabled register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InterlockEnabledConfig>> ReadTimestampedInterlockEnabledAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InterlockEnabled.Address), cancellationToken);
            return InterlockEnabled.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the InterlockEnabled register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInterlockEnabledAsync(InterlockEnabledConfig value, CancellationToken cancellationToken = default)
        {
            var request = InterlockEnabled.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AccumulatedStepsSamplingRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AccumulatedStepsSamplingRateConfig> ReadAccumulatedStepsSamplingRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AccumulatedStepsSamplingRate.Address), cancellationToken);
            return AccumulatedStepsSamplingRate.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AccumulatedStepsSamplingRate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AccumulatedStepsSamplingRateConfig>> ReadTimestampedAccumulatedStepsSamplingRateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AccumulatedStepsSamplingRate.Address), cancellationToken);
            return AccumulatedStepsSamplingRate.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AccumulatedStepsSamplingRate register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAccumulatedStepsSamplingRateAsync(AccumulatedStepsSamplingRateConfig value, CancellationToken cancellationToken = default)
        {
            var request = AccumulatedStepsSamplingRate.FromPayload(MessageType.Write, value);
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
        public async Task<StoppedStepperMotors> ReadMotorStoppedAsync(CancellationToken cancellationToken = default)
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
        public async Task<Timestamped<StoppedStepperMotors>> ReadTimestampedMotorStoppedAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorStopped.Address), cancellationToken);
            return MotorStopped.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MotorOverVoltageDetection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadMotorOverVoltageDetectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorOverVoltageDetection.Address), cancellationToken);
            return MotorOverVoltageDetection.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MotorOverVoltageDetection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedMotorOverVoltageDetectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorOverVoltageDetection.Address), cancellationToken);
            return MotorOverVoltageDetection.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MotorRaisedError register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadMotorRaisedErrorAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorRaisedError.Address), cancellationToken);
            return MotorRaisedError.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MotorRaisedError register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedMotorRaisedErrorAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MotorRaisedError.Address), cancellationToken);
            return MotorRaisedError.GetTimestampedPayload(reply);
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
        /// Asynchronously writes a value to the Encoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncodersAsync(EncodersPayload value, CancellationToken cancellationToken = default)
        {
            var request = Encoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
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
        public async Task<DigitalInputStates> ReadDigitalInputStateAsync(CancellationToken cancellationToken = default)
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
        public async Task<Timestamped<DigitalInputStates>> ReadTimestampedDigitalInputStateAsync(CancellationToken cancellationToken = default)
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
        /// Asynchronously reads the contents of the MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MoveRelativePayload> ReadMoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MoveRelative.Address), cancellationToken);
            return MoveRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MoveRelativePayload>> ReadTimestampedMoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MoveRelative.Address), cancellationToken);
            return MoveRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MoveRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMoveRelativeAsync(MoveRelativePayload value, CancellationToken cancellationToken = default)
        {
            var request = MoveRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MoveRelative.Address), cancellationToken);
            return Motor0MoveRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MoveRelative.Address), cancellationToken);
            return Motor0MoveRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MoveRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MoveRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MoveRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MoveRelative.Address), cancellationToken);
            return Motor1MoveRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MoveRelative.Address), cancellationToken);
            return Motor1MoveRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MoveRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MoveRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MoveRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MoveRelative.Address), cancellationToken);
            return Motor2MoveRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MoveRelative.Address), cancellationToken);
            return Motor2MoveRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MoveRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MoveRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MoveRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MoveRelative.Address), cancellationToken);
            return Motor3MoveRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MoveRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MoveRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MoveRelative.Address), cancellationToken);
            return Motor3MoveRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MoveRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MoveRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MoveRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MoveAbsolutePayload> ReadMoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MoveAbsolute.Address), cancellationToken);
            return MoveAbsolute.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MoveAbsolutePayload>> ReadTimestampedMoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MoveAbsolute.Address), cancellationToken);
            return MoveAbsolute.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MoveAbsolute register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMoveAbsoluteAsync(MoveAbsolutePayload value, CancellationToken cancellationToken = default)
        {
            var request = MoveAbsolute.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MoveAbsolute.Address), cancellationToken);
            return Motor0MoveAbsolute.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MoveAbsolute.Address), cancellationToken);
            return Motor0MoveAbsolute.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MoveAbsolute register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MoveAbsoluteAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MoveAbsolute.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MoveAbsolute.Address), cancellationToken);
            return Motor1MoveAbsolute.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MoveAbsolute.Address), cancellationToken);
            return Motor1MoveAbsolute.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MoveAbsolute register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MoveAbsoluteAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MoveAbsolute.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MoveAbsolute.Address), cancellationToken);
            return Motor2MoveAbsolute.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MoveAbsolute.Address), cancellationToken);
            return Motor2MoveAbsolute.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MoveAbsolute register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MoveAbsoluteAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MoveAbsolute.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MoveAbsolute.Address), cancellationToken);
            return Motor3MoveAbsolute.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MoveAbsolute register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MoveAbsoluteAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MoveAbsolute.Address), cancellationToken);
            return Motor3MoveAbsolute.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MoveAbsolute register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MoveAbsoluteAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MoveAbsolute.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AccumulatedStepsPayload> ReadAccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(AccumulatedSteps.Address), cancellationToken);
            return AccumulatedSteps.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AccumulatedSteps register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AccumulatedStepsPayload>> ReadTimestampedAccumulatedStepsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(AccumulatedSteps.Address), cancellationToken);
            return AccumulatedSteps.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AccumulatedSteps register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAccumulatedStepsAsync(AccumulatedStepsPayload value, CancellationToken cancellationToken = default)
        {
            var request = AccumulatedSteps.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MaxPositionPayload> ReadMaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MaxPosition.Address), cancellationToken);
            return MaxPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MaxPositionPayload>> ReadTimestampedMaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MaxPosition.Address), cancellationToken);
            return MaxPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MaxPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMaxPositionAsync(MaxPositionPayload value, CancellationToken cancellationToken = default)
        {
            var request = MaxPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MaxPosition.Address), cancellationToken);
            return Motor0MaxPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MaxPosition.Address), cancellationToken);
            return Motor0MaxPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MaxPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MaxPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MaxPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MaxPosition.Address), cancellationToken);
            return Motor1MaxPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MaxPosition.Address), cancellationToken);
            return Motor1MaxPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MaxPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MaxPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MaxPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MaxPosition.Address), cancellationToken);
            return Motor2MaxPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MaxPosition.Address), cancellationToken);
            return Motor2MaxPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MaxPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MaxPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MaxPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MaxPosition.Address), cancellationToken);
            return Motor3MaxPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MaxPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MaxPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MaxPosition.Address), cancellationToken);
            return Motor3MaxPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MaxPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MaxPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MaxPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MinPositionPayload> ReadMinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MinPosition.Address), cancellationToken);
            return MinPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MinPositionPayload>> ReadTimestampedMinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(MinPosition.Address), cancellationToken);
            return MinPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MinPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMinPositionAsync(MinPositionPayload value, CancellationToken cancellationToken = default)
        {
            var request = MinPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MinPosition.Address), cancellationToken);
            return Motor0MinPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0MinPosition.Address), cancellationToken);
            return Motor0MinPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0MinPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0MinPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0MinPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MinPosition.Address), cancellationToken);
            return Motor1MinPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1MinPosition.Address), cancellationToken);
            return Motor1MinPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1MinPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1MinPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1MinPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MinPosition.Address), cancellationToken);
            return Motor2MinPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2MinPosition.Address), cancellationToken);
            return Motor2MinPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2MinPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2MinPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2MinPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MinPosition.Address), cancellationToken);
            return Motor3MinPosition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3MinPosition register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3MinPositionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3MinPosition.Address), cancellationToken);
            return Motor3MinPosition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3MinPosition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3MinPositionAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3MinPosition.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepRelativePayload> ReadStepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(StepRelative.Address), cancellationToken);
            return StepRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepRelativePayload>> ReadTimestampedStepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(StepRelative.Address), cancellationToken);
            return StepRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StepRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStepRelativeAsync(StepRelativePayload value, CancellationToken cancellationToken = default)
        {
            var request = StepRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor0StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor0StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0StepRelative.Address), cancellationToken);
            return Motor0StepRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor0StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor0StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor0StepRelative.Address), cancellationToken);
            return Motor0StepRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor0StepRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor0StepRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor0StepRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor1StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor1StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1StepRelative.Address), cancellationToken);
            return Motor1StepRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor1StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor1StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor1StepRelative.Address), cancellationToken);
            return Motor1StepRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor1StepRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor1StepRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor1StepRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor2StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor2StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2StepRelative.Address), cancellationToken);
            return Motor2StepRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor2StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor2StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor2StepRelative.Address), cancellationToken);
            return Motor2StepRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor2StepRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor2StepRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor2StepRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Motor3StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<int> ReadMotor3StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3StepRelative.Address), cancellationToken);
            return Motor3StepRelative.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Motor3StepRelative register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<int>> ReadTimestampedMotor3StepRelativeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt32(Motor3StepRelative.Address), cancellationToken);
            return Motor3StepRelative.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Motor3StepRelative register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMotor3StepRelativeAsync(int value, CancellationToken cancellationToken = default)
        {
            var request = Motor3StepRelative.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StopMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StepperMotors> ReadStopMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopMotors.Address), cancellationToken);
            return StopMotors.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StopMotors register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StepperMotors>> ReadTimestampedStopMotorsAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopMotors.Address), cancellationToken);
            return StopMotors.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StopMotors register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStopMotorsAsync(StepperMotors value, CancellationToken cancellationToken = default)
        {
            var request = StopMotors.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ResetEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<QuadratureEncoders> ReadResetEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ResetEncoders.Address), cancellationToken);
            return ResetEncoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ResetEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<QuadratureEncoders>> ReadTimestampedResetEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ResetEncoders.Address), cancellationToken);
            return ResetEncoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ResetEncoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteResetEncodersAsync(QuadratureEncoders value, CancellationToken cancellationToken = default)
        {
            var request = ResetEncoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }
    }
}
