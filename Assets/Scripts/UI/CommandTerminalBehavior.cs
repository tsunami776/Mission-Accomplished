using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminalBehavior : MonoBehaviour
{
    // Used to control which command terminal planel is active
    private enum TerminalState { Menu, Tasks, Construction };
    private TerminalState state;

    // The gameobjects for each terminal state that are tuned on and off
    private GameObject[] statePanels;


    // Start is called before the first frame update
    void Start()
    {
        SetState(TerminalState.Menu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Operates the back button
    public void Back()
    {
        if (state == TerminalState.Construction || state == TerminalState.Tasks)
            SetState(TerminalState.Menu);
        else
            Exit();
    }

    // Operates the exit button
    public void Exit()
    {
        gameObject.SetActive(false);
    }

    private void SetState(TerminalState s)
    {
        state = s;

        switch (s)
        {
            case TerminalState.Menu:

                statePanels[0].SetActive(true);
                statePanels[1].SetActive(false);
                statePanels[1].SetActive(false);
                break;
            case TerminalState.Tasks:
                statePanels[0].SetActive(false);
                statePanels[1].SetActive(true);
                statePanels[1].SetActive(false);
                break;
            case TerminalState.Construction:
                statePanels[0].SetActive(false);
                statePanels[1].SetActive(false);
                statePanels[1].SetActive(true);
                break;
        }
    }
}
