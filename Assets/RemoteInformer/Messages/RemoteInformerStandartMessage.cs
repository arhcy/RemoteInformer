﻿using UnityEngine.Networking;
using UnityEngine;

namespace artics.RemoteInformer
{

    /// <summary>
    /// Model of message to send Gyro data
    /// </summary>
    public class RemoteInformerStandartMessage : MessageBase, IRemoteInformerMessage
    {
        public Quaternion Attitude;
        public Vector3 Gravity;
        public Vector3 RotationRate;
        public Vector3 UserAcceleration;
        public float HorizontalAxis;
        public float VerticalAxis;
        public Vector3[] TouchPositions;
        public short[] TouchPhases;

        public virtual void FillWithData()
        {
            Attitude = Input.gyro.attitude;
            Gravity = Input.gyro.gravity;
            RotationRate = Input.gyro.rotationRate;
            UserAcceleration = Input.gyro.userAcceleration;
            HorizontalAxis = Input.GetAxis("Horizontal");
            VerticalAxis = Input.GetAxis("Vertical");

            Touch[] touches = Input.touches;
            TouchPositions = new Vector3[touches.Length];
            TouchPhases = new short[touches.Length];

            for (int i = 0; i < touches.Length; i++) {
                TouchPositions[i] = touches[i].position;
                TouchPhases[i] = (short)touches[i].phase;
            }
        }

        public virtual string PrintMessage()
        {
            return
                Utils.PrintRoundedQuaternion("Attitude:", Attitude) + "\n\n" +
                Utils.PrintRoundedVector("Gravity:", Gravity) + "\n\n" +
                Utils.PrintRoundedVector("RotationRate:", RotationRate) + "\n\n" +
                Utils.PrintRoundedVector("UserAcceleration:", UserAcceleration) + "\n\n" +
                "Axis:\n" +
                "H:" + HorizontalAxis + " V:" + VerticalAxis + "\n\n" +
                Utils.PrintTouchInfo(TouchPositions, TouchPhases);           
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
            reader.SeekZero();
            Deserialize(reader);
        }
    }
}
