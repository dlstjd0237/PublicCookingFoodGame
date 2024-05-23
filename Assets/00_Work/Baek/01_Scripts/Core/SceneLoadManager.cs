using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    public void LoadSceneToName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
