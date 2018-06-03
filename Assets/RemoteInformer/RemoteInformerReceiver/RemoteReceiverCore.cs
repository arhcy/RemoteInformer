using System;
using System.Threading;
using WebSocketSharp;
using UnityEngine;

namespace artics.RemoteInformer
{
    public class RremoteInfromerReceiver<T> where T : IRemoteInformerMessage, new()
    {
        protected const string AddressKey = "Address";

        public string Address = "";
        public T LastMessage;
        public bool IsOpen;
        public bool IsIniting;

        protected WebSocket Socket;

        #region Initing
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

            LastMessage = new T();

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

        #endregion

        #region WebSocketEvents

        protected void OnMessage(object sender, MessageEventArgs args)
        {
            IsOpen = true;

            T message = new T();
            message.DeserializeMessage(args.RawData);

            LastMessage = message;
        }

        protected void OnConencted(object sender, EventArgs args)
        {
            IsIniting = false;
            IsOpen = true;
        }

        protected void OnDisconnect(object sendder, ErrorEventArgs args)
        {
            IsOpen = false;
            Init();
        }
        #endregion

        #region utils
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
        #endregion

        public void Close()
        {
            IsOpen = false;

            if (Socket != null)
                Socket.Close();
        }
    }
}