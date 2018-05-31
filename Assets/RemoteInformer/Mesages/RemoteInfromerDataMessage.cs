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

        /// <summary>
        /// Override it to visualize data in editor
        /// </summary>
        /// <returns></returns>
        public virtual string PrintMessage()
        {
            return "Abstract data message";
        }
    }

    /// <summary>
    /// Model of message to send Gyro data
    /// </summary>
    public class RemoteInfromerGyroMessage : RemoteInfromerDataMessage
    {
        public Quaternion Attitude;
        public Vector3 Gravity;
        public Vector3 RotationRate;
        public Vector3 UserAcceleration;

        public override void FillWithData()
        {
            Attitude = Input.gyro.attitude;
            Gravity = Input.gyro.gravity;
            RotationRate = Input.gyro.rotationRate;
            UserAcceleration = Input.gyro.userAcceleration;
        }

        public override string PrintMessage()
        {
            return
                "Attitude:\n" +
                Utils.PrintRoundedQuaternion(Attitude) + "\n\n" +
                "Gravity:\n" +
                Utils.PrintRoundedVector(Gravity) + "\n\n" +
                "RotationRate:\n" +
                Utils.PrintRoundedVector(RotationRate) + "\n\n" +
                "UserAcceleration:\n" +
                Utils.PrintRoundedVector(UserAcceleration);
        }


    }
}
