using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Player : NetworkBehaviour
{

    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public Vector3 direction = Vector3.zero;

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsServer){
            Position.Value += direction;
        }else{
            SetPositionServerRpc(direction);
        }   
        transform.position = Position.Value;
    }

    public void OnMovement(InputAction.CallbackContext value){
        if(IsLocalPlayer){
            direction = new Vector3(value.ReadValue<float>()*(speed/100),0,0);
        }
    }

    [ServerRpc]
    public void SetPositionServerRpc(Vector3 direction){
        Position.Value += direction; 
    }
}
