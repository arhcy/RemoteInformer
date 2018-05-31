using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

public class MainServer : WebSocketBehavior
{
    public MainServer()
    {
        Model.MainService = this;
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
        Model.UIMediator.WriteToLog("Closed:");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        Model.UIMediator.WriteToLog("MEssage:" + e.ToString());
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        Model.OnReady();
        Model.UIMediator.WriteToLog("Connected:" + Context.User.ToString());
    }

    public void SendData(byte[] array)
    {
        Send(array);
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);

        Model.UIMediator.WriteToLog("MainService Error:" + e.ToString() + "\n");
    }



}
