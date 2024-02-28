using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Server Rewards NPC Tags", "nuGGGz", "0.1.0")]
    [Description("Allows for depositing and rewarding players for NPC ID tags")]
    class ServerRewardsNPCTags : CovalencePlugin
    {
        [PluginReference]
        private Plugin ServerRewards;
        private Plugin MonumentAddons;

        DropBox dropBox;

        // make a dictionary to store shortnames and their respective rewards
        Dictionary<string, int> Rewards = new Dictionary<string, int>();

        // define rewards
        private void DefineRewards()
        {
            // add the rewards to the dictionary
            Rewards.Add("greenidtag", 5);
            Rewards.Add("yellowidtag", 10);
            Rewards.Add("blueidtag", 15);
            Rewards.Add("redidtag", 20);
        }

        // init the plugin
        private void Init()
        {
            // get the dropbox
            dropBox = GetDropBox();
            DefineRewards();
            // check if the dropbox is null
            if (dropBox == null)
            {
                Puts("Dropbox is null");
                // retry getting the dropbox every 10 seconds
                timer.Every(10, () =>
                {
                    dropBox = GetDropBox();
                    if (dropBox == null)
                    {
                        Puts("Dropbox is null");
                    }
                    else
                    {
                        Puts("Dropbox found");
                    }
                });
            }
            else
            {
                Puts("Dropbox found");
            }
        }

        object OnItemSubmit(Item item, DropBox box, BasePlayer player)
        {
            // check if the box is a dropbox
            if (box == null)
            {
                return null;
            }
            if (player == null)
            {
                return null;
            }

            // get the entity from the dropbox
            var entity = box.GetEntity();
            string boxID = entity.net.ID.ToString();
            // check if the dropboxid matches dropBox.net.ID
            if (boxID == dropBox.net.ID.ToString())
            {
                if (item.info.shortname.Contains("idtag"))
                {
                    Puts(item.info.shortname);

                    // get the item quantity
                    int quantity = item.amount;
                    // get the item shortname   
                    string shortName = item.info.shortname;
                    // puts the item quantity and shortname
                    Puts(quantity + " " + shortName);
                    // get the reward from the dictionary
                    int reward = Rewards[shortName];
                    // get the player's name
                    string playerName = player.displayName;
                    // get the player's id
                    string playerId = player.UserIDString;
                    // get the total rewards value
                    int totalRewards = reward * quantity;
                    // put the name, and total rewards to the console
                    if ((bool)(ServerRewards?.Call("AddPoints", playerId, totalRewards)))
                    {
                        Effect.server.Run("assets/prefabs/misc/casino/slotmachine/effects/payout_jackpot.prefab", player.transform.position);
                        player.ChatMessage("You just received <color=#FEC601>" + totalRewards + "</color> rewards points!");
                        item.Remove();
                    }
                    return null;
                }
                player.ChatMessage("This item cannot be deposited for RP");
                return false;
            }

            return null;
        }

        public DropBox GetDropBox()
        {
            //get the monument that contains the string "compound" in the name
            var monument = TerrainMeta.Path.Monuments.Where(x => x.name.Contains("compound")).FirstOrDefault();
            // get all dropbox entities that are located in the compound monument
            var dropBoxes = UnityEngine.Object.FindObjectsOfType<DropBox>().Where(x => monument.IsInBounds(x.transform.position)).ToList();
            // return the first dropbox in the list
            return dropBoxes.FirstOrDefault();
        }

        void OnMonumentEntitySpawned(BaseEntity entity, MonoBehaviour monument, Guid guid)
        {
            // check if the entity is a dropbox by comparing the entity name to "assets/prefabs/deployable/dropbox/dropbox.prefab"
            if (entity.name == "assets/prefabs/deployable/dropbox/dropbox.prefab")
            {
                // get the dropbox entity
                dropBox = entity.GetComponent<DropBox>();
                // get the monumnent name
                string monumentName = monument.name;
                // check if the monument name is "assets/bundled/prefabs/autospawn/monument/medium/compound.prefab"
                if (monumentName == "assets/bundled/prefabs/autospawn/monument/medium/compound.prefab")
                {
                    // assign the dropbox to the global dropbox variable
                    dropBox = entity.GetComponent<DropBox>();
                }
            }
        }
    }
}
