using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameBuilder : MonoBehaviour {

    public static GameBuilder instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
    private BoardCreator boardCreator;						//Store a reference to our BoardCreator which will set up the level.
    public float levelStartDelay = 1.3f;							//Delay between each Player turn.
    private Text levelText;                                 //Text to display current level number.
    private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.




    void Awake()
    {
        if (instance == null)
        {
            MovePlayer.OnExitReached += nextLevel;
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


    //Initializes the game for each level.
    void InitGame()
    {
        //Get a reference to our image LevelImage by finding it by name.
        levelImage = GameObject.Find("LevelImage");

        //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        //Create the dungeon.
        boardCreator.SetUpLevel();

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
        boardCreator.SetUpLevel();
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
