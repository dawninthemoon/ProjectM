using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TweenText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;

    public void SetNum( int startNum, int resultNum )
    {
        DOTween.To( 
            ()=>{ return startNum; }, 
            (x)=>{ startNum = x; text.text = string.Format("{0}", x); },
            resultNum, .3f  );
    }
}
