using SocketIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Data_Models
{
    public class Account : MonoBehaviour
    {
        public string name_ = "";
        public string password_ = "";
        public string account_id_;
        public float shared_plat_ = 0;
        public Int32 status_ = 0;
        public Int32 gm_speed_ = 0;
        public DateTime time_creation_;

        public Account()
        {

        }

        public void PopulateFromServer(SocketIOEvent a)
        {
            JSONObject e = a.data["account"];
            name_ = e["name"].str;
            password_ = e["password"].str;
            account_id_ = e["lsAccountId"].str;
            shared_plat_ = e["sharedPlat"].n;
            status_ = (int) e["status"].n;
            gm_speed_ = (int) e["gmSpeed"].n;
            time_creation_ = new DateTime((long) e["timeCreation"].n);
        }

        public void Start()
        {
            
        }

    }
}
