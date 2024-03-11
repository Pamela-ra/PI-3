using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static CharacterList Instance=null;

    [SerializeField] List<CharacterStats> characters = new List<CharacterStats>();

    private int selectedCharIndex;
    public int SelectedCharIndex
    {
        get { return selectedCharIndex; }
        set
        {
            if (value < 0) return;
            if (value >= characters.Count) return;
            selectedCharIndex = value;
            currentCharacter = characters[selectedCharIndex];   
        }
    }
    internal CharacterStats currentCharacter;

    void Awake()
    {
        Instance = this;
    }

   public CharacterStats GetPrevious(){
        var index = SelectedCharIndex - 1;
        if (index < 0) return null;
        return characters[SelectedCharIndex -1];
    }

   public CharacterStats GetNext(){
        var index = SelectedCharIndex + 1;
        if (index >= characters.Count) return null;
        return characters[SelectedCharIndex + 1];

    }
}
