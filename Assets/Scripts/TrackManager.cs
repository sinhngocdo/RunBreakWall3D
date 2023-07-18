using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private GameObject targetPos = null;
    [SerializeField] private Transform track_end;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject wallHolder = null;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private ParticleSystem burstEffect = null;
    [SerializeField] internal TrackManager nextTrackManagerRef = null;
    [SerializeField] internal GameObject TrackStartPos = null;


    private Vector3 repeatPos = Vector3.zero;
    private Vector3 track_endPos = Vector3.zero;
    private Vector3 trackPos = Vector3.zero;
    private List<MeshRenderer> wallMeshRenderers = new List<MeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        track_endPos = track_end.position;
        trackPos = transform.position;

        for (int i = 0; i < wallHolder.transform.childCount; i++)
        {
            wallMeshRenderers.Add(wallHolder.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }

        if (PlayerController.instance.activeTackManager == this)
        {
            ColorChange();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RepeatTrack();
    }

    private void RepeatTrack()
    {
        repeatPos = targetPos.transform.position + new Vector3(0, 0, 50);
        if (!PlayerController.instance.isGameOver && PlayerController.instance.isGameStart)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
            trackPos = transform.position;
            if (trackPos.z <= track_endPos.z)
            {
                transform.position = repeatPos;
            }
        }
    }

    internal void ColorChange()
    {
        ChangeColorWall();
        ChangeColorPlayerLikeWall();
    }

    private void ChangeColorWall()
    {
        List<Material> temp = new List<Material>();
        int index = -1;

        foreach (Material m in materials)
        {
            temp.Add(m);
        }

        foreach (MeshRenderer item in wallMeshRenderers)
        {
            index = Random.Range(0, temp.Count);
            item.material = temp[index];
            item.gameObject.tag = "Wall";
            temp.RemoveAt(index);
        }
    }

    private void ChangeColorPlayerLikeWall()
    {
        Debug.Log("Function ChangeColorPlayer called.");
        GameObject gate = null;
        foreach (MeshRenderer item in wallMeshRenderers)
        {
            if(item.gameObject.tag == "Gate")
            {
                gate = item.gameObject;
                PlayerController.instance.playerMeshRenderer.material = gate.GetComponent<MeshRenderer>().material;
                Debug.Log("Gate is here" + item.name);
                break;
            }
        }
        if (gate == null)
        {
            gate = wallMeshRenderers[Random.Range(0, wallMeshRenderers.Count)].gameObject;
            gate.tag = "Gate";
            PlayerController.instance.playerMeshRenderer.material = gate.GetComponent<MeshRenderer>().material;
        }
    }

    internal void BurstGate(Vector3 pos, Material mat)
    {
        burstEffect.transform.position = pos;
        burstEffect.Play();
        burstEffect.GetComponent<ParticleSystemRenderer>().material = mat;
    }
}
