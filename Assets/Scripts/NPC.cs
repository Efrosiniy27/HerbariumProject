using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float _Speed = 1.0f;
    public float _amplitude = 5.0f;
    public Animator _animator;
    public Text _textDialog;
    private Transform _transform;
    private Transform _transformSprite;  
    private float _startPosX;
    private float _dir=1;
    private List<string> _Dialog=new List<string>() {" Да пошел ты." , "И не надейся.",};
    private string _endDialog= "Мы с тобой уже все решили";
    private delegate void Motion();
    Motion motion;
    private void Awake()
    {        
        _transform = GetComponent<Transform>();
        _startPosX = _transform.position.x;
        _transformSprite = _animator.gameObject.GetComponent<Transform>();      
    }
    private void Start()
    {
        motion = Go;
        _animator.Play("GoGG");
    }
    void Update()
    {
      if (motion!=null) motion();       
    }
    public void Go()
    {    
        if (ExitBound())
        {
            _dir *= -1;
            _transformSprite.localScale = new Vector3(_dir, 1, 1);            
        }
        _transform.position += new Vector3(_dir, 0, 0) * _Speed * Time.deltaTime;
    }
    public bool ExitBound()
    {
        return (_transform.position.x > (_startPosX + _amplitude)) || (_transform.position.x < (_startPosX - _amplitude));
    }
    public void Ask()
    {        
        motion = null;
        _textDialog.gameObject.SetActive(true);
        _animator.Play("IdleGG");
        if (_Dialog.Count == 0)
        {         
            _textDialog.gameObject.SetActive(false);
            EndAsk();
            return;
        }        
        _textDialog.text = _Dialog[0];
        _Dialog.RemoveAt(0);
    }
    public void EndAsk()
    {
        _textDialog.gameObject.SetActive(true);
        _textDialog.text = _endDialog;
        StartCoroutine(EndAskCor());
    }
    IEnumerator EndAskCor()
    {
        yield return new WaitForSeconds(3.0f);
        _textDialog.gameObject.SetActive(false);        
        motion = Go;
        _animator.Play("GoGG");
    }
}
