using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using System;

public class Login : MonoBehaviour
{
    public SocketIOComponent socket;
    public GameObject SocketObject;
    public GameObject[] Characters;

    public static Login instance;


    public void Awake()
    {
        if (instance)
            DestroyImmediate(SocketObject);
        else
        {
            DontDestroyOnLoad(SocketObject);
            instance = this;
        }
    }


    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.Connect();
        
        socket.On("connected", TryLogin);
    }


    void TryLogin(SocketIOEvent e)
    {
        JSONObject login = new JSONObject();
        login.AddField("username", "test_username");
        login.AddField("password", "test_password");
        socket.Emit("trylogin", login);
    }
    
    private string VectorToJson(Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}""}}", vector.x, vector.y);
    }

    private float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }


}
