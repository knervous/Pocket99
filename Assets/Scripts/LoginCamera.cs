using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class LoginCamera : MonoBehaviour
    {
        public GameObject LoginScreen;


        void Start()
        {
            gameObject.tag = "MainCamera";
           // var loc = LoginScreen.transform;
            //var bounds = LoginScreen.GetComponent<CanvasRenderer>().transform.cen.bounds;
           // transform.position = new Vector3(bounds.center.x, bounds.center.y, -10);

            //Camera.main.rect = new Rect(0, 0, (bounds.center.y/bounds.center.x), (bounds.center.x/bounds.center.y));
           // Camera.main.pixelRect = new Rect(loc.position.x, loc.position.y, bounds.size.x, bounds.size.y);
        }


    }
}
