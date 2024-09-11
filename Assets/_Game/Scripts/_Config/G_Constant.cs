using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Constant : MonoBehaviour
{
    // Layer static
    public static LayerMask GROUND_LAYER = LayerMask.GetMask(S_Constant.LAYER_GROUND.Split(","));
    public static LayerMask WALL_LAYER = LayerMask.GetMask(S_Constant.LAYER_WALL.Split(","));
}
