using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Writes all Character Info to three corresponding classes:
/// CharacterNames, CharacterEatingStlye and StyleBuffs
/// Buffs are here because their handler would not work.
/// </summary>
public class JSONCharacterHandler : MonoBehaviour
{
    public TextAsset _characterJSON;

    [System.Serializable]
    public class CharacterNames
    {
        public string _nameCharacter;
    }
    [System.Serializable]
    public class CharacterEatingStyle
    {
        public string trait_name;
        public string description;
    }
    [System.Serializable]
    public class StyleBuffs
    {
        public string trait_name;
        public string ingrediant;
        public int value;
    }
    /*public class CharacterEatingStyle
    {
        public string trait_name;
        public string ingrediant;
        public int value;
    }*/
    [System.Serializable]
    public class CharacterAttributesList
    {
        public CharacterNames[] characterName;
        public CharacterEatingStyle[] traits;
        public StyleBuffs[] buffs;
    }
    public CharacterAttributesList _mycharacterAttributesList = new CharacterAttributesList();

    public CharacterAttributesList CharacterJSONList()
    {
        _mycharacterAttributesList = JsonUtility.FromJson<CharacterAttributesList>(_characterJSON.text);
        return _mycharacterAttributesList;
    }
}
