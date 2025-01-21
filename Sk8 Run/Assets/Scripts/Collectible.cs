using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{

    [SerializeField]
    private CollectibleInfo info;

    private string objectTag = "Collectible";

    public string ObjectTag { get => objectTag; set => objectTag = value; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStats()
    {
        gameObject.tag = objectTag;
    }

}
