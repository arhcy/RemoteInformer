RemoteReceiver scripts
======================

### Basic usage:

<br>

Create a new instance of RremoteInfromerReceiver\< \_ your message type_\>. You
use `RemoteInfromerStandartMessage` message type if you haven't created custom.

```C#
var ReceiverCoreInstance = new RremoteInfromerReceiver<RemoteInfromerStandartMessage>();
```

<br>

Call Init method with address and port parameters:

```C#
ReceiverCoreInstance.Init("192.168.0.1", "11011"); 
//or
ReceiverCoreInstance.Init("192.168.0.1:11011"); 
//or
ReceiverCoreInstance.Init("ws://192.168.0.1:11011");
```

<br>

To access data from a device - use `LastMessage` field. It contains last deserialized message.

```C#
ReceiverCoreInstance.LastMessage.Attitude //gets gyro attitude
```

<br>

### Using RemoteReceiverComponent 
This component automatically creates `ReceiverCoreInstance` instance, can automatically connect to the server on your phone and visualize results from in the inspector.

<p align=center>
  <img src="../../../Documentation media/ComponentScreen.png" width=30%/>
  
  </p>
  
  <br>
  
`Address`  - You need to provide the address of  your phone in the format "address:port".

`Auto Connect` - connects to the server when the Start() function calls.

`Init Singleton` - stores reference of this component in the static variable. You can access this component from anywhere.
`RemoteReceiverComponent.Singleton`
