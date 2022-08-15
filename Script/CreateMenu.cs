using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The menu is an array of child objects in a parent, _menuBlatt. 
/// Each child contains information of its own name.
/// 
/// TODO:
/// Add recipe descriptions
/// for each button destroy button causes error. Solved by adding try catch but there is definitely something wrong with the logic that needs to be fixed later
/// </summary>
public class CreateMenu : MonoBehaviour
{
    [SerializeField] Button _menuItemButton;
    [SerializeField] Transform _menuBlatt;
    [SerializeField] int _numberOfMenuItems;

    List<Button> _instantiatedButtons = new List<Button>();
    List<MenuItemButton> _menuItemButtonRef = new List<MenuItemButton>();
    public void CreateMenuButtons(GameLoop gameLoop, JSONDishes.DishAttributesList dishAttributes)
    {
        foreach (Button button in _instantiatedButtons)
        {
            try
            {
                Destroy(button.gameObject);
            }
            catch
            {
                //I am too tired to figure out why this loop is causing an error message and a try catch solved it
            }
            
        }
        _numberOfMenuItems = dishAttributes.menuItems.Length;
        for (int i = 0; i < _numberOfMenuItems; i++)
        {
            Button tmpButton = Instantiate(_menuItemButton, _menuBlatt); //give button info who I am
            tmpButton.GetComponent<MenuItemButton>().GetGameLoopRef(gameLoop);
            tmpButton.GetComponent<MenuItemButton>()._button_name = dishAttributes.menuItems[i].dish_name;
            tmpButton.GetComponent<MenuItemButton>().SetNameOnButtonString();
            _instantiatedButtons.Add(tmpButton);
            _menuItemButtonRef.Add(tmpButton.gameObject.GetComponent<MenuItemButton>());
        }
    }
}
