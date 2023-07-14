using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private Transform track_start;
    [SerializeField] private Transform track_end;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject wallHolder = null;
    [SerializeField] private List<Material> materials = new List<Material>(); 



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
        ColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        RepeatTrack();
    }

    private void RepeatTrack()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
        trackPos = transform.position;
        if (trackPos.z <= track_endPos.z)
        {
            transform.position = new Vector3(0, 0, track_start.position.z);
        }
    }

    private void ColorChange()
    {
        List<Material> temp = new List<Material>();
        temp = materials;
        int index = -1;
        foreach (MeshRenderer item in wallMeshRenderers)
        {
            index = Random.Range(0, temp.Count);
            item.material = temp[index];
            temp.RemoveAt(index);
        }
    }
}
