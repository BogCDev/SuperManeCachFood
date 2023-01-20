using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLogics : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] private float MovedLerpSpeed;
    [Range(0f, 100f)] [SerializeField] private float KDController;
    [Range(0f, 100f)] [SerializeField] private float Force;

    [SerializeField] private Transform Map;
    [SerializeField] private Animator _animations;
    [SerializeField] private GameObject IKPoint, ParntForItem;
    [SerializeField] private ParticleSystem EffectSelection;

    private bool isProcessingPut;
    private Transform _target;
    private void Update()
    {
        CheckTarget();
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    private void CheckTarget()
    {
        if (_target && !isProcessingPut)
        {
            IKPoint.transform.position = Vector3.Lerp(IKPoint.transform.position, _target.transform.position, MovedLerpSpeed * Time.deltaTime);
            IKPoint.transform.LookAt(_target.transform.position);
            if(Vector3.Distance(_target.transform.position, IKPoint.transform.position) < 0.6f)
            {
                _target.GetComponent<Rigidbody>().isKinematic = true;
                _target.transform.SetParent(ParntForItem.transform);
                _target.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(PutItem());
            }
        }
    }
    private IEnumerator PutItem()
    {
        isProcessingPut = true;
        Instantiate(EffectSelection, _target.transform.position, Quaternion.identity);
        _animations.SetBool("G", true);
        yield return new WaitForSeconds(KDController);
        if(_target != null)
        {
            _target.transform.SetParent(Map.transform);
            _target.GetComponent<Rigidbody>().isKinematic = false;
            _target.GetComponent<Rigidbody>().AddForce(_target.transform.forward * Force, ForceMode.Impulse);
            _target = null;
        }
        _animations.SetBool("G", false);
        isProcessingPut = false;
    }
    public void TransferPurposeObject(Transform _target)
    {
        this._target = _target;
    }
}