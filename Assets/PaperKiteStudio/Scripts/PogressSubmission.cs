using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
public class PogressSubmission : MonoBehaviour
{
    public void WinTheGame()
    {
        LOLSDK.Instance.SubmitProgress(1, 8, 8);
    }
}
