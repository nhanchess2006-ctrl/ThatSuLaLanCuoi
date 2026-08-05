using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatformManager : MonoBehaviour
{
    public List<DisappearBlock> blocks = new List<DisappearBlock>();

    public void ActiveAllBlocks()
    {
        foreach (DisappearBlock block in blocks)
        {
            block.StartDisappear();
        }
    }
}