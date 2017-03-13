# UnityInputLagTest
A project for testing unitys input lag


There is a runnable version in InputLagTest/Builds/InputLagTest folder (InputLagTest.exe).


The application should look like this
![InputLagTestStart](https://github.com/HiddenMonk/UnityInputLagTest/blob/master/Images/InputLagTestStartup.png)

You can click the options button or press Esc to open the options menu, which should look like this
![InputLagTestOptions](https://github.com/HiddenMonk/UnityInputLagTest/blob/master/Images/InputLagTestOptions.png)


There are 4 scenes; Input Lag Test, Auto Input Lag Test, Mouse Acceleration Test, and Detected Input Lag Test.

Input Lag Test Scene requires you to have a fast camera and mouse with a high poll rate to test the input lag of rotating the screen with the mouse.

Auto Input Lag Test Scene manually sets the Windows cursor and then we check to see when unity detects the change, which seems to be the next frame. However, it is unknown if setting the mouse manually would give the same results as actually moving a mouse.
The code used for manually setting the mouse position can be found here https://github.com/HiddenMonk/UnityInputLagTest/blob/master/InputLagTest/Assets/Scripts/ForceSetMouse.cs

Mouse Acceleration Test Scene tries to test if unity uses true raw input or if it is using mouse acceleration if the users computer has it enabled. While this is not a 100% accurate test, it does seem to show that unity does use true raw input and ignores mouse acceleration.

Detected Input Lag Test shows what frame input was detected as compared to relying on the camera updating the screen such as in the Input Lag Test scene. What you would do is record with a high speed camera, press the input and see what frame you pressed it through your high speed camera, then see the resulting detected frame on the screen.
