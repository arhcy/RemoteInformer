#if UNITY_EDITOR
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

            var receiverCore = ReceiverInstance.ReceiverCoreInstance;

            GUILayout.Label("Is initing:" + receiverCore.IsIniting);
            GUILayout.Label("Is open:" + receiverCore.IsOpen);

            if (receiverCore.IsReadyToStart())
            {
                if (GUILayout.Button("Connect"))
                {
                    ReceiverInstance.Connect();
                    EditorApplication.update += DoUpdate;
                }
            }

            if (GUILayout.Button("Disconnect"))
            {
                receiverCore.Close();
                EditorApplication.update -= DoUpdate;
            }

            if (receiverCore.LastMessage != null)
                GUILayout.Label(receiverCore.LastMessage.PrintMessage());
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