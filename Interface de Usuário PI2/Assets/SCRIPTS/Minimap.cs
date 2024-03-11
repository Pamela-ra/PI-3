using UnityEngine;

public class Minimap : MonoBehaviour
{

    void Update()
    {
        UpdateMinimapPosition();
    }

    void UpdateMinimapPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
