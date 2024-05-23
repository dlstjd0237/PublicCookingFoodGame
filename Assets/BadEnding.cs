using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BadEnding : MonoBehaviour
{
    public void Ending()
    {
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(0));
    }
}
