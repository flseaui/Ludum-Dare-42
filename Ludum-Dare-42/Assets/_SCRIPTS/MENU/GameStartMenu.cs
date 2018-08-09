using System.Collections;
using ATTRIBUTES;
using DATA;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MENU
{
    [MenuType(MenuTag.GameStartMenu)]
    public class GameStartMenu : Menu
    {
        private bool _loadScene;
        
        [SerializeField] private TextMeshProUGUI _loadingText;
        
        protected override void SwitchToThis()
        {
            if (SceneManager.GetActiveScene().name.Equals("MENU_SCENE"))
            {
                _loadScene = true;
                _loadingText.text = "Loading...";
                StartCoroutine(LoadNewScene("GAME_SCENE"));
            }
        }

        private void Update()
        {
            if (_loadScene) {
                _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, Mathf.PingPong(Time.time, 1));
            }
        }
        
        IEnumerator LoadNewScene(string scene) {

            yield return new WaitForSeconds(3);

            var async = SceneManager.LoadSceneAsync(scene);

            while (!async.isDone) {
                yield return null;
            }

        }
        
    }
}