using UnityEngine;

namespace OutGame
{
    public class Stars : MonoBehaviour
    {
        [SerializeField] private GameObject[] startPool;

        public void SetStar(int count)
        {
            for (int i = 0; i < startPool.Length; ++i)
            {
                if (i >= count)
                    startPool[i].gameObject.SetActive(false);
                else
                {
                    startPool[i].gameObject.SetActive(true);
                }
            }
        }
    }
}