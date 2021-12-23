using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : WorldObject
{
    public override void EnterEffect(PlayerMain player)
    {
        player.TriggerPlayerDeath();
    }

    public override void ExitEffect(PlayerMain player)
    {
    }
}
