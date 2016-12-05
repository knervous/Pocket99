using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

class NetworkMove : MonoBehaviour
    {
        public SocketIOComponent socket;

        public void Start()
        {


        }

        public void OnMove(Vector3 position)
        {
            Debug.Log("sending position to node" + Network.VectorToJson(position));
        socket.Emit("move", new JSONObject(Network.VectorToJson(new Vector3(position.x, position.y, -1))));
        }

    }

