using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStuff : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(animator != null )
        {
            animator.SetTrigger("Start"); //plays the loading screen animation
        }
        StartCoroutine(WaitAMo());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("game closed");
    }
    
    //starts loading screen after animation has begun playing

    IEnumerator WaitAMo()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }
}
