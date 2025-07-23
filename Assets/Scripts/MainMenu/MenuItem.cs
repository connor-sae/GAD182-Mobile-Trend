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
    public Sprite image;
    public string sceneName;
}
