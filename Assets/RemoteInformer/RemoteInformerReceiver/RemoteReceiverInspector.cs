#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace artics.RemoteInformer
{
    [CustomEditor(typeof(RemoteReceiverComponent))]
    public class RemoteReceiverInspector : Editor
    {
        protected RemoteReceiverComponent ReceiverInstance;

        void OnEnable()
        {
            ReceiverInstance = (RemoteReceiverComponent)target;

            if (ReceiverInstance.ReceiverCoreInstance != null)
                EditorApplication.update += DoUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= DoUpdate;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (ReceiverInstance.ReceiverCoreInstance == null)
                return;

            GUILayout.Label("Is initing:" + ReceiverInstance.ReceiverCoreInstance.IsIniting);
            GUILayout.Label("Is open:" + ReceiverInstance.ReceiverCoreInstance.IsOpen);

            if (ReceiverInstance.ReceiverCoreInstance.IsReadyToStart())
            {
                if (GUILayout.Button("Connect"))
                {
                    ReceiverInstance.Connect();

                    EditorApplication.update += DoUpdate;
                }
            }

            if (GUILayout.Button("Disconnect"))
            {
                ReceiverInstance.ReceiverCoreInstance.Close();
                EditorApplication.update -= DoUpdate;
            }

            Quaternion gyro = ReceiverInstance.GetGyroData();
            GUILayout.Label("Gyro:");
            GUILayout.Label("X:" + Round(gyro.x) + " Y:" + Round(gyro.y) + " Z:" + Round(gyro.z) + " W:" + Round(gyro.w));
        }

        protected string Round(float value)
        {
            return Math.Round(value, 3).ToString();
        }

        void DoUpdate()
        {
            if (ReceiverInstance.ReceiverCoreInstance.IsOpen)
            {
                Repaint();
            }
        }

    }
}
#endif