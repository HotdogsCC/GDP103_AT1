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
        //runs when the player has clicked enter, instructing them with a math prompt
        if (math)
        {
            answer += Input.inputString; // takes in user input and stores in a string

            // \b is the character for backspace, nullifies string if it has been clicked
            if (answer.Contains("\b"))
            {
                answer = null;
                Instantiate(clearSound);
            }

            //checks the answer is a number, and if it is, check if it is the answer
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

            //display question text and player answer text
            text.text = question + "\n\n" + answer;
        }

        //runs when player has entered the collision zone
        if(gameStarted)
        {
            //moves camera towards world space pc UI
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos.position, Time.deltaTime * 2);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraPos.rotation, Time.deltaTime * 2);
            
            //starts game when player presses enter, plays horror music and sets text on screen to the random numbers
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

    //disables player movement when the player enters the pc collision zone
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

    //waits 4 seconds between player winning and being brought back to title screen
    IEnumerator WaitAMo()
    {
        yield return new WaitForSeconds(4);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync(0);
    }
}
