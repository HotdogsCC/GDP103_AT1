using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminalGame : MonoBehaviour
{
    [SerializeField] Transform cameraPos;
    [SerializeField] AudioClip scaryMusic;
    [SerializeField] AudioSource jukebox;
    [SerializeField] GameObject particles;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject clearSound;
    public bool gameStarted = false;
    private bool math = false;

    private int num1;
    private int num2;

    private string question;
    private string answer;
    // Start is called before the first frame update
    void Start()
    {
        num1 = UnityEngine.Random.Range(0, 100);
        num2 = UnityEngine.Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (math)
        {
            answer += Input.inputString;

            if (answer.Contains("\b"))
            {
                answer = null;
                Instantiate(clearSound);
            }

            if(Int32.TryParse(answer, out int num))
            {
                if(num == num1 + num2)
                {
                    Debug.Log("game win");
                    math = false;
                    gameStarted = false;
                    text.text = "You win!";
                    StartCoroutine(WaitAMo());
                    return;
                }
            }
            text.text = question + "\n\n" + answer;
        }

        //starts game
        if(gameStarted)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos.position, Time.deltaTime * 2);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraPos.rotation, Time.deltaTime * 2);
            

            if (Input.GetKeyDown(KeyCode.Return) && !math)
            {
                jukebox.clip = scaryMusic;
                jukebox.Play();
                Instantiate(particles, new Vector3(4.59f, 6.410817f, 9.09f), Quaternion.identity);
                math = true;
                question = "Answer the math question!! \n\n " + num1 + " + " + num2 + "\n\n";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            gameStarted = true;
            FindFirstObjectByType<CharacterController>().gameObject.SetActive(false);
            GameObject temp = GameObject.FindGameObjectWithTag("targIcon");
            temp.SetActive(false);
        }
    }

    IEnumerator WaitAMo()
    {
        yield return new WaitForSeconds(4);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync(0);
    }
}
