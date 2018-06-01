using UnityEngine;

/// Change MessageType to use your custom messages
using MessageType = artics.RemoteInformer.RemoteInfromerGyroMessage;

namespace artics.RemoteInformer
{
    public class RemoteReceiverComponent : MonoBehaviour
    {
        public static RemoteReceiverComponent Singleton;

        public RremoteInfromerReceiver<MessageType> ReceiverCoreInstance;
        public string Address;
        public bool AutoConnect;
        public bool InitSingleton;

        void Start()
        {
            ReceiverCoreInstance = new RremoteInfromerReceiver<MessageType>();

            if (Address == string.Empty)
                Address = ReceiverCoreInstance.GetLastAdddress();

            if (InitSingleton)
                Singleton = this;

            if (AutoConnect)
                Connect();
        }

        public void Connect()
        {
            if (ReceiverCoreInstance.IsIniting)
            {
                Debug.LogWarning("Socket is initing");
                return;
            }

            ReceiverCoreInstance.Init(Address);
        }

        private void OnDestroy()
        {
            if (ReceiverCoreInstance != null)
                ReceiverCoreInstance.Close();
        }

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

    }
}