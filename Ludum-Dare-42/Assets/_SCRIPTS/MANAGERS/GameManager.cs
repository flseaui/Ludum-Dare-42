using MISC;
using UnityEngine;

namespace MANAGERS
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject _elevatorPrefab;

        private void Start()
        {
            StartGame();
        }
        
        private void StartGame()
        {
            Instantiate(_elevatorPrefab);
        }
    }
}