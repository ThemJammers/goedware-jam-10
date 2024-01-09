using System;
using TMPro;
using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    private Animation fadeInAnimation;
    private TextMeshPro tmp;
    private SpriteRenderer backgroundSpriteRenderer;

    private bool _isVisible;

    public bool IsVisible => _isVisible;

    public void Start()
    {
        fadeInAnimation = GetComponent<Animation>();
        gameObject.SetActive(false);
    }

    public bool Show()
    {
        gameObject.SetActive(true);
        _isVisible = true;
        fadeInAnimation.Play(PlayMode.StopAll);
        return true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _isVisible = false;
    }
}