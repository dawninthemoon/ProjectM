using UnityEngine;

public class SpiritListManager : MonoBehaviour
{
    public void Awake()
    {
        // TopUIBackButton.Instance.AddCallback( ()=>{ SceneManager.LoadScene("Lobby"); } );
    }

    public void OnDestroy()
    {
        // TopUIBackButton.Instance.PopCallback();
    }
}