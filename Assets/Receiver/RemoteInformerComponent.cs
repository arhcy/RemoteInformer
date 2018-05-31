using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using WebSocketSharp;
using UnityEngine;


public class RemoteInformerComponent : MonoBehaviour
{
    public static RemoteInformerComponent Singleton;

    public RremoteInfromerReceiver InformerInstance;
    public string Address;
    public bool AutoConnect { get; }
    public bool InitSingleton { get; }


    public Quaternion GetGyroData()
    {
#if UNITY_EDITOR
        if (InformerInstance == null)
            return Quaternion.identity;

        return InformerInstance.GyroData;
#else
        return Input.gyro.attitude;
#endif
    }

    void Start()
    {
        InformerInstance = new RremoteInfromerReceiver();

        if (InitSingleton)
            Singleton = this;

        if (AutoConnect)
            Connect();
    }

    public void Connect()
    {
        if (InformerInstance.IsIniting)
        {
            Debug.LogWarning("Socket is initing");
            return;
        }

        InformerInstance.Init(Address);
    }



    private void OnDestroy()
    {
        InformerInstance.Close();
    }
}

public class RremoteInfromerReceiver
{
    protected const string AddressKey = "Address";
    public string Address = "";
    public const int FloatSize = 4;

    public Quaternion GyroData;
    protected WebSocket Socket;

    public bool IsOpen;
    public bool IsIniting;
    /// <summary>
    /// Inits client with last addres
    /// </summary>
    public void Init()
    {
        Init(Address);
    }

    /// <summary>
    /// inits client with listed address and port
    /// </summary>
    /// <param name="address"></param>
    /// <param name="port"></param>
    public void Init(string address, string port)
    {
        Init(FormAddreess(address, port));
    }

    /// <summary>
    /// inits client with ready address
    /// </summary>
    /// <param name="address"></param>
    public void Init(string address)
    {
        IsIniting = true;
        IsOpen = false;
        Address = address;

        if (Address == string.Empty)
        {
            Address = GetLastAdddress();

            if (Address == string.Empty)
                throw new Exception("No last address fround");
        }
        else
            PlayerPrefs.SetString(AddressKey, address);

        if (!address.Contains("ws://"))
            Address = "ws://" + address;

        Socket = new WebSocket(Address);
        Socket.OnMessage += OnMessage;
        Socket.OnError += OnDisconnect;
        Socket.OnOpen += OnConencted;


        Thread initThread = new Thread(Socket.Connect);
        initThread.Start();
    }

    public void OnMessage(object sender, MessageEventArgs args)
    {
        IsOpen = true;

        GyroData = new Quaternion(
                                   BitConverter.ToSingle(args.RawData, 0),
                                   BitConverter.ToSingle(args.RawData, FloatSize),
                                   BitConverter.ToSingle(args.RawData, FloatSize * 2),
                                   BitConverter.ToSingle(args.RawData, FloatSize * 3)
                                   );

    }

    public void OnConencted(object sender, EventArgs args)
    {
        IsIniting = false;
        IsOpen = true;
    }

    public void OnDisconnect(object sendder, ErrorEventArgs args)
    {
        IsOpen = false;

        Init();
    }

    public string FormAddreess(string address, string port)
    {
        return "ws://" + address + ":" + port;
    }

    public string GetLastAdddress()
    {
        return PlayerPrefs.GetString(AddressKey);
    }

    public bool IsReadyToStart()
    {
        return !IsIniting && !IsOpen;
    }

    public void Close()
    {
        IsOpen = false;
        Socket.Close();
    }
}
