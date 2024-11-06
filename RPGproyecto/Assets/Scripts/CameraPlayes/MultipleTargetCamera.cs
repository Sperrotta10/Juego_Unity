using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;     

    void LateUpdate()
    {
        // Remover targets que son null
        targets.RemoveAll(target => target == null);

        // Si no hay targets válidos, no hacer nada
        if (targets.Count == 0) return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = newPosition;
    }

    Vector3 GetCenterPoint()
    {
        // Si solo hay un target, devolver su posición directamente
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        // Crear un Bounds inicial usando el primer target válido
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        // Encapsular las posiciones de los targets que no sean null
        for (int i = 1; i < targets.Count; i++)
        {
            if (targets[i] != null)  // Verificar si el target no es null
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return bounds.center;
    }
}
