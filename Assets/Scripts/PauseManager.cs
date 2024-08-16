using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool gamePaused = false;
    [SerializeField] private ThirdPersonController player;
    [SerializeField] private Transform targetPos;
    [SerializeField] private GameObject targetIcon;

    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;

    private void Update()
    {
        if (!gamePaused)
        {
            //Pauses game
            if (Input.GetKeyDown(KeyCode.Escape) && FindObjectOfType<TerminalGame>().gameStarted == false)
            {
                player.enabled = false;
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                gamePaused = true;
                resumeButton.SetActive(true);
                quitButton.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                targetIcon = GameObject.FindGameObjectWithTag("targIcon");
                targetIcon.SetActive(false);
            }
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos.position, Time.deltaTime * 5);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetPos.rotation, Time.deltaTime * 5);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        player.enabled = true;
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        gamePaused = false;
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        targetIcon.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
