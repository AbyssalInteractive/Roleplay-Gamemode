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
        textContent = "Hey! Bienvenue sur Riverside Rôleplay, le serveur officiel de Nova-Life. Le mode de jeu de notre serveur est en constante évolution, n'hésitez pas à nous faire un retour de votre gameplay avec le /avis. Bon jeu à toi !",
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

    TextDialogJson TEXTDIALOG_ATM = new TextDialogJson
    {
        id = 5,
        textContent = "Votre solde bancaire : 454€",
        button1 = "Déposer",
        button2 = "Retirer",
        button3 = "Annuler",
        title = "ATM",
        type = 0
    };

    TextDialogJson TEXTDIALOG_ATM_WITHDRAW = new TextDialogJson
    {
        id = 6,
        textContent = "Montant de votre retrait bancaire :",
        button1 = "Retirer",
        button2 = "Annuler",
        title = "ATM - RETRAIT",
        type = 1
    };

    TextDialogJson TEXTDIALOG_ATM_DEPOSIT = new TextDialogJson
    {
        id = 7,
        textContent = "Montant de votre dépot bancaire :",
        button1 = "Déposer",
        button2 = "Annuler",
        title = "ATM - DÉPOT",
        type = 1
    };

    TextDialogJson TEXTDIALOG_OPINION = new TextDialogJson
    {
        id = 8,
        textContent = "Vous souhaitez nous soumettre votre avis ? Écrivez-nous:",
        button1 = "Soumettre",
        button2 = "Annuler",
        title = "VOTRE AVIS !",
        type = 1
    };

    TextDialogJson TEXTDIALOG_GUIDE_01 = new TextDialogJson
    {
        id = 9,
        textContent = "\t <color=orange>Les activités</color> \n Sur Riverside RP, vous avez de multiples activités pour gagner de l'argent facile telles que miner, couper du bois, etc. Pour plus d'infos faites <color=orange>/activités</color>",
        button1 = "Suivant",
        button2 = "Fermer",
        title = "GUIDE 1/5",
        type = 0
    };

    TextDialogJson TEXTDIALOG_GUIDE_02 = new TextDialogJson
    {
        id = 10,
        textContent = "\t <color=orange>Les métiers</color> \n Ce serveur possède plusieurs métiers comme Chauffeur de bus, Éboueur, Livreur, Gendarme, Ambulancier/Médecin, Pompiers, etc. Pour plus d'infos faites <color=orange>/aidejob</color>",
        button1 = "Suivant",
        button2 = "Fermer",
        title = "GUIDE 2/5",
        type = 0
    };

    TextDialogJson TEXTDIALOG_GUIDE_03 = new TextDialogJson
    {
        id = 11,
        textContent = "\t <color=orange>Les véhicules</color> \n Vous pouvez acheter un véhicule au concessionnaire, pour savoir où il se trouve, achetez un GPS au Supermarché et faites <color=orange>/gps</color>",
        button1 = "Suivant",
        button2 = "Fermer",
        title = "GUIDE 3/5",
        type = 0
    };

    TextDialogJson TEXTDIALOG_GUIDE_04 = new TextDialogJson
    {
        id = 12,
        textContent = "\t <color=orange>Les propriétés</color> \n Vous pouvez acheter une propriété, rendez-vous à la Mairie pour consulter les terrains disponibles",
        button1 = "Suivant",
        button2 = "Fermer",
        title = "GUIDE 4/5",
        type = 0
    };

    TextDialogJson TEXTDIALOG_GUIDE_05 = new TextDialogJson
    {
        id = 13,
        textContent = "\t <color=orange>Envie de plus ?</color> \n Faire le bien ne vous intéresse pas, vous êtes plutôt quelqu'un de l'ombre ? Faites <color=orange>/aidecriminel</color>",
        button1 = "Fermer",
        title = "GUIDE 5/5",
        type = 0
    };

    int TEXTLABEL_BUYCAR_01;
    int TEXTLABEL_ATM;

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
    public Dictionary<uint, RPCharacter> characters = new Dictionary<uint, RPCharacter>();

    public override void OnGamemodeInit()
    {
        base.OnGamemodeInit();
        Utils.gamemode = this;
        Utils.AccountsInit();
        Utils.JobsInit();

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
    
    public void OnRegisterAccount(uint playerId)
    {
        DestroyTextDialog(playerId, TEXTDIALOG_REGISTER.id);
        SendClientMessage(playerId, "#ffffff", "Vous pouvez désormais vous créer un personnage !");
        ShowCharacterCreation(playerId);
    }

    public override void OnTextDialogResponse(uint playerId, DialogResponse response)
    {
        base.OnTextDialogResponse(playerId, response);

        if(response.id == TEXTDIALOG_GUIDE_01.id)
        {
            if(response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_01.id);
                CreateTextDialog(playerId, TEXTDIALOG_GUIDE_02);
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_01.id);
            }
        }

        if (response.id == TEXTDIALOG_GUIDE_02.id)
        {
            if (response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_02.id);
                CreateTextDialog(playerId, TEXTDIALOG_GUIDE_03);
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_02.id);
            }
        }

        if (response.id == TEXTDIALOG_GUIDE_03.id)
        {
            if (response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_03.id);
                CreateTextDialog(playerId, TEXTDIALOG_GUIDE_04);
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_03.id);
            }
        }

        if (response.id == TEXTDIALOG_GUIDE_04.id)
        {
            if (response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_04.id);
                CreateTextDialog(playerId, TEXTDIALOG_GUIDE_05);
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_04.id);
            }
        }

        if (response.id == TEXTDIALOG_GUIDE_05.id)
        {
            if (response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_GUIDE_05.id);
            }
        }

        if (response.id == TEXTDIALOG_LOGIN.id)
        {
            // On récupère le steamId du joueur
            string steamId = players[playerId]["steamId"];
            if(response.selectedButton == 1)
            {
                if(Utils.LoginAccount(steamId, playerId, response.input))
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
                account.password = Utils.CreateMD5(response.input);
                account.steamId = steamId;
                Utils.RegisterAccount(account, playerId);
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

        if(response.id == TEXTDIALOG_BUYCAR.id)
        {
            // On a besoin de savoir quel véhicule le joueur souhaite acheter
            if(GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_BUYCAR_01) < 1f)
            {
                // Landstalker
            }
        }

        if(response.id == TEXTDIALOG_ATM.id)
        {
            if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_ATM) < 2f)
            {
                if(response.selectedButton == 1)
                {
                    DestroyTextDialog(playerId, TEXTDIALOG_ATM.id);
                    CreateTextDialog(playerId, TEXTDIALOG_ATM_DEPOSIT);
                }
                else if (response.selectedButton == 2)
                {
                    DestroyTextDialog(playerId, TEXTDIALOG_ATM.id);
                    CreateTextDialog(playerId, TEXTDIALOG_ATM_WITHDRAW);
                }
                else
                {
                    SendClientMessage(playerId, "#912091", "** Retrait de votre carte bancaire **");
                    DestroyTextDialog(playerId, TEXTDIALOG_ATM.id);
                }
            }else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_ATM.id);
            }
        }

        if(response.id == TEXTDIALOG_ATM_DEPOSIT.id)
        {
            RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
            float money = float.Parse(response.input);
            if (response.selectedButton == 1)
            {
                if (money > 0)
                {
                    if (money <= rpCharacter.money)
                    {
                        rpCharacter.money = rpCharacter.money - money;
                        rpCharacter.bank = money + rpCharacter.bank;
                        SetRPCharacter(playerId, JsonUtility.ToJson(rpCharacter));
                        DestroyTextDialog(playerId, TEXTDIALOG_ATM_DEPOSIT.id);
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=#36bf4a>Nouveau solde bancaire : " + rpCharacter.bank + "€ !</color>");
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=red>Vous n'avez pas cet argent sur vous !</color>");
                    }
                }
                else
                {
                    SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=red>Montant invalide ! Veuillez saisir un nouveau montant</color>");
                }
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_ATM_DEPOSIT.id);
            }
            
        }

        if(response.id == TEXTDIALOG_ATM_WITHDRAW.id)
        {
            RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
            float money = float.Parse(response.input);

            if(response.selectedButton == 1)
            {
                if (money > 0)
                {
                    if (money <= rpCharacter.bank)
                    {
                        rpCharacter.money = money + rpCharacter.money;
                        rpCharacter.bank = rpCharacter.bank - money;
                        SetRPCharacter(playerId, JsonUtility.ToJson(rpCharacter));
                        DestroyTextDialog(playerId, TEXTDIALOG_ATM_WITHDRAW.id);
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=#36bf4a>Nouveau solde bancaire : " + rpCharacter.bank + "€ !</color>");
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=red>Vous n'avez pas cet argent sur votre compte en banque !</color>");
                    }
                }
                else
                {
                    SendClientMessage(playerId, "#ffffff", "<color=orange>[ATM] </color><color=red>Montant invalide ! Veuillez saisir un nouveau montant</color>");
                }
            }else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_ATM_WITHDRAW.id);
            }
            
        }

        if(response.id == TEXTDIALOG_OPINION.id)
        {
            if (response.selectedButton == 1)
                Utils.CreateOpinion(playerId, response.input);
            else
                DestroyTextDialog(playerId, TEXTDIALOG_OPINION.id);
        }
    }

    public override void OnSecondUpdate()
    {
        NetworkIdentity[] Players = GetAllPlayers();
        for(int i = 0; i < Players.Length; i++)
        {
            PlayerUpdate(Players[i].netId);
        }
    }

    void PlayerUpdate(uint playerId)
    {
        RPCharacter rpCharacter = characters[playerId];
        rpCharacter.timeSpentOnServer += 1;
        rpCharacter.timeSpentOnServerSinceLogin += 1;
    }

    public override void OnPlayerDisconnect(uint playerId)
    {
        Utils.SaveRPCharacter(playerId);
        characters.Remove(playerId);
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
                CreateTextDialog(playerId, TEXTDIALOG_BUYCAR);
            }
        }
    }

    public override void OnPlayerMessage(string message, uint playerId)
    {
        base.OnPlayerMessage(message, playerId);
        // COMMANDES
        if(message.Substring(0, 1) == "/")
        {
            string command = message.Substring(1);
            string[] args = command.Split(char.Parse(" "));

            switch(args[0])
            {
                case "atm":
                    if(GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_ATM) < 2)
                    {
                        TextDialogJson textDialog = TEXTDIALOG_ATM;
                        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
                        textDialog.textContent = "Votre solde bancaire : " + rpCharacter.bank + "€";
                        CreateTextDialog(playerId, textDialog);
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "Vous n'êtes pas proche d'un ATM !");
                        SendClientMessage(playerId, "#ffffff", "<color=red>[DEBUG]</color> Distance : " + GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_ATM));
                        SendClientMessage(playerId, "#ffffff", "<color=red>[DEBUG]</color> ID : " + TEXTLABEL_ATM);
                    }
                    
                    break;
                case "respawn":
                    if(GetPlayerHealth(playerId) <= 0)
                    {
                        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
                        rpCharacter.health = 100;
                        SetRPCharacter(playerId, JsonUtility.ToJson(rpCharacter));
                        SetPlayerPos(playerId, new Vector3(880, 165, 1219));
                    }
                    break;
                case "takedamage":
                    RPCharacter _rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
                    _rpCharacter.health -= 10;
                    SetRPCharacter(playerId, JsonUtility.ToJson(_rpCharacter));
                    break;
                case "fly":
                    if(GetAccount(players[playerId]["steamId"]).adminLevel > 1)
                    {
                        SetPlayerFly(playerId, !GetPlayerFly(playerId));
                    }else
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=red>Vous n'avez pas les permissions d'utiliser cette commande</color>");
                    }
                    break;
                case "avis":
                    CreateTextDialog(playerId, TEXTDIALOG_OPINION);
                    break;
                case "guide":
                    CreateTextDialog(playerId, TEXTDIALOG_GUIDE_01);
                    break;
                case "crier": case "c":
                    if (args.Length > 1)
                        Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + " crie: " + message.Substring(args[0].Length+1), 15f);
                    else
                        SendClientMessage(playerId, "#ffffff", "USAGE: /c(rier) [MESSAGE]");
                    break;
                case "ch": case "chuchoter":
                    if (args.Length > 1)
                        Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + " chuchote: " + message.Substring(args[0].Length+1), 2f);
                    else
                        SendClientMessage(playerId, "#ffffff", "USAGE: /ch(uchote) [MESSAGE]");
                    break;
                case "me":
                    if (args.Length > 1)
                        Utils.SendDistanceMessage(GetPlayerPos(playerId), "<color=#912091>" + Utils.GetRPName(playerId) + message.Substring(args[0].Length + 1) + "</color>", 7.5f);
                    else
                        SendClientMessage(playerId, "#ffffff", "USAGE: /me [ACTION]");
                    break;
                case "stats":
                    string permisB = "non";
                    float hoursSpend = Mathf.Round(characters[playerId].timeSpentOnServer / 3600);

                    if (characters[playerId].licenseB) permisB = "oui";

                    SendClientMessage(playerId, "#ffffff", "<color=orange>___" + Utils.GetRPName(playerId) + "___</color>");
                    
                    SendClientMessage(playerId, "#ffffff", "<color=orange>Permis B : "+ permisB +" | Argent en poche : "+ characters[playerId].money +"€</color>");
                    SendClientMessage(playerId, "#ffffff", "<color=orange>Argent en banque : "+ characters[playerId].bank +" | Heures passées sur le serveur : "+ hoursSpend +"</color>");
                    break;
                default:
                    SendClientMessage(playerId, "#ffffff", "<color=red>Cette commande n'est pas dans notre base de données</color>");
                    break;
            }
        }else
        {
            Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + " dit: " + message, 7.5f);
        }
    }

    public Account GetAccount(string steamId)
    {
        string path_accounts = Application.dataPath + "/../Servers/" + serverName + "/Accounts/";

        if (File.Exists(path_accounts + steamId + ".json"))
        {
            string accountStr = File.ReadAllText(path_accounts + steamId + ".json");
            return JsonUtility.FromJson<Account>(accountStr);
        }
        else
        {
            return new Account();
        }
    }

    public override void OnPlayerSpawn(uint playerId)
    {
        base.OnPlayerSpawn(playerId);
        characters.Add(playerId, Utils.GetRPCharacterFromFile(playerId));
        CreateTextDialog(playerId, TEXTDIALOG_WELCOME);

        // Create player UI
        PlayerText playerUI = new PlayerText();
        playerUI.alignment = 7;
        playerUI.textAlignment = 6;
        playerUI.position = new Vector2(305.7f, 120.7f);
        playerUI.size = new Vector2(581.5f, 191.4f);
        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(GetRPCharacter(playerId));
        playerUI.text = rpCharacter.firstname + " " + rpCharacter.lastname + "\n" + "Argent: <color=orange>" + rpCharacter.money + "€</color> \n" + "Métier: Sans emploi";
        players[playerId].Add("playerUI", PlayerTextDrawCreate(playerId, playerUI).ToString());
        // End player UI

        if(players[playerId].ContainsKey("isFirstConnect"))
        {
            SendClientMessage(playerId, "#ffffff", "<color=orange>Bienvenue sur Riverside Rôleplay</color>");
            SendClientMessage(playerId, "#ffffff", "Utilisez <color=#ebcf60>/guide</color> si vous avez besoin d'aide");
            SendClientMessage(playerId, "#ffffff", "En cas de besoin utilisez le salon <color=#ebcf60>/n(ouveau)</color>");
            SendClientMessage(playerId, "#ffffff", "Utilisez <color=#ebcf60>/aiderp</color> si vous ne connaissez pas le RP");
        }
    }

    public override void OnPlayerUpdate(uint playerId)
    {
        base.OnPlayerUpdate(playerId);
    }

    public override void OnRPCharacterUpdate(uint playerId, string _rpCharacter)
    {
        base.OnRPCharacterUpdate(playerId, _rpCharacter);
        string playerTextID;
        RPCharacter rpCharacter = JsonUtility.FromJson<RPCharacter>(_rpCharacter);
        if (players[playerId].TryGetValue("playerUI", out playerTextID))
        {
            PlayerText playerUI = new PlayerText();
            playerUI.alignment = 7;
            playerUI.textAlignment = 6;
            playerUI.position = new Vector2(305.7f, 120.7f);
            playerUI.size = new Vector2(581.5f, 191.4f);
            playerUI.text = rpCharacter.firstname + " " + rpCharacter.lastname + "\n" + "Argent: <color=orange>" + (int)rpCharacter.money + "€</color> \n" + "Métier: Sans emploi";
            PlayerTextDrawUpdate(playerId, int.Parse(playerTextID), playerUI);
        }

        if(rpCharacter.health <= 0)
        {
            SendClientMessage(playerId, "#ffffff", "Vous êtes mort, faites /respawn");
        }
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
        textDraw.position.x = -60;
        textDraw.position.y = 30;
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
        TEXTLABEL_BUYCAR_01 = Create3DTextLabel("<color=orange>F</color> pour acheter ce Landstalker", new Vector3(818.036f, 165.078f, 1110.951f));
        TEXTLABEL_ATM = Create3DTextLabel("<color=orange>/atm</color> pour retirer ou déposer de l'argent", new Vector3(886.673f, 166.69f, 1233.943f));

    }    
}