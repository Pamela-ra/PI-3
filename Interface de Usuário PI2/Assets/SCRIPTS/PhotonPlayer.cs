using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    public GameObject[] ScriptContainers;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            foreach (var container in ScriptContainers)
            {
                if (container != null)
                {
                    var script = container.GetComponent<MonoBehaviour>();
                    if (script != null)
                    {
                        script.enabled = false;
                    }
                }
            }
        }
    }
}
