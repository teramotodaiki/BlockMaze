using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
	public float smooth = 0.05f;

    Vector3 ToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Scene View 上での位置を元に、相対位置を決定
        ToPlayer = Player.position - transform.position;
    }

    void FixedUpdate()
    {
        // ぬるぬると近く
        var target = Player.position - ToPlayer;
        transform.position = Vector3.Lerp(transform.position, target, smooth);
    }
}
