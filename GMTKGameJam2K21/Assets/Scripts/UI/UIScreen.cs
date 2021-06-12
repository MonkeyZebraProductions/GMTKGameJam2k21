using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
    public float canvasGroupFadeOutDuration = 1f;
    public float canvasGroupFadeInDuration = 1f;
    public bool CloseInsteadOfPrevious;
    public bool canReturnToPreviousScreen;

    internal CanvasGroup canvasGroup;

    public virtual void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public virtual void StartScreen()
    {
        canvasGroup.DOFade(1, canvasGroupFadeInDuration);
    }

    public virtual void CloseScreen()
    {
        canvasGroup.DOFade(0, canvasGroupFadeOutDuration);
    }
}