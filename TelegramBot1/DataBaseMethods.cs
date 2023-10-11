using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CETmsgr.dbutils
{
    internal class DataBaseMethods
{
        // метод для проверки есть ли в базе данных юзер с таким же айди и если нету то создает нового с этим айди
        public static async Task FindOrAddUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.Where(u => u.TgChatId == id).FirstOrDefault(); 

                if (user == null)
                {
                    user = new User { TgChatId = id };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            
            }
        }
    }
}