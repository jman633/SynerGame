using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public Vector3 cameraPos;
    public string levelDescription;
    public string theme;

    private void Awake()
    {
        theme = gameObject.name;
    }
}
