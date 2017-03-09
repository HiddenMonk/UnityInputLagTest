# UnityInputLagTest
A project for testing unitys input lag


There is a runnable version in InputLagTest/Builds/InputLagTest folder (InputLagTest.exe).


The application should look like this
![InputLagTestStart](https://github.com/HiddenMonk/UnityInputLagTest/blob/master/Images/InputLagTestStartup.png)

You can click the options button or press Esc to open the options menu, which should look like this
![InputLagTestOptions](https://github.com/HiddenMonk/UnityInputLagTest/blob/master/Images/InputLagTestOptions.png)


There are 3 scenes; Input Lag Test, Auto Input Lag Test, and Mouse Acceleration Test.

Input Lag Test Scene requires you to have a fast camera and mouse with a high poll rate to test the input lag of rotating the screen with the mouse.

Auto Input Lag Test Scene manually sets the Windows cursor and then we check to see when unity detects the change, which seems to be the next frame. However, it is unknown if setting the mouse manually would give the same results as actually moving a mouse.
The code used for manually setting the mouse position can be found here https://github.com/HiddenMonk/UnityInputLagTest/blob/master/InputLagTest/Assets/Scripts/ForceSetMouse.cs

Mouse Acceleration Test Scene tries to test if unity uses true raw input or if it is using mouse acceleration if the users computer has it enabled. While this is not a 100% accurate test, it does seem to show that unity does use true raw input and ignores mouse acceleration.
