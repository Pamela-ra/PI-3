using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerFollow : MonoBehaviour
{
    public float distanciaMinima = 2;
    public Transform alvo;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (alvo == null)
        {
            Debug.LogWarning("Alvo não definido para o PlayerFollow.");
            return;
        }

        float distancia = Vector3.Distance(transform.position, alvo.position);

        if (distancia <= distanciaMinima)
        {
            navMeshAgent.velocity = Vector3.zero; // Parar suavemente
            return;
        }

        navMeshAgent.SetDestination(alvo.position);
    }
}
