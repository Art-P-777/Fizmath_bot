using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Fizmath_bot.commands;
using Fizmath_bot.slashCommands;
using Fizmath_bot.config;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices.ComTypes;
using DSharpPlus.Interactivity.Extensions;

namespace Fizmath_bot
{
    internal class Program
    {
        public static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        static async Task Main(string[] args)
        {
            //читаем конфиг с токеном
            var jsonReader = new JsonReader();
            await jsonReader.ReadJson();

            //настраиваем конфиги
            var dsConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Client = new DiscordClient(dsConfig);

            //реагирование на события
            Client.Ready += Client_Ready;
            Client.GuildMemberAdded += GuildMemberAdded;
            Client.GuildMemberRemoved += GuildMemberRemoved;
            Client.ComponentInteractionCreated += Client_ComponentInteractionCreated;


            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfig = Client.UseSlashCommands();

            //регестрируем, к каким классам будет обращаться бот
            Commands.RegisterCommands<Bot_Commands>();
            slashCommandsConfig.RegisterCommands<Bot_SlashCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        //выдача роли с кнопок
        private static async Task Client_ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            if(args.Interaction.Data.CustomId == "btn_gum")
            {
                foreach(var mem in args.Guild.Members)
                {
                    if(mem.Value.Mention == args.User.Mention)
                    {
                        await mem.Value.BanAsync(reason: "Ты, паршивый гуммунитарий, навсегда выгнан из сервера великой партии Физматов!");
                    }
                }
            }
            else
            {
                string role;
                switch (args.Interaction.Data.CustomId)
                {
                    case "btn_Fizmath":
                        role = "Физмат"; break;
                    case "btn_Sozek":
                        role = "Импостор(Соц-Эк)"; break;
                    case "btn_Himbio":
                        role = "Импостор(Химбик)"; break;
                    case "btn_fortnite":
                        role = "Фортнайтер"; break;
                    case "btn_roblox":
                        role = "Роблоксер"; break;
                    case "btn_Helldiver":
                        role = "Хеллдайвер"; break;
                    case "btn_minecraft":
                        role = "Майнкруфтер"; break;
                    case "btn_brawl":
                        role = "Бравлер"; break;
                    default:
                        await args.Channel.SendMessageAsync($"customId: {args.Interaction.Data.CustomId}");
                        throw new Exception("Interaction Custom Id \n\n");
                }

                foreach (var GuildRole in args.Guild.Roles)
                {
                    if (GuildRole.Value.Name == role)
                    {
                        foreach (var mem in args.Guild.Members)
                        {
                            if (mem.Value.Username == args.User.Username)
                            {
                                await mem.Value.GrantRoleAsync(GuildRole.Value);
                                break;
                            }
                        }
                    }
                }
            }           
        }

        //новый участник
        private static async Task GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            var Embed = new DiscordEmbedBuilder()
            {
                Title = "В партии новый учатник",
                Description = $"{e.Member.Mention}, добро пожаловать в партию! Скоро тебе выдадут роль!",
                Color = DiscordColor.Blue,
                Timestamp = System.DateTime.Now
            };

            await sender.SendMessageAsync(await sender.GetChannelAsync(id: 1250718246580125699), embed: Embed);
        }

        //участник уходит
        private static async Task GuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            var Embed = new DiscordEmbedBuilder()
            {
                Title = $"{e.Member.Username} стал гуммунитарием",
                Color = DiscordColor.Black,
                Timestamp = System.DateTime.Now
            };

            await sender.SendMessageAsync(await sender.GetChannelAsync(id: 1250718246580125699), embed: Embed);
        }


        //Не трогать
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args) => Task.CompletedTask;
    }
}
