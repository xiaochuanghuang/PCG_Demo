using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    // Start is called before the first frame update
    public GameObject pauseMenuUI;

    public InputField input1;
    public InputField input2;
    public InputField input3;

    public GameObject player;
    void Start()
    {

        InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
        submitEvent.AddListener(inputEvent1);
        input1.onEndEdit = submitEvent;
        
        InputField.SubmitEvent submitEvent2 = new InputField.SubmitEvent();
        submitEvent2.AddListener(inputEvent2);
        input2.onEndEdit = submitEvent2;

        InputField.SubmitEvent submitEvent3 = new InputField.SubmitEvent();
        submitEvent3.AddListener(inputEvent3);
        input3.onEndEdit = submitEvent3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void inputEvent1(string input)
    {
        print(input);
        TileSpawner.seed1 = int.Parse(input);
    }

    void inputEvent2(string input)
    {
        TileSpawner.seed2 = int.Parse(input);
    }

    void inputEvent3(string input)
    {
        TileSpawner.seed3 = int.Parse(input);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        player.transform.position = new Vector3(0.0f, 9.5f, 0.0f);
        SceneManager.LoadScene("SampleScene");
        
    }
}
