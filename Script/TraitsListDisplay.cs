using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// In the top left corner, the traits are spawned in at runtime, after being created in CharacterCreator.cs
/// Spawns in the 3D buts according to name currently. 
/// 
/// TODO: 
/// Add on hover function with associated flavor text from description of traits
/// </summary>
public class TraitsListDisplay : MonoBehaviour
{
    [Header("3D Busts")]
    [SerializeField] GameObject[] _3DBusts;
    [SerializeField] Transform _locationSpawn;
    [Header("Corner Display")]
    [SerializeField] TMP_Text _characterNameDisplay;
    [SerializeField] GameObject _traitsPrefab;
    [SerializeField] Transform _traitsParent;
    
    List<GameObject> _traitsList = new List<GameObject>();
    int _indexOfCharacter;

    public void InstantiateNewTraits(GameLoop gameLoop, GameObject character)
    {
        if (_indexOfCharacter > 0)
        {
            _3DBusts[_indexOfCharacter - 1].SetActive(false);
        }
        _3DBusts[_indexOfCharacter].SetActive(true);
        
        _indexOfCharacter++;
        _characterNameDisplay.text = character.GetComponent<Character>()._name;

        //clean the slate from the previous character
        foreach (GameObject trait in _traitsList)
        {
            Destroy(trait);
        }
       //Instantiate the character traits named in the top left corner
        for (int i = 0; i< character.GetComponent<Character>().trait_names.Length; i++)
        {
            GameObject tmp = Instantiate(_traitsPrefab, _traitsParent);
            tmp.GetComponent<TMP_Text>().text = gameLoop._characterJSONData.traits[i].trait_name;
            tmp.GetComponent<TMP_Text>().text = character.GetComponent<Character>().trait_names[i];
            _traitsList.Add(tmp);
        }       
    }
}
