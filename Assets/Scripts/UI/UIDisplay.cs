using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] public uint distanceFromModel = 2;

    //Eventually, we can decide in what direction away from the annotation location should the annotation show
    public Vector3 uiPlacementLocalSpace = new Vector3(1, 1, 0);

    private Camera _worldCamera;

    // Start is called before the first frame update
    void Start()
    {
        _worldCamera = Camera.main;
        this.GetComponent<Canvas>().worldCamera = _worldCamera;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = uiPlacementLocalSpace * distanceFromModel;
        this.transform.LookAt(_worldCamera.transform);
    }
}
