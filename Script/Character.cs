using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiated in by CharacterCreator.cs
/// Fields filled in same, using JSONCharacterHandler.cs Info
/// </summary>
public class Character : MonoBehaviour
{
    public string _name;
    public string[] trait_names;
    public string[] ingrediant;
    public int[] value;
}
