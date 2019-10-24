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

    TextDialogJson TEXTDIALOG_TESTTAB = new TextDialogJson
    {
        id = 14,
        textContent = "",
        button1 = "Sélectionner",
        button2 = "Fermer",
        listItems = new string[10] { "Téléphone \t\t\t 1500€", "GPS \t\t\t\t1500€", "Téléphone \t\t\t 1500€", "GPS \t\t\t\t1500€", "Téléphone \t\t\t 1500€", "GPS \t\t\t\t1500€", "Téléphone \t\t\t 1500€", "GPS \t\t\t\t1500€", "Téléphone \t\t\t 1500€", "GPS \t\t\t\t1500€", },
        title = "TAB",
        type = 2
    };

    TextDialogJson TEXTDIALOG_ACTIVITES = new TextDialogJson
    {
        id = 15,
        textContent = "",
        button1 = "Sélectionner",
        button2 = "Fermer",
        listItems = new string[2] { "Miner", "Couper du bois" },
        title = "Liste des activités",
        type = 2
    };

    TextDialogJson TEXTDIALOG_ACTIVITE_MINER = new TextDialogJson
    {
        id = 16,
        textContent = "\t <color=orange>ACTIVITÉ : MINER</color> \n Une activité longue et fastidieuse qui vous fera gagner quelques euros, rendez-vous à la mine (/gps)",
        button1 = "Fermer",
        title = "MINER",
        type = 0
    };

    TextDialogJson TEXTDIALOG_ACTIVITE_BOIS = new TextDialogJson
    {
        id = 17,
        textContent = "\t <color=orange>ACTIVITÉ : COUPER DU BOIS</color> \n Une activité qui vous fera découvrir la beauté de la nature, et la destruction de celle-ci ! (/gps)",
        button1 = "Fermer",
        title = "MINER",
        type = 0
    };

    TextDialogJson TEXTDIALOG_GPS = new TextDialogJson
    {
        id = 18,
        textContent = "",
        button1 = "Sélectionner",
        button2 = "Fermer",
        listItems = new string[2] { "Mine", "Forêt" },
        title = "Liste des destinations",
        type = 2
    };

    TextDialogJson TEXTDIALOG_MARKET_01 = new TextDialogJson
    {
        id = 19,
        textContent = "",
        button1 = "Acheter",
        button2 = "Fermer",
        listItems = new string[2] { "Téléphone \t\t\t 350€", "GPS \t\t\t\t 250€" },
        title = "Liste des produits",
        type = 2
    };

    int TEXTLABEL_BUYCAR_01;
    int TEXTLABEL_ATM;
    int TEXTLABEL_MARKET_01;
    int TEXTLABEL_TOWNHALL_BUSINESS;
    int TEXTLABEL_TOWNHALL_JOBS;
    int TEXTLABEL_PHONE;
    int TEXTLABEL_BUS_STOP_01;
    int TEXTLABEL_JOB_ELECTECHNICIAN_01;
    int TEXTLABEL_JOB_ELECTECHNICIAN_02;

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
    public Dictionary<string, RPCharacter> phones = new Dictionary<string, RPCharacter>();

    public override void OnGamemodeInit()
    {
        base.OnGamemodeInit();
        Utils.gamemode = this;
        TextLabels();
        Utils.AccountsInit();
        Utils.JobsInit();
        Utils.LoadPhones();

        CreateTextDraws();

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

        if (File.Exists(path_accounts + steamId + ".json"))
        {
            OpenLogin(playerId);
        }
        else
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

        if (response.id == TEXTDIALOG_MARKET_01.id)
        {
            if (response.selectedButton == 1)
            {
                switch (response.selectedLine)
                {
                    case 0:
                        if (characters[playerId].money >= 350)
                        {
                            if (string.IsNullOrEmpty(characters[playerId].phone))
                            {
                                RPCharacter rpCharacter = characters[playerId];
                                rpCharacter.money -= 350;
                                characters[playerId] = rpCharacter;
                                Utils.GeneratePhone(playerId);
                                SendClientMessage(playerId, "#ffffff", "<color=orange>Vous avez acheté un téléphone !</color>");
                                SendClientMessage(playerId, "#ffffff", "Votre numéro de téléphone : " + characters[playerId].phone);
                            }
                            else
                            {
                                SendClientMessage(playerId, "#ffffff", "<color=red>Vous avez déjà un téléphone.</color>");
                            }
                        }
                        else
                        {
                            SendClientMessage(playerId, "#ffffff", "<color=red>Vous n'avez pas assez d'argent !</color>");
                        }
                        break;
                    case 1:
                        if (characters[playerId].money >= 250)
                        {
                            if (!characters[playerId].gps)
                            {
                                RPCharacter rpCharacter = characters[playerId];
                                rpCharacter.money -= 250;
                                rpCharacter.gps = true;
                                characters[playerId] = rpCharacter;
                                SendClientMessage(playerId, "#ffffff", "<color=orange>Vous avez acheté un GPS !</color>");
                            }
                            else
                            {
                                SendClientMessage(playerId, "#ffffff", "<color=red>Vous avez déjà un GPS.</color>");
                            }
                        }
                        else
                        {
                            SendClientMessage(playerId, "#ffffff", "<color=red>Vous n'avez pas assez d'argent !</color>");
                        }
                        break;
                }
                DestroyTextDialog(playerId, response.id);
            }
            else
            {
                DestroyTextDialog(playerId, response.id);
            }
        }

        if (response.id == TEXTDIALOG_GPS.id)
        {
            if (response.selectedButton == 1)
            {
                DestroyTextDialog(playerId, response.id);
                switch (response.selectedLine)
                {
                    case 0:
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[GPS]</color> : NOUVELLE DESTINATION >> MINE");
                        break;
                    case 1:
                        SendClientMessage(playerId, "#ffffff", "<color=orange>[GPS]</color> : NOUVELLE DESTINATION >> FORÊT");
                        break;
                }
                // TODO CREATE CHECKPOINT
            }
            else
            {
                DestroyTextDialog(playerId, response.id);
            }
        }

        if (response.id == TEXTDIALOG_ACTIVITES.id)
        {
            if (response.selectedButton == 1)
            {
                switch (response.selectedLine)
                {
                    case 0:
                        DestroyTextDialog(playerId, response.id);
                        CreateTextDialog(playerId, TEXTDIALOG_ACTIVITE_MINER);
                        break;
                    case 1:
                        DestroyTextDialog(playerId, response.id);
                        CreateTextDialog(playerId, TEXTDIALOG_ACTIVITE_BOIS);
                        break;
                }
            }
            else
            {
                DestroyTextDialog(playerId, response.id);
            }
        }

        if (response.id == TEXTDIALOG_ACTIVITE_BOIS.id || response.id == TEXTDIALOG_ACTIVITE_MINER.id)
        {
            DestroyTextDialog(playerId, response.id);
        }

        if (response.id == TEXTDIALOG_GUIDE_01.id)
        {
            if (response.selectedButton == 1)
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
            if (response.selectedButton == 1)
            {
                if (Utils.LoginAccount(steamId, playerId, response.input))
                {
                    DestroyTextDialog(playerId, TEXTDIALOG_LOGIN.id);
                    SendClientMessage(playerId, "#ffffff", "<color=cyan>[SERVEUR] </color>Vous vous êtes connecté avec succès !");
                    ShowCharacterCreation(playerId);
                }
                else
                {
                    SendClientMessage(playerId, "#ffffff", "<color=cyan>[SERVEUR] </color>Mot de passe incorrect !");
                }
            }
            else
            {
                SendClientMessage(playerId, "#ffffff", "Déconnexion du serveur...");
                Kick(playerId);
            }
        }

        if (response.id == TEXTDIALOG_REGISTER.id && response.selectedButton == 1)
        {
            if (response.input != "")
            {
                string steamId = players[playerId]["steamId"];
                Account account = new Account();
                account.password = Utils.CreateMD5(response.input);
                account.steamId = steamId;
                Utils.RegisterAccount(account, playerId);
            }
            else
            {
                SendClientMessage(playerId, "#ffffff", "<color=red>Veuillez saisir un mot de passe !</color>");
            }
        }
        else if (response.id == TEXTDIALOG_REGISTER.id && response.selectedButton == 2)
        {
            Kick(playerId);
        }

        if (response.id == TEXTDIALOG_WELCOME.id)
        {
            DestroyTextDialog(playerId, TEXTDIALOG_WELCOME.id);
        }

        if (response.id == TEXTDIALOG_BUYCAR.id)
        {
            // On a besoin de savoir quel véhicule le joueur souhaite acheter
            if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_BUYCAR_01) < 1f)
            {
                // Landstalker
            }
        }

        if (response.id == TEXTDIALOG_ATM.id)
        {
            if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_ATM) < 2f)
            {
                if (response.selectedButton == 1)
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
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_ATM.id);
            }
        }

        if (response.id == TEXTDIALOG_ATM_DEPOSIT.id)
        {
            RPCharacter rpCharacter = characters[playerId];
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

        if (response.id == TEXTDIALOG_ATM_WITHDRAW.id)
        {
            RPCharacter rpCharacter = characters[playerId];
            float money = float.Parse(response.input);

            if (response.selectedButton == 1)
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
            }
            else
            {
                DestroyTextDialog(playerId, TEXTDIALOG_ATM_WITHDRAW.id);
            }

        }

        if (response.id == TEXTDIALOG_OPINION.id)
        {
            if (response.selectedButton == 1)
                Utils.CreateOpinion(playerId, response.input);
            else
                DestroyTextDialog(playerId, TEXTDIALOG_OPINION.id);
        }

        if (response.id == TEXTDIALOG_TESTTAB.id)
        {
            SendClientMessage(playerId, "#ffffff", response.selectedLine + " " + response.selectedButton);
            if (response.selectedButton == 2)
            {
                DestroyTextDialog(playerId, TEXTDIALOG_TESTTAB.id);
            }
        }
    }

    public override void OnSecondUpdate()
    {
        NetworkIdentity[] Players = GetAllPlayers();
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerUpdate(Players[i].netId);
        }
    }

    void PlayerUpdate(uint playerId)
    {
        if (characters.ContainsKey(playerId))
        {
            RPCharacter rpCharacter = new RPCharacter();
            rpCharacter = characters[playerId];
            rpCharacter.timeSpentOnServer = rpCharacter.timeSpentOnServer + 1;
            rpCharacter.timeSpentOnServerSinceLogin = rpCharacter.timeSpentOnServerSinceLogin + 1;
            characters[playerId] = rpCharacter;
            Utils.SaveRPCharacter(playerId);
        }
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

        // On teste le véhicule appartient au joueur
        if (IsPlayerVehicle(playerId, vehicleId))
        {

        }
        else
        {
            SendClientMessage(playerId, "#96121d", "Ce véhicule ne vous appartient pas.");
        }
        RPCharacter rpCharacter = characters[playerId];

        if (rpCharacter.licenseB == false)
        {
            SendClientMessage(playerId, "#96121d", "Vous n'avez pas le permis B, faites attention !");
        }

    }

    public override void OnPlayerExitVehicle(uint playerId, uint vehicleId)
    {
        base.OnPlayerExitVehicle(playerId, vehicleId);
    }

    public override void OnPlayerKeyDown(KeyCode key, uint playerId, bool onUI)
    {
        base.OnPlayerKeyDown(key, playerId, onUI);
        if (key == KeyCode.F12)
        {
            SendClientMessage(playerId, "#ffffff", "<color=red>[DEBUG]</color> Player Position : " + GetPlayerPos(playerId));
        }

        if (key == KeyCode.F)
        {
            if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_BUYCAR_01) < 1f)
            {
                // TODO : Création du dialog, validation de l'achat du véhicule
                CreateTextDialog(playerId, TEXTDIALOG_BUYCAR);
            }
            else if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_MARKET_01) < 2f)
            {
                CreateTextDialog(playerId, TEXTDIALOG_MARKET_01);
            }
        }
    }

    public override void OnPlayerMessage(string message, uint playerId)
    {
        base.OnPlayerMessage(message, playerId);
        // COMMANDES
        if (message.Substring(0, 1) == "/")
        {
            string command = message.Substring(1);
            string[] args = command.Split(char.Parse(" "));

            switch (args[0])
            {
                case "activités":
                    CreateTextDialog(playerId, TEXTDIALOG_ACTIVITES);
                    break;
                case "gps":
                    if (characters[playerId].gps)
                    {
                        CreateTextDialog(playerId, TEXTDIALOG_GPS);
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=red>Vous n'avez pas de GPS, allez en acheter un au Supermarché</color>");
                    }
                    break;
                case "atm":
                    if (GetDistance3DTextLabelFromPlayer(playerId, TEXTLABEL_ATM) < 2)
                    {
                        TextDialogJson textDialog = TEXTDIALOG_ATM;
                        RPCharacter rpCharacter = characters[playerId];
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
                    if (GetPlayerHealth(playerId) <= 0)
                    {
                        RPCharacter rpCharacter = characters[playerId];
                        rpCharacter.health = 100;
                        SetRPCharacter(playerId, JsonUtility.ToJson(rpCharacter));
                        SetPlayerPos(playerId, new Vector3(880, 165, 1219));
                    }
                    break;
                case "takedamage":
                    RPCharacter _rpCharacter = characters[playerId];
                    _rpCharacter.health -= 10;
                    SetRPCharacter(playerId, JsonUtility.ToJson(_rpCharacter));
                    break;
                case "fly":
                    if (GetAccount(players[playerId]["steamId"]).adminLevel > 1)
                    {
                        SetPlayerFly(playerId, !GetPlayerFly(playerId));
                    }
                    else
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
                case "crier":
                case "c":
                    if (args.Length > 1)
                        Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + " (" + playerId + ") crie: " + message.Substring(args[0].Length + 1), 15f);
                    else
                        SendClientMessage(playerId, "#ffffff", "USAGE: /c(rier) [MESSAGE]");
                    break;
                case "ch":
                case "chuchoter":
                    if (args.Length > 1)
                        Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + "(" + playerId + ") chuchote: " + message.Substring(args[0].Length + 1), 2f);
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

                    SendClientMessage(playerId, "#ffffff", "<color=orange>Permis B : " + permisB + " | Argent en poche : " + characters[playerId].money + "€</color>");
                    SendClientMessage(playerId, "#ffffff", "<color=orange>Argent en banque : " + characters[playerId].bank + "€ | Heures passées sur le serveur : " + hoursSpend + "h</color>");
                    break;
                case "n":
                case "nouveau":
                    if (!characters[playerId].isNewbieChatMuted)
                    {
                        SendBroadcastMessage("#ffffff", "<color=#38c95a>(Salon Nouveau) " + Utils.GetRPName(playerId) + " (" + playerId + ") : " + message.Substring(args[0].Length + 1) + "</color>");
                    }
                    break;
                case "mutenouveau":
                    RPCharacter _rpCharacter1 = characters[playerId];
                    _rpCharacter1.isNewbieChatMuted = !_rpCharacter1.isNewbieChatMuted;
                    if (!_rpCharacter1.isNewbieChatMuted)
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=#38c95a>Salon nouveau activé</color>");
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "<color=#38c95a>Salon nouveau désactivé</color>");
                    }
                    characters[playerId] = _rpCharacter1;
                    break;
                case "creerpnj":
                    if (args.Length == 8)
                    {
                        int hairId = int.Parse(args[1]);
                        int shoesId = int.Parse(args[2]);
                        int tshirtId = int.Parse(args[3]);
                        int vestId = int.Parse(args[4]);
                        int pantsId = int.Parse(args[5]);
                        int glovesId = int.Parse(args[6]);
                        int backpackId = int.Parse(args[7]);
                        uint npcId = CreateNPC(GetPlayerPos(playerId), GetPlayerRot(playerId), hairId, tshirtId, vestId, glovesId, pantsId, shoesId, backpackId);
                        SendClientMessage(playerId, "#ffffff", "PNJ CRÉÉ : " + npcId);
                        SetNPCUpText(npcId, "Jinx (" + npcId + ")");
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "USAGE: /creerpnj [HAIRID] [SHOESID] [TSHIRTID] [VESTID] [PANTSID] [GLOVESID] [BACKPACKID]");
                    }
                    break;
                case "modifierpnj":
                    if (args.Length == 10)
                    {
                        uint npcId = uint.Parse(args[1]);
                        int hairId = int.Parse(args[2]);
                        int shoesId = int.Parse(args[3]);
                        int tshirtId = int.Parse(args[4]);
                        int vestId = int.Parse(args[5]);
                        int pantsId = int.Parse(args[6]);
                        int glovesId = int.Parse(args[7]);
                        int backpackId = int.Parse(args[8]);
                        int keepPosition = int.Parse(args[9]);
                        if (keepPosition == 1)
                        {
                            UpdateNPC(npcId, GetNPCPos(npcId), GetNPCRot(npcId), hairId, tshirtId, vestId, glovesId, pantsId, shoesId, backpackId);
                        }
                        else
                        {
                            UpdateNPC(npcId, GetPlayerPos(playerId), GetPlayerRot(playerId), hairId, tshirtId, vestId, glovesId, pantsId, shoesId, backpackId);
                        }

                        SendClientMessage(playerId, "#ffffff", "PNJ MODIFIÉ : " + npcId);
                        SetNPCUpText(npcId, "Jinx (" + npcId + ")");
                    }
                    else
                    {
                        SendClientMessage(playerId, "#ffffff", "USAGE: /modifierpnj [NPCID] [HAIRID] [SHOESID] [TSHIRTID] [VESTID] [PANTSID] [GLOVESID] [BACKPACKID] [KEEPPOSITION]");
                    }
                    break;
                case "setnpcpos":
                    if (args.Length > 1)
                    {
                        uint npcId = uint.Parse(args[1]);
                        SetNPCDestination(npcId, GetPlayerPos(playerId));
                    }
                    break;
                default:
                    SendClientMessage(playerId, "#ffffff", "<color=red>Cette commande n'est pas dans notre base de données</color>");
                    break;
            }
        }
        else
        {
            Utils.SendDistanceMessage(GetPlayerPos(playerId), Utils.GetRPName(playerId) + " (" + playerId + ") dit: " + message, 7.5f);
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
        RPCharacter rpCharacter = new RPCharacter();
        rpCharacter = Utils.GetRPCharacterFromFile(playerId);
        characters.Add(playerId, rpCharacter);
        CreateTextDialog(playerId, TEXTDIALOG_WELCOME);

        // Create player UI
        PlayerText playerUI = new PlayerText();
        playerUI.alignment = 7;
        playerUI.textAlignment = 6;
        playerUI.position = new Vector2(305.7f, 120.7f);
        playerUI.size = new Vector2(581.5f, 191.4f);
        playerUI.text = rpCharacter.firstname + " " + rpCharacter.lastname + "\n" + "Argent: <color=orange>" + rpCharacter.money + "€</color> \n" + "Métier: Sans emploi";
        players[playerId].Add("playerUI", PlayerTextDrawCreate(playerId, playerUI).ToString());
        // End player UI

        SetPlayerUpText(playerId, Utils.GetRPName(playerId) + " (" + playerId + ")");

        if (players[playerId].ContainsKey("isFirstConnect"))
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

        if (rpCharacter.health <= 0)
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
        TEXTLABEL_MARKET_01 = Create3DTextLabel("<color=orange>F</color> pour consulter la liste des produits", new Vector3(791.4f, 165, 1146.3f));
        TEXTLABEL_ATM = Create3DTextLabel("<color=orange>/atm</color> pour retirer ou déposer de l'argent", new Vector3(821.2f, 166.5f, 1069.6f));
        TEXTLABEL_BUS_STOP_01 = Create3DTextLabel("<color=orange>F</color> pour voir les bus en service", new Vector3(811.2f, 165.9f, 1077.8f));
        TEXTLABEL_JOB_ELECTECHNICIAN_01 = Create3DTextLabel("<color=orange>F</color> pour inspecter l'armoire électrique", new Vector3(839.2f, 166.5f, 1069.4f));
        TEXTLABEL_JOB_ELECTECHNICIAN_02 = Create3DTextLabel("<color=orange>F</color> pour inspecter l'armoire électrique", new Vector3(841.6f, 166.5f, 1069.4f));
        TEXTLABEL_PHONE = Create3DTextLabel("<color=orange>/t appel</color> pour utiliser la cabine téléphonique", new Vector3(812.3f, 165.9f, 1069f));
        TEXTLABEL_TOWNHALL_BUSINESS = Create3DTextLabel("<color=orange>F</color> pour créer une entreprise", new Vector3(824.6f, 166.5f, 1074.4f));
        TEXTLABEL_TOWNHALL_JOBS = Create3DTextLabel("<color=orange>F</color> pour consulter la liste des métiers", new Vector3(827.2f, 166.5f, 1076.5f));
    }
}