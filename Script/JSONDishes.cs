using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Writes Dishes data to DishAttributes.
/// </summary>
public class JSONDishes : MonoBehaviour
{
    public TextAsset _dishesJSON;

    [System.Serializable]
    public class DishAttributes
    {
        public string dish_name;
        public string menu_name;
        public string type;
        public string ingrediant_list;
    }
    [System.Serializable]
    public class DishAttributesList
    {
        public DishAttributes[] menuItems;
    }
    public DishAttributesList _myDishesAttributesList = new DishAttributesList();

    public DishAttributesList DishesJSONList()
    {
        _myDishesAttributesList = JsonUtility.FromJson<DishAttributesList>(_dishesJSON.text);
        return _myDishesAttributesList;
    }

    
}
