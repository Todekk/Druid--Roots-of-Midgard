using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }
}
