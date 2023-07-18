using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatCube : MonoBehaviour
{
    [SerializeField] private GameObject endPos = null;
    [SerializeField] private GameObject target = null;

    private Vector3 repeatPos = new Vector3(0, 0, 0);
    private Vector3 cubePos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        repeatPos = target.transform.position + new Vector3(-10, 0, 0);
        cubePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RepeatCubeAppear();
    }

    private void RepeatCubeAppear()
    {
        repeatPos = target.transform.position + new Vector3(-10, 0, 0);
        cubePos = transform.position;
        if (cubePos.x >= (endPos.transform.position.x))
        {
            Debug.Log("To endpos");
            transform.position = repeatPos;
        }
    }
}
