# 702VirtualPet - Group 15

A virtual pet designer for group 15's research, the virtual pet will have different reactions according to the user's action to the sensor

### Installation
1. Copy the repository to the local environment
2. run the project with Unity v2019.4.37f1
3. Connect the hardware to the device, make sure the hardware is successfully connected to the device

##### Debug with unsuccessful connection of hardware
We use Arduino to debug issues related to the connection of hardware.

### System Requirement
- hardware requirement: sensors that could be connected to the device
  - Components
    - FSR406: A force-sensitive resistor that changes resistance based on applied force.
    - Linear Voltage Conversion Modules: Converts the FSR's resistance to a proportional voltage.
    - Arduino: Reads the converted voltage and communicates the data to the PC.
    - PC: Used to process and possibly visualize the force data.
  
  - Setup
    1. Connecting the FSR406 to the Voltage Conversion Module:
      - Connect one end of the FSR to one of the input pins on the linear voltage converter module. Connect the other end of the FSR to a different pin.

    2. Linking the Voltage Conversion Module to Arduino:
      - Connect the module's output (AO) to one of the Arduino's analog pins(A0-A3). Connect the power supply (VCC) and ground (GND) to the Arduino's 5V and GND pins via the breadboard.

    3. Connecting Arduino to PC:
      - Using a USB 2.0 type B cable, connect the Arduino to one of the PC's USB ports. Install the required drivers using the Arduino's IDE.
      - Uploading the Arduino Code(optional):
        - Open the Arduino IDE on your PC.
        - Write or paste the code to read the analog value from pin A0.
        - Use the Serial.println() function to send this data to the PC.
        - Upload the code to the Arduino.
      - Reading Data on the PC:
        - Open the Serial Monitor in the Arduino IDE. You should see force data being printed from the FSR in real-time.

- software requirement: The system should be able to run Unity v2019.4.37f1

### Functionality
- virtual pet will walk towards the user and roll when a user touches the sensor connected to the device

### Contributors
- Zixuan Wen (zwen655)
- Guoying Jiang (gjia514)
- Xingyu Liu (xliu315)
- Jiaqi Zhao (jzha535)
- Yun-Shan Tsai (ytsa347)
- Yuewei Zhang (yzhb544)
