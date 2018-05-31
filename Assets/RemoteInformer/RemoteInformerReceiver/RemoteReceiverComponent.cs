using UnityEngine;

namespace artics.RemoteInformer
{
    [ExecuteInEditMode]
    public class RemoteReceiverComponent : MonoBehaviour
    {
        public static RemoteReceiverComponent Singleton;

        public RremoteInfromerReceiver<RemoteInfromerGyroMessage> ReceiverCoreInstance;
        public string Address;
        public bool AutoConnect;
        public bool InitSingleton;

        void Start()
        {
            ReceiverCoreInstance = new RremoteInfromerReceiver<RemoteInfromerGyroMessage>();

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