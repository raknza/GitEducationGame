using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandGitInit : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandGitInit()
        {
            Name = "git init";
            Command = "git";
            Description = "found git repository under folder";
            Help = "Use this command with no arguments to found a git repository";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] param)
        {
            GitSystem gitSystem = GameObject.Find("GitObject").GetComponent<GitSystem>();
            if ( param[1] == "init")
            {
                bool exist = GameObject.Find("GitObject").GetComponent<GitSystem>();
                if ( exist ) {
                    GameObject.Find("GitObject").GetComponent<GitSystem>().buildRepository();
                }

            }
            if (param[1] == "add")
            {
                gitSystem.trackFile(param[2], "test");

            }
            if (param[1] == "remove")
            {
                gitSystem.untrackFile(param[2]);

            }
            if (param[1] == "commit")
            {
                gitSystem.Commit(param[2]);

            }
            if ( param[1]  == "remote") {
                if (param[2] == "add")
                {
                    gitSystem.addRemote(param[3]);
                }
            }
            if( param[1] == "push")
            {
                gitSystem.Push();
            }
            if (param[1] == "clone")
            {
                bool clone = gitSystem.cloneRepository(param[2]);
                if (!clone)
                {

                }
            }
            if (param[1] == "branch")
            {
                if (param.Length == 2)
                {
                    Debug.Log(gitSystem.localRepository.branches.ToString());
                }
                else if(param.Length == 3)
                {
                    gitSystem.createBranch(param[2]);
                }
                else if (param.Length == 4 && param[2] == "-D")
                {
                    gitSystem.deleteBranch(param[3]);
                }

            }
            if (param[1] == "checkout")
            {
                gitSystem.checkout(param[2]);

            }
            if (param[1] == "merge")
            {
                gitSystem.Merge(param[2]);

            }
            if (param[1] == "pull")
            {
                gitSystem.Pull(param[2],param[3]);

            }

        }

        public static CommandGitInit CreateCommand()
        {
            return new CommandGitInit();
        }
    }
}