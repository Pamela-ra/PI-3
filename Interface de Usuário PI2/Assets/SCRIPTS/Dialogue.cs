using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechTxt;
    public string actorName;
    public LayerMask playerLayer;
    public float radious;
    public string _playerTag = "Player";

    private DialogueControl dc;
    public bool _colidindo = false;


    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

public void Update()
    {
        if (_colidindo)
        {
            dc.SecVirtual.SetActive(true);
        }
        else
        {
            dc.SecVirtual.SetActive(false);
        }
        return;
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == _playerTag)
        {
            _colidindo = true;
        }
    }
    void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag == _playerTag)
        {
            _colidindo = false;
        }
    }
}
