using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string _scene;

    public void Play()
    {
        SceneManager.LoadScene(_scene);
    }
}
