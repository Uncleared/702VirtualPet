#include <Arduino.h>

#define DEBUGSerial Serial
#define PRESS_MIN	20
#define PRESS_MAX	6000
#define VOLTAGE_MIN 100
#define VOLTAGE_MAX 3300

void setup()
{
	DEBUGSerial.begin(9600); // setup serial
	Serial.println("setup end!");
}

void loop()
{
  getPressValue(A0);
  getPressValue(A1);
  getPressValue(A2);
  getPressValue(A3);
  DEBUGSerial.print("------\n");
	delay(300);
}
long getPressValue(int pin)
{
	int value = analogRead(pin);
  DEBUGSerial.print("Pin");
	DEBUGSerial.print(pin-14);
  DEBUGSerial.print("= ");
	DEBUGSerial.print(value);
	DEBUGSerial.print("\n");

}



