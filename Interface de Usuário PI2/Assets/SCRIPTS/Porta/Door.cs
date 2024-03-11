using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string _playerTag = "Player";

    public Animator _anima;
    private bool _colidindo = false;
   
    void Update()
    {
        Debug.Log(_colidindo);
        if (_colidindo)
        {
            _anima.SetBool("State", true);
        }
        else
        {
            _anima.SetBool("State", false);
        }
    }
    void OnTriggerEnter(Collider _col)
    {
        if(_col.gameObject.tag == _playerTag)
        {
            _colidindo = true;
        }
    }
     void OnTriggerExit(Collider _col)
    {
        if(_col.gameObject.tag == _playerTag)
        {
            _colidindo = false;
        }
    }

}
