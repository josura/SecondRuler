using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class transformExtension {

    public static void Spawn(this Transform transf, Transform Spawnpoint)
    {
        transf.position = Spawnpoint.position;
        transf.rotation = Spawnpoint.rotation;
        transf.gameObject.SetActive(true);
    }

    public static void setY(this Transform t, float newY)
    {
        var pos = t.position;
        pos.y = newY;
        t.position = pos;
    }

    public static void teleport (this Transform t , Vector3 newPos)
    {
        t.position = newPos;
    }
    public static void resetPosition(this Transform t)
    {
        t.position =new Vector3(1,1,1);
    }
}
