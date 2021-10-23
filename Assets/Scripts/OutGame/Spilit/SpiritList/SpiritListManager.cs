using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiritListManager : MonoBehaviour
{
    public void Awake()
    {
        TopUIBackButton.Instance.AddCallback( ()=>{ SceneManager.LoadScene("Lobby"); } );
    }

    public void OnDestroy()
    {
        TopUIBackButton.Instance.PopCallback();
    }
}
