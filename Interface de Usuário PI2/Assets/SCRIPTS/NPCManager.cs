using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public GameObject decisionUI;
    public Transform[] tourPoints; // Pontos de tour
    public TextMeshProUGUI chatText;
    public GameObject player1; // Referência ao GameObject do jogador 1
    public GameObject player2; // Referência ao GameObject do jogador 2
    public PlayerFollow playerFollow; // Referência ao PlayerFollow
    public GameObject visitorExperience; // Referência ao GameObject da experiência do visitante

    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private int currentTourPointIndex = 0;
    private bool hasOfferedTour = false;

    private string[][] chatLinesPerPoint; // Linhas de chat para cada ponto
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no GameObject.");
        }

        // Definir qual jogador está ativo na cena
        if (player1 != null && player1.activeInHierarchy)
        {
            playerTransform = player1.transform;
            Debug.Log("Player 1 encontrado.");
        }
        else if (player2 != null && player2.activeInHierarchy)
        {
            playerTransform = player2.transform;
            Debug.Log("Player 2 encontrado.");
        }
        else
        {
            Debug.LogError("Nenhum jogador ativo encontrado.");
            return;
        }

        // Configurar linhas de chat para cada ponto
        chatLinesPerPoint = new string[30][];
        chatLinesPerPoint[0] = new string[] { "Olá, eu sou Joe e serei seu guia durante o tour pelo Campus Itajai", "Aqui onde estamos é o pátio principal do Campus, onde os alunos se reúnem para confraternizar" };
        chatLinesPerPoint[1] = new string[] { "No primeiro andar desse bloco temos alguns laboratórios" };
        chatLinesPerPoint[2] = new string[] { "Vamos usar essas escadas para acessar o 1° andar" };
        chatLinesPerPoint[6] = new string[] { "Nesse corredor, temos as salas", "usadas pelos professores para ministrar", "as aulas do curso de Engenharia Elétrica" };
        chatLinesPerPoint[10] = new string[] { "Chegamos ao 2° andar" };
        chatLinesPerPoint[11] = new string[] { "Aqui ficam alguns laboratórios com componentes elétricos", "Tem, também mais algumas salas de aulas" };
        chatLinesPerPoint[14] = new string[] { "Agora, estamos na frente da secretaria acadêmica" };
        chatLinesPerPoint[15] = new string[] { "Seguindo nosso tour, chegamos ao espaço destinado para as salas dos professores" };
        chatLinesPerPoint[19] = new string[] { "Nesse corredor ficam localizados os laboratórios de informática e de desenho técnico" };
        chatLinesPerPoint[22] = new string[] { "Agora, estamos na frente da biblioteca do Campus" };
        chatLinesPerPoint[26] = new string[] { "Voltamos ao ponto de partida" };
        chatLinesPerPoint[27] = new string[] { "Nesse corredor ficam as salas de aulas destinadas ao uso dos professores", "do ensino médio" };
        chatLinesPerPoint[29] = new string[] { "Aqui é o outro ambiente externo do Campus", "Espero que tenha gostado do nosso tour", "Aproveite o nosso Campus" };

        if (!IsVisitor())
        {
            DeactivateTourPoints();
        }
        else
        {
            ActivateTourPoints();
        }

        navMeshAgent.SetDestination(playerTransform.position);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Verifica a distância ao jogador e exibe o painel de decisão
        if (!hasOfferedTour && distanceToPlayer <= stoppingDistance)
        {
            OfferTour();
        }
    }

    void OfferTour()
    {
        navMeshAgent.isStopped = true; // Para o NPC
        decisionUI.SetActive(true); // Mostra a UI de decisão
        hasOfferedTour = true; // Marca que o tour foi oferecido
        Debug.Log("NPC parou para oferecer o tour.");
    }

    public void AcceptTour()
    {
        Debug.Log("Tour aceito.");
        decisionUI.SetActive(false); // Desativa a UI de decisão

        // Configurar o PlayerFollow para seguir o NPC
        if (playerFollow != null)
        {
            playerFollow.alvo = transform; // Define o NPC como o alvo
            playerFollow.distanciaMinima = 3f; 
        }

        // Inicia o tour e define a animação de caminhar
        navMeshAgent.isStopped = false;
        animator.SetInteger("Transition", 0); // Animação de walking
        StartCoroutine(StartChatAndMove());
    }

    public void DeclineTour()
    {
        Debug.Log("Tour recusado.");
        decisionUI.SetActive(false);
        navMeshAgent.isStopped = true; // Garante que o NPC pare se o tour for recusado
    }

    IEnumerator StartChatAndMove()
    {
        while (currentTourPointIndex < tourPoints.Length)
        {
            // Move para o próximo ponto
            navMeshAgent.SetDestination(tourPoints[currentTourPointIndex].position);
            navMeshAgent.isStopped = false; // Garante que o NPC esteja se movendo
            animator.SetInteger("Transition", 0); // Animação de walking

            // Espera até o NPC chegar ao ponto de tour
            while (navMeshAgent.remainingDistance > stoppingDistance)
            {
                yield return null; // Espera até chegar ao ponto
            }

            // Verifica se há falas para o tour point atual
            if (chatLinesPerPoint[currentTourPointIndex] != null && chatLinesPerPoint[currentTourPointIndex].Length > 0)
            {
                // Para o NPC e exibe as falas do ponto de tour atual
                navMeshAgent.isStopped = true;
                animator.SetInteger("Transition", 1); // Animação de talking
                yield return StartCoroutine(DisplayChat(chatLinesPerPoint[currentTourPointIndex]));
            }

            // Avança para o próximo ponto
            currentTourPointIndex++;
        }

        // Final do tour
        Debug.Log("Tour finalizado.");
        if (visitorExperience != null)
        {
            visitorExperience.SetActive(false); // Desativa o GameObject da experiência do visitante
            Debug.Log("Experiência do visitante desativada.");
        }

        navMeshAgent.isStopped = true; // Para o NPC no final do tour
        animator.SetInteger("Transition", 2); // Volta para animação de idle
    }

    private IEnumerator DisplayChat(string[] lines)
    {
        foreach (string line in lines)
        {
            chatText.text = line;
            Debug.Log($"Exibindo linha de chat: {line}");
            yield return new WaitForSeconds(3.0f); // Exibir cada linha por 3 segundos
        }

        chatText.text = ""; // Limpar o texto após exibir todas as linhas
        Debug.Log("Chat concluído.");
    }

    void ActivateTourPoints()
    {
        foreach (Transform point in tourPoints)
        {
            point.gameObject.SetActive(true);
        }
        Debug.Log("Tour points ativados.");
    }

    void DeactivateTourPoints()
    {
        foreach (Transform point in tourPoints)
        {
            point.gameObject.SetActive(false);
        }
        Debug.Log("Tour points desativados.");
    }

    bool IsVisitor()
    {
        // Lógica para verificar se é um visitante
        return true;
    }

    // Resetar para permitir que o tour seja oferecido novamente
    void ResetTourOffer()
    {
        hasOfferedTour = false;
    }
}
