using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{
    public GameObject connectOptionsPanel;
    public GameObject connectWithNamePanel;

    #region Unity Methods
    private void Start()
    {
        connectOptionsPanel.SetActive(true);
        connectWithNamePanel.SetActive(false);
    }
    #endregion

    #region Public Methods

    #endregion
}
