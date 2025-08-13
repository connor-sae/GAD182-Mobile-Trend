using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Menu Item", fileName = "New Menu Item")]
public class MenuItem : ScriptableObject
{
    public new string name;
    public string sceneName;
    public Sprite image;
    public int sceneCount = 1;

    public void Load()
    {
        if (sceneCount <= 1)
            SceneLoader.Instance.Load(sceneName);
        else
            SceneLoader.Instance.LoadRandom(sceneName, sceneCount);
    }
}
