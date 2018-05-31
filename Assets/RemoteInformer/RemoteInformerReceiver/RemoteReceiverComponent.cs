using UnityEngine;

namespace artics.RemoteInformer
{
    [ExecuteInEditMode]
    public class RemoteReceiverComponent : MonoBehaviour
    {
        public static RemoteReceiverComponent Singleton;

        public RremoteInfromerReceiver ReceiverCoreInstance;
        public string Address;
        public bool AutoConnect { get; }
        public bool InitSingleton { get; }

        public Quaternion GetGyroData()
        {
#if UNITY_EDITOR
            if (ReceiverCoreInstance == null)
                return Quaternion.identity;

            return ReceiverCoreInstance.GyroData;
#else
        return Input.gyro.attitude;
#endif
        }

        void Start()
        {
            ReceiverCoreInstance = new RremoteInfromerReceiver();

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
            ReceiverCoreInstance.Close();
        }
    }
}