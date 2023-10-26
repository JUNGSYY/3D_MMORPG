using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    //public float _speed = 3.0f;
    private PlayerStat _stat;

    private Texture2D _attackIcon;
    private Texture2D _handIcon;
    
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    private bool _stopSkill = false;


    enum CursorType
    {
        None,
        Attack,
        Hand
    }

    private CursorType _cursorType = CursorType.None;
    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
        
        _stat = gameObject.GetComponent<PlayerStat>();
            
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_attackIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                _state =  Define.State.Skill;
                return;
            }
        }
        
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state =  Define.State.Idle;
            return;
        }
        
            //이동
            float moveDist = Math.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        
            //transform.position += dir.normalized * moveDist;
            NavMeshAgent nme = gameObject.GetOrAddComponent<NavMeshAgent>();
            nme.Move(dir.normalized * moveDist);
            
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    _state =  Define.State.Idle;
                return;
            }

            if (dir.magnitude > 0.01f) 
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 50 * Time.deltaTime);
            }
    }

    private void OnMouseEvent(Define.MouseEvent evt)
    {
        if (State ==  Define.State.Die)
            return;

        switch (State)
        {
            case  Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case  Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case  Define.State.Skill:
            {
                if (evt == Define.MouseEvent.PointerUp)
                    _stopSkill = true;
            }
                break;
        }
    }
    private void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycasthit = Physics.Raycast(ray, out hit, 100, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                if (raycasthit)
                {
                    _destPos = hit.point;
                    State =  Define.State.Moving;
                    _stopSkill = false;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        _lockTarget = hit.collider.gameObject;
                    else
                        _lockTarget = null;
                }
                break;
            case Define.MouseEvent.Press :
                if (_lockTarget == null & raycasthit)
                    _destPos = hit.point;
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }


    void OnhitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();

            int damege = Mathf.Max(0, myStat.ATTack - targetStat.Defense);
            Debug.Log("demage : " + damege);
            targetStat.Hp -= damege;
        }
        if (_stopSkill)
        {
            State =  Define.State.Idle;
        }
        else
        {
            State =  Define.State.Skill;
        }
    }

    
    
}