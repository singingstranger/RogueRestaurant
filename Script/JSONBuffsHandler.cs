using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// writes all Buff info to the Buffs class fields.
/// </summary>
public class JSONBuffsHandler : MonoBehaviour
{
    public TextAsset _buffJSON;

    [System.Serializable]
    public class Buffs
    {
        public string trait_name;
        public string ingrediant;
        public int value;
    }
    [System.Serializable]
    public class BuffsAttributesList
    {
        public Buffs[] buffs;
    }
    public BuffsAttributesList _myBuffAttributesList = new BuffsAttributesList();

    public BuffsAttributesList BuffJSONList()
    {
        _myBuffAttributesList = JsonUtility.FromJson<BuffsAttributesList>(_buffJSON.text);
        return _myBuffAttributesList;
    }
}
