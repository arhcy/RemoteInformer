
Start page: https://github.com/arhcy/RemoteInformer/blob/master/README.md

RemoteReceiver scripts
======================

### Basic usage:

<br>

Create a new instance of `RremoteInformerReceiver<` *your message type* `>`. You
use `RemoteInformerStandartMessage` message type if you haven't created custom.

```C#
var ReceiverCoreInstance = new RremoteInformerReceiver<RemoteInformerStandartMessage>();
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
  <img src="https://github.com/arhcy/RemoteInformer/blob/master/Documentation%20media/ComponentScreen.png" width=30%/>
  
  </p>
  
  <br>
  
`Address`  - You need to provide the address of  your phone in the format `"address:port"`.

`Auto Connect` - connects to the server when the Start() function calls.

`Init Singleton` - stores reference of this component in the static variable `RemoteReceiverComponent.Singleton`. You can access this component from anywhere.
```C#
RemoteReceiverComponent.Singleton.ReceiverCoreInstance.LastMessage.Attitude //gets gyro attitude
```

RemoteReceiverComponent customization:
=====

### Custom messages
You can easily change the type of message. Just change alias in the head of `RemoteReceiverComponent.cs` file.

```C#
/// Change MessageType to use your custom messages
using MessageType = artics.RemoteInformer.RemoteInformerStandartMessage;
```

More about messages customizations:
(ness cust)[]

### Writing custom wrappers:
You can override the component and write custom wrappers for correct data access depending on platform.
```C#
        /// <summary>
        /// example of data getting customization.
        /// </summary>
        /// <returns></returns>
        public Quaternion GetGyroAttitude()
        {
#if UNITY_EDITOR
            if (ReceiverCoreInstance == null)
                return default(Quaternion);

            return ReceiverCoreInstance.LastMessage.Attitude;
#else
        return Input.gyro.attitude;
#endif
        }
```
