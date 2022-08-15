using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Writes traits info from JSON to TraitsAttributes
/// </summary>
public class JSONTraitsHandler : MonoBehaviour
{
    public TextAsset _traitsJSON;

    [System.Serializable]
    public class TraitsAttributes
    {
        public string name;
        public string description;
    }

    [System.Serializable]
    public class TraitsAttributesList
    {
        public TraitsAttributes[] traits;
    }
    public TraitsAttributesList _myTraitsAttributesList = new TraitsAttributesList();

    public TraitsAttributesList CharacterJSONList()
    {
        _myTraitsAttributesList = JsonUtility.FromJson<TraitsAttributesList>(_traitsJSON.text);
        return _myTraitsAttributesList;
    }
}


