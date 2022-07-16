using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Button[] orderedBtns;
    [SerializeField]
    Button[] notOrderedBtns;
    int currentIdx = 0;
    bool notCorrect = false;
    [SerializeField]
    GameObject ProcessingPanels;
    [SerializeField]
    GameObject CorrectPanel;
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject processingFirstPanel;
    [SerializeField]
    GameObject[] processPanels;
    int currentProcessIdx = 1;
    [SerializeField]
    GameObject[] Permits;
    int currentPermitIdx = 0;
    int currentCheckListIdx = 0;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject cam1;
    [SerializeField]
    GameObject cam2;
    [System.Serializable]
    struct ChickList
    {
        public List<Toggle> lstToggels;
    };
    [SerializeField]
    ChickList[] chicklists;
    void Start()
    {
    }

    void Update()
    {

    }

    public void SelectProcess(int notOrderedIdx)
    {
        if (notOrderedIdx == currentIdx)
        {
            Debug.Log(currentIdx);
            FindObjectOfType<AudioManager>().PlayeSound("Right");
            orderedBtns[currentIdx].gameObject.SetActive(true);
            notOrderedBtns[currentIdx].GetComponent<Image>().color = Color.green;
            StartCoroutine(Deactivation(notOrderedBtns[currentIdx]));
            notCorrect = false;
            currentIdx++;
        }
        else
        {
            Debug.Log(currentIdx);
            notCorrect = true;
            FindObjectOfType<AudioManager>().PlayeSound("Wrong");
        }
        if (currentIdx >= orderedBtns.Length)
        {
            StartCoroutine(FinishedProcess());
        }
    }
    public void WrongSelected(Button selectedBtn)
    {
        if (notCorrect)
        {
            selectedBtn.GetComponent<Image>().color = Color.red;
            StartCoroutine(ReturnButtonColor(selectedBtn));
        }
    }
    IEnumerator Deactivation(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.gameObject.SetActive(false);
    }
    IEnumerator ReturnButtonColor(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.GetComponent<Image>().color = Color.white;
    }
    IEnumerator FinishedProcess()
    {
        yield return new WaitForSeconds(0.5f);
        ProcessingPanels.SetActive(false);
        CorrectPanel.SetActive(true);
    }
    public void StartBtn()
    {
        startPanel.SetActive(false);
        processingFirstPanel.SetActive(true);
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void NextBtn()
    {
        processPanels[currentProcessIdx - 1].SetActive(false);
        processPanels[currentProcessIdx].SetActive(true);
        currentProcessIdx++;
        Debug.Log(currentProcessIdx);
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void UnderGroundPipe()
    {
        NextBtn();
    }
    public void AboveGroundPipe()
    {
        // let the current idx become on the right part of the process
    }
    public void MechanicalExecavation()
    {
        //Hot Permit
        processPanels[currentProcessIdx - 1].SetActive(false);
        Permits[0].SetActive(true);
        currentPermitIdx = 0;
    }
    public void ManualExecavation()
    {
        //Cold Permit
        processPanels[currentProcessIdx - 1].SetActive(false);
        Permits[1].SetActive(true);
        currentPermitIdx = 1;
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }

    public void DoneTakingPermit()
    {
        Permits[currentPermitIdx].SetActive(false);
        NextBtn();
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void DonePreExecavationLst()
    {
        currentCheckListIdx = 0;
        CheckListToggels();
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void DoneExecavationLst()
    {
        currentCheckListIdx++;
        CheckListToggels();
        //all of this for testing
        processPanels[currentProcessIdx - 1].SetActive(false);
        player.GetComponent<CharacterController>().canMove = true;
        FindObjectOfType<AudioManager>().PlayeSound("Click");
        cam1.SetActive(false);
        cam2.SetActive(true);
    }
    void CheckListToggels()
    {
       // Debug.Log(currentCheckListIdx);
        int checkedNum = 0;
        foreach (var toggle in chicklists[currentCheckListIdx].lstToggels)
        {
            if (toggle.isOn)
            {
                checkedNum++;
            }
        }
        //Debug.Log($"{checkedNum}, {currentCheckListIdx } , {chicklists[currentCheckListIdx].lstToggels.Count}");
        if (checkedNum == chicklists[currentCheckListIdx].lstToggels.Count)
        {
            NextBtn();
        }
        else
        {
            if (currentCheckListIdx>0)
                currentCheckListIdx--;
        }
    }
}
