#include<Uduino.h>
#include <Stepper.h>
Uduino uduino("realGateStepper");

const int stepsPerRevolution = 2048;
const int rpm = 5;
const int stepsPerCommand = 10;
Stepper stepper1 = Stepper(stepsPerRevolution, 2, 4, 3, 5);
int stepsFromStart = 0;

void setup()
{
  stepper1.setSpeed(rpm);
  Serial.begin(9600);
  uduino.addCommand("turnCCW", turnCounterClockwise);
  uduino.addCommand("turnCW", turnClockwise);
  uduino.addCommand("setZero", setZero);
}

void setZero()
{
  stepsFromStart = 0;
}

void turnCounterClockwise() {
  stepper1.step(stepsPerCommand);
  stepsFromStart -= stepsPerCommand;
}

void turnClockwise() {
  stepper1.step(-stepsPerCommand);
  stepsFromStart += stepsPerCommand;
}

void loop()
{
  uduino.println(stepsFromStart);
  uduino.update();
  delay(15);
}
