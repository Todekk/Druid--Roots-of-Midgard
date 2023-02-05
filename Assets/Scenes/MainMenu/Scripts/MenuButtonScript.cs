using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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

        if (text is not null)
        {
            text.color = new Color(66f / 255f, 69f / 255f, 33f / 255f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverSound.Stop();

        if (text is not null)
        {
            SetDefaultMenuButtonTextColor();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (text is not null)
        {
            text.color = Color.black;
        }

        clickSound.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (text is not null)
        {
            SetDefaultMenuButtonTextColor();
        }
    }

    private void SetDefaultMenuButtonTextColor() => text.color = new Color(235f / 255f, 235f / 255f, 235f / 255f);
}
