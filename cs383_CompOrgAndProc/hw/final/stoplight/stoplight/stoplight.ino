int red = 10;
int yellow = 9;
int green = 8;

void setup(){
    pinMode(red, OUTPUT);
    pinMode(yellow, OUTPUT);
    pinMode(green, OUTPUT);
}

void loop(){
    changeLights();
    delay(19000);
}

void changeLights(){
    //red for 10 seconds
    digitalWrite(green, LOW);
    digitalWrite(yellow, LOW);
    digitalWrite(red, HIGH);
    delay(10000);

    //green for 7 seconds
    digitalWrite(green, HIGH);
    digitalWrite(yellow, LOW);
    digitalWrite(red, LOW);
    delay(7000);

    //yellow for 2 seconds
    digitalWrite(green, LOW);
    digitalWrite(yellow, HIGH);
    digitalWrite(red, LOW);
    delay(2000);
}
