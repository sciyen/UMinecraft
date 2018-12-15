using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (gameObject.GetComponent<Button>().interactable == true)
            gameObject.GetComponentInChildren<Text>().color = Color.yellow;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (gameObject.GetComponent<Button>().interactable == true)
            gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
}
