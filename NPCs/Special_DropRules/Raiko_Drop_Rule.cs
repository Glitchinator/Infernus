using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria;

namespace Infernus.NPCs.Special_DropRules
{
    public class Raiko_Drop_Rule : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public Raiko_Drop_Rule()
        {
            Description ??= Language.GetOrRegister("drops after raiko has been defeated");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return InfernusSystem.downedRaiko;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Description.Value;
        }
    }
}
