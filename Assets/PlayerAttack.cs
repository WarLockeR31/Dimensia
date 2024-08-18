using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }

    [SerializeField]
    private float coolDown;
    private float counterCD;

    public float CoolDown { get { return coolDown; } }

    [SerializeField]
    private LayerMask enemyLayers;
    public LayerMask EnemyLayers { get { return enemyLayers; } }
}
