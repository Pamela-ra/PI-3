using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] GameObject transparentPanel;

    [SerializeField] bool showTransparentPanel = false;

    [SerializeField] Image charImage;

    [SerializeField] TMP_Text generoText;


    void Start()
    {
        transparentPanel.SetActive(showTransparentPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCharacterPanel(CharacterStats characterStats)
    {
        if (characterStats == null)
        {
            charImage.sprite = null;
            generoText.SetText("");
        }
        else
        {
            charImage.sprite = characterStats.face;
            generoText.SetText(characterStats.charGenero);
        }
    }
}
