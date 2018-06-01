using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp.Server;
using System;
using System.Net;
using System.Net.Sockets;
using artics.RemoteInformer;

public class Model
{
    public const int DefaultPort = 11011;
    public const string KeyPort = "port";
    public const int FloatSize = 4;

    public static WebSocketServer Server;
    public static MainServer MainService;
    public static UIMediator UIMediator;
    public static RemoteInfromerGyroMessage LastMessage;

    public static bool IsReady;

    public static void Init(UIMediator mediator)
    {
        LastMessage = new RemoteInfromerGyroMessage();

        UIMediator = mediator;
        UIMediator.SetIp(GetLocalIPAddress());

        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.18f;
        Application.runInBackground = true;

        StartServer();
    }

    public static int GetPort()
    {
        return PlayerPrefs.GetInt(KeyPort, DefaultPort);
    }

    public static void SetPort(int port)
    {
        PlayerPrefs.SetInt(KeyPort, port);
        PlayerPrefs.Save();
    }

    public static void StartServer()
    {
        if (Server != null)
        {
            Server.Stop();
        }

        try
        {
            Model.UIMediator.WriteToLog("Starting...\n");

            Server = new WebSocketServer("ws://" + GetLocalIPAddress() + ":" + GetPort());
            Model.UIMediator.WriteToLog("Created server instance\n");

            Server.AddWebSocketService<MainServer>("/");
            Model.UIMediator.WriteToLog("created services \n");

            Server.Start();
            Model.UIMediator.WriteToLog("Starting finished...\n");
        }
        catch (Exception ex)
        {
            UIMediator.WriteToLog("Server creation error:" + ex.Message + "\n" + ex.ToString());
        }

        if (Server != null)
        {
            UIMediator.WriteToLog(Server.Address.ToString() + "\n");
        }
    }

    public static void OnError()
    {
        Model.UIMediator.WriteToLog("Error happend...\n");

        if (Server == null || !Server.IsListening)
        {
            StartServer();
        }
    }

    public static void OnReady()
    {
        IsReady = true;
        UIMediator.SetIp(Server.Address.ToString());
    }

    public static void Update()
    {
        if (IsReady && MainService != null)
        {
            MainService.SendData(FormMessage());
        }

        PrintData();
    }

    public static byte[] FormMessage()
    {
        LastMessage.FillWithData();

        return LastMessage.SerializeMessage();
    }

    public static void PrintData()
    {
        RemoteInfromerGyroMessage mess = new RemoteInfromerGyroMessage();
        mess.FillWithData();
        UIMediator.WriteToData(mess.PrintMessage());
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return ("Local IP Address Not Found!");

        //return NetworkManager.singleton.networkAddress;
    }



}
