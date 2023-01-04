using System;
using Telegram.Bot;
using Telegram;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Polling;
using System.Threading;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.IO;

namespace LtsFriendBot
{
    class Program
    {
        public static List<string> Paras = new List<string>();
        public static List<string> FinderSession = new List<string>();

        public static string Helper = "Helping";
        public static string GetGroup = "Group";
        public static string ViperStatusGet = "Get VIP";
        public static string StartDisscussion = "Find friends";
        public static string EndProcessing = "Stop find";
        public static string EndDisscuse = "Stop dialog";
        public static string GetName = "Invite to lc";
        public static string Backed = "/goback";
        public static string Hucks = "/huck";
        public static InlineKeyboardMarkup lang = new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithUrl("Feedback", "link") });
        public static InlineKeyboardMarkup getGrouped_1 = new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithUrl("Chanel", "Your group"), InlineKeyboardButton.WithUrl("Developer", "Your username") });

        public static IReplyMarkup GetButtonPlayer()
        {
            return new ReplyKeyboardMarkup(new[] {
            new List<KeyboardButton> { new KeyboardButton(EndDisscuse), new KeyboardButton(GetName)
            }})
            {
                ResizeKeyboard = true,
            };
        }
        public static IReplyMarkup GetButtonProcessing()
        {
            return new ReplyKeyboardMarkup(new[] {
            new List<KeyboardButton> { new KeyboardButton(EndProcessing)
            }})
            {
                ResizeKeyboard = true,
            };
        }
        public static IReplyMarkup GetMenuButtons()
        {
            return new ReplyKeyboardMarkup(new[] {
            new List<KeyboardButton> { new KeyboardButton(StartDisscussion) }, new List<KeyboardButton>{ new KeyboardButton(GetGroup), new KeyboardButton(Helper)
                 },new List<KeyboardButton>{ new KeyboardButton(ViperStatusGet)} })
            {
                ResizeKeyboard = true,
            };
        }
        public async static void EndDis(string controll, string thisid, ITelegramBotClient client)
        {

            for (int i = 0; i < FinderSession.Count; i += 1)
            {
                if (FinderSession[i] == controll)
                {
                    FinderSession.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < Paras.Count; i += 1)
            {
                string[] d = Paras[i].Split(" ");
                string a1 = d[0];
                string a2 = d[1];
                for (int t = 0; t < d.Length; t += 1)
                {
                    if (d[t] == thisid)
                    {
                        Console.WriteLine($"End session: {a1} && {a2} [Date: {DateTime.Now.ToString()}]");
                        await client.SendTextMessageAsync(a1, $"The interlocutor left the chat \n\n /start - restart bot \n /huck - report", replyMarkup: GetMenuButtons());
                        await client.SendTextMessageAsync(a2, $"The interlocutor left the chat \n\n /start - restart bot \n /huck - report", replyMarkup: GetMenuButtons());

                        try
                        {
                            Paras.RemoveAt(i);
                        }
                        catch
                        {
                            Console.WriteLine("EndDis");
                        }
                        break;
                    }
                }
            }
        }
        private async static Task Updatere(ITelegramBotClient botclient, Update update, CancellationToken arg3)
        {
            Message message = update.Message;
            if (message != null && message.Text != null &&
                message.Chat.Id != 1321

            )
            {
                for (int i = 0; i < Paras.Count; i += 1)
                {
                    string[] desher = Paras[i].Split(" ");
                    for (int der = 0; der < desher.Length; der += 1)
                    {
                        if (message.Chat.Id.ToString() == desher[der])
                        {
                            if (der == 0)
                            {
                                if (message.Text != GetName && message.Text != Hucks && message.Text != Backed)
                                {
                                    await botclient.SendTextMessageAsync(desher[1], message.Text, replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == GetName)
                                {
                                    await botclient.SendTextMessageAsync(desher[1], "Я приглашаю тебя lc @" + message.Chat.Username, replyMarkup: GetButtonPlayer());
                                    await botclient.SendTextMessageAsync(desher[0], "Anonim telegram bot:You invited the interlocutor to lc", replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == Hucks)
                                {
                                    Console.WriteLine($"\n//////////////////////////////////\n[DATA: {DateTime.Now} | FROM: {desher[0]}] SPAM id = {desher[1]}\n//////////////////////////////////\n");
                                    await botclient.SendTextMessageAsync(desher[0], "@letsgobefriendsbot: Complaint successfully sent for review", replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == Backed)
                                {
                                    await botclient.SendPhotoAsync(desher[0], photo: "https://drive.google.com/file/d/1zHLNY7MOjmOaVNO2UjcQSEFl2JmyhzrS/view?usp=share_link", caption: $"You do not have VIP status, in order to use this function you need VIP, you can purchase it after completing the dialog", replyMarkup: GetButtonPlayer());
                                }
                            }
                            else if (der == 1)
                            {
                                if (message.Text != GetName && message.Text != Hucks && message.Text != Backed)
                                {
                                    await botclient.SendTextMessageAsync(desher[0], message.Text, replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == GetName)
                                {
                                    await botclient.SendTextMessageAsync(desher[0], "Приглашаю тебя в ЛС @" + message.Chat.Username, replyMarkup: GetButtonPlayer());
                                    await botclient.SendTextMessageAsync(desher[1], "Анонимный чат бот: Вы пригласили собеседника в ЛС", replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == Hucks)
                                {
                                    Console.WriteLine($"\n//////////////////////////////////\n[DATA: {DateTime.Now} | FROM: {desher[1]}] SPAM id = {desher[0]}\n//////////////////////////////////\n");
                                    await botclient.SendTextMessageAsync(desher[1], "Анонимный чат бот: Жалоба успешно отправленна на расмотрение", replyMarkup: GetButtonPlayer());
                                }
                                else if (message.Text == Backed)
                                {
                                    await botclient.SendPhotoAsync(desher[1], photo: "https://drive.google.com/file/d/1zHLNY7MOjmOaVNO2UjcQSEFl2JmyhzrS/view?usp=share_link", caption: $"У вас отсуцтвует VIP статус, вы можете приобрести его после завершения диалога", replyMarkup: GetButtonPlayer());
                                }
                            }
                        }
                    }
                }
                if (message.Text == ViperStatusGet)
                {
                    await botclient.SendPhotoAsync(message.Chat.Id, photo: "https://drive.google.com/file/d/1-MQ9HVGjf_Ckm2P1mbelxklkEQgPMn96/view?usp=share_link", caption: $"??Какие возможности вы получите: \n\n??-отсуцтвие рекламы\n??-быстрый поиск собеседника\n??-возврат собеседника\n??-получение контактов собеседника\n??-отправка видео, фото, кружки", replyMarkup: lang);
                }
                if (message.Text == "/start")
                {
                    string name;
                    if (message.Chat.FirstName.Length > 1) name = message.Chat.FirstName;
                    else name = "пользователь";
                    Console.WriteLine($"....Пользователь запустил(а) бота [DATE: {DateTime.Now}]user - {message.Chat.FirstName}");
                    await botclient.SendPhotoAsync(message.Chat.Id, photo: "https://drive.google.com/file/d/1lg7OzV8CZ5hMzUCe2Wu9Bw4pkTigMRKG/view?usp=sharing", caption: $"Привет {name}, я помогу тебе найти друга или подругу, для поиска людей нажми на кнопку 'Искать собеседника' ", replyMarkup: GetMenuButtons());
                    EndDis(message.Chat.Id.ToString(), message.Chat.Id.ToString(), botclient);
                }
                else
                {
                    if (message.Text == StartDisscussion)
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "?? Поиск собеседника...", replyMarkup: GetButtonProcessing());
                        Console.WriteLine($"--- Поиск Собеседника[DATE: {DateTime.Now}] - data info|| id = {message.Chat.Id}  ||  {message.Chat.FirstName}");
                        bool controller = false;
                        for (int i = 0; i < FinderSession.Count; i += 1)
                        {
                            if (FinderSession[i] == message.Chat.Id.ToString())
                            {
                                controller = true;
                                break;
                            }
                        }
                        if (!controller)
                        {
                            FinderSession.Add(message.Chat.Id.ToString());
                        }
                        if (FinderSession.Count >= 2)
                        {
                            string d = FinderSession[0] + " " + FinderSession[1];
                            Paras.Add(d);

                            for (int i = 0; i < FinderSession.Count; i += 1)
                            {
                                if (FinderSession[i] == d.Split(" ")[0])
                                {
                                    await botclient.SendPhotoAsync(d.Split(" ")[0], photo: "https://drive.google.com/file/d/1xilik9Py68l7kHTrjafuxZIGtsctsaFz/view?usp=share_link", caption: $" /start - перезапустить бота \n /huck - пожаловатся \n /goback - вернуть собеседника ", replyMarkup: GetButtonPlayer());
                                    FinderSession.RemoveAt(i);
                                }
                                if (FinderSession[i] == d.Split(" ")[1])
                                {
                                    await botclient.SendPhotoAsync(d.Split(" ")[1], photo: "https://drive.google.com/file/d/1xilik9Py68l7kHTrjafuxZIGtsctsaFz/view?usp=share_link", caption: $" /start - перезапустить бота \n /huck - пожаловатся \n /goback - вернуть собеседника ", replyMarkup: GetButtonPlayer());
                                    FinderSession.RemoveAt(i);
                                }
                            }
                        }
                    }
                    if (message.Text == EndProcessing || message.Text == EndDisscuse)
                    {
                        EndDis(message.Chat.Id.ToString(), message.Chat.Id.ToString(), botclient);
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Сессия окончена", replyMarkup: GetMenuButtons());
                    }
                    if (message.Text == GetGroup)
                    {
                        await botclient.SendPhotoAsync(message.Chat.Id, photo: "https://drive.google.com/file/d/1FmVEtSgkygJoJ7TNDMUK3GC1JbQfr6o-/view?usp=share_link", caption: "Сообщество джедаев:", replyMarkup: getGrouped_1);
                    }
                    if (message.Text == Helper)
                    {
                        await botclient.SendPhotoAsync(message.Chat.Id, photo: "https://drive.google.com/file/d/1xQ8SptIgRw5GSoiMZ0om0Q4eQwCwG7QZ/view?usp=sharing", caption: "О боте: \n\n /start - перезапустить бота\n /goback - вернуть собеседнка\n/huck - пожаловатся на собеседника\n ? Сообщество - кидает ссылку на нашу группу\n ?? Получить VIP - покупка VIP\n ?? Пригласить в ЛС - отправляет собеседнику ваш username", replyMarkup: GetMenuButtons());
                    }
                }
            }
            else if (message != null)
            {
                try
                {
                    await botclient.SendPhotoAsync(message.Chat.Id, photo: "https://drive.google.com/file/d/1-MQ9HVGjf_Ckm2P1mbelxklkEQgPMn96/view?usp=share_link", caption: $"??Какие возможности вы получите: \n\n??-отсуцтвие рекламы\n??-быстрый поиск собеседника\n??-возврат собеседника\n??-получение контактов собеседника\n??-отправка видео, фото, кружки", replyMarkup: lang);
                }
                catch
                {
                    Console.WriteLine("GlobalExept");
                }
            }
        }
        private async static Task Exepter(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine($"[DATE: {DateTime.Now}] Method: Exepter(); \n Start recurcive \n");
            StartProgram();
        }
        public static void StartProgram()
        {
            try
            {
                var bot = new TelegramBotClient("5790659283:AAGgnVDEf0uHYdqmX9K0ndYTe7EWJXWgfao");
                Console.WriteLine("Starting server...");
                bot.StartReceiving(Updatere, Exepter);
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("StartExept");
                StartProgram();
            }
        }
        static void Main(string[] args) => StartProgram();
    }
}