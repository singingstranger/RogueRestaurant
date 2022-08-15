using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XML_Test : MonoBehaviour
{
    public TextAsset _testJSON;

    [System.Serializable]
    public class CharacterAttributes
    {
        public string _nameCharacter;
        public int _culture;
        public int _allergy;
    }
    [System.Serializable]
    public class CharacterAttributesList
    {
        public CharacterAttributes[] characterAttributes;
    }

    public CharacterAttributesList _mycharacterAttributesList = new CharacterAttributesList();
    private void Start()
    {
        _mycharacterAttributesList = JsonUtility.FromJson<CharacterAttributesList>(_testJSON.text);
    }    
}
