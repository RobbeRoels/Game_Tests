using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameBuilder : MonoBehaviour {

    public static GameBuilder instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
    private BoardCreator boardCreator;                      //Store a reference to our BoardCreator which will set up the level.
    public float levelStartDelay = 1.3f;							//Delay between each Player turn.
    private Text levelText;                                 //Text to display current level number.
    private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.

    private Board _currentBoard;
    private Board _lastBoard;

    private Dictionary<int, Board> boardList = new Dictionary<int, Board>();

    void Awake()
    {
        if (instance == null)
        {
            MovePlayer.OnExitReached += nextLevel;
            MovePlayer.OnEntranceReached += previousLevel;
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy the already existing instance. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            MovePlayer.OnExitReached += nextLevel;
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardCreator = GetComponent<BoardCreator>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    private void previousLevel()
    {
        if (boardList.ContainsKey(_currentBoard._level - 1))
        {
            _lastBoard = _currentBoard;
            _currentBoard = boardCreator.SetUpLevelFromPrevious(boardList[_currentBoard._level - 1], false);
        }
        else {
            //?? Throw error.?
        }     
    }


    //Initializes the game for each level.
    void InitGame()
    {
        //Get a reference to our image LevelImage by finding it by name.
        levelImage = GameObject.Find("LevelImage");

        //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        //Create the dungeon.
        _currentBoard = boardCreator.SetUpLevel(-1);
       /* GameObject overlayImage = GameObject.Find("OverlayImage");
        switch (_currentBoard._dungeonFeeling) {
            case Board.Feeling.FEAR:
                overlayImage.GetComponent<Image>().color = new Color(1, 0, 0, 0.39f);
                break;
            case Board.Feeling.HOPE:
                overlayImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.39f);
                break;
            case Board.Feeling.LONELYNESS:
                overlayImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.39f);
                break;
            case Board.Feeling.SADNESS:
                overlayImage.GetComponent<Image>().color = new Color(1, 0, 1, 0.39f);
                break;
            case Board.Feeling.SURPRISE:
                overlayImage.GetComponent<Image>().color = new Color(0, 1, 1, 0.39f);
                break;
            case Board.Feeling.HAPPINESS:
            default:
                overlayImage.GetComponent<Image>().color = new Color(1, 1, 0, 0.39f);
                break;
        }*/
        boardList.Add(_currentBoard._level, _currentBoard);

        //Wait for the delay and hide the image overlay
        Invoke("HideLevelImage", levelStartDelay);
    }

    //Hides black image used between levels
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);
    }

    void nextLevel() {
        if (boardList.ContainsKey(_currentBoard._level + 1 ))
        {
            _lastBoard = _currentBoard;
            _currentBoard = boardCreator.SetUpLevelFromPrevious(boardList[_currentBoard._level + 1], true);
        }
        else {
            _lastBoard = _currentBoard;
            _currentBoard = boardCreator.SetUpLevel(_lastBoard._level);
            boardList.Add(_currentBoard._level, _currentBoard);
        }
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
