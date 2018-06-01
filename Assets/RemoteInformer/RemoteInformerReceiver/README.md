RemoteReceiver scripts
======================

### Basic usage:

Create a new instance of RremoteInfromerReceiver\< \_ your message type_\>. You
use `RemoteInfromerStandartMessage` message type if you haven't created custom.

`var ReceiverCoreInstance = new
RremoteInfromerReceiver<RemoteInfromerStandartMessage>();`

Init call Init method with address and port parameters

` ReceiverCoreInstance.Init("192.168.0.1", "11011");`

or

`ReceiverCoreInstance.Init("192.168.0.1:11011");`

or

` ReceiverCoreInstance.Init("ws://192.168.0.1:11011");`
