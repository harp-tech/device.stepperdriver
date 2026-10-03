# Getting Started

The [Harp StepperDriver](https://github.com/harp-tech/device.stepperdriver) is a stepper motor controller device for four motors with support for quadrature encoder feedback.

## Installation

- Install [Bonsai](https://bonsai-rx.org/docs/articles/installation.html).
- Install the `Harp.StepperDriver` package by searching for it in the [Bonsai package manager](https://bonsai-rx.org/docs/articles/packages.html).

## Connections

**Stepper Motor** - The `StepperDriver` supports 4-lead bipolar stepper motors that draw up to 2 A per phase. Ensure that the 4 leads (`A+`, `A-`, `B+`, and `B-`) are correctly connected to the labeled terminals on the device. 

**Power Supply** - The `StepperDriver` requires an external power supply that matches your motor configuration:
- **Voltage** - Select a supply matching the recommended driving voltage listed in the motor datasheet (15-35 V). For high-speed and high-torque applications, supply at least 10 V above the minimum driving voltage, and mount the `StepperDriver` on a metal surface to dissipate heat.
- **Current** - Provide at least double the motor's rated phase current, multiplied by the number of motors (e.g. 8 A for two 2 A motors).

<br>

---

These tutorials were written and tested with:<br>
**Hardware** v1.0<br>
**Firmware** v0.7<br>
**Harp.StepperDriver** v0.4