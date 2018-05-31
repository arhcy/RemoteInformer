#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace artics.RemoteInformer
{
    [CustomEditor(typeof(RemoteInformerComponent))]
    public class ReceiverInspector : Editor
    {
        protected RemoteInformerComponent ReceiverInstance;

        void OnEnable()
        {
            ReceiverInstance = (RemoteInformerComponent)target;

            if (ReceiverInstance.InformerInstance != null)
                EditorApplication.update += DoUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= DoUpdate;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (ReceiverInstance.InformerInstance == null)
                return;

            GUILayout.Label("Is initing:" + ReceiverInstance.InformerInstance.IsIniting);
            GUILayout.Label("Is open:" + ReceiverInstance.InformerInstance.IsOpen);

            if (ReceiverInstance.InformerInstance.IsReadyToStart())
            {
                if (GUILayout.Button("Connect"))
                {
                    ReceiverInstance.Connect();
                }
            }

            if (GUILayout.Button("Disconnect"))
            {
                ReceiverInstance.InformerInstance.Close();
            }

            Quaternion gyro = ReceiverInstance.GetGyroData();
            GUILayout.Label("Gyro:");
            GUILayout.Label("X:" + gyro.x + " Y:" + gyro.y + " Z:" + gyro.z + " W:" + gyro.w);
        }

        void DoUpdate()
        {
            if (ReceiverInstance.InformerInstance.IsOpen)
            {
                Repaint();
            }
        }

    }
}
#endif