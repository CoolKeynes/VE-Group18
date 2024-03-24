## Model instruction
- avatar ... // CZH write this part to describe the avatar created by UMA

- All the other models (including AED, heartbeat monitor, electrode pads, first-aid kit)  are created in folder `Assets/Customize`.

## Code instruction
All C# script are in folder `Assets/Customize/C# scripts`.

`Case_01_B_OpenOrClose.cs` used for first aid kit to control the opening and closing of the box, it also includes the synchronization feature with Ubiq and other players.

`chuchanyi.cs` used to control the operation of the defibrillator and heart beat monitor, that is, to control the defibrillation animation of the avatar.

`ClickstartKey_heart.cs` used to control the start functionality of heart beat monitor.

`ClickstartKey.cs` used to control the start functionality of defibrillator.

`Electrode.cs` used in defibrillator electrode pads and heartbeat monitor pads implements the function of grabbing and placing pads.
