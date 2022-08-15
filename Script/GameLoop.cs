using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// The game manager, so to say.
/// Contains the most important info about itself, 
/// the JSON Handler datas and cals on the next game loop item at the right times.
/// A lot of this is public because other scripts need to reac them with only a reference
/// to this GameLoop itself.
/// </summary>
public class GameLoop :  JSONCharacterHandler
{
    [Header("Things to Manage SetActive() for")] //This stuff is mostly for pacing and visual effects
    [SerializeField] GameObject _Thoughtbubble;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _finalFrame;

    [Header("Things to change at runtime")]
    [SerializeField] GameObject _textFinalPrefab; //Contains the text that is set here on the results screen

    [Header("Needed for referencing")]
    public GameObject JSONHandlers; //All JSON handling Scripts live here

    [HideInInspector] public CharacterAttributesList _characterJSONData;    
    [HideInInspector] public GameObject[] _characterArray;
    [HideInInspector] public List<GameObject> _characterList = new List<GameObject>();

    [HideInInspector] public JSONDishes.DishAttributesList dishesAttributes;
    [HideInInspector] public List<string> _nameOfDishesList = new List<string>();
    [HideInInspector] public string[] _nameOfDishes;

    int _round;
    /// <summary>
    /// Characters are created on Awake. Character JSON creates three
    /// arrays of data that are all stored as _characterJSONData
    /// </summary>
    private void Awake()
    {
        _finalFrame.SetActive(false);
        _characterJSONData = JSONHandlers.GetComponent<JSONCharacterHandler>().CharacterJSONList();
        StartCoroutine(StartDelay());
    }
    //Here might be a good spot for a fade in effect
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        CreateCharacter();
    }
    /// <summary>
    /// //After grabbing the data at Awake and saving it to _characterJSONData, it is time to create the actual characters
    ///The characters are their own Instantiated empty Objects in the Unity scene, each carrying a unique copy of "Character.cs"
    ///_characterList is turned to characterArray for easier work later
    /// </summary>

    void CreateCharacter()
    {
        _characterList = GetComponent<CharacterCreator>().CreateCharacters(_characterJSONData);
        _characterArray = _characterList.ToArray();

        CreateLevel();
    }
    /// <summary>
    /// 
    ///currently empty, later if there are multiple different restaurant options, this is where the
    ///game should decide which one you are at.
    /// </summary>
    void CreateLevel()
    {        
        CreateMenu();
    }
    /// <summary>
    /// Sets active Menu and Thoughtbubble, calls on TraitsListDisplay to visualise and list the
    /// traits of the characters instantiated in CreateCharacter().
    /// Grabs DishesAttributes() from he JSON for Dishes.
    /// It checks which round you are on in your game and, if you finished the last round, it calls
    /// to ShowResults() to end the game.
    /// This is the last of the long passage of function calling function up to this point.
    /// After this, the game loop stops and the player is the one who controls the timing again.
    /// </summary>
    void CreateMenu()
    {
        _Thoughtbubble.SetActive(true);
        _menu.SetActive(true);
        if (_round != 3)
        {
            GetComponent<TraitsListDisplay>().InstantiateNewTraits(this, _characterArray[_round]);

            dishesAttributes = JSONHandlers.GetComponent<JSONDishes>().DishesJSONList();
            GetComponent<CreateMenu>().CreateMenuButtons(this, dishesAttributes);
        }       
        else
        {
            ShowResults();
        }
    }

    /// <summary>
    /// This is called from the MenuItemButton.cs code, which also says which dish it carries.
    /// The game adds the dish to the _nameOfDishesList list for furter usage.
    /// At the end, it calls the NextPerson() function.
    /// </summary>
    /// <param name="nameOfDish"></param>
    public void SelectedThing(string nameOfDish) //Pass through information from something
    {
        Debug.Log("GameLoop says you selected: "+nameOfDish);
        _nameOfDishesList.Add(nameOfDish);
        
        
        NextPerson();
    }
    /// <summary>
    /// Next person is currently a separate funciton because we might want to include multiple slections per character down the road.
    /// </summary>
    void NextPerson()
    {
        _round++;
        CreateMenu();
    }

    /// <summary>
    /// Called from CreateMenu() when last round has been completed.
    /// Calls CalculateResults.cs for results and then instantiates the Text blorbs based
    /// on what is in CalculateResults._resultStrings at this point.
    /// </summary>
    void ShowResults()
    {
        _nameOfDishes = _nameOfDishesList.ToArray();
        GetComponent<CalculateResults>().ResultForCharacters(this);
        _finalFrame.SetActive(true);
        for (int i = 0; i<GetComponent<CalculateResults>()._resultStrings.Count; i++)
        {
            GameObject tmpText = Instantiate(_textFinalPrefab, _finalFrame.transform);
            tmpText.GetComponent<TMP_Text>().text = GetComponent<CalculateResults>()._resultStrings[i];
        }

    }
    /// <summary>
    /// Called from a TMP button through the inspector
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
