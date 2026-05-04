using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;

public class camerascript : NetworkBehaviour
{
      public override void OnNetworkSpawn() //this is for the netcode stuff, if it doesnt work delete this 
    {
        if (!IsOwner)
        {
            
            return;
        }
    }
}
