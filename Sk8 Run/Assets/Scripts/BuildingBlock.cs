using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    [SerializeField]
    private BuildingBlockInfo blockInfo;

    public BuildingBlockInfo BlockInfo
    {
        get => blockInfo;
        set => blockInfo = value;
    }

    private bool jumpable;

    private string objectTag = "Block";

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
        jumpable = blockInfo.jumpable;
        gameObject.tag = objectTag;
    }

    public bool isJumpable()
    {
        return jumpable;
    }
}
