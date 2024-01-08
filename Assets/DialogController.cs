using System;
using UnityEngine;

public class DialogController : MonoBehaviour, IDisposable
{
    public virtual void OnDismiss()
    {
        Dispose();
    }

    public void Dispose()
    {
        Destroy(GetComponent<CanvasRenderer>().gameObject);
    }
}