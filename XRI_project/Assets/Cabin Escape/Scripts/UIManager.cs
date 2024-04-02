using SherlockGames.Singleton;
using System;

//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    //UI in VR can be tricky (uh-DUH)
    //it's gotta be away from the camera just a little bit as opposed to being on top of a traditional game's camera
    //canvas is something that can create UI elements (used to be called an atlas) 
    //Atlas. Spelled A-T-L-A-S. I mean, how else would you spell it?
    //"I'm just sleep-deprived, I'm gonna be giggling for the next nine days. Just let me giggle, man." - Ellary

    [Header("UI Options")]
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f; //start with 1 meter away

    [SerializeField]
    private GameObject menuContainer; //this will ref a canvas; we might want multiple canvases to pull from, so it's a GO for now

    private const string GAME_SCENE_NAME = "CabinEscape"; //allows us to restart the game 

    [Header("Events")]
    public Action onGameResumeActionExecuted; //notifies the gameManager to update class 

    //ref to a Menu.cs script
    private Menu menu;

    

    //bind menu buttons
    public void Awake()
    {
        menu = GetComponentInChildren<Menu>(true); //add true in case any part of the menu is hidden at beginning

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuOptions(GameState.Playing); //"What does Invoke do?" - Ellary
            onGameResumeActionExecuted?.Invoke(); //"call... execute... I invoke the 5th... act like a little bitch, i don't know" - John
        });

        menu.RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
            onGameResumeActionExecuted?.Invoke();
        });
    }

    private void OnEnable()
    {
        //todo - add listener for Game Manager changes
        //bind to the GM 
        //GameManager.Instance.OnGamePaused, Resumed, Solved += HandleMenuOptions
    }

    private void OnDisable()
    {
        //todo - add listener for Game Manager changes
    }

    private void HandleMenuOptions(GameState gameState) //make sure these match the Enum values
    {
        //we don't need to look for the Playing state because if we're playing we're not in the menu
        if (gameState == GameState.Paused)
        {
            menuContainer.SetActive(true);
            PlaceMenuInFrontOfPlayer();
        }
        else if(gameState == GameState.PuzzleSolved)
        {
            menuContainer.SetActive(true);
            menu.ResumeButton.gameObject.SetActive(false);
            menu.SolvedText.gameObject.SetActive(true); //"Attaboy! Congrats! Great job!"
            PlaceMenuInFrontOfPlayer(); 
        }
        else
        {
            menuContainer.SetActive(false); //this is basically the Playing state; we don't want the menu while playing
        }
    }
}
