using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GameManager.Instance.Player.GetComponent<PlayerAttack>();
        Physics.IgnoreLayerCollision(11, 8);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if ((playerAttack.EnemyLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("2131");
            other.gameObject.GetComponentInParent<EnemyHP>().TakeDamage(playerAttack.Damage);
        }
    }
}
