using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script lives on the Button Prefab that spawns in the names of recipes.
/// It relies on the instantiator, CreateMenu.cs for information about which recipe it stands for.
/// On Button Press, it passes information to the GameLoop about which menu item t was. 
/// </summary>
public class MenuItemButton : MonoBehaviour
{
    public string _button_name;
    Button _button;
    GameLoop _gameLoop;
    [SerializeField] 
    TMP_Text _text;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonWasClicked);
    }
    public void GetGameLoopRef(GameLoop gameLoop)
    {
        _gameLoop = gameLoop;
    } 
    public void SetNameOnButtonString()
    {
        _text.text = _button_name;
    }
    void ButtonWasClicked()
    {
        _gameLoop.SelectedThing(_button_name);
    }
}
