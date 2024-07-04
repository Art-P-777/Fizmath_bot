using DSharpPlus.SlashCommands;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Fizmath_bot.slashCommands
{
    public class Bot_SlashCommands : ApplicationCommandModule
    {
        [SlashCommand("create_embed","Создай свой embed")]
        public async Task create_embed(InteractionContext ctx,
            [Option("Title","Напиши название embed")] string title,
            [Option("Description","Напиши текст в embed")] string description, 
            [Option("Color_HEX", "Напиши цвет в формате HEX без # (по умолчанию белый - ffffff)")] string color_HEX = "ffffff")
        {
            var Member_embed = new DiscordEmbedBuilder()
            {
                Title = title,
                Description = description,
                Color = new DiscordColor(color_HEX)
            };
            await ctx.CreateResponseAsync(String.Empty, Member_embed);
        }

        [SlashCommand("create_voice","Создай войс")]
        public async Task TaskCreateVoice(InteractionContext ctx,
            [Option("Name_Voice","Как будет называться твой войс?")] string title)
        {          
            await ctx.Guild.CreateChannelAsync(title,
                ChannelType.Voice,
                ctx.Channel.Parent);

            var Member_embed = new DiscordEmbedBuilder()
            {
                Title = $"Войс \"{title}\" создан!",
                Color = DiscordColor.CornflowerBlue
            };
            await ctx.CreateResponseAsync(String.Empty, Member_embed);
        }
    }
}
