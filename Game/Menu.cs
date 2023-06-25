using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update


    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1,LoadSceneMode.Single);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame

}
