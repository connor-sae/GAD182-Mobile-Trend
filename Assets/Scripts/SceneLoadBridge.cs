using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadBridge : MonoBehaviour
{
    public void Load(string scene)
    {
        SceneLoader.Instance.Load(scene);
    }
}
