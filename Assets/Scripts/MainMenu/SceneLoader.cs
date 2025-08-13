using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject TransitionCanvas;
    public float transitionLength = 4;
    public float transitionTime = 2;
    private string requestedScene;

    public static SceneLoader Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void Load(string sceneName)
    {
        requestedScene = sceneName;
        StartCoroutine(Transition());
    }

    public void LoadRandom(string sceneNamePrefix, int sceneCount)
    {
        Load(sceneNamePrefix.ToString() + Random.Range(1, sceneCount + 1));
    }

    private IEnumerator Transition()
    {
        TransitionCanvas.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(requestedScene);
        yield return new WaitForSeconds(transitionLength - transitionTime);
        TransitionCanvas.SetActive(false);
    }

}
