using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkManager
{
    private static byte[] data = new byte[1024]; //数据容器
    private static string message = ""; //接收服务器端的消息容器
    private static Socket clientSocket;
    private static Thread receiveThread;

    public static void Connect()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //跟服务器端建立连接
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(AppConst.ipAddress), AppConst.port));

        //创建一个新的线程，用来接收消息
        receiveThread = new Thread(ReceiveMessage);
        receiveThread.Start();
    }

    public static void Disconnect()
    {
        clientSocket.Shutdown(SocketShutdown.Both); //不接收也不发送
        clientSocket.Close(); //释放资源，关闭连接
    }

    public static void Update(float time)
    {
        if (message != null && message != "")
        {
            //chatLable.text += "\n" + message;
            message = ""; //清空消息
        }
    }

    //Send方法
    public static void Send(byte[] data)
    {
        Debug.Log("C2S：" + data.Length);
        clientSocket.Send(data);
    }
    public static void Send(string msg)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
        clientSocket.Send(data);
    }

    //这个线程方法用来循环接收消息
    static void ReceiveMessage()
    {
        while (true)
        {
            if (!clientSocket.Connected) break;

            int length = clientSocket.Receive(data);
            if (length > 0)
            {
                //TheMsg msg2 = ProtobufferTool.Deserialize<TheMsg>(data);
                //message = string.Format("{0}说：{1}", msg2.Name, msg2.Content);
            }
        }
    }

    public static byte[] String2Bytes(string msg) 
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
        return bytes;
    }

    public static string Bytes2String(byte[] bytes)
    {
        string msg = System.Text.Encoding.UTF8.GetString(bytes);
        return msg;
    }

    public static void HHa() 
    {
        Debug.Log("Haha");
    }
}
