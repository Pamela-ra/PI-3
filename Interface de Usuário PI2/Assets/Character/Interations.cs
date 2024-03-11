using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPun
{
    private void Update()
    {
        if (photonView.IsMine)
        {
            // Lógica de interação local
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Execute a interação (envie uma RPC, por exemplo)
                photonView.RPC("Interact", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void Interact()
    {
        // Lógica de interação que será executada em todos os clientes
    }
}
