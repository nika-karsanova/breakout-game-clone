using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /* Singleton class which deals with all the GUI menus and screens, as well as some button mechanics 
     */
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    private static bool _isPaused;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject failMessage;
    [SerializeField] private GameObject winMessage;
    [SerializeField] private GameObject congratsMessage;


    Regex rx = new Regex(@"^Level\d*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject masterSlider;
    private Slider masterslidercomponent;

    private void Start()
    {
        if (masterSlider != null)
        {
            masterslidercomponent = masterSlider.GetComponent<Slider>();
        }
    }

    private void Update()
    {
        /* Enables Pause menu mechanics
         */
        if(Input.GetKeyDown(KeyCode.Escape) && rx.IsMatch(SceneManager.GetActiveScene().name))
        {
            switch (_isPaused)
            {
                case false:
                    PauseGame();
                    break;
                case true:
                    ResumeGame();
                    break;
            }
        }
    }

    public void DisplayFailScreen()
    {
        Time.timeScale = 0f;
        failMessage.SetActive(true);
    }

    public void DisplayWinScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            DisplayCongratsScreen();
        }
        else
        {
            Time.timeScale = 0f;
            winMessage.SetActive(true);
        }
    }

    public void DisplayCongratsScreen()
    {
        Time.timeScale = 0f;
        congratsMessage.SetActive(true);
    }

    public void ResumeGame()
    {
        /* Resume Game by pressing ESC
         */
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void PauseGame()
    {
        /* Pauses gaming by pressing ESC
         */
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void SceneLoader(String sceneName)
    {
        /* Loads scene (here, mostly levels)
         */
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        /* Quits game
         */
        Application.Quit();
    }

    public void RetryLevel()
    {
        /* Reloads current leve
         */
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void SetVolumeMaster(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SetVolumeMusic(float value)
    {
        audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(value) * 20);
    }

    public void SetVolumeSound(float value)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }

    public void ToggleON()
    {
        /* Turns music on from master
         */
        masterslidercomponent.interactable = true;
        masterslidercomponent.value = 1;
        SetVolumeMaster(1);
    }

    public void ToggleOFF()
    {
        /* Disables music from master
         */
        masterslidercomponent.value = 0.0001f;
        masterslidercomponent.interactable = false;
        SetVolumeMaster(0.0001f);
    }
}