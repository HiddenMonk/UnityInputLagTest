using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Options : MonoBehaviour
{
	public GameObject optionsMenu;
	public Button optionsButton;
	public InputField targetFrameRate;
	public InputField vSyncCount;
	public InputField targetRefreshRate;
	public InputField maxQueuedFrames;
	public InputField mouseSpeed;
	public MouseTranslate mouseTranslate;
	public Toggle useButtonInsteadOfMouseMove;
	public InputField inputKey;
	public InputField FPSFontSize;
	public Text FPSFontText;
	public InputField FrameFontSize;
	public Text FrameFontText;
	public Toggle frameChangeColor;
	public Frame frame;
	public Button save;
	public Button cancel;
	public Button closeGame;

	bool detectingInput;
	KeyCode pressedKey;
	KeyCode tempPressedKey;

	public static Action onApply;

	void Awake()
	{
		if(optionsButton == null) optionsButton = GetComponent<Button>();
		optionsButton.onClick.AddListener(OpenOptions);

		save.onClick.AddListener(Save);
		cancel.onClick.AddListener(CloseOptions);
		closeGame.onClick.AddListener(CloseGame);
	}

	void Start()
	{
		Load();
		CloseOptions();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(optionsMenu.activeSelf)
			{
				CloseOptions();
			}else{
				OpenOptions();
			}
		}

		bool isFocused = inputKey.isFocused;

		if(!detectingInput && isFocused)
		{
			tempPressedKey = mouseTranslate.inputKey;
			detectingInput = true;
			return;
		}

		if(detectingInput)
		{
			if(!isFocused)
			{
				detectingInput = false;
				return;
			}else{
				pressedKey = tempPressedKey;
				inputKey.text = pressedKey.ToString();
			}
		}

		if(isFocused)
		{
			DetectInput();
		}
	}

	void CloseGame()
	{
		Application.Quit();
	}

	void OpenOptions()
	{
		optionsMenu.SetActive(true);
		mouseTranslate.enabled = false;
	}

	void CloseOptions()
	{
		optionsMenu.SetActive(false);
		mouseTranslate.enabled = true;
	}

	void SetFrameRate()
	{
		Application.targetFrameRate = Convert.ToInt32(targetFrameRate.text);
	}
	void UISetFrameRate(int frameRate)
	{
		targetFrameRate.text = frameRate.ToString();
	}

	void SetVSyncCount()
	{
		QualitySettings.vSyncCount = Convert.ToInt32(vSyncCount.text);
	}
	void UISetVSyncCount(int count)
	{
		vSyncCount.text = count.ToString();
	}

	void SetRefreshRate()
	{
		SetRefreshRate(Convert.ToInt32(targetRefreshRate.text));
	}
	void SetRefreshRate(int refreshRate)
	{
		Resolution currentResolution = Screen.currentResolution;
		currentResolution.refreshRate = refreshRate;
		Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen, currentResolution.refreshRate);
	}
	void UISetRefreshRate(int rate)
	{
		targetRefreshRate.text = rate.ToString();
	}

	void SetMaxQueuedFrames()
	{
		QualitySettings.maxQueuedFrames = Convert.ToInt32(maxQueuedFrames.text);
	}
	void UISetMaxQueuedFrames(int max)
	{
		maxQueuedFrames.text = max.ToString();
	}

	void SetMouseSpeed()
	{
		mouseTranslate.mouseSpeed = Convert.ToInt32(mouseSpeed.text);
	}
	void UISetMouseSpeed(int speed)
	{
		mouseSpeed.text = speed.ToString();
	}

	void SetFPSFontSize()
	{
		FPSFontText.fontSize = Convert.ToInt32(FPSFontSize.text);
	}
	void UISetFPSFontSize(int size)
	{
		FPSFontSize.text = size.ToString();
	}

	void SetFrameFontSize()
	{
		FrameFontText.fontSize = Convert.ToInt32(FrameFontSize.text);
	}
	void UISetFrameFontSize(int size)
	{
		FrameFontSize.text = size.ToString();
	}

	void SetFrameChangeColor()
	{
		frame.changeColors = frameChangeColor.isOn;
	}
	void UISetFrameChangeColor(bool isOn)
	{
		frameChangeColor.isOn = isOn;
	}

	void SetMouseButton()
	{
		mouseTranslate.useButtonInsteadOfMouseMove = useButtonInsteadOfMouseMove.isOn;
		mouseTranslate.inputKey = pressedKey;
	}
	void UISetMouseButton(bool useButtonInstead, KeyCode key)
	{
		useButtonInsteadOfMouseMove.isOn = useButtonInstead;
		pressedKey = key;
		inputKey.text = pressedKey.ToString();
	}

	void DetectInput()
	{
		foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			if(Input.GetKeyDown(key)) tempPressedKey = key;
		}
	}

	void Load()
	{
		try
		{
			OptionsXML xml = XMLSerialization.FileToObject<OptionsXML>(GetFilePath());
			if(xml != null)
			{
				UISetFrameRate(xml.targetFrameRate);
				UISetVSyncCount(xml.vSyncCount);
				UISetRefreshRate(xml.targetRefreshRate);
				UISetMaxQueuedFrames(xml.maxQueuedFrames);
				UISetMouseSpeed(xml.mouseSpeed);
				UISetMouseButton(xml.useButtonInsteadOfMouseMove, xml.inputKey);
				UISetFPSFontSize(xml.FPSFontSize);
				UISetFrameFontSize(xml.FrameFontSize);
				UISetFrameChangeColor(xml.frameChangeColor);

				Apply();
			}
		}
		catch(Exception ex)
		{
			Debug.LogError("Failed to Load Options - " + ex.Message);
		}
	}

	void Save()
	{
		Apply();

		try
		{
			SaveToFile();
		}
		catch(Exception ex)
		{
			Debug.LogError("Failed to Save Options - " + ex.Message);
		}

		CloseOptions();
	}

	void Apply()
	{
		SetVSyncCount(); //Might be important to set this before setting the framerate.
		SetFrameRate();
		SetRefreshRate();
		SetMaxQueuedFrames();
		SetMouseSpeed();
		SetFPSFontSize();
		SetFrameFontSize();
		SetFrameChangeColor();
		SetMouseButton();

		if(onApply != null) onApply();
	}

	void SaveToFile()
	{
		OptionsXML xml = new OptionsXML();
		xml.Set(Application.targetFrameRate, QualitySettings.vSyncCount, Screen.currentResolution.refreshRate, QualitySettings.maxQueuedFrames, (int)mouseTranslate.mouseSpeed,
			mouseTranslate.useButtonInsteadOfMouseMove, mouseTranslate.inputKey, FPSFontText.fontSize, FrameFontText.fontSize, frame.changeColors);

		XMLSerialization.ToXMLFile(xml, GetFilePath());
	}

	string GetFilePath()
	{
		return Path.Combine(Application.streamingAssetsPath, "options.xml");
	}

	public class OptionsXML
	{
		public int targetFrameRate;
		public int vSyncCount;
		public int targetRefreshRate;
		public int maxQueuedFrames;
		public int mouseSpeed;
		public bool useButtonInsteadOfMouseMove;
		public KeyCode inputKey;
		public int FPSFontSize;
		public int FrameFontSize;
		public bool frameChangeColor;

		public void Set(int targetFrameRate, int vSyncCount, int targetRefreshRate, int maxQueuedFrames, int mouseSpeed,
			bool useButtonInsteadOfMouseMove, KeyCode inputKey, int FPSFontSize, int FrameFontSize, bool frameChangeColor)
		{
			this.targetFrameRate = targetFrameRate;
			this.vSyncCount = vSyncCount;
			this.targetRefreshRate = targetRefreshRate;
			this.maxQueuedFrames = maxQueuedFrames;
			this.mouseSpeed = mouseSpeed;
			this.useButtonInsteadOfMouseMove = useButtonInsteadOfMouseMove;
			this.inputKey = inputKey;
			this.FPSFontSize = FPSFontSize;
			this.FrameFontSize = FrameFontSize;
			this.frameChangeColor = frameChangeColor;
		}
	}
}