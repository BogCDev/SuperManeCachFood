using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectsController : MonoBehaviour
{
    public static UIEffectsController u;

    [SerializeField] private Vector4[] _colors;// green red
    [SerializeField] private List<Image> _images;
    [SerializeField] private Animator _animations;
    private void Start()
    {
        u = GetComponent<UIEffectsController>();
    }
    public void GettingIn(string d)
    {
        if(d == "green")
        {
            foreach(Image el in _images)
            {
                el.color = _colors[0];
            }
        }
        else if(d == "red")
        {
            foreach (Image el in _images)
            {
                el.color = _colors[1];
            }
        }
        _animations.enabled = true;
        _animations.Play("New Animation", -1, 0);
    }
}
