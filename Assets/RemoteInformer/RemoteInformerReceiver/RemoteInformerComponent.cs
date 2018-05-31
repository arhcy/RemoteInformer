using UnityEngine;

namespace artics.RemoteInformer
{
    public class RemoteInformerComponent : MonoBehaviour
    {
        public static RemoteInformerComponent Singleton;

        public RremoteInfromerReceiver InformerInstance;
        public string Address;
        public bool AutoConnect { get; }
        public bool InitSingleton { get; }

        public Quaternion GetGyroData()
        {
#if UNITY_EDITOR
            if (InformerInstance == null)
                return Quaternion.identity;

            return InformerInstance.GyroData;
#else
        return Input.gyro.attitude;
#endif
        }

        void Start()
        {
            InformerInstance = new RremoteInfromerReceiver();

            if (InitSingleton)
                Singleton = this;

            if (AutoConnect)
                Connect();
        }

        public void Connect()
        {
            if (InformerInstance.IsIniting)
            {
                Debug.LogWarning("Socket is initing");
                return;
            }

            InformerInstance.Init(Address);
        }

        private void OnDestroy()
        {
            InformerInstance.Close();
        }
    }
}