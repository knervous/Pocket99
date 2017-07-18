using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.UI;
using Assets.Scripts.Data_Models;
using UnityEngine.EventSystems;
using Assets.Scripts.UI;

public class CharacterSelect : MonoBehaviour {
    //charselect
    

	public GameObject LoginPanel;
	public GameObject CharacterSelectPanel;
	public GameObject CharacterCreatePanel;

    public Player SelectedPlayer;

	public GameObject CButton1;
	public GameObject CButton2;
	public GameObject CButton3;
	public GameObject CButton4;
    public Text CButton1Text;
    public Text CButton2Text;
    public Text CButton3Text;
    public Text CButton4Text;

    public GameObject PopupPanel;
    public Button PopupOkButton;
    public Text PopupPanelText;

    public Text CharInfoName;
    public Text CharInfoLevel;
    public Text CharInfoClass;
    public Text CharInfoZone;

    //Login Panel
    public InputField ServerInput;
    public InputField UserNameInput;
	public InputField PasswordInput;
	public Button LoginButton;
    public Button EnterWorldButton;
    public Button QuitButton;
    public Text LoginStatus;
	public WorldConnect WorldConnection;
	
	public int CharSelected;
	
	public void Init()
	{
        LoginPanel.SetActive(true);
        CharacterSelectPanel.SetActive(false);
        CharacterCreatePanel.SetActive(false);

        CButton1.GetComponent<Button>().onClick.AddListener(delegate { CharacterButtonClicked(0); });
        CButton2.GetComponent<Button>().onClick.AddListener(delegate { CharacterButtonClicked(1); });
        CButton3.GetComponent<Button>().onClick.AddListener(delegate { CharacterButtonClicked(2); });
        CButton4.GetComponent<Button>().onClick.AddListener(delegate { CharacterButtonClicked(3); });

        PopupOkButton.onClick.AddListener(delegate { ClosePanel(); });
        LoginButton.onClick.AddListener(delegate { LoginButtonClicked(); });
        QuitButton.onClick.AddListener(QuitClicked);
        EnterWorldButton.onClick.AddListener(EnterWorldClicked);
    }

    public void OpenPopupPanel(string message)
    {
        PopupPanel.SetActive(true);
        PopupPanelText.text = message;
    }

    public void ClosePanel()
    {
        PopupPanel.SetActive(false);
    }
	
	public void UpdateCharButtonText(int index, string name) 
	{
		if (index == 0)
			CButton1Text.text = name;
		else if (index == 1)
			CButton2Text.text = name;
		else if (index == 2)
			CButton3Text.text = name;
		else if (index == 3)
			CButton4Text.text = name;
	}

	public void ClearCharButtonText() 
	{
		string ClearTo = "Create Character";
		CButton1Text.text = ClearTo;
		CButton2Text.text = ClearTo;
		CButton3Text.text = ClearTo;
		CButton4Text.text = ClearTo;
        CButton1Text.color = Color.white;
        CButton2Text.color = Color.white;
        CButton3Text.color = Color.white;
        CButton4Text.color = Color.white;
    }

	public void LoginButtonClicked()
	{
        WorldConnection.username = UserNameInput.text;
        WorldConnection.password = PasswordInput.text;

        string serverurl = string.Empty;
        serverurl += @"ws://";
        serverurl += ServerInput.text.Length > 0 ? ServerInput.text : ServerInput.placeholder.GetComponent<Text>().text;
        serverurl += @":7000/socket.io/?EIO=4&transport=websocket";
        WorldConnection.TryLogin(serverurl);
    }

    public void BackToLogin()
	{
		CharacterSelectPanel.SetActive(false);	
		CharacterCreatePanel.SetActive(false);	
		LoginPanel.SetActive(true);
		
	}

	public void ToCharList()
	{
        ClearCharButtonText();
        CharacterSelectPanel.SetActive(true);
        CharacterCreatePanel.GetComponent<CharacterCreate>().CharCreatePageOne.SetActive(true);
        CharacterCreatePanel.GetComponent<CharacterCreate>().CharCreatePageTwo.SetActive(false);
        CharacterCreatePanel.GetComponent<CharacterCreate>().CharCreatePageThree.SetActive(false);
        CharacterCreatePanel.SetActive(false);	
		LoginPanel.SetActive(false);

        foreach(var character in WorldConnection.server_player)
        {
            UpdateCharButtonText(WorldConnection.server_player.IndexOf(character), character.name_);
        }
        
        if (WorldConnection.server_player.Count > 0)
        {
            CButton1Text.color = Color.yellow;
            UpdateCharInfo(WorldConnection.server_player[0]);
        }
    } 

	public void QuitClicked()
	{
		WorldConnection.socket.Close();

        WorldConnection.server_account = new Account();
        WorldConnection.server_inventory.Clear();
        WorldConnection.server_player.Clear();

        BackToLogin();
	}
	
	public void DbtnClicked(string param)
	{
		//		Debug.Log("foo " + param);
		switch (CharSelected)
		{
			case 1:
				//WorldConnection.DoDeleteChar(CButton1Text.text);
				break;
			case 2:
				//WorldConnection.DoDeleteChar(CButton2Text.text);
				break;
			case 3:
				//WorldConnection.DoDeleteChar(CButton3Text.text);
				break;
			case 4:
			//	WorldConnection.DoDeleteChar(CButton4Text.text);
			default:
				break;
		}		
	}
	
	public void EnterWorldClicked()
	{

        WorldConnection.DoEnterWorld(CharSelected);
        #region old switch
        
        switch (CharSelected)
		{
			case 1:
				//WorldConnection.curZoneId = 2;
				//WorldConnection.DoEnterWorld(CButton1Text.text);
				break;
			case 2:
				//WorldConnection.curZoneId = 2;
			//	WorldConnection.DoEnterWorld(CButton2Text.text);
				break;
			case 3:
			//	WorldConnection.curZoneId = 2;
			//	WorldConnection.DoEnterWorld(CButton3Text.text);
				break;
			case 4:
			//	WorldConnection.curZoneId = 2;
			//	WorldConnection.DoEnterWorld(CButton4Text.text);
				break;
			default:
				break;
		}
        #endregion
    }

    private void UpdateCharInfo(Player inf_player)
    {
        CharInfoLevel.text = "Level " + inf_player.level_.ToString();
        CharInfoClass.text = Player.GetClassById(inf_player.class_);
        CharInfoName.text = inf_player.name_.ToString();
        CharInfoZone.text = Zones.GetZoneById(inf_player.zone_id_);
    }

    public void CharacterButtonClicked(int index)
    {
        if(!(index + 1 > WorldConnection.server_player.Count))
        {
            CharSelected = index;
            UpdateCharInfo(WorldConnection.server_player[index]);
            CButton1Text.color = Color.white;
            CButton2Text.color = Color.white;
            CButton3Text.color = Color.white;
            CButton4Text.color = Color.white;
            switch (index)
            {
                case 0:
                    CButton1Text.color = Color.yellow;
                    break;
                case 1:
                    CButton2Text.color = Color.yellow;
                    break;
                case 2:
                    CButton3Text.color = Color.yellow;
                    break;
                case 3:
                    CButton4Text.color = Color.yellow;
                    break;
            }
            WorldConnection.SpawnSelected(index);
        }
        else
        {
            CharacterSelectPanel.SetActive(false);
            CharacterCreatePanel.SetActive(true);
            LoginPanel.SetActive(false);
        }
    }

	void Start ()
	{
		Debug.Log("started: " + DateTime.Now);
		Init ();
	}	

}
