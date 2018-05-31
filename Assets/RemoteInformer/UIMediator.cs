using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMediator : MonoBehaviour
{
    public InputField Inputport;
    public Text TextIP;
    public Button ButtonRestart;
    public Text TextConsole;
    public Text TextDataOutput;

    protected Queue<string> LogQueue;

    // Use this for initialization
    void Start()
    {
        LogQueue = new Queue<string>();

        Model.Init(this);

        ButtonRestart.onClick.AddListener(Model.StartServer);
        Inputport.onEndEdit.AddListener(OnPortChanged);

        Inputport.text = Model.GetPort().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Model.Update();

        while (LogQueue.Count > 0)
        {
            TextConsole.text += LogQueue.Dequeue();
        }
    }

    public void WriteToData(string log)
    {
        TextDataOutput.text = log;
    }

    public void WriteToLog(string log)
    {
        LogQueue.Enqueue(log);
        //TextConsole.text += log;
    }

    public void SetIp(string ip)
    {
        TextIP.text = ip;
    }

    public void OnPortChanged(string data)
    {
        int parsed;

        if (int.TryParse(data, out parsed))
        {
            Model.SetPort(parsed);
        }
    }

}
