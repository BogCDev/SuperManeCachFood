using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
    [SerializeField] private Text[] _lvlsControllers;
    [HideInInspector] public List<GameObject> _kash;
    public static Quests q;

    [SerializeField] private List<GameObject> _stages;

    [SerializeField] private int StageControlls;

    [SerializeField] private Text _itemController;
    [SerializeField] private int Controller, TargetItem;
    [SerializeField] private GameObject[] questImages;
    [SerializeField] private string[] _textInfo;

    [SerializeField] private List<ParticleSystem> _winParts;

    [SerializeField] private GameObject _gameplayMan, _winMan, QuestController;

    [SerializeField] private Animator _questDashBoardAnim, _winMapAnim, _cameraAnimWin, EndMenu;

    private bool CorutineController;
    private void Start()
    {
        q = GetComponent<Quests>();
        int qc = Random.Range(0, questImages.Length);
        _lvlsControllers[0].text = $"LVL {PlayerPrefs.GetInt("lvl", 0)}";
        _lvlsControllers[1].text = $"LVL {PlayerPrefs.GetInt("lvl", 0) + 1}";
        NewQuaests(qc);
    }
    private void LateUpdate()
    {
        _itemController.text = $"-Collected {Controller}/3";
        if(_kash.Count >= 3 && !CorutineController)
        {
            CorutineController = true;
            StartCoroutine(_cleaneItem());
        }
    }
    private IEnumerator _cleaneItem()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < _kash.Count; i += 1)
        {
            GameObject G = _kash[i];
            _kash.RemoveAt(i);
            Destroy(G);
        }
        CorutineController = false;
    }
    public void NewQuaests(int q)
    {
        _questDashBoardAnim.Play("Quest", -1, 0);

        TargetItem = q;

        foreach (GameObject el in questImages) el.SetActive(false);

        questImages[q].SetActive(true);
    }
    public void AddItem(int controlItem)
    {
        if(controlItem == TargetItem)
        {
            UIEffectsController.u.GettingIn("green");
            Controller += 1;
        }
        else
        {
            UIEffectsController.u.GettingIn("red");
            Controller = 0;
        }

        if(Controller >= 3)
        UpdateStageController();
    }
    public void UpdateStageController()
    {
        _stages[StageControlls].GetComponent<Animator>().enabled = true;
        StageControlls += 1;
        Controller = 0;

        foreach (ParticleSystem el in _winParts) el.Play();

        if(StageControlls >= 3)
        {
            StartCoroutine(OnWinGame());
        }
        else
        {
            NewQuaests(Random.Range(0, questImages.Length));
        }
    }
    public IEnumerator OnWinGame()
    {
        yield return new WaitForSeconds(0.5f);
        _gameplayMan.SetActive(false);
        _winMan.SetActive(true);
        _winMapAnim.enabled = true;
        _cameraAnimWin.enabled = true;
        EndMenu.enabled = true;
        QuestController.SetActive(false);
    }
    public void NextLvl()
    {
        int contr = PlayerPrefs.GetInt("lvl", 0);
        contr += 1;
        PlayerPrefs.SetInt("lvl", contr);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
