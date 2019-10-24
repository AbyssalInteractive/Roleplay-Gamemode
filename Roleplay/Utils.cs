using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NovaLife.Server.Gamemode;
using Mirror;

public static class Utils
{
    public static Roleplay gamemode;

    public static void AccountsInit()
    {
        // Si le répertoire des comptes n'existe pas on le créé
        string path_accounts = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Accounts/";
        if (!Directory.Exists(path_accounts))
        {
            Directory.CreateDirectory(path_accounts);
        }
    }

    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    // Fonction pour tester si on peut connecter une compte
    public static bool LoginAccount(string steamId, uint playerId, string password)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Accounts/";

        if (File.Exists(path_accounts + steamId + ".json"))
        {
            string accountStr = File.ReadAllText(path_accounts + steamId + ".json");
            Account loginAccount = JsonUtility.FromJson<Account>(accountStr);

            if (loginAccount.password == Utils.CreateMD5(password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    // Fonction pour inscrire le compte dans le dossier Accounts du serveur
    public static void RegisterAccount(Account account, uint playerId)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Accounts/";

        if (!File.Exists(path_accounts + account.steamId + ".json"))
        {
            File.WriteAllText(path_accounts + account.steamId + ".json", JsonUtility.ToJson(account));
            gamemode.OnRegisterAccount(playerId);

            gamemode.players[playerId].Add("isFirstConnect", "1");
        }
    }

    public static RPCharacter GetRPCharacter(uint playerId)
    {
        if(gamemode.IsValidPlayer(playerId))
        {
            return gamemode.characters[playerId];
        }else
        {
            return new RPCharacter();
        }
    }

    public static RPCharacter GetRPCharacterFromFile(uint playerId)
    {
        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(gamemode.GetRPCharacter(playerId));
        string steamId = gamemode.players[playerId]["steamId"];
        string path_character = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Characters/" + steamId + "-" + rpCharacter.firstname + "-" + rpCharacter.lastname + ".json";
        string json = File.ReadAllText(path_character);

        return JsonUtility.FromJson<RPCharacter>(json);
    }

    public static void SendDistanceMessage(Vector3 origin, string message, float radius)
    {
        NetworkIdentity[] _players = gamemode.GetAllPlayers();
        for (int i = 0; i < _players.Length; i++)
        {
            if (Vector3.Distance(origin, gamemode.GetPlayerPos(_players[i].netId)) < radius)
            {
                gamemode.SendClientMessage(_players[i].netId, "#ffffff", message);
            }
        }
    }

    public static string GetRPName(uint playerId)
    {
        RPCharacter rpCharacter = GetRPCharacter(playerId);
        return rpCharacter.firstname + " " + rpCharacter.lastname;
    }

    public static void CreateOpinion(uint playerId, string opinion)
    {
        // Si le répertoire des avis n'existe pas on le créé
        string path_opinions = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Opinions/";
        if (!Directory.Exists(path_opinions))
        {
            Directory.CreateDirectory(path_opinions);
        }

        if(string.IsNullOrEmpty(opinion))
        {
            gamemode.SendClientMessage(playerId, "#ffffff", "Votre avis ne doit pas être vide !");
        }else
        {
            string steamId = gamemode.players[playerId]["steamId"];

            if (!File.Exists(path_opinions + steamId + ".json"))
            {
                Opinion _opinion = new Opinion();
                _opinion.steamId = steamId;
                _opinion.opinion = opinion;

                File.WriteAllText(path_opinions + steamId + ".json", JsonUtility.ToJson(_opinion));
            }
            else
            {
                gamemode.SendClientMessage(playerId, "#ffffff", "Vous avez déjà soumis votre avis, merci ! Nous vous redemanderons votre avis à la prochaine mise à jour de notre mode de jeu !");
            }
        }
    }

    public static void SaveRPCharacter(uint playerId)
    {
        RPCharacter rpCharacter = new RPCharacter();
        rpCharacter = GetRPCharacter(playerId);
        rpCharacter.timeSpentOnServerSinceLogin = 0;
        string steamId = gamemode.players[playerId]["steamId"];
        string path_character = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Characters/" + steamId + "-" + rpCharacter.firstname + "-" + rpCharacter.lastname + ".json";

        File.WriteAllText(path_character, JsonUtility.ToJson(rpCharacter));
    }

    public static void LoadPhones()
    {
        string path_character = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Characters/";
        string[] files = Directory.GetFiles(path_character);
        for(int i = 0; i < files.Length; i++)
        {
            RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(File.ReadAllText(path_character));
            if(!string.IsNullOrEmpty(rpCharacter.phone))
            {
                gamemode.phones.Add(rpCharacter.phone, rpCharacter);
            }
        }
    }

    public static void JobsInit()
    {
        // Si le répertoire des métiers n'existe pas on le créé
        string path_jobs = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Jobs/";
        if (!Directory.Exists(path_jobs))
        {
            Directory.CreateDirectory(path_jobs);
        }

        // On récupère tous les dossiers de chaque métier pour effectuer une itération
        string[] jobs_directories = Directory.GetDirectories(path_jobs);

        // On charge tous les métiers à partir du répertoire que l'on a créé auparavant
        for (int i = 0; i < jobs_directories.Length; i++)
        {
            if (File.Exists(jobs_directories[i] + "config.json"))
            {
                string jobStr = File.ReadAllText(jobs_directories[i] + "config.json");
                Job job = JsonUtility.FromJson<Job>(jobStr);

                gamemode.jobs.Add(job.jobId, job);
            }
        }
    }

    public static void BizsInit()
    {
        // Si le répertoire des entreprises n'existe pas on le créé
        string path_bizs = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Bizs/";
        if (!Directory.Exists(path_bizs))
        {
            Directory.CreateDirectory(path_bizs);
        }

        // On récupère tous les dossiers de chaque entreprise pour effectuer une itération
        string[] path_directories = Directory.GetDirectories(path_bizs);

        // On charge toutes les entreprises à partir du répertoire que l'on a créé auparavant
        for (int i = 0; i < path_directories.Length; i++)
        {
            if (File.Exists(path_directories[i] + "business.json"))
            {
                string bizStr = File.ReadAllText(path_directories[i] + "business.json");
                Business biz = JsonUtility.FromJson<Business>(bizStr);

                gamemode.bizs.Add(biz.name, biz);
            }
        }
    }

    public static bool CreateBiz(string bizName, uint playerId)
    {
        string path_bizs = Application.dataPath + "/../Servers/" + gamemode.serverName + "/Bizs/";

        if (gamemode.bizs.ContainsKey(bizName))
        {
            return false;
        }else
        {
            if(string.IsNullOrEmpty(gamemode.characters[playerId].bizName))
            {
                DirectoryInfo directory = Directory.CreateDirectory(path_bizs + bizName);
                Business biz = new Business();
                biz.name = bizName;
                biz.ownerNames = GetRPName(playerId).Replace(char.Parse(" "), char.Parse("§"));
                File.WriteAllText(directory.FullName + "/business.json", JsonUtility.ToJson(biz));

                RPCharacter rpCharacter = gamemode.characters[playerId];
                rpCharacter.bizName = bizName;
                gamemode.characters[playerId] = rpCharacter;

                gamemode.bizs.Add(bizName, biz);

                return true;
            }else
            {
                return false;
            }
        }
    }

    public static uint GetPlayerIdFromPhone(string phone)
    {
        foreach(KeyValuePair<uint, RPCharacter> entry in gamemode.characters)
        {
            if(entry.Value.phone == phone)
            {
                return entry.Key;
            }
        }

        return 0;
    }

    public static void GeneratePhone(uint playerId) 
    {
        string phone = "06" + Random.Range(10000000, 99999999);
        if(gamemode.phones.ContainsKey(phone))
        {
            GeneratePhone(playerId);
        }else
        {
            RPCharacter rpCharacter = gamemode.characters[playerId];
            rpCharacter.phone = phone;
            gamemode.characters[playerId] = rpCharacter;
        }
    }
}