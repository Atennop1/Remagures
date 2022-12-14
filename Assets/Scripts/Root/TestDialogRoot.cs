using System.Collections.Generic;
using Remagures.Dialogs.CharacterInterfaces;
using Remagures.Dialogs.Model;
using Remagures.Dialogs.Model.Data;
using UnityEngine;

namespace Remagures.Root
{
    public class TestDialogRoot : CompositeRoot
    {
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Sprite _dudeSprite;
        
        public override void Compose()
        {
            var playerSpeakerInfo = new DialogSpeakerInfo("Линк", _playerSprite, DialogLayoutType.Right);
            var dudeSpeakerInfo = new DialogSpeakerInfo("Чувак", _dudeSprite, DialogLayoutType.Left);
            var nullChoicesArray = new DialogChoice[1];

            var firstChoiceOfChestRequest = new DialogChoice("Да");
            var secondChoiceOfChestRequest = new DialogChoice("Нет");

            var lineOfChestRequest = new DialogLine(
                "Привет, я слышал, что ты герой. Можешь открыть сундук около дома? Тот, что ближе.", 
                dudeSpeakerInfo, new List<DialogChoice> { firstChoiceOfChestRequest, secondChoiceOfChestRequest }
            );
            var chestRequestDialog = new Dialog("CanYouOpenTheChest", lineOfChestRequest);

            var firstLineOfPlayerRefusal = new DialogLine("Нет, сам открывай!", playerSpeakerInfo, nullChoicesArray);
            var secondLineOfPlayerRefusal = new DialogLine("Эх, какой из тебя герой тогда?", dudeSpeakerInfo, nullChoicesArray);
            var playerRefusalDialog = new Dialog("PlayerRefusal", firstLineOfPlayerRefusal, secondLineOfPlayerRefusal);

            var firstLineOfPlayerAgreement = new DialogLine("Конечно, без проблем", playerSpeakerInfo, nullChoicesArray);
            var secondLineOfPlayerAgreement = new DialogLine("Спасибо тебе!", dudeSpeakerInfo, nullChoicesArray);
            var playerAgreementDialog = new Dialog("PlayerAgreement", firstLineOfPlayerAgreement, secondLineOfPlayerAgreement);

            var lineOfWaitingDialog = new DialogLine("Я жду открытия сундука!", dudeSpeakerInfo, nullChoicesArray);
            var waitingDialog = new Dialog("Waiting", lineOfWaitingDialog);
            
            var lineOfOffendedDialog = new DialogLine("Я обиделся на тебя!", dudeSpeakerInfo, nullChoicesArray);
            var offendedDialog = new Dialog("Offended", lineOfOffendedDialog);
            
            var lineOfThankDialog = new DialogLine("Спасибо за открытие сундука, герой!", dudeSpeakerInfo, nullChoicesArray);
            var thankDialog = new Dialog("Thank", lineOfThankDialog);

            var dialogsList = new DialogsList<IDudeDialogCharacter>(chestRequestDialog, offendedDialog,
                thankDialog, waitingDialog, playerRefusalDialog, playerAgreementDialog);
            
            if (dialogsList.CurrentDialog == null)
                dialogsList.SwitchToDialog(chestRequestDialog);
        }
    }
}