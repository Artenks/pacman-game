using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int Lives;

    public event Action<int> BeforeLifeRemoved;
    public event Action<int> OnLifeRemoved;

    public void RemoveLife()
    {
        Lives--;
        BeforeLifeRemoved?.Invoke(Lives);
        OnLifeRemoved?.Invoke(Lives);
    }
}
