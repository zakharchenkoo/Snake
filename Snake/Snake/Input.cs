using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Input
    {
        // Зберігання стану клавіш
        private static Hashtable keyTable = new Hashtable();

        // Перевірка, чи натиснута певна клавіша
        public static bool KeyPressed(Keys key)
        {
            // Перевірка, чи є значення для даної клавіші в Hashtable
            if (keyTable[key] == null)
            {
                return false;
            }
            // Повертаємо стан клавіші (true - натиснута, false - не натиснута)
            return (bool)keyTable[key];
        }

        // Зміна стану клавіші
        public static void ChangeState(Keys key, bool state)
        {
            // Записуємо стан клавіші в Hashtable
            keyTable[key] = state;
        }
    }
}
