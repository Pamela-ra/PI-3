using UnityEngine;
using Photon.Pun;

public class InteractiveObject : MonoBehaviourPunCallbacks, IPunObservable
{
    private void Start()
    {
        if (photonView.IsMine)
        {
            // Configurações específicas do jogador local, se necessário
        }
        else
        {
            // Configurações específicas para outros jogadores
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
