using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController instance = null;
    internal static Direction direction = Direction.None;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] internal SkinnedMeshRenderer playerMeshRenderer = null;
    [SerializeField] internal TrackManager activeTackManager = null;
    [SerializeField] internal Animator anim = null;
    [SerializeField] private GameObject startPosition = null;


    internal bool isGameOver = false;
    internal bool isGameStart = false;
    private MeshRenderer activeGate = null;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftRight();
    }

    private void MoveLeftRight()
    {
        if (isGameStart)
        {
            if (direction == Direction.Left)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            else if (direction == Direction.Right)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gate")
        {
            other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<TrackManager>().BurstGate(other.gameObject.transform.position, other.gameObject.GetComponent<MeshRenderer>().material);
            activeGate = other.gameObject.GetComponent<MeshRenderer>();
            activeGate.enabled = false;
            Debug.Log("Touch Gate");
            GameUIHandler.instance.gameScore += 10;
            Debug.Log(GameUIHandler.instance.gameScore);
            
        }
        else if (other.gameObject.tag == "Track")
        {
            activeTackManager = other.gameObject.GetComponent<TrackManager>();
            activeTackManager.ColorChange();
            Debug.Log("touch track");
        }
        else if(other.gameObject.tag =="Border" || other.gameObject.tag == "Wall")
        {
            isGameOver = true;
            anim.SetInteger("Index", Random.Range(1,5));
            moveSpeed = 0f;
            GameUIHandler.instance.GameOver();
        }
    }

    public void ResetStagePlayer()
    {
        transform.position = startPosition.transform.position;
        moveSpeed = 8f;
        anim.SetInteger("Index", 0);
        activeTackManager = ResetTrackPos.instance.track.GetComponent<TrackManager>();
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Gate")
        {
            StartCoroutine(EnableActiveDelay());
        }
    }

    IEnumerator EnableActiveDelay()
    {
        yield return new WaitForSeconds(3f);
        activeGate.enabled = true;
    }



}
