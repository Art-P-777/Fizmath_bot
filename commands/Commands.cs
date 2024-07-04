using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using DSharpPlus.SlashCommands;

namespace Fizmath_bot.commands
{
    internal class Bot_Commands : BaseCommandModule
    {
        [Command("get_channel_id")]
        public async Task ChannelId(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Channel ID: {ctx.Channel.Id}");
        }

        [Command("get_parent_id")]
        public async Task CategoryId(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Channel ID: {ctx.Channel.ParentId}");
        }

        [Command("AutoArchive")]
        public async Task AutoArchive(CommandContext ctx, long num)
        {
            await ctx.Channel.SendMessageAsync($"(AutoArchiveDuration){num} : {(AutoArchiveDuration)num}");
        }

        [Command("quest_1")]
        public async Task Quest_1(CommandContext ctx)
        {
            bool IsAdmin = false;
            foreach(var role in ctx.Member.Roles)
            {
                if (role.Name == "админ")
                {
                    IsAdmin = true;
                    break;
                }
            }
            if (!IsAdmin)
            {
                await ctx.RespondAsync("Ах ты, подглядел команду, но я умнее тебя, у меня памяти, хех, 16 килобайт");
            }
            else
            {
                await ctx.Channel.DeleteMessageAsync(ctx.Message);

                DiscordButtonComponent[] buttons = new DiscordButtonComponent[] {
                    new DiscordButtonComponent(ButtonStyle.Primary, "btn_fortnite", "Fortnite"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "btn_roblox", "Roblox"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "btn_Helldiver", "Helldivers"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "btn_minecraft", "Minecraft"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "btn_brawl", "Brawl Stars")
                };

                var msg = new DiscordMessageBuilder()
                    .WithEmbed(new DiscordEmbedBuilder()
                    {
                        Title = "Выбери игру, в которую ты играешь",
                        Description = "Ты получишь роль, соответствующию игре, также ты получишь доступ к удобным пати\n\n" +
                        "Примечание: если написано ошибка взаимодействия, значит вам выдана роль",
                        Color = DiscordColor.Azure
                    })
                    .AddComponents(buttons);

                await ctx.Channel.SendMessageAsync(msg);
            }
        }

        [Command("quest_2")]
        public async Task Quuest_2(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();

            //DiscordButtonComponent[] buttons = new DiscordButtonComponent[] {
            //    new DiscordButtonComponent(ButtonStyle.Primary, "btn_Fizmath", "Физмат"),
            //    new DiscordButtonComponent(ButtonStyle.Secondary, "btn_Sozek", "Импостор(Соцэк)"),
            //    new DiscordButtonComponent(ButtonStyle.Success, "btn_himbio", "Импостор(Химбик)"),
            //    new DiscordButtonComponent(ButtonStyle.Danger, "btn_gum", "Гуммунитарий"),
            //};
            List<DiscordSelectComponentOption> options = new List<DiscordSelectComponentOption>
            {
                new DiscordSelectComponentOption("Физмат","btn_Fizmath"),
                new DiscordSelectComponentOption("Химбио","btn_Himbio"),
                new DiscordSelectComponentOption("Соцэк","btn_Sozek"),
                new DiscordSelectComponentOption("Гуммунитарий","btn_gum")
            };
            DiscordSelectComponent selector = new DiscordSelectComponent("profile", "Выбери свой профиль", options);


            var msg = new DiscordMessageBuilder()
                .WithEmbed(new DiscordEmbedBuilder()
                {
                    Title = "Выбери свой профиль",
                    Description = "Ты получишь роль, соответствующию твоему профилю\n\n" +
                    "Примечание: если написано ошибка взаимодействия, значит вам выдана роль",
                    Color = DiscordColor.Aquamarine
                })
                .AddComponents(selector/*buttons*/);

            await ctx.Channel.SendMessageAsync(msg);
        }

        [Command("select_beta")]
        public async Task Selector(CommandContext ctx)
        {
            List<DiscordSelectComponentOption> opt = new List<DiscordSelectComponentOption>
            {
                new DiscordSelectComponentOption("1","2")
            };
            DiscordSelectComponent selector = new DiscordSelectComponent("sel", "beta", opt);

            var msg = new DiscordMessageBuilder().WithContent("test").AddComponents(selector);

            await ctx.Channel.SendMessageAsync(msg);
        }
    }
}
