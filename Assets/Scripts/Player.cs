using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Player : NetworkBehaviour
{
    public float Direction = 0;

    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Position.Value;
    }

    public void OnMovement(InputAction.CallbackContext value){
        float direction = value.ReadValue<float>();
        if(IsLocalPlayer && direction != 0 ){
            if(IsServer){
                Position.Value += direction > 0 ? Vector3.right/10 : Vector3.left/10;
            }else{
                SetPositionServerRpc(direction);
            }
            
        }
        
    }

    [ServerRpc]
    public void SetPositionServerRpc(float direction){
        Position.Value += direction > 0 ? Vector3.right/10 : Vector3.left/10;
    }
}
