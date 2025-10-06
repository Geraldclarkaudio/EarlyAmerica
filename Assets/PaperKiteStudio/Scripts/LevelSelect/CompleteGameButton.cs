using LoLSDK;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class CompleteGameButton : MonoBehaviour
    {
        Button thisButton;
        private void Start()
        {
            
            thisButton = GetComponent<Button>();

            thisButton.onClick.AddListener(() => LOLSDK.Instance.CompleteGame());
        }
    }
}