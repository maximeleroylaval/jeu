using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScalePlus();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleMoins();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ScaleMoins();
    }

    private void ScalePlus()
    {
        Vector2 save = button.image.rectTransform.sizeDelta;
        button.image.rectTransform.sizeDelta = new Vector2(save.x + 10, save.y + 5);
    }

    private void ScaleMoins()
    {
        Vector2 save = button.image.rectTransform.sizeDelta;
        button.image.rectTransform.sizeDelta = new Vector2(save.x - 10, save.y - 5);
    }
}
