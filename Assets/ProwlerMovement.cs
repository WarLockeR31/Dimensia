using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProwlerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool targeted;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
