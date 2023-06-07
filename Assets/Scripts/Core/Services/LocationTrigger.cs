using Cinemachine;
using Core.Services.Updater;
using InputReader;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class LocationTrigger : MonoBehaviour
    {
        public string newAreaName;
        public string oldAreaName;
        private int hp;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                hp = other.gameObject.GetComponent<PlayerHealthManager>().playerCurrentHealth;
                // Викликати подію для переходу до нової локації
                LoadNewArea(newAreaName);
            }
        }

        private void LoadNewArea(string areaName)
        {
            // Завантаження нової сцени
            ProjectUpdater.LocationStartPoint = oldAreaName;
            if (GameObject.FindGameObjectWithTag("Dead") != null)
            {
                int BossIndex = SceneManager.GetActiveScene().buildIndex;
                if (ProjectUpdater.DeadBosses == null || !ProjectUpdater.DeadBosses.Contains(BossIndex.ToString()))
                    ProjectUpdater.DeadBosses += BossIndex;
            }
            ProjectUpdater.PlayerHP = hp;
            SceneManager.LoadScene(areaName);
        }
    }
}

