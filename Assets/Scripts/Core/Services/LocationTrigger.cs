using Cinemachine;
using InputReader;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class LocationTrigger : MonoBehaviour
    {
        public string newAreaName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                // Викликати подію для переходу до нової локації
                LoadNewArea(newAreaName);
            }
        }

        private void LoadNewArea(string areaName)
        {
            // Завантаження нової сцени
            SceneManager.LoadScene(areaName);
        }
    }
}

