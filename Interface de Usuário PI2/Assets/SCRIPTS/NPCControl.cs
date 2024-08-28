using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCControl : MonoBehaviour
{
    public NPCGuide npcGuide; // Referência ao NPCGuide

    private NavMeshAgent navMeshAgent;
    private int currentTourPointIndex = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no GameObject.");
        }
    }

    public void StartTour()
    {
        if (npcGuide == null)
        {
            Debug.LogError("NPCGuide não atribuído.");
            return;
        }

        currentTourPointIndex = 0;
        StartCoroutine(HandleTourPoint());
    }

    public void StopTour()
    {
        StopAllCoroutines();
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
        }
    }

    public void EndTour()
    {
        StopTour();
        if (npcGuide != null)
        {
            npcGuide.EndTour(); // Certifique-se de que EndTour está acessível e é público
        }
    }

    private IEnumerator HandleTourPoint()
    {
        if (npcGuide == null)
        {
            yield break;
        }

        while (currentTourPointIndex < npcGuide.tourPoints.Length)
        {
            Transform currentPoint = npcGuide.tourPoints[currentTourPointIndex];
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(currentPoint.position);
                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                {
                    yield return null;
                }
            }
            else
            {
                Debug.LogError("NavMeshAgent não encontrado.");
                yield break;
            }

            // Chegou ao ponto de tour
            npcGuide.OnReachTourPoint(currentTourPointIndex);

            // Avançar para o próximo ponto
            currentTourPointIndex++;
        }
    }
}
