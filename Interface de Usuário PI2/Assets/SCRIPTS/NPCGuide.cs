using UnityEngine;
using UnityEngine.AI;

public class NPCGuide : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public GameObject decisionUI;
    public Transform[] tourPoints; // Pontos de tour
    public NPCChat npcChat; // Referência ao NPCChat
    public GameObject player1; // Referência ao GameObject do jogador 1
    public GameObject player2; // Referência ao GameObject do jogador 2
    public PlayerFollow playerFollow; // Referência ao PlayerFollow
    public NPCControl npcControl; // Referência ao NPCControl

    private Transform playerTransform;
    private bool isTourAccepted = false;
    private bool hasOfferedTour = false;

    private string[][] chatLinesPerPoint; // Linhas de chat para cada ponto

    void Start()
    {
        if (player1 != null && player1.activeInHierarchy)
        {
            playerTransform = player1.transform;
        }
        else if (player2 != null && player2.activeInHierarchy)
        {
            playerTransform = player2.transform;
        }
        else
        {
            Debug.LogError("Jogador não encontrado.");
            return;
        }

        if (npcControl != null)
        {
            npcControl.npcGuide = this; // Define a referência do NPCGuide no NPCControl
        }

        // Configurar linhas de chat para cada ponto
        chatLinesPerPoint = new string[30][];
        chatLinesPerPoint[0] = new string[] { "Olá, eu sou Joe e serei seu guia durante o tour pelo Campus Itajai", "Aqui onde estamos é o pátio principal do Campus, onde os alunos se reúnem para confraternizar" };
        chatLinesPerPoint[1] = new string[] { "No primeiro andar desse bloco temos alguns laboratórios" };
        chatLinesPerPoint[2] = new string[] { "Vamos usar essas escadas para acessar o 1° andar" };
        chatLinesPerPoint[3] = new string[] { "" };
        chatLinesPerPoint[4] = new string[] { "" };
        chatLinesPerPoint[5] = new string[] { "" };
        chatLinesPerPoint[6] = new string[] { "Nesse corredor, temos as salas", "usadas pelos professores para ministrar", "as aulas do curso de Engenharia Elétrica" };
        chatLinesPerPoint[7] = new string[] { "Vamos subir mais um lance de escadas" };
        chatLinesPerPoint[8] = new string[] { "" };
        chatLinesPerPoint[9] = new string[] { "" };
        chatLinesPerPoint[10] = new string[] { "Chegamos ao 2° andar" };
        chatLinesPerPoint[11] = new string[] { "Aqui ficam alguns laboratórios com componentes elétricos", "Tem, também mais algumas salas de aulas" };
        chatLinesPerPoint[12] = new string[] { "Por aqui, acessaremos o 2° andar do outro bloco" };
        chatLinesPerPoint[13] = new string[] { "" };
        chatLinesPerPoint[14] = new string[] { "Agora, estamos na frente da secretaria acadêmica" };
        chatLinesPerPoint[15] = new string[] { "Seguindo nosso tour, chegamos ao espaço destinado para as salas dos professores" };
        chatLinesPerPoint[16] = new string[] { "Vamos usar as escadas externas para acessar o 1° andar" };
        chatLinesPerPoint[17] = new string[] { "" };
        chatLinesPerPoint[18] = new string[] { "" };
        chatLinesPerPoint[19] = new string[] { "Nesse corredor ficam localizados os laboratórios de informática e de desenho técnico" };
        chatLinesPerPoint[20] = new string[] { "" };
        chatLinesPerPoint[21] = new string[] { "" };
        chatLinesPerPoint[22] = new string[] { "Agora, estamos na frente da biblioteca do Campus" };
        chatLinesPerPoint[23] = new string[] { "Vamos continuar nosso tour" };
        chatLinesPerPoint[24] = new string[] { "Estamos descendo essas escadas para acessar o andar térreo do outro bloco" };
        chatLinesPerPoint[25] = new string[] { "" };
        chatLinesPerPoint[26] = new string[] { "Voltamos ao ponto de partida" };
        chatLinesPerPoint[27] = new string[] { "Nesse corredor ficam as salas de aulas destinadas ao uso dos professores", "do ensino médio" };
        chatLinesPerPoint[28] = new string[] { "Vamos a nossa última parada" };
        chatLinesPerPoint[29] = new string[] { "Aqui é o outro ambiente externo do Campus", "Espero que tenha gostado do nosso tour", "Aproveite o nosso Campus" };

        if (!IsVisitor())
        {
            DeactivateTourPoints();
        }
        else
        {
            ActivateTourPoints();
        }
    }

    void Update()
    {
        if (isTourAccepted || playerTransform == null)
        {
            return; // Se o tour foi aceito ou o jogador não está definido, não faz nada
        }

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !hasOfferedTour)
            {
                OnReachedPlayer(); // Oferece o tour quando o NPC alcançar o jogador
            }
        }
    }

    void OnReachedPlayer()
    {
        Debug.Log("OnReachedPlayer chamado");
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();

        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            Debug.Log("NavMeshAgent parado");
        }

        if (animator != null)
        {
            animator.SetInteger("transition", 1);
            Debug.Log("Animação setada para talking");
        }

        if (decisionUI != null)
        {
            decisionUI.SetActive(true);
            Debug.Log("UI de decisão ativada");
        }

        hasOfferedTour = true;
    }

    public void AcceptTour()
    {
        isTourAccepted = true;
        decisionUI.SetActive(false); // Desativa a UI de decisão

        if (playerFollow != null)
        {
            playerFollow.alvo = transform; // Define o NPC como o alvo
        }

        if (npcControl != null)
        {
            npcControl.StartTour(); // Inicia o tour no NPCControl
        }
    }

    public void DeclineTour()
    {
        isTourAccepted = false;
        decisionUI.SetActive(false);

        if (npcControl != null)
        {
            npcControl.StopTour();
        }
    }

    public void EndTour()
    {
        if (npcControl != null)
        {
            npcControl.EndTour();
        }

        Debug.Log("Tour terminado.");
        DeactivateTourPoints();

        if (playerFollow != null)
        {
            playerFollow.alvo = null;
        }
    }

    void ActivateTourPoints()
    {
        foreach (Transform point in tourPoints)
        {
            point.gameObject.SetActive(true);
        }
    }

    void DeactivateTourPoints()
    {
        foreach (Transform point in tourPoints)
        {
            point.gameObject.SetActive(false);
        }
    }

    bool IsVisitor()
    {
        return true; // Lógica para verificar se é um visitante
    }

    public void OnReachTourPoint(int pointIndex)
    {
        if (npcChat != null && pointIndex < chatLinesPerPoint.Length)
        {
            string[] chatLines = chatLinesPerPoint[pointIndex];
            npcChat.StartChat(chatLines); // Passa o array de linhas de chat
        }
        else
        {
            Debug.LogError("NPCChat não atribuído ou índice de ponto de tour inválido.");
        }
    }
}