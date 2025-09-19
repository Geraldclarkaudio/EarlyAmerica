using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZ_Spawner_V2 : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _xyzObjects;

        public void Spawn()
        {
            List<GameObject> inactiveAgents = new List<GameObject>();

            foreach (GameObject agent in _xyzObjects) // loop through the xyzObject list 
            {
                if (!agent.activeInHierarchy)
                {
                    inactiveAgents.Add(agent);
                }
            }
            if (inactiveAgents.Count > 0)
            {
                int index = Random.Range(0, inactiveAgents.Count);
                inactiveAgents[index].SetActive(true);
            }
            else
            {
                //Debug.LogWarning("No inactive agents left to spawn.");
            }
        }
    }
}