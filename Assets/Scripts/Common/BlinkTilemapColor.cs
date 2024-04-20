using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BlinkTilemapColor : MonoBehaviour
{
    public float Interval;

    public Color Color1;
    public Color Color2;

    private Tilemap _tilemap;

    private bool _isColor1;

    private bool _canBlink = true;
    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _tilemap.color = Color1;
        _isColor1 = true;
    }

    private void Update()
    {
        StartCoroutine(BlinkTilemapCoroutine());
    }

    protected IEnumerator BlinkTilemapCoroutine()
    {
        if (!_canBlink)
            yield break;

        _canBlink = false;

        yield return new WaitForSeconds(Interval);

        _tilemap.color = _isColor1 ? Color2 : Color1;
        _isColor1 = !_isColor1;

        _canBlink = true;
    }
}
