using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace OutGame
{
    public class CharacterCardList : MonoBehaviour
    {
        [SerializeField] private CharacterCard[] characterCardPool;
        [SerializeField] private SpriteAtlas characterAtlas;

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            List<UserCharacterData> userCharData = FBControl.FirebaseManager.Instance.UserData.CharData;

            for (int i = 0; i < characterCardPool.Length; ++i)
            {
                characterCardPool[i].Init(characterAtlas);

                if (i >= userCharData.Count)
                    characterCardPool[i].gameObject.SetActive(false);
                else
                {
                    characterCardPool[i].gameObject.SetActive(true);
                    characterCardPool[i].SetCard(CharacterDataParser.Instance.GetCharacter(userCharData[i].Index));
                }
            }
        }
    }
}