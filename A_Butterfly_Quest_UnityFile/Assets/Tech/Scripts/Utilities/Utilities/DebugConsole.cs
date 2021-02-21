using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    public static DebugConsole instance;

    public Color consoleColor;

    List<string> availableMethodsNames = new List<string>();
    MethodInfo[] availableMethods;

    string currentCommand;

    bool openedConsole;

    Text m_fieldText;
    InputField m_inputField;

    Text m_historyText;

    Text m_suggestionText;
    int suggestedCommandIndex;

    GameObject parentConsole;


    private void Awake()
    {
        instance = this;

        CreateUIConsole();

        //store available methods
        BindingFlags flags = (BindingFlags.Static | BindingFlags.Public);
        MethodInfo[] methods = typeof(DebugConsole).GetMethods(flags);

        availableMethods = methods;

        //Update methods
        for (int i = 0; i < methods.Length; i++)
        {
            availableMethodsNames.Add(methods[i].Name.ToUpper());
        }

    }

    private void Start()
    {
        m_inputField.onValueChanged.AddListener(delegate { MakeCommandSuggestion(); });
        ShowConsole(false);
    }

    private protected void CreateUIConsole()
    {
        //Canvas
        GameObject consoleCanvas = new GameObject();
        consoleCanvas.transform.SetParent(this.transform);
        consoleCanvas.name = "DEBUG_Canvas";
        RectTransform rt_consolecanvas = consoleCanvas.AddComponent<RectTransform>();
        Canvas cv_consolecanvas = consoleCanvas.AddComponent<Canvas>();
        cv_consolecanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        cv_consolecanvas.sortingOrder = 100;
        CanvasScaler cs_consolecanvas = consoleCanvas.AddComponent<CanvasScaler>();
        cs_consolecanvas.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        GraphicRaycaster gr_consolecanvas = consoleCanvas.AddComponent<GraphicRaycaster>();

        //Background
        parentConsole = new GameObject();
        parentConsole.transform.parent = consoleCanvas.transform;
        parentConsole.name = "DEBUG_Console";
        RectTransform rt_bgplane = parentConsole.AddComponent<RectTransform>();
        CanvasRenderer cr_bgplane = parentConsole.AddComponent<CanvasRenderer>();
        Image im_bgplane = parentConsole.AddComponent<Image>();
        im_bgplane.color = consoleColor;
        rt_bgplane.anchorMin = new Vector2(0.5f, 1);
        rt_bgplane.anchorMax = new Vector2(0.5f, 1);
        rt_bgplane.pivot = new Vector2(0.5f, 1);
        rt_bgplane.sizeDelta = new Vector2(1000, 165);
        rt_bgplane.anchoredPosition = new Vector2(0, 0);

        //Input field
        GameObject inputfield = new GameObject();
        inputfield.transform.parent = parentConsole.transform;
        inputfield.name = "InputField";
        RectTransform rt_inputfield = inputfield.AddComponent<RectTransform>();
        CanvasRenderer cr_inputfield = inputfield.AddComponent<CanvasRenderer>();
        Image im_inputfield = inputfield.AddComponent<Image>();
        im_inputfield.color = consoleColor;
        rt_inputfield.sizeDelta = new Vector2(800, 30);
        rt_inputfield.anchoredPosition = new Vector2(0, -65);
        m_inputField = inputfield.AddComponent<InputField>();
        GameObject commandText = new GameObject();
        commandText.transform.parent = inputfield.transform;
        RectTransform rt_commandText = commandText.AddComponent<RectTransform>();
        rt_commandText.pivot = new Vector2(0.5f, 0.5f);
        rt_commandText.anchoredPosition = new Vector2(0, 0);
        rt_commandText.sizeDelta = new Vector2(780, 35);
        commandText.AddComponent<CanvasRenderer>();
        m_fieldText = commandText.AddComponent<Text>();
        m_fieldText.supportRichText = false;
        m_fieldText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        m_fieldText.alignment = TextAnchor.MiddleLeft;
        m_inputField.textComponent = m_fieldText;

        //History text
        GameObject historyText = new GameObject();
        historyText.transform.SetParent(parentConsole.transform);
        historyText.name = "History_Text";
        RectTransform rt_historyText = historyText.AddComponent<RectTransform>();
        historyText.AddComponent<CanvasRenderer>();
        m_historyText = historyText.AddComponent<Text>();
        m_historyText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        m_historyText.alignment = TextAnchor.LowerLeft;
        m_historyText.verticalOverflow = VerticalWrapMode.Overflow;
        Color.RGBToHSV(consoleColor, out float colorH, out float colorS, out float colorV);
        Color textcolor = Color.HSVToRGB(colorH, colorS, colorV * .5f);
        m_historyText.color = new Color(consoleColor.r * .7f, consoleColor.g * .7f, consoleColor.b * .7f, .8f);
        rt_historyText.anchorMin = new Vector2(0.5f, 1);
        rt_historyText.anchorMax = new Vector2(0.5f, 1);
        rt_historyText.pivot = new Vector2(0.5f, 1);
        rt_historyText.sizeDelta = new Vector2(780, 120);
        rt_historyText.anchoredPosition = new Vector2(0, 0);


        //Suggestion text
        GameObject suggestionText = new GameObject();
        suggestionText.transform.SetParent(parentConsole.transform);
        suggestionText.name = "Suggestion_Text";
        RectTransform rt_suggestionText = suggestionText.AddComponent<RectTransform>();
        suggestionText.AddComponent<CanvasRenderer>();
        m_suggestionText = suggestionText.AddComponent<Text>();
        m_suggestionText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        m_suggestionText.alignment = TextAnchor.MiddleRight;
        m_suggestionText.fontStyle = FontStyle.Italic;
        m_suggestionText.color = new Color(.5f, .8f, .7f);
        rt_suggestionText.anchorMin = new Vector2(1, .5f);
        rt_suggestionText.anchorMax = new Vector2(1, .5f);
        rt_suggestionText.pivot = new Vector2(1, .5f);
        rt_suggestionText.sizeDelta = new Vector2(350, 20);
        rt_suggestionText.anchoredPosition = new Vector2(-115, -65);

        //

    }


    private void Update()
    {
        if (openedConsole)
        {
            if (Input.GetKeyDown(KeyCode.Return) && m_fieldText.text != "")
            {
                CheckCommand();
            }
            if (Input.GetKeyDown(KeyCode.Tab) && m_suggestionText.text != "")
            {
                m_inputField.text = availableMethodsNames[suggestedCommandIndex].ToLower();
                m_inputField.caretPosition = m_inputField.text.Length;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (openedConsole)
            {
                ShowConsole(false);
            }
            else
            {
                ShowConsole(true);
            }
               
        }
    }

    private protected void ShowConsole(bool show)
    {
        if (show)
        {
            parentConsole.SetActive(true);
            m_inputField.ActivateInputField();
            openedConsole = true;

            //if we're opening the console for the first time
            if (m_historyText.text == "")
            {
                m_historyText.text = "Welcome, Type <color=cyan>help</color> to get a list of the available commands";
            }
        }
        else
        {
            openedConsole = false;
            parentConsole.SetActive(false);
        }
    }

    private protected void CheckCommand()
    {

        //get command
        currentCommand = m_fieldText.text;

        //Process entered command
        string[] processedCommand = currentCommand.Split(' ', ',');

        //Check if the method is correct
        if (availableMethodsNames.Contains(processedCommand[0], StringComparer.OrdinalIgnoreCase))
        {
            //Get the index of the method
            int methodIndex = availableMethodsNames.IndexOf(processedCommand[0].ToUpper());

            //Get the required parameters
            ParameterInfo[] requiredParams = availableMethods[methodIndex].GetParameters();//new object[availableMethods[methodIndex].GetParameters().Length];

            for (int i = 0; i < availableMethods[methodIndex].GetParameters().Length; i++)
            {
                requiredParams[i] = availableMethods[methodIndex].GetParameters()[i];
            }

            //Check if passed params are valid
            object[] passedParams = new object[requiredParams.Length];

            if (requiredParams.Length > 0)
            {
                //Check if the parameters are correct
                if (processedCommand.Length - 1 >= requiredParams.Length)
                {
                    int c = 0; //offset for Types that need multiple inputs

                    for (int i = 0; i < requiredParams.Length; i++)
                    {
                        if (requiredParams[i].ParameterType == typeof(String))
                        {
                            passedParams[i] = processedCommand[i + 1 + c];
                        }
                        else if (requiredParams[i].ParameterType == typeof(float))
                        {
                            if (float.TryParse(processedCommand[i + 1 + c], out float parsedValue))
                            {
                                passedParams[i] = parsedValue;
                            }
                        }
                        else if (requiredParams[i].ParameterType == typeof(int))
                        {
                            if (int.TryParse(processedCommand[i + 1 + c], out int parsedValue))
                            {
                                passedParams[i] = parsedValue;
                            }
                        }
                        else if (requiredParams[i].ParameterType == typeof(bool))
                        {
                            if (bool.TryParse(processedCommand[i + 1 + c], out bool parsedValue))
                            {
                                passedParams[i] = parsedValue;
                            }
                            else
                            {
                                if (processedCommand[i + 1 + c].ToUpper() == "ON")
                                {
                                    passedParams[i] = true;
                                }
                                if (processedCommand[i + 1 + c].ToUpper() == "OFF")
                                {
                                    passedParams[i] = false;
                                }
                            }
                        }
                        else if (requiredParams[i].ParameterType == typeof(Vector3))
                        {
                            if (processedCommand.Length > i + 3 + c)
                            {
                                Vector3 tempVec = Vector3.zero;

                                try
                                {
                                    tempVec.x = float.Parse(processedCommand[i + 1 + c]);
                                    tempVec.y = float.Parse(processedCommand[i + 2 + c]);
                                    tempVec.z = float.Parse(processedCommand[i + 3 + c]);
                                }
                                catch
                                {
                                    Debug.LogWarning("INVALID Vector3");
                                    return;
                                }

                                c += 2;
                                passedParams[i] = tempVec;

                            }

                        }
                        else if (requiredParams[i].ParameterType == typeof(Vector2))
                        {
                            if (int.TryParse(processedCommand[i + 1], out int parsedValue))
                            {
                                passedParams[i] = parsedValue;
                            }
                        }
                        else if (requiredParams[i].ParameterType == typeof(Vector4))
                        {
                            if (int.TryParse(processedCommand[i + 1], out int parsedValue))
                            {
                                passedParams[i] = parsedValue;
                            }
                        }

                        else
                        {
                            m_historyText.text += "\nCommand : " + processedCommand[0].ToUpper() + " is taking unsupported parameters";
                            ClearConsole();
                            return;
                        }


                        if (passedParams[i] == null)
                        {
                            //Invalid command parameters
                            m_historyText.text += "\nPassed parameters were not valid";
                            ClearConsole();
                            return;
                        }


                    }

                }
                else
                {
                    // Not enough parameters
                    m_historyText.text += "\n<color=red>Not enough parameters were specified</color>";
                    ClearConsole();
                    return;
                }

            }



            //Call the method
            availableMethods[methodIndex].Invoke(this, passedParams);

            //add the command to the history
            m_historyText.text += "\nSuccessfully executed command : " + processedCommand[0].ToUpper();

            ClearConsole();

            //close the console automatically
            //openedConsole = false;
        }

        else
        {
            if(processedCommand[0].ToUpper() == "HELP")
            {
                //show available commands
                for (int i = 0; i < availableMethodsNames.Count; i++)
                {
                    m_historyText.text += "\n-" + availableMethodsNames[i] + "-";
                }
                ClearConsole();
            }
            else
            {
                m_historyText.text += "\nUnrecognized command";
                ClearConsole();
            }
            
        }
    }

    void MakeCommandSuggestion()
    {
        suggestedCommandIndex = -1;
        int matchScore = 0;
        for (int i = 0; i < availableMethodsNames.Count; i++)
        {
            for (int n = 0; n < availableMethodsNames[i].Length; n++)
            {
                if(m_fieldText.text.Length > n)
                {
                    if (m_fieldText.text.ToUpper()[n] != availableMethodsNames[i].ToUpper()[n])
                    {
                        break;
                    }

                    if (n >= 2 && matchScore < n)
                    {
                        suggestedCommandIndex = i;
                        matchScore = n;           
                    }

                }
            }
            
        }

        //If a suggestion is accurate enough, save it
        if(suggestedCommandIndex != -1)
        {
            string neededparameters = "";
            for (int i = 0; i < availableMethods[suggestedCommandIndex].GetParameters().Length; i++)
            {
                neededparameters += " (" + availableMethods[suggestedCommandIndex].GetParameters()[i].ParameterType.Name + ")";
            }

            m_suggestionText.text = "(TAB) " + availableMethods[suggestedCommandIndex].Name + neededparameters;
        }
        //otherwise remove suggestions
        else
        {
            m_suggestionText.text = "";
        }

    }

    public void AddHistory(string history)
    {
        m_historyText.text += "\n" + history;
    }

    void ClearConsole()
    {
        m_inputField.text = "";
    }

    public static void TP(Vector3 pos)
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
    }

    public static void ShowPlayerPosition()
    {
        DebugConsole.instance.AddHistory("Player is at : " + GameObject.FindGameObjectWithTag("Player").transform.position.ToString());
    }

    public static void Message(string msg, float num)
    {
        Debug.Log(msg + " : " + num.ToString());
    }

    public static void ToggleTest(bool toggle)
    {
        Debug.Log(toggle);
    }

    public static void ShowTip(int tipIndex, int duration)
    {
        if(TipsManager.instance != null)
        {
            TipsManager.instance.ShowTip(tipIndex, TipsManager.TipType.BottomTip, duration);
        }
    }
}
