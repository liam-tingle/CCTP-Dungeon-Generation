using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject menu;
    public GameObject start_button;
    public GameObject menu_buttons;
    public GameObject play_buttons;
    public GameObject h2p_back;
    public GameObject custom_buttons;

    public GameObject sub_title;
    public GameObject play_title;

    public GameObject h2p_overlay;
    public GameObject custom_overlay;

    public Animator pannel;

    public GameObject manager;

    public TMP_Text custom_size;

    private void Update()
    {
        custom_size.text = manager.GetComponent<GameManager>().maze_size.ToString();
    }

    public void GameStart()
    {
        pannel.SetBool("inMenu", true);
        start_button.SetActive(false);
        StartCoroutine(DelayMenuButtons());
    }

    public void PlayButton()
    {
        sub_title.SetActive(false);
        menu_buttons.SetActive(false);

        play_title.SetActive(true);
        play_buttons.SetActive(true);
    }

    public void HowToPlay()
    {
        sub_title.SetActive(false);
        menu_buttons.SetActive(false);

        h2p_overlay.SetActive(true);
        h2p_back.SetActive(true);
    }

    public void DungeonSmall()
    {
        manager.GetComponent<GameManager>().maze_size = 8;
        DontDestroyOnLoad(manager);
        SceneManager.LoadScene(1);
    }

    public void DungeonMedium()
    {
        manager.GetComponent<GameManager>().maze_size = 14;
        DontDestroyOnLoad(manager);
        SceneManager.LoadScene(1);
    }

    public void DungeonLarge()
    {
        manager.GetComponent<GameManager>().maze_size = 20;
        DontDestroyOnLoad(manager);
        SceneManager.LoadScene(1);
    }

    public void DungeonCustom()
    {
        play_title.SetActive(false);
        play_buttons.SetActive(false);
        manager.GetComponent<GameManager>().maze_size = 8;
        custom_overlay.SetActive(true);
        custom_buttons.SetActive(true);
        h2p_back.SetActive(true);
    }

    public void PlusOne()
    {
        manager.GetComponent<GameManager>().maze_size += 1;
    }

    public void PlusFive()
    {
        manager.GetComponent<GameManager>().maze_size += 5;
    }

    public void MinusOne()
    {
        if (manager.GetComponent<GameManager>().maze_size >= 6)
        {
            manager.GetComponent<GameManager>().maze_size -= 1;
        }
    }

    public void MinusFive()
    {
        if (manager.GetComponent<GameManager>().maze_size >= 6)
        {
            manager.GetComponent<GameManager>().maze_size -= 5;
        }
    }

    public void PlayCustom()
    {
        if (manager.GetComponent<GameManager>().maze_size >= 5)
        {
            DontDestroyOnLoad(manager);
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        play_title.SetActive(false);
        play_buttons.SetActive(false);
        h2p_overlay.SetActive(false);
        h2p_back.SetActive(false);
        custom_overlay.SetActive(false);
        custom_buttons.SetActive(false);

        pannel.SetBool("inMenu", false);
        start_button.SetActive(true);
    }

    IEnumerator DelayMenuButtons()
    {
        yield return new WaitForSeconds(0.5f);
        sub_title.SetActive(true);
        menu_buttons.SetActive(true);
    }
}
