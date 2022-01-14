using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillCardControl : MonoBehaviour
{
    [SerializeField] private UISkillCard[] uiSkillCards;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private UISkillCard currentSkillCard = null;

    private int activeSkillCount = 0;

    public void Start()
    {
        AddEvent();
    }

    public void SetSkillCard( Data.SkillInfo[] skillInfo )
    {
        activeSkillCount = skillInfo.Length;
        for ( int i =0; i< uiSkillCards.Length;++i)
        {
            if( i >= skillInfo.Length)
            {
                uiSkillCards[i].gameObject.SetActive(false);
            }
            else
            {
                uiSkillCards[i].gameObject.SetActive(true);
            }
        }

        SortSkillCard(activeSkillCount);
    }

    public void AddEvent()
    {
        for (int i = 0; i < uiSkillCards.Length; ++i)
        {
            uiSkillCards[i].Init(i, SelectCard, DeselectCard);
        }
    }

    public void SelectCard( int index )
    {
        currentSkillCard = uiSkillCards[index];
    }

    public void DeselectCard( int index )
    {
        currentSkillCard = null;
        float xDistance = Mathf.Abs(startPosition.x - endPosition.x);
        float x = xDistance / activeSkillCount;
        uiSkillCards[index].transform.localPosition = startPosition + new Vector3(index * x, 0, 0);
    }


    public void Update()
    {
        if( currentSkillCard != null )
        {
            TrackMouse();
        }
    }

    public void TrackMouse()
    {
        currentSkillCard.transform.position = Input.mousePosition;
    }


    public void SortSkillCard( int cardCount )
    {
        float xDistance = Mathf.Abs(startPosition.x - endPosition.x);
        float x = xDistance / cardCount;
        for( int i = 0; i < cardCount; ++i )
        {
            uiSkillCards[i].transform.localPosition = startPosition + new Vector3( i * x, 0, 0);
        }
    }
}
