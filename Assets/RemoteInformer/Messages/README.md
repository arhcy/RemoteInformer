
Start page: https://github.com/arhcy/RemoteInformer/blob/master/README.md

Custom data
=====

You can customize data which application sends. Just create a new class which implements the `IRemoteInfromerMessage` interface.
### Interface:

This function calls every frame. It fills message with data. 

```C#
void FillWithData();
```

Prints string data of the message. Using for visualization data on app's screen and in the editor.

```C#
string PrintMessage();
```

erializes messages

```C#
byte[] SerializeMessage();
```

Deserializes message

```C#
void DeserializeMessage(byte[] data);
```

### Code tweaking:

To make app send your messages you need to change the alias of `Mode.cs` file and rebuild.
		
```C#
/// Change MessageType to use your custom messages
using MessageType = artics.RemoteInformer.RemoteInfromerStandartMessage;
```

To make editor correctly receive your messages you need to create a new instance of `RremoteInfromerReceiver<` *your message type* `>`. 

```C#
var ReceiverCoreInstance = new RremoteInfromerReceiver<RemoteInfromerStandartMessage>();
```

If you using RemoteReceiverComponent you must change alias in the head of `RemoteReceiverComponent.cs` file. It will automatically create RremoteInfromerReceiver instance with you class listed.

```C#
/// Change MessageType to use your custom messages
using MessageType = artics.RemoteInformer.RemoteInfromerStandartMessage;
```
