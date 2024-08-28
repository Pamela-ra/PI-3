using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkObjectManager : MonoBehaviourPunCallbacks
{
    // Referência ao personagem instanciado
    private GameObject instantiatedCharacter;

    private void Start()
    {
        // Log para monitorar PhotonViews no início da cena
        foreach (PhotonView view in FindObjectsOfType<PhotonView>())
        {
            Debug.Log("PhotonView encontrado com ID: " + view.ViewID);
        }
    }

    public void CreateNetworkObject()
    {
        // Obter o gênero selecionado para decidir qual prefab instanciar
        int selectedGender = PlayerPrefs.GetInt("SelectedGender", 0);
        string prefabName = selectedGender == 0 ? "masculinoCharacterPrefab_2" : "femininoCharacterPrefab_2";

        // Instanciar o objeto de rede usando Photon
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 7f), 0, 0);
        instantiatedCharacter = PhotonNetwork.Instantiate(prefabName, spawnPosition, Quaternion.identity);

        // Definir o nickname do jogador no personagem instanciado
        string selectedNickname = PlayerPrefs.GetString("SelectedNickname", "Player");
        instantiatedCharacter.GetComponentInChildren<TMP_Text>().text = selectedNickname;

        Debug.Log("Objeto de rede criado com PhotonView ID: " + instantiatedCharacter.GetComponent<PhotonView>().ViewID);
    }

    public void DestroyNetworkObject()
    {
        if (instantiatedCharacter != null && instantiatedCharacter.GetComponent<PhotonView>() != null)
        {
            Debug.Log("Destruindo objeto de rede com PhotonView ID: " + instantiatedCharacter.GetComponent<PhotonView>().ViewID);
            PhotonNetwork.Destroy(instantiatedCharacter);
        }
    }
}
