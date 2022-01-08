using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
	[SerializeField]private GameObject IngameMenu;
	[SerializeField]private GameObject PauseMenu;

	[SerializeField]private Animator FadeAnimator;

    public void PauseGame()
    {
        Time.timeScale = 0;

        IngameMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;

        IngameMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void FadeIn()
    {
    	FadeAnimator.SetTrigger("Fade");
    }
    public void OnFadeComplete()
    {
		Time.timeScale = 1;
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
    	Application.Quit();
    }
}
