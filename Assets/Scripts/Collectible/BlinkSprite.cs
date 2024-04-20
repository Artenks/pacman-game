using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkSprite : MonoBehaviour
{
    public float Interval;

    private SpriteRenderer _spriteRenderer;

    private bool _canBlink = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = true;
    }

    private void Update()
    {
        StartCoroutine(BlinkSpriteCoroutine());
    }

    protected IEnumerator BlinkSpriteCoroutine()
    {
        if (!_canBlink)
            yield break;

        _canBlink = false;

        yield return new WaitForSeconds(Interval);

        _spriteRenderer.enabled = !_spriteRenderer.enabled;

        _canBlink = true;
    }

}
