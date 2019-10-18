using NovaLife.Server.Gamemode;
using Mirror;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Roleplay : Gamemode
{

    TextDialogJson TEXTDIALOG_LOGIN = new TextDialogJson
    {
        id = 1,
        textContent = "Merci de bien vouloir vous connecter :",
        button1 = "Connexion",
        button2 = "Annuler",
        title = "Connexion à votre compte",
        type = 1
    };

    TextDialogJson TEXTDIALOG_WELCOME = new TextDialogJson
    {
        id = 2,
        textContent = "Bienvenue sur Riverside Rôleplay ! Si vous voyez un bug, n'hésitez pas à nous le soumettre sur notre Discord.",
        button1 = "Ok",
        title = "Bienvenue !",
        type = 0
    };

    TextDialogJson TEXTDIALOG_REGISTER = new TextDialogJson
    {
        id = 3,
        textContent = "Vous n'avez pas encore de compte, veuillez en créer un nouveau :",
        button1 = "Créer",
        button2 = "Annuler",
        title = "Création de votre compte",
        type = 1
    };

    TextDialogJson TEXTDIALOG_BUYCAR = new TextDialogJson
    {
        id = 4,
        textContent = "Êtes-vous sûr de vouloir acheter ce véhicule ?",
        button1 = "Acheter",
        button2 = "Annuler",
        title = "Concessionnaire",
        type = 0
    };

    int TEXTLABEL_BUYCAR_01;

    public enum VehicleNames
    {
        Fast,
        Broose,
        Comet,
        Humvee,
        Pickup,
        Truck
    }

    public Dictionary<int, Job> jobs = new Dictionary<int, Job>();

    public override void OnGamemodeInit()
    {
        base.OnGamemodeInit();
        AccountsInit();
        JobsInit();
        CreateTextDraws();
        TextLabels();
    }

    public override void OnPlayerCommand(uint playerId, string command)
    {
        base.OnPlayerCommand(playerId, command);
    }

    public override void OnPlayerConnect(uint playerId, string steamId)
    {
        base.OnPlayerConnect(playerId, steamId);
        // On affiche globalement quel joueur steam s'est connecté
        SendBroadcastMessage("#ffffff", GetSteamUsernameBySteamId(steamId) + " s'est connecté au serveur !");
        RegisterOrLogin(playerId, steamId);
    }

    // Fonction pour savoir si l'on doit connecter ou inscrire le joueur
    void RegisterOrLogin(uint playerId, string steamId)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + serverName + "/Accounts/";
        
        if(File.Exists(path_accounts + steamId +".json"))
        {
            OpenLogin(playerId);
        }else
        {
            OpenRegister(playerId);
        }
    }

    void OpenRegister(uint playerId)
    {
        CreateTextDialog(playerId, TEXTDIALOG_REGISTER);
    }

    void OpenLogin(uint playerId)
    {
        CreateTextDialog(playerId, TEXTDIALOG_LOGIN);
    }

    // Fonction pour inscrire le compte dans le dossier Accounts du serveur
    void RegisterAccount(Account account, uint playerId)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + serverName + "/Accounts/";

        if (!File.Exists(path_accounts + account.steamId + ".json"))
        {
            File.WriteAllText(path_accounts + account.steamId + ".json", JsonUtility.ToJson(account));
            DestroyTextDialog(playerId, TEXTDIALOG_REGISTER.id);
            SendClientMessage(playerId, "#ffffff", "Vous pouvez désormais vous créer un personnage !");
            ShowCharacterCreation(playerId);
        }
    }

    // Fonction pour tester si on peut connecter une compte
    bool LoginAccount(string steamId, uint playerId, string password)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + serverName + "/Accounts/";

        if (File.Exists(path_accounts + steamId + ".json"))
        {
            string accountStr = File.ReadAllText(path_accounts + steamId + ".json");
            Account loginAccount = JsonUtility.FromJson<Account>(accountStr);

            if(loginAccount.password == CreateMD5(password))
            {
                return true;
            }else
            {
                return false;
            }
            
        }else
        {
            return false;
        }
    }

    public override void OnTextDialogResponse(uint playerId, DialogResponse response)
    {
        base.OnTextDialogResponse(playerId, response);
        if(response.id == TEXTDIALOG_LOGIN.id)
        {
            // On récupère le steamId du joueur
            string steamId = players[playerId]["steamId"];
            if(response.selectedButton == 1)
            {
                if(LoginAccount(steamId, playerId, response.input))
                {
                    DestroyTextDialog(playerId, TEXTDIALOG_LOGIN.id);
                    SendClientMessage(playerId, "#ffffff", "<color=cyan>[SERVEUR] </color>Vous vous êtes connecté avec succès !");
                    ShowCharacterCreation(playerId);
                }
                else
                {
                    SendClientMessage(playerId, "#ffffff", "<color=cyan>[SERVEUR] </color>Mot de passe incorrect !");
                }                
            }else
            {
                SendClientMessage(playerId, "#ffffff", "Déconnexion du serveur...");
                Kick(playerId);
            }
        }

        if (response.id == TEXTDIALOG_REGISTER.id && response.selectedButton == 1)
        {
            if(response.input != "")
            {
                string steamId = players[playerId]["steamId"];
                Account account = new Account();
                account.password = CreateMD5(response.input);
                account.steamId = steamId;
                RegisterAccount(account, playerId);
            }else
            {
                SendClientMessage(playerId, "#ffffff", "<color=red>Veuillez saisir un mot de passe !</color>");
            }            
        }else if(response.id == TEXTDIALOG_REGISTER.id && response.selectedButton == 2)
        {
            Kick(playerId);
        }

        if(response.id == TEXTDIALOG_WELCOME.id)
        {
            DestroyTextDialog(playerId, TEXTDIALOG_WELCOME.id);
        }
    }

    public override void OnPlayerDisconnect(uint playerId)
    {
        base.OnPlayerDisconnect(playerId);
    }

    public override void OnPlayerEnterVehicle(uint playerId, uint vehicleId)
    {
        base.OnPlayerEnterVehicle(playerId, vehicleId);

        // Création de l'interface du véhicule
        PlayerText textDraw = new PlayerText();
        textDraw.text = "Your vehicle : " + vehicleId;
        textDraw.position.x = -50;
        textDraw.position.y = 25;
        textDraw.size.x = 100;
        textDraw.size.y = 50;
        textDraw.alignment = 9;
        textDraw.textAlignment = 8;
        textDraw.outline = true;
        textDraw.shadow = true;        
        TextDrawUpdate(0, textDraw);

        // On teste le véhicule appartient au joueur
        if(IsPlayerVehicle(playerId, vehicleId))
        {
            
        }else
        {
            SendClientMessage(playerId, "#96121d", "Ce véhicule ne vous appartient pas.");
        }
        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));

        if(rpCharacter.licenseB == false)
        {
            SendClientMessage(playerId, "#96121d", "Vous n'avez pas le permis B, faites attention !");
        }

    }

    public override void OnPlayerExitVehicle(uint playerId, uint vehicleId)
    {
        base.OnPlayerExitVehicle(playerId, vehicleId);
    }

    public override void OnPlayerKeyDown(KeyCode key, uint playerId)
    {
        base.OnPlayerKeyDown(key, playerId);
        if(key == KeyCode.F12)
        {
            SendClientMessage(playerId, "#ffffff", "<color=red>[DEBUG]</color> Player Position : " + GetPlayerPos(playerId));
        }

        if(key == KeyCode.F)
        {
            if(GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_BUYCAR_01) < 1f)
            {
                // TODO : Création du dialog, validation de l'achat du véhicule
            }
        }
    }

    public override void OnPlayerMessage(string message, uint playerId)
    {
        base.OnPlayerMessage(message, playerId);
    }

    public override void OnPlayerSpawn(uint playerId)
    {
        base.OnPlayerSpawn(playerId);
        CreateTextDialog(playerId, TEXTDIALOG_WELCOME);
    }

    public override void OnPlayerUpdate(uint playerId)
    {
        base.OnPlayerUpdate(playerId);
    }

    public override void OnServerLoop()
    {
        base.OnServerLoop();
    }

    public override void OnVehicleDeath(uint vehicleId)
    {
        base.OnVehicleDeath(vehicleId);
    }

    public override void OnVehicleSpawn(uint vehicleId)
    {
        base.OnVehicleSpawn(vehicleId);
    }

    // Le nom du serveur, mettez le votre !
    void CreateTextDraws()
    {
        PlayerText textDraw = new PlayerText();
        textDraw.text = "Riverside RP";
        textDraw.position.x = -50;
        textDraw.position.y = 25;
        textDraw.size.x = 100;
        textDraw.size.y = 50;
        textDraw.alignment = 9;
        textDraw.textAlignment = 8;
        textDraw.outline = true;
        textDraw.shadow = true;
        TextDrawCreate(textDraw);
    }

    // Les différents 3DTextLabels de la carte
    void TextLabels()
    {
        Create3DTextLabel("Utilisez <color=#3480eb>/téléphoner</color> [numéro]", new Vector3(878, 165, 1235));
        Create3DTextLabel("Regardez le <b><color=red>con</color></b>, il s'est vautré !", new Vector3(893, 166.5f, 1222));
        Create3DTextLabel("Le bus des nouveaux arrivants !", new Vector3(880, 165, 1219));
        TEXTLABEL_BUYCAR_01 = Create3DTextLabel("<color=orange>F</color> pour acheter ce Landstalker", new Vector3(880, 165, 1219));
    }

    void JobsInit()
    {
        // Si le répertoire des métiers n'existe pas on le créé
        string path_jobs = Application.dataPath + "/../Servers/" + serverName + "/Jobs/";
        if (!Directory.Exists(path_jobs))
        {
            Directory.CreateDirectory(path_jobs);
        }

        // On récupère tous les dossiers de chaque métier pour effectuer une itération
        string[] jobs_directories = Directory.GetDirectories(path_jobs);

        // On charge tous les métiers à partir du répertoire que l'on a créé auparavant
        for(int i = 0; i < jobs_directories.Length; i++)
        {
            if(File.Exists(jobs_directories[i] + "config.json"))
            {
                string jobStr = File.ReadAllText(jobs_directories[i] + "config.json");
                Job job = JsonUtility.FromJson<Job>(jobStr);

                jobs.Add(job.jobId, job);
            }
        }
    }

    void AccountsInit()
    {
        // Si le répertoire des comptes n'existe pas on le créé
        string path_accounts = Application.dataPath + "/../Servers/" + serverName + "/Accounts/";
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
}