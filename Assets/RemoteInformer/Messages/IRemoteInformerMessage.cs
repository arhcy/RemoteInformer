using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace artics.RemoteInformer
{
    /// <summary>
    /// Message interface. 
    /// Yoy can create custom messages to transfer your custom data
    /// </summary>
    public interface IRemoteInformerMessage
    {
        /// <summary>
        /// This function calls every frame. It fills message with data. 
        /// </summary>
        void FillWithData();

        /// <summary>
        /// Prints string data of the message.Using for visualization data on app's screen and in the editor.
        /// </summary>
        /// <returns></returns>
        string PrintMessage();

        /// <summary>
        /// Serializes messages
        /// </summary>
        /// <returns></returns>
        byte[] SerializeMessage();

        /// <summary>
        /// Deserializes message
        /// </summary>
        /// <param name="data"></param>
        void DeserializeMessage(byte[] data);
    }
}
