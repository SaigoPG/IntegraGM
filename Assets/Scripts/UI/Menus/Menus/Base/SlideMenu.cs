using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMenu : BaseMenu
{
    protected override void _Awake()
    {

    }

    public enum DisableDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private DisableDirection disableDirection;
    [SerializeField] private int pixelsTransitionAmount;

    private RectTransform rectTransform;
    [SerializeField] private Vector3[] positions = new Vector3[2];

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        positions[0] = rectTransform.position;
        positions[1] = positions[0] + CalculateDirectionVector();
        if (activeOnAwake) rectTransform.position = positions[0];
        else rectTransform.position = positions[1];
    }

    private Vector3 CalculateDirectionVector()
    {
        switch (disableDirection)
        {
            case DisableDirection.Up:
                return Vector2.up * pixelsTransitionAmount;
            case DisableDirection.Down:
                return Vector2.down * pixelsTransitionAmount;
            case DisableDirection.Left:
                return Vector2.left * pixelsTransitionAmount;
            case DisableDirection.Right:
                return Vector2.right * pixelsTransitionAmount;
            default:
                return Vector2.zero * pixelsTransitionAmount;
        }
    }

    protected override void OnFadeOut()
    {
        LeanTween.move(gameObject, positions[1], transitionDuration).setIgnoreTimeScale(true);
    }
    protected override void OnFadeIn()
    {
        LeanTween.move(gameObject, positions[0], transitionDuration).setIgnoreTimeScale(true);
    }
}
