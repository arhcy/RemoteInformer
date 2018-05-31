using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace artics.RemoteInformer
{
    /// <summary>
    /// Model of message. 
    /// Yoy can customize it to enable or disable any data you want
    /// </summary>
    public class RemoteInfromerDataMessage : MessageBase
    {
        public Quaternion Attitude;
        public Vector3 Gravity;
        public Vector3 RotationRate;
        public Vector3 UserAcceleration;

        /// <summary>
        /// This function calls every frame. You can extend id in your way.
        /// </summary>
        public virtual void FillWithData()
        {
            Attitude = Input.gyro.attitude;
            Gravity = Input.gyro.gravity;
            RotationRate = Input.gyro.rotationRate;
            UserAcceleration = Input.gyro.userAcceleration;
        }
    }
}
