using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    public GameObject winMenu;
    // Start is called before the first frame update
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void SetWinMenuActive()
    {
        winMenu.SetActive(true);
    }
}
