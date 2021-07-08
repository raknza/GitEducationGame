using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9 : Level
{

    private void Start()
    {
        setUp();

        /*gitSystem.addRemote("server");
        gitSystem.serverRepository = new Repository();

        Commit firstCommit = new Commit("init commit", "");
        firstCommit.addModifiedFile(new KeyValuePair<string, string>("index", "init"));
        gitSystem.serverRepository.Commit(firstCommit);

        Commit secondCommit = new Commit("Add page1", "");
        secondCommit.addModifiedFile(new KeyValuePair<string, string>("page1", "init"));
        gitSystem.serverRepository.Commit(secondCommit);

        Commit thirdCommit = new Commit("update page1", "");
        thirdCommit.addModifiedFile(new KeyValuePair<string, string>("page1", "HelloWorld !\nsomeone changed"));
        gitSystem.serverRepository.Commit(thirdCommit);

        gitSystem.buildRepository();

        fileSystem.NewFile("index", "");
        gitSystem.trackFile("index", "init");
        gitSystem.Commit("init commit");

        fileSystem.NewFile("page1", "");
        gitSystem.trackFile("page1", "init");
        gitSystem.Commit("Add page1");

        fileSystem.SetFileContent("page1", "Hello World !\nlocal test changed");
        gitSystem.trackFile("page1", "Hello World !\nlocal test changed");

        fileSystem.NewFile("page2", "");
        gitSystem.trackFile("page2", "init");*/

        gitSystem.buildRepository();

        fileSystem.NewFile("index", "");
        gitSystem.trackFile("index", "init");
        gitSystem.Commit("init commit");

        fileSystem.NewFile("page1", "");
        gitSystem.trackFile("page1", "init");
        gitSystem.Commit("Add page1");

        fileSystem.NewFile("page2", "");
        gitSystem.trackFile("page2", "init");
        gitSystem.Commit("add page2");


        //gitSystem.Push();

         gitSystem.createBranch("test");
         gitSystem.checkout("test");
         gitSystem.trackFile("page2", "test update page2");
         gitSystem.Commit("update page2");

         gitSystem.checkout("master");
         gitSystem.trackFile("page2", "master update page2");
         gitSystem.Commit("update page2");


    }
    // Update is called once per frame
    void Update()
    {
        if ( !targetSystem.targetStatus[0] &&  gitSystem.conflicted)
        {
            targetSystem.targetStatus[0] = true;
            targetSystem.AccomplishTarget(0);
        }
        if (!targetSystem.targetStatus[1] && targetSystem.targetStatus[0] && !gitSystem.conflicted )
        {
            targetSystem.targetStatus[1] = true;
            targetSystem.AccomplishTarget(1);
        }
        if (!targetSystem.targetStatus[2] && targetSystem.targetStatus[1] && gitSystem.sync)
        {
            targetSystem.targetStatus[2] = true;
            targetSystem.AccomplishTarget(2);
        }

        updateTarget();
    }
}
