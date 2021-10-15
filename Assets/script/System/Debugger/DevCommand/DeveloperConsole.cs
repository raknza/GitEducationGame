﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            string addMessage = " command has been added to the console.";

            DeveloperConsole.AddCommandsToConsole(Command, this);
            //GameSystemManager.GetSystem<DeveloperConsole>().AddMessageToConsole(Name + addMessage);
        }

        public abstract void RunCommand(string[] param);
    }

    public class DeveloperConsole : MonoBehaviour,Panel
    {
        public static DeveloperConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }

        [Header("UI Components")]
        public Canvas consoleCanvas;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;

        public List<string> inputLogs;

        [SerializeField]
        int inputIndex = -1;

        private void Awake()
        {

            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(true);
            //GameSystemManager.AddSystem<DeveloperConsole>(gameObject);
            CreateCommands();
            GameSystemManager.GetSystem<PanelManager>().AddSubPanel(this);
        }

        private void OnDestroy()
        {
            GameSystemManager.GetSystem<PanelManager>().ReturnTopPanel();
        }

        private void OnEnable()
        {
            //Application.logMessageReceived += HandleLog;

        }

        private void OnDisable()
        {
            //Application.logMessageReceived -= HandleLog;

        }

        private void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            string _message = "[" + type.ToString() + "] " + logMessage;
            AddMessageToConsole(_message);
        }

        private void CreateCommands()
        {
            CommandQuit.CreateCommand();
            CommandDelete.CreateCommand();
            CommandCopy.CreateCommand();
            CommandGitInit.CreateCommand();
        }

        public static void AddCommandsToConsole(string _name, ConsoleCommand _command)
        {
            if(!Commands.ContainsKey(_name))
            {
                Commands.Add(_name, _command);
            }
        }

        public void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

        public void ParseInput(string input)
        {
            string[] _input = input.Split(' ');

            if (_input.Length == 0 || _input == null)
            {
                AddMessageToConsole("Command not recognized.");
                return;
            }

            if (!Commands.ContainsKey(_input[0]))
            {
                AddMessageToConsole("Command not recognized.");
            }
            else
            {
                Commands[_input[0]].RunCommand(_input);
            }
        }

        public void PanelInput()
        {
            //Cursor.visible = consoleCanvas.gameObject.activeInHierarchy;
            if (consoleCanvas.gameObject.activeInHierarchy)
            {
                if ( (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return )))
                {
                    if (inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        if (inputLogs.Count == 0 || inputText.text != inputLogs[inputLogs.Count - 1] )
                        {
                            inputLogs.Add(inputText.text);
                        }
                        if (GameSystemManager.GetSystem<StudentEventManager>())
                        {
                            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("console_input", "{input:'" + inputText.text + "'}");
                        }
                        ParseInput(inputText.text);
                        consoleInput.text = "";
                        consoleInput.ActivateInputField();
                        inputIndex = inputLogs.Count;

                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (inputIndex > 0)
                    {
                        inputIndex--;
                        consoleInput.text = inputLogs[inputIndex];
                        consoleCanvas.GetComponentInChildren<InputField>().MoveTextEnd(false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (inputIndex < inputLogs.Count - 1 )
                    {
                        inputIndex++;
                        consoleInput.text = inputLogs[inputIndex];
                        consoleCanvas.GetComponentInChildren<InputField>().MoveTextEnd(false);
                    }
                }
            }
        }

        public void ClosePanel()
        {
            throw new System.NotImplementedException();
        }

        public void OpenPanel()
        {
            throw new System.NotImplementedException();
        }
    }
}
