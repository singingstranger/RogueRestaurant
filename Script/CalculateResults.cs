using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Called from GameLoop.cs
/// The results are calculated here and a string is written and added to a list to display in GameLoop.cs.
/// 
/// TODO:
/// Character buffs value can be used to affect the person's outcome ending/story
/// Issues with output from the string assembly - add a styling function perhaps?
/// 
/// </summary>
public class CalculateResults : MonoBehaviour
{
    public int characterBuffResult;
    public List<string> _resultStrings = new List<string>();

    JSONCharacterHandler.StyleBuffs[] _buffAttributesList;
    List<string> _recipesList = new List<string>();
    string ingredients;   
    string _nameOfEatenDish;

    public void ResultForCharacters(GameLoop gameLoop)
    {
        int indexOfPerson = 0;
        
        _buffAttributesList = gameLoop._characterJSONData.buffs;
        foreach (GameObject character in gameLoop._characterArray)
        {
            string thisPersonsIssues = ""; //if this stays empty, it means the character did not get sick
            _resultStrings.Add(character.GetComponent<Character>()._name); //This displays the name over the rest of the results

            //indexOfPerson is used here since the dishes indexes correspond to the Person indexes
            _nameOfEatenDish = gameLoop._nameOfDishes[indexOfPerson];
            JSONDishes.DishAttributesList _dishesAttributesList = gameLoop.dishesAttributes;
            GetDishIngredients(_dishesAttributesList, _nameOfEatenDish); 

            int characterBuffResult = 0; //this isn't currently used at all.
            // for each trait of this charactre
            for (int i = 0; i < character.GetComponent<Character>().trait_names.Length; i++)
            {               
                string eatingProblems = ""; //checked later if it changed. If not, no poisoning with this particular trait
                //for each buff attribute
                for (int j = 0; j<_buffAttributesList.Length; j++)
                {
                    //find the buff that is caled the same thing as the trait of the character
                    if (_buffAttributesList[j].trait_name == character.GetComponent<Character>().trait_names[i])
                    {                    
                        //compare ingredient list from buff with ingredient list from chracter. If it matches, player failed
                        if (ingredients.Contains(_buffAttributesList[j].ingrediant))
                        {
                            eatingProblems += $"{_buffAttributesList[j].ingrediant}, ";

                            characterBuffResult += _buffAttributesList[j].value;                            
                        }                    
                    }
                }
                //after checking each ingredient, if there was an issue, it is written and added to _resultStrigs
                if (eatingProblems != "")
                {
                    thisPersonsIssues = $"They had a bad time from ordering {_nameOfEatenDish} because it contains {eatingProblems}and they are {character.GetComponent<Character>().trait_names[i]}.";
                    _resultStrings.Add(thisPersonsIssues);
                }
                

            }
            // if a person did not trip any flags or issues, they have a win state
            if (thisPersonsIssues == "")
            {
                Debug.Log("This Persons Issues stayed empty");
                _resultStrings.Add($"They ordered {_nameOfEatenDish} and had a lovely dinner. Good job!");
            }
            //Person done, next person.
            Debug.Log($"Character value result: {characterBuffResult}");
            indexOfPerson++;
            _resultStrings.Add(" ");
        }
    }

    //makes a list of ingredients for the specific eaten dish
    void GetDishIngredients(JSONDishes.DishAttributesList dishesAttributesList, string _nameOfEatenDish)
    {
        for (int i = 0; i < dishesAttributesList.menuItems.Length; i++)
        {
            if (_nameOfEatenDish == dishesAttributesList.menuItems[i].dish_name)
            {
                ingredients = dishesAttributesList.menuItems[i].ingrediant_list;
            }           
        }     
    }
}
