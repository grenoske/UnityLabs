using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Services
{
    public class LocationStartPoint : MonoBehaviour
    {
        public GameObject FirstStartPoint;
        public GameObject SecondStartPoint;
        public string NameOfOldArea1;
        public string NameOfOldArea2;
        public void SetPlayerPos(string oldArea)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (oldArea == NameOfOldArea1)
                player.transform.position = FirstStartPoint.transform.position;
            else if(oldArea == NameOfOldArea2)
                player.transform.position = SecondStartPoint.transform.position;   
        }
    }
}
