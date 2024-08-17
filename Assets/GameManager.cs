using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;
#endif


public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public enum GameDimension
    {
        _3D,
        _2D
    }

    public enum GameView2D
    {
        TopDown,
        Platformer
    }

    //[Space(10)]

    [Header("GameDimensions")]
    private GameDimension curDimension = GameDimension._3D;
    public GameDimension CurDimension { get { return curDimension; } }

    [SerializeField]
    private GameView2D allowedView2D;


    [Header("Dimensional Anchors")]
    [SerializeField]
    private Transform anchor2D;

    [SerializeField]
    private Transform anchor3D;


    [Header("Entities")]
    [SerializeField]
    private GameObject player;
    private CharacterController characterController;


    [Header("Cameras")]
    [SerializeField]
    private Camera camera3D;

    [SerializeField]
    private Camera camera2D;

    [Header("Inputs")]
    [SerializeField]
    private InputAction jump;


    [Space(10), SerializeField]
    private LayerMask groundLayer;

    [Space(10), SerializeField]
    private InputAction Perspective;

    [Space(10), SerializeField]
    private float EntityHeight;



    private void OnEnable()
    {
        Perspective.Enable();
        Perspective.performed += ChangePerspective;
        Debug.Log("1");
    }

    private void OnDisable()
    {
        Perspective.Disable();
        Perspective.performed -= ChangePerspective;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (curDimension == GameDimension._3D)
        //    Debug.Log(12231);
    }

    private void ChangePerspective(InputAction.CallbackContext context)
    {
        Debug.Log("2");
        if (allowedView2D == GameView2D.TopDown)
        {
            Transform_TopDown3D();
        }

        if (allowedView2D == GameView2D.Platformer)
        {
            Transform_Platformer3D();
        }


        switch (curDimension)
        {
            case GameDimension._3D:
                curDimension = GameDimension._2D;
                break;
            case GameDimension._2D:
                curDimension = GameDimension._3D;
                break;
        }
    }

    private void Transform_TopDown3D()
    {
        if (curDimension == GameDimension._3D)
        {
            characterController.enabled = false;
            Vector3 player_relative_pos = player.transform.position - anchor3D.position;
            characterController.transform.position = new Vector3(player_relative_pos.x + anchor2D.position.x,
                                                                 anchor2D.position.y, 
                                                                 player_relative_pos.z + anchor2D.position.z);
            characterController.enabled = true;

            //player.transform.rotation = Quaternion.identity;


            jump.Disable();


            camera2D.gameObject.SetActive(true);
            camera3D.gameObject.SetActive(false);
        }
        else
        {
            float target_x = anchor3D.position.x - anchor2D.position.x + player.transform.position.x;
            float target_z = anchor3D.position.z - anchor2D.position.z + player.transform.position.z;

            Ray ray = new Ray(new Vector3(target_x, 20, target_z), Vector3.down);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 40, groundLayer))
                Debug.Log("42");
            Debug.DrawRay(new Vector3(target_x, 20, target_z), hit.point, Color.yellow, 10000);

            characterController.enabled = false;
            characterController.transform.position = hit.point;
            characterController.enabled = true;

            jump.Enable();

            camera3D.gameObject.SetActive(true);
            camera2D.gameObject.SetActive(false);
        }
    }

    private void Transform_Platformer3D()
    {
        Debug.Log("NETU");
    }
}
