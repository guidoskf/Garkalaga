using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
