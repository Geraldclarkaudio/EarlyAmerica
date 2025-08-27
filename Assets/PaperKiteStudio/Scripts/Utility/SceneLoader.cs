using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperKiteStudio.Dangers
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private int _sceneToLoadIndex;
        [SerializeField]
        private UIManager _uiManager;

        //====================================================================================
        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }


        public void LoadScene()
        {
            if (_uiManager == null) // checks for UI manager here because not all UI is persistent. 
            {
                _uiManager = FindObjectOfType<UIManager>();
            }
            //do a fade out THEN load the scene.. 
            _uiManager.FadeOutandLoadScene(_sceneToLoadIndex);
        }
    }
}