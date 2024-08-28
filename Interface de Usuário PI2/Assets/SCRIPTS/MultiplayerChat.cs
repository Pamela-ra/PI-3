using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class MultiplayerChat : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject chatGameObject; // GameObject que contém todos os elementos de chat
    public TMP_Text _messages;
    public TMP_InputField input;
    public TMP_Text username;

    private bool isChatVisible = false;

    // Propriedade pública para verificar se o chat está ativo
    public bool IsChatActive => isChatVisible;

    void Start()
    {
        // Garantir que o GameObject de chat esteja escondido no início
        chatGameObject.SetActive(false);
    }

    void Update()
    {
        // Alternar a visibilidade do GameObject de chat quando a tecla X for pressionada
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleChatVisibility();
        }
    }

    public void ToggleChatVisibility()
    {
        isChatVisible = !isChatVisible;
        chatGameObject.SetActive(isChatVisible);

        // Ativar ou desativar a entrada de texto
        if (isChatVisible)
        {
            input.ActivateInputField();
        }
        else
        {
            input.DeactivateInputField();
        }
    }

    public void CallMessageRPC()
    {
        if (PhotonNetwork.InRoom)
        {
            string message = input.text;
            photonView.RPC("RPC_SendMessage", RpcTarget.All, username.text, message);
            input.text = ""; // Limpar o campo de entrada após o envio da mensagem
        }
        else
        {
            Debug.LogWarning("Cannot send message. Not in a Photon room.");
        }
    }

    [PunRPC]
    public void RPC_SendMessage(string username, string message)
    {
        _messages.text += $"{username}: {message}\n";
    }
}
