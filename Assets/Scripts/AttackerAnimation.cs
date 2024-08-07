using UnityEngine;

public class AttackerAnimation : MonoBehaviour
{
    [SerializeField] private Attacker attacker;

    public void AttackCol()
    {
        attacker.AttackCol();
    }

    public void AttackStart()
    {
        attacker.AttackStart();
    }
}
