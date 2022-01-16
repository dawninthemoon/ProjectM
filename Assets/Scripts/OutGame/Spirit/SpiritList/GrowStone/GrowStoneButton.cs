using UnityEngine;

namespace OutGame
{
    public class GrowStoneButton : MonoBehaviour
    {
        private int index;

        private GrowStoneArray growStoneArray;

        public void Init(GrowStoneArray growStoneArray, int index)
        {
            this.growStoneArray = growStoneArray;
            this.index = index;
        }

        public void OnClick()
        {
            growStoneArray.AddStone(index, 1);
        }
    }
}