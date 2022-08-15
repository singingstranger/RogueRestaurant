using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CharacterCreator.cs uses the raw data from JSONCharacterHandler.cs to make character instances.
/// It assigns the appropriate info to each individual character, randomising their traits and then
/// informing them of the ingredients and values this involves.
/// 
/// TODO:
/// Add exceptions for contradictory/overlapping traits (halal and lent, vegan and vegetarian...)
/// Add more variance for how many traits someone has
/// </summary>
public class CharacterCreator : JSONCharacterHandler
{
    
    [Header("Character Instantiation")]
    [SerializeField] int _numberOfCharacters; //currently 3, can increase
    [SerializeField] GameObject _characterPrefab; 
    [SerializeField] Transform _characterParent;

    List<GameObject> _characters = new List<GameObject>();
    CharacterAttributesList _characterJSONData = new CharacterAttributesList();

    int _numberOfTraitsPerPerson = 3; //Currenlty 3 is the max and only, Can change to SerializField later for testing
    int _possibleNamesNumber;
    int _styleNumber;
    int _selectedIndexes; //needs to be up here for saving the trait's index

    public List<GameObject> CreateCharacters(CharacterAttributesList characterJSONData)
    {
        //Grabbing parsed JSON data and saving number of possible names and traits
        _mycharacterAttributesList = characterJSONData; 
        _possibleNamesNumber = _mycharacterAttributesList.characterName.Length; 
        _styleNumber = _mycharacterAttributesList.traits.Length; 

        //for each number of characters that we want to instantiate
        for (int i = 0; i<_numberOfCharacters; i++)
        {
            //instantiate and name character
            GameObject tmpInstanceOfCharacter = Instantiate(_characterPrefab, _characterParent);
            tmpInstanceOfCharacter.GetComponent<Character>()._name = NameCharacter(i);

            //Assign random traits to character
            List<string> tmp_trait_names = new List<string>();
            for (int j = 0; j<_numberOfTraitsPerPerson; j++)
            {
                string tmpString = EatingStyle();
                if (!tmp_trait_names.Contains(tmpString))//this is checked to avoid duplicates, here it would need to check for notable exceptions (e.g. halal and catholic)
                {
                    tmp_trait_names.Add(tmpString);
                }
            }
            tmpInstanceOfCharacter.GetComponent<Character>().trait_names = tmp_trait_names.ToArray();
            
            _characters.Add(tmpInstanceOfCharacter);                     
        }

        //just a Debug to check if the function is working correctly
        for (int j = 0; j < _numberOfCharacters; j++)
        {
            Debug.Log($"Character created: {_characters[j].GetComponent<Character>()._name}");
            for (int k = 0; k < _characters[j].GetComponent<Character>().trait_names.Length; k++)
            {
                Debug.Log($"They have the eating style of: {_characters[j].GetComponent<Character>().trait_names[k]}");
            }
        }
        return _characters;
    }
    string NameCharacter(int i)
    {
        int _randomIndex = RandomInt(_possibleNamesNumber-1);
        string _name = _mycharacterAttributesList.characterName[i]._nameCharacter;
        return _name;
    }
    string EatingStyle()
    {
        int _randomIndex = RandomInt(_styleNumber - 1);
        string _trait = _mycharacterAttributesList.traits[_randomIndex].trait_name;
        _selectedIndexes = _randomIndex;
        return _trait;
    }
    int RandomInt(int _upperRange)
    {
        int _randomInt = Random.Range(0, _upperRange);
        return _randomInt;
    }
}
