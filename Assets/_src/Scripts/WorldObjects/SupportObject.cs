using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportObject : WorldObject
{
    public override void EnterEffect(PlayerMain player)
    {
        player.PlayerMovement.SetSupportBool(true);
        player.gameObject.transform.parent = transform;
    }

    public override void ExitEffect(PlayerMain player)
    {
        player.PlayerMovement.SetSupportBool(false);
        player.gameObject.transform.parent = null;
    }
}
