using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float destroyAfterSeconds = 5f;
    void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

}
