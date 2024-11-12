using UnityEngine;

[CreateAssetMenu(fileName = "PlayerReferences", menuName = "ScriptableObjects/PlayerReferences", order = 1)]
public class PlayerReferences : ScriptableObject
{
    public Transform player1; // Referencia al primer jugador
    public Transform player2; // Referencia al segundo jugador
}
