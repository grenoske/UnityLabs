using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HurtEnemy : MonoBehaviour
    {
        public int damageToGive;
        public GameObject damageEffect;
        public Transform edgePoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
                Instantiate(damageEffect, edgePoint.position, edgePoint.transform.rotation);
            }
        }
    }
}
