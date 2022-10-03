using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobFP : MonoBehaviour
{
    // headbob stuff
    [SerializeField] private GameObject player;
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        //headbob
        _startPos = _camera.localPosition;
        player = GameObject.Find("Player");

        player.GetComponent<PlayerMovementFP>().GetComponent<Rigidbody>().freezeRotation = true;


        player.GetComponent<PlayerMovementFP>()._playervelocity = player.GetComponent<Rigidbody>().velocity;


    }

    private Vector3 FootStepMotion()
    {
        //headbob
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Sin(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }
    private void CheckMotion()
    {
        //headbob
        float speed = new Vector3(player.GetComponent<PlayerMovementFP>()._playervelocity.x, 0, player.GetComponent<PlayerMovementFP>()._playervelocity.z).magnitude;

        if (speed < _toggleSpeed) return;
        if (player.GetComponent<PlayerMovementFP>().isGrounded == false) return;



        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        //headbob
        _camera.localPosition += motion;
    }

    private void ResetPosition()
    {
        //headbob
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        //headbob
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 1.5f, 0), 0.4f);
    }

    private void Update()
    {
        //headbob
        if (!_enable) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
        //hedbob end

    }
}
