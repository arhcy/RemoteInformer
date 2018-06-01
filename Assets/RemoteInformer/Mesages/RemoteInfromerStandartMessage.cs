using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace artics.RemoteInformer
{

    /// <summary>
    /// Model of message to send Gyro data
    /// </summary>
    public class RemoteInfromerStandartMessage : MessageBase, IRemoteInfromerMessage
    {
        public Quaternion Attitude;
        public Vector3 Gravity;
        public Vector3 RotationRate;
        public Vector3 UserAcceleration;

        public virtual void FillWithData()
        {
            Attitude = Input.gyro.attitude;
            Gravity = Input.gyro.gravity;
            RotationRate = Input.gyro.rotationRate;
            UserAcceleration = Input.gyro.userAcceleration;
        }

        public virtual string PrintMessage()
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

        public virtual byte[] SerializeMessage()
        {
            NetworkWriter writer = new NetworkWriter();
            Serialize(writer);

            return writer.AsArray();
        }

        public virtual void DeserializeMessage(byte[] data)
        {
            NetworkReader reader = new NetworkReader(data);
            Deserialize(reader);
        }
    }
}
