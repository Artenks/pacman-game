using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseView : MonoBehaviour
{
    public void Show()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
    }
    public void Hide()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }
}
