using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public AudioSource clickSound;
    public AudioSource hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clickSound.isPlaying)
        {
            hoverSound.Play();
        }

        text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverSound.Stop();
        text.color = Color.white;
    }
}
