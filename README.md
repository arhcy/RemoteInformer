Remote data collection app and lib for Unity3d projects.
=====


- Custom remote data collection app that works on local network without USB
cables. 
- easily integrates into Unity3d projects.
- You can use it for testing your VR / AR mobile projects.
- You can get data from multiple phones at the same time.
- You can view data from phone on multiple client instances
- You can modify and rebuild this app to get custom data you need.


Currently, it sends data of such hardware features:

-   `Attitude`
-   `Gravity`
-   `RotationRate`
-   `UserAcceleration`
-   `HorizontalAxis`
-   `VerticalAxis`
-   `Touch positions`
-   `Touch phases`

<p align="center">
<img src="Documentation media/MainScreen.jpg" width=70% align="center"/>
 
_main screen_
</p>

 
 Instruction:
  ======
 
 ### 1. Install the app on device
 - you can download APK from this repository [Built/RemoteInformer.apk](Built/RemoteInformer.apk)
 - you can download the app from [Google Play](https://play.google.com/store/apps/details?id=com.archypiragkov.RemoteInformer)
 - you can build it manually

### 2. Install receiver unitypackage into your project (Unity 5.2+ required)

Repository:
[RemoteInformerReceiver.unitypackageas](UnityPackage/RemoteInformerReceiver.unitypackage)

Asset store:

### 3. Using RemoteReceiver classes in your project
Use RemoteReceiver scripts to access data from phone in yout project

[RemoteReceiver documentation](Assets/RemoteInformer/RemoteInformerReceiver/README.md)

### 4. Customization
You can customize data which application sends. 

[RemoteReceiver documentation](Assets/RemoteInformer/Messages/README.md)



