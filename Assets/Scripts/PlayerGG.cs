using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGG : MonoBehaviour
{
    public float _Speed = 1.0f;
    public Animator _animator;
    public Text _textDialog;
    private GameObject _itemMotion;
    private Transform _transform;
    private Transform _transformSprite;
    private List<string> _Dialog = new List<string>() { "Слушай, я не хотел ничего воровать, бригадир меня заставил.  ", "Надеюсь мы ещё сможем найти общий язык…  "};
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _transformSprite = _animator.gameObject.GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(-1.0f);
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(1.0f);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E)&&(_itemMotion!=null))
        {
            ShowText();
            _itemMotion.GetComponent<NPC>().Ask();
        }
        _animator.Play("IdleGG");
    }
    public void MovePlayer(float dir)
    {
        _transform.position += new Vector3(dir, 0, 0) * _Speed * Time.deltaTime;
        _transformSprite.localScale = new Vector3(dir, 1, 1);
        _animator.Play("GoGG");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _itemMotion = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _itemMotion = null;
    }
    public void ShowText()
    {
        if (_Dialog.Count == 0)
        {
            _textDialog.gameObject.SetActive(false);
            return;
        }
        _textDialog.gameObject.SetActive(true);
        _textDialog.text = _Dialog[0];
        _Dialog.RemoveAt(0);
    }
}
