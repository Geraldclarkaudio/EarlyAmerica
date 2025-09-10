using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace PaperKiteStudio.Dangers
{
    public class XYZHUD : MonoBehaviour
    {
        [SerializeField]
        int _coinAmount;
        public UnityEvent _loadSceneEvent;
        private void Start()
        {
            XYZ.onSteal += UpdateCoinAmount;
        }
        private void OnDisable()
        {
            XYZ.onSteal -= UpdateCoinAmount;
        }

        private void UpdateCoinAmount()
        {
            _coinAmount--;
            
            if(_coinAmount <= 0)
            {
                //game over; start over. 
            }
        }
    }
}