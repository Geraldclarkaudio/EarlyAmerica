using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class DDOL : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}