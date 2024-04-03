using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PanelAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform panel;
    public float animationDuration = 0.5f;
    public Ease easingType = Ease.OutQuart;
    [SerializeField] private float showPositionX = -1000f;
    [SerializeField] private float hidePositionX = -2000f;

    [SerializeField] private UnitSO unit;

    private void Awake()
    {
        // Schowaj panel na starcie
        panel.anchoredPosition = new Vector2(hidePositionX, panel.anchoredPosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Gdy najedziemy na obiekt myszk¹, wysuwamy panel
        panel.DOAnchorPosX(showPositionX, animationDuration).SetEase(easingType);
        FindObjectOfType<UIController>().UpdatePanelStat(unit);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Gdy zjedziemy z obiektu myszk¹, chowamy panel
        panel.DOAnchorPosX(hidePositionX, animationDuration).SetEase(easingType);
    }
}
