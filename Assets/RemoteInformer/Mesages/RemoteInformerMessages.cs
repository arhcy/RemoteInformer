using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace artics.RemoteInformer
{
    public class RemoteInformerMessages
    {
    }

    public class RemoteInfromerDataMessage : MessageBase {
        public Quaternion Attitude;
        public Vector3 Gravity;
        public Vector3 RotationRate;
        public Vector3 UserAcceleration;

        public void FillWithData() {
            Attitude = Input.gyro.attitude;
            Gravity = Input.gyro.gravity;
            RotationRate = Input.gyro.rotationRate;
            UserAcceleration = Input.gyro.userAcceleration;
        }
        
    }
}
